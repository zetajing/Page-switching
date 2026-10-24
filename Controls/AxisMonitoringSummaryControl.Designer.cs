namespace Page_switching;

partial class AxisMonitoringSummaryControl
{
    private System.ComponentModel.IContainer components = null!;
    private TableLayoutPanel layout = null!;
    private Label captionLabel = null!;
    private Button copyButton = null!;
    private TextBox reportTextBox = null!;
    protected override void Dispose(bool disposing) { if (disposing) components?.Dispose(); base.Dispose(disposing); }
    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        layout = new TableLayoutPanel(); captionLabel = new Label(); copyButton = new Button(); reportTextBox = new TextBox();
        layout.SuspendLayout(); SuspendLayout();
        layout.ColumnCount = 2;
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 125F));
        layout.RowCount = 2;
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 29F));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        layout.Dock = DockStyle.Fill;
        layout.Controls.Add(captionLabel, 0, 0); layout.Controls.Add(copyButton, 1, 0); layout.Controls.Add(reportTextBox, 0, 1);
        layout.SetColumnSpan(reportTextBox, 2);
        captionLabel.Text = "运行实测超限占比"; captionLabel.Dock = DockStyle.Fill; captionLabel.TextAlign = ContentAlignment.MiddleLeft;
        copyButton.Text = "复制运行统计"; copyButton.Dock = DockStyle.Fill; copyButton.Enabled = false;
        copyButton.Click += CopyButton_Click;
        reportTextBox.Dock = DockStyle.Fill; reportTextBox.Multiline = true; reportTextBox.ReadOnly = true;
        reportTextBox.ScrollBars = ScrollBars.Vertical; reportTextBox.BackColor = Color.White;
        reportTextBox.Text = "尚无本软件发起的运行统计。";
        Controls.Add(layout); Size = new Size(500, 190);
        AutoScaleMode = AutoScaleMode.Dpi; Font = new Font("Microsoft YaHei UI", 9F);
        layout.ResumeLayout(false); layout.PerformLayout(); ResumeLayout(false);
    }
}
