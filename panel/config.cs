using System.Configuration;
using System.Data.Common;
using System.Net;

namespace Page_switching.panel;

public partial class Config : UserControl
{
    private readonly LoginUser? _currentUser;
    internal event EventHandler? MonitoringSettingsRequested;
    // 初始化配置页面并加载当前 App.config 设置。
    public Config() : this(null) { }

    internal Config(LoginUser? user)
    {
        _currentUser = user;
        InitializeComponent();
        monitoringSettingsButton.Enabled = user?.IsAdministrator == true;
        LoadSettings();
        LoadDatabaseSettings();
    }

    private void MonitoringSettingsButton_Click(object? sender, EventArgs e)
    {
        if (_currentUser?.IsAdministrator != true) return;
        // 配置页只提出跳转请求，页面实例和权限统一由主窗体管理。
        MonitoringSettingsRequested?.Invoke(this, e);
    }

    // 加载 ADS 和波形程序配置后，再按当前选项更新控件可用状态。
    private void LoadSettings()
    {
        LoadWaveGeneratorSettings();
        LoadRouterSettings();
        UpdateWaveGeneratorState();
        UpdateInputState();
    }

    private void LoadWaveGeneratorSettings()
    {
        waveGeneratorModeComboBox.Items.AddRange(new object[] { "外部 WFast.exe", "WaveMaker 内置算法", "旧 WP-5-6.exe" });
        string mode = Read("WaveGeneratorMode", "ExternalExe");
        if (mode == "WaveMaker")
        {
            waveGeneratorModeComboBox.SelectedIndex = 1;
        }
        else if (mode == "LegacyExe")
        {
            waveGeneratorModeComboBox.SelectedIndex = 2;
        }
        else
        {
            waveGeneratorModeComboBox.SelectedIndex = 0;
        }
        waveGeneratorPathTextBox.Text = Read("WaveGeneratorPath");
        waveProgramDirectoryTextBox.Text = WaveProgramSettings.ProgramDirectory;
    }

    private void LoadRouterSettings()
    {
        routerEnabledCheckBox.Checked = ReadBool("AdsTcpRouterEnabled", false);
        routerNameTextBox.Text = Read("AdsTcpRouterName", "PageSwitchingRouter");
        localNetIdTextBox.Text = Read("AdsTcpRouterLocalNetId");
        routerTcpPortInput.Value = ReadPort("AdsTcpRouterTcpPort", 48898);
        remoteNameTextBox.Text = Read("AdsTcpRouterRemoteName", "WaveMakerPlc");
        remoteAddressTextBox.Text = Read("AdsTcpRouterRemoteAddress");
        // 两种模式都先显示程序实际连接的 PLC；旧配置里的 Router 路由可能指向其他目标。
        remoteNetIdTextBox.Text = Read("AdsAmsNetId", Read("AdsTcpRouterRemoteNetId"));
        // 只显示 PLC 的 ADS 端口，独立 Router 的 TCP 监听端口在下方单独设置。
        plcAdsPortLabel.Text = $"PLC ADS 端口：{ReadPort("AdsPort", 851)}";
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
            ? "保存操作日志、造波点位、运行及采样；后台补传不参与PLC控制。"
            : "数据库未启用，本地归档继续保存。";
        _databaseStateLabel.ForeColor = enabled
            ? UiPalette.Success
            : UiPalette.Muted;
    }

