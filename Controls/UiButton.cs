using System.Drawing.Drawing2D;

namespace Page_switching;

public sealed class UiButton : Button
{
    private bool _hovered;

    public UiButton()
    {
        FlatStyle = FlatStyle.Flat;
        FlatAppearance.BorderSize = 0;
        SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }

    protected override void OnMouseEnter(EventArgs e)
    {
        _hovered = true;
        Invalidate();
        base.OnMouseEnter(e);
    }

    protected override void OnMouseLeave(EventArgs e)
    {
        _hovered = false;
        Invalidate();
        base.OnMouseLeave(e);
    }

    protected override void OnEnabledChanged(EventArgs e)
    {
        base.OnEnabledChanged(e);
        Invalidate();
    }

    protected override void OnGotFocus(EventArgs e)
    {
        base.OnGotFocus(e);
        Invalidate();
    }

    protected override void OnLostFocus(EventArgs e)
    {
        base.OnLostFocus(e);
        Invalidate();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        var bounds = new Rectangle(0, 0, Width - 1, Height - 1);
        if (bounds.Width < 2 || bounds.Height < 2) return;

        e.Graphics.Clear(Parent?.BackColor ?? UiPalette.Canvas);
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        using var path = CardBorder.CreatePath(bounds);
        var fillColor = BackColor;
        if (!Enabled) fillColor = UiPalette.SecondaryButton;
        else if (_hovered) fillColor = FlatAppearance.MouseOverBackColor.IsEmpty
            ? ControlPaint.Dark(BackColor, 0.08F)
            : FlatAppearance.MouseOverBackColor;
        using var fill = new SolidBrush(fillColor);
        e.Graphics.FillPath(fill, path);
        if (Focused && ShowFocusCues)
        {
            CardBorder.Draw(e.Graphics, new Rectangle(2, 2, Width - 5, Height - 5), UiPalette.PrimaryHover);
        }

        TextRenderer.DrawText(e.Graphics, Text, Font, ClientRectangle,
            Enabled ? ForeColor : UiPalette.SecondaryText,
            TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);
    }
}
