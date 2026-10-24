namespace Page_switching;

// 只显示后台快照；复制不会重新采样、申请控制权或发运动命令。
public partial class AxisMonitoringSummaryControl : UserControl
{
    public AxisMonitoringSummaryControl() { InitializeComponent(); }
    internal void ApplyReport(AxisMonitoringReport? report)
    {
        string text = report?.Format() ?? "尚无本软件发起的运行统计。\r\n管理员可在系统配置中设置独立监测阈值；未配置不阻断造波。";
        if (reportTextBox.Text != text) reportTextBox.Text = text;
        copyButton.Enabled = report is not null;
    }
    internal string BuildReportText() => reportTextBox.Text;
    private void CopyButton_Click(object? sender, EventArgs e)
    {
        try { Clipboard.SetText(BuildReportText()); }
        catch (Exception ex) { OperationJournal.Record("运行超限统计", "复制报告失败：" + ex.Message); }
    }
}
