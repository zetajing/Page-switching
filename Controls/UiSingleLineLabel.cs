namespace Page_switching;

// 日志路径和消息摘要只占一行；完整文本仍保留在 Text 与辅助功能信息中。
public sealed class UiSingleLineLabel : Label
{
    protected override void OnPaint(PaintEventArgs e)
    {
        var bounds = new Rectangle(Padding.Left, Padding.Top,
            Math.Max(0, ClientSize.Width - Padding.Horizontal), Math.Max(0, ClientSize.Height - Padding.Vertical));
        TextRenderer.DrawText(e.Graphics, Text, Font, bounds, ForeColor,
            TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.SingleLine |
            TextFormatFlags.EndEllipsis | TextFormatFlags.NoPrefix);
    }
}
