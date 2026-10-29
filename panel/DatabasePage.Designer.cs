namespace Page_switching.panel;

partial class DatabasePage
{
    private System.ComponentModel.IContainer components;
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            CancelRequests();
            components?.Dispose();
            (grid.DataSource as System.Data.DataTable)?.Dispose();
        }
        base.Dispose(disposing);
    }

    // 筛选用固定的两行，操作按钮另起一行；表格占满剩余空间。
    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        root = new TableLayoutPanel(); title = new Label(); filters = new TableLayoutPanel();
        actions = new FlowLayoutPanel(); kindLabel = new Label(); taskLabel = new Label();
        kindPicker = new ComboBox(); fromDate = new DateTimePicker(); toDate = new DateTimePicker();
        fromLabel = new Label(); toLabel = new Label(); taskId = new TextBox();
        queryButton = new AntdUI.Button(); syncButton = new AntdUI.Button();
        exportButton = new AntdUI.Button(); backButton = new AntdUI.Button();
        grid = new DataGridView(); syncStatus = new Label(); resultStatus = new Label();
        statusTimer = new System.Windows.Forms.Timer(components);
        SuspendLayout(); root.SuspendLayout(); filters.SuspendLayout();
        root.Dock = DockStyle.Fill; root.Padding = new Padding(16);
        root.ColumnCount = 1; root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        root.RowCount = 6;
        root.RowStyles.Add(new RowStyle(SizeType.Absolute, 48));
        root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        title.Text = "数据库记录"; title.Dock = DockStyle.Fill;
        title.Font = new Font("Microsoft YaHei UI", 16, FontStyle.Bold);
        filters.Name = "filters"; filters.Dock = DockStyle.Fill; filters.AutoSize = true;
        filters.BackColor = Color.White; filters.Padding = new Padding(12, 8, 12, 8);
        filters.Margin = new Padding(0);
        filters.ColumnCount = 7;
        filters.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 72));
        filters.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 130));
        filters.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 44));
        filters.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
        filters.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 44));
        filters.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
        filters.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 96));
        filters.RowCount = 2;
        filters.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
        filters.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
        kindLabel.Name = "kindLabel"; kindLabel.Text = "记录类型"; kindLabel.Dock = DockStyle.Fill;
        kindLabel.TextAlign = ContentAlignment.MiddleLeft;
        taskLabel.Name = "taskLabel"; taskLabel.Text = "任务编号"; taskLabel.Dock = DockStyle.Fill;
        taskLabel.TextAlign = ContentAlignment.MiddleLeft;
        kindPicker.Name = "kindPicker"; kindPicker.DropDownStyle = ComboBoxStyle.DropDownList;
        kindPicker.Dock = DockStyle.Fill; kindPicker.Margin = new Padding(0, 9, 8, 4);
        fromLabel.Text = "开始"; fromLabel.Dock = DockStyle.Fill; fromLabel.TextAlign = ContentAlignment.MiddleCenter;
        toLabel.Text = "结束"; toLabel.Dock = DockStyle.Fill; toLabel.TextAlign = ContentAlignment.MiddleCenter;
        fromDate.Name = "fromDate"; fromDate.Format = DateTimePickerFormat.Custom;
        fromDate.CustomFormat = "yyyy-MM-dd HH:mm:ss"; fromDate.Dock = DockStyle.Fill;
        fromDate.Margin = new Padding(0, 9, 8, 4);
        toDate.Name = "toDate"; toDate.Format = DateTimePickerFormat.Custom;
        toDate.CustomFormat = "yyyy-MM-dd HH:mm:ss"; toDate.Dock = DockStyle.Fill;
        toDate.Margin = new Padding(0, 9, 8, 4);
        taskId.Name = "taskId"; taskId.PlaceholderText = "运行、采集任务或点位编号；留空查询全部";
        taskId.Dock = DockStyle.Fill; taskId.Margin = new Padding(0, 9, 8, 4);
        queryButton.Name = "queryButton"; queryButton.Text = "查询"; queryButton.Size = new Size(80, 34);
        queryButton.Radius = 6; queryButton.Click += QueryButton_Click;
        queryButton.Dock = DockStyle.Fill; queryButton.Margin = new Padding(2, 3, 0, 3);
        queryButton.DefaultBack = Color.FromArgb(29, 78, 216); queryButton.ForeColor = Color.White;
        syncButton.Name = "syncButton"; syncButton.Text = "立即同步"; syncButton.Size = new Size(100, 34);
        syncButton.Radius = 6; syncButton.Click += SyncButton_Click;
        syncButton.DefaultBack = Color.White; syncButton.BorderWidth = 1F;
        syncButton.DefaultBorderColor = Color.FromArgb(203, 213, 225);
        exportButton.Name = "exportButton"; exportButton.Text = "导出当前结果"; exportButton.Size = new Size(120, 34);
        exportButton.Radius = 6; exportButton.Click += ExportButton_Click;
        exportButton.DefaultBack = Color.White; exportButton.BorderWidth = 1F;
        exportButton.DefaultBorderColor = Color.FromArgb(203, 213, 225);
        backButton.Name = "backButton"; backButton.Text = "返回数据管理"; backButton.Size = new Size(120, 34);
        backButton.Radius = 6; backButton.Click += BackButton_Click;
        backButton.DefaultBack = Color.White; backButton.BorderWidth = 1F;
        backButton.DefaultBorderColor = Color.FromArgb(203, 213, 225);
        filters.Controls.Add(kindLabel, 0, 0); filters.Controls.Add(kindPicker, 1, 0);
        filters.Controls.Add(fromLabel, 2, 0); filters.Controls.Add(fromDate, 3, 0);
        filters.Controls.Add(toLabel, 4, 0); filters.Controls.Add(toDate, 5, 0);
        filters.Controls.Add(queryButton, 6, 0); filters.Controls.Add(taskLabel, 0, 1);
        filters.Controls.Add(taskId, 1, 1); filters.SetColumnSpan(taskId, 6);
        actions.Name = "actions"; actions.Dock = DockStyle.Fill; actions.AutoSize = true;
        actions.WrapContents = true; actions.Margin = new Padding(0);
        actions.BackColor = Color.White; actions.Padding = new Padding(12, 0, 12, 8);
        actions.Controls.AddRange(new Control[] { syncButton, exportButton, backButton });
        syncStatus.Name = "syncStatus"; syncStatus.AutoSize = true; syncStatus.Dock = DockStyle.Fill;
        syncStatus.Padding = new Padding(4, 6, 4, 6); syncStatus.Text = "数据库未启用，本地归档继续保存。";
        grid.Name = "grid"; grid.Dock = DockStyle.Fill; grid.ReadOnly = true;
        grid.AllowUserToAddRows = false; grid.AllowUserToDeleteRows = false;
        grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        grid.RowHeadersVisible = false; grid.BackgroundColor = Color.White; grid.BorderStyle = BorderStyle.None;
        grid.EnableHeadersVisualStyles = false;
        grid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(234, 241, 250);
        grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(30, 58, 95);
        grid.GridColor = Color.FromArgb(226, 232, 240);
        grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(219, 234, 254);
        grid.DefaultCellStyle.SelectionForeColor = Color.FromArgb(15, 23, 42);
        grid.ColumnHeadersHeight = 32; grid.RowTemplate.Height = 28;
        grid.Paint += Grid_Paint;
        resultStatus.Name = "resultStatus"; resultStatus.Dock = DockStyle.Fill;
        resultStatus.AutoSize = true; resultStatus.MinimumSize = new Size(0, 42);
        resultStatus.Padding = new Padding(4, 8, 4, 8);
        resultStatus.Text = "选择记录类型和时间后查询；任务编号可留空。";
        resultStatus.TextAlign = ContentAlignment.MiddleLeft;
        statusTimer.Interval = 1000; statusTimer.Tick += StatusTimer_Tick;
        root.Controls.Add(title, 0, 0); root.Controls.Add(filters, 0, 1);
        root.Controls.Add(actions, 0, 2); root.Controls.Add(syncStatus, 0, 3);
        root.Controls.Add(grid, 0, 4); root.Controls.Add(resultStatus, 0, 5);
        Controls.Add(root); Name = "DatabasePage"; Size = new Size(1104, 650);
        Font = new Font("Microsoft YaHei UI", 9F); BackColor = Color.FromArgb(241, 245, 249);
        ForeColor = Color.FromArgb(15, 23, 42);
        AutoScaleDimensions = new SizeF(96, 96); AutoScaleMode = AutoScaleMode.Dpi;
        AutoScroll = true; AutoScrollMinSize = new Size(860, 500);
        filters.ResumeLayout(false); filters.PerformLayout(); root.ResumeLayout(false); root.PerformLayout(); ResumeLayout(false);
    }
    private TableLayoutPanel root;
    private TableLayoutPanel filters;
    private FlowLayoutPanel actions;
    private Label kindLabel, taskLabel;
    private Label title, fromLabel, toLabel, syncStatus, resultStatus;
    private ComboBox kindPicker;
    private DateTimePicker fromDate, toDate;
    private TextBox taskId;
    private AntdUI.Button queryButton, syncButton, exportButton, backButton;
    private DataGridView grid;
    private System.Windows.Forms.Timer statusTimer;
}
