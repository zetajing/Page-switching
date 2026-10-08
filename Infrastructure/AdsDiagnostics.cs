using System.Diagnostics;

namespace Page_switching;

internal static class AdsDiagnostics
{
    // 连接库会包装原始异常；界面显示底层原因，避免只看到通用连接失败提示。
    internal static string DescribeException(Exception exception)
    {
        var cause = exception.GetBaseException();
        return $"{cause.GetType().Name}：{cause.Message}";
    }

    // 每行都带操作日志前缀，重新打开日志和复制记录时仍能保留完整异常链及调用位置。
    internal static void RecordException(string operation, Exception exception)
    {
        Debug.WriteLine(operation + "：" + exception);
        foreach (var line in exception.ToString().Split(
                     new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries))
        {
            OperationJournal.Record("ADS 诊断", operation + "：" + line.Trim());
        }
    }
}
