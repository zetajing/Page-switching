using System.ComponentModel;
using System.Diagnostics;
using InduLink.Storage;
using Page_switching.panel;

namespace Page_switching
{
    public partial class Mainpage : Form
    {
        private readonly Auto _autoPage;
        private readonly Manual _manualPage;
        private readonly ControlAuthority _controlAuthorityPage;
        private readonly Config _configPage;
        private readonly WaveformPage _waveformPage;
        private readonly AxisService _axisService;
        private readonly MachineMonitorService _monitorService;
        private readonly LoginUser? _currentUser;
        private readonly Wave_Height_Meter _waveHeightMeterPage;
        private readonly Data _dataPage;
        private readonly Calibration _calibrationPage;
        private readonly WaveBatchCalibrationPage _batchCalibrationPage;
        private readonly WaveAnalysisPage _analysisPage;
        private readonly SignalCorrectionPage _correctionPage;
        private readonly AccountManagementPage _accountManagementPage;
        private readonly AxisMonitoringSettingsPage _monitoringSettingsPage;
        private readonly MachineDiagnosticsPage _diagnosticsPage;
        private UserControl? _accountReturnPage;
        private AntdUI.Button? _accountReturnButton;
        private AntdUI.Button? _activeNavigationButton;
        private readonly UserControl[] _pages;
        private readonly AdsTcpRouterRuntime _router;
        private readonly System.Windows.Forms.Timer _headerStatusTimer = new() { Interval = 500 };
        private readonly CancellationTokenSource _lifetimeCancellation = new();
        private volatile bool _closing;
        private volatile bool _monitorReady;
        private bool _closeAfterCleanup;
        private Task _startupTask = Task.CompletedTask;
        private Task _monitorRefreshTask = Task.CompletedTask;
        private Task? _shutdownTask;
        private string? _lastMonitorError;
        private readonly bool _recordOperations = LicenseManager.UsageMode != LicenseUsageMode.Designtime;
        private UserControl? _currentPage;
        private bool? _lastAdsConnected;

        // 初始化共享轴服务和各个页面，并显示默认页面。
        public Mainpage() : this(null) { }

        internal Mainpage(LoginUser? user)
        {
            _currentUser = user;
            InitializeComponent();
            InitializeOperationJournal();
            // 登录只记录软件使用者；PLC 站点身份和控制权仍走原有流程。
            if (user is not null)
            {
                string role = user.IsAdministrator ? "管理员" : "操作员";
                Text = "造波控制系统 - " + user.UserName;
                currentUserLabel.Text = user.UserName + " · " + role;
                if (_recordOperations) OperationJournal.Record("登录", user.UserName + "（" + role + "）登录成功");
            }

            // 本次启动只读取一次 ADS 配置；保存的新参数在下次启动才应用。
            var settings = AdsConnectionSettings.GetSettings();
            var axisOptions = AxisServiceOptions.FromConfiguration(settings);
            // 正常启动只在这里创建轴服务；下方各页面拿到同一个实例和同一组四轴对象。
            _axisService = new AxisService(axisOptions);
            _monitorService = new MachineMonitorService(MachineMonitorOptions.FromConfiguration(axisOptions, settings));
            _autoPage = new Auto(_axisService);
            _router = new AdsTcpRouterRuntime(settings, Router_MessageReceived);
            _manualPage = new Manual(_axisService);
            _controlAuthorityPage = new ControlAuthority(_axisService);
            _configPage = new Config(_currentUser);
            _waveformPage = new WaveformPage();
            _waveHeightMeterPage = new Wave_Height_Meter();
            _dataPage = new Data();
            _calibrationPage = new Calibration();
            _batchCalibrationPage = new WaveBatchCalibrationPage();
            _analysisPage = new WaveAnalysisPage();
            _correctionPage = new SignalCorrectionPage();
            // 功能页全部放入 panelswitch，复用同一份服务和页面生命周期。
            _accountManagementPage = new AccountManagementPage(new LoginService(), _currentUser);
            _monitoringSettingsPage = new AxisMonitoringSettingsPage(_currentUser);
            _diagnosticsPage = new MachineDiagnosticsPage(_axisService);
            // 页面集合统一用于日志注册和退出释放，不再维护两份清单。
            _pages = new UserControl[]
            {
                _autoPage, _manualPage, _configPage, _waveformPage, _waveHeightMeterPage,
                _dataPage, _calibrationPage, _batchCalibrationPage, _analysisPage, _correctionPage, _controlAuthorityPage,
                _accountManagementPage, _monitoringSettingsPage, _diagnosticsPage
            };
            ConnectPageEvents();
            RegisterPageLogging();
            ApplyUserPermissions();
            InitializeStartup();
        }

