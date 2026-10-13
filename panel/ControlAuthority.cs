using System.ComponentModel;

namespace Page_switching;

// 三站控制权申请页；复用主窗体的只读快照，申请接口确认后再接入 PLC。
public sealed partial class ControlAuthority : UserControl
{
    public ControlAuthority()
    {
        InitializeComponent();
        ApplyMonitorSnapshot(new MachineMonitorSnapshot());
    }

    internal void ApplyMonitorSnapshot(MachineMonitorSnapshot snapshot)
    {
        if (IsDisposed) return;

        _connectionValue.Text = snapshot.IsConnected ? "已连接" : "未连接";
        _connectionValue.ForeColor = snapshot.IsConnected ? UiPalette.Success : UiPalette.Warning;
        _stationValue.Text = "待确认本站所属站点";
        // 沿用已定义的 Owner 编码，不擅自将 1/2/3 解释为三站优先级。
        _ownerValue.Text = Auto.FormatMonitorField(snapshot.ControlOwner, Auto.FormatControlOwner);
        _stateValue.Text = Auto.FormatMonitorField(snapshot.ControlMode, Auto.FormatControlMode);
        _safetyValue.Text = snapshot.QualityText;
        _safetyValue.ForeColor = snapshot.IsLive ? UiPalette.Success : UiPalette.Warning;
        _watchdogValue.Text = snapshot.IsLive
            ? snapshot.Heartbeat.ToDisplayText()
            : "--（" + snapshot.QualityText + "）";

        // 连接成功和 Owner 反馈不能替代三站授权反馈，也不能据此启用申请、释放。
        _requestButton.Enabled = false;
        _releaseButton.Enabled = false;
    }

    private void RefreshButton_Click(object? sender, EventArgs e)
    {
        _hintLabel.Text = "已请求刷新控制权反馈；申请、释放接口待接入，请先确认本站身份、三站编码及授权变量。";
        if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            OperationJournal.Record("控制权申请", _hintLabel.Text);
        RefreshRequested?.Invoke(this, EventArgs.Empty);
    }

    internal event EventHandler? RefreshRequested;
}
