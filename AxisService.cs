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
    public bool UseSimulation { get; init; } = true;
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
        var symbols = Enumerable.Range(1, 4)
            .Select(index => new AxisSymbolMap
            {
                ActualPosition = Read($"AdsAxis{index}ActualPosition"),
                Speed = Read($"AdsAxis{index}Speed"),
                Enabled = Read($"AdsAxis{index}Enabled"),
                Homed = Read($"AdsAxis{index}Homed"),
                Alarm = Read($"AdsAxis{index}Alarm"),
                PositiveLimit = Read($"AdsAxis{index}PositiveLimit"),
                NegativeLimit = Read($"AdsAxis{index}NegativeLimit"),
                OriginSignal = Read($"AdsAxis{index}OriginSignal"),
                EnableCommand = Read($"AdsAxis{index}EnableCommand"),
                ResetAlarmCommand = Read($"AdsAxis{index}ResetAlarmCommand"),
                HomeCommand = Read($"AdsAxis{index}HomeCommand"),
                StopCommand = Read($"AdsAxis{index}StopCommand"),
                JogSpeedCommand = Read($"AdsAxis{index}JogSpeedCommand"),
                JogPositiveCommand = Read($"AdsAxis{index}JogPositiveCommand"),
                JogNegativeCommand = Read($"AdsAxis{index}JogNegativeCommand")
            })
            .ToArray();

        var minimum = ReadDouble("AxisMinimum", -20);
        var maximum = ReadDouble("AxisMaximum", 20);
        if (maximum <= minimum)
        {
            minimum = -20;
            maximum = 20;
        }

        return new AxisServiceOptions
        {
            UseSimulation = ReadBoolean("UseSimulation", true),
            AmsNetId = Read("AdsAmsNetId"),
            AdsPort = Math.Clamp(ReadInt("AdsPort", 851), 1, 65535),
            ConnectTimeoutMilliseconds = Math.Clamp(ReadInt("AdsConnectTimeoutMs", 10000), 1000, 60000),
            OperationTimeoutMilliseconds = Math.Clamp(ReadInt("AdsOperationTimeoutMs", 5000), 1000, 60000),
            RefreshIntervalMilliseconds = Math.Clamp(ReadInt("AdsRefreshIntervalMs", 100), 50, 2000),
            Unit = string.IsNullOrWhiteSpace(Read("AxisUnit")) ? "°" : Read("AxisUnit"),
            MinimumPosition = minimum,
            MaximumPosition = maximum,
            AxisSymbols = symbols
        };
    }

    private static string Read(string key) =>
        ConfigurationManager.AppSettings[key]?.Trim() ?? string.Empty;

    private static int ReadInt(string key, int fallback) =>
        int.TryParse(Read(key), out var value) ? value : fallback;

    private static double ReadDouble(string key, double fallback) =>
        double.TryParse(Read(key), System.Globalization.NumberStyles.Float,
            System.Globalization.CultureInfo.InvariantCulture, out var value)
            ? value
            : fallback;

    private static bool ReadBoolean(string key, bool fallback) =>
        bool.TryParse(Read(key), out var value) ? value : fallback;
}

public sealed class AxisService : IDisposable
{
    private readonly AxisServiceOptions _options;
    private readonly SemaphoreSlim _operationGate = new(1, 1);
    private readonly object _simulationSync = new();
    private readonly SimulationAxis[] _simulationAxes;
    private AdsClient? _adsClient;
    private bool _connected;
    private string? _lastError;
    private DateTime _lastSimulationUpdateUtc = DateTime.UtcNow;
    private bool _disposed;

    public AxisService(AxisServiceOptions options)
    {
        _options = options ?? throw new ArgumentNullException(nameof(options));
        if (_options.AxisSymbols.Count != 4)
        {
            throw new ArgumentException("必须提供四个造波板轴的 ADS 符号映射。", nameof(options));
        }

        _simulationAxes = Enumerable.Range(1, 4)
            .Select(index => new SimulationAxis(index, _options.MinimumPosition, _options.MaximumPosition))
            .ToArray();
    }

    public bool IsSimulation => _options.UseSimulation;
    public int RefreshIntervalMilliseconds => _options.RefreshIntervalMilliseconds;
    public string Unit => _options.Unit;
    public double MinimumPosition => _options.MinimumPosition;
    public double MaximumPosition => _options.MaximumPosition;
    public string? LastError => _lastError;

    public bool CanControlAll => IsSimulation ||
        IsConnected && _options.AxisSymbols.All(map =>
            !string.IsNullOrWhiteSpace(map.EnableCommand) &&
            !string.IsNullOrWhiteSpace(map.ResetAlarmCommand) &&
            !string.IsNullOrWhiteSpace(map.HomeCommand) &&
            !string.IsNullOrWhiteSpace(map.StopCommand));

