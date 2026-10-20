using InduLink.Abstractions;
using InduLink.Protocols.Ads;

namespace Page_switching;

public sealed partial class AxisService : IDisposable
{
    private readonly AxisServiceOptions _options;
    // 手动读写和控制权维护共用此门闩；不要为了简化调用而改成并发写命令。
    private readonly SemaphoreSlim _operationGate = new(1, 1);
    private readonly object _stateSync = new();
    private AdsClient? _adsClient;
    private bool _connected;
    private string? _lastError;
    private bool _disposed;

    // 保存配置，并确认已经提供四根轴的变量映射。
    public AxisService(AxisServiceOptions options)
    {
        _options = options ?? throw new ArgumentNullException(nameof(options));
        if (string.IsNullOrWhiteSpace(options.SnapshotSymbol))
            throw new ArgumentException("监控结构体地址不能为空。", nameof(options));
        if (_options.AxisSymbols.Count != 4)
        {
            throw new ArgumentException("必须提供四个造波板轴的 ADS 符号映射。", nameof(options));
        }
    }

    public int RefreshIntervalMilliseconds => _options.RefreshIntervalMilliseconds;
    public string Unit => _options.Unit;
    public double MinimumPosition => _options.MinimumPosition;
    public double MaximumPosition => _options.MaximumPosition;
    public string? LastError => _lastError;

    public bool CanControlAll => !WaveOperationActive && IsConnected && HasControlAuthority && _options.AxisSymbols.All(map =>
            !string.IsNullOrWhiteSpace(map.EnableCommand) &&
            !string.IsNullOrWhiteSpace(map.ResetAlarmCommand) &&
            !string.IsNullOrWhiteSpace(map.HomeCommand) &&
            !string.IsNullOrWhiteSpace(map.StopCommand));

    // 判断指定轴是否已连接，并且已经配置回零、停止和点动变量。
    public bool CanControlAxis(int axisNumber)
    {
        ValidateAxisNumber(axisNumber);
        var map = _options.AxisSymbols[axisNumber - 1];
        return !WaveOperationActive && IsConnected && HasControlAuthority &&
            !string.IsNullOrWhiteSpace(map.HomeCommand) &&
            !string.IsNullOrWhiteSpace(map.StopCommand) &&
            !string.IsNullOrWhiteSpace(map.JogSpeedCommand) &&
            !string.IsNullOrWhiteSpace(map.JogPositiveCommand) &&
            !string.IsNullOrWhiteSpace(map.JogNegativeCommand);
    }

    public bool IsConnected
    {
        get
        {
            lock (_stateSync)
            {
                return _connected && _adsClient?.IsConnected == true;
            }
        }
    }

    public string ConnectionStateText
    {
        get
        {
            if (IsConnected)
            {
                if (ControlStationCode == 0) return "ADS 已连接，未配置本站身份";
                return HasControlAuthority ? "ADS 已连接，本站已授权" : "ADS 已连接，本站未授权";
            }

            return string.IsNullOrWhiteSpace(_lastError) ? "ADS 未连接" : "ADS 连接失败";
        }
    }

    // 根据配置的 AMS Net ID 和 ADS 端口连接 Beckhoff PLC。
    public async Task ConnectAsync(CancellationToken cancellationToken)
    {
        ThrowIfDisposed();

        await _operationGate.WaitAsync(cancellationToken).ConfigureAwait(false);
        try
        {
            if (IsConnected)
            {
                return;
            }

            var client = new AdsClient(new AdsClientOptions
            {
                DeviceId = "beckhoff-plc",
                AmsNetId = _options.AmsNetId,
                Port = _options.AdsPort,
                ConnectTimeoutMilliseconds = _options.ConnectTimeoutMilliseconds,
                OperationTimeoutMilliseconds = _options.OperationTimeoutMilliseconds,
                ValidateTargetStateOnConnect = true,
                EnableSumCommands = true
            });

            try
            {
                await client.ConnectAsync(cancellationToken).ConfigureAwait(false);
                lock (_stateSync)
                {
                    _adsClient = client;
                    _connected = true;
                    _lastError = null;
                }
                StartAuthorityLoop();
            }
            catch (Exception ex)
            {
                client.Dispose();
                lock (_stateSync)
                {
                    _connected = false;
                    _lastError = ex.Message;
                }

                throw;
            }
        }
        finally
        {
            _operationGate.Release();
        }
    }

