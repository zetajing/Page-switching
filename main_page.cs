using System.Diagnostics;
using Page_switching.panel;
using InduLink.Protocols.Ads.Router;

namespace Page_switching
{
    public partial class Mainpage : Form
    {
        private readonly Auto _autoPage;
        private readonly Manual _manualPage;
        private readonly Config _confige;
        private readonly WaveformPage _waveformPage;
        private readonly AxisService _axisService;
        private AdsTcpRouterHost? _adsTcpRouter;
        private UserControl? _currentPage;

        // 初始化共享轴服务和各个页面，并显示默认页面。
        public Mainpage()
        {
            InitializeComponent();

            _axisService = new AxisService(AxisServiceOptions.FromConfiguration());
            _autoPage = new Auto();
            _manualPage = new Manual(_axisService);
            _confige = new Config();
            _waveformPage = new WaveformPage();
            Disposed += (_, _) =>
            {
                _confige.Dispose();
                _waveformPage.Dispose();
            };

            // 启动时先显示默认页面，避免主区域空白。
            ShowPage(_autoPage);
            SetActiveNavigation(Bu_auto);

            // 窗体先显示，再异步建立 ADS 连接。
            Shown += Mainpage_Shown;
        }

        // 主窗体显示后启动可选 Router，并连接真实 ADS PLC。
        private async void Mainpage_Shown(object? sender, EventArgs e)
        {
            if (AdsTcpRouterRuntime.IsEnabled)
            {
                try
                {
                    _adsTcpRouter = AdsTcpRouterRuntime.Create();
                    await _adsTcpRouter.StartAsync(CancellationToken.None);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("ADS TCP Router 启动失败：" + ex);
                }
            }

            try
            {
                await _axisService.ConnectAsync(CancellationToken.None);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ADS 连接失败：" + ex);
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
            foreach (var button in new[] { Bu_auto, Bu_manual, button2, bu_Configuration })
            {
                var isActive = ReferenceEquals(button, activeButton);
                button.BackColor = isActive
                    ? Color.FromArgb(14, 165, 233)
                    : Color.FromArgb(30, 41, 59);
                button.ForeColor = isActive ? Color.White : Color.FromArgb(226, 232, 240);
            }
        }

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
