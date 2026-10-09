using System.Configuration;
using System.Text;

namespace Page_switching;

internal static class OperationJournal
{
    private static readonly object FileLock = new();

    internal static event Action<string>? EntryAdded;

    internal static string FilePath => GetFilePath(DateTime.Now);

    internal static string DirectoryPath => ResolveDirectory(ConfigurationManager.AppSettings["OperationLogDirectory"]);

    // 空配置沿用原目录；相对路径从程序目录解析，并支持 %LOCALAPPDATA% 等环境变量。
    internal static string ResolveDirectory(string? configuredDirectory)
    {
        if (string.IsNullOrWhiteSpace(configuredDirectory))
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "PageSwitching", "Operations");

        var expanded = Environment.ExpandEnvironmentVariables(configuredDirectory.Trim());
        return Path.GetFullPath(expanded, AppContext.BaseDirectory);
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

        try { EntryAdded?.Invoke(entry); }
        catch (Exception ex) { System.Diagnostics.Debug.WriteLine("更新日志位置失败：" + ex); }
    }

    private static string GetFilePath(DateTime date) =>
        Path.Combine(DirectoryPath, $"{date:yyyy-MM-dd_HH}.log");
}
