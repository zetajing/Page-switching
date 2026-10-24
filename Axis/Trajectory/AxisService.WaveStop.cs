using InduLink.Abstractions;

namespace Page_switching;

// 停止与退出：先取消旧任务，确认实际停稳，再释放停止位和会话。
public sealed partial class AxisService
{
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

}
