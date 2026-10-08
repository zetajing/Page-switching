using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using InduLink.Storage;
using Page_switching.panel;
using InduLink.Protocols.Ads.Router;

namespace Page_switching
{
    public partial class Mainpage : Form
    {
        private readonly Auto _autoPage;
        private readonly Manual _manualPage;
        private readonly Config _config;
        private readonly WaveformPage _waveformPage;
        private readonly AxisService _axisService;
        private readonly MachineMonitorService _monitorService;
        private readonly Wave_Height_Meter _wave_Height_Meter;
        private readonly Data _data;
        private readonly Calibration _calibration;
        private readonly WaveBatchCalibrationPage _batchCalibrationPage;
        private readonly WaveAnalysisPage _analysis;
        private readonly SignalCorrectionPage _correction;
        private readonly System.Windows.Forms.Timer _headerStatusTimer = new() { Interval = 500 };
        private readonly CancellationTokenSource _lifetimeCancellation = new();
        private volatile bool _closing;
        private volatile bool _monitorReady;
        private bool _closeAfterCleanup;
        private Task _startupTask = Task.CompletedTask;
        private Task _adsTcpRouterLifetimeTask = Task.CompletedTask;
        private Task _monitorRefreshTask = Task.CompletedTask;
        private Task? _shutdownTask;
        private string? _lastMonitorError;
        private readonly bool _recordOperations = LicenseManager.UsageMode != LicenseUsageMode.Designtime;
        private AdsTcpRouterHost? _adsTcpRouter;
        private UserControl? _currentPage;
        private bool? _lastAdsConnected;
        private static readonly HashSet<string> ResultLabels =
        [
            "helperLabel", "saveResultLabel", "regularStatusLabel", "irregularStatusLabel",
            "result", "status", "_hintLabel", "_connectionStatusLabel"
        ];

        // 初始化共享轴服务和各个页面，并显示默认页面。
        public Mainpage()
        {
            InitializeComponent();
            if (_recordOperations)
            {
                operationPathLabel.Text = "日志保存位置：" + OperationJournal.FilePath;
                OperationJournal.EntryAdded += AddOperationEntry;
                LoadOperationHistory();
                OperationJournal.Record("系统", "程序启动");
            }

            var axisOptions = AxisServiceOptions.FromConfiguration();
            _axisService = new AxisService(axisOptions);
            _monitorService = new MachineMonitorService(MachineMonitorOptions.FromConfiguration(axisOptions));
            _autoPage = new Auto();
            _manualPage = new Manual(_axisService);
            _config = new Config();
            _waveformPage = new WaveformPage();
            _wave_Height_Meter = new Wave_Height_Meter();
            _data = new Data();
            _calibration = new Calibration();
            _batchCalibrationPage = new WaveBatchCalibrationPage();
            _analysis = new WaveAnalysisPage();
            _correction = new SignalCorrectionPage();
            // 批量标定由主窗体管理，使用同一个 panelswitch 显示和返回。
            _calibration.BatchCalibrationRequested += (_, _) => NavigateTo(_batchCalibrationPage, Bu_Calibration);
            _batchCalibrationPage.BackRequested += (_, _) => NavigateTo(_calibration, Bu_Calibration);
            _batchCalibrationPage.CalibrationSaved += (_, _) => _calibration.LoadChannel();
            _data.AnalysisRequested += (manifest, channel) =>
            {
                _analysis.OpenSession(manifest, channel);
                NavigateTo(_analysis, analysisButton);
            };
            if (_recordOperations)
                foreach (var page in new UserControl[]
                {
                    _autoPage, _manualPage, _config,
                    _waveformPage, _wave_Height_Meter, _data, _calibration, _batchCalibrationPage,
                    _analysis, _correction
                })
                    TrackActions(page, page);
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
            foreach (var page in new UserControl[]
            {
                _autoPage, _manualPage, _config, _waveformPage, _wave_Height_Meter,
                _analysis, _correction, _data, _calibration, _batchCalibrationPage
            })
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
            try
            {
                var router = await StartRouterInBackgroundAsync(cancellationToken);
                // 窗口关闭后返回的 Router 不能再交给已经释放的窗体。
                if (_closing) { router?.Dispose(); return; }
                _adsTcpRouter = router;
                // Router 就绪即允许监控连接，不等待手动客户端连接成功。
                _monitorReady = true;
                PostToUi(() => { _ = RefreshMonitorAsync(); });
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
                    _autoPage.AddLog("手动 ADS 连接失败：" + AdsDiagnostics.DescribeException(ex));
                    AdsDiagnostics.RecordException("手动 ADS 连接失败", ex);
                }
            }
        }

