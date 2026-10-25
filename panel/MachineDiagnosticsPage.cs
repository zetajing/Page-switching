using System.ComponentModel;
using System.Globalization;
using System.Text;

namespace Page_switching.panel;

// 查看参数和复制诊断不改变控制权，也不向 PLC 写入配置或运动命令。
public partial class MachineDiagnosticsPage : UserControl
{
    private readonly AxisService? _axisService;
    private readonly CancellationTokenSource _lifetime = new();

    public MachineDiagnosticsPage() : this(null) { }

    public MachineDiagnosticsPage(AxisService? axisService)
    {
        InitializeComponent();
        _axisService = axisService;
        diagnosticTextBox.Text = $"上位机版本：{MachineDiagnosticsSnapshot.SoftwareVersion}\r\n尚未读取 PLC 诊断。";
    }

    // 缓存页面每次显示时读取一次；隐藏页面不主动发起诊断请求。
    protected override async void OnVisibleChanged(EventArgs e)
    {
        base.OnVisibleChanged(e);
        if (Visible && LicenseManager.UsageMode != LicenseUsageMode.Designtime && !DesignMode && _axisService is not null)
            await RefreshDiagnosticsAsync();
    }

    internal event EventHandler? BackRequested;
    private void BackButton_Click(object? sender, EventArgs e) => BackRequested?.Invoke(this, e);

    // 主窗体退出时先取消只读请求，再释放共享轴服务。
    internal void CancelRequests()
    {
        if (!_lifetime.IsCancellationRequested) _lifetime.Cancel();
    }

    private async void RefreshButton_Click(object? sender, EventArgs e) => await RefreshDiagnosticsAsync();

    private async Task RefreshDiagnosticsAsync()
    {
        if (_axisService is null || !refreshButton.Enabled) return;
        refreshButton.Enabled = false;
        copyButton.Enabled = false;
        parametersGrid.Rows.Clear(); // 读失败不能沿用上次成功参数。
        diagnosticTextBox.Text = "正在读取 PLC 版本、状态和四轴参数……";
        using var timeout = CancellationTokenSource.CreateLinkedTokenSource(_lifetime.Token);
        timeout.CancelAfter(5000);
        try
        {
            var snapshot = await _axisService.ReadDiagnosticsAsync(timeout.Token);
            if (IsDisposed || Disposing || _lifetime.IsCancellationRequested) return;
            ApplySnapshot(snapshot);
        }
        catch (OperationCanceledException) when (_lifetime.IsCancellationRequested) { }
        catch (Exception ex)
        {
            if (!IsDisposed && !Disposing)
            {
                diagnosticTextBox.Text = $"上位机版本：{MachineDiagnosticsSnapshot.SoftwareVersion}\r\n读取时间：{DateTimeOffset.Now:yyyy-MM-dd HH:mm:ss zzz}\r\n诊断读取失败：{ex.Message}"
                    + (string.IsNullOrWhiteSpace(_axisService.LastWaveFailure) ? "" : "\r\n最近一次造波操作失败记录：" + _axisService.LastWaveFailure);
                OperationJournal.Record("运行诊断", diagnosticTextBox.Text);
            }
        }
        finally
        {
            if (!IsDisposed && !Disposing)
            {
                refreshButton.Enabled = true;
                copyButton.Enabled = true;
            }
        }
    }

    internal void ApplySnapshot(MachineDiagnosticsSnapshot snapshot)
    {
        parametersGrid.Rows.Clear();
        // 失败记录只由服务保存；复制使用已显示的文字，不再读取或另存第二份记录。
        diagnosticTextBox.Text = snapshot.Format(_axisService?.LastWaveFailure);
        if (snapshot.Limits is null) return;
        for (int axis = 0; axis < snapshot.Limits.Length; axis++)
        {
            var p = snapshot.Limits[axis];
            parametersGrid.Rows.Add(axis + 1, Number(p.MinPosition), Number(p.MaxPosition),
                Number(p.MaxVelocity), Number(p.MaxAcceleration), Number(p.StopDeceleration),
                Number(p.StartTolerance), Number(p.InitVelocity));
        }
    }

    // G17 保留诊断所需精度，复制内容与屏幕同一份快照。
    private static string Number(double value) => value.ToString("G17", CultureInfo.InvariantCulture);

    internal string BuildDiagnosticText()
    {
        var text = new StringBuilder(diagnosticTextBox.Text);
        text.AppendLine().AppendLine("四轴参数（PLC/NC 原始单位）");
        text.AppendLine(string.Join("\t", parametersGrid.Columns.Cast<DataGridViewColumn>().Select(column => column.HeaderText)));
        foreach (DataGridViewRow row in parametersGrid.Rows)
            text.AppendLine(string.Join("\t", row.Cells.Cast<DataGridViewCell>().Select(cell => cell.Value?.ToString() ?? "--")));
        return text.ToString();
    }

    private void CopyButton_Click(object? sender, EventArgs e)
    {
        try
        {
            Clipboard.SetText(BuildDiagnosticText());
            OperationJournal.Record("运行诊断", "已复制本次诊断和四轴只读参数。");
        }
        catch (Exception ex) { OperationJournal.Record("运行诊断", "复制失败：" + ex.Message); }
    }
}
