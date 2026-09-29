using System.ComponentModel;

namespace Page_switching;

public sealed class NavigationButton : Button
{
    private bool _isActive;

    [DefaultValue(false)]
    public bool IsActive
    {
        get => _isActive;
        set
        {
            _isActive = value;
            Invalidate();
        }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        if (!_isActive) return;

        using var marker = new SolidBrush(UiPalette.Primary);
        e.Graphics.FillRectangle(marker, 0, 9, 4, Height - 18);
    }
}