        // 账号管理、配置与标定仅管理员可用；设备操作仍须另行取得 PLC 控制权。
        private void ApplyUserPermissions()
        {
            bool administrator = _currentUser?.IsAdministrator == true;
            manageAccountsButton.Enabled = administrator;
            bu_Configuration.Enabled = administrator;
            Bu_Calibration.Enabled = administrator;
        }

        private void ManageAccountsButton_Click(object? sender, EventArgs e)
        {
            if (_currentUser?.IsAdministrator != true) return;
            // 记录进入前的页面，返回时保留原来的输入和导航位置。
            if (!ReferenceEquals(_currentPage, _accountManagementPage))
            {
                _accountReturnPage = _currentPage;
                _accountReturnButton = _activeNavigationButton;
            }
            NavigateTo(_accountManagementPage, manageAccountsButton);
        }

        // 页面只提出请求，由主窗体统一安排跳转、刷新和返回。
        private void ConnectPageEvents()
        {
            _controlAuthorityPage.RefreshRequested += ControlAuthorityPage_RefreshRequested;
            _controlAuthorityPage.DiagnosticsRequested += ControlAuthorityPage_DiagnosticsRequested;
            _diagnosticsPage.BackRequested += DiagnosticsPage_BackRequested;
            _configPage.MonitoringSettingsRequested += ConfigPage_MonitoringSettingsRequested;
            _monitoringSettingsPage.BackRequested += MonitoringSettingsPage_BackRequested;
            _accountManagementPage.BackRequested += AccountManagementPage_BackRequested;
            // 批量标定由主窗体管理，使用同一个 panelswitch 显示和返回。
            _calibrationPage.BatchCalibrationRequested += CalibrationPage_BatchCalibrationRequested;
            _batchCalibrationPage.BackRequested += BatchCalibrationPage_BackRequested;
            _batchCalibrationPage.CalibrationSaved += BatchCalibrationPage_CalibrationSaved;
            _dataPage.AnalysisRequested += OpenSessionAnalysis;
        }

        // 事件绑定处可按 F12 进入下面的处理方法，再逐步跟进刷新或页面切换。
        private async void ControlAuthorityPage_RefreshRequested(object? sender, EventArgs e)
        {
            await RefreshMonitorAsync();
        }

        private void ControlAuthorityPage_DiagnosticsRequested(object? sender, EventArgs e)
        {
            NavigateTo(_diagnosticsPage, controlAuthorityButton);
        }

        private void DiagnosticsPage_BackRequested(object? sender, EventArgs e)
        {
            NavigateTo(_controlAuthorityPage, controlAuthorityButton);
        }

        private void ConfigPage_MonitoringSettingsRequested(object? sender, EventArgs e)
        {
            NavigateTo(_monitoringSettingsPage, bu_Configuration);
        }

        private void MonitoringSettingsPage_BackRequested(object? sender, EventArgs e)
        {
            NavigateTo(_configPage, bu_Configuration);
        }

        private void AccountManagementPage_BackRequested(object? sender, EventArgs e)
        {
            UserControl page = _accountReturnPage ?? _autoPage;
            AntdUI.Button button = _accountReturnButton ?? Bu_auto;
            NavigateTo(page, button);
        }

        private void CalibrationPage_BatchCalibrationRequested(object? sender, EventArgs e)
        {
            NavigateTo(_batchCalibrationPage, Bu_Calibration);
        }

