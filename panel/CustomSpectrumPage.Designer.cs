namespace Page_switching.panel
{
    partial class CustomSpectrumPage
    {
        private System.ComponentModel.IContainer components = null;
        private Panel pagePanel;
        private TableLayoutPanel root;
        private GroupBox parameterGroup;
        private TableLayoutPanel options;
        private GroupBox previewGroup;
        private Label status;
        private TextBox spectrumPath;
        private Button spectrumBrowse;
        private TextBox outputPath;
        private Button outputBrowse;
        private NumericUpDown depthInput;
        private NumericUpDown stepInput;
        private NumericUpDown countInput;
        private NumericUpDown seedInput;
        private NumericUpDown frequencyInput;
        private NumericUpDown periodInput;
        private NumericUpDown angleInput;
        private ComboBox theoryInput;
        private ComboBox sideInput;
        private ComboBox modeInput;
        private CheckBox absorbInput;
        private Button runButton;
        private Button previewButton;
        private Label caption_spectrumPath;
        private Label caption_outputPath;
        private Label caption_depthInput;
        private Label caption_stepInput;
        private Label caption_countInput;
        private Label caption_seedInput;
        private Label caption_frequencyInput;
        private Label caption_periodInput;
        private Label caption_angleInput;
        private Label caption_theoryInput;
        private Label caption_sideInput;
        private Label caption_modeInput;
        private ComparisonPlotControl plot;

        protected override void Dispose(bool disposing)
        {
            if (disposing) components?.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            pagePanel = new Panel();
            root = new TableLayoutPanel();
            parameterGroup = new GroupBox();
            options = new TableLayoutPanel();
            caption_modeInput = new Label();
            modeInput = new ComboBox();
            caption_theoryInput = new Label();
            theoryInput = new ComboBox();
            caption_sideInput = new Label();
            sideInput = new ComboBox();
            caption_angleInput = new Label();
            angleInput = new NumericUpDown();
            caption_depthInput = new Label();
            depthInput = new NumericUpDown();
            caption_stepInput = new Label();
            stepInput = new NumericUpDown();
            caption_countInput = new Label();
            countInput = new NumericUpDown();
            caption_seedInput = new Label();
            seedInput = new NumericUpDown();
            caption_frequencyInput = new Label();
            frequencyInput = new NumericUpDown();
            caption_periodInput = new Label();
            periodInput = new NumericUpDown();
            absorbInput = new CheckBox();
            caption_spectrumPath = new Label();
            spectrumPath = new TextBox();
            spectrumBrowse = new Button();
            caption_outputPath = new Label();
            outputPath = new TextBox();
            outputBrowse = new Button();
            runButton = new Button();
            previewButton = new Button();
            status = new Label();
            previewGroup = new GroupBox();
            plot = new ComparisonPlotControl();
            pagePanel.SuspendLayout();
            root.SuspendLayout();
            parameterGroup.SuspendLayout();
            options.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)angleInput).BeginInit();
            ((System.ComponentModel.ISupportInitialize)depthInput).BeginInit();
            ((System.ComponentModel.ISupportInitialize)stepInput).BeginInit();
            ((System.ComponentModel.ISupportInitialize)countInput).BeginInit();
            ((System.ComponentModel.ISupportInitialize)seedInput).BeginInit();
            ((System.ComponentModel.ISupportInitialize)frequencyInput).BeginInit();
            ((System.ComponentModel.ISupportInitialize)periodInput).BeginInit();
            previewGroup.SuspendLayout();
            SuspendLayout();
            //
            // pagePanel
            //
            pagePanel.Controls.Add(root);
            pagePanel.Dock = DockStyle.Fill;
            pagePanel.Location = new Point(0, 0);
            pagePanel.Name = "pagePanel";
            pagePanel.Padding = new Padding(8);
            pagePanel.Size = new Size(1104, 606);
            pagePanel.TabIndex = 0;
            //
            // root
            //
            root.ColumnCount = 2;
            root.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 480F));
            root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            root.Controls.Add(parameterGroup, 0, 0);
            root.Controls.Add(previewGroup, 1, 0);
            root.Dock = DockStyle.Fill;
            root.Location = new Point(8, 8);
            root.MinimumSize = new Size(930, 460);
            root.Name = "root";
            root.RowCount = 1;
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            root.Size = new Size(1088, 590);
            root.TabIndex = 0;
            //
            // parameterGroup
            //
            parameterGroup.BackColor = Color.White;
            parameterGroup.Controls.Add(options);
            parameterGroup.Dock = DockStyle.Fill;
            parameterGroup.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
            parameterGroup.ForeColor = Color.FromArgb(15, 23, 42);
            parameterGroup.Location = new Point(3, 3);
            parameterGroup.Name = "parameterGroup";
            parameterGroup.Padding = new Padding(10, 16, 10, 10);
            parameterGroup.Size = new Size(474, 584);
            parameterGroup.TabIndex = 0;
            parameterGroup.TabStop = false;
            parameterGroup.Text = "自定义谱参数";
            //
            // options
            //
            options.AutoSize = true;
            options.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            options.ColumnCount = 4;
            options.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            options.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 115F));
            options.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            options.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            options.Controls.Add(caption_modeInput, 0, 0);
            options.Controls.Add(modeInput, 1, 0);
            options.Controls.Add(caption_theoryInput, 2, 0);
            options.Controls.Add(theoryInput, 3, 0);
            options.Controls.Add(caption_sideInput, 0, 1);
            options.Controls.Add(sideInput, 1, 1);
            options.Controls.Add(caption_angleInput, 2, 1);
            options.Controls.Add(angleInput, 3, 1);
            options.Controls.Add(caption_depthInput, 0, 2);
            options.Controls.Add(depthInput, 1, 2);
            options.Controls.Add(caption_stepInput, 2, 2);
            options.Controls.Add(stepInput, 3, 2);
            options.Controls.Add(caption_countInput, 0, 3);
            options.Controls.Add(countInput, 1, 3);
            options.Controls.Add(caption_seedInput, 2, 3);
            options.Controls.Add(seedInput, 3, 3);
            options.Controls.Add(caption_frequencyInput, 0, 4);
            options.Controls.Add(frequencyInput, 1, 4);
            options.Controls.Add(caption_periodInput, 2, 4);
            options.Controls.Add(periodInput, 3, 4);
            options.Controls.Add(absorbInput, 0, 5);
            options.Controls.Add(caption_spectrumPath, 0, 6);
            options.Controls.Add(spectrumPath, 1, 6);
            options.Controls.Add(spectrumBrowse, 3, 6);
            options.Controls.Add(caption_outputPath, 0, 7);
            options.Controls.Add(outputPath, 1, 7);
            options.Controls.Add(outputBrowse, 3, 7);
            options.Controls.Add(runButton, 0, 8);
            options.Controls.Add(previewButton, 2, 8);
            options.Controls.Add(status, 0, 9);
            options.Dock = DockStyle.Top;
            options.Location = new Point(10, 32);
            options.Name = "options";
            options.RowCount = 10;
            options.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
            options.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
            options.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
            options.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
            options.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
            options.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
            options.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            options.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            options.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
            options.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            options.Size = new Size(454, 388);
            options.TabIndex = 0;
            //
            // caption_modeInput
            //
            caption_modeInput.Dock = DockStyle.Fill;
            caption_modeInput.Font = new Font("Microsoft YaHei UI", 8.5F);
            caption_modeInput.ForeColor = Color.FromArgb(71, 85, 105);
            caption_modeInput.Location = new Point(3, 0);
            caption_modeInput.Name = "caption_modeInput";
            caption_modeInput.Size = new Size(94, 34);
            caption_modeInput.TabIndex = 0;
            caption_modeInput.Text = "造波模式";
            caption_modeInput.TextAlign = ContentAlignment.MiddleLeft;
            //
            // modeInput
            //
            modeInput.BackColor = Color.White;
            modeInput.Dock = DockStyle.Fill;
            modeInput.DropDownStyle = ComboBoxStyle.DropDownList;
            modeInput.FlatStyle = FlatStyle.Flat;
            modeInput.Font = new Font("Microsoft YaHei UI", 9F);
            modeInput.ForeColor = Color.FromArgb(15, 23, 42);
            modeInput.Items.AddRange(new object[] { "单向", "多向" });
            modeInput.Location = new Point(103, 5);
            modeInput.Margin = new Padding(3, 5, 3, 5);
            modeInput.Name = "modeInput";
            modeInput.Size = new Size(109, 25);
            modeInput.TabIndex = 1;
            //
            // caption_theoryInput
            //
            caption_theoryInput.Dock = DockStyle.Fill;
            caption_theoryInput.Font = new Font("Microsoft YaHei UI", 8.5F);
            caption_theoryInput.ForeColor = Color.FromArgb(71, 85, 105);
            caption_theoryInput.Location = new Point(218, 0);
            caption_theoryInput.Name = "caption_theoryInput";
            caption_theoryInput.Size = new Size(94, 34);
            caption_theoryInput.TabIndex = 2;
            caption_theoryInput.Text = "造波理论";
            caption_theoryInput.TextAlign = ContentAlignment.MiddleLeft;
            //
            // theoryInput
            //
            theoryInput.BackColor = Color.White;
            theoryInput.Dock = DockStyle.Fill;
            theoryInput.DropDownStyle = ComboBoxStyle.DropDownList;
            theoryInput.FlatStyle = FlatStyle.Flat;
            theoryInput.Font = new Font("Microsoft YaHei UI", 9F);
            theoryInput.ForeColor = Color.FromArgb(15, 23, 42);
            theoryInput.Items.AddRange(new object[] { "线性", "非线性" });
            theoryInput.Location = new Point(318, 5);
            theoryInput.Margin = new Padding(3, 5, 3, 5);
            theoryInput.Name = "theoryInput";
            theoryInput.Size = new Size(133, 25);
            theoryInput.TabIndex = 3;
            //
            // caption_sideInput
            //
            caption_sideInput.Dock = DockStyle.Fill;
            caption_sideInput.Font = new Font("Microsoft YaHei UI", 8.5F);
            caption_sideInput.ForeColor = Color.FromArgb(71, 85, 105);
            caption_sideInput.Location = new Point(3, 34);
            caption_sideInput.Name = "caption_sideInput";
            caption_sideInput.Size = new Size(94, 34);
            caption_sideInput.TabIndex = 4;
            caption_sideInput.Text = "造波段";
            caption_sideInput.TextAlign = ContentAlignment.MiddleLeft;
            //
            // sideInput
            //
            sideInput.BackColor = Color.White;
            sideInput.Dock = DockStyle.Fill;
            sideInput.DropDownStyle = ComboBoxStyle.DropDownList;
            sideInput.FlatStyle = FlatStyle.Flat;
            sideInput.Font = new Font("Microsoft YaHei UI", 9F);
            sideInput.ForeColor = Color.FromArgb(15, 23, 42);
            sideInput.Items.AddRange(new object[] { "X轴+Y轴", "X轴", "Y轴" });
            sideInput.Location = new Point(103, 39);
            sideInput.Margin = new Padding(3, 5, 3, 5);
            sideInput.Name = "sideInput";
            sideInput.Size = new Size(109, 25);
            sideInput.TabIndex = 5;
            //
            // caption_angleInput
            //
            caption_angleInput.Dock = DockStyle.Fill;
            caption_angleInput.Font = new Font("Microsoft YaHei UI", 8.5F);
            caption_angleInput.ForeColor = Color.FromArgb(71, 85, 105);
            caption_angleInput.Location = new Point(218, 34);
            caption_angleInput.Name = "caption_angleInput";
            caption_angleInput.Size = new Size(94, 34);
            caption_angleInput.TabIndex = 6;
            caption_angleInput.Text = "波向偏角";
            caption_angleInput.TextAlign = ContentAlignment.MiddleLeft;
            //
            // angleInput
            //
            angleInput.BackColor = Color.White;
            angleInput.BorderStyle = BorderStyle.FixedSingle;
            angleInput.DecimalPlaces = 3;
            angleInput.Dock = DockStyle.Fill;
            angleInput.Font = new Font("Microsoft YaHei UI", 9F);
            angleInput.ForeColor = Color.FromArgb(15, 23, 42);
            angleInput.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            angleInput.Location = new Point(318, 39);
            angleInput.Margin = new Padding(3, 5, 3, 5);
            angleInput.Maximum = new decimal(new int[] { 180, 0, 0, 0 });
            angleInput.Name = "angleInput";
            angleInput.Size = new Size(133, 23);
            angleInput.TabIndex = 7;
            angleInput.Value = new decimal(new int[] { 6, 0, 0, 65536 });
            //
            // caption_depthInput
            //
            caption_depthInput.Dock = DockStyle.Fill;
            caption_depthInput.Font = new Font("Microsoft YaHei UI", 8.5F);
            caption_depthInput.ForeColor = Color.FromArgb(71, 85, 105);
            caption_depthInput.Location = new Point(3, 68);
            caption_depthInput.Name = "caption_depthInput";
            caption_depthInput.Size = new Size(94, 34);
            caption_depthInput.TabIndex = 8;
            caption_depthInput.Text = "水深(m)";
            caption_depthInput.TextAlign = ContentAlignment.MiddleLeft;
            //
            // depthInput
            //
            depthInput.BackColor = Color.White;
            depthInput.BorderStyle = BorderStyle.FixedSingle;
            depthInput.DecimalPlaces = 3;
            depthInput.Dock = DockStyle.Fill;
            depthInput.Font = new Font("Microsoft YaHei UI", 9F);
            depthInput.ForeColor = Color.FromArgb(15, 23, 42);
            depthInput.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            depthInput.Location = new Point(103, 73);
            depthInput.Margin = new Padding(3, 5, 3, 5);
            depthInput.Minimum = new decimal(new int[] { 1, 0, 0, 196608 });
            depthInput.Name = "depthInput";
            depthInput.Size = new Size(109, 23);
            depthInput.TabIndex = 9;
            depthInput.Value = new decimal(new int[] { 6, 0, 0, 65536 });
            //
            // caption_stepInput
            //
            caption_stepInput.Dock = DockStyle.Fill;
            caption_stepInput.Font = new Font("Microsoft YaHei UI", 8.5F);
            caption_stepInput.ForeColor = Color.FromArgb(71, 85, 105);
            caption_stepInput.Location = new Point(218, 68);
            caption_stepInput.Name = "caption_stepInput";
            caption_stepInput.Size = new Size(94, 34);
            caption_stepInput.TabIndex = 10;
            caption_stepInput.Text = "步长(s)";
            caption_stepInput.TextAlign = ContentAlignment.MiddleLeft;
            //
            // stepInput
            //
            stepInput.BackColor = Color.White;
            stepInput.BorderStyle = BorderStyle.FixedSingle;
            stepInput.DecimalPlaces = 3;
            stepInput.Dock = DockStyle.Fill;
            stepInput.Font = new Font("Microsoft YaHei UI", 9F);
            stepInput.ForeColor = Color.FromArgb(15, 23, 42);
            stepInput.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            stepInput.Location = new Point(318, 73);
            stepInput.Margin = new Padding(3, 5, 3, 5);
            stepInput.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            stepInput.Minimum = new decimal(new int[] { 2, 0, 0, 196608 });
            stepInput.Name = "stepInput";
            stepInput.Size = new Size(133, 23);
            stepInput.TabIndex = 11;
            stepInput.Value = new decimal(new int[] { 2, 0, 0, 131072 });
            //
            // caption_countInput
            //
            caption_countInput.Dock = DockStyle.Fill;
            caption_countInput.Font = new Font("Microsoft YaHei UI", 8.5F);
            caption_countInput.ForeColor = Color.FromArgb(71, 85, 105);
            caption_countInput.Location = new Point(3, 102);
            caption_countInput.Name = "caption_countInput";
            caption_countInput.Size = new Size(94, 34);
            caption_countInput.TabIndex = 12;
            caption_countInput.Text = "采样点数";
            caption_countInput.TextAlign = ContentAlignment.MiddleLeft;
            //
            // countInput
            //
            countInput.BackColor = Color.White;
            countInput.BorderStyle = BorderStyle.FixedSingle;
            countInput.Dock = DockStyle.Fill;
            countInput.Font = new Font("Microsoft YaHei UI", 9F);
            countInput.ForeColor = Color.FromArgb(15, 23, 42);
            countInput.Location = new Point(103, 107);
            countInput.Margin = new Padding(3, 5, 3, 5);
            countInput.Maximum = new decimal(new int[] { 131072, 0, 0, 0 });
            countInput.Minimum = new decimal(new int[] { 64, 0, 0, 0 });
            countInput.Name = "countInput";
            countInput.Size = new Size(109, 23);
            countInput.TabIndex = 13;
            countInput.Value = new decimal(new int[] { 8192, 0, 0, 0 });
            //
            // caption_seedInput
            //
            caption_seedInput.Dock = DockStyle.Fill;
            caption_seedInput.Font = new Font("Microsoft YaHei UI", 8.5F);
            caption_seedInput.ForeColor = Color.FromArgb(71, 85, 105);
            caption_seedInput.Location = new Point(218, 102);
            caption_seedInput.Name = "caption_seedInput";
            caption_seedInput.Size = new Size(94, 34);
            caption_seedInput.TabIndex = 14;
            caption_seedInput.Text = "随机种子";
            caption_seedInput.TextAlign = ContentAlignment.MiddleLeft;
            //
            // seedInput
            //
            seedInput.BackColor = Color.White;
            seedInput.BorderStyle = BorderStyle.FixedSingle;
            seedInput.Dock = DockStyle.Fill;
            seedInput.Font = new Font("Microsoft YaHei UI", 9F);
            seedInput.ForeColor = Color.FromArgb(15, 23, 42);
            seedInput.Location = new Point(318, 107);
            seedInput.Margin = new Padding(3, 5, 3, 5);
            seedInput.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            seedInput.Name = "seedInput";
            seedInput.Size = new Size(133, 23);
            seedInput.TabIndex = 15;
            seedInput.Value = new decimal(new int[] { 30, 0, 0, 0 });
            //
            // caption_frequencyInput
            //
            caption_frequencyInput.Dock = DockStyle.Fill;
            caption_frequencyInput.Font = new Font("Microsoft YaHei UI", 8.5F);
            caption_frequencyInput.ForeColor = Color.FromArgb(71, 85, 105);
            caption_frequencyInput.Location = new Point(3, 136);
            caption_frequencyInput.Name = "caption_frequencyInput";
            caption_frequencyInput.Size = new Size(94, 34);
            caption_frequencyInput.TabIndex = 16;
            caption_frequencyInput.Text = "特征频率(Hz)";
            caption_frequencyInput.TextAlign = ContentAlignment.MiddleLeft;
            //
            // frequencyInput
            //
            frequencyInput.BackColor = Color.White;
            frequencyInput.BorderStyle = BorderStyle.FixedSingle;
            frequencyInput.DecimalPlaces = 3;
            frequencyInput.Dock = DockStyle.Fill;
            frequencyInput.Font = new Font("Microsoft YaHei UI", 9F);
            frequencyInput.ForeColor = Color.FromArgb(15, 23, 42);
            frequencyInput.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            frequencyInput.Location = new Point(103, 141);
            frequencyInput.Margin = new Padding(3, 5, 3, 5);
            frequencyInput.Minimum = new decimal(new int[] { 1, 0, 0, 196608 });
            frequencyInput.Name = "frequencyInput";
            frequencyInput.Size = new Size(109, 23);
            frequencyInput.TabIndex = 17;
            frequencyInput.Value = new decimal(new int[] { 514, 0, 0, 196608 });
            //
            // caption_periodInput
            //
            caption_periodInput.Dock = DockStyle.Fill;
            caption_periodInput.Font = new Font("Microsoft YaHei UI", 8.5F);
            caption_periodInput.ForeColor = Color.FromArgb(71, 85, 105);
            caption_periodInput.Location = new Point(218, 136);
            caption_periodInput.Name = "caption_periodInput";
            caption_periodInput.Size = new Size(94, 34);
            caption_periodInput.TabIndex = 18;
            caption_periodInput.Text = "特征周期(s)";
            caption_periodInput.TextAlign = ContentAlignment.MiddleLeft;
            //
            // periodInput
            //
            periodInput.BackColor = Color.White;
            periodInput.BorderStyle = BorderStyle.FixedSingle;
            periodInput.DecimalPlaces = 3;
            periodInput.Dock = DockStyle.Fill;
            periodInput.Font = new Font("Microsoft YaHei UI", 9F);
            periodInput.ForeColor = Color.FromArgb(15, 23, 42);
            periodInput.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            periodInput.Location = new Point(318, 141);
            periodInput.Margin = new Padding(3, 5, 3, 5);
            periodInput.Minimum = new decimal(new int[] { 1, 0, 0, 196608 });
            periodInput.Name = "periodInput";
            periodInput.Size = new Size(133, 23);
            periodInput.TabIndex = 19;
            periodInput.Value = new decimal(new int[] { 4, 0, 0, 0 });
            //
            // absorbInput
            //
            options.SetColumnSpan(absorbInput, 4);
            absorbInput.Dock = DockStyle.Fill;
            absorbInput.Font = new Font("Microsoft YaHei UI", 9F);
            absorbInput.ForeColor = Color.FromArgb(15, 23, 42);
            absorbInput.Location = new Point(3, 175);
            absorbInput.Margin = new Padding(3, 5, 3, 5);
            absorbInput.Name = "absorbInput";
            absorbInput.Size = new Size(448, 24);
            absorbInput.TabIndex = 20;
            absorbInput.Text = "吸收式 (0.002s)";
            //
            // caption_spectrumPath
            //
            caption_spectrumPath.Dock = DockStyle.Fill;
            caption_spectrumPath.Font = new Font("Microsoft YaHei UI", 8.5F);
            caption_spectrumPath.ForeColor = Color.FromArgb(71, 85, 105);
            caption_spectrumPath.Location = new Point(3, 204);
            caption_spectrumPath.Name = "caption_spectrumPath";
            caption_spectrumPath.Size = new Size(94, 40);
            caption_spectrumPath.TabIndex = 21;
            caption_spectrumPath.Text = "输入谱(.dat)";
            caption_spectrumPath.TextAlign = ContentAlignment.MiddleLeft;
            //
            // spectrumPath
            //
            spectrumPath.BackColor = Color.White;
            spectrumPath.BorderStyle = BorderStyle.FixedSingle;
            options.SetColumnSpan(spectrumPath, 2);
            spectrumPath.Dock = DockStyle.Fill;
            spectrumPath.Font = new Font("Microsoft YaHei UI", 9F);
            spectrumPath.ForeColor = Color.FromArgb(15, 23, 42);
            spectrumPath.Location = new Point(103, 209);
            spectrumPath.Margin = new Padding(3, 5, 3, 5);
            spectrumPath.Name = "spectrumPath";
            spectrumPath.ReadOnly = true;
            spectrumPath.Size = new Size(209, 23);
            spectrumPath.TabIndex = 22;
            //
            // spectrumBrowse
            //
            spectrumBrowse.BackColor = Color.FromArgb(248, 250, 252);
            spectrumBrowse.Cursor = Cursors.Hand;
            spectrumBrowse.Dock = DockStyle.Fill;
            spectrumBrowse.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            spectrumBrowse.FlatAppearance.MouseOverBackColor = Color.FromArgb(226, 232, 240);
            spectrumBrowse.FlatStyle = FlatStyle.Flat;
            spectrumBrowse.Font = new Font("Microsoft YaHei UI", 9F);
            spectrumBrowse.ForeColor = Color.FromArgb(15, 23, 42);
            spectrumBrowse.Location = new Point(318, 208);
            spectrumBrowse.Margin = new Padding(3, 4, 3, 4);
            spectrumBrowse.Name = "spectrumBrowse";
            spectrumBrowse.Size = new Size(133, 32);
            spectrumBrowse.TabIndex = 23;
            spectrumBrowse.Text = "浏览";
            spectrumBrowse.UseVisualStyleBackColor = false;
            //
            // caption_outputPath
            //
            caption_outputPath.Dock = DockStyle.Fill;
            caption_outputPath.Font = new Font("Microsoft YaHei UI", 8.5F);
            caption_outputPath.ForeColor = Color.FromArgb(71, 85, 105);
            caption_outputPath.Location = new Point(3, 244);
            caption_outputPath.Name = "caption_outputPath";
            caption_outputPath.Size = new Size(94, 40);
            caption_outputPath.TabIndex = 24;
            caption_outputPath.Text = "保存文件";
            caption_outputPath.TextAlign = ContentAlignment.MiddleLeft;
            //
            // outputPath
            //
            outputPath.BackColor = Color.White;
            outputPath.BorderStyle = BorderStyle.FixedSingle;
            options.SetColumnSpan(outputPath, 2);
            outputPath.Dock = DockStyle.Fill;
            outputPath.Font = new Font("Microsoft YaHei UI", 9F);
            outputPath.ForeColor = Color.FromArgb(15, 23, 42);
            outputPath.Location = new Point(103, 249);
            outputPath.Margin = new Padding(3, 5, 3, 5);
            outputPath.Name = "outputPath";
            outputPath.ReadOnly = true;
            outputPath.Size = new Size(209, 23);
            outputPath.TabIndex = 25;
            //
            // outputBrowse
            //
            outputBrowse.BackColor = Color.FromArgb(248, 250, 252);
            outputBrowse.Cursor = Cursors.Hand;
            outputBrowse.Dock = DockStyle.Fill;
            outputBrowse.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            outputBrowse.FlatAppearance.MouseOverBackColor = Color.FromArgb(226, 232, 240);
            outputBrowse.FlatStyle = FlatStyle.Flat;
            outputBrowse.Font = new Font("Microsoft YaHei UI", 9F);
            outputBrowse.ForeColor = Color.FromArgb(15, 23, 42);
            outputBrowse.Location = new Point(318, 248);
            outputBrowse.Margin = new Padding(3, 4, 3, 4);
            outputBrowse.Name = "outputBrowse";
            outputBrowse.Size = new Size(133, 32);
            outputBrowse.TabIndex = 26;
            outputBrowse.Text = "浏览";
            outputBrowse.UseVisualStyleBackColor = false;
            //
            // runButton
            //
            runButton.BackColor = Color.FromArgb(29, 78, 216);
            options.SetColumnSpan(runButton, 2);
            runButton.Cursor = Cursors.Hand;
            runButton.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            runButton.FlatAppearance.BorderSize = 0;
            runButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(30, 64, 175);
            runButton.FlatStyle = FlatStyle.Flat;
            runButton.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
            runButton.ForeColor = Color.White;
            runButton.Location = new Point(3, 287);
            runButton.Name = "runButton";
            runButton.Size = new Size(160, 34);
            runButton.TabIndex = 27;
            runButton.Text = "计算并保存";
            runButton.UseVisualStyleBackColor = false;
            //
            // previewButton
            //
            previewButton.BackColor = Color.FromArgb(248, 250, 252);
            options.SetColumnSpan(previewButton, 2);
            previewButton.Cursor = Cursors.Hand;
            previewButton.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            previewButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(226, 232, 240);
            previewButton.FlatStyle = FlatStyle.Flat;
            previewButton.Font = new Font("Microsoft YaHei UI", 9F);
            previewButton.ForeColor = Color.FromArgb(15, 23, 42);
            previewButton.Location = new Point(218, 287);
            previewButton.Name = "previewButton";
            previewButton.Size = new Size(160, 34);
            previewButton.TabIndex = 28;
            previewButton.Text = "查看输入谱";
            previewButton.UseVisualStyleBackColor = false;
            //
            // status
            //
            status.AutoEllipsis = true;
            options.SetColumnSpan(status, 4);
            status.Dock = DockStyle.Fill;
            status.Font = new Font("Microsoft YaHei UI", 8.5F);
            status.ForeColor = Color.FromArgb(71, 85, 105);
            status.Location = new Point(3, 328);
            status.Name = "status";
            status.Size = new Size(448, 60);
            status.TabIndex = 29;
            status.Text = "请选择输入波谱和 CSV 保存路径";
            status.TextAlign = ContentAlignment.MiddleLeft;
            //
            // previewGroup
            //
            previewGroup.BackColor = Color.White;
            previewGroup.Controls.Add(plot);
            previewGroup.Dock = DockStyle.Fill;
            previewGroup.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
            previewGroup.ForeColor = Color.FromArgb(15, 23, 42);
            previewGroup.Location = new Point(483, 3);
            previewGroup.Name = "previewGroup";
            previewGroup.Padding = new Padding(10, 18, 10, 10);
            previewGroup.Size = new Size(602, 584);
            previewGroup.TabIndex = 1;
            previewGroup.TabStop = false;
            previewGroup.Text = "波谱与波形预览";
            //
            // plot
            //
            plot.BackColor = Color.White;
            plot.Dock = DockStyle.Fill;
            plot.FirstCaption = "输入谱";
            plot.Font = new Font("Microsoft YaHei UI", 9F);
            plot.ForeColor = Color.FromArgb(29, 78, 216);
            plot.HorizontalCaption = "Frequency (Hz)";
            plot.Location = new Point(10, 34);
            plot.MinimumSize = new Size(300, 180);
            plot.Name = "plot";
            plot.SecondCaption = "实测谱";
            plot.Size = new Size(582, 540);
            plot.TabIndex = 0;
            plot.VerticalCaption = "S(f) (m²·s)";
            //
            // CustomSpectrumPage
            //
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoScroll = true;
            AutoScrollMinSize = new Size(946, 476);
            BackColor = Color.FromArgb(241, 245, 249);
            Controls.Add(pagePanel);
            Font = new Font("Microsoft YaHei UI", 9F);
            ForeColor = Color.FromArgb(15, 23, 42);
            Name = "CustomSpectrumPage";
            Size = new Size(1104, 606);
            pagePanel.ResumeLayout(false);
            root.ResumeLayout(false);
            parameterGroup.ResumeLayout(false);
            parameterGroup.PerformLayout();
            options.ResumeLayout(false);
            options.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)angleInput).EndInit();
            ((System.ComponentModel.ISupportInitialize)depthInput).EndInit();
            ((System.ComponentModel.ISupportInitialize)stepInput).EndInit();
            ((System.ComponentModel.ISupportInitialize)countInput).EndInit();
            ((System.ComponentModel.ISupportInitialize)seedInput).EndInit();
            ((System.ComponentModel.ISupportInitialize)frequencyInput).EndInit();
            ((System.ComponentModel.ISupportInitialize)periodInput).EndInit();
            previewGroup.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}
