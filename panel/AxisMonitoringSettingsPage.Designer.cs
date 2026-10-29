namespace Page_switching.panel;

partial class AxisMonitoringSettingsPage
{
    private System.ComponentModel.IContainer components = null!;
    private TableLayoutPanel layout = null!;
    private Label noticeLabel = null!;
    private Label titleLabel = null!;
    private Label stateLabel = null!;
    private AntdUI.Button saveButton = null!;
    private AntdUI.Button backButton = null!;
    private FlowLayoutPanel buttonsPanel = null!;
    private DataGridView thresholdsGrid = null!;
    private DataGridViewTextBoxColumn axisColumn = null!, minColumn = null!, maxColumn = null!, speedColumn = null!;
    protected override void Dispose(bool disposing) { if (disposing) components?.Dispose(); base.Dispose(disposing); }
    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        layout = new TableLayoutPanel(); noticeLabel = new Label(); stateLabel = new Label(); saveButton = new AntdUI.Button(); thresholdsGrid = new DataGridView();
        titleLabel = new Label();
        buttonsPanel = new FlowLayoutPanel();
        backButton = new AntdUI.Button();
        axisColumn = new DataGridViewTextBoxColumn(); minColumn = new DataGridViewTextBoxColumn();
        maxColumn = new DataGridViewTextBoxColumn(); speedColumn = new DataGridViewTextBoxColumn();
        layout.SuspendLayout(); ((System.ComponentModel.ISupportInitialize)thresholdsGrid).BeginInit(); SuspendLayout();
        layout.Dock = DockStyle.Fill; layout.Padding = new Padding(16); layout.ColumnCount = 1;
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F)); layout.RowCount = 6;
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 72F)); layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 176F));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F)); layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 46F));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        layout.Controls.Add(titleLabel, 0, 0); layout.Controls.Add(noticeLabel, 0, 1); layout.Controls.Add(thresholdsGrid, 0, 2); layout.Controls.Add(stateLabel, 0, 3); layout.Controls.Add(buttonsPanel, 0, 4);
        titleLabel.Text = "运行监测阈值"; titleLabel.Dock = DockStyle.Fill;
        titleLabel.Font = new Font("Microsoft YaHei UI", 16F, FontStyle.Bold);
        titleLabel.ForeColor = Color.FromArgb(15, 23, 42);
        titleLabel.Margin = new Padding(0);
        noticeLabel.Dock = DockStyle.Fill;
        noticeLabel.Text = "分析用阈值，与PLC运动限值分开，不写入PLC。位置和速度均为PLC/NC原始单位，不换算角度或转速。\r\n每场运行锁存一份，修改后下次生效；未配置仍允许造波，但不提供超限比例。角加速度和扭矩暂未接入。";
        thresholdsGrid.Dock = DockStyle.Fill; thresholdsGrid.AllowUserToAddRows = false; thresholdsGrid.AllowUserToDeleteRows = false;
        thresholdsGrid.RowHeadersVisible = false; thresholdsGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        // 只显示四轴输入行，保存入口紧跟参数表，避免大窗口出现整片灰色空表。
        thresholdsGrid.BackgroundColor = Color.White;
        thresholdsGrid.BorderStyle = BorderStyle.None;
        thresholdsGrid.GridColor = Color.FromArgb(226, 232, 240);
        thresholdsGrid.EnableHeadersVisualStyles = false;
        thresholdsGrid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(234, 241, 250);
        thresholdsGrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(30, 58, 95);
        thresholdsGrid.ColumnHeadersHeight = 36;
        thresholdsGrid.RowTemplate.Height = 32;
        thresholdsGrid.DefaultCellStyle.Padding = new Padding(6, 0, 6, 0);
        thresholdsGrid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(219, 234, 254);
        thresholdsGrid.DefaultCellStyle.SelectionForeColor = Color.FromArgb(15, 23, 42);
        axisColumn.Name = "axisColumn"; axisColumn.HeaderText = "轴号"; axisColumn.ReadOnly = true; axisColumn.FillWeight = 45F;
        minColumn.Name = "minColumn"; minColumn.HeaderText = "位置下限（原始单位）";
        maxColumn.Name = "maxColumn"; maxColumn.HeaderText = "位置上限（原始单位）";
        speedColumn.Name = "speedColumn"; speedColumn.HeaderText = "最大速度（原始单位/s）";
        // 行号固定对应物理轴，禁止排序后把另一根轴的阈值保存到当前下标。
        axisColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
        minColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
        maxColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
        speedColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
        thresholdsGrid.Columns.AddRange(axisColumn, minColumn, maxColumn, speedColumn);
        stateLabel.Dock = DockStyle.Fill; stateLabel.Text = "未读取配置";
        buttonsPanel.Dock = DockStyle.Fill;
        buttonsPanel.WrapContents = false;
        buttonsPanel.Margin = new Padding(0);
        buttonsPanel.Controls.Add(saveButton);
        buttonsPanel.Controls.Add(backButton);
        saveButton.Size = new Size(330, 32); saveButton.Text = "保存监测阈值（下次运行生效）"; saveButton.Click += SaveButton_Click;
        backButton.Size = new Size(150, 32); backButton.Text = "返回系统配置"; backButton.Click += BackButton_Click;
        saveButton.Radius = 6; saveButton.DefaultBack = Color.FromArgb(29, 78, 216); saveButton.ForeColor = Color.White;
        saveButton.Margin = new Padding(0, 0, 10, 0);
        backButton.Radius = 6; backButton.DefaultBack = Color.White; backButton.BorderWidth = 1F;
        backButton.DefaultBorderColor = Color.FromArgb(203, 213, 225); backButton.Margin = new Padding(0);
        Controls.Add(layout); Size = new Size(850, 400);
        AutoScroll = true; AutoScrollMinSize = new Size(720, 450);
        BackColor = Color.FromArgb(241, 245, 249);
        Name = "AxisMonitoringSettingsPage";
        Text = "运行超限监测阈值（不修改PLC）";
        Font = new Font("Microsoft YaHei UI", 9F); AutoScaleMode = AutoScaleMode.Dpi;
        layout.ResumeLayout(false); ((System.ComponentModel.ISupportInitialize)thresholdsGrid).EndInit(); ResumeLayout(false);
    }
}
