using System.Drawing.Drawing2D;
using System.Globalization;

namespace Page_switching;

public sealed class WaveformPreviewControl : Control
{
    private IReadOnlyList<double> _samples = Array.Empty<double>();

    // 初始化波形预览控件的绘制样式。
    public WaveformPreviewControl()
    {
        SetStyle(
            ControlStyles.AllPaintingInWmPaint |
            ControlStyles.UserPaint |
            ControlStyles.OptimizedDoubleBuffer |
            ControlStyles.ResizeRedraw,
            true);
        BackColor = Color.White;
        ForeColor = Color.FromArgb(30, 64, 175);
        MinimumSize = new Size(360, 260);
    }

    public double SampleIntervalSeconds { get; private set; } = 0.02;

    public string UnitText { get; set; } = "m";

    // 设置需要显示的采样点和采样时间间隔，并触发重绘。
    public void SetSamples(IReadOnlyList<double> samples, double sampleIntervalSeconds)
    {
        _samples = samples?.ToArray() ?? Array.Empty<double>();
        SampleIntervalSeconds = sampleIntervalSeconds > 0 ? sampleIntervalSeconds : 0.02;
        Invalidate();
    }

    // 清空当前波形数据并刷新控件。
    public void ClearSamples()
    {
        _samples = Array.Empty<double>();
        Invalidate();
    }

    // 绘制背景、坐标和当前波形曲线。
    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        e.Graphics.Clear(BackColor);

        var plot = new RectangleF(58, 24, Math.Max(1, Width - 82), Math.Max(1, Height - 58));
        using var gridPen = new Pen(Color.FromArgb(226, 232, 240));
        using var axisPen = new Pen(Color.FromArgb(100, 116, 139));
        using var curvePen = new Pen(ForeColor, 2f);
        using var textBrush = new SolidBrush(Color.FromArgb(71, 85, 105));
        using var emptyBrush = new SolidBrush(Color.FromArgb(100, 116, 139));
        using var labelFont = new Font(Font.FontFamily, 8.5F);

        for (var i = 0; i <= 4; i++)
        {
            var y = plot.Top + plot.Height * i / 4F;
            e.Graphics.DrawLine(gridPen, plot.Left, y, plot.Right, y);
        }

        for (var i = 0; i <= 5; i++)
        {
            var x = plot.Left + plot.Width * i / 5F;
            e.Graphics.DrawLine(gridPen, x, plot.Top, x, plot.Bottom);
        }

        e.Graphics.DrawLine(axisPen, plot.Left, plot.Bottom, plot.Right, plot.Bottom);
        e.Graphics.DrawLine(axisPen, plot.Left, plot.Top, plot.Left, plot.Bottom);

        if (_samples.Count < 2)
        {
            const string message = "暂无波形数据";
            var size = e.Graphics.MeasureString(message, Font);
            e.Graphics.DrawString(message, Font, emptyBrush,
                plot.Left + (plot.Width - size.Width) / 2,
                plot.Top + (plot.Height - size.Height) / 2);
            return;
        }

        var min = _samples.Min();
        var max = _samples.Max();
        if (Math.Abs(max - min) < 1e-12)
        {
            min -= 1;
            max += 1;
        }

        e.Graphics.DrawString(max.ToString("0.###"), labelFont, textBrush, 4, plot.Top - 6);
        e.Graphics.DrawString(min.ToString("0.###"), labelFont, textBrush, 4, plot.Bottom - 9);
        e.Graphics.DrawString(UnitText, labelFont, textBrush, 4, plot.Top - 22);
        var duration = (_samples.Count - 1) * SampleIntervalSeconds;
        e.Graphics.DrawString("0 s", labelFont, textBrush, plot.Left - 9, plot.Bottom + 6);
        var endText = duration.ToString("0.###", CultureInfo.InvariantCulture) + " s";
        var endSize = e.Graphics.MeasureString(endText, labelFont);
        e.Graphics.DrawString(endText, labelFont, textBrush, plot.Right - endSize.Width, plot.Bottom + 6);

        var stride = Math.Max(1, (int)Math.Ceiling(_samples.Count / 4000D));
        var pointCount = (_samples.Count + stride - 1) / stride;
        var points = new PointF[pointCount];
        var pointIndex = 0;
        for (var index = 0; index < _samples.Count; index += stride)
        {
            var normalizedX = _samples.Count == 1 ? 0 : (float)index / (_samples.Count - 1);
            var normalizedY = (float)((_samples[index] - min) / (max - min));
            points[pointIndex++] = new PointF(
                plot.Left + plot.Width * normalizedX,
                plot.Bottom - plot.Height * normalizedY);
        }

        if (pointIndex > 1)
        {
            e.Graphics.DrawLines(curvePen, points);
        }
    }
}
