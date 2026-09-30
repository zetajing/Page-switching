namespace Page_switching.panel
{
    public partial class IrregularWavePage : UserControl
    {
        public IrregularWavePage()
        {
            InitializeComponent();
            // 默认选择放在页面初始化中，避免设计器保存时丢失。
            irregularModeComboBox.SelectedIndex = 0;
            irregularTheoryComboBox.SelectedIndex = 0;
            irregularSpectrumComboBox.SelectedIndex = 0;
            irregularSegmentComboBox.SelectedIndex = 0;
        }

        internal event EventHandler? GenerateRequested;
        internal event EventHandler? OutputBrowseRequested;

        private void GenerateIrregularButton_Click(object? sender, EventArgs e) =>
            GenerateRequested?.Invoke(this, e);

        private void BrowseIrregularOutputButton_Click(object? sender, EventArgs e) =>
            OutputBrowseRequested?.Invoke(this, e);
    }
}
