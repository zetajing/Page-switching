namespace Page_switching.panel;

partial class WaveBatchCalibrationForm
{
    private Panel pagePanel;

    private TableLayoutPanel root = null!;
    private FlowLayoutPanel tools = null!;
    private CheckedListBox channels = null!;
    private Button addButton = null!;
    private Button readButton = null!;
    private Button removeButton = null!;
    private Button saveButton = null!;
    private DataGridView points = null!;
    private Label status = null!;

    private void InitializeComponent()
    {
        pagePanel = new Panel();
        root = new TableLayoutPanel();
        tools = new FlowLayoutPanel();
        channels = new CheckedListBox();
        addButton = new Button();
        readButton = new Button();
        removeButton = new Button();
        saveButton = new Button();
        points = new DataGridView();
        status = new Label();
        root.Dock = DockStyle.Fill;
        root.Padding = new Padding(16);
        root.ColumnCount = 1;
        root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        root.RowCount = 3;
        root.RowStyles.Add(new RowStyle(SizeType.Absolute, 110F));
        root.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        root.RowStyles.Add(new RowStyle(SizeType.Absolute, 70F));
        tools.Dock = DockStyle.Fill;
        channels.Items.AddRange(new object[] { "CH1", "CH2(待验证)", "CH3(待验证)", "CH4(待验证)", "CH5(待验证)", "CH6(待验证)" });
        channels.Size = new Size(280, 95);
        channels.MultiColumn = true;
        channels.ColumnWidth = 130;
        channels.CheckOnClick = true;
        addButton.Text = "添加水位点";
        addButton.Size = new Size(110, 32);
        readButton.Text = "读取最近原始值";
        readButton.Size = new Size(130, 32);
        removeButton.Text = "删除所选点";
        removeButton.Size = new Size(110, 32);
        saveButton.Text = "计算并批量保存";
        saveButton.Size = new Size(130, 32);
        saveButton.BackColor = Color.FromArgb(29, 78, 216);
        saveButton.ForeColor = Color.White;
        saveButton.FlatStyle = FlatStyle.Flat;
        tools.Controls.Add(channels);
        tools.Controls.Add(addButton);
        tools.Controls.Add(readButton);
        tools.Controls.Add(removeButton);
        tools.Controls.Add(saveButton);
        points.Dock = DockStyle.Fill;
        points.BackgroundColor = Color.White;
        points.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        points.Columns.Add("level", "共同水位(mm)");
        points.Columns.Add("CH1", "CH1原始值");
        points.Columns.Add("CH2", "CH2原始值");
        points.Columns.Add("CH3", "CH3原始值");
        points.Columns.Add("CH4", "CH4原始值");
        points.Columns.Add("CH5", "CH5原始值");
        points.Columns.Add("CH6", "CH6原始值");
        status.Dock = DockStyle.Fill;
        status.AutoEllipsis = true;
        status.Text = "每行是同一个实际水位，各列填写独立通道的原始值。至少两个点，任何所选通道计算失败时均不保存。";
        root.Controls.Add(tools, 0, 0);
        root.Controls.Add(points, 0, 1);
        root.Controls.Add(status, 0, 2);
        Font = new Font("Microsoft YaHei UI", 9F);
        BackColor = Color.FromArgb(241, 245, 249);
        AutoScaleDimensions = new SizeF(96F, 96F);
        AutoScaleMode = AutoScaleMode.Dpi;
        ClientSize = new Size(980, 600);
        MinimumSize = new Size(900, 500);
        StartPosition = FormStartPosition.CenterParent;
        Text = "多传感器批量标定";
        pagePanel.Name = "pagePanel";
        pagePanel.Dock = DockStyle.Fill;
        pagePanel.Controls.Add(root);
        Name = "WaveBatchCalibrationForm";
        Controls.Add(pagePanel);
    }
}
