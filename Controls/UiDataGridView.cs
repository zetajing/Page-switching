using System.ComponentModel;

namespace Page_switching;

public sealed class UiDataGridView : DataGridView
{
    public UiDataGridView()
    {
        BackgroundColor = UiPalette.Surface;
        BorderStyle = BorderStyle.None;
        CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
        ColumnHeadersHeight = 36;
        EnableHeadersVisualStyles = false;
        GridColor = UiPalette.SecondaryButton;
        RowHeadersVisible = false;
        RowTemplate.Height = 34;
        Font = new Font("Microsoft YaHei UI", 9F);
    }

    [DefaultValue("")]
    public string EmptyText { get; set; } = "";

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        var empty = Rows.Count == 0 || Rows.Count == 1 && Rows[0].IsNewRow;
        if (!empty || string.IsNullOrEmpty(EmptyText)) return;

        var area = new Rectangle(0, ColumnHeadersHeight, Width, Height - ColumnHeadersHeight);
        TextRenderer.DrawText(e.Graphics, EmptyText, Font, area, UiPalette.Muted,
            TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
    }
}
