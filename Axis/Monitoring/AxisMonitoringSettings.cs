using System.Text.Json;
using System.Text.Json.Serialization;

namespace Page_switching;

// 分析用阈值，不写入 PLC，也不改变轴的运动配置。
internal sealed record AxisMonitoringThreshold([property: JsonRequired] double MinPosition,
    [property: JsonRequired] double MaxPosition, [property: JsonRequired] double MaxSpeed);
internal sealed record AxisMonitoringProfile([property: JsonRequired] int SchemaVersion,
    [property: JsonRequired] string Version, [property: JsonRequired] DateTimeOffset SavedAt,
    [property: JsonRequired] AxisMonitoringThreshold[] Axes)
{
    internal void Validate()
    {
        if (SchemaVersion != 1 || string.IsNullOrWhiteSpace(Version) || Axes is null || Axes.Length != 4)
            throw new InvalidDataException("监测配置不完整或版本不支持，需要完整四轴阈值。");
        for (int i = 0; i < Axes.Length; i++)
        {
            var axis = Axes[i];
            if (axis is null || !double.IsFinite(axis.MinPosition) || !double.IsFinite(axis.MaxPosition)
                || axis.MinPosition >= axis.MaxPosition || !double.IsFinite(axis.MaxSpeed) || axis.MaxSpeed <= 0)
                throw new InvalidDataException($"轴 {i + 1} 监测阈值无效：位置下限须小于上限，最大速度须为正数，所有值须有限。");
        }
    }

    internal AxisMonitoringProfile Copy() => this with { Axes = Axes.ToArray() };
}

internal static class AxisMonitoringSettings
{
    internal static string FilePath => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        "PageSwitching", "AxisMonitoringSettings.json");

    internal static (AxisMonitoringProfile? Profile, string? Error) Load(string? path = null)
    {
        try
        {
            path ??= FilePath;
            if (!File.Exists(path)) return (null, "监测阈值未配置，未统计。");
            var profile = JsonSerializer.Deserialize<AxisMonitoringProfile>(File.ReadAllText(path))
                ?? throw new InvalidDataException("监测配置为空。");
            profile.Validate();
            return (profile, null);
        }
        catch (Exception ex) { return (null, "监测配置不可用，未统计：" + ex.Message); }
    }

    internal static AxisMonitoringProfile Save(AxisMonitoringThreshold[] axes, LoginUser? user, string? path = null)
    {
        if (user?.IsAdministrator != true) throw new UnauthorizedAccessException("仅管理员可以保存运行监测阈值。");
        var profile = new AxisMonitoringProfile(1, Guid.NewGuid().ToString("N"), DateTimeOffset.UtcNow, axes.ToArray());
        profile.Validate();
        path ??= FilePath;
        Directory.CreateDirectory(Path.GetDirectoryName(Path.GetFullPath(path))!);
        string temporary = path + "." + Guid.NewGuid().ToString("N") + ".tmp";
        try
        {
            File.WriteAllText(temporary, JsonSerializer.Serialize(profile, new JsonSerializerOptions { WriteIndented = true }));
            File.Move(temporary, path, overwrite: true);
        }
        finally { if (File.Exists(temporary)) File.Delete(temporary); }
        OperationJournal.Record("运行监测配置", $"管理员 {user.UserName} 已保存阈值，版本 {profile.Version}；下次运行生效，不修改 PLC。");
        return profile;
    }
}
