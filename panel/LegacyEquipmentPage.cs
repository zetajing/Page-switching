using System.ComponentModel;

namespace Page_switching.panel;

public partial class LegacyEquipmentPage : UserControl
{
    private readonly LegacyFeedbackClient _client = new();
    private readonly CancellationTokenSource _lifetime = new();
    private LegacyProjectSettings? _settings;
    private bool _busy;

    public LegacyEquipmentPage()
    {
        InitializeComponent();
        if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
        try { _settings = LegacyProjectSettings.Load(); properties.SelectedObject = _settings; LoadPoints(); }
        catch (Exception ex) { status.Text = "配置加载失败，原文件已保留：" + ex.Message; }
        browseButton.Click += (_, _) => BrowsePrograms();
        saveButton.Click += (_, _) => Save();
        sensorInput.SelectedIndexChanged += (_, _) => LoadPoints();
        readButton.Click += async (_, _) => await ReadRawAsync();
        fitButton.Click += (_, _) => Fit();
        writeButton.Click += async (_, _) => await WriteAsync();
        properties.PropertyValueChanged += (_, e) => OperationJournal.Record("旧设备配置",
            e.ChangedItem?.Label + "：" + e.ChangedItem?.Value);
        points.CellEndEdit += (_, e) => OperationJournal.Record("反馈标定",
            $"{sensorInput.Text}第{e.RowIndex + 1}行{points.Columns[e.ColumnIndex].HeaderText}：{points.Rows[e.RowIndex].Cells[e.ColumnIndex].Value}");
        connectButton.Click += async (_, _) => await ConnectAsync();
        Disposed += (_, _) => { _lifetime.Cancel(); _client.Dispose(); _lifetime.Dispose(); };
    }

    private void BrowsePrograms()
    {
        if (_settings is null) return;
        using var dialog = new FolderBrowserDialog { Description = "选择旧工程bin/Debug或计算程序目录" };
        if (dialog.ShowDialog(this) != DialogResult.OK) return;
        _settings.ProgramDirectory = dialog.SelectedPath;
        properties.Refresh();
        status.Text = string.Join("；", new[] { "WP-5-6.exe", "WP-7.exe", "WC-12.exe", "WaveTJU-6m-11.exe" }
            .Select(name => name + (File.Exists(Path.Combine(dialog.SelectedPath, name)) ? " 已找到" : " 缺失")));
        OperationJournal.Record("旧设备配置", status.Text);
    }

    private void LoadPoints()
    {
        points.Rows.Clear();
        if (_settings is null) return;
        foreach (var point in sensorInput.SelectedIndex == 0 ? _settings.SensorAPoints : _settings.SensorBPoints)
            points.Rows.Add(WaveSeriesFile.Format(point.RawCount), WaveSeriesFile.Format(point.WaterLevel));
    }

    private void Save()
    {
        if (_settings is null) return;
        try
        {
            _settings.Save();
            status.Text = "设备配置已保存。水池布局和机械参数作为项目记录，未改变旧计算程序的固定模型。";
            OperationJournal.Record("旧设备配置", status.Text);
        }
        catch (Exception ex) { status.Text = "保存失败：" + ex.Message; OperationJournal.Record("旧设备配置", status.Text); }
    }

    private async Task ReadRawAsync()
    {
        if (_busy) return;
        _busy = true;
        readButton.Enabled = false;
        try
        {
            var state = await _client.ReadAsync(_lifetime.Token);
            if (IsDisposed) return;
            var raw = sensorInput.SelectedIndex == 0 ? state.SensorA : state.SensorB;
            points.Rows.Add(raw, "");
            status.Text = "已加入原始反馈值，请填写对应的实测位移(mm)。";
            OperationJournal.Record("反馈标定", sensorInput.Text + " 原始反馈 " + raw);
        }
        catch (Exception ex) { if (!IsDisposed) status.Text = "读取失败：" + ex.Message; OperationJournal.Record("反馈标定", ex.Message); }
        finally { _busy = false; if (!IsDisposed) readButton.Enabled = true; }
    }

    private bool Fit()
    {
        if (_settings is null) return false;
        try
        {
            points.EndEdit();
            var profile = new WaveCalibrationProfile();
            foreach (DataGridViewRow row in points.Rows)
            {
                if (row.IsNewRow) continue;
                profile.Points.Add(new WaveCalibrationPoint(WaveSeriesFile.Number(Convert.ToString(row.Cells[0].Value) ?? ""),
                    WaveSeriesFile.Number(Convert.ToString(row.Cells[1].Value) ?? "")));
            }
            profile.Fit();
            if (sensorInput.SelectedIndex == 0)
            {
                _settings.SensorASlope = profile.Slope; _settings.SensorAIntercept = profile.Intercept; _settings.SensorAPoints = profile.Points;
            }
            else
            {
                _settings.SensorBSlope = profile.Slope; _settings.SensorBIntercept = profile.Intercept; _settings.SensorBPoints = profile.Points;
            }
            _settings.Save();
            properties.Refresh();
            status.Text = $"{sensorInput.Text}：位移 = 原始反馈 × {profile.Slope:0.######} + {profile.Intercept:0.######} mm；已本地保存。";
            OperationJournal.Record("反馈标定", status.Text);
            return true;
        }
        catch (Exception ex) { status.Text = "标定失败：" + ex.Message; OperationJournal.Record("反馈标定", status.Text); return false; }
    }

    private async Task WriteAsync()
    {
        if (_settings is null || _busy) return;
        _busy = true;
        writeButton.Enabled = false;
        try
        {
            _settings.Save();
            await _client.WriteCalibrationAsync(_settings, _lifetime.Token);
            if (!IsDisposed) status.Text = "反馈传感器A/B系数已写入旧PLC。";
        }
        catch (Exception ex) { if (!IsDisposed) status.Text = "写入失败，可能已有部分系数写入：" + ex.Message; OperationJournal.Record("反馈标定", ex.Message); }
        finally { _busy = false; if (!IsDisposed) writeButton.Enabled = true; }
    }

    private async Task ConnectAsync()
    {
        if (_busy) return;
        _busy = true;
        connectButton.Enabled = false;
        try
        {
            await _client.ConnectAsync(_lifetime.Token);
            if (!IsDisposed) status.Text = "反馈PLC已连接，只读取A/B反馈并按按钮写入标定系数。";
        }
        catch (Exception ex) { if (!IsDisposed) status.Text = "连接失败：" + ex.Message; OperationJournal.Record("反馈标定", ex.Message); }
        finally { _busy = false; if (!IsDisposed) connectButton.Enabled = true; }
    }
}
