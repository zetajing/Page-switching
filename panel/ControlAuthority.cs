using System.Configuration;

namespace Page_switching;

// 控制权页面：展示站点、Owner、PLC 状态和安全配置，后续接入真实 ADS 变量。
public sealed partial class ControlAuthority : UserControl
{
    private readonly AxisService _axisService;
    private readonly bool _ownsAxisService;
    private readonly System.Windows.Forms.Timer _refreshTimer = new() { Interval = 500 };

    // 供 Visual Studio Designer 使用的无参构造函数。
    public ControlAuthority()
        : this(new AxisService(AxisServiceOptions.FromConfiguration()), true)
    {
    }

    // 使用主窗体传入的共享 ADS 轴服务创建控制权页面。
    public ControlAuthority(AxisService axisService)
        : this(axisService, false)
    {
    }

    // 初始化控制权页面、状态刷新器和按钮事件。
    private ControlAuthority(AxisService axisService, bool ownsAxisService)
    {
        _axisService = axisService ?? throw new ArgumentNullException(nameof(axisService));
        _ownsAxisService = ownsAxisService;
        InitializeComponent();
        _requestButton.Click += (_, _) => ShowPendingMessage("申请控制权");
        _releaseButton.Click += (_, _) => ShowPendingMessage("释放控制权");
        RefreshConfiguration();
        _refreshTimer.Tick += (_, _) => RefreshConfiguration();
        VisibleChanged += (_, _) =>
        {
            if (Visible)
            {
                _refreshTimer.Start();
                RefreshConfiguration();
            }
            else
            {
                _refreshTimer.Stop();
            }
        };
        Disposed += (_, _) =>
        {
            _refreshTimer.Dispose();
            if (_ownsAxisService)
            {
                _axisService.Dispose();
            }
        };
    }

    // 从 App.config 读取控制权变量配置并刷新页面状态。
    private void RefreshConfiguration()
    {
        if (IsDisposed)
        {
            return;
        }

        var stationId = Read("ControlStationId", "1");
        var ownerConfigured = HasSetting("AdsControlOwnerStationId");
        var stateConfigured = HasSetting("AdsControlState");
        var safetyConfigured = HasSetting("AdsSafetyOk");
        var heartbeatConfigured = HasSetting("AdsControlHeartbeat");
        var requestConfigured = HasSetting("AdsRequestOwner");
        var releaseConfigured = HasSetting("AdsReleaseOwner");
        var stopConfigured = HasSetting("AdsSafeStopCommand");
        var allConfigured = ownerConfigured && stateConfigured && safetyConfigured &&
                            heartbeatConfigured && requestConfigured && releaseConfigured && stopConfigured;

        _connectionValue.Text = _axisService.IsConnected ? "已连接" : "未连接";
        _connectionValue.ForeColor = _axisService.IsConnected
            ? Color.FromArgb(5, 150, 105)
            : Color.FromArgb(220, 38, 38);
        _stationValue.Text = stationId;
        _ownerValue.Text = ownerConfigured ? "已配置，等待读取" : "待配置";
        _stateValue.Text = stateConfigured ? "已配置，等待读取" : "待配置";
        _safetyValue.Text = safetyConfigured ? "已配置，等待读取" : "待配置";
        _watchdogValue.Text = heartbeatConfigured
            ? $"{Read("ControlHeartbeatIntervalMs", "500")} ms / {Read("ControlHeartbeatMissLimit", "3")} 次"
            : "待配置";

        _requestButton.Enabled = allConfigured && _axisService.IsConnected;
        _releaseButton.Enabled = allConfigured && _axisService.IsConnected;
        _hintLabel.Text = allConfigured
            ? "控制权变量已填写，下一步接入 ADS 申请、释放和心跳读写。"
            : "请先在 App.config 填写控制权变量名；当前页面不会发送虚假的 PLC 命令。";
    }

    // 显示尚未接入 ADS 写入逻辑的操作提示。
    private void ShowPendingMessage(string action)
    {
        _hintLabel.Text = $"{action}接口待接入 ADS，收到 PLC 变量类型后启用。";
    }

    // 读取配置项并在缺失时返回默认值。
    private static string Read(string key, string fallback = "") =>
        ConfigurationManager.AppSettings[key]?.Trim() is { Length: > 0 } value ? value : fallback;

    // 判断配置项是否填写了 PLC 变量名。
    private static bool HasSetting(string key) =>
        !string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings[key]);
}