    // 断开 ADS 连接并释放客户端。
    public async Task DisconnectAsync(CancellationToken cancellationToken)
    {
        if (_disposed)
        {
            return;
        }

        await ShutdownWaveAsync().ConfigureAwait(false);
        await StopAuthorityLoopAsync().ConfigureAwait(false);
        await _operationGate.WaitAsync(cancellationToken).ConfigureAwait(false);
        try
        {
            AdsClient? client;
            lock (_stateSync)
            {
                client = _adsClient;
                _adsClient = null;
                _connected = false;
            }

            if (client is not null)
            {
                try
                {
                    if (client.IsConnected)
                    {
                        await client.DisconnectAsync(cancellationToken).ConfigureAwait(false);
                    }
                }
                finally
                {
                    client.Dispose();
                }
            }
        }
        finally
        {
            _operationGate.Release();
        }
    }

    // 读取四根轴的当前位置、速度和状态，供手动页面刷新显示。
    public async Task<IReadOnlyList<AxisSnapshot>> ReadSnapshotAsync(CancellationToken cancellationToken)
    {
        ThrowIfDisposed();

        await _operationGate.WaitAsync(cancellationToken).ConfigureAwait(false);
        try
        {
            var client = GetConnectedClient();
            if (client is null)
            {
                return CreateUnavailableSnapshots("ADS 未连接");
            }

            return await ReadAdsSnapshotsAsync(client, cancellationToken).ConfigureAwait(false);
        }
        catch (Exception ex) when (ex is not OperationCanceledException)
        {
            lock (_stateSync)
            {
                _lastError = ex.Message;
            }

            return CreateUnavailableSnapshots("读取失败");
        }
        finally
        {
            _operationGate.Release();
        }
    }

    // 将四根轴的 EnableCommand 全部写为 true。
    public Task EnableAllAsync(CancellationToken cancellationToken) =>
        WriteAllAsync(
            _options.AxisSymbols.Select(axis => axis.EnableCommand).ToArray(), true, cancellationToken);

    // 将四根轴的 EnableCommand 全部写为 false。
    public Task DisableAllAsync(CancellationToken cancellationToken) =>
        WriteAllAsync(
            _options.AxisSymbols.Select(axis => axis.EnableCommand).ToArray(), false, cancellationToken);

    // 将一组布尔变量批量写成指定值。
    private async Task WriteAllAsync(
        IReadOnlyList<string> symbols,
        bool value,
        CancellationToken cancellationToken)
    {
        ThrowIfDisposed();
        await _operationGate.WaitAsync(cancellationToken).ConfigureAwait(false);
        try
        {
            var client = GetConnectedClient() ?? throw new InvalidOperationException("ADS 尚未连接。");
            RequireControlAuthority();
            if (WaveOperationActive) throw new InvalidOperationException("造波会话进行中，请先停止造波再手动操作。");
            await client.WriteManyAsync(CreateWriteRequests(client, symbols, value), cancellationToken)
                .ConfigureAwait(false);
        }
        finally
        {
            _operationGate.Release();
        }
    }

    // 将四根轴的 ResetAlarmCommand 置位 50 毫秒后复位。
    public Task ResetAlarmsAsync(CancellationToken cancellationToken) =>
        PulseAllAsync(
            _options.AxisSymbols.Select(axis => axis.ResetAlarmCommand).ToArray(), cancellationToken);

    // 将四根轴的 HomeCommand 置位 50 毫秒后复位。
    public Task HomeAllAsync(CancellationToken cancellationToken) =>
        PulseAllAsync(
            _options.AxisSymbols.Select(axis => axis.HomeCommand).ToArray(), cancellationToken);

