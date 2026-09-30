namespace Page_switching.panel
{
    public partial class RegularWavePage : UserControl
    {
        public RegularWavePage()
        {
            InitializeComponent();
            // 默认选择放在页面初始化中，避免设计器保存时丢失。
            regularSegmentComboBox.SelectedIndex = 0;
            regularTheoryComboBox.SelectedIndex = 0;
        }

        internal event EventHandler? GenerateRequested;
        internal event EventHandler? OutputBrowseRequested;

        private void GenerateRegularButton_Click(object? sender, EventArgs e) =>
            GenerateRequested?.Invoke(this, e);

        private void BrowseRegularOutputButton_Click(object? sender, EventArgs e) =>
            OutputBrowseRequested?.Invoke(this, e);
    }
}
