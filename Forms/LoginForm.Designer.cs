namespace Page_switching
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing) components?.Dispose();
            base.Dispose(disposing);
        }

        // 固定的控件与布局保留在 Designer，账号验证逻辑放在 LoginForm.cs。
        private void InitializeComponent()
        {
            brandPanel = new Panel();
            brandTitleLabel = new AntdUI.Label();
            brandDescriptionLabel = new AntdUI.Label();
            brandFooterLabel = new AntdUI.Label();
            loginPanel = new Panel();
            titleLabel = new AntdUI.Label();
            descriptionLabel = new AntdUI.Label();
            userNameLabel = new AntdUI.Label();
            userNameInput = new AntdUI.Input();
            passwordLabel = new AntdUI.Label();
            passwordInput = new AntdUI.Input();
            confirmPasswordLabel = new AntdUI.Label();
            confirmPasswordInput = new AntdUI.Input();
            showPasswordCheckBox = new CheckBox();
            messageLabel = new AntdUI.Label();
            submitButton = new AntdUI.Button();
            exitButton = new AntdUI.Button();
            brandPanel.SuspendLayout();
            loginPanel.SuspendLayout();
            SuspendLayout();
            // 左侧系统标识。
            brandPanel.BackColor = Color.FromArgb(15, 23, 42);
            brandPanel.Controls.Add(brandTitleLabel);
            brandPanel.Controls.Add(brandDescriptionLabel);
            brandPanel.Controls.Add(brandFooterLabel);
            brandPanel.Dock = DockStyle.Left;
            brandPanel.Name = "brandPanel";
            brandPanel.Size = new Size(370, 620);
            brandTitleLabel.Font = new Font("Microsoft YaHei UI", 25F, FontStyle.Bold);
            brandTitleLabel.ForeColor = Color.White;
            brandTitleLabel.Location = new Point(42, 170);
            brandTitleLabel.Name = "brandTitleLabel";
            brandTitleLabel.Size = new Size(300, 56);
            brandTitleLabel.Text = "造波控制系统";
            brandDescriptionLabel.Font = new Font("Microsoft YaHei UI", 12F);
            brandDescriptionLabel.ForeColor = Color.FromArgb(203, 213, 225);
            brandDescriptionLabel.Location = new Point(44, 239);
            brandDescriptionLabel.Name = "brandDescriptionLabel";
            brandDescriptionLabel.Size = new Size(285, 32);
            brandDescriptionLabel.Text = "实验控制与数据监测";
            brandFooterLabel.Font = new Font("Microsoft YaHei UI", 10F);
            brandFooterLabel.ForeColor = Color.FromArgb(148, 163, 184);
            brandFooterLabel.Location = new Point(44, 542);
            brandFooterLabel.Name = "brandFooterLabel";
            brandFooterLabel.Size = new Size(285, 30);
            brandFooterLabel.Text = "宁波材料所";
            // 右侧登录输入区。
            loginPanel.BackColor = Color.White;
            loginPanel.Controls.Add(titleLabel);
            loginPanel.Controls.Add(descriptionLabel);
            loginPanel.Controls.Add(userNameLabel);
            loginPanel.Controls.Add(userNameInput);
            loginPanel.Controls.Add(passwordLabel);
            loginPanel.Controls.Add(passwordInput);
            loginPanel.Controls.Add(confirmPasswordLabel);
            loginPanel.Controls.Add(confirmPasswordInput);
            loginPanel.Controls.Add(showPasswordCheckBox);
            loginPanel.Controls.Add(messageLabel);
            loginPanel.Controls.Add(submitButton);
            loginPanel.Controls.Add(exitButton);
            loginPanel.Dock = DockStyle.Fill;
            loginPanel.Name = "loginPanel";
            titleLabel.Font = new Font("Microsoft YaHei UI", 23F, FontStyle.Bold);
            titleLabel.ForeColor = Color.FromArgb(15, 23, 42);
            titleLabel.Location = new Point(60, 58);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(380, 48);
            titleLabel.Text = "用户登录";
            descriptionLabel.Font = new Font("Microsoft YaHei UI", 10F);
            descriptionLabel.ForeColor = Color.FromArgb(100, 116, 139);
            descriptionLabel.Location = new Point(62, 115);
            descriptionLabel.Name = "descriptionLabel";
            descriptionLabel.Size = new Size(400, 32);
            descriptionLabel.Text = "请输入账号和密码，进入控制系统。";
            userNameLabel.Location = new Point(62, 170);
            userNameLabel.Name = "userNameLabel";
            userNameLabel.Size = new Size(120, 26);
            userNameLabel.Text = "账号";
            userNameInput.Location = new Point(60, 200);
            userNameInput.Name = "userNameInput";
            userNameInput.Size = new Size(400, 44);
            userNameInput.TabIndex = 0;
            userNameInput.PlaceholderText = "请输入账号";
            userNameInput.Radius = 6;
            userNameInput.WaveSize = 0;
            passwordLabel.Location = new Point(62, 260);
            passwordLabel.Name = "passwordLabel";
            passwordLabel.Size = new Size(120, 26);
            passwordLabel.Text = "密码";
            passwordInput.Location = new Point(60, 290);
            passwordInput.Name = "passwordInput";
            passwordInput.Size = new Size(400, 44);
            passwordInput.TabIndex = 1;
            passwordInput.PlaceholderText = "请输入密码";
            passwordInput.UseSystemPasswordChar = true;
            passwordInput.Radius = 6;
            passwordInput.WaveSize = 0;
            confirmPasswordLabel.Location = new Point(62, 350);
            confirmPasswordLabel.Name = "confirmPasswordLabel";
            confirmPasswordLabel.Size = new Size(160, 26);
            confirmPasswordLabel.Text = "确认密码";
            confirmPasswordInput.Location = new Point(60, 380);
            confirmPasswordInput.Name = "confirmPasswordInput";
            confirmPasswordInput.Size = new Size(400, 44);
            confirmPasswordInput.TabIndex = 2;
            confirmPasswordInput.PlaceholderText = "再次输入密码（至少 8 位）";
            confirmPasswordInput.UseSystemPasswordChar = true;
            confirmPasswordInput.Radius = 6;
            confirmPasswordInput.WaveSize = 0;
            showPasswordCheckBox.AutoSize = true;
            showPasswordCheckBox.Location = new Point(64, 439);
            showPasswordCheckBox.Name = "showPasswordCheckBox";
            showPasswordCheckBox.TabIndex = 3;
            showPasswordCheckBox.Text = "显示密码";
            showPasswordCheckBox.UseVisualStyleBackColor = true;
            showPasswordCheckBox.CheckedChanged += ShowPasswordCheckBox_CheckedChanged;
            messageLabel.ForeColor = Color.FromArgb(185, 28, 28);
            messageLabel.Location = new Point(62, 473);
            messageLabel.Name = "messageLabel";
            messageLabel.Size = new Size(400, 38);
            messageLabel.Text = "";
            submitButton.BackColor = Color.FromArgb(29, 78, 216);
            submitButton.DefaultBack = Color.FromArgb(29, 78, 216);
            submitButton.ForeColor = Color.White;
            submitButton.Font = new Font("Microsoft YaHei UI", 11F, FontStyle.Bold);
            submitButton.Location = new Point(60, 525);
            submitButton.Name = "submitButton";
            submitButton.Size = new Size(285, 46);
            submitButton.TabIndex = 4;
            submitButton.Text = "登录";
            submitButton.Click += SubmitButton_Click;
            exitButton.BackColor = Color.White;
            exitButton.DefaultBack = Color.White;
            exitButton.DefaultBorderColor = Color.FromArgb(203, 213, 225);
            exitButton.BorderWidth = 1F;
            exitButton.ForeColor = Color.FromArgb(71, 85, 105);
            exitButton.Location = new Point(355, 525);
            exitButton.Name = "exitButton";
            exitButton.Size = new Size(105, 46);
            exitButton.TabIndex = 5;
            exitButton.Text = "退出";
            exitButton.Click += ExitButton_Click;
            // 登录窗口。
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.White;
            ClientSize = new Size(900, 620);
            Controls.Add(loginPanel);
            Controls.Add(brandPanel);
            Font = new Font("Microsoft YaHei UI", 10F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "LoginForm";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "造波控制系统 · 登录";
            brandPanel.ResumeLayout(false);
            loginPanel.ResumeLayout(false);
            loginPanel.PerformLayout();
            ResumeLayout(false);
        }

        private Panel brandPanel;
        private Panel loginPanel;
        private AntdUI.Label brandTitleLabel;
        private AntdUI.Label brandDescriptionLabel;
        private AntdUI.Label brandFooterLabel;
        private AntdUI.Label titleLabel;
        private AntdUI.Label descriptionLabel;
        private AntdUI.Label userNameLabel;
        private AntdUI.Input userNameInput;
        private AntdUI.Label passwordLabel;
        private AntdUI.Input passwordInput;
        private AntdUI.Label confirmPasswordLabel;
        private AntdUI.Input confirmPasswordInput;
        private CheckBox showPasswordCheckBox;
        private AntdUI.Label messageLabel;
        private AntdUI.Button submitButton;
        private AntdUI.Button exitButton;
    }
}
