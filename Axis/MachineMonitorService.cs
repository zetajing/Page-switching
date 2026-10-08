using InduLink.Abstractions;
using InduLink.Protocols.Ads;

namespace Page_switching;

// 可替换为离线模拟；接口只允许连接和读取，不向监控调用方暴露写入能力。
public interface IMachineMonitorTransport : IAsyncDisposable
{
    bool IsConnected { get; }
    Task ConnectAsync(CancellationToken cancellationToken);
    Task<BatchReadResult> ReadManyAsync(IReadOnlyCollection<ReadRequest> requests, CancellationToken cancellationToken);
}

public sealed class MachineMonitorService : IDisposable, IAsyncDisposable
{
    private readonly MachineMonitorOptions _options;
    private readonly Func<IMachineMonitorTransport> _transportFactory;
    private readonly TimeProvider _time;
    private readonly SemaphoreSlim _refreshGate = new(1, 1);
    private readonly CancellationTokenSource _lifetime = new();
    private readonly CancellationToken _lifetimeToken;
    private readonly object _stateSync = new();
    private readonly object _disposeSync = new();
    private MachineMonitorSnapshot _snapshot = new();
    private IMachineMonitorTransport? _transport;
    private long? _lastSuccessTick;
    private long? _heartbeatBaselineTick;
    private long? _heartbeatChangeTick;
    private uint? _lastHeartbeat;
    private long? _failureSinceTick;
    private bool _disposeRequested;
    private Task? _disposeTask;

    public MachineMonitorService(
        MachineMonitorOptions options,
        Func<IMachineMonitorTransport>? transportFactory = null,
        TimeProvider? timeProvider = null)
    {
        ArgumentNullException.ThrowIfNull(options);
        if (string.IsNullOrWhiteSpace(options.DeviceId)) throw new ArgumentException("监控 DeviceId 不能为空。", nameof(options));
        if (options.AxisSymbols.Count != 4) throw new ArgumentException("监控需要四轴反馈映射。", nameof(options));
        if (options.ConnectTimeoutMilliseconds <= 0 || options.OperationTimeoutMilliseconds <= 0 ||
            options.StaleAfterMilliseconds <= 0 || options.ReconnectIntervalMilliseconds <= 0)
            throw new ArgumentException("监控超时必须大于零。", nameof(options));

        _options = options;
        _time = timeProvider ?? TimeProvider.System;
        _transportFactory = transportFactory ?? (() => new AdsMonitorTransport(options));
        _lifetimeToken = _lifetime.Token;
    }

    // 按当前单调时钟派生过期状态；正在等待 ADS 时，界面仍可发现旧反馈已失效。
    public MachineMonitorSnapshot LatestSnapshot
    {
        get
        {
            lock (_stateSync)
            {
                var connected = !_disposeRequested && _transport?.IsConnected == true;
                var snapshot = _snapshot with { IsConnected = connected };
                if (!connected)
                    return snapshot.WithoutAvailability(MachineMonitorQuality.Disconnected,
                        _disposeRequested ? "监控已停止" : snapshot.ErrorMessage ?? "监控未连接");
                if (_lastSuccessTick.HasValue && HasElapsed(_lastSuccessTick.Value, _options.StaleAfterMilliseconds))
                    return snapshot.WithoutAvailability(MachineMonitorQuality.Stale, "超过 3 秒未收到有效反馈");
                if (!snapshot.Heartbeat.IsAvailable)
                    return snapshot.WithoutMachineAvailability(snapshot.OverallQuality,
                        "PLC 心跳不可用：" + (snapshot.Heartbeat.ErrorMessage ?? "未读取"));
                var heartbeatTick = _heartbeatChangeTick ?? _heartbeatBaselineTick;
                if (heartbeatTick.HasValue && HasElapsed(heartbeatTick.Value, _options.StaleAfterMilliseconds))
                    return snapshot.WithoutMachineAvailability(MachineMonitorQuality.HeartbeatStalled, "PLC 心跳超过 3 秒未变化");
                if (!_heartbeatChangeTick.HasValue)
                    return snapshot.WithoutMachineAvailability(MachineMonitorQuality.WaitingHeartbeat, "等待 PLC 心跳变化确认机器状态");
                return snapshot;
            }
        }
    }

