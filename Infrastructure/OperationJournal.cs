using System.Text;

namespace Page_switching;

internal static class OperationJournal
{
    private static readonly object FileLock = new();

    internal static event Action<string>? EntryAdded;

    internal static string FilePath => GetFilePath(DateTime.Now);

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
        "PageSwitching", "Operations", $"{date:yyyy-MM-dd}.log");
}
