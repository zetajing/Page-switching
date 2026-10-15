using TwinCAT.Ads;

namespace Page_switching;

// 直接读取 PLC 反馈，本服务不提供写入或控制方法。
public sealed class MachineMonitorService : IDisposable, IAsyncDisposable
{
    private readonly MachineMonitorOptions _options;
    private readonly SemaphoreSlim _refreshGate = new(1, 1);
    private readonly CancellationTokenSource _lifetime = new();
    private readonly object _stateSync = new();
    // 句柄只属于当前客户端；刷新与清理都受 _refreshGate 保护，不并发访问。
    private uint? _monitorHandle;
    private AdsClient? _client;
    private MachineMonitorSnapshot _snapshot = new();
    private long? _lastSuccessTick;
    private long? _lastHeartbeatTick;
    private long? _failureSinceTick;
    private uint? _lastHeartbeat;
    private bool _heartbeatConfirmed;
    private bool _disposed;
    private Task? _disposeTask;
    // RefreshAsync 不重入，保留当前读取步骤，超时时能直接定位变量。
    private string _readOperation = "连接";

    public MachineMonitorService(MachineMonitorOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);
        if (string.IsNullOrWhiteSpace(options.SnapshotSymbol)) throw new ArgumentException("监控结构体地址不能为空。", nameof(options));
        if (options.ConnectTimeoutMilliseconds <= 0 || options.OperationTimeoutMilliseconds <= 0 ||
            options.RefreshTimeoutMilliseconds <= 0 || options.StaleAfterMilliseconds <= 0 || options.ReconnectIntervalMilliseconds <= 0)
            throw new ArgumentException("监控超时必须大于零。", nameof(options));
        _options = options;
    }

    // 显示前检查反馈时间与心跳，不能把历史数据当成实时状态。
    public MachineMonitorSnapshot LatestSnapshot
    {
        get
        {
            lock (_stateSync)
            {
                var snapshot = _snapshot with { IsConnected = !_disposed && _client?.IsConnected == true };
                if (!snapshot.IsConnected)
                    return snapshot.WithoutAvailability(MachineMonitorQuality.Disconnected,
                        _disposed ? "监控已停止" : snapshot.ErrorMessage ?? "监控未连接");
                if (_lastSuccessTick.HasValue && HasElapsed(_lastSuccessTick.Value, _options.StaleAfterMilliseconds))
                    return snapshot.WithoutAvailability(MachineMonitorQuality.Stale, "超过 3 秒未收到有效反馈");
                if (!snapshot.Heartbeat.IsAvailable)
                    return snapshot.WithoutMachineAvailability(snapshot.OverallQuality,
                        "PLC 心跳不可用：" + (snapshot.Heartbeat.ErrorMessage ?? "未读取"));
                if (_lastHeartbeatTick.HasValue && HasElapsed(_lastHeartbeatTick.Value, _options.StaleAfterMilliseconds))
                    return snapshot.WithoutMachineAvailability(MachineMonitorQuality.HeartbeatStalled, "PLC 心跳超过 3 秒未变化");
                if (!_heartbeatConfirmed)
                    return snapshot.WithoutMachineAvailability(MachineMonitorQuality.WaitingHeartbeat, "等待 PLC 心跳变化确认机器状态");
                return snapshot;
            }
        }
    }

    // 调试入口：连接 → 读取监控结构体 → 保存结果。忙时跳过本轮，不排队。
    public async Task<MachineMonitorSnapshot> RefreshAsync(bool includeAxes, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        lock (_stateSync)
        {
            if (_disposed || !_refreshGate.Wait(0)) return LatestSnapshot;
        }

        using var timeout = CancellationTokenSource.CreateLinkedTokenSource(_lifetime.Token, cancellationToken);
        var startedAt = Environment.TickCount64;
        var timeoutMilliseconds = _options.ConnectTimeoutMilliseconds;
        _readOperation = "连接";
        try
        {
            bool closeClient;
            lock (_stateSync)
            {
                if (_failureSinceTick.HasValue && !HasElapsed(_failureSinceTick.Value, _options.ReconnectIntervalMilliseconds))
                    return LatestSnapshot;
                closeClient = _failureSinceTick.HasValue || _client is not null && !_client.IsConnected;
            }
            if (closeClient) await CloseClientAsync().ConfigureAwait(false);
            lock (_stateSync)
            {
                _client ??= new AdsClient { Timeout = _options.ConnectTimeoutMilliseconds };
            }
            timeout.CancelAfter(_options.ConnectTimeoutMilliseconds);
            timeout.Token.ThrowIfCancellationRequested();
            if (!_client.IsConnected)
            {
                if (string.IsNullOrWhiteSpace(_options.AmsNetId))
                    await _client.ConnectAsync(_options.AdsPort, timeout.Token).ConfigureAwait(false);
                else
                    await _client.ConnectAsync(new AmsNetId(_options.AmsNetId), _options.AdsPort, timeout.Token).ConfigureAwait(false);
                // 本机地址来自实际 Router；两个客户端的 ADS 端口由 Router 分配。
                OperationJournal.Record("ADS 诊断", $"监控 ADS 地址：本机={_client.SourceAddress}，目标={_client.Address}");
                _readOperation = "读取 PLC 状态";
                var state = await _client.ReadStateAsync(timeout.Token).ConfigureAwait(false);
                if (!state.Succeeded) throw new InvalidOperationException("PLC 状态读取失败：" + state.ErrorCode);
            }
            timeout.Token.ThrowIfCancellationRequested();

            _client.Timeout = _options.OperationTimeoutMilliseconds;
            // 整块读取复用句柄；单次请求超时与整轮读取超时仍分开计算。
            timeoutMilliseconds = _options.RefreshTimeoutMilliseconds;
            timeout.CancelAfter(timeoutMilliseconds);
            var errors = new List<string>();
            var snapshot = await ReadSnapshotAsync(includeAxes, errors, timeout.Token).ConfigureAwait(false);
            timeout.Token.ThrowIfCancellationRequested();
            SaveFrame(snapshot);
            // 所有字段均因通信失败而无效时才重连；符号或类型错误不靠重连解决。
            if (snapshot.OverallQuality == MachineMonitorQuality.Unavailable && errors.Any(error => !IsContractError(error)))
                RecordCommunicationFailure(snapshot.ErrorMessage ?? "监控读取失败", startedAt);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested || _lifetime.IsCancellationRequested)
        {
            throw;
        }
        catch (OperationCanceledException)
        {
            RecordCommunicationFailure($"监控超时（上限 {timeoutMilliseconds} ms）：{_readOperation}", startedAt);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("监控结构体布局不匹配", StringComparison.Ordinal))
        {
            SaveFrame(new MachineMonitorSnapshot { IsConnected = true }
                .WithoutAvailability(MachineMonitorQuality.Unavailable, ex.Message));
        }
        catch (Exception ex)
        {
            RecordCommunicationFailure(AdsDiagnostics.DescribeException(ex), startedAt);
        }
        finally
        {
            _refreshGate.Release();
        }
        return LatestSnapshot;
    }

    // 句柄缓存后，每轮只有一次数据读取；失败使整帧无效，不显示默认零值。
    private async Task<MachineMonitorSnapshot> ReadSnapshotAsync(bool includeAxes, List<string> errors, CancellationToken token)
    {
        var frame = await ReadFrameAsync(errors, token).ConfigureAwait(false);
        if (!frame.IsAvailable || !frame.Value.HasValue)
        {
            var message = $"读取 {_options.SnapshotSymbol} 失败：{frame.ErrorMessage}；请核对配套 PLC 结构体是否已部署。";
            return new MachineMonitorSnapshot { IsConnected = true }
                .WithoutAvailability(MachineMonitorQuality.Unavailable, message);
        }
        return frame.Value.Value.ToSnapshot(includeAxes);
    }

    // PLC 实际读取就在这里：断点查看 symbol、result.Value 和 result.ErrorCode。
    private async Task<FeedbackField<PlcMachineMonitor>> ReadFrameAsync(List<string> errors, CancellationToken token)
    {
        var symbol = _options.SnapshotSymbol;
        var error = "未配置符号";
        if (!string.IsNullOrWhiteSpace(symbol))
        {
            symbol = symbol.Trim();
            try
            {
                // 首次使用才获取句柄，后续刷新只发送读取请求。
                if (!_monitorHandle.HasValue)
                {
                    _readOperation = $"获取句柄 {symbol}";
                    var created = await _client!.CreateVariableHandleAsync(symbol, token).ConfigureAwait(false);
                    if (!created.Succeeded)
                    {
                        error = $"ADS error {created.ErrorCode} (0x{(int)created.ErrorCode:X8})";
                        errors.Add($"{symbol}：{error}");
                        return FeedbackField<PlcMachineMonitor>.Unavailable(error);
                    }
                    _monitorHandle = created.Handle;
                }
                _readOperation = $"读取变量 {symbol}";
                var result = await _client!.ReadAnyAsync<PlcMachineMonitor>(_monitorHandle.Value, token).ConfigureAwait(false);
                if (result.Succeeded)
                {
                    return new FeedbackField<PlcMachineMonitor>(result.Value, true);
                }
                else
                {
                    error = $"ADS error {result.ErrorCode} (0x{(int)result.ErrorCode:X8})";
                    // PLC 下载或在线修改可能使句柄失效；当前字段报错，下轮重新获取。
                    if (IsInvalidHandle(result.ErrorCode))
                    {
                        var invalidHandle = _monitorHandle.Value;
                        _monitorHandle = null;
                        _readOperation = $"释放失效句柄 {symbol}";
                        await _client.DeleteVariableHandleAsync(invalidHandle, token).ConfigureAwait(false);
                    }
                }
            }
            catch (OperationCanceledException) when (token.IsCancellationRequested) { throw; }
            catch (Exception ex) { error = AdsDiagnostics.DescribeException(ex); }
        }
        // 整帧读取失败时返回无效，不能把失败值显示成零或 false。
        errors.Add($"{symbol}：{error}");
        return FeedbackField<PlcMachineMonitor>.Unavailable(error);
    }

    private void SaveFrame(MachineMonitorSnapshot snapshot)
    {
        lock (_stateSync)
        {
            if (_disposed) return;
            snapshot = snapshot with
            {
                LastSuccessTimestamp = _snapshot.LastSuccessTimestamp,
                HeartbeatLastChangedAt = _snapshot.HeartbeatLastChangedAt
            };
            if (snapshot.OverallQuality is MachineMonitorQuality.Good or MachineMonitorQuality.Partial)
            {
                _lastSuccessTick = Environment.TickCount64;
                snapshot = snapshot with { LastSuccessTimestamp = DateTimeOffset.UtcNow };
            }
            if (snapshot.Heartbeat.IsAvailable && snapshot.Heartbeat.Value is uint heartbeat)
            {
                // 首帧建立基线，之后变化才确认运行，同时支持计数回绕与 PLC 重启。
                if (!_lastHeartbeat.HasValue) _lastHeartbeatTick = Environment.TickCount64;
                else if (heartbeat != _lastHeartbeat.Value)
                {
                    _heartbeatConfirmed = true;
                    _lastHeartbeatTick = Environment.TickCount64;
                    snapshot = snapshot with { HeartbeatLastChangedAt = DateTimeOffset.UtcNow };
                }
                _lastHeartbeat = heartbeat;
            }
            _snapshot = snapshot;
            _failureSinceTick = null;
        }
    }

    private void RecordCommunicationFailure(string message, long startedAt)
    {
        lock (_stateSync)
        {
            if (_disposed) return;
            _failureSinceTick ??= Environment.TickCount64;
            _snapshot = _snapshot.WithoutAvailability(MachineMonitorQuality.Unavailable, message);
        }
        // 保留实际错误和变量步骤，偶发故障也能从主窗体操作记录中定位。
        OperationJournal.Record("ADS 诊断", $"监控读取失败：{message}；当前步骤={_readOperation}；本轮耗时={Environment.TickCount64 - startedAt} ms");
    }

    private static bool IsContractError(string message) =>
        message.Contains("未配置符号", StringComparison.Ordinal) ||
        message.Contains("DeviceSymbolNotFound", StringComparison.OrdinalIgnoreCase) ||
        message.Contains("DeviceInvalidSize", StringComparison.OrdinalIgnoreCase) ||
        message.Contains("DeviceInvalidData", StringComparison.OrdinalIgnoreCase) ||
        message.Contains("Marshalling", StringComparison.OrdinalIgnoreCase);

    private static bool HasElapsed(long timestamp, int milliseconds) =>
        Environment.TickCount64 - timestamp >= milliseconds;

    private static bool IsInvalidHandle(AdsErrorCode code) =>
        code is AdsErrorCode.DeviceSymbolVersionInvalid or AdsErrorCode.DeviceSymbolNotFound;

    private async Task CloseClientAsync()
    {
        AdsClient? client;
        lock (_stateSync)
        {
            client = _client;
            _client = null;
            _lastHeartbeat = null;
            _lastHeartbeatTick = null;
            _heartbeatConfirmed = false;
            _snapshot = _snapshot with { HeartbeatLastChangedAt = null };
        }
        try
        {
            if (client?.IsConnected == true && _monitorHandle.HasValue)
            {
                // 重连或退出时集中释放；整批清理最多等待一次请求超时，断网时不逐项等待。
                using var cleanup = new CancellationTokenSource(_options.OperationTimeoutMilliseconds);
                var result = await client.DeleteVariableHandleAsync(_monitorHandle.Value, cleanup.Token).ConfigureAwait(false);
                cleanup.Token.ThrowIfCancellationRequested();
                if (!result.Succeeded && !IsInvalidHandle(result.ErrorCode))
                    throw new InvalidOperationException("释放监控句柄失败：" + result.ErrorCode);
            }
        }
        catch (Exception ex)
        {
            OperationJournal.Record("ADS 诊断", "清理监控句柄：" + AdsDiagnostics.DescribeException(ex));
        }
        finally
        {
            // 句柄不能跨客户端复用，清理失败时也必须丢弃本地缓存。
            _monitorHandle = null;
            client?.Dispose();
        }
    }

    // 同步 Dispose 不阻塞界面，异步关闭等待同一个清理任务完成。
    public void Dispose() => _ = DisposeAsync();

    public ValueTask DisposeAsync()
    {
        lock (_stateSync)
        {
            if (_disposeTask is not null) return new ValueTask(_disposeTask);
            _disposed = true;
            // 先登记任务，取消回调再次 Dispose 时不会重复清理。
            _disposeTask = Task.Run(DisposeCoreAsync);
            return new ValueTask(_disposeTask);
        }
    }

    private async Task DisposeCoreAsync()
    {
        try { _lifetime.Cancel(); }
        catch (Exception ex) { System.Diagnostics.Debug.WriteLine("监控取消失败：" + ex); }
        await _refreshGate.WaitAsync().ConfigureAwait(false);
        try
        {
            await CloseClientAsync().ConfigureAwait(false);
        }
        catch (Exception ex) { System.Diagnostics.Debug.WriteLine("监控连接清理失败：" + ex); }
        finally
        {
            _refreshGate.Release();
            _refreshGate.Dispose();
            _lifetime.Dispose();
        }
    }
}
