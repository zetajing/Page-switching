using System.ComponentModel;
using System.Data;
using System.Text;

namespace Page_switching.panel;

// 查询和导出只读；配置及建表仍在管理员的系统配置页面。
public partial class DatabasePage : UserControl
{
    private readonly CancellationTokenSource _requests = new();
    private bool _busy;
    internal event EventHandler? BackRequested;

    public DatabasePage()
    {
        InitializeComponent();
        kindPicker.Items.AddRange(DatabaseStore.Names);
        kindPicker.SelectedIndex = 0;
        fromDate.Value = DateTime.Today.AddDays(-7);
        toDate.Value = DateTime.Now;
        VisibleChanged += DatabasePage_VisibleChanged;
    }

    private void DatabasePage_VisibleChanged(object? sender, EventArgs e)
    {
        if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
        statusTimer.Enabled = Visible;
        if (Visible) UpdateSyncStatus();
    }

    private void StatusTimer_Tick(object? sender, EventArgs e) => UpdateSyncStatus();
    private void UpdateSyncStatus() => syncStatus.Text = DatabaseSyncService.Shared.Status
        + (DatabaseArchive.LastError is null ? "" : "；" + DatabaseArchive.LastError);
    private void BackButton_Click(object? sender, EventArgs e) => BackRequested?.Invoke(this, e);
    internal void CancelRequests() => _requests.Cancel();

    private async void QueryButton_Click(object? sender, EventArgs e)
    {
        if (_busy) return;
        SetBusy(true);
        using var timeout = CancellationTokenSource.CreateLinkedTokenSource(_requests.Token);
        timeout.CancelAfter(TimeSpan.FromSeconds(20));
        try
        {
            // 先读取页面输入，再异步查询；查询结果与后续改动的筛选条件分开。
            int kind = kindPicker.SelectedIndex;
            var rows = await DatabaseStore.QueryAsync(kind, new DateTimeOffset(fromDate.Value),
                new DateTimeOffset(toDate.Value), taskId.Text, timeout.Token);
            if (IsDisposed || _requests.IsCancellationRequested) { rows.Dispose(); return; }
            var previous = grid.DataSource as DataTable;
            grid.DataSource = rows;
            previous?.Dispose();
            resultStatus.Text = kind == 2 ? $"显示1块点位，共{rows.Rows.Count}点；并非整份文件。" : $"显示最近{rows.Rows.Count}条记录（上限500条）。";
        }
        catch (OperationCanceledException) { if (!IsDisposed) resultStatus.Text = "查询已取消或超时。"; }
        catch (Exception ex) { if (!IsDisposed) resultStatus.Text = "查询失败：" + ex.Message; }
        finally { if (!IsDisposed) SetBusy(false); }
    }

    private async void SyncButton_Click(object? sender, EventArgs e)
    {
        if (_busy) return;
        SetBusy(true);
        try { await DatabaseSyncService.Shared.SynchronizeAsync(_requests.Token); }
        catch (OperationCanceledException) { }
        catch (Exception ex) { if (!IsDisposed) resultStatus.Text = "同步失败，本地归档保留：" + ex.Message; }
        finally { if (!IsDisposed) { SetBusy(false); UpdateSyncStatus(); } }
    }

    // 导出当前查询结果，避免把截断的预览误写为完整历史。
    private void ExportButton_Click(object? sender, EventArgs e)
    {
        if (grid.DataSource is not DataTable table || table.Rows.Count == 0)
        { resultStatus.Text = "请先查询到记录。"; return; }
        using var dialog = new SaveFileDialog { Filter = "查询结果 (*.csv)|*.csv", FileName = "数据库查询.csv" };
        if (dialog.ShowDialog(this) != DialogResult.OK) return;
        try
        {
            using var writer = new StreamWriter(dialog.FileName, false, new UTF8Encoding(true));
            writer.WriteLine(string.Join(",", table.Columns.Cast<DataColumn>().Select(column => Quote(column.ColumnName))));
            foreach (DataRow row in table.Rows) writer.WriteLine(string.Join(",", row.ItemArray.Select(value => Quote(Convert.ToString(value, System.Globalization.CultureInfo.InvariantCulture) ?? ""))));
            resultStatus.Text = "已导出当前查询结果：" + dialog.FileName;
            OperationJournal.Record("数据库", resultStatus.Text);
        }
        catch (Exception ex) { resultStatus.Text = "导出失败：" + ex.Message; }
    }
    private static string Quote(string value) => "\"" + value.Replace("\"", "\"\"") + "\"";
    private void SetBusy(bool busy)
    {
        _busy = busy; queryButton.Enabled = !busy; syncButton.Enabled = !busy;
        exportButton.Enabled = !busy; filters.Enabled = !busy;
    }
}
