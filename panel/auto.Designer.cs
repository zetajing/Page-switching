namespace Page_switching
{
    partial class Auto
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private TableLayoutPanel rootLayout;
        private Panel headerPanel;
        private Label titleLabel;
        private Label subtitleLabel;
        private GroupBox statusGroup;
        private TableLayoutPanel statusLayout;
        private Label taskCaptionLabel;
        private Label _runStateLabel;
        private GroupBox logGroup;
        private TableLayoutPanel logLayout;
        private ListBox _logList;
        private Button clearLogButton;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        // 释放设计器创建的自动页面组件。
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        // 创建设计器中的自动页面控件并设置布局。
        private void InitializeComponent()
        {
            rootLayout = new TableLayoutPanel();
            headerPanel = new Panel();
            titleLabel = new Label();
            subtitleLabel = new Label();
            statusGroup = new GroupBox();
            statusLayout = new TableLayoutPanel();
            taskCaptionLabel = new Label();
            _runStateLabel = new Label();
            logGroup = new GroupBox();
            logLayout = new TableLayoutPanel();
            _logList = new ListBox();
            clearLogButton = new Button();
            rootLayout.SuspendLayout();
            headerPanel.SuspendLayout();
            statusGroup.SuspendLayout();
            statusLayout.SuspendLayout();
            logGroup.SuspendLayout();
            logLayout.SuspendLayout();
            SuspendLayout();
            // 
            // rootLayout
            // 
            rootLayout.BackColor = Color.FromArgb(241, 245, 249);
            rootLayout.ColumnCount = 1;
            rootLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            rootLayout.Controls.Add(headerPanel, 0, 0);
            rootLayout.Controls.Add(statusGroup, 0, 1);
            rootLayout.Controls.Add(logGroup, 0, 2);
            rootLayout.Dock = DockStyle.Fill;
            rootLayout.Margin = new Padding(0);
            rootLayout.Padding = new Padding(20);
            rootLayout.RowCount = 3;
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 78F));
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            rootLayout.TabIndex = 0;
            // 
            // headerPanel
            // 
            headerPanel.BackColor = Color.FromArgb(241, 245, 249);
            headerPanel.Controls.Add(subtitleLabel);
            headerPanel.Controls.Add(titleLabel);
            headerPanel.Dock = DockStyle.Fill;
            headerPanel.Margin = new Padding(0);
            headerPanel.TabIndex = 0;
            // 
            // titleLabel
            // 
            titleLabel.Dock = DockStyle.Top;
            titleLabel.Font = new Font("Microsoft YaHei UI", 16F, FontStyle.Bold);
            titleLabel.ForeColor = Color.FromArgb(15, 23, 42);
            titleLabel.Height = 34;
            titleLabel.Name = "titleLabel";
            titleLabel.TabIndex = 0;
            titleLabel.Text = "自动运行";
            titleLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // subtitleLabel
            // 
            subtitleLabel.Dock = DockStyle.Fill;
            subtitleLabel.Font = new Font("Microsoft YaHei UI", 9F);
            subtitleLabel.ForeColor = Color.FromArgb(71, 85, 105);
            subtitleLabel.Name = "subtitleLabel";
            subtitleLabel.TabIndex = 1;
            subtitleLabel.Text = "自动任务状态、PLC 连接和运行日志";
            subtitleLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // statusGroup
            // 
            statusGroup.Controls.Add(statusLayout);
            statusGroup.Dock = DockStyle.Fill;
            statusGroup.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
            statusGroup.ForeColor = Color.FromArgb(15, 23, 42);
            statusGroup.Margin = new Padding(0, 4, 0, 4);
            statusGroup.Padding = new Padding(14, 18, 14, 8);
            statusGroup.TabIndex = 1;
            statusGroup.TabStop = false;
            statusGroup.Text = "运行状态";
            // 
            // statusLayout
            // 
            statusLayout.ColumnCount = 2;
            statusLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            statusLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            statusLayout.Controls.Add(taskCaptionLabel, 0, 0);
            statusLayout.Controls.Add(_runStateLabel, 1, 0);
            statusLayout.Dock = DockStyle.Fill;
            statusLayout.RowCount = 1;
            statusLayout.TabIndex = 0;
            // 
            // taskCaptionLabel
            // 
            taskCaptionLabel.Dock = DockStyle.Fill;
            taskCaptionLabel.Font = new Font("Microsoft YaHei UI", 9F);
            taskCaptionLabel.ForeColor = Color.FromArgb(71, 85, 105);
            taskCaptionLabel.Name = "taskCaptionLabel";
            taskCaptionLabel.TabIndex = 0;
            taskCaptionLabel.Text = "自动任务";
            taskCaptionLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // _runStateLabel
            // 
            _runStateLabel.Dock = DockStyle.Fill;
            _runStateLabel.Font = new Font("Microsoft YaHei UI", 11F, FontStyle.Bold);
            _runStateLabel.ForeColor = Color.FromArgb(5, 150, 105);
            _runStateLabel.Name = "_runStateLabel";
            _runStateLabel.TabIndex = 1;
            _runStateLabel.Text = "待机";
            _runStateLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // logGroup
            // 
            logGroup.Controls.Add(logLayout);
            logGroup.Dock = DockStyle.Fill;
            logGroup.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
            logGroup.ForeColor = Color.FromArgb(15, 23, 42);
            logGroup.Margin = new Padding(0, 4, 0, 4);
            logGroup.Padding = new Padding(14, 18, 14, 12);
            logGroup.TabIndex = 2;
            logGroup.TabStop = false;
            logGroup.Text = "运行日志";
            // 
            // logLayout
            // 
            logLayout.ColumnCount = 1;
            logLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            logLayout.Controls.Add(_logList, 0, 0);
            logLayout.Controls.Add(clearLogButton, 0, 1);
            logLayout.Dock = DockStyle.Fill;
            logLayout.RowCount = 2;
            logLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            logLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            logLayout.TabIndex = 0;
            // 
            // _logList
            // 
            _logList.BackColor = Color.White;
            _logList.BorderStyle = BorderStyle.FixedSingle;
            _logList.Dock = DockStyle.Fill;
            _logList.Font = new Font("Consolas", 9F);
            _logList.IntegralHeight = false;
            _logList.ItemHeight = 17;
            _logList.TabIndex = 0;
            // 
            // clearLogButton
            // 
            clearLogButton.Anchor = AnchorStyles.Left;
            clearLogButton.AutoSize = true;
            clearLogButton.BackColor = Color.FromArgb(226, 232, 240);
            clearLogButton.FlatAppearance.BorderSize = 0;
            clearLogButton.FlatStyle = FlatStyle.Flat;
            clearLogButton.ForeColor = Color.FromArgb(15, 23, 42);
            clearLogButton.Location = new Point(3, 386);
            clearLogButton.Name = "clearLogButton";
            clearLogButton.TabIndex = 1;
            clearLogButton.Text = "清空日志";
            clearLogButton.UseVisualStyleBackColor = false;
            clearLogButton.Click += ClearLogButton_Click;
            // 
            // Auto
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(241, 245, 249);
            Controls.Add(rootLayout);
            Name = "Auto";
            Size = new Size(905, 681);
            logLayout.ResumeLayout(false);
            logGroup.ResumeLayout(false);
            statusLayout.ResumeLayout(false);
            statusGroup.ResumeLayout(false);
            headerPanel.ResumeLayout(false);
            rootLayout.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
    }
}