    // 验证并保存数据库配置到 App.config。
    private void SaveDatabaseButton_Click(object? sender, EventArgs e)
    {
        if (_currentUser?.IsAdministrator != true) return;
        try
        {
            var connectionString = _databaseConnectionTextBox.Text.Trim();
            var userName = _databaseUserNameTextBox.Text.Trim();
            var password = _databasePasswordTextBox.Text;
            connectionString = ValidateDatabaseSettings(connectionString, userName, password);

            SaveSettings(
                ("DatabaseEnabled", _databaseEnabledCheckBox.Checked.ToString().ToLowerInvariant()),
                ("DatabaseProvider", "SQL Server"),
                ("DatabaseConnectionString", connectionString),
                ("DatabaseUserName", userName),
                ("DatabasePassword", password));

            _databaseConnectionTextBox.Text = connectionString;

            _databaseStateLabel.ForeColor = UiPalette.Success;
            _databaseStateLabel.Text = "配置已保存，后台每5秒补传；可从数据管理进入数据库记录。";
            ShowSaveResult("数据库配置保存成功。", UiPalette.Success);
        }
        catch (Exception ex)
        {
            ShowSaveResult("数据库配置保存失败：" + ex.Message, UiPalette.Danger);
        }
    }

    // 明确点击才连接服务器并建表，配置编辑和打开页面均不访问数据库。
    private async void TestDatabaseButton_Click(object? sender, EventArgs e)
    {
        if (_currentUser?.IsAdministrator != true) return;
        _testDatabaseButton.Enabled = false;
        using var timeout = new CancellationTokenSource(TimeSpan.FromSeconds(20));
        try
        {
            string connection = ValidateDatabaseSettings(_databaseConnectionTextBox.Text.Trim(),
                _databaseUserNameTextBox.Text.Trim(), _databasePasswordTextBox.Text);
            await DatabaseStore.InitializeAsync(DatabaseStore.Normalize(connection), timeout.Token);
            if (!IsDisposed) _databaseStateLabel.Text = "连接成功，8张数据表已就绪；请保存配置后启用同步。";
            OperationJournal.Record("数据库", "连接测试及数据表初始化成功");
        }
        catch (Exception ex)
        {
            if (!IsDisposed) _databaseStateLabel.Text = "连接或初始化失败：" + ex.Message;
        }
        finally { if (!IsDisposed) _testDatabaseButton.Enabled = true; }
    }