        private void BatchCalibrationPage_BackRequested(object? sender, EventArgs e)
        {
            NavigateTo(_calibrationPage, Bu_Calibration);
        }

        private void BatchCalibrationPage_CalibrationSaved(object? sender, EventArgs e)
        {
            _calibrationPage.LoadChannel();
        }

        private void Router_MessageReceived(string message)
        {
            if (!_closing)
            {
                _autoPage.AddLog(message);
            }
        }

        private void OpenSessionAnalysis(WaveCaptureManifest manifest, int? channel)
        {
            _analysisPage.OpenSession(manifest, channel);
            NavigateTo(_analysisPage, analysisButton);
        }

        // 先显示默认页面，窗口真正显示后再启动通信。
        private void InitializeStartup()
        {
            _headerStatusTimer.Tick += HeaderStatusTimer_Tick;
            Disposed += Mainpage_Disposed;

            // 启动时先显示默认页面，避免主区域空白。
            NavigateTo(_autoPage, Bu_auto);
            if (_recordOperations)
            {
                _headerStatusTimer.Start();
                // Designer 只创建控件；实际窗口显示后才启动 Router 和两个独立 ADS 客户端。
                Shown += Mainpage_Shown;
            }
        }

        // 页面统一释放一次；手动页已在 BeginShutdown 中先停止刷新。
        private void Mainpage_Disposed(object? sender, EventArgs e)
        {
            Disposed -= Mainpage_Disposed;
            _ = BeginShutdown();
            OperationJournal.EntryAdded -= AddOperationEntry;
            _headerStatusTimer.Stop();
            _headerStatusTimer.Dispose();
            // 当前页面可能已由窗体释放，隐藏页面才需要在这里补充释放。
            foreach (var page in _pages)
                if (!page.IsDisposed) page.Dispose();
        }

        // 主窗体显示后启动可选 Router，并连接真实 ADS PLC。
        private async void Mainpage_Shown(object? sender, EventArgs e)
        {
            var cancellationToken = _lifetimeCancellation.Token;
            // 启动任务不依赖 UI 消息循环，直接 Dispose 时也能等待清理完成。
            _startupTask = Task.Run(() => StartServicesAsync(cancellationToken), cancellationToken);
            try { await _startupTask; }
            catch (OperationCanceledException) when (_closing) { }
            if (!_closing && !IsDisposed) UpdateHeaderStatus();
        }

        private async Task StartServicesAsync(CancellationToken cancellationToken)
        {
            var stage = "ADS Router 启动";
            try
            {
                await _router.StartAsync(cancellationToken).ConfigureAwait(false);
                cancellationToken.ThrowIfCancellationRequested();
                // Router 就绪即允许监控连接，不等待手动客户端连接成功。
                _monitorReady = true;
                PostToUi(() => { _ = RefreshMonitorAsync(); });
                stage = "手动 ADS 连接";
                await _axisService.ConnectAsync(cancellationToken).ConfigureAwait(false);
                if (!_closing) _autoPage.AddLog("手动 ADS 连接成功");
            }
            catch (OperationCanceledException) when (_closing)
            {
                // 关闭窗口时取消连接属于正常退出。
            }
            catch (Exception ex)
            {
                if (!_closing)
                {
                    _autoPage.AddLog(stage + "失败：" + AdsDiagnostics.DescribeException(ex));
                    AdsDiagnostics.RecordException(stage + "失败", ex);
                }
            }
        }

        private async void HeaderStatusTimer_Tick(object? sender, EventArgs e)
        {
            if (_closing || IsDisposed) return;
            // 即使读取仍在执行，也按当前时间将历史反馈降为过期。
            PublishMonitorSnapshot();
            await RefreshMonitorAsync();
        }

        // 一次取得反馈，同一份快照同时更新页面和顶部状态，避免各处重复读取。
        private void PublishMonitorSnapshot()
        {
            var snapshot = _monitorService.LatestSnapshot;
            _autoPage.ApplyMonitorSnapshot(snapshot);
            _controlAuthorityPage.ApplyMonitorSnapshot(snapshot);
            UpdateHeaderStatus(snapshot);
        }