    // 将四根轴的 StopCommand 置位 50 毫秒后复位。
    public Task StopAllAsync(CancellationToken cancellationToken) =>
        PulseAllAsync(
            _options.AxisSymbols.Select(axis => axis.StopCommand).ToArray(), cancellationToken);

    // 触发指定轴的 HomeCommand，使该轴开始回零。
    public Task HomeAxisAsync(int axisNumber, CancellationToken cancellationToken)
    {
        ValidateAxisNumber(axisNumber);
        return PulseAxisAsync(axisNumber, _options.AxisSymbols[axisNumber - 1].HomeCommand, cancellationToken);
    }

    // 触发指定轴的 StopCommand，使该轴停止运动。
    public Task StopAxisAsync(int axisNumber, CancellationToken cancellationToken)
    {
        ValidateAxisNumber(axisNumber);
        return PulseAxisAsync(axisNumber, _options.AxisSymbols[axisNumber - 1].StopCommand, cancellationToken);
    }

    // 写入点动速度和方向变量；按下时写 true，松开时写 false。
    public async Task JogAsync(
        int axisNumber,
        bool positive,
        bool start,
        double speed,
        CancellationToken cancellationToken)
    {
        ThrowIfDisposed();
        ValidateAxisNumber(axisNumber);

        var map = _options.AxisSymbols[axisNumber - 1];
        var commandSymbol = positive ? map.JogPositiveCommand : map.JogNegativeCommand;
        if (string.IsNullOrWhiteSpace(commandSymbol) || string.IsNullOrWhiteSpace(map.JogSpeedCommand))
        {
            throw new InvalidOperationException($"轴 {axisNumber} 尚未完整配置 ADS 点动速度和方向符号。");
        }

        await _operationGate.WaitAsync(cancellationToken).ConfigureAwait(false);
        try
        {
            var client = GetConnectedClient() ?? throw new InvalidOperationException("ADS 尚未连接。");
            if (start)
            {
                RequireControlAuthority();
                if (WaveOperationActive) throw new InvalidOperationException("造波会话进行中，禁止点动。");
                await client.WriteAsync(
                    new WriteRequest(client.DeviceId, map.JogSpeedCommand, DataType.Double, Math.Abs(speed)),
                    cancellationToken).ConfigureAwait(false);
            }

            await client.WriteAsync(
                new WriteRequest(client.DeviceId, commandSymbol, DataType.Bool, start),
                cancellationToken).ConfigureAwait(false);
        }
        finally
        {
            _operationGate.Release();
        }
    }

    // 将四根轴的命令变量置位 50 毫秒，再全部复位。
    private async Task PulseAllAsync(
        IReadOnlyList<string> symbols,
        CancellationToken cancellationToken)
    {
        ThrowIfDisposed();
        await _operationGate.WaitAsync(cancellationToken).ConfigureAwait(false);
        try
        {
            var client = GetConnectedClient() ?? throw new InvalidOperationException("ADS 尚未连接。");
            RequireControlAuthority();
            if (WaveOperationActive) throw new InvalidOperationException("造波会话进行中，请先停止造波再手动操作。");
            var setRequests = CreateWriteRequests(client, symbols, true);
            var resetRequests = CreateWriteRequests(client, symbols, false);
            try
            {
                await client.WriteManyAsync(setRequests, cancellationToken).ConfigureAwait(false);
                await Task.Delay(50, cancellationToken).ConfigureAwait(false);
            }
            finally
            {
                // 置位可能部分成功；即使取消，也按原轴顺序尝试复位，超时由 ADS 客户端控制。
                await client.WriteManyAsync(resetRequests, CancellationToken.None).ConfigureAwait(false);
            }
        }
        finally
        {
            _operationGate.Release();
        }
    }

