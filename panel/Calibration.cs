using System.ComponentModel;
using System.Globalization;

namespace Page_switching.panel;

public partial class Calibration : UserControl
{
    private readonly System.Data.DataTable pointsGridRows = new();
    private WaveDataWorkspace? _workspace;
    private WaveCalibrationProfile? _profile;

    public Calibration()
    {
        InitializeComponent();
        InitializeTables();
        if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;

        _workspace = WaveDataWorkspace.Shared;
        for (var channel = 1; channel <= 6; channel++)
            channelPicker.Items.Add($"CH{channel}");
        channelPicker.SelectedIndexChanged += (_, _) => LoadChannel();
        addButton.Click += (_, _) => AddCurrentPoint();
        removeButton.Click += (_, _) => RemoveSelectedPoint();
        zeroButton.Click += (_, _) => SetZero();
        fitButton.Click += (_, _) => Fit();
        saveButton.Click += (_, _) => Save();
        importButton.Click += (_, _) => Import();
        exportButton.Click += (_, _) => Export();
        batchButton.Click += (_, _) => BatchCalibrationRequested?.Invoke(this, EventArgs.Empty);
        _workspace.SampleRecorded += Workspace_SampleRecorded;
        Disposed += Calibration_Disposed;
        channelPicker.SelectedIndex = 0;
    }

    // 页面只发出切换请求，具体显示由主窗体处理。
    internal event EventHandler? BatchCalibrationRequested;

    private void InitializeTables()
    {
        TableColumns.Initialize(pointsGrid, pointsGridRows, true,
            "原始计数", "标定水位 (mm)");
        pointsGrid.EditMode = AntdUI.TEditMode.DoubleClick;
        pointsGrid.MultipleRows = true;
    }

    // 批量标定保存成功后，主窗体同步刷新这里的通道系数。
    internal void LoadChannel()
    {
        if (_workspace is null || channelPicker.SelectedIndex < 0) return;
        var channel = channelPicker.SelectedIndex + 1;
        _profile = _workspace.GetCalibration(channel);
        pointsGridRows.Rows.Clear();
        foreach (var point in _profile.Points)
            pointsGridRows.Rows.Add(point.RawCount.ToString("R", CultureInfo.InvariantCulture),
                point.WaterLevel.ToString("R", CultureInfo.InvariantCulture));
        pointsGrid.Refresh();
        rawValue.Text = _workspace.GetLatestRaw(channel)?.ToString(CultureInfo.CurrentCulture) ?? "--";
        result.Text = $"CH{channel}：零点 {_profile.ZeroRawCount:0.###}，斜率 {_profile.Slope:0.######}，截距 {_profile.Intercept:0.######}；版本 {_profile.Version[..8]}";
        if (channel > 1)
            result.Text += "。设备协议待验证，暂不能读取实时值。";
        if (_workspace.CalibrationLoadError is { } error)
            result.Text += "；" + error;
    }

    private void Workspace_SampleRecorded(object? sender, WaveSample sample)
    {
        // 先切回界面线程，再读取控件；关闭或尚未创建句柄时忽略刷新。
        if (IsDisposed || Disposing || !IsHandleCreated) return;
        if (InvokeRequired)
        {
            try { BeginInvoke(() => Workspace_SampleRecorded(sender, sample)); }
            catch (InvalidOperationException) { /* 句柄可能在排队期间关闭。 */ }
            return;
        }
        if (channelPicker.SelectedIndex + 1 != sample.Channel) return;
        rawValue.Text = sample.RawCount?.ToString(CultureInfo.CurrentCulture) ?? "--";
    }

    private void AddCurrentPoint()
    {
        if (_workspace is null || channelPicker.SelectedIndex < 0) return;
        var raw = _workspace.GetLatestRaw(channelPicker.SelectedIndex + 1);
        pointsGridRows.Rows.Add(raw?.ToString(CultureInfo.InvariantCulture) ?? "", "");
        pointsGrid.Refresh();
        result.Text = raw.HasValue ? "已加入当前原始值，请填写对应标定水位。" : "设备没有实时值，请手动填写原始计数和标定水位。";
    }

