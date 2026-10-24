namespace Page_switching;

partial class AxisMonitoringSettingsForm
{
    private System.ComponentModel.IContainer components = null!;
    private TableLayoutPanel layout = null!;
    private Label noticeLabel = null!;
    private Label stateLabel = null!;
    private Button saveButton = null!;
    private DataGridView thresholdsGrid = null!;
    private DataGridViewTextBoxColumn axisColumn = null!, minColumn = null!, maxColumn = null!, speedColumn = null!;
    protected override void Dispose(bool disposing) { if (disposing) components?.Dispose(); base.Dispose(disposing); }
    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        layout = new TableLayoutPanel(); noticeLabel = new Label(); stateLabel = new Label(); saveButton = new Button(); thresholdsGrid = new DataGridView();
        axisColumn = new DataGridViewTextBoxColumn(); minColumn = new DataGridViewTextBoxColumn();
        maxColumn = new DataGridViewTextBoxColumn(); speedColumn = new DataGridViewTextBoxColumn();
        layout.SuspendLayout(); ((System.ComponentModel.ISupportInitialize)thresholdsGrid).BeginInit(); SuspendLayout();
        layout.Dock = DockStyle.Fill; layout.Padding = new Padding(16); layout.ColumnCount = 1;
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F)); layout.RowCount = 4;
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 78F)); layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F)); layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        layout.Controls.Add(noticeLabel, 0, 0); layout.Controls.Add(thresholdsGrid, 0, 1); layout.Controls.Add(stateLabel, 0, 2); layout.Controls.Add(saveButton, 0, 3);
        noticeLabel.Dock = DockStyle.Fill;
        noticeLabel.Text = "分析用阈值，与PLC运动限值分开，不写入PLC。位置和速度均为PLC/NC原始单位，不换算角度或转速。\r\n每场运行锁存一份，修改后下次生效；未配置仍允许造波，但不提供超限比例。角加速度和扭矩暂未接入。";
        thresholdsGrid.Dock = DockStyle.Fill; thresholdsGrid.AllowUserToAddRows = false; thresholdsGrid.AllowUserToDeleteRows = false;
        thresholdsGrid.RowHeadersVisible = false; thresholdsGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
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
        saveButton.Dock = DockStyle.Fill; saveButton.Text = "保存监测阈值（下次运行生效）"; saveButton.Click += SaveButton_Click;
        Controls.Add(layout); ClientSize = new Size(850, 400); MinimumSize = new Size(720, 390);
        Font = new Font("Microsoft YaHei UI", 9F); AutoScaleMode = AutoScaleMode.Dpi;
        Text = "运行超限监测阈值（不修改PLC）"; StartPosition = FormStartPosition.CenterParent;
        layout.ResumeLayout(false); ((System.ComponentModel.ISupportInitialize)thresholdsGrid).EndInit(); ResumeLayout(false);
    }
}
