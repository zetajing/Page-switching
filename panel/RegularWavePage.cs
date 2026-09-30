namespace Page_switching.panel
{
    public partial class RegularWavePage : UserControl
    {
        public RegularWavePage()
        {
            InitializeComponent();
        }

        internal event EventHandler? GenerateRequested;
        internal event EventHandler? OutputBrowseRequested;

        private void GenerateRegularButton_Click(object? sender, EventArgs e) =>
            GenerateRequested?.Invoke(this, e);

        private void BrowseRegularOutputButton_Click(object? sender, EventArgs e) =>
            OutputBrowseRequested?.Invoke(this, e);
    }
}
