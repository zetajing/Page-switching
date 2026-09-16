using System.Configuration;
using InduLink.Abstractions;
using InduLink.Protocols.Ads;

namespace Page_switching;

public sealed class AxisSnapshot
{
    public AxisSnapshot(int axisNumber)
    {
        AxisNumber = axisNumber;
        StatusText = "未连接";
    }

    public int AxisNumber { get; }
    public double? ActualPosition { get; internal set; }
    public double? Speed { get; internal set; }
    public bool IsEnabled { get; internal set; }
    public bool IsHomed { get; internal set; }
    public bool HasAlarm { get; internal set; }
    public bool PositiveLimit { get; internal set; }
    public bool NegativeLimit { get; internal set; }
    public bool PositiveLimitAvailable { get; internal set; }
    public bool NegativeLimitAvailable { get; internal set; }
    public bool? OriginSignal { get; internal set; }
    public string StatusText { get; internal set; }
}

public sealed class AxisSymbolMap
{
    public string ActualPosition { get; init; } = string.Empty;
    public string Speed { get; init; } = string.Empty;
    public string Enabled { get; init; } = string.Empty;
    public string Homed { get; init; } = string.Empty;
    public string Alarm { get; init; } = string.Empty;
    public string PositiveLimit { get; init; } = string.Empty;
    public string NegativeLimit { get; init; } = string.Empty;
    public string OriginSignal { get; init; } = string.Empty;
    public string EnableCommand { get; init; } = string.Empty;
    public string ResetAlarmCommand { get; init; } = string.Empty;
    public string HomeCommand { get; init; } = string.Empty;
    public string StopCommand { get; init; } = string.Empty;
    public string JogSpeedCommand { get; init; } = string.Empty;
    public string JogPositiveCommand { get; init; } = string.Empty;
    public string JogNegativeCommand { get; init; } = string.Empty;

    public bool HasActualPositionSymbol => !string.IsNullOrWhiteSpace(ActualPosition);
}

public sealed class AxisServiceOptions
{
    public string AmsNetId { get; init; } = string.Empty;
    public int AdsPort { get; init; } = 851;
    public int ConnectTimeoutMilliseconds { get; init; } = 10000;
    public int OperationTimeoutMilliseconds { get; init; } = 5000;
    public int RefreshIntervalMilliseconds { get; init; } = 100;
    public string Unit { get; init; } = "°";
    public double MinimumPosition { get; init; } = -20;
    public double MaximumPosition { get; init; } = 20;
    public IReadOnlyList<AxisSymbolMap> AxisSymbols { get; init; } = Array.Empty<AxisSymbolMap>();

    public static AxisServiceOptions FromConfiguration()
    {
        var settings = ConfigurationManager.AppSettings;
        var symbols = new AxisSymbolMap[4];
        for (var axisNumber = 1; axisNumber <= 4; axisNumber++)
        {
            symbols[axisNumber - 1] = new AxisSymbolMap
            {
                ActualPosition = settings[$"AdsAxis{axisNumber}ActualPosition"] ?? "",
                Speed = settings[$"AdsAxis{axisNumber}Speed"] ?? "",
                Enabled = settings[$"AdsAxis{axisNumber}Enabled"] ?? "",
                Homed = settings[$"AdsAxis{axisNumber}Homed"] ?? "",
                Alarm = settings[$"AdsAxis{axisNumber}Alarm"] ?? "",
                PositiveLimit = settings[$"AdsAxis{axisNumber}PositiveLimit"] ?? "",
                NegativeLimit = settings[$"AdsAxis{axisNumber}NegativeLimit"] ?? "",
                OriginSignal = settings[$"AdsAxis{axisNumber}OriginSignal"] ?? "",
                EnableCommand = settings[$"AdsAxis{axisNumber}EnableCommand"] ?? "",
                ResetAlarmCommand = settings[$"AdsAxis{axisNumber}ResetAlarmCommand"] ?? "",
                HomeCommand = settings[$"AdsAxis{axisNumber}HomeCommand"] ?? "",
                StopCommand = settings[$"AdsAxis{axisNumber}StopCommand"] ?? "",
                JogSpeedCommand = settings[$"AdsAxis{axisNumber}JogSpeedCommand"] ?? "",
                JogPositiveCommand = settings[$"AdsAxis{axisNumber}JogPositiveCommand"] ?? "",
                JogNegativeCommand = settings[$"AdsAxis{axisNumber}JogNegativeCommand"] ?? ""
            };
        }

        return new AxisServiceOptions
        {
            AmsNetId = settings["AdsAmsNetId"] ?? "",
            AdsPort = Convert.ToInt32(settings["AdsPort"] ?? "851"),
            ConnectTimeoutMilliseconds = Convert.ToInt32(settings["AdsConnectTimeoutMs"] ?? "10000"),
            OperationTimeoutMilliseconds = Convert.ToInt32(settings["AdsOperationTimeoutMs"] ?? "5000"),
            RefreshIntervalMilliseconds = Convert.ToInt32(settings["AdsRefreshIntervalMs"] ?? "100"),
            Unit = settings["AxisUnit"] ?? "°",
            MinimumPosition = Convert.ToDouble(settings["AxisMinimum"] ?? "-20",
                System.Globalization.CultureInfo.InvariantCulture),
            MaximumPosition = Convert.ToDouble(settings["AxisMaximum"] ?? "20",
                System.Globalization.CultureInfo.InvariantCulture),
            AxisSymbols = symbols
        };
    }
}

