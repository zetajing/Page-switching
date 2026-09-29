namespace Page_switching;

public sealed class UiTabControl : TabControl
{
    public UiTabControl()
    {
        Appearance = TabAppearance.FlatButtons;
        DrawMode = TabDrawMode.OwnerDrawFixed;
        SizeMode = TabSizeMode.Fixed;
        ItemSize = new Size(90, 34);
    }

    protected override void OnDrawItem(DrawItemEventArgs e)
    {
        if (e.Index < 0 || e.Index >= TabPages.Count) return;

        var selected = e.Index == SelectedIndex;
        using var background = new SolidBrush(selected ? UiPalette.Surface : UiPalette.Sidebar);
        e.Graphics.FillRectangle(background, e.Bounds);
        TextRenderer.DrawText(e.Graphics, TabPages[e.Index].Text, Font, e.Bounds,
            selected ? UiPalette.PrimaryHover : UiPalette.SecondaryText,
            TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);

        if (selected)
        {
            using var marker = new SolidBrush(UiPalette.Primary);
            e.Graphics.FillRectangle(marker, e.Bounds.Left + 8, e.Bounds.Bottom - 3, e.Bounds.Width - 16, 3);
        }
    }
}
