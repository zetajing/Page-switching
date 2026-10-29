using InduLink.Abstractions;

namespace Page_switching;

public sealed record WaveOperationStatus(string Stage, double Fraction, bool Active, bool Ready, string? Error = null);

// 运行主线：核对本次表 → 使能及必要回原 → 启动和等待执行结束。
public sealed partial class AxisService
{
    private DatabaseWaveRun? _databaseRun; // 只关联本次运行的归档，不参与运动条件判断。

    public Task StartWaveAsync(CancellationToken cancellationToken = default) =>
        RunWaveOperationAsync(StartWaveCoreAsync, cancellationToken);

    // 页面无需保存第二份上传序号；直接使用服务保存的本次下发凭据解释条件。
    internal List<string> GetWaveStartBlockReasons(PlcMachineMonitor frame) =>
        WaveCommandChecks.ForStart(frame, ControlStationCode, _waveReadySequence);

    // 启动主线只有三步：核对本次点位表、准备四轴、启动并等待执行结束。
    private async Task StartWaveCoreAsync(CancellationToken token)
    {
        var frame = await ReadWaveFrameAsync(token);
        WaveCommandChecks.ThrowIfBlocked(GetWaveStartBlockReasons(frame));
        // PLC 已不做整表数值检查；参数改变后必须重新预检、下发，不能沿用旧结论。
        await VerifyWaveAxisLimitsAsync(token);
        _databaseRun = DatabaseArchive.BeginRun(_databaseDataId, null, ControlStationCode, _waveReadySequence);
        _waveNeedsStop = true;
        frame = await PrepareWaveAxesAsync(token);
        await ExecuteWaveAsync(frame.PointCount, token);
    }

    // 使能和回原分别等待 PLC 实际反馈；保持原来的命令顺序和超时。
    private async Task<PlcMachineMonitor> PrepareWaveAxesAsync(CancellationToken token)
    {
        _waveStatus = new("正在使能四轴", 0, true, false);
        await WaveWriteAsync("GVL_motion.xStop", DataType.Bool, false, token);
        await SetWaveAxisCommandsAsync(enable: true, stop: false, token);
        var frame = await WaitWaveFrameAsync(f =>
        {
            CheckPreparation(f);
            return f.Axis1.Enabled && f.Axis2.Enabled && f.Axis3.Enabled && f.Axis4.Enabled && f.AxesIdle;
        }, 10000, "四轴使能", token);
        var axes = new[] { frame.Axis1, frame.Axis2, frame.Axis3, frame.Axis4 };
        _waveStatus = new("正在回原未就绪轴", 0, true, false);
        for (int i = 0; i < axes.Length; i++)
        {
            if (axes[i].Homed) continue;
            var symbol = $"{StationPrefix}.aAxis[{i + 1}].gohome";
            await WaveWriteAsync(symbol, DataType.Bool, true, token);
            try { await Task.Delay(50, token); }
            finally { await WaveWriteAsync(symbol, DataType.Bool, false, CancellationToken.None); }
        }
        return await WaitWaveFrameAsync(f =>
        {
            CheckPreparation(f);
            return f.AxesIdle && f.Axis1.Homed && f.Axis2.Homed && f.Axis3.Homed && f.Axis4.Homed;
        }, 120000, "四轴回原", token);
    }

    private async Task ExecuteWaveAsync(uint pointCount, CancellationToken token)
    {
        var frame = await ReadWaveFrameAsync(token);
        frame.ValidateWaveContract();
        await VerifyWaveAxisLimitsAsync(token); // 回原可能耗时，再核对一次，参数相同才发启动沿。
        _waveStatus = new("PLC 检查启动条件并定位到首点", 0, true, false);
        StartRuntimeMonitoring(_waveReadySequence, frame.Heartbeat); // 只读后台任务，不增加运动启动条件。
        var monitor = RuntimeMonitoringReport;
        if (monitor is not null && monitor.Sequence == _waveReadySequence)
            _databaseRun = DatabaseArchive.AttachMonitor(_databaseRun, monitor.RunId);
        await PulseWaveAsync("xStart", token);
        await WaitWaveFrameAsync(f => f.AcceptedSequence == _waveReadySequence && f.TrajectoryBusy,
            120000, "轨迹启动确认", token);
        var sequence = _waveReadySequence;
        _waveReadySequence = 0; // 一次消费；完成或停止后必须重新发送。
        await WaitWaveFrameAsync(f =>
        {
            if (f.AcceptedSequence != sequence) throw new InvalidOperationException("PLC 执行序号已改变。");
            var fraction = f.PointCount > 1 ? f.CurrentPoint / (double)(f.PointCount - 1) : 0;
            _waveStatus = new(f.Running ? "正在造波" : "定位或退出中", fraction, true, false);
            if (f.Stopped) throw new OperationCanceledException("PLC 已停止轨迹。", token);
            return f.Done && !f.TrajectoryBusy && f.AxesIdle;
        }, Math.Max(180000, (int)(pointCount * 2L + 180000)), "造波执行", token);
        _waveStatus = new("造波完成，再次运行请重新发送", 1, false, false);
    }

    private static void CheckPreparation(PlcMachineMonitor frame)
    {
        WaveCommandChecks.ThrowIfBlocked(WaveCommandChecks.ForPreparation(frame));
    }

}
