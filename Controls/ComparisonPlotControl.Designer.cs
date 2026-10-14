namespace Page_switching;

partial class ComparisonPlotControl
{
    private System.ComponentModel.IContainer components = null!;
    private ScottPlot.WinForms.FormsPlot plotView = null!;
    private Panel toolbar = null!;
    private AntdUI.Button resetViewButton = null!;
    private AntdUI.Label emptyLabel = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing) components?.Dispose();
        base.Dispose(disposing);
    }

    // 控件创建、布局和事件保留在 Designer，运行时只配置曲线数据与坐标说明。
    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        plotView = new ScottPlot.WinForms.FormsPlot();
        toolbar = new Panel();
        resetViewButton = new AntdUI.Button();
        emptyLabel = new AntdUI.Label();
        SuspendLayout();
        toolbar.SuspendLayout();
        plotView.Name = "plotView";
        plotView.Dock = DockStyle.Fill;
        toolbar.Name = "toolbar";
        toolbar.Dock = DockStyle.Top;
        toolbar.Height = 38;
        toolbar.BackColor = Color.White;
        toolbar.Controls.Add(resetViewButton);
        resetViewButton.Name = "resetViewButton";
        resetViewButton.Text = "复位视图";
        resetViewButton.Radius = 6;
        resetViewButton.Size = new Size(100, 32);
        resetViewButton.Location = new Point(590, 3);
        resetViewButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        resetViewButton.Click += ResetViewButton_Click;
        emptyLabel.Name = "emptyLabel";
        emptyLabel.Text = "暂无分析数据";
        emptyLabel.BackColor = Color.White;
        emptyLabel.ForeColor = Color.FromArgb(100, 116, 139);
        emptyLabel.TextAlign = ContentAlignment.MiddleCenter;
        emptyLabel.Size = new Size(220, 38);
        emptyLabel.Location = new Point(230, 100);
        emptyLabel.Anchor = AnchorStyles.Top;
        Controls.Add(emptyLabel);
        Controls.Add(plotView);
        Controls.Add(toolbar);
        BackColor = Color.White;
        AutoScaleDimensions = new SizeF(96F, 96F);
        AutoScaleMode = AutoScaleMode.Dpi;
        MinimumSize = new Size(300, 180);
        Size = new Size(700, 400);
        Name = "ComparisonPlotControl";
        toolbar.ResumeLayout(false);
        ResumeLayout(false);
    }
}
