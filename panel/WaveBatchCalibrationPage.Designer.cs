namespace Page_switching.panel
{

    partial class WaveBatchCalibrationPage
    {
        private Panel pagePanel;
        private FlowLayoutPanel header;
        private Label title;
        private Button backButton;

        private TableLayoutPanel root;
        private FlowLayoutPanel tools;
        private CheckedListBox channels;
        private Button addButton;
        private Button readButton;
        private Button removeButton;
        private Button saveButton;
        private DataGridView points;
        private Label status;

        private void InitializeComponent()
        {
            pagePanel = new Panel();
            header = new FlowLayoutPanel();
            title = new Label();
            backButton = new Button();
            root = new TableLayoutPanel();
            tools = new FlowLayoutPanel();
            channels = new CheckedListBox();
            addButton = new Button();
            readButton = new Button();
            removeButton = new Button();
            saveButton = new Button();
            points = new DataGridView();
            status = new Label();
            dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn5 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn6 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn7 = new DataGridViewTextBoxColumn();
            root.SuspendLayout();
            tools.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)points).BeginInit();
            SuspendLayout();
            //
            // root
            //
            root.ColumnCount = 1;
            root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            root.Controls.Add(header, 0, 0);
            root.Controls.Add(tools, 0, 1);
            root.Controls.Add(points, 0, 2);
            root.Controls.Add(status, 0, 3);
            root.Dock = DockStyle.Fill;
            root.Location = new Point(0, 0);
            root.Name = "root";
            root.Padding = new Padding(16);
            root.RowCount = 4;
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 110F));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 70F));
            root.Size = new Size(980, 600);
            root.TabIndex = 0;
            //
            // tools
            //
            header.Name = "header";
            header.Dock = DockStyle.Fill;
            header.Margin = new Padding(0);
            header.WrapContents = false;
            header.Controls.Add(title);
            header.Controls.Add(backButton);
            title.Name = "title";
            title.Text = "多传感器批量标定";
            title.Font = new Font("Microsoft YaHei UI", 16F, FontStyle.Bold);
            title.ForeColor = Color.FromArgb(15, 23, 42);
            title.Size = new Size(300, 36);
            title.Margin = new Padding(0, 0, 16, 0);
            backButton.Name = "backButton";
            backButton.Text = "返回单通道标定";
            backButton.Size = new Size(165, 36);
            backButton.FlatStyle = FlatStyle.Flat;
            backButton.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            backButton.BackColor = Color.FromArgb(248, 250, 252);
            backButton.ForeColor = Color.FromArgb(15, 23, 42);
            backButton.Click += BackButton_Click;
            tools.Controls.Add(channels);
            tools.Controls.Add(addButton);
            tools.Controls.Add(readButton);
            tools.Controls.Add(removeButton);
            tools.Controls.Add(saveButton);
            tools.Dock = DockStyle.Fill;
            tools.Location = new Point(19, 19);
            tools.Name = "tools";
            tools.Size = new Size(942, 104);
            tools.TabIndex = 0;
            //
            // channels
            //
            channels.CheckOnClick = true;
            channels.ColumnWidth = 130;
            channels.Items.AddRange(new object[] { "CH1", "CH2(待验证)", "CH3(待验证)", "CH4(待验证)", "CH5(待验证)", "CH6(待验证)" });
            channels.Location = new Point(3, 3);
            channels.MultiColumn = true;
            channels.Name = "channels";
            channels.Size = new Size(280, 94);
            channels.TabIndex = 0;
            //
            // addButton
            //
            addButton.Location = new Point(289, 3);
            addButton.Name = "addButton";
            addButton.Size = new Size(110, 32);
            addButton.TabIndex = 1;
            addButton.Text = "添加水位点";
            //
            // readButton
            //
            readButton.Location = new Point(405, 3);
            readButton.Name = "readButton";
            readButton.Size = new Size(130, 32);
            readButton.TabIndex = 2;
            readButton.Text = "读取最近原始值";
            //
            // removeButton
            //
            removeButton.Location = new Point(541, 3);
            removeButton.Name = "removeButton";
            removeButton.Size = new Size(110, 32);
            removeButton.TabIndex = 3;
            removeButton.Text = "删除所选点";
            //
            // saveButton
            //
            saveButton.BackColor = Color.FromArgb(29, 78, 216);
            saveButton.FlatStyle = FlatStyle.Flat;
            saveButton.ForeColor = Color.White;
            saveButton.Location = new Point(657, 3);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(130, 32);
            saveButton.TabIndex = 4;
            saveButton.Text = "计算并批量保存";
            saveButton.UseVisualStyleBackColor = false;
            //
            // points
            //
            points.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            points.BackgroundColor = Color.White;
            points.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn1, dataGridViewTextBoxColumn2, dataGridViewTextBoxColumn3, dataGridViewTextBoxColumn4, dataGridViewTextBoxColumn5, dataGridViewTextBoxColumn6, dataGridViewTextBoxColumn7 });
            points.Dock = DockStyle.Fill;
            points.Location = new Point(19, 129);
            points.Name = "points";
            points.Size = new Size(942, 382);
            points.TabIndex = 1;
            //
            // status
            //
            status.AutoEllipsis = true;
            status.Dock = DockStyle.Fill;
            status.Location = new Point(19, 514);
            status.Name = "status";
            status.Size = new Size(942, 70);
            status.TabIndex = 2;
            status.Text = "每行是同一个实际水位，各列填写独立通道的原始值。至少两个点，任何所选通道计算失败时均不保存。";
            //
            // dataGridViewTextBoxColumn1
            //
            dataGridViewTextBoxColumn1.HeaderText = "共同水位(mm)";
            dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            //
            // dataGridViewTextBoxColumn2
            //
            dataGridViewTextBoxColumn2.HeaderText = "CH1原始值";
            dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            //
            // dataGridViewTextBoxColumn3
            //
            dataGridViewTextBoxColumn3.HeaderText = "CH2原始值";
            dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            //
            // dataGridViewTextBoxColumn4
            //
            dataGridViewTextBoxColumn4.HeaderText = "CH3原始值";
            dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            //
            // dataGridViewTextBoxColumn5
            //
            dataGridViewTextBoxColumn5.HeaderText = "CH4原始值";
            dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            //
            // dataGridViewTextBoxColumn6
            //
            dataGridViewTextBoxColumn6.HeaderText = "CH5原始值";
            dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            //
            // dataGridViewTextBoxColumn7
            //
            dataGridViewTextBoxColumn7.HeaderText = "CH6原始值";
            dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            //
            // WaveBatchCalibrationPage
            //
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(241, 245, 249);
            Size = new Size(1104, 606);
            pagePanel.Name = "pagePanel";
            pagePanel.Dock = DockStyle.Fill;
            pagePanel.Controls.Add(root);
            Name = "WaveBatchCalibrationPage";
            Controls.Add(pagePanel);
            Font = new Font("Microsoft YaHei UI", 9F);
            AutoScroll = true;
            AutoScrollMinSize = new Size(860, 530);
            Text = "多传感器批量标定";
            root.ResumeLayout(false);
            tools.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)points).EndInit();
            ResumeLayout(false);
        }
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
    }
}
