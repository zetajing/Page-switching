namespace Page_switching
{
    partial class Mainpage
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        // 释放设计器创建的窗体组件。
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _autoPage?.Dispose();
                _manualPage?.Dispose();
                components?.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        // 创建设计器中的控件并设置布局和事件。
        private void InitializeComponent()
        {
            analysisButton = new Button();
            correctionButton = new Button();
            equipmentButton = new Button();
            panel1 = new Panel();
            headerStatusPanel = new Panel();
            controlStatusLabel = new Label();
            adsStatusLabel = new Label();
            appSubtitleLabel = new Label();
            appTitleLabel = new Label();
            panel2 = new Panel();
            systemSectionLabel = new Label();
            toolsSectionLabel = new Label();
            controlSectionLabel = new Label();
            bu_Configuration = new Button();
            button5 = new Button();
            button2 = new Button();
            Bu_data = new Button();
            Bu_Calibration = new Button();
            button3 = new Button();
            Bu_manual = new Button();
            Bu_auto = new Button();
            contentSplit = new SplitContainer();
            operationGroup = new Panel();
            operationList = new DataGridView();
            operationTimeColumn = new DataGridViewTextBoxColumn();
            operationPageColumn = new DataGridViewTextBoxColumn();
            operationMessageColumn = new DataGridViewTextBoxColumn();
            operationHeader = new Panel();
            operationTitle = new Label();
            operationCopyButton = new Button();
            operationFolderButton = new Button();
            operationAutoScroll = new CheckBox();
            operationPathLabel = new Label();
            navigationMarker = new Panel();
            panelswitch = new Panel();
            ((System.ComponentModel.ISupportInitialize)contentSplit).BeginInit();
            contentSplit.Panel1.SuspendLayout();
            contentSplit.Panel2.SuspendLayout();
            contentSplit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)operationList).BeginInit();
            operationHeader.SuspendLayout();
            panel1.SuspendLayout();
            headerStatusPanel.SuspendLayout();
            panel2.SuspendLayout();
            operationGroup.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(headerStatusPanel);
            panel1.Controls.Add(appSubtitleLabel);
            panel1.Controls.Add(appTitleLabel);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1280, 60);
            panel1.TabIndex = 0;
            // 
            // headerStatusPanel
            // 
            headerStatusPanel.BackColor = Color.White;
            headerStatusPanel.Controls.Add(controlStatusLabel);
            headerStatusPanel.Controls.Add(adsStatusLabel);
            headerStatusPanel.Dock = DockStyle.Right;
            headerStatusPanel.Location = new Point(1005, 0);
            headerStatusPanel.Name = "headerStatusPanel";
            headerStatusPanel.Padding = new Padding(12, 6, 20, 6);
            headerStatusPanel.Size = new Size(440, 60);
            headerStatusPanel.TabIndex = 2;
            // 
            // controlStatusLabel
            // 
            controlStatusLabel.Dock = DockStyle.Fill;
            controlStatusLabel.Font = new Font("Microsoft YaHei UI", 8F);
            controlStatusLabel.ForeColor = Color.FromArgb(100, 116, 139);
            controlStatusLabel.Location = new Point(10, 33);
            controlStatusLabel.Name = "controlStatusLabel";
            controlStatusLabel.AutoEllipsis = true;
            controlStatusLabel.Size = new Size(367, 25);
            controlStatusLabel.TabIndex = 1;
            controlStatusLabel.Text = "控制权：待配置    安全状态：待配置";
            controlStatusLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // adsStatusLabel
            // 
            adsStatusLabel.Dock = DockStyle.Top;
            adsStatusLabel.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
            adsStatusLabel.ForeColor = Color.FromArgb(154, 52, 18);
            adsStatusLabel.Location = new Point(10, 8);
            adsStatusLabel.Name = "adsStatusLabel";
            adsStatusLabel.Size = new Size(367, 25);
            adsStatusLabel.TabIndex = 0;
            adsStatusLabel.Text = "●  ADS 未连接";
            adsStatusLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // appSubtitleLabel
            // 
            appSubtitleLabel.AutoSize = true;
            appSubtitleLabel.Font = new Font("Microsoft YaHei UI", 9F);
            appSubtitleLabel.ForeColor = Color.FromArgb(100, 116, 139);
            appSubtitleLabel.Location = new Point(205, 23);
            appSubtitleLabel.Name = "appSubtitleLabel";
            appSubtitleLabel.Size = new Size(186, 19);
            appSubtitleLabel.TabIndex = 1;
            appSubtitleLabel.Text = "设备控制 / 波浪采集";
            // 
            // appTitleLabel
            // 
            appTitleLabel.AutoSize = true;
            appTitleLabel.Font = new Font("Microsoft YaHei UI", 14F, FontStyle.Bold);
            appTitleLabel.ForeColor = Color.FromArgb(15, 23, 42);
            appTitleLabel.Location = new Point(20, 15);
            appTitleLabel.Name = "appTitleLabel";
            appTitleLabel.Size = new Size(177, 36);
            appTitleLabel.TabIndex = 0;
            appTitleLabel.Text = "造波控制系统";
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(248, 250, 252);
            panel2.Controls.Add(systemSectionLabel);
            panel2.Controls.Add(toolsSectionLabel);
            panel2.Controls.Add(controlSectionLabel);
            panel2.Controls.Add(bu_Configuration);
            panel2.Controls.Add(button5);
            panel2.Controls.Add(button2);
            panel2.Controls.Add(Bu_data);
            panel2.Controls.Add(Bu_Calibration);
            panel2.Controls.Add(button3);
            panel2.Controls.Add(Bu_manual);
            panel2.Controls.Add(Bu_auto);
            panel2.Controls.Add(navigationMarker);
            panel2.Controls.Add(analysisButton);
            panel2.Controls.Add(correctionButton);
            panel2.Controls.Add(equipmentButton);
            panel2.Dock = DockStyle.Left;
            panel2.Location = new Point(0, 64);
            panel2.Name = "panel2";
            panel2.Padding = new Padding(12, 0, 12, 12);
            panel2.Size = new Size(176, 760);
            panel2.TabIndex = 1;
            // 
            // systemSectionLabel
            // 
            systemSectionLabel.Font = new Font("Microsoft YaHei UI", 8.5F);
            systemSectionLabel.ForeColor = Color.FromArgb(100, 116, 139);
            systemSectionLabel.Location = new Point(16, 544);
            systemSectionLabel.Name = "systemSectionLabel";
            systemSectionLabel.Size = new Size(154, 20);
            systemSectionLabel.TabIndex = 10;
            systemSectionLabel.Text = "系统";
            // 
            // toolsSectionLabel
            // 
            toolsSectionLabel.Font = new Font("Microsoft YaHei UI", 8.5F);
            toolsSectionLabel.ForeColor = Color.FromArgb(100, 116, 139);
            toolsSectionLabel.Location = new Point(16, 208);
            toolsSectionLabel.Name = "toolsSectionLabel";
            toolsSectionLabel.Size = new Size(154, 20);
            toolsSectionLabel.TabIndex = 9;
            toolsSectionLabel.Text = "数据工具";
            // 
            // controlSectionLabel
            // 
            controlSectionLabel.Font = new Font("Microsoft YaHei UI", 8.5F);
            controlSectionLabel.ForeColor = Color.FromArgb(100, 116, 139);
            controlSectionLabel.Location = new Point(16, 16);
            controlSectionLabel.Name = "controlSectionLabel";
            controlSectionLabel.Size = new Size(154, 20);
            controlSectionLabel.TabIndex = 8;
            controlSectionLabel.Text = "设备控制";
            // 
            // bu_Configuration
            // 
            bu_Configuration.BackColor = Color.FromArgb(248, 250, 252);
            bu_Configuration.Cursor = Cursors.Hand;
            bu_Configuration.FlatAppearance.BorderSize = 0;
            bu_Configuration.FlatStyle = FlatStyle.Flat;
            bu_Configuration.Font = new Font("Microsoft YaHei UI", 9.5F);
            bu_Configuration.ForeColor = Color.FromArgb(71, 85, 105);
            bu_Configuration.Location = new Point(12, 622);
            bu_Configuration.Name = "bu_Configuration";
            bu_Configuration.Padding = new Padding(12, 0, 0, 0);
            bu_Configuration.Size = new Size(152, 44);
            bu_Configuration.TabIndex = 7;
            bu_Configuration.Text = "系统配置";
            bu_Configuration.TextAlign = ContentAlignment.MiddleLeft;
            bu_Configuration.UseVisualStyleBackColor = false;
            bu_Configuration.Click += bu_Configuration_Click;
            // 
            // button5
            // 
            button5.BackColor = Color.FromArgb(248, 250, 252);
            button5.Cursor = Cursors.Hand;
            button5.FlatAppearance.BorderSize = 0;
            button5.FlatStyle = FlatStyle.Flat;
            button5.Font = new Font("Microsoft YaHei UI", 9.5F);
            button5.ForeColor = Color.FromArgb(71, 85, 105);
            button5.Location = new Point(12, 386);
            button5.Name = "button5";
            button5.Padding = new Padding(12, 0, 0, 0);
            button5.Size = new Size(152, 44);
            button5.TabIndex = 6;
            button5.Text = "浪高监测";
            button5.TextAlign = ContentAlignment.MiddleLeft;
            button5.UseVisualStyleBackColor = false;
            button5.Click += button5_Click;
            // 
            // button2
            // 
            button2.BackColor = Color.FromArgb(248, 250, 252);
            button2.Cursor = Cursors.Hand;
            button2.FlatAppearance.BorderSize = 0;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Font = new Font("Microsoft YaHei UI", 9.5F);
            button2.ForeColor = Color.FromArgb(71, 85, 105);
            button2.Location = new Point(12, 336);
            button2.Name = "button2";
            button2.Padding = new Padding(12, 0, 0, 0);
            button2.Size = new Size(152, 44);
            button2.TabIndex = 5;
            button2.Text = "波形生成";
            button2.TextAlign = ContentAlignment.MiddleLeft;
            button2.UseVisualStyleBackColor = false;
            button2.Click += WaveformButton_Click;
            // 
            // Bu_data
            // 
            Bu_data.BackColor = Color.FromArgb(248, 250, 252);
            Bu_data.Cursor = Cursors.Hand;
            Bu_data.FlatAppearance.BorderSize = 0;
            Bu_data.FlatStyle = FlatStyle.Flat;
            Bu_data.Font = new Font("Microsoft YaHei UI", 9.5F);
            Bu_data.ForeColor = Color.FromArgb(71, 85, 105);
            Bu_data.Location = new Point(12, 286);
            Bu_data.Name = "Bu_data";
            Bu_data.Padding = new Padding(12, 0, 0, 0);
            Bu_data.Size = new Size(152, 44);
            Bu_data.TabIndex = 4;
            Bu_data.Text = "数据管理";
            Bu_data.TextAlign = ContentAlignment.MiddleLeft;
            Bu_data.UseVisualStyleBackColor = false;
            Bu_data.Click += Bu_data_Click;
            // 
            // Bu_Calibration
            // 
            Bu_Calibration.BackColor = Color.FromArgb(248, 250, 252);
            Bu_Calibration.Cursor = Cursors.Hand;
            Bu_Calibration.FlatAppearance.BorderSize = 0;
            Bu_Calibration.FlatStyle = FlatStyle.Flat;
            Bu_Calibration.Font = new Font("Microsoft YaHei UI", 9.5F);
            Bu_Calibration.ForeColor = Color.FromArgb(71, 85, 105);
            Bu_Calibration.Location = new Point(12, 236);
            Bu_Calibration.Name = "Bu_Calibration";
            Bu_Calibration.Padding = new Padding(12, 0, 0, 0);
            Bu_Calibration.Size = new Size(152, 44);
            Bu_Calibration.TabIndex = 3;
            Bu_Calibration.Text = "标定管理";
            Bu_Calibration.TextAlign = ContentAlignment.MiddleLeft;
            Bu_Calibration.UseVisualStyleBackColor = false;
            Bu_Calibration.Click += Bu_Calibration_Click;
            // 
            // button3
            // 
            button3.BackColor = Color.FromArgb(248, 250, 252);
            button3.Cursor = Cursors.Hand;
            button3.FlatAppearance.BorderSize = 0;
            button3.FlatStyle = FlatStyle.Flat;
            button3.Font = new Font("Microsoft YaHei UI", 9.5F);
            button3.ForeColor = Color.FromArgb(71, 85, 105);
            button3.Location = new Point(12, 144);
            button3.Name = "button3";
            button3.Padding = new Padding(12, 0, 0, 0);
            button3.Size = new Size(152, 44);
            button3.TabIndex = 2;
            button3.Text = "控制权申请";
            button3.TextAlign = ContentAlignment.MiddleLeft;
            button3.UseVisualStyleBackColor = false;
            button3.Click += ControlAuthorityButton_Click;
            // 
            // Bu_manual
            // 
            Bu_manual.BackColor = Color.FromArgb(248, 250, 252);
            Bu_manual.Cursor = Cursors.Hand;
            Bu_manual.FlatAppearance.BorderSize = 0;
            Bu_manual.FlatStyle = FlatStyle.Flat;
            Bu_manual.Font = new Font("Microsoft YaHei UI", 9.5F);
            Bu_manual.ForeColor = Color.FromArgb(71, 85, 105);
            Bu_manual.Location = new Point(12, 94);
            Bu_manual.Name = "Bu_manual";
            Bu_manual.Padding = new Padding(12, 0, 0, 0);
            Bu_manual.Size = new Size(152, 44);
            Bu_manual.TabIndex = 1;
            Bu_manual.Text = "手动控制";
            Bu_manual.TextAlign = ContentAlignment.MiddleLeft;
            Bu_manual.UseVisualStyleBackColor = false;
            Bu_manual.Click += Bu_manual_Click;
            // 
            // Bu_auto
            // 
            Bu_auto.BackColor = Color.FromArgb(219, 234, 254);
            Bu_auto.Cursor = Cursors.Hand;
            Bu_auto.FlatAppearance.BorderSize = 0;
            Bu_auto.FlatStyle = FlatStyle.Flat;
            Bu_auto.Font = new Font("Microsoft YaHei UI", 9.5F);
            Bu_auto.ForeColor = Color.FromArgb(30, 64, 175);
            Bu_auto.Location = new Point(12, 44);
            Bu_auto.Name = "Bu_auto";
            Bu_auto.Padding = new Padding(12, 0, 0, 0);
            Bu_auto.Size = new Size(152, 44);
            Bu_auto.TabIndex = 0;
            Bu_auto.Text = "自动运行";
            Bu_auto.TextAlign = ContentAlignment.MiddleLeft;
            Bu_auto.UseVisualStyleBackColor = false;
            Bu_auto.Click += Bu_auto_Click;
            // 
            // panelswitch
            // 
            panelswitch.BackColor = Color.FromArgb(241, 245, 249);
            panelswitch.Dock = DockStyle.Fill;
            panelswitch.Location = new Point(0, 0);
            panelswitch.Name = "panelswitch";
            panelswitch.Size = new Size(1104, 608);
            panelswitch.TabIndex = 2;
            //
            // contentSplit
            //
            contentSplit.BackColor = Color.FromArgb(241, 245, 249);
            contentSplit.Dock = DockStyle.Fill;
            contentSplit.FixedPanel = FixedPanel.Panel2;
            contentSplit.Location = new Point(176, 60);
            contentSplit.Name = "contentSplit";
            contentSplit.Orientation = Orientation.Horizontal;
            contentSplit.Panel1.Controls.Add(panelswitch);
            contentSplit.Panel2.Controls.Add(operationGroup);
            contentSplit.Size = new Size(1104, 760);
            contentSplit.Panel1MinSize = 380;
            contentSplit.Panel2MinSize = 120;
            contentSplit.SplitterDistance = 608;
            contentSplit.SplitterWidth = 6;
            contentSplit.TabIndex = 2;
            //
            // navigationMarker
            //
            navigationMarker.BackColor = Color.FromArgb(29, 78, 216);
            navigationMarker.Location = new Point(0, 44);
            navigationMarker.Name = "navigationMarker";
            navigationMarker.Size = new Size(3, 44);
            navigationMarker.TabIndex = 11;
            //
            // operationGroup
            //
            operationGroup.BackColor = Color.White;
            operationGroup.Controls.Add(operationList);
            operationGroup.Controls.Add(operationPathLabel);
            operationGroup.Controls.Add(operationHeader);
            operationGroup.Dock = DockStyle.Fill;
            operationGroup.ForeColor = Color.FromArgb(15, 23, 42);
            operationGroup.Location = new Point(0, 0);
            operationGroup.Name = "operationGroup";
            operationGroup.Padding = new Padding(12, 0, 12, 4);
            operationGroup.Size = new Size(1104, 146);
            operationGroup.TabIndex = 3;
            //
            // operationHeader
            //
            operationHeader.Controls.Add(operationTitle);
            operationHeader.Controls.Add(operationAutoScroll);
            operationHeader.Controls.Add(operationCopyButton);
            operationHeader.Controls.Add(operationFolderButton);
            operationHeader.Dock = DockStyle.Top;
            operationHeader.Name = "operationHeader";
            operationHeader.Size = new Size(1080, 34);
            //
            // operationTitle
            //
            operationTitle.Dock = DockStyle.Fill;
            operationTitle.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
            operationTitle.Name = "operationTitle";
            operationTitle.Text = "操作记录（拖动上沿调整高度）";
            operationTitle.TextAlign = ContentAlignment.MiddleLeft;
            //
            // operationAutoScroll
            //
            operationAutoScroll.Checked = true;
            operationAutoScroll.CheckState = CheckState.Checked;
            operationAutoScroll.Dock = DockStyle.Right;
            operationAutoScroll.Name = "operationAutoScroll";
            operationAutoScroll.Size = new Size(92, 34);
            operationAutoScroll.Text = "自动滚动";
            operationAutoScroll.CheckedChanged += OperationAutoScroll_CheckedChanged;
            //
            // operationCopyButton
            //
            operationCopyButton.BackColor = Color.White;
            operationCopyButton.Dock = DockStyle.Right;
            operationCopyButton.FlatAppearance.BorderSize = 0;
            operationCopyButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(241, 245, 249);
            operationCopyButton.FlatStyle = FlatStyle.Flat;
            operationCopyButton.ForeColor = Color.FromArgb(71, 85, 105);
            operationCopyButton.Name = "operationCopyButton";
            operationCopyButton.Cursor = Cursors.Hand;
            operationCopyButton.Size = new Size(92, 34);
            operationCopyButton.Text = "复制选中";
            operationCopyButton.UseVisualStyleBackColor = false;
            operationCopyButton.Click += CopyOperationButton_Click;
            //
            // operationFolderButton
            //
            operationFolderButton.BackColor = Color.White;
            operationFolderButton.Dock = DockStyle.Right;
            operationFolderButton.FlatAppearance.BorderSize = 0;
            operationFolderButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(241, 245, 249);
            operationFolderButton.FlatStyle = FlatStyle.Flat;
            operationFolderButton.ForeColor = Color.FromArgb(71, 85, 105);
            operationFolderButton.Name = "operationFolderButton";
            operationFolderButton.Cursor = Cursors.Hand;
            operationFolderButton.Size = new Size(104, 34);
            operationFolderButton.Text = "打开日志目录";
            operationFolderButton.UseVisualStyleBackColor = false;
            operationFolderButton.Click += OpenOperationFolderButton_Click;
            //
            // operationList
            //
            operationList.AllowUserToAddRows = false;
            operationList.AllowUserToDeleteRows = false;
            operationList.AllowUserToResizeRows = false;
            operationList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            operationList.BackgroundColor = Color.White;
            operationList.BorderStyle = BorderStyle.None;
            operationList.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            operationList.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            operationList.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
            operationList.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(71, 85, 105);
            operationList.ColumnHeadersHeight = 28;
            operationList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            operationList.Columns.AddRange(operationTimeColumn, operationPageColumn, operationMessageColumn);
            operationList.DefaultCellStyle.SelectionBackColor = Color.FromArgb(219, 234, 254);
            operationList.DefaultCellStyle.SelectionForeColor = Color.FromArgb(15, 23, 42);
            operationList.Dock = DockStyle.Fill;
            operationList.EnableHeadersVisualStyles = false;
            operationList.Font = new Font("Microsoft YaHei UI", 8.5F);
            operationList.ForeColor = Color.FromArgb(15, 23, 42);
            operationList.GridColor = Color.FromArgb(226, 232, 240);
            operationList.Name = "operationList";
            operationList.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.FromArgb(71, 85, 105);
            operationList.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(248, 250, 252);
            operationList.ReadOnly = true;
            operationList.RowHeadersVisible = false;
            operationList.RowTemplate.Height = 26;
            operationList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            operationList.Size = new Size(1080, 85);
            operationList.TabIndex = 0;
            operationTimeColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            operationTimeColumn.HeaderText = "时间";
            operationTimeColumn.Name = "operationTimeColumn";
            operationTimeColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            operationTimeColumn.Width = 185;
            operationPageColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            operationPageColumn.HeaderText = "页面";
            operationPageColumn.Name = "operationPageColumn";
            operationPageColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            operationPageColumn.Width = 105;
            operationMessageColumn.HeaderText = "操作与结果";
            operationMessageColumn.Name = "operationMessageColumn";
            operationMessageColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            //
            // operationPathLabel
            //
            operationPathLabel.Dock = DockStyle.Bottom;
            operationPathLabel.AutoEllipsis = true;
            operationPathLabel.Font = new Font("Microsoft YaHei UI", 8F);
            operationPathLabel.ForeColor = Color.FromArgb(71, 85, 105);
            operationPathLabel.Location = new Point(10, 122);
            operationPathLabel.Name = "operationPathLabel";
            operationPathLabel.Size = new Size(1080, 23);
            operationPathLabel.TabIndex = 1;
            operationPathLabel.Text = "日志保存位置：";
            operationPathLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Mainpage
            // 
            BackColor = Color.FromArgb(241, 245, 249);
            ForeColor = Color.FromArgb(15, 23, 42);
            Font = new Font("Microsoft YaHei UI", 9F);
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(1280, 820);
            MinimumSize = new Size(1080, 720);
            panel2.AutoScroll = true;
            panel2.AutoScrollMinSize = new Size(0, 680);
            analysisButton.Name = "analysisButton";
            analysisButton.Text = "波浪分析";
            analysisButton.Location = new Point(12, 436);
            analysisButton.Size = new Size(145, 40);
            analysisButton.FlatStyle = FlatStyle.Flat;
            analysisButton.FlatAppearance.BorderSize = 0;
            analysisButton.TextAlign = ContentAlignment.MiddleLeft;
            analysisButton.BackColor = Color.FromArgb(248, 250, 252);
            analysisButton.ForeColor = Color.FromArgb(71, 85, 105);
            analysisButton.Click += AnalysisButton_Click;
            correctionButton.Name = "correctionButton";
            correctionButton.Text = "信号修正";
            correctionButton.Location = new Point(12, 486);
            correctionButton.Size = new Size(145, 40);
            correctionButton.FlatStyle = FlatStyle.Flat;
            correctionButton.FlatAppearance.BorderSize = 0;
            correctionButton.TextAlign = ContentAlignment.MiddleLeft;
            correctionButton.BackColor = Color.FromArgb(248, 250, 252);
            correctionButton.ForeColor = Color.FromArgb(71, 85, 105);
            correctionButton.Click += CorrectionButton_Click;
            equipmentButton.Name = "equipmentButton";
            equipmentButton.Text = "旧设备配置";
            equipmentButton.Location = new Point(12, 572);
            equipmentButton.Size = new Size(145, 40);
            equipmentButton.FlatStyle = FlatStyle.Flat;
            equipmentButton.FlatAppearance.BorderSize = 0;
            equipmentButton.TextAlign = ContentAlignment.MiddleLeft;
            equipmentButton.BackColor = Color.FromArgb(248, 250, 252);
            equipmentButton.ForeColor = Color.FromArgb(71, 85, 105);
            equipmentButton.Click += EquipmentButton_Click;
            Bu_auto.Width = 145;
            Bu_manual.Width = 145;
            button3.Width = 145;
            Bu_Calibration.Width = 145;
            Bu_data.Width = 145;
            button2.Width = 145;
            button5.Width = 145;
            bu_Configuration.Width = 145;
            Controls.Add(contentSplit);
            Controls.Add(panel2);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.Sizable;
            MaximizeBox = true;
            MinimizeBox = true;
            Name = "Mainpage";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "造波控制系统";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            headerStatusPanel.ResumeLayout(false);
            panel2.ResumeLayout(false);
            operationGroup.ResumeLayout(false);
            operationHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)operationList).EndInit();
            contentSplit.Panel1.ResumeLayout(false);
            contentSplit.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)contentSplit).EndInit();
            contentSplit.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Button analysisButton;
        private Button correctionButton;
        private Button equipmentButton;
        private Panel panel1;
        private Panel headerStatusPanel;
        private Panel panel2;
        private Panel panelswitch;
        private SplitContainer contentSplit;
        private Panel operationGroup;
        private DataGridView operationList;
        private DataGridViewTextBoxColumn operationTimeColumn;
        private DataGridViewTextBoxColumn operationPageColumn;
        private DataGridViewTextBoxColumn operationMessageColumn;
        private Panel operationHeader;
        private Label operationTitle;
        private Button operationCopyButton;
        private Button operationFolderButton;
        private CheckBox operationAutoScroll;
        private Panel navigationMarker;
        private Label operationPathLabel;
        private Button Bu_Calibration;
        private Button button3;
        private Button Bu_manual;
        private Button Bu_auto;
        private Button bu_Configuration;
        private Button button5;
        private Button button2;
        private Button Bu_data;
        private Label appTitleLabel;
        private Label appSubtitleLabel;
        private Label controlSectionLabel;
        private Label toolsSectionLabel;
        private Label systemSectionLabel;
        private Label adsStatusLabel;
        private Label controlStatusLabel;
    }
}
