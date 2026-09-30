using System.Drawing.Drawing2D;
using System.Globalization;

namespace Page_switching;

public sealed class ComparisonPlotControl : Control
{
    private double[] _x = [];
    private double[] _first = [];
    private double[] _second = [];
    public string HorizontalCaption { get; set; } = "Frequency (Hz)";
    public string VerticalCaption { get; set; } = "S(f) (m²·s)";
    public string FirstCaption { get; set; } = "理论谱";
    public string SecondCaption { get; set; } = "实测谱";

    public ComparisonPlotControl()
    {
        DoubleBuffered = true;
        BackColor = Color.White;
        MinimumSize = new Size(300, 180);
        SetStyle(ControlStyles.ResizeRedraw, true);
    }

    internal void SetSeries(double[] x, double[] first, double[] second)
    {
        if (x.Length != first.Length || (second.Length != 0 && x.Length != second.Length))
            throw new ArgumentException("曲线坐标数量不一致。");
        _x = x; _first = first; _second = second;
        Invalidate();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        var plot = new RectangleF(70, 38, Math.Max(1, Width - 95), Math.Max(1, Height - 90));
        using var grid = new Pen(UiPalette.Border);
        using var text = new SolidBrush(UiPalette.SecondaryText);
        using var blue = new Pen(UiPalette.Primary, 1.6f);
        using var red = new Pen(UiPalette.Danger, 1.6f);
        e.Graphics.DrawString(VerticalCaption, Font, text, 8, 7);
        e.Graphics.DrawString(HorizontalCaption, Font, text, plot.Left + plot.Width / 2 - 35, plot.Bottom + 29);
        if (_x.Length < 2) { e.Graphics.DrawString("暂无分析数据", Font, text, plot.Left + 30, plot.Top + 40); return; }
        var min = Math.Min(0, _first.Concat(_second).Min());
        var max = Math.Max(0, _first.Concat(_second).Max());
        if (max - min < 1e-12) max = min + 1;
        var spanX = Math.Max(1e-12, _x[^1] - _x[0]);
        for (var i = 0; i <= 4; i++)
        {
            var y = plot.Top + plot.Height * i / 4;
            var x = plot.Left + plot.Width * i / 4;
            e.Graphics.DrawLine(grid, plot.Left, y, plot.Right, y);
            e.Graphics.DrawLine(grid, x, plot.Top, x, plot.Bottom);
            e.Graphics.DrawString((max - (max - min) * i / 4).ToString("0.###", CultureInfo.InvariantCulture), Font, text, 6, y - 8);
            e.Graphics.DrawString((_x[0] + spanX * i / 4).ToString("0.##", CultureInfo.InvariantCulture), Font, text, x - 12, plot.Bottom + 5);
        }
        void Draw(double[] values, Pen pen)
        {
            if (values.Length < 2) return;
            // 每像素保留区间最小/最大值，长记录不会因抽样跳过尖峰。
            var pixels = Math.Max(1, (int)plot.Width);
            for (var pixel = 0; pixel < pixels; pixel++)
            {
                var start = pixel * values.Length / pixels;
                var end = Math.Max(start + 1, (pixel + 1) * values.Length / pixels);
                if (start >= values.Length) break;
                double low = values[start], high = low;
                for (var j = start + 1; j < Math.Min(end, values.Length); j++) { low = Math.Min(low, values[j]); high = Math.Max(high, values[j]); }
                var x = plot.Left + (float)((_x[start] - _x[0]) / spanX) * plot.Width;
                float Y(double value) => plot.Bottom - (float)((value - min) / (max - min)) * plot.Height;
                e.Graphics.DrawLine(pen, x, Y(low), x, Y(high));
                if (end < values.Length) e.Graphics.DrawLine(pen, x, Y(values[end - 1]),
                    plot.Left + (float)((_x[end] - _x[0]) / spanX) * plot.Width, Y(values[end]));
            }
        }
        Draw(_first, blue); Draw(_second, red);
        e.Graphics.DrawLine(blue, plot.Left, 23, plot.Left + 20, 23);
        e.Graphics.DrawString(FirstCaption, Font, text, plot.Left + 25, 15);
        if (_second.Length > 0)
        {
            e.Graphics.DrawLine(red, plot.Left + 130, 23, plot.Left + 150, 23);
            e.Graphics.DrawString(SecondCaption, Font, text, plot.Left + 155, 15);
        }
    }
}
