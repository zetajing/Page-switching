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
    private volatile string? _lastWaveFailure; // 最近一次原始失败；停车错误和后续成功不能抹掉它。
    public WaveOperationStatus WaveStatus => _waveStatus;
    public string? LastWaveFailure => _lastWaveFailure;
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
                Exception? failure = null;
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
                }
                catch (Exception ex)
                {
                    failure = ex;
                    _waveReadySequence = 0;
                    _waveStatus = new(ex is OperationCanceledException ? "正在停止" : "造波操作失败", 0, true, false, ex.Message);
                    if (ex is not OperationCanceledException)
                    {
                        _lastWaveFailure = ex.Message;
                        AdsDiagnostics.RecordException("造波操作失败", ex);
                    }
                    throw;
                }
                finally
                {
                    try
                    {
                        if (failure is not null && _waveNeedsStop) await StopWaveCoreAsync();
                        if (HasControlAuthority && !_waveFailSafe)
                            await WaveWriteAsync(StationPrefix + ".xWaveSession", DataType.Bool, false, CancellationToken.None);
                        // 停止确认会更新进度；结束后仍保留原始故障，方便页面和断点检查。
                        if (failure is not null && failure is not OperationCanceledException && !_waveFailSafe)
                            _waveStatus = new("造波操作失败", 0, false, false, failure.Message);
                        else if (failure is OperationCanceledException && !_waveNeedsStop && !_waveFailSafe)
                            _waveStatus = new("操作已取消，未下发点位", 0, false, false);
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
        (await ReadWaveFrameAsync(token)).ValidateWaveContract();
        await VerifyWaveAxisLimitsAsync(token); // 回原可能耗时，再核对一次，参数相同才发启动沿。
        _waveStatus = new("PLC 检查启动条件并定位到首点", 0, true, false);
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

    private async Task WaveWriteAsync(string symbol, DataType type, object value, CancellationToken token)
    {
        try
        {
            await WaveAccessAsync((client, ct) => client.WriteAsync(new WriteRequest(client.DeviceId, symbol, type, value), ct), token);
        }
        catch (Exception ex) when (ex is not OperationCanceledException)
        {
            // 保留 InnerException 和具体符号，不能把底层 ADS 错误换成通用“发送失败”。
            throw new InvalidOperationException($"写入 {symbol} 失败：{AdsDiagnostics.DescribeException(ex)}", ex);
        }
    }

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
            try
            {
                frame = await client.ReadAnyAsync<PlcMachineMonitor>(_options.SnapshotSymbol, cancellationToken: ct);
            }
            catch (Exception ex) when (ex is not OperationCanceledException)
            {
                throw new InvalidOperationException($"读取 {_options.SnapshotSymbol} 失败：{AdsDiagnostics.DescribeException(ex)}", ex);
            }
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
        PlcMachineMonitor frame;
        do
        {
            token.ThrowIfCancellationRequested();
            frame = await ReadWaveFrameAsync(token);
            try
            {
                if (!allowError && frame.Error)
                    throw new InvalidOperationException(PlcWaveDiagnostics.DescribeFault(frame.TrajectoryErrorId));
                if (condition(frame)) return frame;
            }
            catch (Exception ex) when (ex is not OperationCanceledException)
            {
                // 记录失败当时的反馈；后续安全停止可能改变 PLC 状态，不能事后再取值替代。
                throw new InvalidOperationException($"{operation}失败：{ex.Message}；{PlcWaveDiagnostics.DescribeFrame(frame)}", ex);
            }
            await Task.Delay(100, token);
        } while (Environment.TickCount64 < until);
        throw new TimeoutException($"{operation}超时（{milliseconds / 1000.0:g} 秒），不能视为已完成；{PlcWaveDiagnostics.DescribeFrame(frame)}");
    }
}
