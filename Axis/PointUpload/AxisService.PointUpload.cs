using InduLink.Abstractions;
using InduLink.Protocols.Ads;

namespace Page_switching;

// 点位下发：写独立段、等待接收确认并请求拼表；本文件不启动轴运动。
public sealed partial class AxisService
{
    private uint _waveSequence;
    private volatile uint _waveReadySequence; // 上传任务写入，页面读取；只保留服务中的这一份序号。
    private AxisTrajectoryLimits[]? _waveValidatedLimits; // 已下发表使用的四轴参数；启动前核对是否变化。

    public void ForgetWaveSignal()
    {
        lock (_waveSync)
        {
            if (WaveOperationActive) throw new InvalidOperationException("造波操作期间不能更换信号。");
            _waveReadySequence = 0;
            _waveValidatedLimits = null;
            _waveStatus = new("已导入，待发送", 0, false, false);
        }
    }

    // 所有段登记且 PLC 拼表完成后才发布 Ready；启动仍由 StartWaveAsync 单独触发。
    public Task UploadWaveAsync(WaveTrajectoryFile signal, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(signal);
        // 调用前复制一份，防止外部数组在后台上传期间被更改。
        var points = signal.Positions.ToArray();
        if (points.Length is < 4 or > WaveTrajectoryFile.MaximumPoints)
            throw new InvalidDataException("点位数量必须为 4～2,100,000；数值和运动限制在下发前统一预检。");
        return RunWaveOperationAsync(token => UploadWaveCoreAsync(points, token), cancellationToken);
    }

    // 先读取真实参数、预检整表；合格后才清表及写入点位，失败不会破坏 PLC 原来的表。
    private async Task UploadWaveCoreAsync(double[] points, CancellationToken token)
    {
        _waveReadySequence = 0;
        _waveValidatedLimits = null;
        var frame = await ReadWaveFrameAsync(token);
        WaveCommandChecks.ThrowIfBlocked(WaveCommandChecks.ForUpload(frame));
        _waveStatus = new("正在读取轴参数并检查点位", 0, true, false);
        var limits = await ReadWaveAxisLimitsAsync(token);
        WavePointValidator.Validate(points, limits, token);
        OperationJournal.Record("点位预检", $"四轴点位预检通过，共 {points.Length} 点，周期 2 ms，开始下发。");
        _waveNeedsStop = true;
        await WaveWriteAsync("GVL_motion.xStop", DataType.Bool, false, token);
        // 用户明确重发时只清一次组故障；不自动复位各轴报警或反复重试。
        if (frame.Error) await PulseWaveAsync("xReset", token);
        await PulseWaveAsync("xClear", token);
        await WaitWaveFrameAsync(f => f.ReceivedCount == 0 && f.LoadedCount == 0 && !f.DataReady && !f.Error,
            5000, "清除旧轨迹", token);
        _waveSequence = Math.Max(_waveSequence, Math.Max(frame.AcceptedSequence, frame.ReceivedSequence));
        do { _waveSequence = unchecked(_waveSequence + 1); }
        while (_waveSequence == 0 || _waveSequence == frame.AcceptedSequence || _waveSequence == frame.ReceivedSequence);
        await WaveWriteAsync("GVL_motion.nPointCount", DataType.UInt32, (uint)points.Length, token);
        await WaveWriteAsync("GVL_motion.nSampleTimeUs", DataType.UInt16, (ushort)2000, token);
        await WaveWriteAsync("GVL_motion.udiUploadSequence", DataType.UInt32, _waveSequence, token);
        foreach (var chunk in WaveTransferChunks.Create(points.Length))
        {
            token.ThrowIfCancellationRequested();
            await WaveAccessAsync(async (client, ct) =>
            {
                // DLL 支持批量标量写；不对 scalar 符号伪造数组切片。
                var writes = new WriteRequest[chunk.Count];
                for (int i = 0; i < writes.Length; i++)
                    writes[i] = new(client.DeviceId,
                        $"GVL_motion.aUpload[{chunk.Segment},{chunk.Axis},{chunk.Point + i}]",
                        DataType.Double, points[chunk.Offset + i]);
                try
                {
                    await client.WriteManyAsync(writes, ct);
                }
                catch (Exception ex) when (ex is not OperationCanceledException)
                {
                    throw new InvalidOperationException(
                        $"写入 GVL_motion.aUpload 段 {chunk.Segment}、轴 {chunk.Axis}、段内点 {chunk.Point}..{chunk.Point + chunk.Count - 1} 失败：{AdsDiagnostics.DescribeException(ex)}", ex);
                }
            }, token);
            _waveStatus = new("正在发送", chunk.Completed / (double)(points.Length * 4L), true, false);
            if (!chunk.EndOfSegment) continue;
            await WaveWriteAsync("GVL_motion.nUploadOffset", DataType.UInt32, (uint)(chunk.Segment * 100000), token);
            await WaveWriteAsync("GVL_motion.nUploadCount", DataType.UInt32, (uint)chunk.SegmentCount, token);
            await PulseWaveAsync("xUpload", token);
            var expected = (uint)(chunk.Segment * 100000 + chunk.SegmentCount);
            await WaitWaveFrameAsync(f => f.ReceivedSequence == _waveSequence && f.ReceivedCount == expected,
                5000, "等待本段接收确认", token);
        }
        _waveStatus = new("PLC 正在拼接", 0, true, false);
        await PulseWaveAsync("xAssemble", token);
        await WaitWaveFrameAsync(f =>
        {
            _waveStatus = new("PLC 正在拼接", f.LoadedCount / (double)points.Length, true, false);
            return f.DataReady && !f.UploadBusy && f.LoadedCount == points.Length
                && f.ReceivedSequence == _waveSequence && f.UploadStation == ControlStationCode;
        }, 30000, "PLC 拼接", token);
        _waveReadySequence = _waveSequence;
        _waveValidatedLimits = limits;
        _waveStatus = new("已发送，等待开始", 1, false, true);
    }

    // 四轴参数只读自 PLC，不在上位机另存一套默认限制，也不整体写回 aConfig。
    private async Task<AxisTrajectoryLimits[]> ReadWaveAxisLimitsAsync(CancellationToken token)
    {
        AxisTrajectoryLimits[] limits = [];
        await WaveAccessAsync(async (client, ct) => limits = await ReadAxisLimitsAsync(client, ct), token);
        return limits;
    }

    private async Task VerifyWaveAxisLimitsAsync(CancellationToken token)
    {
        var current = await ReadWaveAxisLimitsAsync(token);
        if (_waveValidatedLimits is null || !_waveValidatedLimits.SequenceEqual(current))
            throw new InvalidOperationException("PLC 轴参数在下发后发生变化，请重新发送点位，通过预检后再开始。");
    }
}
