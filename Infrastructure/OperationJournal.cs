using System.Text;

namespace Page_switching;

internal static class OperationJournal
{
    private static readonly object FileLock = new();

    internal static event Action<string>? EntryAdded;

    internal static string FilePath => GetFilePath(DateTime.Now);

    internal static IReadOnlyList<string> ReadRecentEntries(int maximumEntries = 500)
    {
        var now = DateTime.Now;
        var directory = Path.GetDirectoryName(GetFilePath(now))!;
        var entries = new List<string>();
        if (maximumEntries <= 0 || !Directory.Exists(directory))
            return entries;

        // 按小时保存后，仍合并当天历史记录，并兼容原来的按天日志。
        lock (FileLock)
        {
            var paths = Directory.EnumerateFiles(directory, $"{now:yyyy-MM-dd}*.log")
                .OrderByDescending(path => Path.GetFileName(path), StringComparer.Ordinal);
            foreach (var path in paths)
            {
                entries.InsertRange(0, File.ReadLines(path).TakeLast(maximumEntries - entries.Count));
                if (entries.Count >= maximumEntries)
                    break;
            }
        }

        return entries;
    }

    internal static void Record(string page, string message)
    {
        var now = DateTime.Now;
        var entry = $"{now:yyyy-MM-dd HH:mm:ss.fff}  [{page}] {message}";
        var path = GetFilePath(now);
        try
        {
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
        catch (Exception ex) { System.Diagnostics.Debug.WriteLine("显示操作记录失败：" + ex); }
    }

    private static string GetFilePath(DateTime date) => Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        "PageSwitching", "Operations", $"{date:yyyy-MM-dd_HH}.log");
}
