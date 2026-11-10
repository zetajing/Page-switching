using Microsoft.VisualBasic.FileIO;

namespace Page_switching;

// 本程序自动生成的文件与运行配置放在同一个程序目录下。
internal static class ProgramDataPaths
{
    internal const string ArchiveOriginFile = "legacy-source.txt";
    internal static string LegacyRoot => Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "PageSwitching");

    internal static string InProgramDirectory(string name) => Path.Combine(AppContext.BaseDirectory, name);

    // 在登录和启动后台任务之前搬移，目标已有数据时将旧版数据完整保存在 LegacyData。
    internal static void MigrateLegacyData()
    {
        var sourceRoot = LegacyRoot;
        if (string.Equals(sourceRoot, Path.TrimEndingDirectorySeparator(AppContext.BaseDirectory),
                StringComparison.OrdinalIgnoreCase))
            return;

        foreach (var name in new[] { "users.json", "AxisMonitoringSettings.json" })
        {
            var source = Path.Combine(sourceRoot, name);
            if (File.Exists(source))
                File.Move(source, GetMigrationDestination(name));
        }

        foreach (var name in new[] { "AxisMonitoring", "DatabaseArchive", "Operations", "WaveHeight", "WavePrograms" })
        {
            var source = Path.Combine(sourceRoot, name);
            var pending = InProgramDirectory($".{name}.migration");
            // 固定的中转目录支持跨盘搬移；中断后继续搬剩余文件，再整体启用目标目录。
            if (Directory.Exists(source))
                FileSystem.MoveDirectory(source, pending, overwrite: false);
            if (!Directory.Exists(pending))
                continue;

            var origin = Path.Combine(pending, ArchiveOriginFile);
            if (name == "DatabaseArchive" && !File.Exists(origin))
                File.WriteAllText(origin, Path.Combine(sourceRoot, name));
            Directory.Move(pending, GetMigrationDestination(name));
        }
    }

    private static string GetMigrationDestination(string name)
    {
        var destination = InProgramDirectory(name);
        if (!Path.Exists(destination))
            return destination;

        var backup = InProgramDirectory(Path.Combine("LegacyData", Guid.NewGuid().ToString("N")));
        Directory.CreateDirectory(backup);
        return Path.Combine(backup, name);
    }
}
