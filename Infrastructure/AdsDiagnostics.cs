using System.Diagnostics;

namespace Page_switching;

internal static class AdsDiagnostics
{
    // 同时保留失败步骤和底层原因；只取根异常会丢失变量名、请求阶段或撤销失败提示。
    internal static string DescribeException(Exception exception)
    {
        if (exception is AggregateException aggregate)
        {
            // 提交和撤销可能同时失败，两个原因都要显示，不能只保留其中一个。
            return string.Join("；", aggregate.InnerExceptions.Select(DescribeException));
        }

        string message = $"{exception.GetType().Name}：{exception.Message}";
        if (exception.InnerException is not null)
        {
            string cause = DescribeException(exception.InnerException);
            if (!message.Contains(cause, StringComparison.Ordinal))
                message += " → " + cause;
        }
        return message;
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
