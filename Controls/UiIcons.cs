using System.Drawing.Drawing2D;

namespace Page_switching;

internal enum UiIcon { Overview, Manual, Calibration, Data, Wave, Monitor, Analysis, Correction, Settings, Owner, Mode, State }

// 本地图形图标不依赖字体字形或外部资源；调用方负责释放返回的位图。
internal static class UiIcons
{
    internal static GraphicsPath RoundedRectangle(RectangleF bounds, float radius)
    {
        var path = new GraphicsPath();
        var diameter = Math.Min(radius * 2, Math.Min(bounds.Width, bounds.Height));
        if (diameter <= 0) { path.AddRectangle(bounds); return path; }
        path.AddArc(bounds.Left, bounds.Top, diameter, diameter, 180, 90);
        path.AddArc(bounds.Right - diameter, bounds.Top, diameter, diameter, 270, 90);
        path.AddArc(bounds.Right - diameter, bounds.Bottom - diameter, diameter, diameter, 0, 90);
        path.AddArc(bounds.Left, bounds.Bottom - diameter, diameter, diameter, 90, 90);
        path.CloseFigure();
        return path;
    }

    internal static Bitmap Create(UiIcon icon, Color color, int size)
    {
        var bitmap = new Bitmap(Math.Max(1, size), Math.Max(1, size));
        using var graphics = Graphics.FromImage(bitmap);
        graphics.SmoothingMode = SmoothingMode.AntiAlias;
        graphics.ScaleTransform(size / 24f, size / 24f);
        using var pen = new Pen(color, 1.7f) { StartCap = LineCap.Round, EndCap = LineCap.Round, LineJoin = LineJoin.Round };
        switch (icon)
        {
            case UiIcon.Overview:
                foreach (var point in new[] { new Point(3, 3), new Point(14, 3), new Point(3, 14), new Point(14, 14) })
                    graphics.DrawRectangle(pen, point.X, point.Y, 7, 7);
                break;
            case UiIcon.Manual:
            case UiIcon.Mode:
                for (var index = 0; index < 3; index++)
                {
                    var x = 5 + index * 7;
                    var y = index == 1 ? 15 : 8;
                    graphics.DrawLine(pen, x, 3, x, y - 3);
                    graphics.DrawLine(pen, x, y + 3, x, 21);
                    graphics.DrawEllipse(pen, x - 3, y - 3, 6, 6);
                }
                break;
            case UiIcon.Settings:
                graphics.DrawEllipse(pen, 5, 5, 14, 14);
                graphics.DrawEllipse(pen, 9, 9, 6, 6);
                for (var index = 0; index < 8; index++)
                {
                    var angle = index * Math.PI / 4;
                    graphics.DrawLine(pen, 12 + (float)Math.Cos(angle) * 8, 12 + (float)Math.Sin(angle) * 8,
                        12 + (float)Math.Cos(angle) * 10, 12 + (float)Math.Sin(angle) * 10);
                }
                break;
            case UiIcon.Owner:
                graphics.DrawRectangle(pen, 3, 4, 18, 12);
                graphics.DrawLine(pen, 12, 16, 12, 21);
                graphics.DrawLine(pen, 7, 21, 17, 21);
                break;
            case UiIcon.State:
                graphics.DrawEllipse(pen, 3, 3, 18, 18);
                graphics.DrawLines(pen, new[] { new Point(10, 8), new Point(16, 12), new Point(10, 16), new Point(10, 8) });
                break;
            case UiIcon.Data:
                graphics.DrawRectangle(pen, 4, 3, 16, 18);
                graphics.DrawLine(pen, 4, 9, 20, 9);
                graphics.DrawLine(pen, 9, 3, 9, 21);
                graphics.DrawLine(pen, 4, 15, 20, 15);
                break;
            case UiIcon.Calibration:
                graphics.DrawRectangle(pen, 3, 6, 18, 12);
                for (var x = 6; x < 21; x += 4) graphics.DrawLine(pen, x, 6, x, 11);
                break;
            case UiIcon.Analysis:
                graphics.DrawLine(pen, 3, 3, 3, 21);
                graphics.DrawLine(pen, 3, 21, 21, 21);
                graphics.DrawLines(pen, new[] { new Point(6, 17), new Point(11, 10), new Point(15, 13), new Point(21, 5) });
                break;
            case UiIcon.Correction:
                graphics.DrawLines(pen, new[] { new Point(3, 17), new Point(8, 9), new Point(13, 15), new Point(20, 4) });
                graphics.DrawLine(pen, 16, 4, 20, 4);
                graphics.DrawLine(pen, 20, 4, 20, 8);
                break;
            default:
                graphics.DrawCurve(pen, new[] { new Point(2, 12), new Point(7, 5), new Point(12, 12), new Point(17, 19), new Point(22, 12) });
                if (icon == UiIcon.Monitor) graphics.DrawLine(pen, 3, 22, 21, 22);
                break;
        }
        return bitmap;
    }
}
