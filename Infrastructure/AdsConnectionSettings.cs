using System.Collections.Specialized;
using System.Configuration;
using System.Xml.Linq;

namespace Page_switching;

internal static class AdsConnectionSettings
{
    private const string LegacyPlcNetIdKey = "AdsTcpRouterRemoteNetId";
    private static readonly HashSet<string> ConnectionKeys = new(StringComparer.Ordinal)
    {
        "AdsAmsNetId", "AdsPort", "AdsConnectTimeoutMs", "AdsOperationTimeoutMs",
        "AdsTcpRouterEnabled", "AdsTcpRouterName", "AdsTcpRouterLocalNetId",
        "AdsTcpRouterTcpPort", "AdsTcpRouterLoopbackIp", "AdsTcpRouterLoopbackPort",
        "AdsTcpRouterRemoteName", "AdsTcpRouterRemoteAddress",
        ControlStationIdentity.SettingKey
    };

    internal static string FilePath => ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None).FilePath;

    // 从运行目录的配置文件读取 ADS 参数；通信服务使用本次启动的快照。
    internal static NameValueCollection GetSettings()
    {
        var settings = new NameValueCollection(ConfigurationManager.AppSettings);
        // 兼容只填写旧 Router 目标的 App.config；两项都有值时以 AdsAmsNetId 为准。
        settings["AdsAmsNetId"] ??= settings[LegacyPlcNetIdKey];
        settings.Remove(LegacyPlcNetIdKey);
        return settings;
    }

    internal static string? Read(string key) => GetSettings()[key];

    // 页面和启动读取使用同一份配置；只更新传入的 ADS 字段。
    internal static string Save(params (string Key, string Value)[] values)
    {
        var configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        var section = configuration.AppSettings.SectionInformation;
        // 修改原始 XML，让配置保存后仍保留 PLC 目标和独立 Router 的分组注释。
        var settings = section.GetRawXml() is { Length: > 0 } xml
            ? XElement.Parse(xml, LoadOptions.PreserveWhitespace)
            : new XElement("appSettings");
        XElement? FindSetting(string key) => settings.Elements("add").LastOrDefault(element =>
            string.Equals((string?)element.Attribute("key"), key, StringComparison.OrdinalIgnoreCase));

        if (FindSetting(LegacyPlcNetIdKey) is { } legacyPlcNetId)
        {
            if (FindSetting("AdsAmsNetId") is null)
                legacyPlcNetId.SetAttributeValue("key", "AdsAmsNetId");
            else
                legacyPlcNetId.Remove();
        }

        foreach (var (key, value) in values)
        {
            if (!ConnectionKeys.Contains(key))
                throw new ArgumentException("不支持的 ADS 配置项：" + key);
            if (FindSetting(key) is { } existing)
                existing.SetAttributeValue("value", value);
            else
                settings.Add(new XElement("add", new XAttribute("key", key), new XAttribute("value", value)));
        }

        section.SetRawXml(settings.ToString(SaveOptions.DisableFormatting));
        configuration.Save(ConfigurationSaveMode.Modified);
        ConfigurationManager.RefreshSection("appSettings");
        return configuration.FilePath;
    }
}
