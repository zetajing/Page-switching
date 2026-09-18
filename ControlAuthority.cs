using System.Configuration;

namespace Page_switching;

// 控制权页面：展示站点、Owner、PLC 状态和安全配置，后续接入真实 ADS 变量。
public sealed class ControlAuthority : UserControl
{
    private readonly AxisService _axisService;
    private readonly Label _connectionValue = new();
    private readonly Label _stationValue = new();
    private readonly Label _ownerValue = new();
    private readonly Label _stateValue = new();
    private readonly Label _safetyValue = new();
    private readonly Label _watchdogValue = new();
    private readonly Label _hintLabel = new();
    private readonly Button _requestButton = new();
    private readonly Button _releaseButton = new();
    private readonly System.Windows.Forms.Timer _refreshTimer = new() { Interval = 500 };

    // 使用共享 ADS 轴服务创建控制权页面，避免重复建立 PLC 连接。
    public ControlAuthority(AxisService axisService)
    {
        _axisService = axisService ?? throw new ArgumentNullException(nameof(axisService));
        Size = new Size(905, 681);
        BuildPage();
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
        Disposed += (_, _) => _refreshTimer.Dispose();
    }

    // 创建控制权页面的标题、状态卡片和操作按钮。
    private void BuildPage()
    {
        BackColor = Color.FromArgb(241, 245, 249);
        var root = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 1,
            RowCount = 4,
            Padding = new Padding(20),
            BackColor = Color.FromArgb(241, 245, 249)
        };
        root.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
        root.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
        root.RowStyles.Add(new RowStyle(SizeType.Absolute, 210F));
        root.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

        root.Controls.Add(new Label
        {
            Dock = DockStyle.Fill,
            Text = "控制权申请",
            Font = new Font("Microsoft YaHei UI", 16F, FontStyle.Bold),
            ForeColor = Color.FromArgb(15, 23, 42),
            TextAlign = ContentAlignment.MiddleLeft
        }, 0, 0);
        root.Controls.Add(new Label
        {
            Dock = DockStyle.Fill,
            Text = "PLC 统一管理 Owner，只有当前 Owner 可以执行控制命令",
            Font = new Font("Microsoft YaHei UI", 9F),
            ForeColor = Color.FromArgb(71, 85, 105),
            TextAlign = ContentAlignment.MiddleLeft
        }, 0, 1);

        var statusGroup = new GroupBox
        {
            Dock = DockStyle.Fill,
            Text = "控制权状态",
            Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold),
            ForeColor = Color.FromArgb(15, 23, 42),
            Padding = new Padding(14, 18, 14, 10)
        };
        var statusLayout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 4,
            RowCount = 3
        };
        statusLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
        statusLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        statusLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
        statusLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        for (var row = 0; row < 3; row++)
        {
            statusLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 33.333F));
        }

        AddStatusRow(statusLayout, 0, "ADS 连接", _connectionValue, "本站点", _stationValue);
        AddStatusRow(statusLayout, 1, "当前 Owner", _ownerValue, "PLC 状态", _stateValue);
        AddStatusRow(statusLayout, 2, "安全状态", _safetyValue, "看门狗", _watchdogValue);
        statusGroup.Controls.Add(statusLayout);
        root.Controls.Add(statusGroup, 0, 2);

        var actionGroup = new GroupBox
        {
            Dock = DockStyle.Fill,
            Text = "控制权操作",
            Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold),
            ForeColor = Color.FromArgb(15, 23, 42),
            Padding = new Padding(14, 18, 14, 12)
        };
        var actionLayout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 2,
            RowCount = 3
        };
        actionLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 170F));
        actionLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        actionLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 52F));
        actionLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 52F));
        actionLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

        ConfigureActionButton(_requestButton, "申请控制权");
        ConfigureActionButton(_releaseButton, "释放控制权");
        _requestButton.Click += (_, _) => ShowPendingMessage("申请控制权");
        _releaseButton.Click += (_, _) => ShowPendingMessage("释放控制权");
        actionLayout.Controls.Add(_requestButton, 0, 0);
        actionLayout.Controls.Add(_releaseButton, 0, 1);
        _hintLabel.Dock = DockStyle.Fill;
        _hintLabel.Font = new Font("Microsoft YaHei UI", 9F);
        _hintLabel.ForeColor = Color.FromArgb(180, 83, 9);
        _hintLabel.TextAlign = ContentAlignment.MiddleLeft;
        actionLayout.Controls.Add(_hintLabel, 1, 0);
        actionLayout.SetRowSpan(_hintLabel, 2);
        actionGroup.Controls.Add(actionLayout);
        root.Controls.Add(actionGroup, 0, 3);

        Controls.Add(root);
    }

    // 添加一行状态标签和值标签。
    private static void AddStatusRow(
        TableLayoutPanel layout,
        int row,
        string firstCaption,
        Label firstValue,
        string secondCaption,
        Label secondValue)
    {
        layout.Controls.Add(CreateCaption(firstCaption), 0, row);
        layout.Controls.Add(firstValue, 1, row);
        layout.Controls.Add(CreateCaption(secondCaption), 2, row);
        layout.Controls.Add(secondValue, 3, row);
        ConfigureValue(firstValue);
        ConfigureValue(secondValue);
    }

    // 创建状态行左侧的字段名称。
    private static Label CreateCaption(string text) => new()
    {
        Dock = DockStyle.Fill,
        Text = text,
        Font = new Font("Microsoft YaHei UI", 9F),
        ForeColor = Color.FromArgb(71, 85, 105),
        TextAlign = ContentAlignment.MiddleLeft
    };

    // 设置状态值标签的统一样式。
    private static void ConfigureValue(Label label)
    {
        label.Dock = DockStyle.Fill;
        label.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
        label.ForeColor = Color.FromArgb(15, 23, 42);
        label.TextAlign = ContentAlignment.MiddleLeft;
    }

    // 设置控制权操作按钮的统一样式。
    private static void ConfigureActionButton(Button button, string text)
    {
        button.Dock = DockStyle.Fill;
        button.Text = text;
        button.Enabled = false;
        button.FlatStyle = FlatStyle.Flat;
        button.FlatAppearance.BorderSize = 0;
        button.BackColor = Color.FromArgb(226, 232, 240);
        button.ForeColor = Color.FromArgb(100, 116, 139);
        button.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
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
