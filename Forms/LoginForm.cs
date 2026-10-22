namespace Page_switching;

public partial class LoginForm : Form
{
    private readonly LoginService _loginService;
    private readonly bool _createAccount;
    private bool _loginUnavailable;
    private bool _submitting;

    // 无参构造保留 Designer 支持；创建窗体本身不连接 PLC，也不写账号文件。
    public LoginForm() : this(new LoginService()) { }

    internal LoginForm(LoginService loginService)
    {
        _loginService = loginService;
        InitializeComponent();
        try
        {
            _createAccount = !_loginService.HasAccount;
        }
        catch (Exception ex) when (ex is IOException or InvalidDataException or UnauthorizedAccessException)
        {
            // 资料损坏时禁止登录，不能把已有账号误当作首次使用。
            _loginUnavailable = true;
            ShowError(ex.Message);
            submitButton.Enabled = false;
        }
        if (_createAccount)
        {
            titleLabel.Text = "创建管理员";
            descriptionLabel.Text = "首次使用，请设置管理员账号和密码。";
            submitButton.Text = "创建并登录";
        }
        confirmPasswordLabel.Visible = _createAccount;
        confirmPasswordInput.Visible = _createAccount;
        if (!_createAccount)
        {
            // 普通登录收起确认密码这一行，避免留下大块空白；Designer 保留完整创建布局。
            int rowHeight = confirmPasswordInput.Bottom - passwordInput.Bottom;
            foreach (Control control in new Control[] { showPasswordCheckBox, messageLabel, submitButton, exitButton, brandFooterLabel })
                control.Top -= rowHeight;
            ClientSize = new Size(ClientSize.Width, ClientSize.Height - rowHeight);
        }
    }

    internal LoginUser? User { get; private set; }

    private async void SubmitButton_Click(object? sender, EventArgs e) => await SubmitAsync();

    // 校验和保存放在后台执行；等待期间禁止重复提交，不阻塞窗口绘制。
    private async Task SubmitAsync()
    {
        if (_submitting || _loginUnavailable) return;
        string userName = userNameInput.Text.Trim();
        string password = passwordInput.Text;
        if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrEmpty(password))
        {
            ShowError("请填写账号和密码。");
            return;
        }
        if (_createAccount && password != confirmPasswordInput.Text)
        {
            ShowError("两次输入的密码不一致。");
            confirmPasswordInput.Focus();
            return;
        }

        SetSubmitting(true);
        try
        {
            LoginUser? user = await Task.Run(() =>
            {
                if (!_createAccount) return _loginService.ValidateLogin(userName, password);
                return _loginService.CreateAccount(userName, password);
            });
            if (IsDisposed || Disposing) return;
            if (user is null)
            {
                ShowError("账号或密码不正确。");
                passwordInput.Clear();
                passwordInput.Focus();
                return;
            }

            User = user;
            passwordInput.Clear();
            confirmPasswordInput.Clear();
            DialogResult = DialogResult.OK;
            Close();
        }
        catch (Exception ex) when (ex is ArgumentException or InvalidOperationException or IOException or InvalidDataException or UnauthorizedAccessException
                                   or System.Security.Cryptography.CryptographicException)
        {
            if (!IsDisposed && !Disposing) ShowError(ex.Message);
        }
        finally
        {
            if (!IsDisposed && !Disposing) SetSubmitting(false);
        }
    }

    private void ShowError(string message)
    {
        messageLabel.Text = message;
        messageLabel.ForeColor = UiPalette.Danger;
    }

    private void SetSubmitting(bool submitting)
    {
        _submitting = submitting;
        userNameInput.Enabled = !submitting;
        passwordInput.Enabled = !submitting;
        confirmPasswordInput.Enabled = !submitting;
        showPasswordCheckBox.Enabled = !submitting;
        submitButton.Enabled = !submitting;
        exitButton.Enabled = !submitting;
        if (submitting) messageLabel.Text = "正在验证，请稍候…";
    }

    private void ShowPasswordCheckBox_CheckedChanged(object? sender, EventArgs e)
    {
        passwordInput.UseSystemPasswordChar = !showPasswordCheckBox.Checked;
        confirmPasswordInput.UseSystemPasswordChar = !showPasswordCheckBox.Checked;
    }

    private void ExitButton_Click(object? sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }

    // Enter 提交、Esc 退出；输入框无需各自重复注册快捷键。
    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
        if (keyData == Keys.Enter)
        {
            _ = SubmitAsync();
            return true;
        }
        if (keyData == Keys.Escape)
        {
            if (!_submitting) ExitButton_Click(this, EventArgs.Empty);
            return true;
        }
        return base.ProcessCmdKey(ref msg, keyData);
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        // 账号写入期间先等待本次提交完成，避免窗口关闭后仍创建账号。
        if (_submitting && DialogResult != DialogResult.OK) e.Cancel = true;
        base.OnFormClosing(e);
    }
}
