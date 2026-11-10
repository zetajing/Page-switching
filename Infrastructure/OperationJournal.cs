using System.Configuration;
using System.Text;

namespace Page_switching;

internal static class OperationJournal
{
    private static readonly object FileLock = new();

    internal static event Action<string>? EntryAdded;

    internal static string FilePath => GetFilePath(DateTime.Now);

    internal static string DirectoryPath => ResolveDirectory(ConfigurationManager.AppSettings["OperationLogDirectory"]);

    // 日志可选程序目录内的子目录，旧版配置指向外部时使用程序目录下的 Operations。
    internal static string ResolveDirectory(string? configuredDirectory)
    {
        if (!string.IsNullOrWhiteSpace(configuredDirectory))
        {
            var expanded = Environment.ExpandEnvironmentVariables(configuredDirectory.Trim());
            var directory = Path.GetFullPath(expanded, AppContext.BaseDirectory);
            var relative = Path.GetRelativePath(AppContext.BaseDirectory, directory);
            if (!Path.IsPathRooted(relative) && relative != ".." &&
                !relative.StartsWith(".." + Path.DirectorySeparatorChar, StringComparison.Ordinal))
                return directory;
        }

        return ProgramDataPaths.InProgramDirectory("Operations");
    }

    internal static void Record(string page, string message)
    {
        var now = DateTime.Now;
        var entry = $"{now:yyyy-MM-dd HH:mm:ss.fff}  [{page}] {message}";
        try
        {
            var path = GetFilePath(now);
            lock (FileLock)
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path)!);
                File.AppendAllText(path, entry + Environment.NewLine, Encoding.UTF8);
            }
        }
        catch (Exception ex)
        {
            entry += $"  [写入日志失败：{ex.Message}]";
        }

        DatabaseArchive.RecordOperation(now, page, message);

        try { EntryAdded?.Invoke(entry); }
        catch (Exception ex) { System.Diagnostics.Debug.WriteLine("更新日志位置失败：" + ex); }
    }

    private static string GetFilePath(DateTime date) =>
        Path.Combine(DirectoryPath, $"{date:yyyy-MM-dd_HH}.log");
}
