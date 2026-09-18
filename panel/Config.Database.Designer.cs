namespace Page_switching.panel;

partial class Config
{
    private GroupBox _databaseGroup = null!;
    private TableLayoutPanel _databaseLayout = null!;
    private CheckBox _databaseEnabledCheckBox = null!;
    private TextBox _databaseConnectionTextBox = null!;
    private Button _saveDatabaseButton = null!;
    private Label _databaseStateLabel = null!;

    // 创建 Designer 中可见的 SQL Server 数据库日志区域。
    private void InitializeDatabaseControls()
    {
        _databaseGroup = new GroupBox();
        _databaseLayout = new TableLayoutPanel();
        _databaseEnabledCheckBox = new CheckBox();
        _databaseConnectionTextBox = new TextBox();
        _saveDatabaseButton = new Button();
        _databaseStateLabel = new Label();
        rootLayout.SuspendLayout();
        _databaseGroup.SuspendLayout();
        _databaseLayout.SuspendLayout();
        //
        // databaseGroup
        //
        _databaseGroup.Controls.Add(_databaseLayout);
        _databaseGroup.Dock = DockStyle.Fill;
        _databaseGroup.Font = new Font("Microsoft YaHei UI", 10F, FontStyle.Bold);
        _databaseGroup.ForeColor = Color.FromArgb(15, 23, 42);
        _databaseGroup.Padding = new Padding(14, 18, 14, 10);
        _databaseGroup.TabStop = false;
        _databaseGroup.Text = "数据库日志";
        //
        // databaseLayout
        //
        _databaseLayout.ColumnCount = 3;
        _databaseLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 220F));
        _databaseLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _databaseLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
        _databaseLayout.Controls.Add(_databaseEnabledCheckBox, 0, 0);
        _databaseLayout.Controls.Add(_databaseConnectionTextBox, 1, 0);
        _databaseLayout.Controls.Add(_saveDatabaseButton, 2, 0);
        _databaseLayout.Controls.Add(_databaseStateLabel, 1, 1);
        _databaseLayout.Dock = DockStyle.Fill;
        _databaseLayout.RowCount = 2;
        _databaseLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
        _databaseLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
        _databaseLayout.SetColumnSpan(_databaseStateLabel, 2);
        //
        // databaseEnabledCheckBox
        //
        _databaseEnabledCheckBox.Dock = DockStyle.Fill;
        _databaseEnabledCheckBox.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
        _databaseEnabledCheckBox.Text = "启用 SQL 日志";
        _databaseEnabledCheckBox.CheckedChanged += (_, _) => UpdateDatabaseState();
        //
        // databaseConnectionTextBox
        //
        _databaseConnectionTextBox.Dock = DockStyle.Fill;
        _databaseConnectionTextBox.Font = new Font("Microsoft YaHei UI", 9F);
        _databaseConnectionTextBox.PlaceholderText = "SQL Server 连接字符串";
        //
        // saveDatabaseButton
        //
        _saveDatabaseButton.BackColor = Color.FromArgb(226, 232, 240);
        _saveDatabaseButton.Dock = DockStyle.Fill;
        _saveDatabaseButton.FlatAppearance.BorderSize = 0;
        _saveDatabaseButton.FlatStyle = FlatStyle.Flat;
        _saveDatabaseButton.Font = new Font("Microsoft YaHei UI", 9F);
        _saveDatabaseButton.ForeColor = Color.FromArgb(15, 23, 42);
        _saveDatabaseButton.Text = "保存数据库";
        _saveDatabaseButton.Click += SaveDatabaseButton_Click;
        //
        // databaseStateLabel
        //
        _databaseStateLabel.Dock = DockStyle.Fill;
        _databaseStateLabel.Font = new Font("Microsoft YaHei UI", 8F);
        _databaseStateLabel.ForeColor = Color.FromArgb(100, 116, 139);
        _databaseStateLabel.Text = "数据库日志未启用。";
        _databaseStateLabel.TextAlign = ContentAlignment.MiddleLeft;
        //
        // add the database area to the page
        //
        rootLayout.RowCount = 5;
        rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 100F));
        rootLayout.Controls.Add(_databaseGroup, 0, 4);
        _databaseLayout.ResumeLayout(false);
        _databaseGroup.ResumeLayout(false);
        rootLayout.ResumeLayout(false);
    }
}
