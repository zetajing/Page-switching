namespace Page_switching.panel;

partial class MachineDiagnosticsPage
{
    private System.ComponentModel.IContainer components = null!;
    private TableLayoutPanel rootLayout = null!;
    private FlowLayoutPanel buttonsPanel = null!;
    private AntdUI.Button refreshButton = null!;
    private AntdUI.Button copyButton = null!;
    private AntdUI.Button backButton = null!;
    private Label titleLabel = null!;
    private Label noticeLabel = null!;
    private DataGridView parametersGrid = null!;
    private TextBox diagnosticTextBox = null!;
    private DataGridViewTextBoxColumn axisColumn = null!;
    private DataGridViewTextBoxColumn minColumn = null!;
    private DataGridViewTextBoxColumn maxColumn = null!;
    private DataGridViewTextBoxColumn velocityColumn = null!;
    private DataGridViewTextBoxColumn accelerationColumn = null!;
    private DataGridViewTextBoxColumn stopColumn = null!;
    private DataGridViewTextBoxColumn toleranceColumn = null!;
    private DataGridViewTextBoxColumn initColumn = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing && !IsDisposed)
        {
            CancelRequests();
            _lifetime.Dispose();
            components?.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        rootLayout = new TableLayoutPanel();
        buttonsPanel = new FlowLayoutPanel();
        refreshButton = new AntdUI.Button();
        copyButton = new AntdUI.Button();
        backButton = new AntdUI.Button();
        titleLabel = new Label();
        noticeLabel = new Label();
        parametersGrid = new DataGridView();
        diagnosticTextBox = new TextBox();
        axisColumn = new DataGridViewTextBoxColumn();
        minColumn = new DataGridViewTextBoxColumn();
        maxColumn = new DataGridViewTextBoxColumn();
        velocityColumn = new DataGridViewTextBoxColumn();
        accelerationColumn = new DataGridViewTextBoxColumn();
        stopColumn = new DataGridViewTextBoxColumn();
        toleranceColumn = new DataGridViewTextBoxColumn();
        initColumn = new DataGridViewTextBoxColumn();
        rootLayout.SuspendLayout();
        buttonsPanel.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)parametersGrid).BeginInit();
        SuspendLayout();
        // Designer 保留布局；运行时仅填充只读数据。
        rootLayout.ColumnCount = 1;
        rootLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        rootLayout.RowCount = 5;
        rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
        rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
        rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 52F));
        rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 180F));
        rootLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        rootLayout.Dock = DockStyle.Fill;
        rootLayout.Padding = new Padding(16);
        rootLayout.Controls.Add(titleLabel, 0, 0);
        rootLayout.Controls.Add(buttonsPanel, 0, 1);
        rootLayout.Controls.Add(noticeLabel, 0, 2);
        rootLayout.Controls.Add(parametersGrid, 0, 3);
        rootLayout.Controls.Add(diagnosticTextBox, 0, 4);
        titleLabel.Text = "运行诊断";
        titleLabel.Font = new Font("Microsoft YaHei UI", 16F, FontStyle.Bold);
        titleLabel.ForeColor = Color.FromArgb(15, 23, 42);
        titleLabel.Dock = DockStyle.Fill;
        titleLabel.Margin = new Padding(0);
        buttonsPanel.Dock = DockStyle.Fill;
        buttonsPanel.WrapContents = false;
        buttonsPanel.Margin = new Padding(0);
        buttonsPanel.Controls.Add(refreshButton);
        buttonsPanel.Controls.Add(copyButton);
        buttonsPanel.Controls.Add(backButton);
        refreshButton.Text = "读取诊断";
        refreshButton.Size = new Size(130, 36);
        refreshButton.Radius = 6;
        refreshButton.DefaultBack = Color.FromArgb(29, 78, 216);
        refreshButton.ForeColor = Color.White;
        refreshButton.Margin = new Padding(0, 0, 10, 0);
        refreshButton.Click += RefreshButton_Click;
        copyButton.Text = "复制本次诊断";
        copyButton.Size = new Size(150, 36);
        copyButton.Radius = 6;
        copyButton.DefaultBack = Color.White;
        copyButton.DefaultBorderColor = Color.FromArgb(203, 213, 225);
        copyButton.BorderWidth = 1F;
        copyButton.Margin = new Padding(0, 0, 10, 0);
        copyButton.Click += CopyButton_Click;
        backButton.Text = "返回自动运行";
        backButton.Size = new Size(150, 36);
        backButton.Radius = 6;
        backButton.DefaultBack = Color.White;
        backButton.DefaultBorderColor = Color.FromArgb(203, 213, 225);
        backButton.BorderWidth = 1F;
        backButton.Margin = new Padding(0);
        backButton.Click += BackButton_Click;
        noticeLabel.Text = "四轴参数只读，来源：PLC 的 FB_AxisConfig。位置、速度、加速度均使用 PLC/NC 原始单位。\r\n手动读取一次快照；参数与状态分次读取，时间以本次读取完成时间为准。";
        noticeLabel.Dock = DockStyle.Fill;
        parametersGrid.Dock = DockStyle.Fill;
        parametersGrid.ReadOnly = true;
        parametersGrid.AllowUserToAddRows = false;
        parametersGrid.AllowUserToDeleteRows = false;
        parametersGrid.RowHeadersVisible = false;
        parametersGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        parametersGrid.BackgroundColor = Color.White;
        // 四轴固定行使用紧凑表格；剩余空间交给诊断正文。
        parametersGrid.BorderStyle = BorderStyle.None;
        parametersGrid.GridColor = Color.FromArgb(226, 232, 240);
        parametersGrid.EnableHeadersVisualStyles = false;
        parametersGrid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(234, 241, 250);
        parametersGrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(30, 58, 95);
        parametersGrid.ColumnHeadersHeight = 36;
        parametersGrid.RowTemplate.Height = 30;
        parametersGrid.DefaultCellStyle.Padding = new Padding(6, 0, 6, 0);
        parametersGrid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(219, 234, 254);
        parametersGrid.DefaultCellStyle.SelectionForeColor = Color.FromArgb(15, 23, 42);
        parametersGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        parametersGrid.Columns.AddRange(axisColumn, minColumn, maxColumn, velocityColumn,
            accelerationColumn, stopColumn, toleranceColumn, initColumn);
        axisColumn.Name = "axisColumn";
        axisColumn.HeaderText = "轴号";
        axisColumn.FillWeight = 45F;
        axisColumn.MinimumWidth = 72;
        axisColumn.HeaderCell.Style.WrapMode = DataGridViewTriState.False;
        axisColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
        minColumn.Name = "minColumn";
        minColumn.HeaderText = "最小位置";
        maxColumn.Name = "maxColumn";
        maxColumn.HeaderText = "最大位置";
        velocityColumn.Name = "velocityColumn";
        velocityColumn.HeaderText = "最大速度";
        accelerationColumn.Name = "accelerationColumn";
        accelerationColumn.HeaderText = "最大加速度";
        stopColumn.Name = "stopColumn";
        stopColumn.HeaderText = "停车减速度";
        toleranceColumn.Name = "toleranceColumn";
        toleranceColumn.HeaderText = "到位容差";
        initColumn.Name = "initColumn";
        initColumn.HeaderText = "首点速度";
        diagnosticTextBox.Dock = DockStyle.Fill;
        diagnosticTextBox.Multiline = true;
        diagnosticTextBox.ReadOnly = true;
        diagnosticTextBox.ScrollBars = ScrollBars.Both;
        diagnosticTextBox.WordWrap = true;
        diagnosticTextBox.BackColor = Color.White;
        diagnosticTextBox.Text = "尚未读取 PLC 诊断。";
        BackColor = Color.FromArgb(241, 245, 249);
        AutoScaleDimensions = new SizeF(96F, 96F);
        AutoScaleMode = AutoScaleMode.Dpi;
        Font = new Font("Microsoft YaHei UI", 9F);
        Size = new Size(1060, 700);
        AutoScroll = true;
        AutoScrollMinSize = new Size(860, 600);
        Name = "MachineDiagnosticsPage";
        Text = "运行诊断与四轴参数（只读）";
        Controls.Add(rootLayout);
        rootLayout.ResumeLayout(false);
        rootLayout.PerformLayout();
        buttonsPanel.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)parametersGrid).EndInit();
        ResumeLayout(false);
    }
}
