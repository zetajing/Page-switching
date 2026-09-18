using System.Configuration;
using System.Data.Common;
using System.Net;

namespace Page_switching.panel;

public partial class Config : UserControl
{
    // 初始化配置页面并加载当前 App.config 设置。
    public Config()
    {
        InitializeComponent();
        Size = new Size(1210, 796);
        waveGeneratorLayout.ColumnStyles[0].Width = 220F;
        LoadSettings();
        LoadDatabaseSettings();
    }

    // 将 Router 和波形生成器配置加载到界面控件。
    private void LoadSettings()
    {
        waveGeneratorModeComboBox.Items.AddRange(["外部 WFast.exe", "WaveMaker 内置算法"]);
        waveGeneratorModeComboBox.SelectedIndex = string.Equals(
            Read("WaveGeneratorMode", "ExternalExe"),
            nameof(WaveformGeneratorMode.WaveMaker),
            StringComparison.OrdinalIgnoreCase) ? 1 : 0;
        waveGeneratorPathTextBox.Text = Read("WaveGeneratorPath");
        routerEnabledCheckBox.Checked = ReadBool("AdsTcpRouterEnabled", false);
        routerNameTextBox.Text = Read("AdsTcpRouterName", "PageSwitchingRouter");
        localNetIdTextBox.Text = Read("AdsTcpRouterLocalNetId");
        routerTcpPortInput.Value = ReadPort("AdsTcpRouterTcpPort", 48898);
        remoteNameTextBox.Text = Read("AdsTcpRouterRemoteName", "WaveMakerPlc");
        remoteAddressTextBox.Text = Read("AdsTcpRouterRemoteAddress");
        remoteNetIdTextBox.Text = Read("AdsTcpRouterRemoteNetId", Read("AdsAmsNetId"));
        UpdateWaveGeneratorState();
        UpdateInputState();
    }

    // 从 App.config 读取数据库开关、连接字符串以及 SQL Server 账号密码。
    private void LoadDatabaseSettings()
    {
        _databaseEnabledCheckBox.Checked = ReadBool("DatabaseEnabled", false);
        var connectionString = Read(
            "DatabaseConnectionString",
            "Server=localhost;Database=WaveControl;Integrated Security=True;TrustServerCertificate=True");
        _databaseConnectionTextBox.Text = connectionString;

        var connectionCredentials = ReadDatabaseCredentials(connectionString);
        _databaseUserNameTextBox.Text = Read("DatabaseUserName", connectionCredentials.UserName);
        _databasePasswordTextBox.Text = Read("DatabasePassword", connectionCredentials.Password);
        UpdateDatabaseState();
    }

    // 响应数据库日志开关变化并更新连接字符串输入状态。
    private void DatabaseEnabledCheckBox_CheckedChanged(object? sender, EventArgs e)
    {
        UpdateDatabaseState();
    }

    // 根据数据库开关更新输入控件和状态文字。
    private void UpdateDatabaseState()
    {
        if (_databaseEnabledCheckBox is null)
        {
            return;
        }

        var enabled = _databaseEnabledCheckBox.Checked;
        _databaseConnectionTextBox.Enabled = enabled;
        _databaseUserNameTextBox.Enabled = enabled;
        _databasePasswordTextBox.Enabled = enabled;
        _databaseStateLabel.Text = enabled
            ? "数据库只用于运行日志和操作追溯，不参与 PLC 实时控制。"
            : "数据库日志未启用。";
        _databaseStateLabel.ForeColor = enabled
            ? Color.FromArgb(5, 150, 105)
            : Color.FromArgb(100, 116, 139);
    }