    // 将指定轴的命令变量置位 50 毫秒，再复位。
    private async Task PulseAxisAsync(
        int axisNumber,
        string symbol,
        CancellationToken cancellationToken)
    {
        ThrowIfDisposed();
        ValidateAxisNumber(axisNumber);
        await _operationGate.WaitAsync(cancellationToken).ConfigureAwait(false);
        try
        {
            var client = GetConnectedClient() ?? throw new InvalidOperationException("ADS 尚未连接。");
            if (string.IsNullOrWhiteSpace(symbol))
            {
                throw new InvalidOperationException($"轴 {axisNumber} 尚未配置 ADS 控制符号。");
            }

            RequireControlAuthority();
            if (WaveOperationActive) throw new InvalidOperationException("造波会话进行中，请先停止造波再手动操作。");
            try
            {
                await client.WriteAsync(
                    new WriteRequest(client.DeviceId, symbol, DataType.Bool, true), cancellationToken)
                    .ConfigureAwait(false);
                await Task.Delay(50, cancellationToken).ConfigureAwait(false);
            }
            finally
            {
                // 页面取消不应跳过复位；断线时仍会向调用方报告写入失败。
                await client.WriteAsync(
                    new WriteRequest(client.DeviceId, symbol, DataType.Bool, false), CancellationToken.None)
                    .ConfigureAwait(false);
            }
        }
        finally
        {
            _operationGate.Release();
        }
    }

    // 把变量地址和值转换成 ADS 批量写请求。
    private static IReadOnlyCollection<WriteRequest> CreateWriteRequests(
        AdsClient client,
        IReadOnlyList<string> symbols,
        bool value)
    {
        var requests = new List<WriteRequest>(symbols.Count);
        for (var index = 0; index < symbols.Count; index++)
        {
            var symbol = symbols[index];
            if (string.IsNullOrWhiteSpace(symbol))
            {
                throw new InvalidOperationException($"轴 {index + 1} 尚未配置 ADS 控制符号。");
            }

            requests.Add(new WriteRequest(client.DeviceId, symbol, DataType.Bool, value));
        }

        return requests;
    }

    // 使用现有 DLL 一次读取四轴反馈，布局校验通过后才更新手动页。
    // 控制命令仍逐项写本站邮箱，不整体回写反馈结构体。
    private async Task<IReadOnlyList<AxisSnapshot>> ReadAdsSnapshotsAsync(
        AdsClient client,
        CancellationToken cancellationToken)
    {
        var frame = await client.ReadAnyAsync<PlcMachineMonitor>(
            _options.SnapshotSymbol, cancellationToken: cancellationToken).ConfigureAwait(false);
        return frame.ToAxisSnapshots();
    }

    // 在未连接或读取失败时，生成四根轴的占位状态。
    private IReadOnlyList<AxisSnapshot> CreateUnavailableSnapshots(string status)
    {
        return Enumerable.Range(1, 4)
            .Select(index =>
            {
                var snapshot = new AxisSnapshot(index) { StatusText = status };
                return snapshot;
            })
            .ToArray();
    }

    // 仅在服务和 ADS 客户端都处于连接状态时返回客户端。
    private AdsClient? GetConnectedClient()
    {
        lock (_stateSync)
        {
            return _connected && _adsClient?.IsConnected == true ? _adsClient : null;
        }
    }

    // 检查轴号必须处于 1 到 4 之间。
    private static void ValidateAxisNumber(int axisNumber)
    {
        if (axisNumber is < 1 or > 4)
        {
            throw new ArgumentOutOfRangeException(nameof(axisNumber), "轴号必须为 1 到 4。");
        }
    }

    // 服务释放后继续调用时，立即抛出明确异常。
    private void ThrowIfDisposed()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
    }

    // 关闭 ADS 连接并释放服务内部资源。
    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        try
        {
            DisconnectAsync(CancellationToken.None).GetAwaiter().GetResult();
        }
        catch
        {
            // 窗体关闭阶段不再向用户抛出通信清理异常。
        }

        _disposed = true;
        _waveCancellation?.Dispose();
        _waveStopGate.Dispose();
        _operationGate.Dispose();
    }

}