        // 定时器直接等待 ADS 读取，再在当前 UI 线程更新页面，便于逐行调试。
        private async Task RefreshMonitorAsync()
        {
            if (!_monitorReady || _closing || IsDisposed || !_monitorRefreshTask.IsCompleted) return;
            var cancellationToken = _lifetimeCancellation.Token;
            try
            {
                var includeAxes = ReferenceEquals(_currentPage, _autoPage);
                // 保存实际读取任务；关闭窗体时只等待它，不依赖 UI 回调继续运行。
                _monitorRefreshTask = _monitorService.RefreshAsync(includeAxes, cancellationToken);
                await _monitorRefreshTask;
                if (_closing || IsDisposed) return;
                _lastMonitorError = null;
                PublishMonitorSnapshot();
            }
            catch (OperationCanceledException) when (_closing || cancellationToken.IsCancellationRequested) { }
            catch (Exception ex)
            {
                Debug.WriteLine("监控刷新失败：" + ex);
                if (_lastMonitorError == ex.Message) return;
                _lastMonitorError = ex.Message;
                _autoPage.AddLog("监控刷新失败：" + ex.Message);
            }
        }

        // 后台任务不等待 UI 回调，避免关闭消息循环时清理任务无法退出。
        private void PostToUi(Action action)
        {
            if (_closing || IsDisposed || Disposing || !IsHandleCreated) return;
            try
            {
                BeginInvoke((Action)(() =>
                {
                    if (!_closing && !IsDisposed && !Disposing) action();
                }));
            }
            catch (InvalidOperationException) { }
        }

        // 先异步退出监控与启动任务，再允许关闭，Router 始终最后释放。
        protected override async void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (e.Cancel || _closeAfterCleanup || !_recordOperations) return;
            e.Cancel = true;
            if (_shutdownTask is not null) return;
            await BeginShutdown();
            _closeAfterCleanup = true;
            if (!IsDisposed) Close();
        }

        private Task BeginShutdown()
        {
            if (_shutdownTask is not null) return _shutdownTask;
            _closing = true;
            _monitorReady = false;
            _lifetimeCancellation.Cancel();
            _headerStatusTimer.Stop();
            // 等待后台任务期间先停止手动页刷新和按钮请求，轴客户端仍留到最后释放。
            if (!IsDisposed && !Disposing) Enabled = false;
            _controlAuthorityPage?.CancelRequests();
            _diagnosticsPage?.CancelRequests();
            _manualPage?.Dispose();
            _shutdownTask = CleanupServicesAsync();
            return _shutdownTask;
        }

        private async Task CleanupServicesAsync()
        {
            async Task DisposeMonitorAsync()
            {
                if (_monitorService is not null) await _monitorService.DisposeAsync().ConfigureAwait(false);
            }
            try
            {
                try { await _axisService.ShutdownWaveAsync().ConfigureAwait(false); }
                // 停车异常也要结束独立采样；统计清理有界，不改变停车结果。
                finally { await _axisService.ShutdownRuntimeMonitoringAsync().ConfigureAwait(false); }
                // WhenAll 等待所有任务退出；某个任务取消也不能提前释放 Router。
                await Task.WhenAll(DisposeMonitorAsync(), _monitorRefreshTask, _startupTask).ConfigureAwait(false);
            }
            catch (OperationCanceledException) { }
            catch (Exception ex) { Debug.WriteLine("监控退出失败：" + ex); }
            try { _axisService?.Dispose(); }
            catch (Exception ex) { Debug.WriteLine("手动 ADS 退出失败：" + ex); }
            try { await _router.DisposeAsync().ConfigureAwait(false); }
            catch (Exception ex) { Debug.WriteLine("Router 退出失败：" + ex); }
            _lifetimeCancellation.Dispose();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _ = BeginShutdown();
            if (_recordOperations) OperationJournal.Record("系统", "程序退出");
            try
            {
                base.OnFormClosed(e);
            }
            finally
            {
                LogDisplayHelper.Shutdown();
            }
        }