    // 验证并保存数据库配置到 App.config。
    private void SaveDatabaseButton_Click(object? sender, EventArgs e)
    {
        try
        {
            var connectionString = _databaseConnectionTextBox.Text.Trim();
            var userName = _databaseUserNameTextBox.Text.Trim();
            var password = _databasePasswordTextBox.Text;
            if (_databaseEnabledCheckBox.Checked && string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException("启用数据库日志时，连接字符串不能为空。");
            }

            if (!string.IsNullOrWhiteSpace(password) && string.IsNullOrWhiteSpace(userName))
            {
                throw new InvalidOperationException("填写数据库密码时，账号不能为空。");
            }

            connectionString = AddSqlServerCredentials(connectionString, userName, password);

            var configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var settings = configuration.AppSettings.Settings;
            Set(settings, "DatabaseEnabled", _databaseEnabledCheckBox.Checked.ToString().ToLowerInvariant());
            Set(settings, "DatabaseProvider", "SQL Server");
            Set(settings, "DatabaseConnectionString", connectionString);
            Set(settings, "DatabaseUserName", userName);
            Set(settings, "DatabasePassword", password);
            configuration.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");

            _databaseConnectionTextBox.Text = connectionString;

            _databaseStateLabel.ForeColor = Color.FromArgb(5, 150, 105);
            _databaseStateLabel.Text = "数据库配置保存成功，当前仅用于日志和追溯。";
            saveResultLabel.ForeColor = Color.FromArgb(5, 150, 105);
            saveResultLabel.Text = "数据库配置保存成功。";
        }
        catch (Exception ex)
        {
            saveResultLabel.ForeColor = Color.FromArgb(220, 38, 38);
            saveResultLabel.Text = "数据库配置保存失败：" + ex.Message;
        }
    }

    // 打开文件选择框，让用户选择波形生成程序。
    private void BrowseWaveGeneratorButton_Click(object? sender, EventArgs e)
    {
        using var dialog = new OpenFileDialog
        {
            Filter = "WFast 程序 (WFast.exe)|WFast.exe|所有程序 (*.exe)|*.exe",
            Title = "选择 WFast.exe"
        };

        if (File.Exists(waveGeneratorPathTextBox.Text.Trim()))
        {
            dialog.FileName = waveGeneratorPathTextBox.Text.Trim();
        }

        if (dialog.ShowDialog() == DialogResult.OK)
        {
            waveGeneratorPathTextBox.Text = dialog.FileName;
            UpdateWaveGeneratorState();
        }
    }

    // 路径文字变化后重新检查波形生成程序是否有效。
    private void WaveGeneratorPathTextBox_TextChanged(object? sender, EventArgs e)
    {
        UpdateWaveGeneratorState();
    }

    // 生成方案变化后更新路径输入框和状态提示。
    private void WaveGeneratorModeComboBox_SelectedIndexChanged(object? sender, EventArgs e)
    {
        UpdateWaveGeneratorState();
    }

    // 验证并保存波形生成程序路径。
    private void SaveWaveGeneratorButton_Click(object? sender, EventArgs e)
    {
        try
        {
            var path = waveGeneratorPathTextBox.Text.Trim();
            var useWaveMaker = IsWaveMakerModeSelected();
            if (!useWaveMaker && string.IsNullOrWhiteSpace(path))
            {
                throw new InvalidOperationException("请先选择 WFast.exe 文件。");
            }

            if (!useWaveMaker && !File.Exists(path))
            {
                throw new InvalidOperationException("WFast.exe 路径不存在。");
            }

            var configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            Set(configuration.AppSettings.Settings, "WaveGeneratorMode",
                useWaveMaker ? nameof(WaveformGeneratorMode.WaveMaker) : nameof(WaveformGeneratorMode.ExternalExe));
            Set(configuration.AppSettings.Settings, "WaveGeneratorPath", path);
            configuration.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");

            waveGeneratorStateLabel.Text = useWaveMaker
                ? "已启用 WaveMaker 内置算法，不需要 WFast.exe"
                : "已保存 WFast.exe 路径，波形页面可以直接生成";
            waveGeneratorStateLabel.ForeColor = Color.FromArgb(5, 150, 105);
            saveResultLabel.ForeColor = Color.FromArgb(5, 150, 105);
            saveResultLabel.Text = useWaveMaker ? "WaveMaker 内置方案保存成功。" : "WFast.exe 方案保存成功。";
        }
        catch (Exception ex)
        {
            saveResultLabel.ForeColor = Color.FromArgb(220, 38, 38);
            saveResultLabel.Text = "WFast 路径保存失败：" + ex.Message;
        }
    }

