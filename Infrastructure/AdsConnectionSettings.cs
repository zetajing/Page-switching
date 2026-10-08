using System.Collections.Specialized;
using System.Configuration;
using System.Text;
using System.Text.Json;

namespace Page_switching;

internal static class AdsConnectionSettings
{
    private static readonly object SettingsLock = new();
    private static Dictionary<string, string>? _savedValues;
    private static readonly HashSet<string> ConnectionKeys = new(StringComparer.Ordinal)
    {
        "AdsAmsNetId", "AdsPort", "AdsConnectTimeoutMs", "AdsOperationTimeoutMs",
        "AdsTcpRouterEnabled", "AdsTcpRouterName", "AdsTcpRouterLocalNetId",
        "AdsTcpRouterTcpPort", "AdsTcpRouterLoopbackIp", "AdsTcpRouterLoopbackPort",
        "AdsTcpRouterRemoteName", "AdsTcpRouterRemoteAddress", "AdsTcpRouterRemoteNetId"
    };

    internal static string FilePath => Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        "PageSwitching", "AdsConnectionSettings.json");

    internal static string? LoadError { get; private set; }

    // 用户保存的连接参数优先于构建输出中的 App.config，避免重新生成覆盖现场设置。
    internal static NameValueCollection GetSettings()
    {
        var settings = new NameValueCollection(ConfigurationManager.AppSettings);
        lock (SettingsLock)
        {
            foreach (var (key, value) in LoadSavedValues())
                settings[key] = value;
        }

        return settings;
    }

    internal static string? Read(string key)
    {
        lock (SettingsLock)
        {
            if (LoadSavedValues().TryGetValue(key, out var value))
                return value;
        }

        return ConfigurationManager.AppSettings[key];
    }

    // 只保存 ADS 连接和 Router 参数；写入完成后才替换旧文件并更新缓存。
    internal static void Save(params (string Key, string Value)[] values)
    {
        lock (SettingsLock)
        {
            var settings = GetSettings();
            var savedValues = ConnectionKeys.Where(key => settings[key] is not null)
                .ToDictionary(key => key, key => settings[key]!, StringComparer.Ordinal);
            foreach (var (key, value) in values)
            {
                if (!ConnectionKeys.Contains(key))
                    throw new ArgumentException("不支持的 ADS 连接配置项：" + key);
                savedValues[key] = value;
            }

            Directory.CreateDirectory(Path.GetDirectoryName(FilePath)!);
            var temporaryPath = FilePath + "." + Guid.NewGuid().ToString("N") + ".tmp";
            try
            {
                File.WriteAllText(temporaryPath,
                    JsonSerializer.Serialize(savedValues, new JsonSerializerOptions { WriteIndented = true }),
                    Encoding.UTF8);
                File.Move(temporaryPath, FilePath, overwrite: true);
                _savedValues = savedValues;
                LoadError = null;
            }
            finally
            {
                if (File.Exists(temporaryPath))
                    File.Delete(temporaryPath);
            }
        }
    }

    // 同一进程共用一次读取结果；手动修改用户配置后需要重启程序。
    private static Dictionary<string, string> LoadSavedValues()
    {
        if (_savedValues is not null)
            return _savedValues;

        _savedValues = new Dictionary<string, string>(StringComparer.Ordinal);
        if (!File.Exists(FilePath))
            return _savedValues;

        try
        {
            var values = JsonSerializer.Deserialize<Dictionary<string, string>>(File.ReadAllText(FilePath));
            if (values is not null)
                foreach (var (key, value) in values)
                    if (ConnectionKeys.Contains(key) && value is not null)
                        _savedValues[key] = value;
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or JsonException)
        {
            LoadError = ex.Message;
        }

        return _savedValues;
    }
}