        private async void HeaderStatusTimer_Tick(object? sender, EventArgs e)
        {
            if (_closing || IsDisposed) return;
            // 即使读取仍在执行，也按当前时间将历史反馈降为过期。
            _autoPage.ApplyMonitorSnapshot(_monitorService.LatestSnapshot);
            UpdateHeaderStatus();
            await RefreshMonitorAsync();
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
                _autoPage.ApplyMonitorSnapshot(_monitorService.LatestSnapshot);
                UpdateHeaderStatus();
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

        // 在后台启动可选的 ADS TCP Router，避免启动阶段阻塞主界面。
        private async Task<AdsTcpRouterHost?> StartRouterInBackgroundAsync(CancellationToken cancellationToken)
        {
            var configurationFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None).FilePath;
            // 记录本次进程实际读取的文件和目标，便于区分保存的设置与重启后的设置。
            var settings = AdsConnectionSettings.GetSettings();
            _autoPage.AddLog("ADS 配置文件：" + configurationFile);
            if (File.Exists(AdsConnectionSettings.FilePath))
                _autoPage.AddLog("ADS 用户配置：" + AdsConnectionSettings.FilePath);
            if (AdsConnectionSettings.LoadError is { } loadError)
                _autoPage.AddLog("ADS 用户配置读取失败，采用程序默认配置：" + loadError);
            _autoPage.AddLog($"ADS 连接目标：AMS Net ID={settings["AdsAmsNetId"]}，ADS 端口={settings["AdsPort"]}，连接超时={settings["AdsConnectTimeoutMs"]} ms");
            if (!AdsTcpRouterRuntime.IsEnabled)
            {
                Debug.WriteLine($"独立 ADS TCP Router 未启用。配置文件：{configurationFile}");
                _autoPage.AddLog($"独立 ADS TCP Router 未启用，使用系统 TwinCAT Router。配置文件：{configurationFile}");
                return null;
            }

            Debug.WriteLine("ADS TCP Router 配置文件：" + configurationFile);
            _autoPage.AddLog($"ADS Router 本机：AMS Net ID={settings["AdsTcpRouterLocalNetId"]}，TCP 端口={settings["AdsTcpRouterTcpPort"]}，回环={settings["AdsTcpRouterLoopbackIp"]}:{settings["AdsTcpRouterLoopbackPort"]}");
            _autoPage.AddLog($"ADS Router PLC 路由：IP={settings["AdsTcpRouterRemoteAddress"]}，AMS Net ID={settings["AdsTcpRouterRemoteNetId"]}");
            AdsTcpRouterHost? host = null;
            Task? routerTask = null;
            try
            {
                var routerHost = AdsTcpRouterRuntime.Create();
                host = routerHost;
                routerHost.StatusChanged += (_, _) =>
                {
                    var status = routerHost.Status;
                    Debug.WriteLine("ADS TCP Router 状态：" + status);
                    PostToUi(() => _autoPage.AddLog("ADS TCP Router 状态：" + status));
                };

                // StartAsync 返回 Router 的整个运行期任务；单独保存它并等待 IsRunning，不能等待它结束才继续启动 ADS 客户端。
                routerTask = routerHost.StartAsync(cancellationToken);
                _adsTcpRouterLifetimeTask = routerTask;
                _ = routerTask.ContinueWith(
                    task =>
                    {
                        var error = task.Exception?.GetBaseException();
                        if (error is null) return;
                        Debug.WriteLine("ADS TCP Router 运行异常：" + error);
                        PostToUi(() => _autoPage.AddLog("ADS TCP Router 运行异常：" + error.Message));
                    },
                    CancellationToken.None,
                    TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously,
                    TaskScheduler.Default);

                while (!routerHost.IsRunning)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    if (routerTask.IsCompleted)
                    {
                        await routerTask.ConfigureAwait(false);
                        throw new InvalidOperationException("Router 在进入运行状态前已停止。");
                    }

                    await Task.Delay(50, cancellationToken).ConfigureAwait(false);
                }

                Debug.WriteLine($"ADS TCP Router 已运行：IsRunning={routerHost.IsRunning}, Status={routerHost.Status}");
                if (!_closing) _autoPage.AddLog("独立 ADS TCP Router 已启动");
                return host;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ADS TCP Router 启动失败：" + ex);
                var rootException = ex.GetBaseException();
                if (!_closing) _autoPage.AddLog($"ADS TCP Router 启动失败（{rootException.GetType().Name}）：{rootException.Message}");
                if (routerTask is not null)
                {
                    try { await routerTask.ConfigureAwait(false); }
                    catch (OperationCanceledException) { }
                    catch (Exception routerException) { Debug.WriteLine("ADS TCP Router 退出：" + routerException); }
                }
                host?.Dispose();
                return null;
            }
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
                // WhenAll 等待所有任务退出；某个任务取消也不能提前释放 Router。
                await Task.WhenAll(DisposeMonitorAsync(), _monitorRefreshTask, _startupTask).ConfigureAwait(false);
            }
            catch (OperationCanceledException) { }
            catch (Exception ex) { Debug.WriteLine("监控退出失败：" + ex); }
            try { await _adsTcpRouterLifetimeTask.ConfigureAwait(false); }
            catch (OperationCanceledException) { }
            catch (Exception ex) { Debug.WriteLine("Router 运行任务退出失败：" + ex); }
            finally
            {
                try { _axisService?.Dispose(); }
                catch (Exception ex) { Debug.WriteLine("手动 ADS 退出失败：" + ex); }
                try { _adsTcpRouter?.Dispose(); }
                catch (Exception ex) { Debug.WriteLine("Router 退出失败：" + ex); }
                _lifetimeCancellation.Dispose();
            }
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

        private void NavigateTo(UserControl page, Button button)
        {
            if (_recordOperations) OperationJournal.Record("页面切换", "打开" + GetPageName(page));
            ShowPage(page);
            SetActiveNavigation(button);
            if (ReferenceEquals(page, _autoPage)) _ = RefreshMonitorAsync();
        }

        private void AddOperationEntry(string entry)
        {
            if (_closing || IsDisposed || Disposing) return;
            if (InvokeRequired)
            {
                PostToUi(() => AddOperationEntry(entry));
                return;
            }

            AppendOperationRow(entry);
            if (operationList.Rows.Count > 500)
                operationList.Rows.RemoveAt(0);
            ScrollOperationsToLatest();
            operationPathLabel.Text = "日志保存位置：" + OperationJournal.FilePath;
        }

        private void AppendOperationRow(string entry)
        {
            var pageStart = entry.IndexOf("  [", StringComparison.Ordinal);
            var pageEnd = pageStart >= 0 ? entry.IndexOf(']', pageStart + 3) : -1;
            var index = pageEnd >= 0
                ? operationList.Rows.Add(entry[..pageStart], entry[(pageStart + 3)..pageEnd], entry[(pageEnd + 1)..].TrimStart())
                : operationList.Rows.Add("", "系统", entry);
            var row = operationList.Rows[index];
            row.Tag = entry;
            if (entry.Contains("失败", StringComparison.Ordinal) || entry.Contains("中断", StringComparison.Ordinal))
                row.Cells[2].Style.ForeColor = UiPalette.Danger;
        }

        private void ScrollOperationsToLatest()
        {
            if (operationAutoScroll.Checked && operationList.IsHandleCreated && operationList.Rows.Count > 0)
                operationList.FirstDisplayedScrollingRowIndex = operationList.Rows.Count - 1;
        }

        private void OperationAutoScroll_CheckedChanged(object? sender, EventArgs e)
        {
            ScrollOperationsToLatest();
            if (_recordOperations)
                OperationJournal.Record("操作记录", operationAutoScroll.Checked ? "开启自动滚动" : "暂停自动滚动");
        }

        private void CopyOperationButton_Click(object? sender, EventArgs e)
        {
            try
            {
                var entries = operationList.SelectedRows.Cast<DataGridViewRow>()
                    .OrderBy(row => row.Index).Select(row => Convert.ToString(row.Tag));
                var text = string.Join(Environment.NewLine, entries);
                if (text.Length == 0) return;
                Clipboard.SetText(text);
                OperationJournal.Record("操作记录", "已复制选中的记录");
            }
            catch (Exception ex)
            {
                OperationJournal.Record("操作记录", "复制失败：" + ex.Message);
            }
        }

        private void OpenOperationFolderButton_Click(object? sender, EventArgs e)
        {
            try
            {
                var directory = Path.GetDirectoryName(OperationJournal.FilePath)!;
                Directory.CreateDirectory(directory);
                Process.Start(new ProcessStartInfo(directory) { UseShellExecute = true });
                OperationJournal.Record("操作记录", "已打开日志目录");
            }
            catch (Exception ex)
            {
                OperationJournal.Record("操作记录", "打开目录失败：" + ex.Message);
            }
        }

        private void LoadOperationHistory()
        {
            try
            {
                foreach (var entry in OperationJournal.ReadRecentEntries())
                    AppendOperationRow(entry);
            }
            catch (Exception ex)
            {
                AppendOperationRow("读取历史操作记录失败：" + ex.Message);
            }
        }

        private static void TrackActions(UserControl page, Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                if (control is Button button && button.Name is not ("jogNegativeButton" or "jogPositiveButton"))
                    button.Click += (_, _) => OperationJournal.Record(GetPageName(page),
                        "点击" + button.Text.Replace("\r", " ").Replace("\n", " ").Trim());
                else if (control is CheckBox checkBox)
                    checkBox.CheckedChanged += (_, _) => OperationJournal.Record(GetPageName(page),
                        $"{checkBox.Text.Trim()}：{(checkBox.Checked ? "选中" : "取消")}");
                else if (control is ComboBox comboBox)
                    comboBox.SelectionChangeCommitted += (_, _) => OperationJournal.Record(GetPageName(page),
                        "选择" + comboBox.Text);
                else if (control is RadioButton radioButton)
                    radioButton.CheckedChanged += (_, _) =>
                    {
                        if (radioButton.Checked)
                            OperationJournal.Record(GetPageName(page), "选择" + radioButton.Text);
                    };
                else if (control is TabControl tabs)
                    tabs.SelectedIndexChanged += (_, _) => OperationJournal.Record(GetPageName(page),
                        "切换到" + tabs.SelectedTab?.Text);
                else if (control is Label label && ResultLabels.Contains(label.Name))
                    label.TextChanged += (_, _) => OperationJournal.Record(GetPageName(page), label.Text);

                TrackActions(page, control);
            }
        }

        // 普通按钮保留白底边框；当前页面使用蓝底白字，并区分悬停和按下状态。
        private void SetActiveNavigation(Button activeButton)
        {
            foreach (var button in new[] { Bu_auto, Bu_manual, Bu_Calibration, Bu_data, button2, button5,
                         analysisButton, correctionButton, bu_Configuration })
            {
                var isActive = ReferenceEquals(button, activeButton);
                button.BackColor = isActive ? UiPalette.Primary : UiPalette.Surface;
                button.ForeColor = isActive ? Color.White : UiPalette.Text;
                button.FlatAppearance.BorderColor = isActive ? UiPalette.Primary : UiPalette.Border;
                button.FlatAppearance.MouseOverBackColor = isActive ? UiPalette.PrimaryHover : UiPalette.Selection;
                button.FlatAppearance.MouseDownBackColor = isActive ? UiPalette.PrimaryHover : UiPalette.SecondaryButton;
            }

            navigationMarker.Top = activeButton.Top;
            navigationMarker.Height = activeButton.Height;

            UpdateHeaderStatus();
        }

        // 页面名称和设备反馈分开显示，切页不改变实际控制端或运行模式。
        private void UpdateHeaderStatus()
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
            var snapshot = _monitorService.LatestSnapshot;
            adsStatusLabel.Text = $"● ADS：手动{(connected ? "已连接" : "未连接")} / 监控{(snapshot.IsConnected ? "已连接" : "未连接")}";
            adsStatusLabel.ForeColor = connected && snapshot.IsLive ? UiPalette.Success : UiPalette.Warning;
            var owner = Auto.FormatMonitorField(snapshot.ControlOwner, Auto.FormatControlOwner);
            var mode = Auto.FormatMonitorField(snapshot.ControlMode, Auto.FormatControlMode);
            controlStatusLabel.Text = $"页面：{GetCurrentPageName()}    控制端：{owner}    模式：{mode}";
            controlStatusLabel.ForeColor = snapshot.ControlOwner.IsAvailable && snapshot.ControlOwner.Value is ushort ownerCode && ownerCode <= 3 &&
                snapshot.ControlMode.IsAvailable && snapshot.ControlMode.Value is ushort modeCode && modeCode <= 2
                ? UiPalette.Muted : UiPalette.Warning;
        }

        // 根据当前缓存页面返回顶部状态栏要显示的页面名称。
        private string GetCurrentPageName() => _currentPage is null ? "系统" : GetPageName(_currentPage);

        private static string GetPageName(UserControl page) => page switch
        {
            Auto => "自动运行",
            Manual => "手动控制",
            Calibration => "标定管理",
            WaveBatchCalibrationPage => "批量标定",
            Data => "数据管理",
            WaveformPage => "波形生成",
            Config => "系统配置",
            Wave_Height_Meter => "浪高监测",
            WaveAnalysisPage => "波浪分析",
            SignalCorrectionPage => "信号修正",
            _ => "系统"
        };

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

        // 切换到配置页面。
        private void bu_Configuration_Click(object sender, EventArgs e) => NavigateTo(_config, bu_Configuration);

        // 切换到波形生成页面。
        private void WaveformButton_Click(object sender, EventArgs e) => NavigateTo(_waveformPage, button2);

        private void button5_Click(object sender, EventArgs e) => NavigateTo(_wave_Height_Meter, button5);

        private void Bu_data_Click(object sender, EventArgs e) => NavigateTo(_data, Bu_data);

        private void Bu_Calibration_Click(object sender, EventArgs e) => NavigateTo(_calibration, Bu_Calibration);

        private void AnalysisButton_Click(object sender, EventArgs e) => NavigateTo(_analysis, analysisButton);
        private void CorrectionButton_Click(object sender, EventArgs e) => NavigateTo(_correction, correctionButton);
    }
}
