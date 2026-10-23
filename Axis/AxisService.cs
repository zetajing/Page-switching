using InduLink.Protocols.Ads;

namespace Page_switching;

// 共享连接与生命周期入口；读取、手动、控制权、点位下发各在对应目录。
// 分部文件共用本连接和串行锁，避免各功能重复建立写连接。
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

        // 每根轴只创建一个对象；页面取得的是同一组对象，通信仍由本服务统一管理。
        Axis[] axes = new Axis[4];
        for (int index = 0; index < axes.Length; index++)
        {
            axes[index] = new Axis(this, index + 1);
        }
        Axes = Array.AsReadOnly(axes);
    }

    public IReadOnlyList<Axis> Axes { get; }

    // 外部使用 1～4 的轴号，内部集合下标从 0 开始。
    public Axis GetAxis(int axisNumber)
    {
        ValidateAxisNumber(axisNumber);
        return Axes[axisNumber - 1];
    }

    public int RefreshIntervalMilliseconds => _options.RefreshIntervalMilliseconds;
    public string Unit => _options.Unit;
    public double MinimumPosition => _options.MinimumPosition;
    public double MaximumPosition => _options.MaximumPosition;
    public string? LastError
    {
        get { lock (_stateSync) return _lastError; }
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
            // ADS 连接成功仍可能读不到反馈；保留具体变量和底层错误，方便直接查问题。
            string? error = LastError;
            if (IsConnected)
            {
                if (!string.IsNullOrWhiteSpace(error)) return "ADS 已连接；" + error;
                if (ControlStationCode == 0) return "ADS 已连接，未配置本站身份";
                return HasControlAuthority ? "ADS 已连接，本站已授权" : "ADS 已连接，本站未授权";
            }

            return string.IsNullOrWhiteSpace(error) ? "ADS 未连接" : "ADS 未连接；" + error;
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
            UpdateAxisSnapshots(CreateUnavailableSnapshots("ADS 未连接"));

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
