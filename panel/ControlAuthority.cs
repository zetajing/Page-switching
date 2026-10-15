using System.ComponentModel;

namespace Page_switching
{
    // 保持可继承的 partial UserControl，供 WinForms Designer 创建设计实例。
    public partial class ControlAuthority : UserControl
    {
        // 与自动页相同：设计器设置 Site 后也应跳过运行时布局调整。
        private bool IsDesignSurface => LicenseManager.UsageMode == LicenseUsageMode.Designtime
            || DesignMode || Site?.DesignMode == true;

        private readonly AxisService? _axisService;
        private readonly CancellationTokenSource _requestsCancellation = new();
        private bool _requestPending;
        private string? _lastOperation;

        public ControlAuthority() : this(null) { }

        public ControlAuthority(AxisService? axisService)
        {
            _axisService = axisService;
            InitializeComponent();
            // 设计模式只创建静态控件，不调用运行时快照格式化逻辑。
            if (IsDesignSurface) return;
            ApplyMonitorSnapshot(new MachineMonitorSnapshot());
            Disposed += (_, _) => { _requestsCancellation.Cancel(); _requestsCancellation.Dispose(); };
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
            _stationValue.Text = _axisService?.ControlStationCode switch
            {
                1 => "造波控制室", 2 => "拖车本地", 3 => "远程操作室",
                _ => "未配置本站身份（保存后重启生效）"
            };
            // 新配套 PLC 明确定义 1 中心、2 本地、3 远程，反馈是实际仲裁结果。
            _ownerValue.Text = Auto.FormatMonitorField(snapshot.ControlOwner, Auto.FormatControlOwner);
            _stateValue.Text = Auto.FormatMonitorField(snapshot.ControlMode, Auto.FormatControlMode);
            _safetyValue.Text = snapshot.QualityText;
            _safetyValue.ForeColor = snapshot.IsLive ? UiPalette.Success : UiPalette.Warning;
            _watchdogValue.Text = snapshot.IsLive
                ? snapshot.Heartbeat.ToDisplayText()
                : "--（" + snapshot.QualityText + "）";

            var authority = _axisService?.AuthorityStatus;
            var available = _axisService?.IsConnected == true && _axisService.ControlStationCode != 0
                && authority?.IsFresh == true;
            var locked = authority?.Locked != false;
            _requestButton.Enabled = available && !locked && !_requestPending && authority?.Granted != true;
            _releaseButton.Enabled = available && !locked && !_requestPending && authority?.Granted == true;
            if (available)
            {
                _ownerValue.Text = Auto.FormatControlOwner(authority!.Owner);
                _safetyValue.Text = locked ? "运行中锁定控制权，禁止交接"
                    : _lastOperation ?? "设备已退出运动，可提交申请或释放";
                _safetyValue.ForeColor = locked ? UiPalette.Warning : UiPalette.Success;
            }
            else if (_axisService?.ControlStationCode == 0)
                _safetyValue.Text = "请配置本站身份并重启；本站身份不等于 PLC 已授权";
            else if (authority?.Error is string error)
                _safetyValue.Text = error;
        }

        // 请求发送成功后继续等同序号 PLC 应答，不在本地直接修改控制端。
        private async void RequestButton_Click(object? sender, EventArgs e) => await ChangeAuthorityAsync(false);
        private async void ReleaseButton_Click(object? sender, EventArgs e) => await ChangeAuthorityAsync(true);

        private async Task ChangeAuthorityAsync(bool release)
        {
            if (_axisService is null || _requestPending) return;
            _requestPending = true;
            _requestButton.Enabled = false;
            _releaseButton.Enabled = false;
            try
            {
                await _axisService.ChangeControlAuthorityAsync(release, _requestsCancellation.Token);
                _lastOperation = release ? "PLC 已确认释放控制权" : "PLC 已确认本站取得控制权";
                OperationJournal.Record("控制权", _lastOperation);
            }
            catch (OperationCanceledException) when (_requestsCancellation.IsCancellationRequested) { }
            catch (Exception ex)
            {
                _lastOperation = ex.Message;
                OperationJournal.Record("控制权", "操作未成功：" + ex.Message);
            }
            finally
            {
                _requestPending = false;
                if (!IsDisposed && !Disposing)
                {
                    _safetyValue.Text = _lastOperation ?? "操作已取消";
                    RefreshRequested?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        internal void CancelRequests()
        {
            // 主窗体和控件 Dispose 都可能进入清理，已取消后不再访问已释放的源。
            if (!_requestsCancellation.IsCancellationRequested) _requestsCancellation.Cancel();
        }

        private void RefreshButton_Click(object? sender, EventArgs e)
        {
          
            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)

            RefreshRequested?.Invoke(this, EventArgs.Empty);
        }

        internal event EventHandler? RefreshRequested;
    }
}