    private void RemoveSelectedPoint()
    {
        foreach (System.Data.DataRow row in pointsGrid.SelectedsReal().OfType<System.Data.DataRow>().ToArray())
            pointsGridRows.Rows.Remove(row);
        pointsGrid.Refresh();
    }

    private void SetZero()
    {
        if (_workspace is null || _profile is null) return;
        var raw = _workspace.GetLatestRaw(_profile.Channel);
        if (!raw.HasValue)
        {
            result.Text = "当前通道没有实时原始值，无法定零。";
            return;
        }
        _profile.ZeroRawCount = raw.Value;
        result.Text = $"CH{_profile.Channel} 零点设为 {raw.Value}。请重新计算并保存标定系数。";
    }

    private bool Fit()
    {
        if (_profile is null) return false;
        try
        {
            pointsGrid.EditModeClose();
            var points = new List<WaveCalibrationPoint>();
            foreach (System.Data.DataRow row in pointsGridRows.Rows)
            {
                var rawText = Convert.ToString(row[0])?.Trim() ?? "";
                var levelText = Convert.ToString(row[1])?.Trim() ?? "";
                if (!TryNumber(rawText, out var raw) || !TryNumber(levelText, out var level) ||
                    !double.IsFinite(raw) || !double.IsFinite(level))
                    throw new InvalidOperationException($"第 {pointsGridRows.Rows.IndexOf(row) + 1} 行需要有效的原始计数和水位。");
                points.Add(new WaveCalibrationPoint(raw, level));
            }
            _profile.Points = points;
            _profile.Fit();
            result.Text = $"CH{_profile.Channel}：水位 = (原始值 - {_profile.ZeroRawCount:0.###}) × {_profile.Slope:0.######} + {_profile.Intercept:0.######} mm";
            result.ForeColor = UiPalette.Success;
            return true;
        }
        catch (Exception ex)
        {
            result.Text = "计算失败：" + ex.Message;
            result.ForeColor = UiPalette.Danger;
            return false;
        }
    }

    private void Save()
    {
        if (_workspace is null || _profile is null || !Fit()) return;
        try
        {
            _workspace.SaveCalibration(_profile);
            result.Text += "；已保存。";
        }
        catch (Exception ex)
        {
            result.Text = "保存失败：" + ex.Message;
        }
    }

    private void Import()
    {
        if (_workspace is null) return;
        using var dialog = new OpenFileDialog { Filter = "旧标定文件 (*.conf)|*.conf|所有文件 (*.*)|*.*" };
        if (dialog.ShowDialog(this) != DialogResult.OK) return;
        try
        {
            _workspace.ImportLegacyCalibration(dialog.FileName);
            LoadChannel();
            result.Text = "旧 .conf 已导入。格式依据旧工程源码实现，仍需真实样例验收。";
        }
        catch (Exception ex) { result.Text = "导入失败：" + ex.Message; }
    }

    private void Export()
    {
        if (_workspace is null) return;
        using var dialog = new SaveFileDialog
        {
            Filter = "旧标定文件 (*.conf)|*.conf",
            DefaultExt = "conf",
            AddExtension = true
        };
        if (dialog.ShowDialog(this) != DialogResult.OK) return;
        try
        {
            _workspace.ExportLegacyCalibration(dialog.FileName);
            result.Text = "标定文件已导出。";
        }
        catch (Exception ex) { result.Text = "导出失败：" + ex.Message; }
    }

    // 工作区比页面存活更久，释放页面时必须解除采样通知。
    private void Calibration_Disposed(object? sender, EventArgs e)
    {
        Disposed -= Calibration_Disposed;
        if (_workspace is not null)
            _workspace.SampleRecorded -= Workspace_SampleRecorded;
    }

    private static bool TryNumber(string text, out double value) =>
        double.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out value) ||
        double.TryParse(text, NumberStyles.Float, CultureInfo.CurrentCulture, out value);
}
