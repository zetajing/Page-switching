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

    // 全部布局保留在Designer，筛选区随窗口宽度换行，表格占满剩余空间。
    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        root = new TableLayoutPanel(); title = new Label(); filters = new FlowLayoutPanel();
        kindPicker = new ComboBox(); fromDate = new DateTimePicker(); toDate = new DateTimePicker();
        fromLabel = new Label(); toLabel = new Label(); taskId = new TextBox();
        queryButton = new AntdUI.Button(); syncButton = new AntdUI.Button();
        exportButton = new AntdUI.Button(); backButton = new AntdUI.Button();
        grid = new DataGridView(); syncStatus = new Label(); resultStatus = new Label();
        statusTimer = new System.Windows.Forms.Timer(components);
        SuspendLayout(); root.SuspendLayout(); filters.SuspendLayout();
        root.Dock = DockStyle.Fill; root.Padding = new Padding(16);
        root.ColumnCount = 1; root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        root.RowCount = 5;
        root.RowStyles.Add(new RowStyle(SizeType.Absolute, 42));
        root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        root.RowStyles.Add(new RowStyle(SizeType.Absolute, 42));
        title.Text = "数据库记录"; title.Dock = DockStyle.Fill;
        title.Font = new Font("Microsoft YaHei UI", 16, FontStyle.Bold);
        filters.Name = "filters"; filters.Dock = DockStyle.Fill; filters.AutoSize = true;
        filters.WrapContents = true; filters.Padding = new Padding(6);
        kindPicker.Name = "kindPicker"; kindPicker.DropDownStyle = ComboBoxStyle.DropDownList;
        kindPicker.Size = new Size(115, 30);
        fromLabel.Text = "开始"; fromLabel.AutoSize = true; fromLabel.Margin = new Padding(5, 7, 2, 0);
        toLabel.Text = "结束"; toLabel.AutoSize = true; toLabel.Margin = new Padding(5, 7, 2, 0);
        fromDate.Name = "fromDate"; fromDate.Format = DateTimePickerFormat.Custom;
        fromDate.CustomFormat = "yyyy-MM-dd HH:mm:ss"; fromDate.Size = new Size(180, 30);
        toDate.Name = "toDate"; toDate.Format = DateTimePickerFormat.Custom;
        toDate.CustomFormat = "yyyy-MM-dd HH:mm:ss"; toDate.Size = new Size(180, 30);
        taskId.Name = "taskId"; taskId.PlaceholderText = "任务/点位编号（可留空）"; taskId.Size = new Size(220, 30);
        queryButton.Name = "queryButton"; queryButton.Text = "查询"; queryButton.Size = new Size(80, 34);
        queryButton.Radius = 6; queryButton.Click += QueryButton_Click;
        syncButton.Name = "syncButton"; syncButton.Text = "立即同步"; syncButton.Size = new Size(100, 34);
        syncButton.Radius = 6; syncButton.Click += SyncButton_Click;
        exportButton.Name = "exportButton"; exportButton.Text = "导出当前结果"; exportButton.Size = new Size(120, 34);
        exportButton.Radius = 6; exportButton.Click += ExportButton_Click;
        backButton.Name = "backButton"; backButton.Text = "返回数据管理"; backButton.Size = new Size(120, 34);
        backButton.Radius = 6; backButton.Click += BackButton_Click;
        filters.Controls.AddRange(new Control[] { kindPicker, fromLabel, fromDate, toLabel, toDate, taskId,
            queryButton, syncButton, exportButton, backButton });
        syncStatus.Name = "syncStatus"; syncStatus.AutoSize = true; syncStatus.Dock = DockStyle.Fill;
        syncStatus.Padding = new Padding(4, 6, 4, 6); syncStatus.Text = "数据库未启用，本地归档继续保存。";
        grid.Name = "grid"; grid.Dock = DockStyle.Fill; grid.ReadOnly = true;
        grid.AllowUserToAddRows = false; grid.AllowUserToDeleteRows = false;
        grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
        grid.RowHeadersVisible = false; grid.BackgroundColor = Color.White; grid.BorderStyle = BorderStyle.FixedSingle;
        resultStatus.Name = "resultStatus"; resultStatus.Dock = DockStyle.Fill;
        resultStatus.Text = "任务编号：运行用RunId；轴反馈/报告用MonitorRunId；传感器用SessionId；点位用DataId。";
        resultStatus.TextAlign = ContentAlignment.MiddleLeft;
        statusTimer.Interval = 1000; statusTimer.Tick += StatusTimer_Tick;
        root.Controls.Add(title, 0, 0); root.Controls.Add(filters, 0, 1);
        root.Controls.Add(syncStatus, 0, 2); root.Controls.Add(grid, 0, 3); root.Controls.Add(resultStatus, 0, 4);
        Controls.Add(root); Name = "DatabasePage"; Size = new Size(1104, 650);
        Font = new Font("Microsoft YaHei UI", 9F); BackColor = Color.FromArgb(241, 245, 249);
        AutoScaleDimensions = new SizeF(96, 96); AutoScaleMode = AutoScaleMode.Dpi;
        AutoScroll = true; AutoScrollMinSize = new Size(860, 500);
        filters.ResumeLayout(false); filters.PerformLayout(); root.ResumeLayout(false); root.PerformLayout(); ResumeLayout(false);
    }
    private TableLayoutPanel root;
    private FlowLayoutPanel filters;
    private Label title, fromLabel, toLabel, syncStatus, resultStatus;
    private ComboBox kindPicker;
    private DateTimePicker fromDate, toDate;
    private TextBox taskId;
    private AntdUI.Button queryButton, syncButton, exportButton, backButton;
    private DataGridView grid;
    private System.Windows.Forms.Timer statusTimer;
}
