using InduLink.Abstractions;
using InduLink.Protocols.Ads;

namespace Page_switching;

public sealed record WaveOperationStatus(string Stage, double Fraction, bool Active, bool Ready, string? Error = null);

// 轨迹会话和运行：保留原有准备、启动、停止、心跳及退出流程。
// 点位上传实现位于 PointUpload/AxisService.PointUpload.cs。
public sealed partial class AxisService
{
    private readonly object _waveSync = new();
    private readonly SemaphoreSlim _waveStopGate = new(1, 1);
    private CancellationTokenSource? _waveCancellation;
    private Task _waveTask = Task.CompletedTask;
    private volatile bool _waveActive;
    private volatile bool _waveStopRequested;
    private volatile bool _waveFailSafe;
    private volatile WaveOperationStatus _waveStatus = new("未导入", 0, false, false);
    private uint _waveHeartbeat;
    private uint? _waveLastPlcHeartbeat;
    private long _waveHeartbeatTick;
    private bool _waveHeartbeatConfirmed;
    private bool _waveNeedsStop;
    public WaveOperationStatus WaveStatus => _waveStatus;
    public bool WaveOperationActive => _waveActive || _waveStopRequested;

    // 会话任务属于服务，切换页面不会取消。停止先取消，再等原任务退出，防止迟到的启动写入。
    private Task RunWaveOperationAsync(Func<CancellationToken, Task> operation, CancellationToken cancellationToken)
    {
        lock (_waveSync)
        {
            ThrowIfDisposed();
            RequireControlAuthority();
            if (WaveOperationActive) throw new InvalidOperationException("已有造波操作正在执行。");
            _waveCancellation?.Dispose();
            _waveCancellation = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            var token = _waveCancellation.Token;
            _waveActive = true;
            _waveNeedsStop = false;
            _waveTask = Task.Run(async () =>
            {
                bool successful = false;
                try
                {
                    await WaveWriteAsync(StationPrefix + ".xWaveSession", DataType.Bool, true, token);
                    // 确认 PLC 已进入会话锁，再开始任何数据或运动操作。
                    var until = Environment.TickCount64 + 2000;
                    while (!AuthorityStatus.Locked)
                    {
                        token.ThrowIfCancellationRequested();
                        RequireControlAuthority();
                        if (Environment.TickCount64 >= until) throw new TimeoutException("PLC 未确认造波会话锁。");
                        await Task.Delay(50, token);
                    }
                    await WaveWriteAsync("GVL_motion.nCommandStation", DataType.UInt16, ControlStationCode, token);
                    _waveLastPlcHeartbeat = null;
                    _waveHeartbeatConfirmed = false;
                    await WaitWaveFrameAsync(_ => _waveHeartbeatConfirmed, 2000, "等待 PLC 心跳", token, allowError: true);
                    await operation(token);
                    successful = true;
                }
                catch (Exception ex)
                {
                    _waveReadySequence = 0;
                    _waveStatus = new(ex is OperationCanceledException ? "正在停止" : "造波操作失败", 0, true, false, ex.Message);
                    throw;
                }
                finally
                {
                    try
                    {
                        if (!successful && _waveNeedsStop) await StopWaveCoreAsync();
                        if (HasControlAuthority && !_waveFailSafe)
                            await WaveWriteAsync(StationPrefix + ".xWaveSession", DataType.Bool, false, CancellationToken.None);
                    }
                    catch (Exception ex)
                    {
                        // 停止结果不确定时停止本站心跳，让 PLC 独立失联停车；不能恢复旧动作。
                        _waveFailSafe = true;
                        _waveStatus = new("停止未确认，等待 PLC 失联停车", 0, false, false, ex.Message);
                        OperationJournal.Record("造波", _waveStatus.Stage + "：" + ex.Message);
                    }
                    finally { _waveActive = false; }
                }
            }, CancellationToken.None);
            return _waveTask;
        }
    }

