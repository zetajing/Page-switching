using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace Page_switching;

// Designer 可编辑的轻量内容面板；圆角仅影响绘制，不改变子控件布局或事件。
public sealed class UiSurfacePanel : Panel
{
    private int _cornerRadius = 8;
    private Color _accentColor = Color.Empty;

    public UiSurfacePanel()
    {
        SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint |
            ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
        BackColor = Color.White;
        ForeColor = UiPalette.WorkText;
    }

    [Category("外观"), DefaultValue(8)]
    public int CornerRadius
    {
        get => _cornerRadius;
        set { _cornerRadius = Math.Max(0, value); Invalidate(); }
    }

    [Category("外观")]
    public Color AccentColor
    {
        get => _accentColor;
        set { _accentColor = value; Invalidate(); }
    }

    [System.Diagnostics.CodeAnalysis.AllowNull]
    [Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public override string Text
    {
        get => base.Text;
        set { base.Text = value; Invalidate(); }
    }

    protected override void OnPaintBackground(PaintEventArgs e)
    {
        var scale = DeviceDpi / 96f;
        var bounds = new RectangleF(.5f, .5f, Math.Max(0, Width - 1), Math.Max(0, Height - 1));
        if (bounds.Width < 2 || bounds.Height < 2) return;
        e.Graphics.Clear(Parent?.BackColor ?? UiPalette.WorkCanvas);
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        using var shape = UiIcons.RoundedRectangle(bounds, CornerRadius * scale);
        using var fill = new SolidBrush(BackColor);
        using var border = new Pen(UiPalette.WorkBorder);
        e.Graphics.FillPath(fill, shape);
        e.Graphics.DrawPath(border, shape);
        if (!AccentColor.IsEmpty)
        {
            using var accent = new Pen(AccentColor, 3 * scale);
            e.Graphics.DrawLine(accent, 18 * scale, 1.5f * scale, Width - 18 * scale, 1.5f * scale);
        }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        if (string.IsNullOrEmpty(Text)) return;
        var scale = DeviceDpi / 96f;
        using var headingFont = new Font("Microsoft YaHei UI", 12, FontStyle.Bold);
        TextRenderer.DrawText(e.Graphics, Text, headingFont,
            new Rectangle((int)(16 * scale), (int)(10 * scale), Width - (int)(32 * scale), (int)(28 * scale)),
            ForeColor, TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);
    }
}
