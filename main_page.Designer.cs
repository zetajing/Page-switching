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
            bu_Configuration = new Button();
            button5 = new Button();
            button2 = new Button();
            button1 = new Button();
            button4 = new Button();
            button3 = new Button();
            Bu_manual = new Button();
            Bu_auto = new Button();
            panelswitch = new Panel();
            panel1.SuspendLayout();
            headerStatusPanel.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(15, 23, 42);
            panel1.Controls.Add(headerStatusPanel);
            panel1.Controls.Add(appSubtitleLabel);
            panel1.Controls.Add(appTitleLabel);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1400, 64);
            panel1.TabIndex = 0;
            // 
            // headerStatusPanel
            // 
            headerStatusPanel.BackColor = Color.FromArgb(15, 23, 42);
            headerStatusPanel.Controls.Add(controlStatusLabel);
            headerStatusPanel.Controls.Add(adsStatusLabel);
            headerStatusPanel.Dock = DockStyle.Right;
            headerStatusPanel.Location = new Point(1005, 0);
            headerStatusPanel.Name = "headerStatusPanel";
            headerStatusPanel.Padding = new Padding(10, 8, 18, 6);
            headerStatusPanel.Size = new Size(395, 64);
            headerStatusPanel.TabIndex = 2;
            // 
            // controlStatusLabel
            // 
            controlStatusLabel.Dock = DockStyle.Fill;
            controlStatusLabel.Font = new Font("Microsoft YaHei UI", 8F);
            controlStatusLabel.ForeColor = Color.FromArgb(148, 163, 184);
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
            adsStatusLabel.ForeColor = Color.FromArgb(251, 146, 60);
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
            appSubtitleLabel.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            appSubtitleLabel.ForeColor = Color.FromArgb(56, 189, 248);
            appSubtitleLabel.Location = new Point(226, 34);
            appSubtitleLabel.Name = "appSubtitleLabel";
            appSubtitleLabel.Size = new Size(186, 19);
            appSubtitleLabel.TabIndex = 1;
            appSubtitleLabel.Text = "WAVE CONTROL CONSOLE";
            // 
            // appTitleLabel
            // 
            appTitleLabel.AutoSize = true;
            appTitleLabel.Font = new Font("Microsoft YaHei UI", 16F, FontStyle.Bold);
            appTitleLabel.ForeColor = Color.White;
            appTitleLabel.Location = new Point(20, 14);
            appTitleLabel.Name = "appTitleLabel";
            appTitleLabel.Size = new Size(177, 36);
            appTitleLabel.TabIndex = 0;
            appTitleLabel.Text = "造波控制系统";
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(17, 24, 39);
            panel2.Controls.Add(systemSectionLabel);
            panel2.Controls.Add(toolsSectionLabel);
            panel2.Controls.Add(controlSectionLabel);
            panel2.Controls.Add(bu_Configuration);
            panel2.Controls.Add(button5);
            panel2.Controls.Add(button2);
            panel2.Controls.Add(button1);
            panel2.Controls.Add(button4);
            panel2.Controls.Add(button3);
            panel2.Controls.Add(Bu_manual);
            panel2.Controls.Add(Bu_auto);
            panel2.Dock = DockStyle.Left;
            panel2.Location = new Point(0, 64);
            panel2.Name = "panel2";
            panel2.Padding = new Padding(15, 0, 15, 12);
            panel2.Size = new Size(190, 796);
            panel2.TabIndex = 1;
            // 
            // systemSectionLabel
            // 
            systemSectionLabel.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            systemSectionLabel.ForeColor = Color.FromArgb(100, 116, 139);
            systemSectionLabel.Location = new Point(18, 531);
            systemSectionLabel.Name = "systemSectionLabel";
            systemSectionLabel.Size = new Size(154, 20);
            systemSectionLabel.TabIndex = 10;
            systemSectionLabel.Text = "SYSTEM  系统";
            // 
            // toolsSectionLabel
            // 
            toolsSectionLabel.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            toolsSectionLabel.ForeColor = Color.FromArgb(100, 116, 139);
            toolsSectionLabel.Location = new Point(18, 175);
            toolsSectionLabel.Name = "toolsSectionLabel";
            toolsSectionLabel.Size = new Size(154, 20);
            toolsSectionLabel.TabIndex = 9;
            toolsSectionLabel.Text = "TOOLS  工具";
            // 
            // controlSectionLabel
            // 
            controlSectionLabel.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            controlSectionLabel.ForeColor = Color.FromArgb(100, 116, 139);
            controlSectionLabel.Location = new Point(18, 16);
            controlSectionLabel.Name = "controlSectionLabel";
            controlSectionLabel.Size = new Size(154, 20);
            controlSectionLabel.TabIndex = 8;
            controlSectionLabel.Text = "CONTROL  主控制";
            // 
            // bu_Configuration
            // 
            bu_Configuration.BackColor = Color.FromArgb(30, 41, 59);
            bu_Configuration.Cursor = Cursors.Hand;
            bu_Configuration.FlatAppearance.BorderSize = 0;
            bu_Configuration.FlatStyle = FlatStyle.Flat;
            bu_Configuration.Font = new Font("Microsoft YaHei UI", 10F, FontStyle.Bold);
            bu_Configuration.ForeColor = Color.FromArgb(226, 232, 240);
            bu_Configuration.Location = new Point(15, 557);
            bu_Configuration.Name = "bu_Configuration";
            bu_Configuration.Padding = new Padding(12, 0, 0, 0);
            bu_Configuration.Size = new Size(160, 52);
            bu_Configuration.TabIndex = 7;
            bu_Configuration.Text = "⚙  系统配置";
            bu_Configuration.TextAlign = ContentAlignment.MiddleLeft;
            bu_Configuration.UseVisualStyleBackColor = false;
            bu_Configuration.Click += bu_Configuration_Click;
            // 
            // button5
            // 
            button5.BackColor = Color.FromArgb(17, 24, 39);
            button5.Enabled = true;
            button5.FlatAppearance.BorderSize = 0;
            button5.FlatStyle = FlatStyle.Flat;
            button5.Font = new Font("Microsoft YaHei UI", 9F);
            button5.ForeColor = Color.FromArgb(100, 116, 139);
            button5.Location = new Point(15, 465);
            button5.Name = "button5";
            button5.Padding = new Padding(12, 0, 0, 0);
            button5.Size = new Size(160, 48);
            button5.TabIndex = 6;
            button5.Text = "◌  浪高监测 · 预留";
            button5.TextAlign = ContentAlignment.MiddleLeft;
            button5.UseVisualStyleBackColor = false;
            button5.Click += button5_Click;
            // 
            // button2
            // 
            button2.BackColor = Color.FromArgb(30, 41, 59);
            button2.Cursor = Cursors.Hand;
            button2.FlatAppearance.BorderSize = 0;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Font = new Font("Microsoft YaHei UI", 10F, FontStyle.Bold);
            button2.ForeColor = Color.FromArgb(226, 232, 240);
            button2.Location = new Point(15, 411);
            button2.Name = "button2";
            button2.Padding = new Padding(12, 0, 0, 0);
            button2.Size = new Size(160, 48);
            button2.TabIndex = 5;
            button2.Text = "≈  波形生成";
            button2.TextAlign = ContentAlignment.MiddleLeft;
            button2.UseVisualStyleBackColor = false;
            button2.Click += WaveformButton_Click;
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(17, 24, 39);
            button1.Enabled = true;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Microsoft YaHei UI", 9F);
            button1.ForeColor = Color.FromArgb(100, 116, 139);
            button1.Location = new Point(15, 357);
            button1.Name = "button1";
            button1.Padding = new Padding(12, 0, 0, 0);
            button1.Size = new Size(160, 48);
            button1.TabIndex = 4;
            button1.Text = "▦  数据管理 · 预留";
            button1.TextAlign = ContentAlignment.MiddleLeft;
            button1.UseVisualStyleBackColor = false;
            // 
            // button4
            // 
            button4.BackColor = Color.FromArgb(17, 24, 39);
            button4.Enabled = true;
            button4.FlatAppearance.BorderSize = 0;
            button4.FlatStyle = FlatStyle.Flat;
            button4.Font = new Font("Microsoft YaHei UI", 9F);
            button4.ForeColor = Color.FromArgb(100, 116, 139);
            button4.Location = new Point(15, 303);
            button4.Name = "button4";
            button4.Padding = new Padding(12, 0, 0, 0);
            button4.Size = new Size(160, 48);
            button4.TabIndex = 3;
            button4.Text = "⌖  标定管理 · 预留";
            button4.TextAlign = ContentAlignment.MiddleLeft;
            button4.UseVisualStyleBackColor = false;
            // 
            // button3
            // 
            button3.BackColor = Color.FromArgb(17, 24, 39);
            button3.Cursor = Cursors.Hand;
            button3.FlatAppearance.BorderSize = 0;
            button3.FlatStyle = FlatStyle.Flat;
            button3.Font = new Font("Microsoft YaHei UI", 10F, FontStyle.Bold);
            button3.ForeColor = Color.FromArgb(226, 232, 240);
            button3.Location = new Point(15, 249);
            button3.Name = "button3";
            button3.Padding = new Padding(12, 0, 0, 0);
            button3.Size = new Size(160, 48);
            button3.TabIndex = 2;
            button3.Text = "◇  控制权申请";
            button3.TextAlign = ContentAlignment.MiddleLeft;
            button3.UseVisualStyleBackColor = false;
            button3.Click += ControlAuthorityButton_Click;
            // 
            // Bu_manual
            // 
            Bu_manual.BackColor = Color.FromArgb(30, 41, 59);
            Bu_manual.Cursor = Cursors.Hand;
            Bu_manual.FlatAppearance.BorderSize = 0;
            Bu_manual.FlatStyle = FlatStyle.Flat;
            Bu_manual.Font = new Font("Microsoft YaHei UI", 11F, FontStyle.Bold);
            Bu_manual.ForeColor = Color.FromArgb(226, 232, 240);
            Bu_manual.Location = new Point(15, 105);
            Bu_manual.Name = "Bu_manual";
            Bu_manual.Padding = new Padding(12, 0, 0, 0);
            Bu_manual.Size = new Size(160, 56);
            Bu_manual.TabIndex = 1;
            Bu_manual.Text = "◆  手动控制";
            Bu_manual.TextAlign = ContentAlignment.MiddleLeft;
            Bu_manual.UseVisualStyleBackColor = false;
            Bu_manual.Click += Bu_manual_Click;
            // 
            // Bu_auto
            // 
            Bu_auto.BackColor = Color.FromArgb(14, 165, 233);
            Bu_auto.Cursor = Cursors.Hand;
            Bu_auto.FlatAppearance.BorderSize = 0;
            Bu_auto.FlatStyle = FlatStyle.Flat;
            Bu_auto.Font = new Font("Microsoft YaHei UI", 11F, FontStyle.Bold);
            Bu_auto.ForeColor = Color.White;
            Bu_auto.Location = new Point(15, 43);
            Bu_auto.Name = "Bu_auto";
            Bu_auto.Padding = new Padding(12, 0, 0, 0);
            Bu_auto.Size = new Size(160, 56);
            Bu_auto.TabIndex = 0;
            Bu_auto.Text = "●  自动运行";
            Bu_auto.TextAlign = ContentAlignment.MiddleLeft;
            Bu_auto.UseVisualStyleBackColor = false;
            Bu_auto.Click += Bu_auto_Click;
            // 
            // panelswitch
            // 
            panelswitch.Dock = DockStyle.Fill;
            panelswitch.Location = new Point(190, 64);
            panelswitch.Name = "panelswitch";
            panelswitch.Size = new Size(1210, 796);
            panelswitch.TabIndex = 2;
            // 
            // Mainpage
            // 
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
        private Button button4;
        private Button button3;
        private Button Bu_manual;
        private Button Bu_auto;
        private Button bu_Configuration;
        private Button button5;
        private Button button2;
        private Button button1;
        private Label appTitleLabel;
        private Label appSubtitleLabel;
        private Label controlSectionLabel;
        private Label toolsSectionLabel;
        private Label systemSectionLabel;
        private Label adsStatusLabel;
        private Label controlStatusLabel;
    }
}
