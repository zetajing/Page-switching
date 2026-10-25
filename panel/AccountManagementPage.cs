namespace Page_switching.panel;

public partial class AccountManagementPage : UserControl
{
    private readonly LoginService _loginService;
    private readonly LoginUser? _currentUser;
    private bool _busy;

    public AccountManagementPage() : this(new LoginService(), null) { }

    internal AccountManagementPage(LoginService loginService, LoginUser? currentUser)
    {
        _loginService = loginService;
        _currentUser = currentUser;
        InitializeComponent();
        roleComboBox.SelectedIndex = 0;
    }

    // 每次回到账号页重新读取列表；隐藏时清除密码，不在页面缓存中保留明文。
    protected override void OnVisibleChanged(EventArgs e)
    {
        base.OnVisibleChanged(e);
        if (!Visible)
        {
            passwordInput.Clear();
            confirmPasswordInput.Clear();
            return;
        }
        if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime
            || DesignMode || _busy) return;
        try { ReloadUsers(); }
        catch (Exception ex) when (ex is IOException or InvalidDataException or UnauthorizedAccessException)
        {
            messageLabel.Text = ex.Message;
            addButton.Enabled = resetButton.Enabled = deleteButton.Enabled = false;
        }
    }

    private void ReloadUsers()
    {
        if (_currentUser is null || !_currentUser.IsAdministrator)
            throw new UnauthorizedAccessException("仅管理员可以管理账号。");
        var users = _loginService.GetUsers(_currentUser);
        accountGrid.Rows.Clear();
        foreach (var user in users)
            accountGrid.Rows.Add(user.UserName, user.IsAdministrator ? "管理员" : "操作员");
        accountGrid.ClearSelection();
        addButton.Enabled = !_busy;
        UpdateSelection();
    }

    private string? SelectedUserName => accountGrid.SelectedRows.Count == 0
        ? null : accountGrid.SelectedRows[0].Cells[0].Value?.ToString();

    private void AccountGrid_SelectionChanged(object? sender, EventArgs e) => UpdateSelection();

    private void UpdateSelection()
    {
        string? selected = SelectedUserName;
        selectedAccountLabel.Text = selected is null ? "左侧选择账号可重置密码或删除。" : "当前选择：" + selected;
        resetButton.Enabled = !_busy && selected is not null;
        // 当前管理员不能删除自己的登录账号；服务还会独立检查最后一个管理员。
        deleteButton.Enabled = !_busy && selected is not null
            && !string.Equals(selected, _currentUser?.UserName, StringComparison.OrdinalIgnoreCase);
    }

    private async void AddButton_Click(object? sender, EventArgs e)
    {
        if (!ValidatePassword()) return;
        string name = userNameInput.Text.Trim();
        string password = passwordInput.Text;
        UserRole role = roleComboBox.SelectedIndex == 1 ? UserRole.Administrator : UserRole.Operator;
        await RunActionAsync(() => _loginService.AddUser(_currentUser!, name, password, role),
            "新增账号：" + name);
    }

    private async void ResetButton_Click(object? sender, EventArgs e)
    {
        string? name = SelectedUserName;
        if (name is null || !ValidatePassword()) return;
        string password = passwordInput.Text;
        await RunActionAsync(() => _loginService.ResetPassword(_currentUser!, name, password),
            "已重置密码：" + name);
    }

    private async void DeleteButton_Click(object? sender, EventArgs e)
    {
        string? name = SelectedUserName;
        if (name is null) return;
        if (MessageBox.Show(this, "确定删除账号“" + name + "”？", "删除账号",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
        await RunActionAsync(() => _loginService.DeleteUser(_currentUser!, name), "删除账号：" + name);
    }

    private bool ValidatePassword()
    {
        if (passwordInput.Text.Length is < 8 or > 128)
        {
            messageLabel.Text = "密码请填写 8～128 个字符。";
            return false;
        }
        if (passwordInput.Text != confirmPasswordInput.Text)
        {
            messageLabel.Text = "两次输入的密码不一致。";
            return false;
        }
        return true;
    }

    // 新增、重置和删除共用一次提交流程；服务再次校验管理员权限。
    private async Task RunActionAsync(Action action, string successMessage)
    {
        if (_busy || _currentUser?.IsAdministrator != true) return;
        SetBusy(true);
        try
        {
            await Task.Run(action);
            if (IsDisposed || Disposing) return;
            passwordInput.Clear();
            confirmPasswordInput.Clear();
            ReloadUsers();
            messageLabel.Text = successMessage;
            OperationJournal.Record("账号管理", _currentUser.UserName + "：" + successMessage);
        }
        catch (Exception ex) when (ex is ArgumentException or InvalidOperationException or IOException or InvalidDataException
                                   or UnauthorizedAccessException or System.Security.Cryptography.CryptographicException)
        {
            if (!IsDisposed && !Disposing) messageLabel.Text = ex.Message;
            OperationJournal.Record("账号管理", "操作失败：" + ex.Message);
        }
        finally
        {
            if (!IsDisposed && !Disposing) SetBusy(false);
        }
    }

    private void SetBusy(bool busy)
    {
        _busy = busy;
        accountGrid.Enabled = userNameInput.Enabled = roleComboBox.Enabled = !busy;
        passwordInput.Enabled = confirmPasswordInput.Enabled = addButton.Enabled = !busy;
        closeButton.Enabled = !busy;
        if (busy) messageLabel.Text = "正在保存，请稍候…";
        UpdateSelection();
    }

    internal event EventHandler? BackRequested;

    // 返回也由主窗体切页；账号写入期间等待这次提交完成。
    private void CloseButton_Click(object? sender, EventArgs e)
    {
        if (!_busy) BackRequested?.Invoke(this, e);
    }
}