    // 外部每 500 ms 调度；忙时立即返回当前快照，不排队，也不使用手动轴服务的锁。
    public async Task<MachineMonitorSnapshot> RefreshAsync(bool includeAxes, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        lock (_disposeSync)
        {
            if (_disposeRequested || !_refreshGate.Wait(0)) return LatestSnapshot;
        }

        using var linked = CancellationTokenSource.CreateLinkedTokenSource(_lifetimeToken, cancellationToken);
        try
        {
            linked.Token.ThrowIfCancellationRequested();
            bool reconnect;
            lock (_stateSync)
            {
                // 通信失败后留出重连间隔，不向可能仍持有内部锁的客户端追加读请求。
                if (_failureSinceTick.HasValue && !HasElapsed(_failureSinceTick.Value, _options.ReconnectIntervalMilliseconds))
                    return LatestSnapshot;
                reconnect = _failureSinceTick.HasValue || _transport is not null && !_transport.IsConnected;
            }
            if (reconnect) await RetireTransportAsync().ConfigureAwait(false);
            linked.Token.ThrowIfCancellationRequested();

            IMachineMonitorTransport transport;
            lock (_stateSync)
            {
                _transport ??= _transportFactory() ?? throw new InvalidOperationException("未创建监控连接。");
                transport = _transport;
            }
            if (!transport.IsConnected)
            {
                // 连接沿用原 ADS 连接预算；读取的 1000 ms 预算不套用于首次连接。
                using var connectCancellation = CancellationTokenSource.CreateLinkedTokenSource(linked.Token);
                connectCancellation.CancelAfter(_options.ConnectTimeoutMilliseconds);
                try
                {
                    await transport.ConnectAsync(connectCancellation.Token).ConfigureAwait(false);
                    connectCancellation.Token.ThrowIfCancellationRequested();
                }
                catch (OperationCanceledException) when (!linked.IsCancellationRequested)
                {
                    RecordCommunicationFailure("监控连接超时");
                    return LatestSnapshot;
                }
                linked.Token.ThrowIfCancellationRequested();
                if (!transport.IsConnected) throw new InvalidOperationException("监控连接未建立。");
            }

            var requests = BuildRequests(includeAxes);
            using var readCancellation = CancellationTokenSource.CreateLinkedTokenSource(linked.Token);
            readCancellation.CancelAfter(_options.OperationTimeoutMilliseconds);
            BatchReadResult result;
            try
            {
                result = await transport.ReadManyAsync(requests, readCancellation.Token).ConfigureAwait(false);
                readCancellation.Token.ThrowIfCancellationRequested();
            }
            catch (OperationCanceledException) when (!linked.IsCancellationRequested)
            {
                RecordCommunicationFailure("监控读取超时");
                return LatestSnapshot;
            }
            linked.Token.ThrowIfCancellationRequested();
            var frame = ReadFrame(result, includeAxes);
            SaveFrame(frame);
            if (IsCommunicationFailure(result, requests.Count))
                RecordCommunicationFailure(frame.ErrorMessage ?? "监控读取失败");
            return LatestSnapshot;
        }
        catch (OperationCanceledException) when (linked.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            // 监控卡片也显示底层 ADS 原因；重连时不反复追加完整调用栈。
            RecordCommunicationFailure(AdsDiagnostics.DescribeException(ex));
            return LatestSnapshot;
        }
        finally
        {
            _refreshGate.Release();
        }
    }

    private List<ReadRequest> BuildRequests(bool includeAxes)
    {
        var requests = new List<ReadRequest>(includeAxes ? 33 : 5);
        void Add(string symbol, DataType type)
        {
            if (!string.IsNullOrWhiteSpace(symbol)) requests.Add(new ReadRequest(_options.DeviceId, symbol.Trim(), type));
        }
        Add(_options.ControlOwnerSymbol, DataType.UInt16);
        Add(_options.ControlModeSymbol, DataType.UInt16);
        Add(_options.WaveStateSymbol, DataType.UInt16);
        Add(_options.WaveFaultCodeSymbol, DataType.UInt32);
        Add(_options.HeartbeatSymbol, DataType.UInt32);
        if (includeAxes)
        {
            foreach (var axis in _options.AxisSymbols)
            {
                Add(axis.ActualPosition, DataType.Double);
                Add(axis.Speed, DataType.Double);
                Add(axis.Homed, DataType.Bool);
                Add(axis.Alarm, DataType.Bool);
                Add(axis.PositiveLimit, DataType.Bool);
                Add(axis.NegativeLimit, DataType.Bool);
                Add(axis.OriginSignal, DataType.Bool);
            }
        }
        return requests;
    }

