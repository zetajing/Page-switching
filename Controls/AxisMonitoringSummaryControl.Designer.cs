namespace Page_switching;

partial class AxisMonitoringSummaryControl
{
    private System.ComponentModel.IContainer components = null!;
    private TableLayoutPanel layout = null!;
    private Label captionLabel = null!;
    private AntdUI.Button copyButton = null!;
    private TextBox reportTextBox = null!;
    protected override void Dispose(bool disposing) { if (disposing) components?.Dispose(); base.Dispose(disposing); }
    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        layout = new TableLayoutPanel(); captionLabel = new Label(); copyButton = new AntdUI.Button(); reportTextBox = new TextBox();
        layout.SuspendLayout(); SuspendLayout();
        layout.ColumnCount = 2;
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 108F));
        layout.RowCount = 2;
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        layout.Dock = DockStyle.Fill;
        layout.Controls.Add(captionLabel, 0, 0); layout.Controls.Add(copyButton, 1, 0); layout.Controls.Add(reportTextBox, 0, 1);
        layout.SetColumnSpan(reportTextBox, 2);
        captionLabel.Text = "运行实测超限占比";
        captionLabel.Dock = DockStyle.Fill;
        captionLabel.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
        captionLabel.TextAlign = ContentAlignment.MiddleLeft;
        copyButton.Text = "复制运行统计";
        copyButton.Dock = DockStyle.Fill;
        copyButton.Enabled = false;
        copyButton.Margin = new Padding(0, 1, 0, 3);
        copyButton.Cursor = Cursors.Hand;
        copyButton.BorderWidth = 1F;
        copyButton.DefaultBack = Color.FromArgb(248, 250, 252);
        copyButton.DefaultBorderColor = Color.FromArgb(203, 213, 225);
        copyButton.WaveSize = 0;
        copyButton.Click += CopyButton_Click;
        reportTextBox.Dock = DockStyle.Fill; reportTextBox.Multiline = true; reportTextBox.ReadOnly = true;
        reportTextBox.BorderStyle = BorderStyle.FixedSingle;
        reportTextBox.Margin = new Padding(0, 4, 0, 0);
        reportTextBox.ScrollBars = ScrollBars.Vertical; reportTextBox.BackColor = Color.White;
        reportTextBox.Text = "尚无本软件发起的运行统计。";
        Controls.Add(layout); Size = new Size(500, 190);
        AutoScaleMode = AutoScaleMode.Dpi; Font = new Font("Microsoft YaHei UI", 9F);
        layout.ResumeLayout(false); layout.PerformLayout(); ResumeLayout(false);
    }
}