        // 隐藏当前页面并显示指定的缓存页面。
        private void ShowPage(UserControl page)
        {
            ArgumentNullException.ThrowIfNull(page);

            if (ReferenceEquals(_currentPage, page))
            {
                return;
            }

            panelswitch.SuspendLayout();

            try
            {
                if (_currentPage is not null)
                {
                    // 先隐藏再移出容器，让各页的显示事件正确停止刷新。
                    _currentPage.Visible = false;
                    panelswitch.Controls.Remove(_currentPage);
                }

                page.Dock = DockStyle.Fill;
                panelswitch.Controls.Add(page);
                // 复用已有页面并恢复显示，保留输入和标签选择，触发需要的刷新。
                page.Visible = true;
                page.BringToFront();
                _currentPage = page;
            }
            finally
            {
                panelswitch.ResumeLayout(true);
            }
        }

        private void NavigateTo(UserControl page, AntdUI.Button button)
        {
            // 集中复查入口，事件或内部跳转同样不能绕过管理员页面限制。
            if (!CanOpenPage(page))
            {
                if (_recordOperations) OperationJournal.Record("权限", "当前账号无权打开" + GetPageName(page));
                return;
            }
            if (_recordOperations) OperationJournal.Record("页面切换", "打开" + GetPageName(page));
            ShowPage(page);
            // 切页时共用同一份反馈，保持顶部与控制权页显示一致。
            var snapshot = _monitorService.LatestSnapshot;
            SetActiveNavigation(button, snapshot);
            _activeNavigationButton = button;
            if (ReferenceEquals(page, _autoPage) || ReferenceEquals(page, _controlAuthorityPage))
            {
                _controlAuthorityPage.ApplyMonitorSnapshot(snapshot);
                _ = RefreshMonitorAsync();
            }
        }

        private bool CanOpenPage(UserControl page)
        {
            switch (page)
            {
                case Config:
                case AccountManagementPage:
                case AxisMonitoringSettingsPage:
                case Calibration:
                case WaveBatchCalibrationPage:
                    return _currentUser?.IsAdministrator == true;
                default:
                    return true;
            }
        }

        // AntdUI 导航统一更新默认、悬停和按下颜色；页面切换仍使用同一个入口。
        private void SetActiveNavigation(AntdUI.Button activeButton, MachineMonitorSnapshot snapshot)
        {
            foreach (var button in new[] { Bu_auto, Bu_manual, Bu_Calibration, Bu_data, button2, button5,
                         analysisButton, correctionButton, bu_Configuration, controlAuthorityButton, manageAccountsButton })
            {
                var isActive = ReferenceEquals(button, activeButton);
                button.DefaultBack = isActive ? UiPalette.Primary : UiPalette.Surface;
                button.ForeColor = isActive ? Color.White : UiPalette.Text;
                button.DefaultBorderColor = isActive ? UiPalette.Primary : UiPalette.Border;
                button.BackHover = isActive ? UiPalette.PrimaryHover : UiPalette.Selection;
                button.BackActive = isActive ? UiPalette.PrimaryHover : UiPalette.SecondaryButton;
            }

            // 账号入口在顶部，左侧标记仅跟随左侧导航按钮。
            navigationMarker.Visible = !ReferenceEquals(activeButton, manageAccountsButton);
            if (navigationMarker.Visible)
            {
                navigationMarker.Top = activeButton.Top;
                navigationMarker.Height = activeButton.Height;
            }

            UpdateHeaderStatus(snapshot);
        }