    private MachineMonitorSnapshot ReadFrame(BatchReadResult result, bool includeAxes)
    {
        ArgumentNullException.ThrowIfNull(result);
        // 以地址核对结果，丢项、重排或重复地址不能把另一字段的值错填到本字段。
        var values = result.Values.Where(value => value is not null && !string.IsNullOrWhiteSpace(value.Address))
            .GroupBy(value => value.Address.Trim(), StringComparer.Ordinal)
            .ToDictionary(group => group.Key, group => group.ToArray(), StringComparer.Ordinal);
        var errors = new List<string>();
        var goodCount = 0;
        FeedbackField<T> Read<T>(string symbol, DataType type) where T : struct
        {
            string? error = null;
            DataValue? value = null;
            if (string.IsNullOrWhiteSpace(symbol)) error = "未配置符号";
            else if (!values.TryGetValue(symbol.Trim(), out var matches)) error = "缺少读取结果";
            else if (matches.Length != 1) error = "读取结果地址重复";
            else value = matches[0];
            if (value is not null)
            {
                if (value.Quality != QualityStatus.Good) error = value.ErrorMessage ?? "数据质量无效";
                else if (value.DataType != type || value.Value is not T typed ||
                         typed is double number && !double.IsFinite(number)) error = "反馈类型或数值无效";
                else
                {
                    goodCount++;
                    return new FeedbackField<T>(typed, true);
                }
            }
            errors.Add($"{symbol}：{error}");
            return FeedbackField<T>.Unavailable(error ?? "读取失败");
        }

        var snapshot = new MachineMonitorSnapshot
        {
            IsConnected = true,
            ControlOwner = Read<ushort>(_options.ControlOwnerSymbol, DataType.UInt16),
            ControlMode = Read<ushort>(_options.ControlModeSymbol, DataType.UInt16),
            WaveState = Read<ushort>(_options.WaveStateSymbol, DataType.UInt16),
            WaveFaultCode = Read<uint>(_options.WaveFaultCodeSymbol, DataType.UInt32),
            Heartbeat = Read<uint>(_options.HeartbeatSymbol, DataType.UInt32)
        };
        var axes = new List<MachineAxisSnapshot>(4);
        if (includeAxes)
        {
            for (var index = 0; index < 4; index++)
            {
                var map = _options.AxisSymbols[index];
                axes.Add(new MachineAxisSnapshot
                {
                    AxisNumber = index + 1,
                    ActualPosition = Read<double>(map.ActualPosition, DataType.Double),
                    Speed = Read<double>(map.Speed, DataType.Double),
                    IsHomed = Read<bool>(map.Homed, DataType.Bool),
                    HasAlarm = Read<bool>(map.Alarm, DataType.Bool),
                    PositiveLimit = Read<bool>(map.PositiveLimit, DataType.Bool),
                    NegativeLimit = Read<bool>(map.NegativeLimit, DataType.Bool),
                    OriginSignal = Read<bool>(map.OriginSignal, DataType.Bool)
                });
            }
        }
        // 未附带轴读取时清空旧轴缓存，返回自动页前不能把历史值显示成实时反馈。
        return snapshot with
        {
            Axes = Array.AsReadOnly(axes.ToArray()),
            OverallQuality = goodCount == 0 ? MachineMonitorQuality.Unavailable
                : errors.Count == 0 ? MachineMonitorQuality.Good : MachineMonitorQuality.Partial,
            ErrorMessage = errors.Count == 0 ? null : string.Join("；", errors.Take(4))
        };
    }

    private void SaveFrame(MachineMonitorSnapshot frame)
    {
        lock (_stateSync)
        {
            if (_disposeRequested) return;
            var now = _time.GetUtcNow();
            if (frame.OverallQuality is MachineMonitorQuality.Good or MachineMonitorQuality.Partial)
            {
                _lastSuccessTick = _time.GetTimestamp();
                frame = frame with { LastSuccessTimestamp = now };
            }
            else frame = frame with { LastSuccessTimestamp = _snapshot.LastSuccessTimestamp };
            frame = frame with { HeartbeatLastChangedAt = _snapshot.HeartbeatLastChangedAt };
            if (frame.Heartbeat.IsAvailable && frame.Heartbeat.Value is uint heartbeat)
            {
                if (!_lastHeartbeat.HasValue)
                {
                    // 第一帧只建立基线，不能据此判定 PLC 任务正在运行。
                    _heartbeatBaselineTick = _time.GetTimestamp();
                }
                else if (heartbeat != _lastHeartbeat.Value)
                {
                    // 第二个不同值才确认运行；不等比较也支持 UDINT 回绕和 PLC 重启。
                    _heartbeatChangeTick = _time.GetTimestamp();
                    frame = frame with { HeartbeatLastChangedAt = now };
                }
                _lastHeartbeat = heartbeat;
            }
            _snapshot = frame;
            _failureSinceTick = null;
        }
    }

