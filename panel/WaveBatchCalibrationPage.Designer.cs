namespace Page_switching.panel
{

    partial class WaveBatchCalibrationPage
    {
        private Panel pagePanel;
        private FlowLayoutPanel header;
        private AntdUI.Label title;
        private AntdUI.Button backButton;

        private TableLayoutPanel root;
        private FlowLayoutPanel tools;
        private FlowLayoutPanel channels;
        private AntdUI.Checkbox channel1Check;
        private AntdUI.Checkbox channel2Check;
        private AntdUI.Checkbox channel3Check;
        private AntdUI.Checkbox channel4Check;
        private AntdUI.Checkbox channel5Check;
        private AntdUI.Checkbox channel6Check;
        private AntdUI.Button addButton;
        private AntdUI.Button readButton;
        private AntdUI.Button removeButton;
        private AntdUI.Button saveButton;
        private AntdUI.Table points;
        private AntdUI.Label status;

        private void InitializeComponent()
        {
            pagePanel = new Panel();
            header = new FlowLayoutPanel();
            title = new AntdUI.Label();
            backButton = new AntdUI.Button();
            backButton.Radius = 6;
            backButton.BorderWidth = 1F;
            root = new TableLayoutPanel();
            tools = new FlowLayoutPanel();
            channels = new FlowLayoutPanel();
            channel1Check = new AntdUI.Checkbox();
            channel2Check = new AntdUI.Checkbox();
            channel3Check = new AntdUI.Checkbox();
            channel4Check = new AntdUI.Checkbox();
            channel5Check = new AntdUI.Checkbox();
            channel6Check = new AntdUI.Checkbox();
            channel1Check.Name = "channel1Check";
            channel1Check.Text = "CH1";
            channel1Check.Size = new Size(130, 25);
            channels.Controls.Add(channel1Check);
            channel2Check.Name = "channel2Check";
            channel2Check.Text = "CH2(待验证)";
            channel2Check.Size = new Size(130, 25);
            channels.Controls.Add(channel2Check);
            channel3Check.Name = "channel3Check";
            channel3Check.Text = "CH3(待验证)";
            channel3Check.Size = new Size(130, 25);
            channels.Controls.Add(channel3Check);
            channel4Check.Name = "channel4Check";
            channel4Check.Text = "CH4(待验证)";
            channel4Check.Size = new Size(130, 25);
            channels.Controls.Add(channel4Check);
            channel5Check.Name = "channel5Check";
            channel5Check.Text = "CH5(待验证)";
            channel5Check.Size = new Size(130, 25);
            channels.Controls.Add(channel5Check);
            channel6Check.Name = "channel6Check";
            channel6Check.Text = "CH6(待验证)";
            channel6Check.Size = new Size(130, 25);
            channels.Controls.Add(channel6Check);
            channel1Check.Checked = true;
            addButton = new AntdUI.Button();
            addButton.Radius = 6;
            addButton.BorderWidth = 1F;
            readButton = new AntdUI.Button();
            readButton.Radius = 6;
            readButton.BorderWidth = 1F;
            removeButton = new AntdUI.Button();
            removeButton.Radius = 6;
            removeButton.BorderWidth = 1F;
            saveButton = new AntdUI.Button();
            saveButton.Radius = 6;
            saveButton.BorderWidth = 1F;
            points = new AntdUI.Table();
            points.EmptyHeader = true;
            points.Bordered = true;
            points.Radius = 6;
            points.BackColor = Color.White;
            status = new AntdUI.Label();
            root.SuspendLayout();
            tools.SuspendLayout();
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
            backButton.DefaultBorderColor = Color.FromArgb(203, 213, 225);
            backButton.BackColor = Color.FromArgb(248, 250, 252);
            backButton.DefaultBack = Color.FromArgb(248, 250, 252);
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
            channels.Location = new Point(3, 3);
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
            saveButton.DefaultBack = Color.FromArgb(29, 78, 216);
            saveButton.ForeColor = Color.White;
            saveButton.Location = new Point(657, 3);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(130, 32);
            saveButton.TabIndex = 4;
            saveButton.Text = "计算并批量保存";
            //
            // points
            //

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
            //
            // dataGridViewTextBoxColumn2
            //
            //
            // dataGridViewTextBoxColumn3
            //
            //
            // dataGridViewTextBoxColumn4
            //
            //
            // dataGridViewTextBoxColumn5
            //
            //
            // dataGridViewTextBoxColumn6
            //
            //
            // dataGridViewTextBoxColumn7
            //
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
            ResumeLayout(false);
        }
    }
}