        // 页面名称和设备反馈分开显示，切页不改变实际控制端或运行模式。
        private void UpdateHeaderStatus(MachineMonitorSnapshot? snapshot = null)
        {
            if (_closing || IsDisposed || Disposing)
            {
                return;
            }

            var connected = _axisService.IsConnected;
            if (_recordOperations && _lastAdsConnected != connected)
            {
                OperationJournal.Record("系统", connected ? "手动 ADS 已连接" : "手动 ADS 未连接");
                _lastAdsConnected = connected;
            }
            snapshot ??= _monitorService.LatestSnapshot;
            adsStatusLabel.Text = $"● ADS：手动{(connected ? "已连接" : "未连接")} / 监控{(snapshot.IsConnected ? "已连接" : "未连接")}";
            adsStatusLabel.ForeColor = connected && snapshot.IsLive ? UiPalette.Success : UiPalette.Warning;
            var owner = Auto.FormatMonitorField(snapshot.ControlOwner, Auto.FormatControlOwner);
            var mode = Auto.FormatMonitorField(snapshot.ControlMode, Auto.FormatControlMode);
            controlStatusLabel.Text = $"页面：{GetCurrentPageName()}    控制端：{owner}    模式：{mode}";
            // 分开查看两个字段是否有效，断点处能直接观察每个判断结果。
            bool ownerValid = snapshot.ControlOwner.IsAvailable && snapshot.ControlOwner.Value.HasValue
                && snapshot.ControlOwner.Value.Value <= 3;
            bool modeValid = snapshot.ControlMode.IsAvailable && snapshot.ControlMode.Value.HasValue
                && snapshot.ControlMode.Value.Value <= 2;
            if (ownerValid && modeValid)
            {
                controlStatusLabel.ForeColor = UiPalette.Muted;
            }
            else
            {
                controlStatusLabel.ForeColor = UiPalette.Warning;
            }
        }

        // 根据当前缓存页面返回顶部状态栏要显示的页面名称。
        private string GetCurrentPageName()
        {
            if (_currentPage == null)
            {
                return "系统";
            }
            return GetPageName(_currentPage);
        }

        // 页面类型集中对应显示名称，使用 switch 语句逐项返回。
        private static string GetPageName(UserControl page)
        {
            switch (page)
            {
                case Auto:
                    return "自动运行";
                case Manual:
                    return "手动控制";
                case ControlAuthority:
                    return "控制权申请";
                case Calibration:
                    return "标定管理";
                case WaveBatchCalibrationPage:
                    return "批量标定";
                case Data:
                    return "数据管理";
                case WaveformPage:
                    return "波形生成";
                case Config:
                    return "系统配置";
                case AccountManagementPage:
                    return "账号管理";
                case AxisMonitoringSettingsPage:
                    return "轴运行监测配置";
                case MachineDiagnosticsPage:
                    return "运行诊断";
                case Wave_Height_Meter:
                    return "浪高监测";
                case WaveAnalysisPage:
                    return "波浪分析";
                case SignalCorrectionPage:
                    return "信号修正";
                default:
                    return "系统";
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            var area = Screen.FromControl(this).WorkingArea;
            Size = new Size(Math.Min(Width, area.Width), Math.Min(Height, area.Height));
            CenterToScreen();
        }

        // 切换到自动页面。
        private void Bu_auto_Click(object sender, EventArgs e) => NavigateTo(_autoPage, Bu_auto);

        // 切换到手动控制页面。
        private void Bu_manual_Click(object sender, EventArgs e) => NavigateTo(_manualPage, Bu_manual);

        private void ControlAuthorityButton_Click(object sender, EventArgs e) =>
            NavigateTo(_controlAuthorityPage, controlAuthorityButton);

        // 切换到配置页面。
        private void bu_Configuration_Click(object sender, EventArgs e) => NavigateTo(_configPage, bu_Configuration);

        // 切换到波形生成页面。
        private void WaveformButton_Click(object sender, EventArgs e) => NavigateTo(_waveformPage, button2);

        private void button5_Click(object sender, EventArgs e) => NavigateTo(_waveHeightMeterPage, button5);

        private void Bu_data_Click(object sender, EventArgs e) => NavigateTo(_dataPage, Bu_data);

        private void Bu_Calibration_Click(object sender, EventArgs e) => NavigateTo(_calibrationPage, Bu_Calibration);

        private void AnalysisButton_Click(object sender, EventArgs e) => NavigateTo(_analysisPage, analysisButton);
        private void CorrectionButton_Click(object sender, EventArgs e) => NavigateTo(_correctionPage, correctionButton);
    }
}
