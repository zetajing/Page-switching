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
                // 直接 Dispose 也先取消监控，后台清理完成后才释放 Router。
                _ = BeginShutdown();
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
            panel1 = new Panel();
            headerStatusPanel = new Panel();
            controlStatusLabel = new Label();
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
            Bu_manual = new Button();
            Bu_auto = new Button();
            navigationMarker = new Panel();
            contentPanel = new Panel();
            panelswitch = new Panel();
            operationGroup = new Panel();
            operationPathLabel = new Label();
            operationFolderButton = new Button();
            adsStatusLabel = new Label();
            panel1.SuspendLayout();
            headerStatusPanel.SuspendLayout();
            panel2.SuspendLayout();
            contentPanel.SuspendLayout();
            operationGroup.SuspendLayout();
            SuspendLayout();
            // 
            // analysisButton
            // 
            analysisButton.BackColor = Color.White;
            analysisButton.Cursor = Cursors.Hand;
            analysisButton.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            analysisButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(226, 232, 240);
            analysisButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(219, 234, 254);
            analysisButton.FlatStyle = FlatStyle.Flat;
            analysisButton.Font = new Font("Microsoft YaHei UI", 9.5F, FontStyle.Bold);
            analysisButton.ForeColor = Color.FromArgb(15, 23, 42);
            analysisButton.Location = new Point(15, 482);
            analysisButton.Margin = new Padding(4, 4, 4, 4);
            analysisButton.Name = "analysisButton";
            analysisButton.Padding = new Padding(15, 0, 0, 0);
            analysisButton.Size = new Size(181, 55);
            analysisButton.TabIndex = 12;
            analysisButton.Text = "波浪分析";
            analysisButton.TextAlign = ContentAlignment.MiddleLeft;
            analysisButton.UseVisualStyleBackColor = false;
            analysisButton.Click += AnalysisButton_Click;
            // 
            // correctionButton
            // 
            correctionButton.BackColor = Color.White;
            correctionButton.Cursor = Cursors.Hand;
            correctionButton.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            correctionButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(226, 232, 240);
            correctionButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(219, 234, 254);
            correctionButton.FlatStyle = FlatStyle.Flat;
            correctionButton.Font = new Font("Microsoft YaHei UI", 9.5F, FontStyle.Bold);
            correctionButton.ForeColor = Color.FromArgb(15, 23, 42);
            correctionButton.Location = new Point(15, 545);
            correctionButton.Margin = new Padding(4, 4, 4, 4);
            correctionButton.Name = "correctionButton";
            correctionButton.Padding = new Padding(15, 0, 0, 0);
            correctionButton.Size = new Size(181, 55);
            correctionButton.TabIndex = 13;
            correctionButton.Text = "信号修正";
            correctionButton.TextAlign = ContentAlignment.MiddleLeft;
            correctionButton.UseVisualStyleBackColor = false;
            correctionButton.Click += CorrectionButton_Click;
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(headerStatusPanel);
            panel1.Controls.Add(appTitleLabel);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(4, 4, 4, 4);
            panel1.Name = "panel1";
            panel1.Size = new Size(1600, 75);
            panel1.TabIndex = 0;
            // 
            // headerStatusPanel
            // 
            headerStatusPanel.BackColor = Color.White;
            headerStatusPanel.Controls.Add(controlStatusLabel);
            headerStatusPanel.Controls.Add(adsStatusLabel);
            headerStatusPanel.Dock = DockStyle.Right;
            headerStatusPanel.Location = new Point(1173, 0);
            headerStatusPanel.Margin = new Padding(4, 4, 4, 4);
            headerStatusPanel.Name = "headerStatusPanel";
            headerStatusPanel.Padding = new Padding(15, 8, 25, 8);
            headerStatusPanel.Size = new Size(427, 75);
            headerStatusPanel.TabIndex = 2;
            // 
            // controlStatusLabel
            // 
            controlStatusLabel.AutoEllipsis = true;
            controlStatusLabel.Dock = DockStyle.Fill;
            controlStatusLabel.Font = new Font("Microsoft YaHei UI", 7.8F, FontStyle.Regular, GraphicsUnit.Point, 134);
            controlStatusLabel.ForeColor = Color.FromArgb(100, 116, 139);
            controlStatusLabel.Location = new Point(15, 39);
            controlStatusLabel.Margin = new Padding(4, 0, 4, 0);
            controlStatusLabel.Name = "controlStatusLabel";
            controlStatusLabel.Size = new Size(387, 28);
            controlStatusLabel.TabIndex = 1;
            controlStatusLabel.Text = "页面：自动运行    控制端：等待反馈    模式：等待反馈";
            controlStatusLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // appTitleLabel
            // 
            appTitleLabel.AutoSize = true;
            appTitleLabel.Font = new Font("Microsoft YaHei UI", 14F, FontStyle.Bold);
            appTitleLabel.ForeColor = Color.FromArgb(15, 23, 42);
            appTitleLabel.Location = new Point(25, 19);
            appTitleLabel.Margin = new Padding(4, 0, 4, 0);
            appTitleLabel.Name = "appTitleLabel";
            appTitleLabel.Size = new Size(158, 31);
            appTitleLabel.TabIndex = 0;
            appTitleLabel.Text = "造波控制系统";
            // 
            // panel2
            // 
            panel2.AutoScroll = true;
            panel2.AutoScrollMinSize = new Size(0, 580);
            panel2.BackColor = Color.FromArgb(248, 250, 252);
            panel2.Controls.Add(systemSectionLabel);
            panel2.Controls.Add(toolsSectionLabel);
            panel2.Controls.Add(controlSectionLabel);
            panel2.Controls.Add(bu_Configuration);
            panel2.Controls.Add(button5);
            panel2.Controls.Add(button2);
            panel2.Controls.Add(Bu_data);
            panel2.Controls.Add(Bu_Calibration);
            panel2.Controls.Add(Bu_manual);
            panel2.Controls.Add(Bu_auto);
            panel2.Controls.Add(navigationMarker);
            panel2.Controls.Add(analysisButton);
            panel2.Controls.Add(correctionButton);
            panel2.Dock = DockStyle.Left;
            panel2.Location = new Point(0, 75);
            panel2.Margin = new Padding(4, 4, 4, 4);
            panel2.Name = "panel2";
            panel2.Padding = new Padding(15, 0, 15, 15);
            panel2.Size = new Size(220, 950);
            panel2.TabIndex = 1;
            // 
            // systemSectionLabel
            // 
            systemSectionLabel.Font = new Font("Microsoft YaHei UI", 8.5F);
            systemSectionLabel.ForeColor = Color.FromArgb(100, 116, 139);
            systemSectionLabel.Location = new Point(36, 618);
            systemSectionLabel.Margin = new Padding(4, 0, 4, 0);
            systemSectionLabel.Name = "systemSectionLabel";
            systemSectionLabel.Size = new Size(169, 25);
            systemSectionLabel.TabIndex = 10;
            systemSectionLabel.Text = "系统";
            // 
            // toolsSectionLabel
            // 
            toolsSectionLabel.Font = new Font("Microsoft YaHei UI", 8.5F);
            toolsSectionLabel.ForeColor = Color.FromArgb(100, 116, 139);
            toolsSectionLabel.Location = new Point(36, 198);
            toolsSectionLabel.Margin = new Padding(4, 0, 4, 0);
            toolsSectionLabel.Name = "toolsSectionLabel";
            toolsSectionLabel.Size = new Size(169, 25);
            toolsSectionLabel.TabIndex = 9;
            toolsSectionLabel.Text = "数据工具";
            // 
            // controlSectionLabel
            // 
            controlSectionLabel.Font = new Font("Microsoft YaHei UI", 8.5F);
            controlSectionLabel.ForeColor = Color.FromArgb(100, 116, 139);
            controlSectionLabel.Location = new Point(36, 20);
            controlSectionLabel.Margin = new Padding(4, 0, 4, 0);
            controlSectionLabel.Name = "controlSectionLabel";
            controlSectionLabel.Size = new Size(169, 25);
            controlSectionLabel.TabIndex = 8;
            controlSectionLabel.Text = "设备控制";
            // 
            // bu_Configuration
            // 
            bu_Configuration.BackColor = Color.White;
            bu_Configuration.Cursor = Cursors.Hand;
            bu_Configuration.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            bu_Configuration.FlatAppearance.MouseDownBackColor = Color.FromArgb(226, 232, 240);
            bu_Configuration.FlatAppearance.MouseOverBackColor = Color.FromArgb(219, 234, 254);
            bu_Configuration.FlatStyle = FlatStyle.Flat;
            bu_Configuration.Font = new Font("Microsoft YaHei UI", 9.5F, FontStyle.Bold);
            bu_Configuration.ForeColor = Color.FromArgb(15, 23, 42);
            bu_Configuration.Location = new Point(15, 652);
            bu_Configuration.Margin = new Padding(4, 4, 4, 4);
            bu_Configuration.Name = "bu_Configuration";
            bu_Configuration.Padding = new Padding(15, 0, 0, 0);
            bu_Configuration.Size = new Size(181, 55);
            bu_Configuration.TabIndex = 7;
            bu_Configuration.Text = "系统配置";
            bu_Configuration.TextAlign = ContentAlignment.MiddleLeft;
            bu_Configuration.UseVisualStyleBackColor = false;
            bu_Configuration.Click += bu_Configuration_Click;
            // 
            // button5
            // 
            button5.BackColor = Color.White;
            button5.Cursor = Cursors.Hand;
            button5.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            button5.FlatAppearance.MouseDownBackColor = Color.FromArgb(226, 232, 240);
            button5.FlatAppearance.MouseOverBackColor = Color.FromArgb(219, 234, 254);
            button5.FlatStyle = FlatStyle.Flat;
            button5.Font = new Font("Microsoft YaHei UI", 9.5F, FontStyle.Bold);
            button5.ForeColor = Color.FromArgb(15, 23, 42);
            button5.Location = new Point(15, 420);
            button5.Margin = new Padding(4, 4, 4, 4);
            button5.Name = "button5";
            button5.Padding = new Padding(15, 0, 0, 0);
            button5.Size = new Size(181, 55);
            button5.TabIndex = 6;
            button5.Text = "浪高监测";
            button5.TextAlign = ContentAlignment.MiddleLeft;
            button5.UseVisualStyleBackColor = false;
            button5.Click += button5_Click;
            // 
            // button2
            // 
            button2.BackColor = Color.White;
            button2.Cursor = Cursors.Hand;
            button2.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            button2.FlatAppearance.MouseDownBackColor = Color.FromArgb(226, 232, 240);
            button2.FlatAppearance.MouseOverBackColor = Color.FromArgb(219, 234, 254);
            button2.FlatStyle = FlatStyle.Flat;
            button2.Font = new Font("Microsoft YaHei UI", 9.5F, FontStyle.Bold);
            button2.ForeColor = Color.FromArgb(15, 23, 42);
            button2.Location = new Point(15, 358);
            button2.Margin = new Padding(4, 4, 4, 4);
            button2.Name = "button2";
            button2.Padding = new Padding(15, 0, 0, 0);
            button2.Size = new Size(181, 55);
            button2.TabIndex = 5;
            button2.Text = "波形生成";
            button2.TextAlign = ContentAlignment.MiddleLeft;
            button2.UseVisualStyleBackColor = false;
            button2.Click += WaveformButton_Click;
            // 
            // Bu_data
            // 
            Bu_data.BackColor = Color.White;
            Bu_data.Cursor = Cursors.Hand;
            Bu_data.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            Bu_data.FlatAppearance.MouseDownBackColor = Color.FromArgb(226, 232, 240);
            Bu_data.FlatAppearance.MouseOverBackColor = Color.FromArgb(219, 234, 254);
            Bu_data.FlatStyle = FlatStyle.Flat;
            Bu_data.Font = new Font("Microsoft YaHei UI", 9.5F, FontStyle.Bold);
            Bu_data.ForeColor = Color.FromArgb(15, 23, 42);
            Bu_data.Location = new Point(15, 295);
            Bu_data.Margin = new Padding(4, 4, 4, 4);
            Bu_data.Name = "Bu_data";
            Bu_data.Padding = new Padding(15, 0, 0, 0);
            Bu_data.Size = new Size(181, 55);
            Bu_data.TabIndex = 4;
            Bu_data.Text = "数据管理";
            Bu_data.TextAlign = ContentAlignment.MiddleLeft;
            Bu_data.UseVisualStyleBackColor = false;
            Bu_data.Click += Bu_data_Click;
            // 
            // Bu_Calibration
            // 
            Bu_Calibration.BackColor = Color.White;
            Bu_Calibration.Cursor = Cursors.Hand;
            Bu_Calibration.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            Bu_Calibration.FlatAppearance.MouseDownBackColor = Color.FromArgb(226, 232, 240);
            Bu_Calibration.FlatAppearance.MouseOverBackColor = Color.FromArgb(219, 234, 254);
            Bu_Calibration.FlatStyle = FlatStyle.Flat;
            Bu_Calibration.Font = new Font("Microsoft YaHei UI", 9.5F, FontStyle.Bold);
            Bu_Calibration.ForeColor = Color.FromArgb(15, 23, 42);
            Bu_Calibration.Location = new Point(15, 232);
            Bu_Calibration.Margin = new Padding(4, 4, 4, 4);
            Bu_Calibration.Name = "Bu_Calibration";
            Bu_Calibration.Padding = new Padding(15, 0, 0, 0);
            Bu_Calibration.Size = new Size(181, 55);
            Bu_Calibration.TabIndex = 3;
            Bu_Calibration.Text = "标定管理";
            Bu_Calibration.TextAlign = ContentAlignment.MiddleLeft;
            Bu_Calibration.UseVisualStyleBackColor = false;
            Bu_Calibration.Click += Bu_Calibration_Click;
            // 
            // Bu_manual
            // 
            Bu_manual.BackColor = Color.White;
            Bu_manual.Cursor = Cursors.Hand;
            Bu_manual.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            Bu_manual.FlatAppearance.MouseDownBackColor = Color.FromArgb(226, 232, 240);
            Bu_manual.FlatAppearance.MouseOverBackColor = Color.FromArgb(219, 234, 254);
            Bu_manual.FlatStyle = FlatStyle.Flat;
            Bu_manual.Font = new Font("Microsoft YaHei UI", 9.5F, FontStyle.Bold);
            Bu_manual.ForeColor = Color.FromArgb(15, 23, 42);
            Bu_manual.Location = new Point(15, 118);
            Bu_manual.Margin = new Padding(4, 4, 4, 4);
            Bu_manual.Name = "Bu_manual";
            Bu_manual.Padding = new Padding(15, 0, 0, 0);
            Bu_manual.Size = new Size(181, 55);
            Bu_manual.TabIndex = 1;
            Bu_manual.Text = "手动控制";
            Bu_manual.TextAlign = ContentAlignment.MiddleLeft;
            Bu_manual.UseVisualStyleBackColor = false;
            Bu_manual.Click += Bu_manual_Click;
            // 
            // Bu_auto
            // 
            Bu_auto.BackColor = Color.FromArgb(29, 78, 216);
            Bu_auto.Cursor = Cursors.Hand;
            Bu_auto.FlatAppearance.BorderColor = Color.FromArgb(29, 78, 216);
            Bu_auto.FlatAppearance.MouseDownBackColor = Color.FromArgb(30, 64, 175);
            Bu_auto.FlatAppearance.MouseOverBackColor = Color.FromArgb(30, 64, 175);
            Bu_auto.FlatStyle = FlatStyle.Flat;
            Bu_auto.Font = new Font("Microsoft YaHei UI", 9.5F, FontStyle.Bold);
            Bu_auto.ForeColor = Color.White;
            Bu_auto.Location = new Point(15, 55);
            Bu_auto.Margin = new Padding(4, 4, 4, 4);
            Bu_auto.Name = "Bu_auto";
            Bu_auto.Padding = new Padding(15, 0, 0, 0);
            Bu_auto.Size = new Size(181, 55);
            Bu_auto.TabIndex = 0;
            Bu_auto.Text = "自动运行";
            Bu_auto.TextAlign = ContentAlignment.MiddleLeft;
            Bu_auto.UseVisualStyleBackColor = false;
            Bu_auto.Click += Bu_auto_Click;
            // 
            // navigationMarker
            // 
            navigationMarker.BackColor = Color.FromArgb(29, 78, 216);
            navigationMarker.Location = new Point(0, 55);
            navigationMarker.Margin = new Padding(4, 4, 4, 4);
            navigationMarker.Name = "navigationMarker";
            navigationMarker.Size = new Size(4, 55);
            navigationMarker.TabIndex = 11;
            // 
            // contentPanel
            // 
            contentPanel.BackColor = Color.FromArgb(241, 245, 249);
            contentPanel.Controls.Add(panelswitch);
            contentPanel.Controls.Add(operationGroup);
            contentPanel.Dock = DockStyle.Fill;
            contentPanel.Location = new Point(220, 75);
            contentPanel.Margin = new Padding(4, 4, 4, 4);
            contentPanel.Name = "contentPanel";
            contentPanel.Size = new Size(1380, 950);
            contentPanel.TabIndex = 2;
            // 
            // panelswitch
            // 
            panelswitch.BackColor = Color.FromArgb(241, 245, 249);
            panelswitch.Dock = DockStyle.Fill;
            panelswitch.Location = new Point(0, 0);
            panelswitch.Margin = new Padding(4, 4, 4, 4);
            panelswitch.Name = "panelswitch";
            panelswitch.Size = new Size(1380, 908);
            panelswitch.TabIndex = 2;
            // 
            // operationGroup
            // 
            operationGroup.BackColor = Color.White;
            operationGroup.Controls.Add(operationPathLabel);
            operationGroup.Controls.Add(operationFolderButton);
            operationGroup.Dock = DockStyle.Bottom;
            operationGroup.ForeColor = Color.FromArgb(15, 23, 42);
            operationGroup.Location = new Point(0, 908);
            operationGroup.Margin = new Padding(4, 4, 4, 4);
            operationGroup.Name = "operationGroup";
            operationGroup.Padding = new Padding(15, 0, 15, 0);
            operationGroup.Size = new Size(1380, 42);
            operationGroup.TabIndex = 3;
            // 
            // operationPathLabel
            // 
            operationPathLabel.AutoEllipsis = true;
            operationPathLabel.Dock = DockStyle.Fill;
            operationPathLabel.Font = new Font("Microsoft YaHei UI", 8F);
            operationPathLabel.ForeColor = Color.FromArgb(71, 85, 105);
            operationPathLabel.Location = new Point(15, 0);
            operationPathLabel.Margin = new Padding(4, 0, 4, 0);
            operationPathLabel.Name = "operationPathLabel";
            operationPathLabel.Size = new Size(1220, 42);
            operationPathLabel.TabIndex = 1;
            operationPathLabel.Text = "日志保存位置：";
            operationPathLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // operationFolderButton
            // 
            operationFolderButton.BackColor = Color.White;
            operationFolderButton.Cursor = Cursors.Hand;
            operationFolderButton.Dock = DockStyle.Right;
            operationFolderButton.FlatAppearance.BorderSize = 0;
            operationFolderButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(241, 245, 249);
            operationFolderButton.FlatStyle = FlatStyle.Flat;
            operationFolderButton.ForeColor = Color.FromArgb(71, 85, 105);
            operationFolderButton.Location = new Point(1235, 0);
            operationFolderButton.Margin = new Padding(4, 4, 4, 4);
            operationFolderButton.Name = "operationFolderButton";
            operationFolderButton.Size = new Size(130, 42);
            operationFolderButton.TabIndex = 2;
            operationFolderButton.Text = "打开日志目录";
            operationFolderButton.UseVisualStyleBackColor = false;
            operationFolderButton.Click += OpenOperationFolderButton_Click;
            // 
            // adsStatusLabel
            // 
            adsStatusLabel.Dock = DockStyle.Top;
            adsStatusLabel.Font = new Font("Microsoft YaHei UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 134);
            adsStatusLabel.ForeColor = Color.FromArgb(154, 52, 18);
            adsStatusLabel.Location = new Point(15, 8);
            adsStatusLabel.Margin = new Padding(4, 0, 4, 0);
            adsStatusLabel.Name = "adsStatusLabel";
            adsStatusLabel.Size = new Size(387, 31);
            adsStatusLabel.TabIndex = 0;
            adsStatusLabel.Text = "● ADS：手动未连接 / 监控未连接";
            adsStatusLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // Mainpage
            // 
            AutoScaleDimensions = new SizeF(120F, 120F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(241, 245, 249);
            ClientSize = new Size(1600, 1025);
            Controls.Add(contentPanel);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Font = new Font("Microsoft YaHei UI", 9F);
            ForeColor = Color.FromArgb(15, 23, 42);
            Margin = new Padding(4, 4, 4, 4);
            MinimumSize = new Size(1346, 888);
            Name = "Mainpage";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "造波控制系统";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            headerStatusPanel.ResumeLayout(false);
            panel2.ResumeLayout(false);
            contentPanel.ResumeLayout(false);
            operationGroup.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Button analysisButton;
        private Button correctionButton;
        private Panel panel1;
        private Panel headerStatusPanel;
        private Panel panel2;
        private Panel panelswitch;
        private Panel contentPanel;
        private Panel operationGroup;
        private Button operationFolderButton;
        private Panel navigationMarker;
        private Label operationPathLabel;
        private Button Bu_Calibration;
        private Button Bu_manual;
        private Button Bu_auto;
        private Button bu_Configuration;
        private Button button5;
        private Button button2;
        private Button Bu_data;
        private Label appTitleLabel;
        private Label controlSectionLabel;
        private Label toolsSectionLabel;
        private Label systemSectionLabel;
        private Label controlStatusLabel;
        private Label adsStatusLabel;
    }
}
