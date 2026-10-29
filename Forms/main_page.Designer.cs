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
        // 可见按钮、标题和状态文字统一使用 AntdUI；滚动容器及页面承载面板保留 WinForms 布局。
        private void InitializeComponent()
        {
            analysisButton = new AntdUI.Button();
            correctionButton = new AntdUI.Button();
            controlAuthorityButton = new AntdUI.Button();
            diagnosticsButton = new AntdUI.Button();
            panel1 = new AntdUI.Panel();
            headerStatusPanel = new AntdUI.Panel();
            accountPanel = new Panel();
            currentUserLabel = new AntdUI.Label();
            manageAccountsButton = new AntdUI.Button();
            controlStatusLabel = new AntdUI.Label();
            adsStatusLabel = new AntdUI.Label();
            appTitleLabel = new AntdUI.Label();
            panel2 = new Panel();
            systemSectionLabel = new AntdUI.Label();
            toolsSectionLabel = new AntdUI.Label();
            controlSectionLabel = new AntdUI.Label();
            bu_Configuration = new AntdUI.Button();
            button5 = new AntdUI.Button();
            button2 = new AntdUI.Button();
            Bu_data = new AntdUI.Button();
            Bu_Calibration = new AntdUI.Button();
            Bu_manual = new AntdUI.Button();
            Bu_auto = new AntdUI.Button();
            navigationMarker = new Panel();
            contentPanel = new Panel();
            panelswitch = new Panel();
            operationGroup = new AntdUI.Panel();
            operationPathLabel = new AntdUI.Label();
            operationFolderButton = new AntdUI.Button();
            panel1.SuspendLayout();
            headerStatusPanel.SuspendLayout();
            panel2.SuspendLayout();
            contentPanel.SuspendLayout();
            operationGroup.SuspendLayout();
            SuspendLayout();
            // 
            // analysisButton
            // 
            analysisButton.BackActive = Color.FromArgb(226, 232, 240);
            analysisButton.BackColor = Color.White;
            analysisButton.BackHover = Color.FromArgb(219, 234, 254);
            analysisButton.BorderWidth = 1F;
            analysisButton.Cursor = Cursors.Hand;
            analysisButton.DefaultBack = Color.White;
            analysisButton.DefaultBorderColor = Color.FromArgb(203, 213, 225);
            analysisButton.Font = new Font("Microsoft YaHei UI", 10.5F);
            analysisButton.ForeColor = Color.FromArgb(15, 23, 42);
            analysisButton.Location = new Point(10, 476);
            analysisButton.Margin = new Padding(4);
            analysisButton.Name = "analysisButton";
            analysisButton.Padding = new Padding(15, 0, 0, 0);
            analysisButton.Size = new Size(180, 50);
            analysisButton.TabIndex = 12;
            analysisButton.Text = "波浪分析";
            analysisButton.Click += AnalysisButton_Click;
            // 
            // correctionButton
            // 
            correctionButton.BackActive = Color.FromArgb(226, 232, 240);
            correctionButton.BackColor = Color.White;
            correctionButton.BackHover = Color.FromArgb(219, 234, 254);
            correctionButton.BorderWidth = 1F;
            correctionButton.Cursor = Cursors.Hand;
            correctionButton.DefaultBack = Color.White;
            correctionButton.DefaultBorderColor = Color.FromArgb(203, 213, 225);
            correctionButton.Font = new Font("Microsoft YaHei UI", 10.5F);
            correctionButton.ForeColor = Color.FromArgb(15, 23, 42);
            correctionButton.Location = new Point(10, 530);
            correctionButton.Margin = new Padding(4);
            correctionButton.Name = "correctionButton";
            correctionButton.Padding = new Padding(15, 0, 0, 0);
            correctionButton.Size = new Size(180, 50);
            correctionButton.TabIndex = 13;
            correctionButton.Text = "信号修正";
            correctionButton.Click += CorrectionButton_Click;
            // 
            // controlAuthorityButton
            // 
            controlAuthorityButton.BackActive = Color.FromArgb(226, 232, 240);
            controlAuthorityButton.BackColor = Color.White;
            controlAuthorityButton.BackHover = Color.FromArgb(219, 234, 254);
            controlAuthorityButton.BorderWidth = 1F;
            controlAuthorityButton.Cursor = Cursors.Hand;
            controlAuthorityButton.DefaultBack = Color.White;
            controlAuthorityButton.DefaultBorderColor = Color.FromArgb(203, 213, 225);
            controlAuthorityButton.Font = new Font("Microsoft YaHei UI", 10.5F);
            controlAuthorityButton.ForeColor = Color.FromArgb(15, 23, 42);
            controlAuthorityButton.Location = new Point(10, 160);
            controlAuthorityButton.Margin = new Padding(4);
            controlAuthorityButton.Name = "controlAuthorityButton";
            controlAuthorityButton.Padding = new Padding(15, 0, 0, 0);
            controlAuthorityButton.Size = new Size(180, 50);
            controlAuthorityButton.TabIndex = 14;
            controlAuthorityButton.Text = "控制权申请";
            controlAuthorityButton.Click += ControlAuthorityButton_Click;
            //
            // diagnosticsButton
            //
            diagnosticsButton.BackActive = Color.FromArgb(226, 232, 240);
            diagnosticsButton.BackColor = Color.White;
            diagnosticsButton.BackHover = Color.FromArgb(219, 234, 254);
            diagnosticsButton.BorderWidth = 1F;
            diagnosticsButton.Cursor = Cursors.Hand;
            diagnosticsButton.DefaultBack = Color.White;
            diagnosticsButton.DefaultBorderColor = Color.FromArgb(203, 213, 225);
            diagnosticsButton.Font = new Font("Microsoft YaHei UI", 10.5F);
            diagnosticsButton.ForeColor = Color.FromArgb(15, 23, 42);
            diagnosticsButton.Location = new Point(10, 630);
            diagnosticsButton.Margin = new Padding(4);
            diagnosticsButton.Name = "diagnosticsButton";
            diagnosticsButton.Padding = new Padding(15, 0, 0, 0);
            diagnosticsButton.Size = new Size(180, 50);
            diagnosticsButton.TabIndex = 15;
            diagnosticsButton.Text = "运行诊断";
            diagnosticsButton.Click += DiagnosticsButton_Click;
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(headerStatusPanel);
            panel1.Controls.Add(accountPanel);
            panel1.Controls.Add(appTitleLabel);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(4);
            panel1.Name = "panel1";
            panel1.Radius = 0;
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
            headerStatusPanel.Margin = new Padding(4);
            headerStatusPanel.Name = "headerStatusPanel";
            headerStatusPanel.Padding = new Padding(15, 8, 25, 8);
            headerStatusPanel.Radius = 0;
            headerStatusPanel.Size = new Size(427, 75);
            headerStatusPanel.TabIndex = 2;
            // 登录账号与管理入口独立于 PLC 控制端状态。
            accountPanel.BackColor = Color.White;
            accountPanel.Controls.Add(currentUserLabel);
            accountPanel.Controls.Add(manageAccountsButton);
            accountPanel.Dock = DockStyle.Right;
            accountPanel.Name = "accountPanel";
            accountPanel.Padding = new Padding(10, 14, 10, 14);
            accountPanel.Size = new Size(340, 75);
            currentUserLabel.AutoEllipsis = true;
            currentUserLabel.Dock = DockStyle.Fill;
            currentUserLabel.Font = new Font("Microsoft YaHei UI", 9F);
            currentUserLabel.Name = "currentUserLabel";
            currentUserLabel.Text = "尚未登录";
            manageAccountsButton.Dock = DockStyle.Right;
            manageAccountsButton.Name = "manageAccountsButton";
            manageAccountsButton.Size = new Size(100, 47);
            manageAccountsButton.Text = "账号管理";
            manageAccountsButton.Click += ManageAccountsButton_Click;
            // 
            // controlStatusLabel
            // 
            controlStatusLabel.AutoEllipsis = true;
            controlStatusLabel.Dock = DockStyle.Fill;
            controlStatusLabel.Font = new Font("Microsoft YaHei UI", 9F);
            controlStatusLabel.ForeColor = Color.FromArgb(100, 116, 139);
            controlStatusLabel.Location = new Point(15, 39);
            controlStatusLabel.Margin = new Padding(4, 0, 4, 0);
            controlStatusLabel.Name = "controlStatusLabel";
            controlStatusLabel.Size = new Size(387, 28);
            controlStatusLabel.TabIndex = 1;
            controlStatusLabel.Text = "页面：自动运行    控制端：等待反馈    模式：等待反馈";
            controlStatusLabel.TextAlign = ContentAlignment.MiddleRight;
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
            // appTitleLabel
            // 
            appTitleLabel.AutoSizeMode = AntdUI.TAutoSize.Auto;
            appTitleLabel.Font = new Font("Microsoft YaHei UI", 15F, FontStyle.Bold);
            appTitleLabel.ForeColor = Color.FromArgb(15, 23, 42);
            appTitleLabel.Location = new Point(25, 19);
            appTitleLabel.Margin = new Padding(4, 0, 4, 0);
            appTitleLabel.Name = "appTitleLabel";
            appTitleLabel.Size = new Size(139, 26);
            appTitleLabel.TabIndex = 0;
            appTitleLabel.Text = "造波控制系统";
            // 
            // panel2
            // 
            panel2.AutoScroll = true;
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
            panel2.Controls.Add(controlAuthorityButton);
            panel2.Controls.Add(diagnosticsButton);
            panel2.Dock = DockStyle.Left;
            panel2.Location = new Point(0, 75);
            panel2.Margin = new Padding(4);
            panel2.Name = "panel2";
            panel2.Padding = new Padding(15, 0, 15, 15);
            panel2.Size = new Size(220, 950);
            panel2.TabIndex = 1;
            // 
            // systemSectionLabel
            // 
            systemSectionLabel.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
            systemSectionLabel.ForeColor = Color.FromArgb(100, 116, 139);
            systemSectionLabel.Location = new Point(12, 594);
            systemSectionLabel.Margin = new Padding(4, 0, 4, 0);
            systemSectionLabel.Name = "systemSectionLabel";
            systemSectionLabel.Size = new Size(180, 26);
            systemSectionLabel.TabIndex = 10;
            systemSectionLabel.Text = "系统";
            systemSectionLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // toolsSectionLabel
            // 
            toolsSectionLabel.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
            toolsSectionLabel.ForeColor = Color.FromArgb(100, 116, 139);
            toolsSectionLabel.Location = new Point(12, 224);
            toolsSectionLabel.Margin = new Padding(4, 0, 4, 0);
            toolsSectionLabel.Name = "toolsSectionLabel";
            toolsSectionLabel.Size = new Size(180, 26);
            toolsSectionLabel.TabIndex = 9;
            toolsSectionLabel.Text = "数据工具";
            toolsSectionLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // controlSectionLabel
            // 
            controlSectionLabel.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
            controlSectionLabel.ForeColor = Color.FromArgb(100, 116, 139);
            controlSectionLabel.Location = new Point(12, 14);
            controlSectionLabel.Margin = new Padding(4, 0, 4, 0);
            controlSectionLabel.Name = "controlSectionLabel";
            controlSectionLabel.Size = new Size(180, 26);
            controlSectionLabel.TabIndex = 8;
            controlSectionLabel.Text = "设备控制";
            controlSectionLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // bu_Configuration
            // 
            bu_Configuration.BackActive = Color.FromArgb(226, 232, 240);
            bu_Configuration.BackColor = Color.White;
            bu_Configuration.BackHover = Color.FromArgb(219, 234, 254);
            bu_Configuration.BorderWidth = 1F;
            bu_Configuration.Cursor = Cursors.Hand;
            bu_Configuration.DefaultBack = Color.White;
            bu_Configuration.DefaultBorderColor = Color.FromArgb(203, 213, 225);
            bu_Configuration.Font = new Font("Microsoft YaHei UI", 10.5F);
            bu_Configuration.ForeColor = Color.FromArgb(15, 23, 42);
            bu_Configuration.Location = new Point(10, 684);
            bu_Configuration.Margin = new Padding(4);
            bu_Configuration.Name = "bu_Configuration";
            bu_Configuration.Padding = new Padding(15, 0, 0, 0);
            bu_Configuration.Size = new Size(180, 50);
            bu_Configuration.TabIndex = 7;
            bu_Configuration.Text = "系统配置";
            bu_Configuration.Click += bu_Configuration_Click;
            // 
            // button5
            // 
            button5.BackActive = Color.FromArgb(226, 232, 240);
            button5.BackColor = Color.White;
            button5.BackHover = Color.FromArgb(219, 234, 254);
            button5.BorderWidth = 1F;
            button5.Cursor = Cursors.Hand;
            button5.DefaultBack = Color.White;
            button5.DefaultBorderColor = Color.FromArgb(203, 213, 225);
            button5.Font = new Font("Microsoft YaHei UI", 10.5F);
            button5.ForeColor = Color.FromArgb(15, 23, 42);
            button5.Location = new Point(10, 422);
            button5.Margin = new Padding(4);
            button5.Name = "button5";
            button5.Padding = new Padding(15, 0, 0, 0);
            button5.Size = new Size(180, 50);
            button5.TabIndex = 6;
            button5.Text = "浪高监测";
            button5.Click += button5_Click;
            // 
            // button2
            // 
            button2.BackActive = Color.FromArgb(226, 232, 240);
            button2.BackColor = Color.White;
            button2.BackHover = Color.FromArgb(219, 234, 254);
            button2.BorderWidth = 1F;
            button2.Cursor = Cursors.Hand;
            button2.DefaultBack = Color.White;
            button2.DefaultBorderColor = Color.FromArgb(203, 213, 225);
            button2.Font = new Font("Microsoft YaHei UI", 10.5F);
            button2.ForeColor = Color.FromArgb(15, 23, 42);
            button2.Location = new Point(10, 368);
            button2.Margin = new Padding(4);
            button2.Name = "button2";
            button2.Padding = new Padding(15, 0, 0, 0);
            button2.Size = new Size(180, 50);
            button2.TabIndex = 5;
            button2.Text = "波形生成";
            button2.Click += WaveformButton_Click;
            // 
            // Bu_data
            // 
            Bu_data.BackActive = Color.FromArgb(226, 232, 240);
            Bu_data.BackColor = Color.White;
            Bu_data.BackHover = Color.FromArgb(219, 234, 254);
            Bu_data.BorderWidth = 1F;
            Bu_data.Cursor = Cursors.Hand;
            Bu_data.DefaultBack = Color.White;
            Bu_data.DefaultBorderColor = Color.FromArgb(203, 213, 225);
            Bu_data.Font = new Font("Microsoft YaHei UI", 10.5F);
            Bu_data.ForeColor = Color.FromArgb(15, 23, 42);
            Bu_data.Location = new Point(10, 314);
            Bu_data.Margin = new Padding(4);
            Bu_data.Name = "Bu_data";
            Bu_data.Padding = new Padding(15, 0, 0, 0);
            Bu_data.Size = new Size(180, 50);
            Bu_data.TabIndex = 4;
            Bu_data.Text = "数据管理";
            Bu_data.Click += Bu_data_Click;
            // 
            // Bu_Calibration
            // 
            Bu_Calibration.BackActive = Color.FromArgb(226, 232, 240);
            Bu_Calibration.BackColor = Color.White;
            Bu_Calibration.BackHover = Color.FromArgb(219, 234, 254);
            Bu_Calibration.BorderWidth = 1F;
            Bu_Calibration.Cursor = Cursors.Hand;
            Bu_Calibration.DefaultBack = Color.White;
            Bu_Calibration.DefaultBorderColor = Color.FromArgb(203, 213, 225);
            Bu_Calibration.Font = new Font("Microsoft YaHei UI", 10.5F);
            Bu_Calibration.ForeColor = Color.FromArgb(15, 23, 42);
            Bu_Calibration.Location = new Point(10, 260);
            Bu_Calibration.Margin = new Padding(4);
            Bu_Calibration.Name = "Bu_Calibration";
            Bu_Calibration.Padding = new Padding(15, 0, 0, 0);
            Bu_Calibration.Size = new Size(180, 50);
            Bu_Calibration.TabIndex = 3;
            Bu_Calibration.Text = "标定管理";
            Bu_Calibration.Click += Bu_Calibration_Click;
            // 
            // Bu_manual
            // 
            Bu_manual.BackActive = Color.FromArgb(226, 232, 240);
            Bu_manual.BackColor = Color.White;
            Bu_manual.BackHover = Color.FromArgb(219, 234, 254);
            Bu_manual.BorderWidth = 1F;
            Bu_manual.Cursor = Cursors.Hand;
            Bu_manual.DefaultBack = Color.White;
            Bu_manual.DefaultBorderColor = Color.FromArgb(203, 213, 225);
            Bu_manual.Font = new Font("Microsoft YaHei UI", 10.5F);
            Bu_manual.ForeColor = Color.FromArgb(15, 23, 42);
            Bu_manual.Location = new Point(10, 106);
            Bu_manual.Margin = new Padding(4);
            Bu_manual.Name = "Bu_manual";
            Bu_manual.Padding = new Padding(15, 0, 0, 0);
            Bu_manual.Size = new Size(180, 50);
            Bu_manual.TabIndex = 1;
            Bu_manual.Text = "手动控制";
            Bu_manual.Click += Bu_manual_Click;
            // 
            // Bu_auto
            // 
            Bu_auto.BackActive = Color.FromArgb(30, 64, 175);
            Bu_auto.BackColor = Color.FromArgb(29, 78, 216);
            Bu_auto.BackHover = Color.FromArgb(30, 64, 175);
            Bu_auto.BorderWidth = 1F;
            Bu_auto.Cursor = Cursors.Hand;
            Bu_auto.DefaultBack = Color.FromArgb(29, 78, 216);
            Bu_auto.DefaultBorderColor = Color.FromArgb(29, 78, 216);
            Bu_auto.Font = new Font("Microsoft YaHei UI", 10.5F);
            Bu_auto.ForeColor = Color.White;
            Bu_auto.Location = new Point(10, 52);
            Bu_auto.Margin = new Padding(4);
            Bu_auto.Name = "Bu_auto";
            Bu_auto.Padding = new Padding(15, 0, 0, 0);
            Bu_auto.Size = new Size(180, 50);
            Bu_auto.TabIndex = 0;
            Bu_auto.Text = "自动运行";
            Bu_auto.Click += Bu_auto_Click;
            // 
            // navigationMarker
            // 
            navigationMarker.BackColor = Color.FromArgb(29, 78, 216);
            navigationMarker.Location = new Point(0, 52);
            navigationMarker.Margin = new Padding(4);
            navigationMarker.Name = "navigationMarker";
            navigationMarker.Size = new Size(4, 50);
            navigationMarker.TabIndex = 11;
            // 
            // contentPanel
            // 
            contentPanel.BackColor = Color.FromArgb(241, 245, 249);
            contentPanel.Controls.Add(panelswitch);
            contentPanel.Controls.Add(operationGroup);
            contentPanel.Dock = DockStyle.Fill;
            contentPanel.Location = new Point(220, 75);
            contentPanel.Margin = new Padding(4);
            contentPanel.Name = "contentPanel";
            contentPanel.Size = new Size(1380, 950);
            contentPanel.TabIndex = 2;
            // 
            // panelswitch
            // 
            panelswitch.BackColor = Color.FromArgb(241, 245, 249);
            panelswitch.Dock = DockStyle.Fill;
            panelswitch.Location = new Point(0, 0);
            panelswitch.Margin = new Padding(4);
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
            operationGroup.Margin = new Padding(4);
            operationGroup.Name = "operationGroup";
            operationGroup.Padding = new Padding(15, 0, 15, 0);
            operationGroup.Radius = 0;
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
            // 
            // operationFolderButton
            // 
            operationFolderButton.BackColor = Color.White;
            operationFolderButton.BackHover = Color.FromArgb(241, 245, 249);
            operationFolderButton.Cursor = Cursors.Hand;
            operationFolderButton.DefaultBack = Color.White;
            operationFolderButton.Dock = DockStyle.Right;
            operationFolderButton.ForeColor = Color.FromArgb(71, 85, 105);
            operationFolderButton.Location = new Point(1235, 0);
            operationFolderButton.Margin = new Padding(4);
            operationFolderButton.Name = "operationFolderButton";
            operationFolderButton.Size = new Size(130, 42);
            operationFolderButton.TabIndex = 2;
            operationFolderButton.Text = "打开日志目录";
            operationFolderButton.Click += OpenOperationFolderButton_Click;
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
            Margin = new Padding(4);
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

        private AntdUI.Button analysisButton;
        private AntdUI.Button correctionButton;
        private AntdUI.Button controlAuthorityButton;
        private AntdUI.Button diagnosticsButton;
        private AntdUI.Panel panel1;
        private AntdUI.Panel headerStatusPanel;
        private Panel accountPanel;
        private AntdUI.Label currentUserLabel;
        private AntdUI.Button manageAccountsButton;
        private Panel panel2;
        private Panel panelswitch;
        private Panel contentPanel;
        private AntdUI.Panel operationGroup;
        private AntdUI.Button operationFolderButton;
        private Panel navigationMarker;
        private AntdUI.Label operationPathLabel;
        private AntdUI.Button Bu_Calibration;
        private AntdUI.Button Bu_manual;
        private AntdUI.Button Bu_auto;
        private AntdUI.Button bu_Configuration;
        private AntdUI.Button button5;
        private AntdUI.Button button2;
        private AntdUI.Button Bu_data;
        private AntdUI.Label controlSectionLabel;
        private AntdUI.Label toolsSectionLabel;
        private AntdUI.Label systemSectionLabel;
        private AntdUI.Label controlStatusLabel;
        private AntdUI.Label adsStatusLabel;
        private AntdUI.Label appTitleLabel;
    }
}
