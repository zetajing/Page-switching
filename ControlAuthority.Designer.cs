namespace Page_switching;

public sealed partial class ControlAuthority
{
    private System.ComponentModel.IContainer components = null!;
    private TableLayoutPanel rootLayout = null!;
    private Label titleLabel = null!;
    private Label subtitleLabel = null!;
    private GroupBox statusGroup = null!;
    private TableLayoutPanel statusLayout = null!;
    private Label connectionCaptionLabel = null!;
    private Label stationCaptionLabel = null!;
    private Label ownerCaptionLabel = null!;
    private Label stateCaptionLabel = null!;
    private Label safetyCaptionLabel = null!;
    private Label watchdogCaptionLabel = null!;
    private Label _connectionValue = null!;
    private Label _stationValue = null!;
    private Label _ownerValue = null!;
    private Label _stateValue = null!;
    private Label _safetyValue = null!;
    private Label _watchdogValue = null!;
    private GroupBox actionGroup = null!;
    private TableLayoutPanel actionLayout = null!;
    private Button _requestButton = null!;
    private Button _releaseButton = null!;
    private Label _hintLabel = null!;

    // 释放 Designer 创建的控件资源。
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            components?.Dispose();
        }

        base.Dispose(disposing);
    }

    // 创建控制权页面的标题、状态区域和操作区域。
    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        rootLayout = new TableLayoutPanel();
        titleLabel = new Label();
        subtitleLabel = new Label();
        statusGroup = new GroupBox();
        statusLayout = new TableLayoutPanel();
        connectionCaptionLabel = new Label();
        stationCaptionLabel = new Label();
        ownerCaptionLabel = new Label();
        stateCaptionLabel = new Label();
        safetyCaptionLabel = new Label();
        watchdogCaptionLabel = new Label();
        _connectionValue = new Label();
        _stationValue = new Label();
        _ownerValue = new Label();
        _stateValue = new Label();
        _safetyValue = new Label();
        _watchdogValue = new Label();
        actionGroup = new GroupBox();
        actionLayout = new TableLayoutPanel();
        _requestButton = new Button();
        _releaseButton = new Button();
        _hintLabel = new Label();
        rootLayout.SuspendLayout();
        statusGroup.SuspendLayout();
        statusLayout.SuspendLayout();
        actionGroup.SuspendLayout();
        actionLayout.SuspendLayout();
        SuspendLayout();
        //
        // rootLayout
        //
        rootLayout.BackColor = Color.FromArgb(241, 245, 249);
        rootLayout.ColumnCount = 1;
        rootLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        rootLayout.Controls.Add(titleLabel, 0, 0);
        rootLayout.Controls.Add(subtitleLabel, 0, 1);
        rootLayout.Controls.Add(statusGroup, 0, 2);
        rootLayout.Controls.Add(actionGroup, 0, 3);
        rootLayout.Dock = DockStyle.Fill;
        rootLayout.Padding = new Padding(20);
        rootLayout.RowCount = 4;
        rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
        rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
        rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 210F));
        rootLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        rootLayout.TabIndex = 0;
        //
        // titleLabel
        //
        titleLabel.Dock = DockStyle.Fill;
        titleLabel.Font = new Font("Microsoft YaHei UI", 16F, FontStyle.Bold);
        titleLabel.ForeColor = Color.FromArgb(15, 23, 42);
        titleLabel.Text = "控制权申请";
        titleLabel.TextAlign = ContentAlignment.MiddleLeft;
        //
        // subtitleLabel
        //
        subtitleLabel.Dock = DockStyle.Fill;
        subtitleLabel.Font = new Font("Microsoft YaHei UI", 9F);
        subtitleLabel.ForeColor = Color.FromArgb(71, 85, 105);
        subtitleLabel.Text = "PLC 统一管理 Owner，只有当前 Owner 可以执行控制命令";
        subtitleLabel.TextAlign = ContentAlignment.MiddleLeft;
        //
        // statusGroup
        //
        statusGroup.Controls.Add(statusLayout);
        statusGroup.Dock = DockStyle.Fill;
        statusGroup.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
        statusGroup.ForeColor = Color.FromArgb(15, 23, 42);
        statusGroup.Padding = new Padding(14, 18, 14, 10);
        statusGroup.TabStop = false;
        statusGroup.Text = "控制权状态";
        //
        // statusLayout
        //
        statusLayout.ColumnCount = 4;
        statusLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
        statusLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        statusLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
        statusLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        statusLayout.Controls.Add(connectionCaptionLabel, 0, 0);
        statusLayout.Controls.Add(_connectionValue, 1, 0);
        statusLayout.Controls.Add(stationCaptionLabel, 2, 0);
        statusLayout.Controls.Add(_stationValue, 3, 0);
        statusLayout.Controls.Add(ownerCaptionLabel, 0, 1);
        statusLayout.Controls.Add(_ownerValue, 1, 1);
        statusLayout.Controls.Add(stateCaptionLabel, 2, 1);
        statusLayout.Controls.Add(_stateValue, 3, 1);
        statusLayout.Controls.Add(safetyCaptionLabel, 0, 2);
        statusLayout.Controls.Add(_safetyValue, 1, 2);
        statusLayout.Controls.Add(watchdogCaptionLabel, 2, 2);
        statusLayout.Controls.Add(_watchdogValue, 3, 2);
        statusLayout.Dock = DockStyle.Fill;
        statusLayout.RowCount = 3;
        statusLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 33.333F));
        statusLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 33.333F));
        statusLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 33.334F));
        statusLayout.TabIndex = 0;
        ConfigureCaption(connectionCaptionLabel, "ADS 连接");
        ConfigureCaption(stationCaptionLabel, "本站点");
        ConfigureCaption(ownerCaptionLabel, "当前 Owner");
        ConfigureCaption(stateCaptionLabel, "PLC 状态");
        ConfigureCaption(safetyCaptionLabel, "安全状态");
        ConfigureCaption(watchdogCaptionLabel, "看门狗");
        ConfigureValue(_connectionValue);
        ConfigureValue(_stationValue);
        ConfigureValue(_ownerValue);
        ConfigureValue(_stateValue);
        ConfigureValue(_safetyValue);
        ConfigureValue(_watchdogValue);
        connectionCaptionLabel.Text = "ADS 连接";
        stationCaptionLabel.Text = "本站点";
        ownerCaptionLabel.Text = "当前 Owner";
        stateCaptionLabel.Text = "PLC 状态";
        safetyCaptionLabel.Text = "安全状态";
        watchdogCaptionLabel.Text = "看门狗";
        _connectionValue.Text = "未连接";
        _stationValue.Text = "1";
        _ownerValue.Text = "待配置";
        _stateValue.Text = "待配置";
        _safetyValue.Text = "待配置";
        _watchdogValue.Text = "待配置";
        //
        // actionGroup
        //
        actionGroup.Controls.Add(actionLayout);
        actionGroup.Dock = DockStyle.Fill;
        actionGroup.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
        actionGroup.ForeColor = Color.FromArgb(15, 23, 42);
        actionGroup.Padding = new Padding(14, 18, 14, 12);
        actionGroup.TabStop = false;
        actionGroup.Text = "控制权操作";
        //
        // actionLayout
        //
        actionLayout.ColumnCount = 2;
        actionLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 170F));
        actionLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        actionLayout.Controls.Add(_requestButton, 0, 0);
        actionLayout.Controls.Add(_releaseButton, 0, 1);
        actionLayout.Controls.Add(_hintLabel, 1, 0);
        actionLayout.Dock = DockStyle.Fill;
        actionLayout.RowCount = 3;
        actionLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 52F));
        actionLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 52F));
        actionLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        actionLayout.SetRowSpan(_hintLabel, 2);
        actionLayout.TabIndex = 0;
        ConfigureActionButton(_requestButton, "申请控制权");
        ConfigureActionButton(_releaseButton, "释放控制权");
        _requestButton.Text = "申请控制权";
        _releaseButton.Text = "释放控制权";
        _hintLabel.Dock = DockStyle.Fill;
        _hintLabel.Font = new Font("Microsoft YaHei UI", 9F);
        _hintLabel.ForeColor = Color.FromArgb(180, 83, 9);
        _hintLabel.Text = "请先在 App.config 填写控制权变量名。";
        _hintLabel.TextAlign = ContentAlignment.MiddleLeft;
        //
        // ControlAuthority
        //
        AutoScaleDimensions = new SizeF(9F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(241, 245, 249);
        Controls.Add(rootLayout);
        Name = "ControlAuthority";
        Size = new Size(1210, 796);
        actionLayout.ResumeLayout(false);
        actionGroup.ResumeLayout(false);
        statusLayout.ResumeLayout(false);
        statusGroup.ResumeLayout(false);
        rootLayout.ResumeLayout(false);
        ResumeLayout(false);
    }

    // 设置状态区域左侧字段名称样式。
    private static void ConfigureCaption(Label label, string text)
    {
        label.Dock = DockStyle.Fill;
        label.Font = new Font("Microsoft YaHei UI", 9F);
        label.ForeColor = Color.FromArgb(71, 85, 105);
        label.Text = text;
        label.TextAlign = ContentAlignment.MiddleLeft;
    }

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
        button.BackColor = Color.FromArgb(226, 232, 240);
        button.Dock = DockStyle.Fill;
        button.Enabled = false;
        button.FlatAppearance.BorderSize = 0;
        button.FlatStyle = FlatStyle.Flat;
        button.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
        button.ForeColor = Color.FromArgb(100, 116, 139);
        button.Text = text;
    }
}
