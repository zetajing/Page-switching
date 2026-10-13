using System.ComponentModel;

namespace Page_switching
{
    // 保持可继承的 partial UserControl，供 WinForms Designer 创建设计实例。
    public partial class ControlAuthority : UserControl
    {
        // 与自动页相同：设计器设置 Site 后也应跳过运行时布局调整。
        private bool IsDesignSurface => LicenseManager.UsageMode == LicenseUsageMode.Designtime
            || DesignMode || Site?.DesignMode == true;

        public ControlAuthority()
        {
            InitializeComponent();
            // 设计模式只创建静态控件，不调用运行时快照格式化逻辑。
            if (IsDesignSurface) return;
            ApplyMonitorSnapshot(new MachineMonitorSnapshot());
        }

        protected override void OnLayout(LayoutEventArgs e)
        {
            if (IsDesignSurface)
            {
                base.OnLayout(e);
                return;
            }
            // 同步已随 DPI 缩放的最小画布，小窗口可滚动到全部规则和按钮。
            if (pagePanel != null && AutoScrollMinSize != pagePanel.MinimumSize)
                AutoScrollMinSize = pagePanel.MinimumSize;
            base.OnLayout(e);
        }

        internal void ApplyMonitorSnapshot(MachineMonitorSnapshot snapshot)
        {
            if (IsDisposed) return;

            _connectionValue.Text = snapshot.IsConnected ? "已连接" : "未连接";
            _connectionValue.ForeColor = snapshot.IsConnected ? UiPalette.Success : UiPalette.Warning;
            _stationValue.Text = ControlStationIdentity.Format(ControlStationIdentity.Current);
            // 沿用已定义的 Owner 编码，不擅自将 1/2/3 解释为三站优先级。
            _ownerValue.Text = Auto.FormatMonitorField(snapshot.ControlOwner, Auto.FormatControlOwner);
            _stateValue.Text = Auto.FormatMonitorField(snapshot.ControlMode, Auto.FormatControlMode);
            _safetyValue.Text = snapshot.QualityText;
            _safetyValue.ForeColor = snapshot.IsLive ? UiPalette.Success : UiPalette.Warning;
            _watchdogValue.Text = snapshot.IsLive
                ? snapshot.Heartbeat.ToDisplayText()
                : "--（" + snapshot.QualityText + "）";

            // 连接及 Owner 反馈不能替代三站授权，申请、释放接口确认后再启用。
            _requestButton.Enabled = false;
            _releaseButton.Enabled = false;
        }

        private void RefreshButton_Click(object? sender, EventArgs e)
        {
            _hintLabel.Text = "已请求刷新控制权反馈；PLC 申请、释放接口待定义，暂不可操作。";
            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
                OperationJournal.Record("控制权申请", _hintLabel.Text);
            RefreshRequested?.Invoke(this, EventArgs.Empty);
        }

        internal event EventHandler? RefreshRequested;
    }
}
