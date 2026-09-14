using System.Diagnostics;
using System.Configuration;
using InduLink.Protocols.S7;
using S7.Net;

namespace Page_switching
{
    public partial class Mainpage : Form
    {
        private readonly Auto _autoPage;
        private readonly Manual _manualPage;
        private UserControl? _currentPage;
        private SiemensS7Client? _s7Client;

        public Mainpage()
        {
            InitializeComponent();

            _autoPage = new Auto(() => _s7Client);
            _manualPage = new Manual(() => _s7Client);

            // 启动时先显示默认页面，避免主区域空白。
            ShowPage(_autoPage);

            // 不改变原有界面，在主窗体显示后异步建立 S7 连接。
            Shown += Mainpage_Shown;
        }

        private async void Mainpage_Shown(object? sender, EventArgs e)
        {
            try
            {
                await ConnectS7Async();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("S7 连接失败：" + ex);
                MessageBox.Show(this, ex.Message, "S7 连接失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task ConnectS7Async()
        {
            await DisconnectS7Async();

            var client = new SiemensS7Client(new SiemensS7ClientOptions
            {
                DeviceId = "page-switching-s7",
                Host = ConfigurationManager.AppSettings["S7IpAddress"]?.Trim() ?? throw new InvalidOperationException("App.config 中未配置 S7IpAddress。"),
                CpuType = CpuType.S71200,
                Rack = Convert.ToInt16(ConfigurationManager.AppSettings["S7Rack"]),
                Slot = Convert.ToInt16(ConfigurationManager.AppSettings["S7Slot"]),
                AutoReconnect = true,
                AutoReconnectWrites = false,
            });

            _s7Client = client;
            await client.ConnectAsync(CancellationToken.None);
        }

        private async Task DisconnectS7Async()
        {
            var client = _s7Client;
            _s7Client = null;

            if (client is null)
            {
                return;
            }

            try
            {
                if (client.IsConnected)
                {
                    await client.DisconnectAsync(CancellationToken.None);
                }
            }
            finally
            {
                client.Dispose();
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            // 窗体关闭时释放主页面持有的 PLC 连接。
            DisconnectS7Async().GetAwaiter().GetResult();
            base.OnFormClosed(e);
        }

        private void ShowPage(UserControl page)
        {
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

    }
}