    // 校验通过后才将页面账号密码写入连接字符串。
    private string ValidateDatabaseSettings(string connectionString, string userName, string password)
    {
        if (_databaseEnabledCheckBox.Checked && string.IsNullOrWhiteSpace(connectionString))
            throw new InvalidOperationException("启用数据库日志时，连接字符串不能为空。");
        if (!string.IsNullOrWhiteSpace(password) && string.IsNullOrWhiteSpace(userName))
            throw new InvalidOperationException("填写数据库密码时，账号不能为空。");
        // 保存与连接测试使用相同校验，禁止将无效配置交给后台补传。
        if (string.IsNullOrWhiteSpace(connectionString)) return connectionString;
        connectionString = DatabaseStore.Normalize(connectionString);
        return DatabaseStore.Normalize(AddSqlServerCredentials(connectionString, userName, password));
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

    private void BrowseWaveProgramDirectoryButton_Click(object? sender, EventArgs e)
    {
        using var dialog = new FolderBrowserDialog
        {
            Description = "选择包含WP-5-6.exe、WP-7.exe或WC-12.exe的波形程序目录",
            SelectedPath = waveProgramDirectoryTextBox.Text.Trim(),
            UseDescriptionForTitle = true
        };
        if (dialog.ShowDialog(this) == DialogResult.OK) waveProgramDirectoryTextBox.Text = dialog.SelectedPath;
    }

    private void SaveWaveProgramDirectoryButton_Click(object? sender, EventArgs e)
    {
        try
        {
            ValidateWaveProgramDirectory();
            SaveSettings(("WaveProgramDirectory", waveProgramDirectoryTextBox.Text.Trim()));
            ShowSaveResult("波形程序目录已保存，供波形生成、分析和修正使用。", UiPalette.Success);
        }
        catch (Exception ex)
        {
            ShowSaveResult("波形程序目录保存失败：" + ex.Message, UiPalette.Danger);
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
            ValidateWaveProgramDirectory(requireGenerator: true);
            if (IsLegacyModeSelected())
            {
                SaveSettings(("WaveGeneratorMode", nameof(WaveformGeneratorMode.LegacyExe)),
                    ("WaveProgramDirectory", waveProgramDirectoryTextBox.Text.Trim()));
                ShowSaveResult("已启用旧WP-5-6.exe，输出旧格式造波板位移。", UiPalette.Success);
                UpdateWaveGeneratorState();
                return;
            }
            if (!useWaveMaker && string.IsNullOrWhiteSpace(path))
            {
                throw new InvalidOperationException("请先选择 WFast.exe 文件。");
            }

            if (!useWaveMaker && !File.Exists(path))
            {
                throw new InvalidOperationException("WFast.exe 路径不存在。");
            }

            var mode = useWaveMaker ? nameof(WaveformGeneratorMode.WaveMaker)
                : nameof(WaveformGeneratorMode.ExternalExe);
            SaveSettings(
                ("WaveGeneratorMode", mode),
                ("WaveGeneratorPath", path),
                ("WaveProgramDirectory", waveProgramDirectoryTextBox.Text.Trim()));

            waveGeneratorStateLabel.Text = useWaveMaker
                ? "已启用 WaveMaker 内置算法，不需要 WFast.exe"
                : "已保存 WFast.exe 路径，波形页面可以直接生成";
            waveGeneratorStateLabel.ForeColor = UiPalette.Success;
            ShowSaveResult(useWaveMaker ? "WaveMaker 内置方案保存成功。" : "WFast.exe 方案保存成功。", UiPalette.Success);
        }
        catch (Exception ex)
        {
            ShowSaveResult("波形程序设置保存失败：" + ex.Message, UiPalette.Danger);
        }
    }

    // 根据文件是否存在更新路径状态提示。
    private void UpdateWaveGeneratorState()
    {
        var useWaveMaker = IsWaveMakerModeSelected();
        waveGeneratorPathTextBox.Enabled = !useWaveMaker && !IsLegacyModeSelected();
        browseWaveGeneratorButton.Enabled = !useWaveMaker && !IsLegacyModeSelected();
        if (IsLegacyModeSelected())
        {
            waveGeneratorStateLabel.Text = "从下方波形程序目录调用WP-5-6.exe，生成旧格式造波板位移。";
            waveGeneratorStateLabel.ForeColor = UiPalette.SecondaryText;
            return;
        }
        if (useWaveMaker)
        {
            waveGeneratorStateLabel.Text = "已选择 WaveMaker 内置算法，不需要配置 WFast.exe";
            waveGeneratorStateLabel.ForeColor = UiPalette.Success;
            return;
        }

        var path = waveGeneratorPathTextBox.Text.Trim();
        if (string.IsNullOrWhiteSpace(path))
        {
            waveGeneratorStateLabel.Text = "未配置，波形生成页面运行时会提示设置路径";
            waveGeneratorStateLabel.ForeColor = UiPalette.Warning;
        }
        else if (File.Exists(path))
        {
            waveGeneratorStateLabel.Text = "已找到 WFast.exe";
            waveGeneratorStateLabel.ForeColor = UiPalette.Success;
        }
        else
        {
            waveGeneratorStateLabel.Text = "路径不存在，请重新选择 WFast.exe";
            waveGeneratorStateLabel.ForeColor = UiPalette.Danger;
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
        // PLC 目标在系统 Router 和独立 Router 模式下都需要可编辑。
        remoteNetIdTextBox.Enabled = true;
        routerStateLabel.Text = enabled ? "独立 Router：启用（重启后生效）" : "独立 Router：关闭，使用系统 TwinCAT Router";
        routerStateLabel.ForeColor = enabled ? UiPalette.Success : UiPalette.Muted;
    }

    // 验证并保存 ADS TCP Router 配置。
    private void SaveRouterButton_Click(object? sender, EventArgs e)
    {
        try
        {
            ValidateRouterInputs();
            AdsConnectionSettings.Save(
                ("AdsTcpRouterEnabled", routerEnabledCheckBox.Checked.ToString().ToLowerInvariant()),
                ("AdsTcpRouterName", routerNameTextBox.Text.Trim()),
                ("AdsTcpRouterLocalNetId", localNetIdTextBox.Text.Trim()),
                ("AdsTcpRouterTcpPort", decimal.ToInt32(routerTcpPortInput.Value).ToString()),
                ("AdsTcpRouterLoopbackIp", "127.0.0.1"),
                ("AdsTcpRouterLoopbackPort", decimal.ToInt32(routerTcpPortInput.Value).ToString()),
                ("AdsTcpRouterRemoteName", remoteNameTextBox.Text.Trim()),
                ("AdsTcpRouterRemoteAddress", remoteAddressTextBox.Text.Trim()),
                ("AdsTcpRouterRemoteNetId", remoteNetIdTextBox.Text.Trim()),
                ("AdsAmsNetId", remoteNetIdTextBox.Text.Trim()));

            ShowSaveResult("保存成功；ADS连接设置已保留，重启后生效。", UiPalette.Success,
                $"ADS连接配置已保存：独立Router={routerEnabledCheckBox.Checked}，PLC IP={remoteAddressTextBox.Text.Trim()}，目标AMS Net ID={remoteNetIdTextBox.Text.Trim()}；重启后生效。");
        }
        catch (Exception ex)
        {
            ShowSaveResult("保存失败：" + ex.Message, UiPalette.Danger, "ADS连接配置保存失败：" + ex.Message);
        }
    }

    // 显示与记录共用一个入口，重复保存同样的结果也能留下明确记录。
    private void ShowSaveResult(string message, Color color, string? logMessage = null)
    {
        saveResultLabel.ForeColor = color;
        saveResultLabel.Text = message;
        OperationJournal.Record("系统配置", logMessage ?? message);
    }

    // 两种模式都检查 PLC 目标；独立 Router 才需要检查本机和路由参数。
    private void ValidateRouterInputs()
    {
        ValidateAmsNetId(remoteNetIdTextBox.Text, "PLC AMS Net ID");
        if (!routerEnabledCheckBox.Checked)
        {
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
    }

    private void ValidateWaveProgramDirectory(bool requireGenerator = false)
    {
        var directory = waveProgramDirectoryTextBox.Text.Trim();
        if (directory.Length > 0 && !Directory.Exists(directory))
            throw new InvalidOperationException("波形程序目录不存在。");
        if (requireGenerator && IsLegacyModeSelected() && (directory.Length == 0 || !File.Exists(Path.Combine(directory, "WP-5-6.exe"))))
            throw new InvalidOperationException("请在波形程序目录中选择包含WP-5-6.exe的文件夹。");
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

    // ADS 连接参数优先读取用户保存值，其余配置沿用 App.config。
    private static string Read(string key, string fallback = "")
    {
        string? value = AdsConnectionSettings.Read(key);
        if (value == null)
        {
            return fallback;
        }

        value = value.Trim();
        if (value.Length == 0)
        {
            return fallback;
        }
        return value;
    }

    // 读取布尔配置项，格式无效时使用默认值。
    private static bool ReadBool(string key, bool fallback)
    {
        bool value;
        if (bool.TryParse(Read(key), out value))
        {
            return value;
        }
        return fallback;
    }

    // 读取端口配置并限制到有效端口范围。
    private static decimal ReadPort(string key, int fallback)
    {
        int value;
        if (int.TryParse(Read(key), out value) && value >= 1 && value <= 65535)
        {
            return value;
        }
        return fallback;
    }

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
    private bool IsLegacyModeSelected() => waveGeneratorModeComboBox.SelectedIndex == 2;

    private static void SaveSettings(params (string Key, string Value)[] values)
    {
        var configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        foreach (var (key, value) in values)
            Set(configuration.AppSettings.Settings, key, value);
        configuration.Save(ConfigurationSaveMode.Modified);
        ConfigurationManager.RefreshSection("appSettings");
    }

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