    public bool CanControlAxis(int axisNumber)
    {
        ValidateAxisNumber(axisNumber);
        if (IsSimulation)
        {
            return IsConnected;
        }

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
            if (IsSimulation)
            {
                return _connected;
            }

            lock (_simulationSync)
            {
                return _connected && _adsClient?.IsConnected == true;
            }
        }
    }

    public string ConnectionStateText
    {
        get
        {
            if (IsSimulation)
            {
                return "模拟模式";
            }

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

        if (IsSimulation)
        {
            lock (_simulationSync)
            {
                _connected = true;
                _lastError = null;
                _lastSimulationUpdateUtc = DateTime.UtcNow;
            }

            return;
        }

        await _operationGate.WaitAsync(cancellationToken).ConfigureAwait(false);
        try
        {
            if (IsConnected)
            {
                return;
            }

            var client = new AdsClient(new AdsClientOptions
            {
                DeviceId = "virtual-plc",
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
                lock (_simulationSync)
                {
                    _adsClient = client;
                    _connected = true;
                    _lastError = null;
                }
            }
            catch (Exception ex)
            {
                client.Dispose();
                lock (_simulationSync)
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
            lock (_simulationSync)
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

        if (IsSimulation)
        {
            lock (_simulationSync)
            {
                UpdateSimulation();
                return _simulationAxes.Select(axis => axis.ToSnapshot()).ToArray();
            }
        }

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
            lock (_simulationSync)
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
        RunSimulationOrWriteLevelAsync(
            simulationAction: () =>
            {
                foreach (var axis in _simulationAxes) axis.Enabled = true;
            },
            symbolSelector: map => map.EnableCommand,
            value: true,
            cancellationToken);

    public Task DisableAllAsync(CancellationToken cancellationToken) =>
        RunSimulationOrWriteLevelAsync(
            simulationAction: () =>
            {
                foreach (var axis in _simulationAxes)
                {
                    axis.Enabled = false;
                    axis.Jogging = false;
                    axis.DemoMotion = false;
                    axis.Target = axis.Actual;
                }
            },
            symbolSelector: map => map.EnableCommand,
            value: false,
            cancellationToken);

    private async Task RunSimulationOrWriteLevelAsync(
        Action simulationAction,
        Func<AxisSymbolMap, string> symbolSelector,
        bool value,
        CancellationToken cancellationToken)
    {
        ThrowIfDisposed();
        if (IsSimulation)
        {
            lock (_simulationSync) simulationAction();
            return;
        }

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
        RunSimulationOrMomentaryAsync(
            simulationAction: () =>
            {
                foreach (var axis in _simulationAxes) axis.Alarm = false;
            },
            symbolSelector: map => map.ResetAlarmCommand,
            value: true,
            cancellationToken);

    public Task HomeAllAsync(CancellationToken cancellationToken) =>
        RunSimulationOrMomentaryAsync(
            simulationAction: () =>
            {
                foreach (var axis in _simulationAxes)
                {
                    axis.Target = 0;
                    axis.Homed = true;
                    axis.Jogging = false;
                    axis.DemoMotion = false;
                }
            },
            symbolSelector: map => map.HomeCommand,
            value: true,
            cancellationToken);

    public Task StopAllAsync(CancellationToken cancellationToken) =>
        RunSimulationOrMomentaryAsync(
            simulationAction: () =>
            {
                foreach (var axis in _simulationAxes)
                {
                    axis.Target = axis.Actual;
                    axis.Jogging = false;
                    axis.DemoMotion = false;
                }
            },
            symbolSelector: map => map.StopCommand,
            value: true,
            cancellationToken);

    public Task HomeAxisAsync(int axisNumber, CancellationToken cancellationToken) =>
        RunAxisSimulationOrMomentaryAsync(
            axisNumber,
            simulationAction: axis =>
            {
                axis.Target = 0;
                axis.Homed = true;
                axis.Jogging = false;
                axis.DemoMotion = false;
            },
            symbolSelector: map => map.HomeCommand,
            value: true,
            cancellationToken);

    public Task StopAxisAsync(int axisNumber, CancellationToken cancellationToken) =>
        RunAxisSimulationOrMomentaryAsync(
            axisNumber,
            simulationAction: axis =>
            {
                axis.Target = axis.Actual;
                axis.Jogging = false;
                axis.DemoMotion = false;
            },
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

        if (IsSimulation)
        {
            lock (_simulationSync)
            {
                var axis = _simulationAxes[axisNumber - 1];
                axis.Jogging = start;
                axis.JogPositive = positive;
                axis.JogSpeed = Math.Clamp(Math.Abs(speed), 0.1, 180);
                if (start)
                {
                    axis.Enabled = true;
                    axis.DemoMotion = false;
                }
            }

            return;
        }

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

    private async Task RunSimulationOrMomentaryAsync(
        Action simulationAction,
        Func<AxisSymbolMap, string> symbolSelector,
        bool value,
        CancellationToken cancellationToken)
    {
        ThrowIfDisposed();
        if (IsSimulation)
        {
            lock (_simulationSync) simulationAction();
            return;
        }

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

    private async Task RunAxisSimulationOrMomentaryAsync(
        int axisNumber,
        Action<SimulationAxis> simulationAction,
        Func<AxisSymbolMap, string> symbolSelector,
        bool value,
        CancellationToken cancellationToken)
    {
        ThrowIfDisposed();
        ValidateAxisNumber(axisNumber);
        if (IsSimulation)
        {
            lock (_simulationSync) simulationAction(_simulationAxes[axisNumber - 1]);
            return;
        }

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
        var snapshots = CreateUnavailableSnapshots("未配置");
        var descriptors = new List<ReadDescriptor>();

        for (var index = 0; index < _options.AxisSymbols.Count; index++)
        {
            var map = _options.AxisSymbols[index];
            AddDescriptor(descriptors, index, map.ActualPosition, DataType.Double, ReadKind.Actual);
            AddDescriptor(descriptors, index, map.Speed, DataType.Double, ReadKind.Speed);
            AddDescriptor(descriptors, index, map.Enabled, DataType.Bool, ReadKind.Enabled);
            AddDescriptor(descriptors, index, map.Homed, DataType.Bool, ReadKind.Homed);
            AddDescriptor(descriptors, index, map.Alarm, DataType.Bool, ReadKind.Alarm);
            AddDescriptor(descriptors, index, map.PositiveLimit, DataType.Bool, ReadKind.PositiveLimit);
            AddDescriptor(descriptors, index, map.NegativeLimit, DataType.Bool, ReadKind.NegativeLimit);
            AddDescriptor(descriptors, index, map.OriginSignal, DataType.Bool, ReadKind.OriginSignal);
        }

        if (descriptors.Count == 0)
        {
            return snapshots;
        }

        var requests = descriptors
            .Select(descriptor => new ReadRequest(client.DeviceId, descriptor.Symbol, descriptor.DataType))
            .ToArray();
        var result = await client.ReadManyAsync(requests, cancellationToken).ConfigureAwait(false);

        foreach (var descriptor in descriptors)
        {
            var value = descriptor.Index < result.Values.Count
                ? result.Values[descriptor.Index]
                : null;
            if (value is null || value.Quality != QualityStatus.Good || value.Value is null)
            {
                descriptor.Failed = true;
                continue;
            }

            ApplyValue(snapshots[descriptor.AxisIndex], descriptor.Kind, value.Value);
        }

        for (var index = 0; index < snapshots.Count; index++)
        {
            var snapshot = snapshots[index];
            var map = _options.AxisSymbols[index];
            var hasFailure = descriptors.Any(item => item.AxisIndex == index && item.Failed);
            snapshot.StatusText = !map.HasActualPositionSymbol
                ? "未配置"
                : hasFailure
                    ? "读取失败"
                    : snapshot.HasAlarm
                        ? "报警"
                        : snapshot.PositiveLimit || snapshot.NegativeLimit
                            ? "限位"
                            : !snapshot.IsEnabled
                                ? "未使能"
                                : !snapshot.IsHomed
                                    ? "未回零"
                                    : "就绪";
        }

        return snapshots;
    }

    private static void AddDescriptor(
        ICollection<ReadDescriptor> descriptors,
        int axisIndex,
        string symbol,
        DataType dataType,
        ReadKind kind)
    {
        if (!string.IsNullOrWhiteSpace(symbol))
        {
            descriptors.Add(new ReadDescriptor(descriptors.Count, axisIndex, symbol, dataType, kind));
        }
    }

    private static void ApplyValue(AxisSnapshot snapshot, ReadKind kind, object value)
    {
        switch (kind)
        {
            case ReadKind.Actual:
                snapshot.ActualPosition = Convert.ToDouble(value, System.Globalization.CultureInfo.InvariantCulture);
                break;
            case ReadKind.Speed:
                snapshot.Speed = Convert.ToDouble(value, System.Globalization.CultureInfo.InvariantCulture);
                break;
            case ReadKind.Enabled:
                snapshot.IsEnabled = Convert.ToBoolean(value, System.Globalization.CultureInfo.InvariantCulture);
                break;
            case ReadKind.Homed:
                snapshot.IsHomed = Convert.ToBoolean(value, System.Globalization.CultureInfo.InvariantCulture);
                break;
            case ReadKind.Alarm:
                snapshot.HasAlarm = Convert.ToBoolean(value, System.Globalization.CultureInfo.InvariantCulture);
                break;
            case ReadKind.PositiveLimit:
                snapshot.PositiveLimit = Convert.ToBoolean(value, System.Globalization.CultureInfo.InvariantCulture);
                snapshot.PositiveLimitAvailable = true;
                break;
            case ReadKind.NegativeLimit:
                snapshot.NegativeLimit = Convert.ToBoolean(value, System.Globalization.CultureInfo.InvariantCulture);
                snapshot.NegativeLimitAvailable = true;
                break;
            case ReadKind.OriginSignal:
                snapshot.OriginSignal = Convert.ToBoolean(value, System.Globalization.CultureInfo.InvariantCulture);
                break;
        }
    }

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
        lock (_simulationSync)
        {
            return _connected && _adsClient?.IsConnected == true ? _adsClient : null;
        }
    }

    private void UpdateSimulation()
    {
        var now = DateTime.UtcNow;
        var elapsed = Math.Clamp((now - _lastSimulationUpdateUtc).TotalSeconds, 0, 0.25);
        _lastSimulationUpdateUtc = now;

        foreach (var axis in _simulationAxes)
        {
            if (axis.Jogging && axis.Enabled && !axis.Alarm)
            {
                axis.Target += (axis.JogPositive ? 1 : -1) * axis.JogSpeed * elapsed;
            }
            else if (axis.DemoMotion && Math.Abs(axis.Target - axis.Actual) < 0.01)
            {
                axis.Target = axis.Target > (_options.MinimumPosition + _options.MaximumPosition) / 2
                    ? _options.MinimumPosition + 10
                    : _options.MaximumPosition - 10;
            }

            axis.Target = Math.Clamp(axis.Target, _options.MinimumPosition, _options.MaximumPosition);
            var difference = axis.Target - axis.Actual;
            var commandedSpeed = axis.Jogging ? axis.JogSpeed : 12;
            var step = Math.Min(Math.Abs(difference), commandedSpeed * elapsed);
            axis.Actual += Math.Sign(difference) * step;
            axis.Speed = axis.Enabled && Math.Abs(difference) > 0.01
                ? Math.Sign(difference) * commandedSpeed
                : 0;
            axis.PositiveLimit = axis.Actual >= _options.MaximumPosition - 0.001;
            axis.NegativeLimit = axis.Actual <= _options.MinimumPosition + 0.001;
            axis.Status = axis.Alarm
                ? "报警"
                : axis.PositiveLimit || axis.NegativeLimit
                    ? "限位"
                    : !axis.Enabled
                        ? "未使能"
                        : !axis.Homed
                            ? "未回零"
                            : Math.Abs(difference) > 0.01
                                ? "运行中"
                                : "就绪";
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

    private sealed class SimulationAxis
    {
        public SimulationAxis(int axisNumber, double minimum, double maximum)
        {
            AxisNumber = axisNumber;
            Actual = minimum + (maximum - minimum) * (axisNumber - 1) / 3;
            Target = axisNumber % 2 == 0 ? maximum - 10 : minimum + 10;
            Enabled = true;
            Homed = true;
            DemoMotion = true;
        }

        public int AxisNumber { get; }
        public double Actual { get; set; }
        public double Target { get; set; }
        public double Speed { get; set; }
        public bool Enabled { get; set; }
        public bool Homed { get; set; }
        public bool Alarm { get; set; }
        public bool PositiveLimit { get; set; }
        public bool NegativeLimit { get; set; }
        public bool Jogging { get; set; }
        public bool JogPositive { get; set; }
        public double JogSpeed { get; set; } = 10;
        public bool DemoMotion { get; set; }
        public string Status { get; set; } = "未使能";

        public AxisSnapshot ToSnapshot() => new(AxisNumber)
        {
            ActualPosition = Actual,
            Speed = Speed,
            IsEnabled = Enabled,
            IsHomed = Homed,
            HasAlarm = Alarm,
            PositiveLimit = PositiveLimit,
            NegativeLimit = NegativeLimit,
            PositiveLimitAvailable = true,
            NegativeLimitAvailable = true,
            OriginSignal = Math.Abs(Actual) <= 0.1,
            StatusText = Status
        };
    }

    private sealed class ReadDescriptor
    {
        public ReadDescriptor(int index, int axisIndex, string symbol, DataType dataType, ReadKind kind)
        {
            Index = index;
            AxisIndex = axisIndex;
            Symbol = symbol;
            DataType = dataType;
            Kind = kind;
        }

        public int AxisIndex { get; }
        public string Symbol { get; }
        public DataType DataType { get; }
        public ReadKind Kind { get; }
        public bool Failed { get; set; }
        public int Index { get; }
    }

    private enum ReadKind
    {
        Actual,
        Speed,
        Enabled,
        Homed,
        Alarm,
        PositiveLimit,
        NegativeLimit,
        OriginSignal
    }
}
