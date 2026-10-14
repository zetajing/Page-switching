namespace Page_switching;

partial class WaveformPreviewControl
{
    private ScottPlot.WinForms.FormsPlot plotView = null!;
    private Label emptyLabel = null!;
    private Panel toolbar = null!;
    private AntdUI.Checkbox autoScaleCheckBox = null!;
    private AntdUI.Button resetViewButton = null!;

    // ScottPlot 和空数据提示均由 Designer 创建，预览容器仍可拖拽编辑。
    private void InitializeComponent()
    {
        plotView = new ScottPlot.WinForms.FormsPlot();
        emptyLabel = new Label();
        toolbar = new Panel();
        autoScaleCheckBox = new AntdUI.Checkbox();
        resetViewButton = new AntdUI.Button();
        toolbar.SuspendLayout();
        SuspendLayout();
        toolbar.BackColor = Color.White;
        toolbar.Controls.Add(autoScaleCheckBox);
        toolbar.Controls.Add(resetViewButton);
        toolbar.Dock = DockStyle.Top;
        toolbar.Name = "toolbar";
        toolbar.Size = new Size(640, 36);
        toolbar.TabIndex = 0;
        autoScaleCheckBox.Checked = true;
        autoScaleCheckBox.Location = new Point(10, 3);
        autoScaleCheckBox.Name = "autoScaleCheckBox";
        autoScaleCheckBox.Size = new Size(120, 30);
        autoScaleCheckBox.Text = "自动跟随";
        autoScaleCheckBox.TabIndex = 0;
        autoScaleCheckBox.CheckedChanged += AutoScaleCheckBox_CheckedChanged;
        resetViewButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        resetViewButton.BorderWidth = 1;
        resetViewButton.Location = new Point(536, 3);
        resetViewButton.Name = "resetViewButton";
        resetViewButton.Size = new Size(94, 30);
        resetViewButton.Text = "复位视图";
        resetViewButton.TabIndex = 1;
        resetViewButton.Click += ResetViewButton_Click;
        plotView.Dock = DockStyle.Fill;
        plotView.Location = new Point(0, 0);
        plotView.Name = "plotView";
        plotView.Size = new Size(640, 360);
        plotView.TabIndex = 0;
        emptyLabel.Anchor = AnchorStyles.None;
        emptyLabel.AutoSize = true;
        emptyLabel.BackColor = Color.White;
        emptyLabel.ForeColor = UiPalette.Muted;
        emptyLabel.Location = new Point(265, 170);
        emptyLabel.Name = "emptyLabel";
        emptyLabel.Text = "暂无波形数据";
        emptyLabel.TabIndex = 1;
        BackColor = UiPalette.Surface;
        ForeColor = UiPalette.Primary;
        AutoScaleMode = AutoScaleMode.Dpi;
        Controls.Add(emptyLabel);
        Controls.Add(plotView);
        Controls.Add(toolbar);
        MinimumSize = new Size(360, 260);
        Name = "WaveformPreviewControl";
        Size = new Size(640, 360);
        toolbar.ResumeLayout(false);
        ResumeLayout(false);
        PerformLayout();
    }
}
