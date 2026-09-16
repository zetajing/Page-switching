using System.Diagnostics;
using Page_switching.panel;

namespace Page_switching
{
    public partial class Mainpage : Form
    {
        private readonly Auto _autoPage;
        private readonly Manual _manualPage;
        private readonly Config _confige;
        private readonly AxisService _axisService;
        private UserControl? _currentPage;

        public Mainpage()
        {
            InitializeComponent();

            _axisService = new AxisService(AxisServiceOptions.FromConfiguration());
            _autoPage = new Auto();
            _manualPage = new Manual(_axisService);
            _confige = new Config();
            Disposed += (_, _) => _confige.Dispose();

            // 启动时先显示默认页面，避免主区域空白。
            ShowPage(_autoPage);

            // 窗体先显示，再异步建立 ADS 连接；模拟模式不会访问 PLC。
            Shown += Mainpage_Shown;
        }

        private async void Mainpage_Shown(object? sender, EventArgs e)
        {
            try
            {
                await _axisService.ConnectAsync(CancellationToken.None);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ADS 连接失败：" + ex);
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            try
            {
                _axisService.Dispose();
            }
            finally
            {
                base.OnFormClosed(e);
            }
        }

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

        private void Bu_auto_Click(object sender, EventArgs e)
        {
            ShowPage(_autoPage);
        }

        private void Bu_manual_Click(object sender, EventArgs e)
        {
            ShowPage(_manualPage);
        }

        private void bu_Configuration_Click(object sender, EventArgs e)
        {
            ShowPage(_confige);
        }
    }
}
