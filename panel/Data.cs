using System.ComponentModel;
using System.Text;

namespace Page_switching.panel;

public partial class Data : UserControl
{
    private WaveDataWorkspace? _workspace;

    public Data()
    {
        InitializeComponent();
        if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
        _workspace = WaveDataWorkspace.Shared;
        fromDate.Value = DateTime.Today.AddDays(-30);
        toDate.Value = DateTime.Today;
        channelPicker.Items.Add("全部");
        for (var channel = 1; channel <= 6; channel++) channelPicker.Items.Add($"CH{channel}");
        channelPicker.SelectedIndex = 0;
        refreshButton.Click += (_, _) => RefreshSessions();
        channelPicker.SelectedIndexChanged += (_, _) => RefreshSessions();
        sessionGrid.SelectionChanged += (_, _) => ShowSelectedSession();
        importButton.Click += (_, _) => ImportLegacy();
        exportButton.Click += (_, _) => ExportSelected();
        syncButton.Click += async (_, _) => await SyncAsync();
        VisibleChanged += (_, _) => { if (Visible) RefreshSessions(); };
        RefreshSessions();
    }

    private void RefreshSessions()
    {
        if (_workspace is null) return;
        var selectedId = SelectedManifest()?.SessionId;
        sessionGrid.Rows.Clear();
        sampleGrid.Rows.Clear();
        preview.ClearSamples();
        var channel = SelectedChannel();
        foreach (var manifest in _workspace.ListSessions())
        {
            if (manifest.StartedAt.LocalDateTime.Date < fromDate.Value.Date ||
                manifest.StartedAt.LocalDateTime.Date > toDate.Value.Date ||
                (channel.HasValue && !manifest.Channels.Contains(channel.Value)))
                continue;
            var index = sessionGrid.Rows.Add(manifest.SessionId.ToString("N")[..8],
                manifest.StartedAt.LocalDateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                string.Join(", ", manifest.Channels.Select(x => $"CH{x}")),
                manifest.SampleCount.ToString("N0"), manifest.SyncStatus);
            sessionGrid.Rows[index].Tag = manifest;
            if (manifest.SessionId == selectedId)
                sessionGrid.Rows[index].Selected = true;
        }
        status.Text = $"找到 {sessionGrid.Rows.Count} 个采集任务。";
        if (sessionGrid.Rows.Count > 0 && sessionGrid.SelectedRows.Count == 0)
            sessionGrid.Rows[0].Selected = true;
        ShowSelectedSession();
    }

    private WaveCaptureManifest? SelectedManifest() =>
        sessionGrid.SelectedRows.Count > 0
            ? sessionGrid.SelectedRows[0].Tag as WaveCaptureManifest : null;

    private int? SelectedChannel() => channelPicker.SelectedIndex > 0 ? channelPicker.SelectedIndex : null;

    private void ShowSelectedSession()
    {
        if (_workspace is null) return;
        sampleGrid.Rows.Clear();
        preview.ClearSamples();
        var manifest = SelectedManifest();
        if (manifest is null) return;
        try
        {
            var samples = _workspace.ReadSamples(manifest, SelectedChannel(), 2000);
            foreach (var sample in samples)
                sampleGrid.Rows.Add(sample.Timestamp.LocalDateTime.ToString("HH:mm:ss.fff"),
                    $"CH{sample.Channel}", sample.RawCount?.ToString() ?? "—",
                    sample.CalibratedValue?.ToString("0.####") ?? "未标定");
            var previewChannel = SelectedChannel() ?? manifest.Channels.FirstOrDefault();
            var recent = _workspace.ReadRecentSamples(manifest, previewChannel > 0 ? previewChannel : null);
            var chart = recent.Select(x => x.CalibratedValue ?? x.RawCount ?? 0)
                .ToList();
            preview.UnitText = recent.Any(x => x.CalibratedValue.HasValue) ? "mm" : "count";
            preview.SetSamples(chart, manifest.SampleRateHz > 0 ? 1.0 / manifest.SampleRateHz : 1);
            status.Text = $"任务 {manifest.SessionId:N}：{manifest.SampleCount:N0} 点；表格显示前 2,000 点。文件：{manifest.CsvPath}";
            if (!string.IsNullOrWhiteSpace(manifest.SyncError))
                status.Text += "；数据库：" + manifest.SyncError;
        }
        catch (Exception ex)
        {
            status.Text = "读取采集记录失败：" + ex.Message;
        }
    }

    private void ImportLegacy()
    {
        if (_workspace is null) return;
        using var dialog = new OpenFileDialog
        {
            Filter = "旧波浪采集文件 (*.txt;*.dat;*.csv)|*.txt;*.dat;*.csv|所有文件 (*.*)|*.*"
        };
        if (dialog.ShowDialog(this) != DialogResult.OK) return;
        try
        {
            var manifest = _workspace.ImportLegacyCapture(dialog.FileName);
            fromDate.Value = manifest.StartedAt.LocalDateTime.Date;
            toDate.Value = manifest.StartedAt.LocalDateTime.Date;
            RefreshSessions();
            status.Text = "旧采集文件已导入；旧文件没有原始计数和真实采样时间，所显示时间按文件间隔推算。";
        }
        catch (Exception ex) { status.Text = "导入失败：" + ex.Message; }
    }

    private void ExportSelected()
    {
        var manifest = SelectedManifest();
        if (manifest is null) { status.Text = "请先选择采集任务。"; return; }
        _workspace?.FlushActiveSession();
        using var dialog = new SaveFileDialog
        {
            Filter = "CSV 文件 (*.csv)|*.csv", DefaultExt = "csv", AddExtension = true,
            FileName = $"WaveHeight_{manifest.StartedAt:yyyyMMdd_HHmmss}.csv"
        };
        if (dialog.ShowDialog(this) != DialogResult.OK) return;
        try
        {
            var channel = SelectedChannel();
            if (!channel.HasValue)
                File.Copy(manifest.CsvPath, dialog.FileName, true);
            else
            {
                using var writer = new StreamWriter(dialog.FileName, false, new UTF8Encoding(true));
                writer.WriteLine("Timestamp,Channel,RawCount,CalibratedValue,CalibrationVersion");
                foreach (var line in File.ReadLines(manifest.CsvPath).Skip(1))
                {
                    var fields = line.Split(',');
                    if (fields.Length >= 2 && fields[1] == channel.Value.ToString())
                        writer.WriteLine(line);
                }
            }
            status.Text = "已导出：" + dialog.FileName;
        }
        catch (Exception ex) { status.Text = "导出失败：" + ex.Message; }
    }

    private async Task SyncAsync()
    {
        if (_workspace is null) return;
        syncButton.Enabled = false;
        try
        {
            var count = await _workspace.SynchronizeSessionsAsync();
            RefreshSessions();
            status.Text = $"数据库同步完成：{count} 个任务。数据库未启用时本地记录仍正常保存。";
        }
        catch (Exception ex) { status.Text = "数据库同步失败，本地文件已保留：" + ex.Message; }
        finally { syncButton.Enabled = true; }
    }
}