    private void RecordCommunicationFailure(string message)
    {
        lock (_stateSync)
        {
            if (_disposeRequested) return;
            _failureSinceTick ??= _time.GetTimestamp();
            _snapshot = _snapshot.WithoutAvailability(MachineMonitorQuality.Unavailable, message);
        }
    }

    private static bool IsCommunicationFailure(BatchReadResult result, int requestCount)
    {
        if (requestCount == 0) return false;
        if (result.Values.Count == 0) return true;
        if (result.Values.Any(value => value is not null && value.Quality == QualityStatus.Good)) return false;
        // PLC 契约尚未部署或类型不匹配时，重连不能修复变量定义，不反复断开连接。
        return result.Values.Any(value => value is null || !IsContractError(value.ErrorMessage));
    }

    private static bool IsContractError(string? message) => message is not null &&
        (message.Contains("DeviceSymbolNotFound", StringComparison.OrdinalIgnoreCase) ||
         message.Contains("DeviceInvalidSize", StringComparison.OrdinalIgnoreCase) ||
         message.Contains("DeviceInvalidData", StringComparison.OrdinalIgnoreCase));

    private bool HasElapsed(long timestamp, int milliseconds) =>
        _time.GetElapsedTime(timestamp) >= TimeSpan.FromMilliseconds(milliseconds);

    private async Task RetireTransportAsync()
    {
        IMachineMonitorTransport? transport;
        lock (_stateSync)
        {
            transport = _transport;
            _transport = null;
            _lastHeartbeat = null;
            _heartbeatBaselineTick = null;
            _heartbeatChangeTick = null;
            _snapshot = _snapshot with { HeartbeatLastChangedAt = null };
        }
        if (transport is not null) await transport.DisposeAsync().ConfigureAwait(false);
    }

    public ValueTask DisposeAsync() => new(BeginDispose());

    // Form.Dispose 的同步入口也先取消；实际连接清理在后台等待，不阻塞 UI。
    public void Dispose() => _ = BeginDispose();

    private Task BeginDispose()
    {
        TaskCompletionSource completion;
        lock (_disposeSync)
        {
            if (_disposeTask is not null) return _disposeTask;
            // 先登记同一清理任务，再取消，避免取消回调再次 Dispose 时启动重复清理。
            completion = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
            _disposeTask = completion.Task;
            lock (_stateSync) _disposeRequested = true;
        }
        try
        {
            _lifetime.Cancel();
        }
        catch (Exception ex)
        {
            lock (_stateSync)
                _snapshot = _snapshot.WithoutAvailability(MachineMonitorQuality.Disconnected, "监控取消失败：" + ex.Message);
        }
        _ = Task.Run(async () =>
        {
            try
            {
                await DisposeCoreAsync().ConfigureAwait(false);
                completion.TrySetResult();
            }
            catch (Exception ex)
            {
                completion.TrySetException(ex);
            }
        });
        return completion.Task;
    }

    private async Task DisposeCoreAsync()
    {
        await _refreshGate.WaitAsync().ConfigureAwait(false);
        try
        {
            await RetireTransportAsync().ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            lock (_stateSync)
                _snapshot = _snapshot.WithoutAvailability(MachineMonitorQuality.Disconnected, "监控连接清理失败：" + ex.Message);
        }
        finally
        {
            _refreshGate.Release();
            _refreshGate.Dispose();
            _lifetime.Dispose();
        }
    }

    private sealed class AdsMonitorTransport : IMachineMonitorTransport
    {
        private readonly AdsClient _client;

        public AdsMonitorTransport(MachineMonitorOptions options)
        {
            // 每个监控实例独占客户端及其内部锁，只复用同一个 Router 和 PLC 目标。
            _client = new AdsClient(new AdsClientOptions
            {
                DeviceId = options.DeviceId,
                AmsNetId = options.AmsNetId,
                Port = options.AdsPort,
                ConnectTimeoutMilliseconds = options.ConnectTimeoutMilliseconds,
                OperationTimeoutMilliseconds = options.OperationTimeoutMilliseconds,
                EnableSumCommands = true,
                ValidateTargetStateOnConnect = true
            });
        }

        public bool IsConnected => _client.IsConnected;
        public Task ConnectAsync(CancellationToken cancellationToken) => _client.ConnectAsync(cancellationToken);
        public Task<BatchReadResult> ReadManyAsync(IReadOnlyCollection<ReadRequest> requests, CancellationToken cancellationToken) =>
            _client.ReadManyAsync(requests, cancellationToken);
        public ValueTask DisposeAsync() => _client.DisposeAsync();
    }
}
