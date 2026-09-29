using System.Drawing.Drawing2D;

namespace Page_switching;

public sealed class CardGroupBox : GroupBox
{
    public CardGroupBox() => DoubleBuffered = true;

    protected override void OnPaint(PaintEventArgs e)
    {
        e.Graphics.Clear(BackColor);
        CardBorder.Draw(e.Graphics, new Rectangle(0, Font.Height / 2, Width - 1, Height - Font.Height / 2 - 1));

        if (string.IsNullOrEmpty(Text)) return;

        var titleSize = TextRenderer.MeasureText(Text, Font);
        using var background = new SolidBrush(BackColor);
        e.Graphics.FillRectangle(background, 12, 0, titleSize.Width + 8, Font.Height + 2);
        TextRenderer.DrawText(e.Graphics, Text, Font, new Point(16, 0), ForeColor, TextFormatFlags.NoPadding);
    }
}

public sealed class CardPanel : Panel
{
    public CardPanel() => DoubleBuffered = true;

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        CardBorder.Draw(e.Graphics, new Rectangle(0, 0, Width - 1, Height - 1));
    }
}

public sealed class CardFlowPanel : FlowLayoutPanel
{
    public CardFlowPanel() => DoubleBuffered = true;

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        CardBorder.Draw(e.Graphics, new Rectangle(0, 0, Width - 1, Height - 1));
    }
}

internal static class CardBorder
{
    public static void Draw(Graphics graphics, Rectangle bounds, Color? color = null)
    {
        if (bounds.Width < 2 || bounds.Height < 2) return;

        using var path = CreatePath(bounds);
        graphics.SmoothingMode = SmoothingMode.AntiAlias;
        using var border = new Pen(color ?? UiPalette.SecondaryButton);
        graphics.DrawPath(border, path);
    }

    public static GraphicsPath CreatePath(Rectangle bounds)
    {
        var radius = Math.Min(10, Math.Min(bounds.Width, bounds.Height) / 2);
        var diameter = radius * 2;
        var path = new GraphicsPath();
        path.AddArc(bounds.Left, bounds.Top, diameter, diameter, 180, 90);
        path.AddArc(bounds.Right - diameter, bounds.Top, diameter, diameter, 270, 90);
        path.AddArc(bounds.Right - diameter, bounds.Bottom - diameter, diameter, diameter, 0, 90);
        path.AddArc(bounds.Left, bounds.Bottom - diameter, diameter, diameter, 90, 90);
        path.CloseFigure();
        return path;
    }
}
