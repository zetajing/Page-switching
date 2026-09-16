using System.Configuration;
using System.Net;

namespace Page_switching.panel;

public partial class Config : UserControl
{
    public Config()
    {
        InitializeComponent();
        LoadSettings();
    }

    private void LoadSettings()
    {
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
            SaveWaveGeneratorButton_Click(sender, EventArgs.Empty);
        }
    }

    private void WaveGeneratorPathTextBox_TextChanged(object? sender, EventArgs e)
    {
        UpdateWaveGeneratorState();
    }

    private void SaveWaveGeneratorButton_Click(object? sender, EventArgs e)
    {
        try
        {
            var path = waveGeneratorPathTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new InvalidOperationException("请先选择 WFast.exe 文件。");
            }

            if (!File.Exists(path))
            {
                throw new InvalidOperationException("WFast.exe 路径不存在。");
            }

            var configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            Set(configuration.AppSettings.Settings, "WaveGeneratorPath", path);
            configuration.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");

            waveGeneratorStateLabel.Text = "已保存 WFast.exe 路径，波形页面可以直接生成";
            waveGeneratorStateLabel.ForeColor = Color.FromArgb(5, 150, 105);
            saveResultLabel.ForeColor = Color.FromArgb(5, 150, 105);
            saveResultLabel.Text = "WFast.exe 路径保存成功。";
        }
        catch (Exception ex)
        {
            saveResultLabel.ForeColor = Color.FromArgb(220, 38, 38);
            saveResultLabel.Text = "WFast 路径保存失败：" + ex.Message;
        }
    }

    private void UpdateWaveGeneratorState()
    {
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

    private void RouterEnabledCheckBox_CheckedChanged(object? sender, EventArgs e)
    {
        UpdateInputState();
    }

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

    private void SaveRouterButton_Click(object? sender, EventArgs e)
    {
        try
        {
            ValidateInputs();
            var configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var settings = configuration.AppSettings.Settings;
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
            saveResultLabel.Text = "保存成功；Router 和 ADS 配置重启后生效，WFast 路径立即可用于波形生成。";
        }
        catch (Exception ex)
        {
            saveResultLabel.ForeColor = Color.FromArgb(220, 38, 38);
            saveResultLabel.Text = "保存失败：" + ex.Message;
        }
    }

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

    private void ValidateWaveGeneratorPath()
    {
        var path = waveGeneratorPathTextBox.Text.Trim();
        if (!string.IsNullOrWhiteSpace(path) && !File.Exists(path))
        {
            throw new InvalidOperationException("WFast.exe 路径不存在。");
        }
    }

    private static void ValidateAmsNetId(string value, string caption)
    {
        var parts = value.Trim().Split('.');
        if (parts.Length != 6 || parts.Any(part => !byte.TryParse(part, out _)))
        {
            throw new InvalidOperationException($"{caption} 必须是六段数字，例如 192.168.1.20.1.1。");
        }
    }

    private static string Read(string key, string fallback = "") =>
        ConfigurationManager.AppSettings[key]?.Trim() is { Length: > 0 } value ? value : fallback;

    private static bool ReadBool(string key, bool fallback) =>
        bool.TryParse(Read(key), out var value) ? value : fallback;

    private static decimal ReadPort(string key, int fallback) =>
        int.TryParse(Read(key), out var value) && value is >= 1 and <= 65535 ? value : fallback;

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
