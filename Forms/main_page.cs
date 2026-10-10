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
        private Button? _activeNavigationButton;
        private readonly Dictionary<Button, Image> _navigationImages = new();
        private readonly ToolTip _logPathTip;
        private static readonly HashSet<string> ResultLabels =
        [
            "regularStatusLabel", "irregularStatusLabel",
            "result", "status", "_hintLabel", "_connectionStatusLabel"
        ];

        // 初始化共享轴服务和各个页面，并显示默认页面。
        public Mainpage()
        {
            InitializeComponent();
            components ??= new Container();
            _logPathTip = new ToolTip(components) { AutoPopDelay = 30000 };
            // 路径缩略显示时仍能悬停查看完整目录；保存配置后的 TextChanged 同步提示。
            operationPathLabel.TextChanged += (_, _) => _logPathTip.SetToolTip(operationPathLabel, operationPathLabel.Text);
            if (_recordOperations)
            {
                operationPathLabel.Text = "日志保存位置：" + OperationJournal.DirectoryPath;
                OperationJournal.EntryAdded += AddOperationEntry;
                OperationJournal.Record("系统", "程序启动");
            }

            // 本次启动只读取一次 ADS 配置；保存的新参数在下次启动才应用。
            var settings = AdsConnectionSettings.GetSettings();
            var axisOptions = AxisServiceOptions.FromConfiguration(settings);
            _axisService = new AxisService(axisOptions);
            _monitorService = new MachineMonitorService(MachineMonitorOptions.FromConfiguration(axisOptions, settings));
            _autoPage = new Auto();
            _router = new AdsTcpRouterRuntime(settings, message =>
            {
                if (!_closing) _autoPage.AddLog(message);
            });
            _manualPage = new Manual(_axisService);
            _config = new Config();
            _waveformPage = new WaveformPage();
            _wave_Height_Meter = new Wave_Height_Meter();
            _data = new Data();
            _calibration = new Calibration();
            _batchCalibrationPage = new WaveBatchCalibrationPage();
            _analysis = new WaveAnalysisPage();
            _correction = new SignalCorrectionPage();
            // 页面集合统一用于日志注册和退出释放，不再维护两份清单。
            _pages = [_autoPage, _manualPage, _config, _waveformPage, _wave_Height_Meter,
                _data, _calibration, _batchCalibrationPage, _analysis, _correction];
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
                foreach (var page in _pages)
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
            foreach (var image in _navigationImages.Values) image.Dispose();
            _navigationImages.Clear();
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

        private void NavigateTo(UserControl page, Button button)
        {
            if (_recordOperations) OperationJournal.Record("页面切换", "打开" + GetPageName(page));
            ShowPage(page);
            SetActiveNavigation(button);
            if (ReferenceEquals(page, _autoPage)) _ = RefreshMonitorAsync();
        }

        // 日志内容只写入文件；新记录通知仅用于同步配置后的保存目录。
        private void AddOperationEntry(string entry)
        {
            if (_closing || IsDisposed || Disposing) return;
            if (InvokeRequired)
            {
                PostToUi(() => AddOperationEntry(entry));
                return;
            }

            operationPathLabel.Text = "日志保存位置：" + OperationJournal.DirectoryPath;
        }

        private void OpenOperationFolderButton_Click(object? sender, EventArgs e)
        {
            try
            {
                var directory = OperationJournal.DirectoryPath;
                Directory.CreateDirectory(directory);
                Process.Start(new ProcessStartInfo(directory) { UseShellExecute = true });
                OperationJournal.Record("操作记录", "已打开日志目录");
            }
            catch (Exception ex)
            {
                OperationJournal.Record("操作记录", "打开目录失败：" + ex.Message);
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
                // 已主动记录结果的页面不再从标签变化重复推断结果。
                // 旧页面仍保留原来的标签日志，逐步迁移时不会丢失操作结果。
                else if (control is Label label && ResultLabels.Contains(label.Name) &&
                    page is not (WaveAnalysisPage or SignalCorrectionPage or WaveBatchCalibrationPage) &&
                    !(page is WaveformPage && label.Name == "status"))
                    label.TextChanged += (_, _) => OperationJournal.Record(GetPageName(page), label.Text);

                TrackActions(page, control);
            }
        }

        // 图标与文字使用同一选中颜色；替换图标时释放旧位图，避免切页积累 GDI 资源。
        private void SetActiveNavigation(Button activeButton)
        {
            _activeNavigationButton = activeButton;
            foreach (var button in new[] { Bu_auto, Bu_manual, Bu_Calibration, Bu_data, button2, button5,
                         analysisButton, correctionButton, bu_Configuration })
            {
                var isActive = ReferenceEquals(button, activeButton);
                button.BackColor = isActive ? UiPalette.WorkSelection : UiPalette.WorkCanvas;
                button.ForeColor = isActive ? UiPalette.WorkPrimary : UiPalette.WorkText;
                button.FlatAppearance.BorderSize = 0;
                button.FlatAppearance.MouseOverBackColor = UiPalette.WorkSelection;
                button.FlatAppearance.MouseDownBackColor = UiPalette.WorkPressed;
                var icon = button.Name switch
                {
                    "Bu_auto" => UiIcon.Overview, "Bu_manual" => UiIcon.Manual,
                    "Bu_Calibration" => UiIcon.Calibration, "Bu_data" => UiIcon.Data,
                    "button2" => UiIcon.Wave, "button5" => UiIcon.Monitor,
                    "analysisButton" => UiIcon.Analysis, "correctionButton" => UiIcon.Correction,
                    _ => UiIcon.Settings
                };
                var image = UiIcons.Create(icon, button.ForeColor, (int)Math.Round(20 * DeviceDpi / 96d));
                button.Image = image;
                if (_navigationImages.Remove(button, out var oldImage)) oldImage.Dispose();
                _navigationImages.Add(button, image);
            }

            navigationMarker.Top = activeButton.Top;
            navigationMarker.Height = activeButton.Height;

            UpdateHeaderStatus();
        }

        protected override void OnDpiChanged(DpiChangedEventArgs e)
        {
            base.OnDpiChanged(e);
            if (_activeNavigationButton is not null) SetActiveNavigation(_activeNavigationButton);
        }

        protected override void OnLayout(LayoutEventArgs e)
        {
            base.OnLayout(e);
            if (panel2 is null) return;
            // 为出现的原生纵向滚动条留出空间，侧栏自身不产生横向滚动。
            var scale = DeviceDpi / 96d;
            var width = Math.Max((int)(110 * scale), panel2.ClientSize.Width - panel2.Padding.Horizontal);
            foreach (var button in panel2.Controls.OfType<Button>())
                if (button.Width != width) button.Width = width;
            foreach (var label in panel2.Controls.OfType<Label>())
                if (label.Width != panel2.ClientSize.Width - (int)(40 * scale))
                    label.Width = Math.Max(1, panel2.ClientSize.Width - (int)(40 * scale));
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
        private string GetCurrentPageName() => _currentPage is Auto ? "运行总览"
            : _currentPage is null ? "系统" : GetPageName(_currentPage);

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
