namespace Page_switching.panel
{
    partial class WaveformPage
    {
        private System.ComponentModel.IContainer components = null;
        private Panel pagePanel;
        private TableLayoutPanel rootLayout;
        private AntdUI.Label titleLabel;
        private AntdUI.Label subtitleLabel;
        private AntdUI.Tabs waveformTabs;
        private AntdUI.TabPage regularTab;
        private AntdUI.TabPage irregularTab;
        private AntdUI.TabPage customTab;
        private RegularWavePage regularPage;
        private IrregularWavePage irregularPage;
        private CustomSpectrumPage customSpectrumPage;

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            pagePanel = new Panel();
            rootLayout = new TableLayoutPanel();
            titleLabel = new AntdUI.Label();
            subtitleLabel = new AntdUI.Label();
            waveformTabs = new AntdUI.Tabs();
            regularTab = new AntdUI.TabPage();
            irregularTab = new AntdUI.TabPage();
            customTab = new AntdUI.TabPage();
            regularPage = new RegularWavePage();
            irregularPage = new IrregularWavePage();
            customSpectrumPage = new CustomSpectrumPage();
            pagePanel.SuspendLayout();
            rootLayout.SuspendLayout();
            waveformTabs.SuspendLayout();
            regularTab.SuspendLayout();
            irregularTab.SuspendLayout();
            customTab.SuspendLayout();
            SuspendLayout();
            pagePanel.Name = "pagePanel";
            pagePanel.Dock = DockStyle.Fill;
            pagePanel.Controls.Add(rootLayout);
            rootLayout.Name = "rootLayout";
            rootLayout.Dock = DockStyle.Fill;
            rootLayout.Padding = new Padding(16);
            rootLayout.ColumnCount = 1;
            rootLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            rootLayout.RowCount = 3;
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 26F));
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            rootLayout.Controls.Add(titleLabel, 0, 0);
            rootLayout.Controls.Add(subtitleLabel, 0, 1);
            rootLayout.Controls.Add(waveformTabs, 0, 2);
            titleLabel.Name = "titleLabel";
            titleLabel.Text = "波形生成";
            titleLabel.Font = new Font("Microsoft YaHei UI", 16F, FontStyle.Bold);
            titleLabel.Dock = DockStyle.Fill;
            titleLabel.TextAlign = ContentAlignment.MiddleLeft;
            subtitleLabel.Name = "subtitleLabel";
            subtitleLabel.Text = "规则波、不规则波与自定义谱；参数设置、信号生成和曲线预览";
            subtitleLabel.Dock = DockStyle.Fill;
            subtitleLabel.ForeColor = Color.FromArgb(71, 85, 105);
            subtitleLabel.TextAlign = ContentAlignment.MiddleLeft;
            waveformTabs.Name = "waveformTabs";
            waveformTabs.Dock = DockStyle.Fill;
            waveformTabs.Pages.Add(regularTab);
            waveformTabs.Pages.Add(irregularTab);
            waveformTabs.Pages.Add(customTab);
            waveformTabs.SelectedIndex = 0;
            regularTab.Name = "regularTab";
            regularTab.Text = "规则波";
            regularTab.Controls.Add(regularPage);
            irregularTab.Name = "irregularTab";
            irregularTab.Text = "不规则波";
            irregularTab.Controls.Add(irregularPage);
            customTab.Name = "customTab";
            customTab.Text = "自定义谱";
            customTab.Controls.Add(customSpectrumPage);
            regularPage.Name = "regularPage";
            regularPage.Dock = DockStyle.Fill;
            irregularPage.Name = "irregularPage";
            irregularPage.Dock = DockStyle.Fill;
            customSpectrumPage.Name = "customSpectrumPage";
            customSpectrumPage.Dock = DockStyle.Fill;
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoScroll = true;
            AutoScrollMinSize = new Size(1000, 600);
            BackColor = Color.FromArgb(241, 245, 249);
            Font = new Font("Microsoft YaHei UI", 9F);
            ForeColor = Color.FromArgb(15, 23, 42);
            Name = "WaveformPage";
            Size = new Size(1104, 606);
            Controls.Add(pagePanel);
            customTab.ResumeLayout(false);
            irregularTab.ResumeLayout(false);
            regularTab.ResumeLayout(false);
            waveformTabs.ResumeLayout(false);
            rootLayout.ResumeLayout(false);
            pagePanel.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}
