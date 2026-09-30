namespace Page_switching.panel
{
    public partial class IrregularWavePage : UserControl
    {
        public IrregularWavePage()
        {
            InitializeComponent();
        }

        internal event EventHandler? GenerateRequested;
        internal event EventHandler? OutputBrowseRequested;

        private void GenerateIrregularButton_Click(object? sender, EventArgs e) =>
            GenerateRequested?.Invoke(this, e);

        private void BrowseIrregularOutputButton_Click(object? sender, EventArgs e) =>
            OutputBrowseRequested?.Invoke(this, e);
    }
}
