using System.ComponentModel;
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
        private readonly Wave_Height_Meter _wave_Height_Meter;
        private readonly Data _data;
        private readonly Calibration _calibration;
        private readonly WaveAnalysisPage _analysis;
        private readonly SignalCorrectionPage _correction;
        private readonly System.Windows.Forms.Timer _headerStatusTimer = new() { Interval = 500 };
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

            _axisService = new AxisService(AxisServiceOptions.FromConfiguration());
            _autoPage = new Auto();
            _manualPage = new Manual(_axisService);
            _config = new Config();
            _waveformPage = new WaveformPage();
            _wave_Height_Meter = new Wave_Height_Meter();
            _data = new Data();
            _calibration = new Calibration();
            _analysis = new WaveAnalysisPage();
            _correction = new SignalCorrectionPage();
            _data.AnalysisRequested += (manifest, channel) =>
            {
                _analysis.OpenSession(manifest, channel);
                NavigateTo(_analysis, analysisButton);
            };
            if (_recordOperations)
                foreach (var page in new UserControl[]
                {
                    _autoPage, _manualPage, _config,
                    _waveformPage, _wave_Height_Meter, _data, _calibration,
                    _analysis, _correction
                })
                    TrackActions(page, page);
            _headerStatusTimer.Tick += (_, _) => UpdateHeaderStatus();
            Disposed += (_, _) =>
            {
                OperationJournal.EntryAdded -= AddOperationEntry;
                _headerStatusTimer.Stop();
                _headerStatusTimer.Dispose();
                _config.Dispose();
                _waveformPage.Dispose();
                _wave_Height_Meter.Dispose();
                _analysis.Dispose();
                _correction.Dispose();
                _data.Dispose();
                _calibration.Dispose();
            };

            // 启动时先显示默认页面，避免主区域空白。
            NavigateTo(_autoPage, Bu_auto);
            _headerStatusTimer.Start();

            // 窗体先显示，再异步建立 ADS 连接。
            Shown += Mainpage_Shown;
        }

        // 主窗体显示后启动可选 Router，并连接真实 ADS PLC。
        private async void Mainpage_Shown(object? sender, EventArgs e)
        {
            _adsTcpRouter = await StartRouterInBackgroundAsync();

            try
            {
                // 部分 ADS 客户端连接方法会在返回 Task 前同步等待；放到后台避免卡住界面绘制。
                await Task.Run(() => _axisService.ConnectAsync(CancellationToken.None));
                _autoPage.AddLog("ADS 连接成功");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ADS 连接失败：" + ex);
                _autoPage.AddLog("ADS 连接失败：" + ex.Message);
            }

            UpdateHeaderStatus();
        }

        // 在后台启动可选的 ADS TCP Router，避免启动阶段阻塞主界面。
        private async Task<AdsTcpRouterHost?> StartRouterInBackgroundAsync()
        {
            if (!AdsTcpRouterRuntime.IsEnabled)
            {
                _autoPage.AddLog("使用系统 TwinCAT Router");
                return null;
            }

            try
            {
                var router = await Task.Run(async () =>
                {
                    var host = AdsTcpRouterRuntime.Create();
                    await host.StartAsync(CancellationToken.None).ConfigureAwait(false);
                    return host;
                });
                _autoPage.AddLog("独立 ADS TCP Router 已启动");
                return router;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ADS TCP Router 启动失败：" + ex);
                _autoPage.AddLog("ADS TCP Router 启动失败：" + ex.Message);
                return null;
            }
        }

        // 主窗体关闭时释放 ADS 连接和 Router。
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            if (_recordOperations) OperationJournal.Record("系统", "程序退出");
            try
            {
                _axisService.Dispose();
                _adsTcpRouter?.Dispose();
            }
            finally
            {
                base.OnFormClosed(e);
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
                    panelswitch.Controls.Remove(_currentPage);
                }

                page.Dock = DockStyle.Fill;
                panelswitch.Controls.Add(page);
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
        }

        private void AddOperationEntry(string entry)
        {
            if (IsDisposed) return;
            if (InvokeRequired)
            {
                BeginInvoke(() => AddOperationEntry(entry));
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
                if (File.Exists(OperationJournal.FilePath))
                    foreach (var entry in File.ReadLines(OperationJournal.FilePath).TakeLast(500))
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

        // 高亮当前页面对应的导航按钮。
        private void SetActiveNavigation(Button activeButton)
        {
            foreach (var button in new[] { Bu_auto, Bu_manual, Bu_Calibration, Bu_data, button2, button5,
                         analysisButton, correctionButton, bu_Configuration })
            {
                var isActive = ReferenceEquals(button, activeButton);
                button.BackColor = isActive ? UiPalette.Selection : UiPalette.Sidebar;
                button.ForeColor = isActive ? UiPalette.PrimaryHover : UiPalette.SecondaryText;
                button.FlatAppearance.MouseOverBackColor = UiPalette.Selection;
            }

            navigationMarker.Top = activeButton.Top;
            navigationMarker.Height = activeButton.Height;

            UpdateHeaderStatus();
        }

        // 更新顶部状态栏，显示手动控制的 ADS 连接和当前页面。
        private void UpdateHeaderStatus()
        {
            if (IsDisposed)
            {
                return;
            }

            var connected = _axisService.IsConnected;
            if (_recordOperations && _lastAdsConnected != connected)
            {
                OperationJournal.Record("系统", connected ? "ADS 已连接" : "ADS 未连接");
                _lastAdsConnected = connected;
            }
            adsStatusLabel.Text = connected ? "●  ADS 已连接" : "●  ADS 未连接";
            adsStatusLabel.ForeColor = connected ? UiPalette.Success : UiPalette.Warning;

            controlStatusLabel.Text = $"当前：{GetCurrentPageName()}    PLC：仅手动控制";
        }

        // 根据当前缓存页面返回顶部状态栏要显示的页面名称。
        private string GetCurrentPageName() => _currentPage is null ? "系统" : GetPageName(_currentPage);

        private static string GetPageName(UserControl page) => page switch
        {
            Auto => "自动运行",
            Manual => "手动控制",
            Calibration => "标定管理",
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
