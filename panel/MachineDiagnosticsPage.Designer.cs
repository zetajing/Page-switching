namespace Page_switching.panel;

partial class MachineDiagnosticsPage
{
    private System.ComponentModel.IContainer components = null!;
    private TableLayoutPanel rootLayout = null!;
    private FlowLayoutPanel buttonsPanel = null!;
    private Button refreshButton = null!;
    private Button copyButton = null!;
    private Button backButton = null!;
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
        refreshButton = new Button();
        copyButton = new Button();
        backButton = new Button();
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
        rootLayout.RowCount = 4;
        rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
        rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 52F));
        rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 180F));
        rootLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        rootLayout.Dock = DockStyle.Fill;
        rootLayout.Padding = new Padding(16);
        rootLayout.Controls.Add(buttonsPanel, 0, 0);
        rootLayout.Controls.Add(noticeLabel, 0, 1);
        rootLayout.Controls.Add(parametersGrid, 0, 2);
        rootLayout.Controls.Add(diagnosticTextBox, 0, 3);
        buttonsPanel.Dock = DockStyle.Fill;
        buttonsPanel.Controls.Add(refreshButton);
        buttonsPanel.Controls.Add(copyButton);
        buttonsPanel.Controls.Add(backButton);
        refreshButton.Text = "读取诊断";
        refreshButton.Size = new Size(130, 36);
        refreshButton.Click += RefreshButton_Click;
        copyButton.Text = "复制本次诊断";
        copyButton.Size = new Size(150, 36);
        copyButton.Click += CopyButton_Click;
        backButton.Text = "返回控制权页";
        backButton.Size = new Size(150, 36);
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
        parametersGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        parametersGrid.Columns.AddRange(axisColumn, minColumn, maxColumn, velocityColumn,
            accelerationColumn, stopColumn, toleranceColumn, initColumn);
        axisColumn.Name = "axisColumn";
        axisColumn.HeaderText = "轴号";
        axisColumn.FillWeight = 45F;
        axisColumn.MinimumWidth = 65;
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
