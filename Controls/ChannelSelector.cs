using System.Drawing.Drawing2D;

namespace Page_switching;

public sealed class ChannelSelector : CheckBox
{
    public ChannelSelector()
    {
        Appearance = Appearance.Button;
        SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        var bounds = new Rectangle(0, 0, Width - 1, Height - 1);
        if (bounds.Width < 2 || bounds.Height < 2) return;

        var fillColor = !Enabled ? UiPalette.Sidebar : Checked ? UiPalette.Selection : UiPalette.Surface;
        var titleColor = !Enabled ? UiPalette.Muted : Checked ? UiPalette.PrimaryHover : UiPalette.Text;
        e.Graphics.Clear(Parent?.BackColor ?? UiPalette.Canvas);
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        using var path = CardBorder.CreatePath(bounds);
        using var fill = new SolidBrush(fillColor);
        e.Graphics.FillPath(fill, path);
        CardBorder.Draw(e.Graphics, bounds, Checked ? UiPalette.Primary : UiPalette.SecondaryButton);

        var lines = Text.Replace("\r", "").Split('\n', 2);
        var textTop = (Height - 44) / 2;
        using var titleFont = new Font(Font, FontStyle.Bold);
        TextRenderer.DrawText(e.Graphics, lines[0], titleFont, new Rectangle(4, textTop, Width - 8, 22),
            titleColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        if (lines.Length > 1)
        {
            TextRenderer.DrawText(e.Graphics, lines[1], Font, new Rectangle(4, textTop + 24, Width - 8, 22),
                Enabled ? UiPalette.SecondaryText : UiPalette.Muted,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }
    }
}
