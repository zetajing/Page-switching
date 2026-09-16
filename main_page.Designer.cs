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
        private void InitializeComponent()
        {
            panel1 = new Panel();
            panel2 = new Panel();
            bu_Configuration = new Button();
            button5 = new Button();
            button2 = new Button();
            button1 = new Button();
            button4 = new Button();
            button3 = new Button();
            Bu_manual = new Button();
            Bu_auto = new Button();
            panelswitch = new Panel();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.Silver;
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1047, 56);
            panel1.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.ActiveCaption;
            panel2.Controls.Add(bu_Configuration);
            panel2.Controls.Add(button5);
            panel2.Controls.Add(button2);
            panel2.Controls.Add(button1);
            panel2.Controls.Add(button4);
            panel2.Controls.Add(button3);
            panel2.Controls.Add(Bu_manual);
            panel2.Controls.Add(Bu_auto);
            panel2.Dock = DockStyle.Left;
            panel2.Location = new Point(0, 56);
            panel2.Name = "panel2";
            panel2.Size = new Size(142, 681);
            panel2.TabIndex = 1;
            // 
            // bu_Configuration
            // 
            bu_Configuration.Location = new Point(5, 594);
            bu_Configuration.Name = "bu_Configuration";
            bu_Configuration.Size = new Size(131, 75);
            bu_Configuration.TabIndex = 7;
            bu_Configuration.Text = "配置页面";
            bu_Configuration.UseVisualStyleBackColor = true;
            bu_Configuration.Click += bu_Configuration_Click;
            // 
            // button5
            // 
            button5.Location = new Point(5, 510);
            button5.Name = "button5";
            button5.Size = new Size(131, 75);
            button5.TabIndex = 6;
            button5.Text = "浪高仪";
            button5.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new Point(5, 426);
            button2.Name = "button2";
            button2.Size = new Size(131, 75);
            button2.TabIndex = 5;
            button2.Text = "波形生成";
            button2.UseVisualStyleBackColor = true;
            button2.Click += WaveformButton_Click;
            // 
            // button1
            // 
            button1.Location = new Point(5, 342);
            button1.Name = "button1";
            button1.Size = new Size(131, 75);
            button1.TabIndex = 4;
            button1.Text = "数据库";
            button1.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            button4.Location = new Point(5, 258);
            button4.Name = "button4";
            button4.Size = new Size(131, 75);
            button4.TabIndex = 3;
            button4.Text = "标定页面";
            button4.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Location = new Point(5, 174);
            button3.Name = "button3";
            button3.Size = new Size(131, 75);
            button3.TabIndex = 2;
            button3.Text = "button3";
            button3.UseVisualStyleBackColor = true;
            // 
            // Bu_manual
            // 
            Bu_manual.Font = new Font("Microsoft YaHei UI", 19.8000011F, FontStyle.Regular, GraphicsUnit.Point, 134);
            Bu_manual.Location = new Point(5, 90);
            Bu_manual.Name = "Bu_manual";
            Bu_manual.Size = new Size(131, 75);
            Bu_manual.TabIndex = 1;
            Bu_manual.Text = "手动";
            Bu_manual.UseVisualStyleBackColor = true;
            Bu_manual.Click += Bu_manual_Click;
            // 
            // Bu_auto
            // 
            Bu_auto.Font = new Font("Microsoft YaHei UI", 19.8000011F, FontStyle.Regular, GraphicsUnit.Point, 134);
            Bu_auto.Location = new Point(5, 6);
            Bu_auto.Name = "Bu_auto";
            Bu_auto.Size = new Size(131, 75);
            Bu_auto.TabIndex = 0;
            Bu_auto.Text = "自动";
            Bu_auto.UseVisualStyleBackColor = true;
            Bu_auto.Click += Bu_auto_Click;
            // 
            // panelswitch
            // 
            panelswitch.Dock = DockStyle.Fill;
            panelswitch.Location = new Point(142, 56);
            panelswitch.Name = "panelswitch";
            panelswitch.Size = new Size(905, 681);
            panelswitch.TabIndex = 2;
            // 
            // Mainpage
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1047, 737);
            Controls.Add(panelswitch);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Name = "Mainpage";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "mainpage";
            panel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
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
    }
}