public sealed class AxisService : IDisposable
{
    private readonly AxisServiceOptions _options;
    private readonly SemaphoreSlim _operationGate = new(1, 1);
    private readonly object _stateSync = new();
    private AdsClient? _adsClient;
    private bool _connected;
    private string? _lastError;
    private bool _disposed;

    public AxisService(AxisServiceOptions options)
    {
        _options = options ?? throw new ArgumentNullException(nameof(options));
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

    public bool CanControlAll => IsConnected && _options.AxisSymbols.All(map =>
            !string.IsNullOrWhiteSpace(map.EnableCommand) &&
            !string.IsNullOrWhiteSpace(map.ResetAlarmCommand) &&
            !string.IsNullOrWhiteSpace(map.HomeCommand) &&
            !string.IsNullOrWhiteSpace(map.StopCommand));

    public bool CanControlAxis(int axisNumber)
    {
        ValidateAxisNumber(axisNumber);
        var map = _options.AxisSymbols[axisNumber - 1];
        return IsConnected &&
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
                return "ADS 已连接";
            }

            return string.IsNullOrWhiteSpace(_lastError) ? "ADS 未连接" : "ADS 连接失败";
        }
    }

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

    public async Task DisconnectAsync(CancellationToken cancellationToken)
    {
        if (_disposed)
        {
            return;
        }

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

    public Task EnableAllAsync(CancellationToken cancellationToken) =>
        WriteAllAsync(
            symbolSelector: map => map.EnableCommand,
            value: true,
            cancellationToken);

    public Task DisableAllAsync(CancellationToken cancellationToken) =>
        WriteAllAsync(
            symbolSelector: map => map.EnableCommand,
            value: false,
            cancellationToken);

    private async Task WriteAllAsync(
        Func<AxisSymbolMap, string> symbolSelector,
        bool value,
        CancellationToken cancellationToken)
    {
        ThrowIfDisposed();
        await _operationGate.WaitAsync(cancellationToken).ConfigureAwait(false);
        try
        {
            var client = GetConnectedClient() ?? throw new InvalidOperationException("ADS 尚未连接。");
            await client.WriteManyAsync(CreateCommandRequests(symbolSelector, value), cancellationToken)
                .ConfigureAwait(false);
        }
        finally
        {
            _operationGate.Release();
        }
    }

    public Task ResetAlarmsAsync(CancellationToken cancellationToken) =>
        PulseAllAsync(
            symbolSelector: map => map.ResetAlarmCommand,
            value: true,
            cancellationToken);

    public Task HomeAllAsync(CancellationToken cancellationToken) =>
        PulseAllAsync(
            symbolSelector: map => map.HomeCommand,
            value: true,
            cancellationToken);

    public Task StopAllAsync(CancellationToken cancellationToken) =>
        PulseAllAsync(
            symbolSelector: map => map.StopCommand,
            value: true,
            cancellationToken);

    public Task HomeAxisAsync(int axisNumber, CancellationToken cancellationToken) =>
        PulseAxisAsync(
            axisNumber,
            symbolSelector: map => map.HomeCommand,
            value: true,
            cancellationToken);

    public Task StopAxisAsync(int axisNumber, CancellationToken cancellationToken) =>
        PulseAxisAsync(
            axisNumber,
            symbolSelector: map => map.StopCommand,
            value: true,
            cancellationToken);

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

    private async Task PulseAllAsync(
        Func<AxisSymbolMap, string> symbolSelector,
        bool value,
        CancellationToken cancellationToken)
    {
        ThrowIfDisposed();
        await _operationGate.WaitAsync(cancellationToken).ConfigureAwait(false);
        try
        {
            var client = GetConnectedClient() ?? throw new InvalidOperationException("ADS 尚未连接。");
            var requests = CreateCommandRequests(symbolSelector, value);
            await client.WriteManyAsync(requests, cancellationToken).ConfigureAwait(false);
            await Task.Delay(50, cancellationToken).ConfigureAwait(false);
            await client.WriteManyAsync(CreateCommandRequests(symbolSelector, false), cancellationToken)
                .ConfigureAwait(false);
        }
        finally
        {
            _operationGate.Release();
        }
    }

    private async Task PulseAxisAsync(
        int axisNumber,
        Func<AxisSymbolMap, string> symbolSelector,
        bool value,
        CancellationToken cancellationToken)
    {
        ThrowIfDisposed();
        ValidateAxisNumber(axisNumber);
        await _operationGate.WaitAsync(cancellationToken).ConfigureAwait(false);
        try
        {
            var client = GetConnectedClient() ?? throw new InvalidOperationException("ADS 尚未连接。");
            var symbol = symbolSelector(_options.AxisSymbols[axisNumber - 1]);
            if (string.IsNullOrWhiteSpace(symbol))
            {
                throw new InvalidOperationException($"轴 {axisNumber} 尚未配置 ADS 控制符号。");
            }

            await client.WriteAsync(
                new WriteRequest(client.DeviceId, symbol, DataType.Bool, value), cancellationToken)
                .ConfigureAwait(false);
            await Task.Delay(50, cancellationToken).ConfigureAwait(false);
            await client.WriteAsync(
                new WriteRequest(client.DeviceId, symbol, DataType.Bool, false), cancellationToken)
                .ConfigureAwait(false);
        }
        finally
        {
            _operationGate.Release();
        }
    }

    private IReadOnlyCollection<WriteRequest> CreateCommandRequests(
        Func<AxisSymbolMap, string> symbolSelector,
        bool value)
    {
        var client = GetConnectedClient() ?? throw new InvalidOperationException("ADS 尚未连接。");
        var requests = new List<WriteRequest>(_options.AxisSymbols.Count);
        for (var index = 0; index < _options.AxisSymbols.Count; index++)
        {
            var symbol = symbolSelector(_options.AxisSymbols[index]);
            if (string.IsNullOrWhiteSpace(symbol))
            {
                throw new InvalidOperationException($"轴 {index + 1} 尚未配置 ADS 控制符号。");
            }

            requests.Add(new WriteRequest(client.DeviceId, symbol, DataType.Bool, value));
        }

        return requests;
    }

    private async Task<IReadOnlyList<AxisSnapshot>> ReadAdsSnapshotsAsync(
        AdsClient client,
        CancellationToken cancellationToken)
    {
        var snapshots = new List<AxisSnapshot>(4);
        for (var axisIndex = 0; axisIndex < 4; axisIndex++)
        {
            snapshots.Add(await ReadAxisAsync(client, axisIndex, cancellationToken).ConfigureAwait(false));
        }

        return snapshots;
    }

    private async Task<AxisSnapshot> ReadAxisAsync(
        AdsClient client,
        int axisIndex,
        CancellationToken cancellationToken)
    {
        var symbols = _options.AxisSymbols[axisIndex];
        var snapshot = new AxisSnapshot(axisIndex + 1) { StatusText = "未配置" };
        if (!symbols.HasActualPositionSymbol)
        {
            return snapshot;
        }

        var result = await client.ReadManyAsync(
        [
            new ReadRequest(client.DeviceId, symbols.ActualPosition, DataType.Double),
            new ReadRequest(client.DeviceId, symbols.Speed, DataType.Double),
            new ReadRequest(client.DeviceId, symbols.Enabled, DataType.Bool),
            new ReadRequest(client.DeviceId, symbols.Homed, DataType.Bool),
            new ReadRequest(client.DeviceId, symbols.Alarm, DataType.Bool),
            new ReadRequest(client.DeviceId, symbols.PositiveLimit, DataType.Bool),
            new ReadRequest(client.DeviceId, symbols.NegativeLimit, DataType.Bool),
            new ReadRequest(client.DeviceId, symbols.OriginSignal, DataType.Bool)
        ], cancellationToken).ConfigureAwait(false);

        var values = result.Values;
        if (values.Count != 8 || values.Any(value =>
                value.Quality != QualityStatus.Good || value.Value is null))
        {
            snapshot.StatusText = "读取失败";
            return snapshot;
        }

        snapshot.ActualPosition = (double)values[0].Value;
        snapshot.Speed = (double)values[1].Value;
        snapshot.IsEnabled = (bool)values[2].Value;
        snapshot.IsHomed = (bool)values[3].Value;
        snapshot.HasAlarm = (bool)values[4].Value;
        snapshot.PositiveLimit = (bool)values[5].Value;
        snapshot.NegativeLimit = (bool)values[6].Value;
        snapshot.OriginSignal = (bool)values[7].Value;
        snapshot.PositiveLimitAvailable = true;
        snapshot.NegativeLimitAvailable = true;
        snapshot.StatusText = GetAxisStatus(snapshot);

        return snapshot;
    }

    private static string GetAxisStatus(AxisSnapshot axis) =>
        axis.HasAlarm ? "报警" :
        axis.PositiveLimit || axis.NegativeLimit ? "限位" :
        !axis.IsEnabled ? "未使能" :
        !axis.IsHomed ? "未回零" : "就绪";

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

    private AdsClient? GetConnectedClient()
    {
        lock (_stateSync)
        {
            return _connected && _adsClient?.IsConnected == true ? _adsClient : null;
        }
    }

    private static void ValidateAxisNumber(int axisNumber)
    {
        if (axisNumber is < 1 or > 4)
        {
            throw new ArgumentOutOfRangeException(nameof(axisNumber), "轴号必须为 1 到 4。");
        }
    }

    private void ThrowIfDisposed()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
    }

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
        _operationGate.Dispose();
    }

}
