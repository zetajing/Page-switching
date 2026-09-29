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
            bu_Configuration = new NavigationButton();
            button5 = new NavigationButton();
            button2 = new NavigationButton();
            Bu_data = new NavigationButton();
            Bu_Calibration = new NavigationButton();
            button3 = new NavigationButton();
            Bu_manual = new NavigationButton();
            Bu_auto = new NavigationButton();
            panelswitch = new Panel();
            panel1.SuspendLayout();
            headerStatusPanel.SuspendLayout();
            panel2.SuspendLayout();
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
            panel1.Size = new Size(1400, 72);
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
            headerStatusPanel.Padding = new Padding(10, 8, 18, 6);
            headerStatusPanel.Size = new Size(395, 72);
            headerStatusPanel.TabIndex = 2;
            // 
            // controlStatusLabel
            // 
            controlStatusLabel.Dock = DockStyle.Fill;
            controlStatusLabel.Font = new Font("Microsoft YaHei UI", 8F);
            controlStatusLabel.ForeColor = Color.FromArgb(100, 116, 139);
            controlStatusLabel.Location = new Point(10, 33);
            controlStatusLabel.Name = "controlStatusLabel";
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
            appSubtitleLabel.Font = new Font("Segoe UI", 8F);
            appSubtitleLabel.ForeColor = Color.FromArgb(100, 116, 139);
            appSubtitleLabel.Location = new Point(24, 45);
            appSubtitleLabel.Name = "appSubtitleLabel";
            appSubtitleLabel.Size = new Size(186, 19);
            appSubtitleLabel.TabIndex = 1;
            appSubtitleLabel.Text = "WAVE CONTROL CONSOLE";
            // 
            // appTitleLabel
            // 
            appTitleLabel.AutoSize = true;
            appTitleLabel.Font = new Font("Microsoft YaHei UI", 15F, FontStyle.Bold);
            appTitleLabel.ForeColor = Color.FromArgb(15, 23, 42);
            appTitleLabel.Location = new Point(22, 8);
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
            panel2.Dock = DockStyle.Left;
            panel2.Location = new Point(0, 72);
            panel2.Name = "panel2";
            panel2.Padding = new Padding(15, 0, 15, 12);
            panel2.Size = new Size(206, 788);
            panel2.TabIndex = 1;
            // 
            // systemSectionLabel
            // 
            systemSectionLabel.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            systemSectionLabel.ForeColor = Color.FromArgb(100, 116, 139);
            systemSectionLabel.Location = new Point(20, 474);
            systemSectionLabel.Name = "systemSectionLabel";
            systemSectionLabel.Size = new Size(154, 20);
            systemSectionLabel.TabIndex = 10;
            systemSectionLabel.Text = "系统";
            // 
            // toolsSectionLabel
            // 
            toolsSectionLabel.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            toolsSectionLabel.ForeColor = Color.FromArgb(100, 116, 139);
            toolsSectionLabel.Location = new Point(20, 173);
            toolsSectionLabel.Name = "toolsSectionLabel";
            toolsSectionLabel.Size = new Size(154, 20);
            toolsSectionLabel.TabIndex = 9;
            toolsSectionLabel.Text = "功能";
            // 
            // controlSectionLabel
            // 
            controlSectionLabel.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            controlSectionLabel.ForeColor = Color.FromArgb(100, 116, 139);
            controlSectionLabel.Location = new Point(20, 16);
            controlSectionLabel.Name = "controlSectionLabel";
            controlSectionLabel.Size = new Size(154, 20);
            controlSectionLabel.TabIndex = 8;
            controlSectionLabel.Text = "控制";
            // 
            // bu_Configuration
            // 
            bu_Configuration.BackColor = Color.FromArgb(248, 250, 252);
            bu_Configuration.Cursor = Cursors.Hand;
            bu_Configuration.FlatAppearance.BorderSize = 0;
            bu_Configuration.FlatStyle = FlatStyle.Flat;
            bu_Configuration.Font = new Font("Microsoft YaHei UI", 10F, FontStyle.Bold);
            bu_Configuration.ForeColor = Color.FromArgb(71, 85, 105);
            bu_Configuration.Location = new Point(14, 504);
            bu_Configuration.Name = "bu_Configuration";
            bu_Configuration.Padding = new Padding(12, 0, 0, 0);
            bu_Configuration.Size = new Size(178, 48);
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
            button5.Font = new Font("Microsoft YaHei UI", 10F, FontStyle.Bold);
            button5.ForeColor = Color.FromArgb(71, 85, 105);
            button5.Location = new Point(14, 401);
            button5.Name = "button5";
            button5.Padding = new Padding(12, 0, 0, 0);
            button5.Size = new Size(178, 44);
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
            button2.Font = new Font("Microsoft YaHei UI", 10F, FontStyle.Bold);
            button2.ForeColor = Color.FromArgb(71, 85, 105);
            button2.Location = new Point(14, 352);
            button2.Name = "button2";
            button2.Padding = new Padding(12, 0, 0, 0);
            button2.Size = new Size(178, 44);
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
            Bu_data.Font = new Font("Microsoft YaHei UI", 10F, FontStyle.Bold);
            Bu_data.ForeColor = Color.FromArgb(71, 85, 105);
            Bu_data.Location = new Point(14, 303);
            Bu_data.Name = "Bu_data";
            Bu_data.Padding = new Padding(12, 0, 0, 0);
            Bu_data.Size = new Size(178, 44);
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
            Bu_Calibration.Font = new Font("Microsoft YaHei UI", 10F, FontStyle.Bold);
            Bu_Calibration.ForeColor = Color.FromArgb(71, 85, 105);
            Bu_Calibration.Location = new Point(14, 254);
            Bu_Calibration.Name = "Bu_Calibration";
            Bu_Calibration.Padding = new Padding(12, 0, 0, 0);
            Bu_Calibration.Size = new Size(178, 44);
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
            button3.Font = new Font("Microsoft YaHei UI", 10F, FontStyle.Bold);
            button3.ForeColor = Color.FromArgb(71, 85, 105);
            button3.Location = new Point(14, 205);
            button3.Name = "button3";
            button3.Padding = new Padding(12, 0, 0, 0);
            button3.Size = new Size(178, 44);
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
            Bu_manual.Font = new Font("Microsoft YaHei UI", 11F, FontStyle.Bold);
            Bu_manual.ForeColor = Color.FromArgb(71, 85, 105);
            Bu_manual.Location = new Point(14, 102);
            Bu_manual.Name = "Bu_manual";
            Bu_manual.Padding = new Padding(12, 0, 0, 0);
            Bu_manual.Size = new Size(178, 50);
            Bu_manual.TabIndex = 1;
            Bu_manual.Text = "手动控制";
            Bu_manual.TextAlign = ContentAlignment.MiddleLeft;
            Bu_manual.UseVisualStyleBackColor = false;
            Bu_manual.Click += Bu_manual_Click;
            // 
            // Bu_auto
            // 
            Bu_auto.BackColor = Color.FromArgb(219, 234, 254);
            Bu_auto.IsActive = true;
            Bu_auto.Cursor = Cursors.Hand;
            Bu_auto.FlatAppearance.BorderSize = 0;
            Bu_auto.FlatStyle = FlatStyle.Flat;
            Bu_auto.Font = new Font("Microsoft YaHei UI", 11F, FontStyle.Bold);
            Bu_auto.ForeColor = Color.FromArgb(30, 64, 175);
            Bu_auto.Location = new Point(14, 43);
            Bu_auto.Name = "Bu_auto";
            Bu_auto.Padding = new Padding(12, 0, 0, 0);
            Bu_auto.Size = new Size(178, 50);
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
            panelswitch.Location = new Point(206, 72);
            panelswitch.Name = "panelswitch";
            panelswitch.Size = new Size(1194, 788);
            panelswitch.TabIndex = 2;
            // 
            // Mainpage
            // 
            BackColor = Color.FromArgb(241, 245, 249);
            ForeColor = Color.FromArgb(15, 23, 42);
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1400, 860);
            Controls.Add(panelswitch);
            Controls.Add(panel2);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.SizableToolWindow;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Mainpage";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "造波控制系统";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            headerStatusPanel.ResumeLayout(false);
            panel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel headerStatusPanel;
        private Panel panel2;
        private Panel panelswitch;
        private NavigationButton Bu_Calibration;
        private NavigationButton button3;
        private NavigationButton Bu_manual;
        private NavigationButton Bu_auto;
        private NavigationButton bu_Configuration;
        private NavigationButton button5;
        private NavigationButton button2;
        private NavigationButton Bu_data;
        private Label appTitleLabel;
        private Label appSubtitleLabel;
        private Label controlSectionLabel;
        private Label toolsSectionLabel;
        private Label systemSectionLabel;
        private Label adsStatusLabel;
        private Label controlStatusLabel;
    }
}
