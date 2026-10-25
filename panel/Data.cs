using System.ComponentModel;
using System.Text;

namespace Page_switching.panel;

public partial class Data : UserControl
{
    private readonly System.Data.DataTable sessionGridRows = new();
    private readonly System.Data.DataTable sampleGridRows = new();
    private WaveDataWorkspace? _workspace;
    private readonly Dictionary<System.Data.DataRow, WaveCaptureManifest> _sessionManifests = new();
    internal event Action<WaveCaptureManifest, int?>? AnalysisRequested;

    public Data()
    {
        InitializeComponent();
        InitializeTables();
        if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
        _workspace = WaveDataWorkspace.Shared;
        fromDate.Value = DateTime.Today.AddDays(-30);
        toDate.Value = DateTime.Today;
        channelPicker.Items.Add("全部");
        for (var channel = 1; channel <= 6; channel++) channelPicker.Items.Add($"CH{channel}");
        channelPicker.SelectedIndex = 0;
        refreshButton.Click += (_, _) => RefreshSessions();
        channelPicker.SelectedIndexChanged += (_, _) => RefreshSessions();
        sessionGrid.SelectIndexChanged += (_, _) => ShowSelectedSession();
        importButton.Click += (_, _) => ImportLegacy();
        exportButton.Click += (_, _) => ExportSelected();
        syncButton.Click += async (_, _) => await SyncAsync();
        legacyExportButton.Click += async (_, _) => await ExportLegacyAsync();
        analyzeButton.Click += (_, _) => OpenSelectedAnalysis();
        VisibleChanged += Data_VisibleChanged;
        RefreshSessions();
    }

    // 页面显示时再刷新列表，隐藏页面保持原有会话选择和采样内容。
    private void Data_VisibleChanged(object? sender, EventArgs e)
    {
        if (Visible)
        {
            RefreshSessions();
        }
    }

    private void InitializeTables()
    {
        TableColumns.Initialize(sessionGrid, sessionGridRows, false,
            "任务 ID", "开始时间", "通道", "样本数", "数据库");

        TableColumns.Initialize(sampleGrid, sampleGridRows, false,
            "采样时间", "通道", "原始计数", "标定值 (mm)");
    }

    // 先刷新采集文件，再请求主窗体切页；失败时保留当前页面和错误提示。
    private void OpenSelectedAnalysis()
    {
        var manifest = SelectedManifest();
        if (manifest is null)
        {
            status.Text = "请先选择采集任务。";
            return;
        }

        try
        {
            _workspace!.FlushActiveSession();
            AnalysisRequested?.Invoke(manifest, SelectedChannel());
        }
        catch (Exception ex)
        {
            status.Text = "打开分析失败：" + ex.Message;
        }
    }

    private void RefreshSessions()
    {
        if (_workspace is null) return;
        var selectedId = SelectedManifest()?.SessionId;
        sessionGridRows.Rows.Clear();
        _sessionManifests.Clear();
        sampleGridRows.Rows.Clear();
        preview.ClearSamples();
        var channel = SelectedChannel();
        foreach (var manifest in _workspace.ListSessions())
        {
            if (manifest.StartedAt.LocalDateTime.Date < (fromDate.Value ?? DateTime.Today).Date ||
                manifest.StartedAt.LocalDateTime.Date > (toDate.Value ?? DateTime.Today).Date ||
                (channel.HasValue && !manifest.Channels.Contains(channel.Value)))
                continue;
            var index = sessionGridRows.Rows.Add(manifest.SessionId.ToString("N")[..8],
                manifest.StartedAt.LocalDateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                string.Join(", ", manifest.Channels.Select(x => $"CH{x}")),
                manifest.SampleCount.ToString("N0"), manifest.SyncStatus);
            _sessionManifests[index] = manifest;

        }
        // 先刷新所有行，再按原任务 ID 恢复选中；AntdUI 不自动监听 DataTable 增删。
        sessionGrid.Refresh();
        sampleGrid.Refresh();
        status.Text = $"找到 {sessionGridRows.Rows.Count} 个采集任务。";
        if (sessionGridRows.Rows.Count > 0)
        {
            var selectedRow = _sessionManifests.FirstOrDefault(pair => pair.Value.SessionId == selectedId).Key
                ?? sessionGridRows.Rows[0];
            sessionGrid.SetSelected(selectedRow, true);
        }
        ShowSelectedSession();
    }