    public Task StartWaveAsync(CancellationToken cancellationToken = default) => RunWaveOperationAsync(async token =>
    {
        var frame = await ReadWaveFrameAsync(token);
        if (_waveReadySequence == 0 || !frame.DataReady || frame.ReceivedSequence != _waveReadySequence
            || frame.UploadStation != ControlStationCode || frame.AcceptedSequence == _waveReadySequence)
            throw new InvalidOperationException("请先重新发送当前轨迹并等待 PLC 拼接完成。");
        CheckPreparation(frame);
        if (!frame.AxesIdle) throw new InvalidOperationException("轴尚未静止，不能开始自动准备。");
        _waveNeedsStop = true;
        _waveStatus = new("正在使能四轴", 0, true, false);
        await WaveWriteAsync("GVL_motion.xStop", DataType.Bool, false, token);
        await SetWaveAxisCommandsAsync(enable: true, stop: false, token);
        frame = await WaitWaveFrameAsync(f =>
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
        await WaitWaveFrameAsync(f =>
        {
            CheckPreparation(f);
            return f.AxesIdle && f.Axis1.Homed && f.Axis2.Homed && f.Axis3.Homed && f.Axis4.Homed;
        }, 120000, "四轴回原", token);
        _waveStatus = new("PLC 校验并定位到首点", 0, true, false);
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
        }, Math.Max(180000, (int)(frame.PointCount * 2L + 180000)), "造波执行", token);
        _waveStatus = new("造波完成，再次运行请重新发送", 1, false, false);
    }, cancellationToken);

    private static void CheckPreparation(PlcMachineMonitor frame)
    {
        if (!frame.MotionPermit) throw new InvalidOperationException("PLC 运动许可未满足。");
        if (!frame.ConfigValid) throw new InvalidOperationException("PLC 轴参数未配置或无效。");
        if (new[] { frame.Axis1, frame.Axis2, frame.Axis3, frame.Axis4 }
            .Any(axis => !double.IsFinite(axis.ActualPosition) || !double.IsFinite(axis.Speed)))
            throw new InvalidOperationException("轴位置或速度反馈无效。");
        if (frame.AxisFault || frame.Axis1.Alarm || frame.Axis2.Alarm || frame.Axis3.Alarm || frame.Axis4.Alarm)
            throw new InvalidOperationException("存在轴报警或无效反馈，请先处理故障。");
    }

    public async Task StopWaveAsync()
    {
        RequireControlAuthority();
        Task pending;
        lock (_waveSync)
        {
            if (_waveStopRequested) throw new InvalidOperationException("停止请求正在执行。");
            _waveStopRequested = true;
            _waveCancellation?.Cancel();
            pending = _waveTask;
        }
        try
        {
            try { await pending; } catch (Exception ex) { System.Diagnostics.Debug.WriteLine(ex); }
            await StopWaveCoreAsync();
        }
        finally { _waveStopRequested = false; }
    }

    private async Task StopWaveCoreAsync()
    {
        await _waveStopGate.WaitAsync();
        using var timeout = new CancellationTokenSource(30000);
        try
        {
            RequireControlAuthority();
            _waveStatus = new("正在停止，等待 PLC 停稳", 0, true, false);
            _waveReadySequence = 0;
            await WaveWriteAsync("GVL_motion.nCommandStation", DataType.UInt16, ControlStationCode, timeout.Token);
            await WaveWriteAsync("GVL_motion.xStop", DataType.Bool, true, timeout.Token);
            // 撤掉准备中的回原/点动请求，轴停止保持到实际静止，不在轨迹中抢发 MC_Stop。
            await SetWaveAxisCommandsAsync(enable: null, stop: true, timeout.Token);
            var stopFrame = await ReadWaveFrameAsync(timeout.Token);
            // 必须看到停止请求之后的新 PLC 心跳帧，不能用请求前的旧“静止”值确认。
            await WaitWaveFrameAsync(f => f.Heartbeat != stopFrame.Heartbeat
                && !f.TrajectoryBusy && !f.UploadBusy && f.AxesStopped,
                20000, "停止确认", timeout.Token, allowError: true);
            await SetWaveAxisCommandsAsync(enable: null, stop: false, timeout.Token);
            await WaveWriteAsync("GVL_motion.xStop", DataType.Bool, false, timeout.Token);
            // MC_Stop 在保持 Execute 时可能仍 Busy；停稳后撤请求，再等块退出。
            await WaitWaveFrameAsync(f => f.AxesIdle && !f.TrajectoryBusy && !f.UploadBusy,
                5000, "停止块退出", timeout.Token, allowError: true);
            await WaveWriteAsync(StationPrefix + ".xWaveSession", DataType.Bool, false, timeout.Token);
            _waveStatus = new("已停止，再次运行请重新发送", 0, false, false);
        }
        catch
        {
            _waveFailSafe = true;
            throw;
        }
        finally { _waveStopGate.Release(); }
    }

    // 程序退出先尝试停止；失败后由 PLC 的独立心跳检测接管，不自动重新连接启动。
    public async Task ShutdownWaveAsync()
    {
        if (_waveStopRequested)
        {
            var until = Environment.TickCount64 + 35000;
            while (_waveStopRequested && Environment.TickCount64 < until) await Task.Delay(50);
            return;
        }
        if (!_waveActive) return;
        try { await StopWaveAsync(); }
        catch (Exception ex) { OperationJournal.Record("造波", "退出时停止未确认：" + ex.Message); }
    }

    private Task SetWaveAxisCommandsAsync(bool? enable, bool stop, CancellationToken token) => WaveAccessAsync(async (client, ct) =>
    {
        var writes = new List<WriteRequest>();
        for (int i = 1; i <= 4; i++)
        {
            string prefix = $"{StationPrefix}.aAxis[{i}].";
            foreach (var field in new[] { "jogz", "jogf", "gohome", "reset" })
                writes.Add(new(client.DeviceId, prefix + field, DataType.Bool, false));
            if (enable.HasValue) writes.Add(new(client.DeviceId, prefix + "xStart", DataType.Bool, enable.Value));
            writes.Add(new(client.DeviceId, prefix + "xStop", DataType.Bool, stop));
        }
        await client.WriteManyAsync(writes, ct);
    }, token);

    private async Task PulseWaveAsync(string field, CancellationToken token)
    {
        await WaveWriteAsync("GVL_motion." + field, DataType.Bool, false, token);
        await Task.Delay(20, token);
        await WaveWriteAsync("GVL_motion." + field, DataType.Bool, true, token);
        try { await Task.Delay(50, token); }
        finally { await WaveWriteAsync("GVL_motion." + field, DataType.Bool, false, CancellationToken.None); }
    }

    private Task WaveWriteAsync(string symbol, DataType type, object value, CancellationToken token) =>
        WaveAccessAsync((client, ct) => client.WriteAsync(new WriteRequest(client.DeviceId, symbol, type, value), ct), token);

    private async Task WaveAccessAsync(Func<AdsClient, CancellationToken, Task> action, CancellationToken token)
    {
        await _operationGate.WaitAsync(token);
        try
        {
            token.ThrowIfCancellationRequested();
            RequireControlAuthority();
            await action(GetConnectedClient() ?? throw new InvalidOperationException("ADS 未连接。"), token);
        }
        finally { _operationGate.Release(); }
    }

    private async Task<PlcMachineMonitor> ReadWaveFrameAsync(CancellationToken token)
    {
        PlcMachineMonitor frame = default;
        await WaveAccessAsync(async (client, ct) =>
        {
            frame = await client.ReadAnyAsync<PlcMachineMonitor>(_options.SnapshotSymbol, cancellationToken: ct);
        }, token);
        frame.ValidateLayout();
        if (frame.ControlOwner != ControlStationCode) throw new InvalidOperationException("PLC 控制端已改变，禁止继续发送或启动。");
        var now = Environment.TickCount64;
        if (!_waveLastPlcHeartbeat.HasValue) _waveHeartbeatTick = now;
        else if (_waveLastPlcHeartbeat.Value != frame.Heartbeat)
        {
            _waveHeartbeatConfirmed = true;
            _waveHeartbeatTick = now;
        }
        _waveLastPlcHeartbeat = frame.Heartbeat;
        if (now - _waveHeartbeatTick > 1500) throw new TimeoutException("PLC 心跳停止，造波反馈不可用。");
        return frame;
    }

    private async Task<PlcMachineMonitor> WaitWaveFrameAsync(Func<PlcMachineMonitor, bool> condition,
        int milliseconds, string operation, CancellationToken token, bool allowError = false)
    {
        long until = Environment.TickCount64 + milliseconds;
        do
        {
            token.ThrowIfCancellationRequested();
            var frame = await ReadWaveFrameAsync(token);
            if (!allowError && frame.Error) throw new InvalidOperationException($"PLC 轨迹故障：{frame.TrajectoryErrorId}。");
            if (condition(frame)) return frame;
            await Task.Delay(100, token);
        } while (Environment.TickCount64 < until);
        throw new TimeoutException(operation + "超时，不能视为已完成。");
    }
}
