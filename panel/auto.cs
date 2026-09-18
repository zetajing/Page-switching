using LogHelper;

namespace Page_switching
{
    public partial class Auto : UserControl
    {
        private readonly ListBox _logList = new();
        private readonly Label _runStateLabel = new();

        // 初始化自动页面控件。
        public Auto()
        {
            InitializeComponent();
            BuildPage();
            AddLog("自动运行页面已就绪");
        }

        // 向自动运行日志区域追加一条带时间的消息。
        public void AddLog(string message)
        {
            if (IsDisposed)
            {
                return;
            }

            if (InvokeRequired)
            {
                BeginInvoke(() => AddLog(message));
                return;
            }

            _logList.Items.Add($"{DateTime.Now:HH:mm:ss}  {message}");
            try
            {
                LogDisplayHelper.ShowMsg($"[自动运行] {message}");
            }
            catch
            {
                // 本地日志组件异常时不影响自动页面显示。
            }

            if (_logList.Items.Count > 500)
            {
                _logList.Items.RemoveAt(0);
            }

            _logList.TopIndex = Math.Max(0, _logList.Items.Count - 1);
        }

        // 更新自动运行页面顶部的当前状态。
        public void SetRunState(string state, Color? color = null)
        {
            _runStateLabel.Text = state;
            _runStateLabel.ForeColor = color ?? Color.FromArgb(5, 150, 105);
            AddLog("状态：" + state);
        }

        // 创建自动运行页面的标题、状态卡片和日志区域。
        private void BuildPage()
        {
            BackColor = Color.FromArgb(241, 245, 249);
            var root = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(0),
                AutoSize = false,
                ColumnCount = 1,
                RowCount = 3,
                Padding = new Padding(20),
                BackColor = Color.FromArgb(241, 245, 249)
            };
            root.RowStyles.Clear();
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 78F));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            var header = new Panel
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(0),
                BackColor = Color.FromArgb(241, 245, 249)
            };
            var title = new Label
            {
                Dock = DockStyle.Top,
                Height = 34,
                Text = "自动运行",
                Font = new Font("Microsoft YaHei UI", 16F, FontStyle.Bold),
                ForeColor = Color.FromArgb(15, 23, 42),
                TextAlign = ContentAlignment.MiddleLeft
            };
            var subtitle = new Label
            {
                Dock = DockStyle.Fill,
                Text = "自动任务状态、PLC 连接和运行日志",
                Font = new Font("Microsoft YaHei UI", 9F),
                ForeColor = Color.FromArgb(71, 85, 105),
                TextAlign = ContentAlignment.MiddleLeft
            };
            header.Controls.Add(subtitle);
            header.Controls.Add(title);
            root.Controls.Add(header, 0, 0);

            var statusGroup = new GroupBox
            {
                Dock = DockStyle.Fill,
                Text = "运行状态",
                Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(15, 23, 42),
                Margin = new Padding(0, 4, 0, 4),
                Padding = new Padding(14, 18, 14, 8)
            };
            var statusLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1
            };
            statusLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            statusLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            statusLayout.Controls.Add(new Label
            {
                Dock = DockStyle.Fill,
                Text = "自动任务",
                ForeColor = Color.FromArgb(71, 85, 105),
                TextAlign = ContentAlignment.MiddleLeft
            }, 0, 0);
            _runStateLabel.Dock = DockStyle.Fill;
            _runStateLabel.Font = new Font("Microsoft YaHei UI", 11F, FontStyle.Bold);
            _runStateLabel.Text = "待机";
            _runStateLabel.TextAlign = ContentAlignment.MiddleLeft;
            _runStateLabel.ForeColor = Color.FromArgb(5, 150, 105);
            statusLayout.Controls.Add(_runStateLabel, 1, 0);
            statusGroup.Controls.Add(statusLayout);
            root.Controls.Add(statusGroup, 0, 1);

            var logGroup = new GroupBox
            {
                Dock = DockStyle.Fill,
                Text = "运行日志",
                Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(15, 23, 42),
                Margin = new Padding(0, 4, 0, 4),
                Padding = new Padding(14, 18, 14, 12)
            };
            var logLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2
            };
            logLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            logLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            _logList.Dock = DockStyle.Fill;
            _logList.Font = new Font("Consolas", 9F);
            _logList.BackColor = Color.White;
            _logList.BorderStyle = BorderStyle.FixedSingle;
            _logList.IntegralHeight = false;
            logLayout.Controls.Add(_logList, 0, 0);
            var clearButton = new Button
            {
                Anchor = AnchorStyles.Left,
                AutoSize = true,
                Text = "清空日志",
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(226, 232, 240),
                ForeColor = Color.FromArgb(15, 23, 42)
            };
            clearButton.FlatAppearance.BorderSize = 0;
            clearButton.Click += (_, _) => _logList.Items.Clear();
            logLayout.Controls.Add(clearButton, 0, 1);
            logGroup.Controls.Add(logLayout);
            root.Controls.Add(logGroup, 0, 2);

            Controls.Add(root);
        }
    }
}