    private WaveCaptureManifest? SelectedManifest()
    {
        var row = sessionGrid.SelectedsReal().OfType<System.Data.DataRow>().FirstOrDefault();
        return row is not null && _sessionManifests.TryGetValue(row, out var manifest) ? manifest : null;
    }

    private int? SelectedChannel() => channelPicker.SelectedIndex > 0 ? channelPicker.SelectedIndex : null;

    private void ShowSelectedSession()
    {
        if (_workspace is null) return;
        sampleGridRows.Rows.Clear();
        preview.ClearSamples();
        var manifest = SelectedManifest();
        if (manifest is null) { sampleGrid.Refresh(); return; }
        try
        {
            var samples = _workspace.ReadSamples(manifest, SelectedChannel(), 2000);
            foreach (var sample in samples)
                sampleGridRows.Rows.Add(sample.Timestamp.LocalDateTime.ToString("HH:mm:ss.fff"),
                    $"CH{sample.Channel}", sample.RawCount?.ToString() ?? "—",
                    sample.CalibratedValue?.ToString("0.####") ?? "未标定");
            sampleGrid.Refresh();
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
        using var dialog = new SaveFileDialog
        {
            Filter = "CSV 文件 (*.csv)|*.csv",
            DefaultExt = "csv",
            AddExtension = true,
            FileName = $"WaveHeight_{manifest.StartedAt:yyyyMMdd_HHmmss}.csv"
        };
        if (dialog.ShowDialog(this) != DialogResult.OK) return;
        try
        {
            // 按通道导出会新建文件，必须先阻止覆盖原始采集文件。
            if (string.Equals(Path.GetFullPath(manifest.CsvPath), Path.GetFullPath(dialog.FileName),
                    StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("导出位置不能是原始采集文件，请选择其他文件名。");
            _workspace?.FlushActiveSession();
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
            File.WriteAllText(dialog.FileName + ".session.json", System.Text.Json.JsonSerializer.Serialize(manifest,
                new System.Text.Json.JsonSerializerOptions { WriteIndented = true }));
            status.Text = "已导出：" + dialog.FileName + "；会话信息已保存在同名.session.json。";
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

    private async Task ExportLegacyAsync()
    {
        var manifest = SelectedManifest();
        if (_workspace is null || manifest is null) { status.Text = "请先选择采集任务。"; return; }
        if (_workspace.ActiveSession?.SessionId == manifest.SessionId)
        { status.Text = "请先结束采集再导出旧格式，以保持各通道样本数量一致。"; return; }
        using var dialog = new SaveFileDialog { Filter = "旧采集文件 (*.txt)|*.txt", DefaultExt = "txt", AddExtension = true };
        if (dialog.ShowDialog(this) != DialogResult.OK) return;
        var channel = SelectedChannel();
        legacyExportButton.Enabled = false;
        try
        {
            await Task.Run(() => _workspace.ExportLegacyCapture(manifest, dialog.FileName, channel));
            if (!IsDisposed) status.Text = "已导出旧格式：" + dialog.FileName + "；标定值单位保持不变，未转换单位。";
            OperationJournal.Record("数据管理", "旧格式导出：" + dialog.FileName);
        }
        catch (Exception ex) { if (!IsDisposed) status.Text = "旧格式导出失败：" + ex.Message; OperationJournal.Record("数据管理", ex.Message); }
        finally { if (!IsDisposed) legacyExportButton.Enabled = true; }
    }
}