    // 根据文件是否存在更新路径状态提示。
    private void UpdateWaveGeneratorState()
    {
        var useWaveMaker = IsWaveMakerModeSelected();
        waveGeneratorPathTextBox.Enabled = !useWaveMaker;
        browseWaveGeneratorButton.Enabled = !useWaveMaker;
        if (useWaveMaker)
        {
            waveGeneratorStateLabel.Text = "已选择 WaveMaker 内置算法，不需要配置 WFast.exe";
            waveGeneratorStateLabel.ForeColor = Color.FromArgb(5, 150, 105);
            return;
        }

        var path = waveGeneratorPathTextBox.Text.Trim();
        if (string.IsNullOrWhiteSpace(path))
        {
            waveGeneratorStateLabel.Text = "未配置，波形生成页面运行时会提示设置路径";
            waveGeneratorStateLabel.ForeColor = Color.FromArgb(180, 83, 9);
        }
        else if (File.Exists(path))
        {
            waveGeneratorStateLabel.Text = "已找到 WFast.exe";
            waveGeneratorStateLabel.ForeColor = Color.FromArgb(5, 150, 105);
        }
        else
        {
            waveGeneratorStateLabel.Text = "路径不存在，请重新选择 WFast.exe";
            waveGeneratorStateLabel.ForeColor = Color.FromArgb(220, 38, 38);
        }
    }

    // Router 启用状态变化后更新相关输入框可用状态。
    private void RouterEnabledCheckBox_CheckedChanged(object? sender, EventArgs e)
    {
        UpdateInputState();
    }

    // 根据是否启用 Router 决定哪些配置输入框可以编辑。
    private void UpdateInputState()
    {
        var enabled = routerEnabledCheckBox.Checked;
        routerNameTextBox.Enabled = enabled;
        localNetIdTextBox.Enabled = enabled;
        routerTcpPortInput.Enabled = enabled;
        remoteNameTextBox.Enabled = enabled;
        remoteAddressTextBox.Enabled = enabled;
        remoteNetIdTextBox.Enabled = enabled;
        routerStateLabel.Text = enabled ? "独立 Router：启用（重启后生效）" : "独立 Router：关闭，使用系统 TwinCAT Router";
        routerStateLabel.ForeColor = enabled ? Color.FromArgb(5, 150, 105) : Color.FromArgb(100, 116, 139);
    }

    // 验证并保存 ADS TCP Router 配置。
    private void SaveRouterButton_Click(object? sender, EventArgs e)
    {
        try
        {
            ValidateInputs();
            var configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var settings = configuration.AppSettings.Settings;
            Set(settings, "WaveGeneratorMode",
                IsWaveMakerModeSelected() ? nameof(WaveformGeneratorMode.WaveMaker) : nameof(WaveformGeneratorMode.ExternalExe));
            Set(settings, "WaveGeneratorPath", waveGeneratorPathTextBox.Text.Trim());
            Set(settings, "AdsTcpRouterEnabled", routerEnabledCheckBox.Checked.ToString().ToLowerInvariant());
            Set(settings, "AdsTcpRouterName", routerNameTextBox.Text.Trim());
            Set(settings, "AdsTcpRouterLocalNetId", localNetIdTextBox.Text.Trim());
            Set(settings, "AdsTcpRouterTcpPort", decimal.ToInt32(routerTcpPortInput.Value).ToString());
            Set(settings, "AdsTcpRouterLoopbackIp", "127.0.0.1");
            Set(settings, "AdsTcpRouterLoopbackPort", decimal.ToInt32(routerTcpPortInput.Value).ToString());
            Set(settings, "AdsTcpRouterRemoteName", remoteNameTextBox.Text.Trim());
            Set(settings, "AdsTcpRouterRemoteAddress", remoteAddressTextBox.Text.Trim());
            Set(settings, "AdsTcpRouterRemoteNetId", remoteNetIdTextBox.Text.Trim());
            Set(settings, "AdsAmsNetId", remoteNetIdTextBox.Text.Trim());
            configuration.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");

            saveResultLabel.ForeColor = Color.FromArgb(5, 150, 105);
            saveResultLabel.Text = "保存成功；Router 和 ADS 配置重启后生效，波形生成方案立即可用。";
        }
        catch (Exception ex)
        {
            saveResultLabel.ForeColor = Color.FromArgb(220, 38, 38);
            saveResultLabel.Text = "保存失败：" + ex.Message;
        }
    }

