using InduLink.Abstractions;

namespace Page_switching;

// 会话生命周期：登记会话、执行本次操作、失败清理；跨页持续运行。
public sealed partial class AxisService
{
    private readonly object _waveSync = new(); // 登记操作与停止请求，防止同时启动两个会话。
    private readonly SemaphoreSlim _waveStopGate = new(1, 1); // 用户停止与失败清理共用，防止同时释放请求。
    private CancellationTokenSource? _waveCancellation; // 停止先取消旧任务，再等待它退出。
    private Task _waveTask = Task.CompletedTask; // 保存当前任务，禁止停车后还有迟到的启动写入。
    private volatile bool _waveActive; // 上传、准备、执行和失败清理期间均占用手动入口。
    private volatile bool _waveStopRequested; // 停止等待期间仍拦截新的普通操作。
    private volatile bool _waveFailSafe; // 停车未确认后禁止继续命令并停止本站心跳，不自动恢复旧动作。
    private volatile WaveOperationStatus _waveStatus = new("未导入", 0, false, false);
    private uint _waveHeartbeat; // 控制权维护任务周期写给 PLC 的轨迹心跳序号。
    private bool _waveNeedsStop; // 已进入写表或运动阶段；失败不能仅释放会话，必须先停车确认。
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
            _waveTask = Task.Run(() => ExecuteWaveOperationAsync(operation, token), CancellationToken.None);
            return _waveTask;
        }
    }

    // 原任务的异常与清理顺序保持不变；此入口方便定位原始失败。
    private async Task ExecuteWaveOperationAsync(Func<CancellationToken, Task> operation, CancellationToken token)
    {
        Exception? failure = null;
        try
        {
            await BeginWaveSessionAsync(token);
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
            await FinishWaveSessionAsync(failure);
        }
    }

    // 先等 PLC 会话锁和新心跳，再允许读参、下发或启动。
    private async Task BeginWaveSessionAsync(CancellationToken token)
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
    }

    // 必要时停车，再释放会话；停车错误不能清掉原始失败，最后才结束忙状态。
    private async Task FinishWaveSessionAsync(Exception? failure)
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
}
