using System.ComponentModel;

namespace Page_switching;

// 对比曲线沿用原始 X/Y 数据和单位，只统一显示控件，不进行物理换算。
public sealed partial class ComparisonPlotControl : UserControl
{
    private string _horizontalCaption = "Frequency (Hz)";
    private string _verticalCaption = "S(f) (m²·s)";
    private string _firstCaption = "理论谱";
    private string _secondCaption = "实测谱";
    private ScottPlot.Plottables.Scatter? _firstCurve;
    private ScottPlot.Plottables.Scatter? _secondCurve;

    public ComparisonPlotControl()
    {
        InitializeComponent();
        plotView.Plot.Font.Set("Microsoft YaHei UI");
        ApplyCaptions();
        plotView.Plot.Axes.SetLimits(0, 1, -1, 1);
    }

    [DefaultValue("Frequency (Hz)")]
    public string HorizontalCaption { get => _horizontalCaption; set { _horizontalCaption = value ?? ""; ApplyCaptions(); } }
    [DefaultValue("S(f) (m²·s)")]
    public string VerticalCaption { get => _verticalCaption; set { _verticalCaption = value ?? ""; ApplyCaptions(); } }
    [DefaultValue("理论谱")]
    public string FirstCaption { get => _firstCaption; set { _firstCaption = value ?? ""; ApplyCaptions(); } }
    [DefaultValue("实测谱")]
    public string SecondCaption { get => _secondCaption; set { _secondCaption = value ?? ""; ApplyCaptions(); } }

    private void ApplyCaptions()
    {
        if (plotView is null || IsDisposed || Disposing) return;
        plotView.Plot.XLabel(_horizontalCaption);
        plotView.Plot.YLabel(_verticalCaption);
        if (_firstCurve is not null) _firstCurve.LegendText = _firstCaption;
        if (_secondCurve is not null) _secondCurve.LegendText = _secondCaption;
        plotView.Refresh();
    }

    internal void SetSeries(double[] x, double[] first, double[] second)
    {
        if (x.Length != first.Length || (second.Length != 0 && x.Length != second.Length))
            throw new ArgumentException("曲线坐标数量不一致。");
        if (IsDisposed || Disposing) return;
        plotView.Plot.Clear();
        _firstCurve = _secondCurve = null;
        emptyLabel.Visible = x.Length == 0;
        // 保存数据副本，避免调用方后续修改数组影响已显示的曲线。
        if (x.Length > 0)
        {
            _firstCurve = plotView.Plot.Add.Scatter(x.ToArray(), first.ToArray());
            _firstCurve.Color = ScottPlot.Color.FromColor(UiPalette.Primary);
            _firstCurve.MarkerSize = x.Length == 1 ? 6 : 0;
            if (second.Length > 0)
            {
                _secondCurve = plotView.Plot.Add.Scatter(x.ToArray(), second.ToArray());
                _secondCurve.Color = ScottPlot.Color.FromColor(UiPalette.Danger);
                _secondCurve.MarkerSize = x.Length == 1 ? 6 : 0;
            }
            plotView.Plot.ShowLegend();
            FitSeries();
        }
        else
        {
            plotView.Plot.HideLegend();
            plotView.Plot.Axes.SetLimits(0, 1, -1, 1);
        }
        ApplyCaptions();
    }

    // 保留原对比图包含零基准的纵轴范围，缩放不改变原始数据或单位。
    private void FitSeries()
    {
        plotView.Plot.Axes.AutoScale();
        var limits = plotView.Plot.Axes.GetLimits();
        plotView.Plot.Axes.SetLimitsY(Math.Min(0, limits.Bottom), Math.Max(0, limits.Top));
    }
    private void ResetViewButton_Click(object? sender, EventArgs e)
    {
        if (plotView.Plot.GetPlottables().Any()) FitSeries();
        plotView.Refresh();
        if (LicenseManager.UsageMode != LicenseUsageMode.Designtime && !DesignMode && Site?.DesignMode != true)
            OperationJournal.Record("对比曲线", "已复位曲线视图");
    }
}