    // 检查 Router 名称、Net ID、地址和端口是否合法。
    private void ValidateInputs()
    {
        if (!routerEnabledCheckBox.Checked)
        {
            ValidateWaveGeneratorPath();
            return;
        }

        if (string.IsNullOrWhiteSpace(routerNameTextBox.Text))
        {
            throw new InvalidOperationException("Router 名称不能为空。");
        }

        ValidateAmsNetId(localNetIdTextBox.Text, "本机 AMS Net ID");
        if (!IPAddress.TryParse(remoteAddressTextBox.Text.Trim(), out _))
        {
            throw new InvalidOperationException("PLC IP 地址格式不正确。");
        }

        if (string.IsNullOrWhiteSpace(remoteNameTextBox.Text))
        {
            throw new InvalidOperationException("PLC 路由名称不能为空。");
        }

        ValidateAmsNetId(remoteNetIdTextBox.Text, "PLC AMS Net ID");
        ValidateWaveGeneratorPath();
    }

    // 检查波形生成程序路径是否为空且文件是否存在。
    private void ValidateWaveGeneratorPath()
    {
        if (IsWaveMakerModeSelected())
        {
            return;
        }

        var path = waveGeneratorPathTextBox.Text.Trim();
        if (!string.IsNullOrWhiteSpace(path) && !File.Exists(path))
        {
            throw new InvalidOperationException("WFast.exe 路径不存在。");
        }
    }

    // 检查 AMS Net ID 是否为六段字节数字。
    private static void ValidateAmsNetId(string value, string caption)
    {
        var parts = value.Trim().Split('.');
        if (parts.Length != 6 || parts.Any(part => !byte.TryParse(part, out _)))
        {
            throw new InvalidOperationException($"{caption} 必须是六段数字，例如 192.168.1.20.1.1。");
        }
    }

    // 读取指定 App.config 配置项，缺失时使用默认值。
    private static string Read(string key, string fallback = "") =>
        ConfigurationManager.AppSettings[key]?.Trim() is { Length: > 0 } value ? value : fallback;

    // 读取布尔配置项，格式无效时使用默认值。
    private static bool ReadBool(string key, bool fallback) =>
        bool.TryParse(Read(key), out var value) ? value : fallback;

    // 读取端口配置并限制到有效端口范围。
    private static decimal ReadPort(string key, int fallback) =>
        int.TryParse(Read(key), out var value) && value is >= 1 and <= 65535 ? value : fallback;

    // 从连接字符串中读取已有的 SQL Server 账号密码，兼容旧配置。
    private static (string UserName, string Password) ReadDatabaseCredentials(string connectionString)
    {
        try
        {
            var builder = new DbConnectionStringBuilder { ConnectionString = connectionString };
            return (
                ReadConnectionValue(builder, "User ID"),
                ReadConnectionValue(builder, "Password"));
        }
        catch (ArgumentException)
        {
            return (string.Empty, string.Empty);
        }
    }

    // 将页面输入的账号密码写回 SQL Server 连接字符串。
    private static string AddSqlServerCredentials(string connectionString, string userName, string password)
    {
        if (string.IsNullOrWhiteSpace(userName))
        {
            return connectionString;
        }

        var builder = new DbConnectionStringBuilder { ConnectionString = connectionString };
        builder["Integrated Security"] = false;
        builder.Remove("Trusted_Connection");
        builder["User ID"] = userName;
        builder["Password"] = password;
        return builder.ConnectionString;
    }

    // 从连接字符串读取一个键，不存在时返回空字符串。
    private static string ReadConnectionValue(DbConnectionStringBuilder builder, string key) =>
        builder.TryGetValue(key, out var value) ? Convert.ToString(value) ?? string.Empty : string.Empty;

    // 判断当前是否选择 WaveMaker 内置方案。
    private bool IsWaveMakerModeSelected() => waveGeneratorModeComboBox.SelectedIndex == 1;

    // 新增或更新一个 App.config 配置项。
    private static void Set(KeyValueConfigurationCollection settings, string key, string value)
    {
        if (settings[key] is null)
        {
            settings.Add(key, value);
        }
        else
        {
            settings[key]!.Value = value;
        }
    }
}
