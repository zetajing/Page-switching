using System.ComponentModel;

namespace Page_switching;

// 保留原有 SetSamples 接口，所有调用页面直接使用 ScottPlot 显示已有采样值。
public sealed partial class WaveformPreviewControl : UserControl
{
    private string _unitText = "m";

    public WaveformPreviewControl()
    {
        InitializeComponent();
        plotView.Plot.Font.Set("Microsoft YaHei UI");
        plotView.Plot.XLabel("时间 (s)");
        plotView.Plot.YLabel(_unitText);
        plotView.Plot.Axes.SetLimits(0, 1, -1, 1);
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public double SampleIntervalSeconds { get; private set; } = 0.02;

    [DefaultValue("m")]
    public string UnitText
    {
        get => _unitText;
        set
        {
            var unit = value ?? "";
            // 浪高仪每个采样都会设置单位，未变化时不重复绘制一次。
            if (_unitText == unit) return;
            _unitText = unit;
            if (IsDisposed || Disposing) return;
            plotView.Plot.YLabel(_unitText);
            plotView.Refresh();
        }
    }

    // Signal 使用真实采样间隔构造时间轴，保留完整数据，不按固定步长丢弃尖峰。
    public void SetSamples(IReadOnlyList<double> samples, double sampleIntervalSeconds)
    {
        if (IsDisposed || Disposing) return;
        var values = samples?.ToArray() ?? Array.Empty<double>();
        SampleIntervalSeconds = double.IsFinite(sampleIntervalSeconds) && sampleIntervalSeconds > 0
            ? sampleIntervalSeconds : 0.02;
        plotView.Plot.Clear();
        emptyLabel.Visible = values.Length == 0;
        if (values.Length == 1)
        {
            // 首个采样值也显示出来，不等待第二个点才有曲线。
            var point = plotView.Plot.Add.Scatter([0.0], values);
            point.Color = ScottPlot.Color.FromColor(ForeColor);
            point.MarkerSize = 6;
        }
        else if (values.Length > 1)
        {
            var signal = plotView.Plot.Add.Signal(values, period: SampleIntervalSeconds);
            signal.Color = ScottPlot.Color.FromColor(ForeColor);
            signal.LineWidth = 2;
        }
        else
        {
            plotView.Plot.Axes.SetLimits(0, 1, -1, 1);
        }
        // 关闭自动跟随后保留用户缩放范围，接收新数据时仍然可以查看局部细节。
        if (values.Length > 0 && autoScaleCheckBox.Checked) plotView.Plot.Axes.AutoScale();
        plotView.Refresh();
    }

    public void ClearSamples() => SetSamples(Array.Empty<double>(), SampleIntervalSeconds);

    private void ResetViewButton_Click(object? sender, EventArgs e)
    {
        if (plotView.Plot.GetPlottables().Any()) plotView.Plot.Axes.AutoScale();
        plotView.Refresh();
        if (!DesignMode && Site?.DesignMode != true && LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            OperationJournal.Record("曲线预览", "已复位曲线视图");
    }

    private void AutoScaleCheckBox_CheckedChanged(object? sender, EventArgs e)
    {
        if (autoScaleCheckBox.Checked && plotView.Plot.GetPlottables().Any())
        {
            plotView.Plot.Axes.AutoScale();
            plotView.Refresh();
        }
        if (!DesignMode && Site?.DesignMode != true && LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            OperationJournal.Record("曲线预览", "自动跟随：" + (autoScaleCheckBox.Checked ? "开启" : "关闭"));
    }
}
