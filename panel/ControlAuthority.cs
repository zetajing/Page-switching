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

            ApplyAuthorityStatus();
        }

        // 提示与按钮始终使用同一份当前授权反馈，历史操作结果只写入日志。
        private void ApplyAuthorityStatus()
        {
            var authority = _axisService?.AuthorityStatus;
            var available = _axisService?.IsConnected == true && _axisService.ControlStationCode != 0
                && authority?.IsFresh == true;
            var locked = authority?.Locked != false;
            var granted = authority?.Granted == true && authority.Owner == _axisService?.ControlStationCode;
            _requestButton.Enabled = available && !locked && !_requestPending && !granted;
            _releaseButton.Enabled = available && !locked && !_requestPending && granted;
            if (available)
            {
                _ownerValue.Text = Auto.FormatControlOwner(authority!.Owner);
                // I/O 故障与运动锁分别提示；无主时可申请复位，不能把授权当作允许运动。
                if (authority!.FeedbackInvalid)
                {
                    if (granted) _safetyValue.Text = "NC I/O 无效：本站可复位，禁止运动";
                    else if (authority.Owner == 0)
                        _safetyValue.Text = locked ? "NC I/O 无效且运动占用，暂不可申请"
                            : "NC I/O 无效：可申请后复位，禁止运动";
                    else _safetyValue.Text = "NC I/O 无效：禁止交接，由持有站处理";
                }
                else if (granted)
                    _safetyValue.Text = "PLC 已确认本站持有控制权";
                else if (authority.Owner == 0)
                    _safetyValue.Text = locked ? "PLC 未分配控制权，运动或命令未退出" : "PLC 当前未分配控制权，可申请";
                else if (authority.Owner != _axisService!.ControlStationCode)
                    _safetyValue.Text = $"{Auto.FormatControlOwner(authority.Owner)}持有控制权，本站未授权";
                else
                    _safetyValue.Text = "PLC 尚未授予本站有效控制权";
                if (locked && !authority.FeedbackInvalid) _safetyValue.Text += "；禁止交接";
                _safetyValue.ForeColor = granted && !locked && !authority.FeedbackInvalid ? UiPalette.Success : UiPalette.Warning;
            }
            else if (_axisService?.ControlStationCode == 0)
            {
                _safetyValue.Text = "请配置本站身份并重启；本站身份不等于 PLC 已授权";
                _safetyValue.ForeColor = UiPalette.Warning;
            }
            else if (_axisService is not null)
            {
                // 监控连接与控制连接独立；授权反馈失效时不能沿用旧的成功提示或绿色。
                _safetyValue.Text = !_axisService.IsConnected ? "控制 ADS 未连接"
                    : authority?.Error ?? "控制权反馈已过期，等待 PLC 最新反馈";
                _safetyValue.ForeColor = UiPalette.Warning;
            }
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
                OperationJournal.Record("控制权", release ? "PLC 已确认释放控制权" : "PLC 已确认本站取得控制权");
            }
            catch (OperationCanceledException) when (_requestsCancellation.IsCancellationRequested) { }
            catch (Exception ex)
            {
                OperationJournal.Record("控制权", "操作未成功：" + ex.Message);
            }
            finally
            {
                _requestPending = false;
                if (!IsDisposed && !Disposing)
                {
                    ApplyAuthorityStatus();
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
