using System.Configuration;
using System.Diagnostics;
using LogHelper;
using Page_switching.panel;
using InduLink.Protocols.Ads.Router;

namespace Page_switching
{
    public partial class Mainpage : Form
    {
        private readonly Auto _autoPage;
        private readonly Manual _manualPage;
        private readonly ControlAuthority _controlAuthorityPage;
        private readonly Config _confige;
        private readonly WaveformPage _waveformPage;
        private readonly AxisService _axisService;
        private readonly System.Windows.Forms.Timer _headerStatusTimer = new() { Interval = 500 };
        private AdsTcpRouterHost? _adsTcpRouter;
        private UserControl? _currentPage;

        // 初始化共享轴服务和各个页面，并显示默认页面。
        public Mainpage()
        {
            InitializeComponent();

            _axisService = new AxisService(AxisServiceOptions.FromConfiguration());
            _autoPage = new Auto();
            _manualPage = new Manual(_axisService);
            _controlAuthorityPage = new ControlAuthority(_axisService);
            _confige = new Config();
            _waveformPage = new WaveformPage();
            _headerStatusTimer.Tick += (_, _) => UpdateHeaderStatus();
            Disposed += (_, _) =>
            {
                _headerStatusTimer.Stop();
                _headerStatusTimer.Dispose();
                _confige.Dispose();
                _waveformPage.Dispose();
                _controlAuthorityPage.Dispose();
            };

            // 启动时先显示默认页面，避免主区域空白。
            ShowPage(_autoPage);
            SetActiveNavigation(Bu_auto);
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

        // 高亮当前页面对应的导航按钮，并恢复其他按钮的深色背景。
        private void SetActiveNavigation(Button activeButton)
        {
            foreach (var button in new[] { Bu_auto, Bu_manual, button3, button2, bu_Configuration })
            {
                var isActive = ReferenceEquals(button, activeButton);
                button.BackColor = isActive
                    ? Color.FromArgb(14, 165, 233)
                    : Color.FromArgb(30, 41, 59);
                button.ForeColor = isActive ? Color.White : Color.FromArgb(226, 232, 240);
            }

            UpdateHeaderStatus(activeButton);
        }

        // 更新顶部状态栏，显示 ADS 连接、当前页面、控制权和安全配置状态。
        private void UpdateHeaderStatus(Button? activeButton = null)
        {
            if (IsDisposed)
            {
                return;
            }

            var connected = _axisService.IsConnected;
            adsStatusLabel.Text = connected ? "●  ADS 已连接" : "●  ADS 未连接";
            adsStatusLabel.ForeColor = connected
                ? Color.FromArgb(74, 222, 128)
                : Color.FromArgb(251, 146, 60);

            var pageName = activeButton is null
                ? GetCurrentPageName()
                : GetPageName(activeButton);
            var ownerState = HasControlSetting("AdsControlOwnerStationId") ? "已配置" : "待配置";
            var safetyState = HasControlSetting("AdsSafetyOk") ? "已配置" : "待配置";
            controlStatusLabel.Text = $"当前：{pageName}    控制权：{ownerState}    安全：{safetyState}";
        }

        // 根据当前缓存页面返回顶部状态栏要显示的页面名称。
        private string GetCurrentPageName() => _currentPage switch
        {
            Auto => "自动运行",
            Manual => "手动控制",
            WaveformPage => "波形生成",
            Config => "系统配置",
            _ => "系统"
        };

        // 根据导航按钮返回简短的页面名称。
        private string GetPageName(Button button) => button switch
        {
            var value when ReferenceEquals(value, Bu_auto) => "自动运行",
            var value when ReferenceEquals(value, Bu_manual) => "手动控制",
            var value when ReferenceEquals(value, button3) => "控制权申请",
            var value when ReferenceEquals(value, button2) => "波形生成",
            var value when ReferenceEquals(value, bu_Configuration) => "系统配置",
            _ => "系统"
        };

        // 检查一个控制权相关配置项是否已经填入 PLC 变量名。
        private static bool HasControlSetting(string key) =>
            !string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings[key]);

        // 切换到自动页面。
        private void Bu_auto_Click(object sender, EventArgs e)
        {
            ShowPage(_autoPage);
            SetActiveNavigation(Bu_auto);
        }

        // 切换到手动控制页面。
        private void Bu_manual_Click(object sender, EventArgs e)
        {
            ShowPage(_manualPage);
            SetActiveNavigation(Bu_manual);
        }

        // 切换到控制权申请页面。
        private void ControlAuthorityButton_Click(object sender, EventArgs e)
        {
            ShowPage(_controlAuthorityPage);
            SetActiveNavigation(button3);
        }

        // 切换到配置页面。
        private void bu_Configuration_Click(object sender, EventArgs e)
        {
            ShowPage(_confige);
            SetActiveNavigation(bu_Configuration);
        }

        // 切换到波形生成页面。
        private void WaveformButton_Click(object sender, EventArgs e)
        {
            ShowPage(_waveformPage);
            SetActiveNavigation(button2);
        }
    }
}
