namespace Page_switching.panel
{
    partial class SignalCorrectionPage
    {
        private Panel pagePanel;

        private System.ComponentModel.IContainer components = null;
        private TableLayoutPanel root;
        private Label title;
        private TableLayoutPanel options;
        private TableLayoutPanel parameters;
        private TableLayoutPanel unitGroup;
        private TableLayoutPanel rateGroup;
        private TableLayoutPanel channelGroup;
        private TableLayoutPanel passesGroup;
        private TableLayoutPanel pointsGroup;
        private TableLayoutPanel actions;
        private TableLayoutPanel body;
        private Label status;
        private TextBox originalPath;
        private Button originalBrowse;
        private TextBox measuredPath;
        private Button measuredBrowse;
        private TextBox outputPath;
        private Button outputBrowse;
        private ComboBox unitInput;
        private NumericUpDown rateInput;
        private NumericUpDown channelInput;
        private NumericUpDown passesInput;
        private NumericUpDown pointsInput;
        private Button runButton;
        private Button previewButton;
        private Label caption_originalPath;
        private Label caption_measuredPath;
        private Label caption_outputPath;
        private Label caption_unitInput;
        private Label caption_rateInput;
        private Label caption_channelInput;
        private Label caption_passesInput;
        private Label caption_pointsInput;
        private global::Page_switching.ComparisonPlotControl plot;

        protected override void Dispose(bool disposing)
        {
            if (disposing) components?.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            plot = new ComparisonPlotControl();
            pagePanel = new Panel();
            root = new TableLayoutPanel();
            title = new Label();
            options = new TableLayoutPanel();
            caption_originalPath = new Label();
            originalPath = new TextBox();
            originalBrowse = new Button();
            caption_measuredPath = new Label();
            measuredPath = new TextBox();
            measuredBrowse = new Button();
            caption_outputPath = new Label();
            outputPath = new TextBox();
            outputBrowse = new Button();
            parameters = new TableLayoutPanel();
            unitGroup = new TableLayoutPanel();
            caption_unitInput = new Label();
            unitInput = new ComboBox();
            rateGroup = new TableLayoutPanel();
            caption_rateInput = new Label();
            rateInput = new NumericUpDown();
            channelGroup = new TableLayoutPanel();
            caption_channelInput = new Label();
            channelInput = new NumericUpDown();
            passesGroup = new TableLayoutPanel();
            caption_passesInput = new Label();
            passesInput = new NumericUpDown();
            pointsGroup = new TableLayoutPanel();
            caption_pointsInput = new Label();
            pointsInput = new NumericUpDown();
            actions = new TableLayoutPanel();
            previewButton = new Button();
            runButton = new Button();
            body = new TableLayoutPanel();
            status = new Label();
            pagePanel.SuspendLayout();
            root.SuspendLayout();
            options.SuspendLayout();
            parameters.SuspendLayout();
            unitGroup.SuspendLayout();
            rateGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)rateInput).BeginInit();
            channelGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)channelInput).BeginInit();
            passesGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)passesInput).BeginInit();
            pointsGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pointsInput).BeginInit();
            actions.SuspendLayout();
            body.SuspendLayout();
            SuspendLayout();
            //
            // plot
            //
            plot.BackColor = Color.White;
            plot.Dock = DockStyle.Fill;
            plot.FirstCaption = "原信号";
            plot.HorizontalCaption = "Time (s)";
            plot.Location = new Point(3, 3);
            plot.MinimumSize = new Size(300, 180);
            plot.Name = "plot";
            plot.SecondCaption = "修正信号";
            plot.Size = new Size(1066, 278);
            plot.TabIndex = 0;
            plot.VerticalCaption = "造波板位移(mm)";
            //
            // pagePanel
            //
            pagePanel.Controls.Add(root);
            pagePanel.Dock = DockStyle.Fill;
            pagePanel.Location = new Point(0, 0);
            pagePanel.MinimumSize = new Size(820, 560);
            pagePanel.Name = "pagePanel";
            pagePanel.Size = new Size(1104, 606);
            pagePanel.TabIndex = 0;
            //
            // root
            //
            root.BackColor = Color.FromArgb(241, 245, 249);
            root.ColumnCount = 1;
            root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            root.Controls.Add(title, 0, 0);
            root.Controls.Add(options, 0, 1);
            root.Controls.Add(body, 0, 2);
            root.Controls.Add(status, 0, 3);
            root.Dock = DockStyle.Fill;
            root.Location = new Point(0, 0);
            root.Margin = new Padding(0);
            root.Name = "root";
            root.Padding = new Padding(16);
            root.RowCount = 4;
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 210F));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
            root.Size = new Size(1104, 606);
            root.TabIndex = 0;
            //
            // title
            //
            title.Dock = DockStyle.Fill;
            title.Font = new Font("Microsoft YaHei UI", 16F, FontStyle.Bold);
            title.Location = new Point(16, 16);
            title.Margin = new Padding(0);
            title.Name = "title";
            title.Size = new Size(1072, 40);
            title.TabIndex = 0;
            title.Text = "造波信号修正";
            //
            // options
            //
            options.BackColor = Color.White;
            options.ColumnCount = 3;
            options.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 112F));
            options.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            options.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 128F));
            options.Controls.Add(caption_originalPath, 0, 0);
            options.Controls.Add(originalPath, 1, 0);
            options.Controls.Add(originalBrowse, 2, 0);
            options.Controls.Add(caption_measuredPath, 0, 1);
            options.Controls.Add(measuredPath, 1, 1);
            options.Controls.Add(measuredBrowse, 2, 1);
            options.Controls.Add(caption_outputPath, 0, 2);
            options.Controls.Add(outputPath, 1, 2);
            options.Controls.Add(outputBrowse, 2, 2);
            options.Controls.Add(parameters, 0, 3);
            options.Controls.Add(actions, 0, 4);
            options.Dock = DockStyle.Fill;
            options.Location = new Point(16, 56);
            options.Margin = new Padding(0);
            options.Name = "options";
            options.Padding = new Padding(8);
            options.RowCount = 5;
            options.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
            options.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
            options.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
            options.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
            options.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
            options.Size = new Size(1072, 210);
            options.TabIndex = 1;
            //
            // caption_originalPath
            //
            caption_originalPath.Dock = DockStyle.Fill;
            caption_originalPath.Location = new Point(8, 8);
            caption_originalPath.Margin = new Padding(0);
            caption_originalPath.Name = "caption_originalPath";
            caption_originalPath.Size = new Size(112, 34);
            caption_originalPath.TabIndex = 0;
            caption_originalPath.Text = "原造波信号";
            caption_originalPath.TextAlign = ContentAlignment.MiddleLeft;
            //
            // originalPath
            //
            originalPath.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            originalPath.BackColor = Color.White;
            originalPath.BorderStyle = BorderStyle.FixedSingle;
            originalPath.Location = new Point(120, 13);
            originalPath.Margin = new Padding(0);
            originalPath.Name = "originalPath";
            originalPath.ReadOnly = true;
            originalPath.Size = new Size(816, 23);
            originalPath.TabIndex = 1;
            //
            // originalBrowse
            //
            originalBrowse.BackColor = Color.FromArgb(248, 250, 252);
            originalBrowse.Dock = DockStyle.Fill;
            originalBrowse.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            originalBrowse.FlatStyle = FlatStyle.Flat;
            originalBrowse.Location = new Point(944, 9);
            originalBrowse.Margin = new Padding(8, 1, 0, 1);
            originalBrowse.Name = "originalBrowse";
            originalBrowse.Size = new Size(120, 32);
            originalBrowse.TabIndex = 2;
            originalBrowse.Text = "选择信号";
            originalBrowse.UseVisualStyleBackColor = false;
            //
            // caption_measuredPath
            //
            caption_measuredPath.Dock = DockStyle.Fill;
            caption_measuredPath.Location = new Point(8, 42);
            caption_measuredPath.Margin = new Padding(0);
            caption_measuredPath.Name = "caption_measuredPath";
            caption_measuredPath.Size = new Size(112, 34);
            caption_measuredPath.TabIndex = 3;
            caption_measuredPath.Text = "实测波浪文件";
            caption_measuredPath.TextAlign = ContentAlignment.MiddleLeft;
            //
            // measuredPath
            //
            measuredPath.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            measuredPath.BackColor = Color.White;
            measuredPath.BorderStyle = BorderStyle.FixedSingle;
            measuredPath.Location = new Point(120, 47);
            measuredPath.Margin = new Padding(0);
            measuredPath.Name = "measuredPath";
            measuredPath.ReadOnly = true;
            measuredPath.Size = new Size(816, 23);
            measuredPath.TabIndex = 4;
            //
            // measuredBrowse
            //
            measuredBrowse.BackColor = Color.FromArgb(248, 250, 252);
            measuredBrowse.Dock = DockStyle.Fill;
            measuredBrowse.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            measuredBrowse.FlatStyle = FlatStyle.Flat;
            measuredBrowse.Location = new Point(944, 43);
            measuredBrowse.Margin = new Padding(8, 1, 0, 1);
            measuredBrowse.Name = "measuredBrowse";
            measuredBrowse.Size = new Size(120, 32);
            measuredBrowse.TabIndex = 5;
            measuredBrowse.Text = "选择实测文件";
            measuredBrowse.UseVisualStyleBackColor = false;
            //
            // caption_outputPath
            //
            caption_outputPath.Dock = DockStyle.Fill;
            caption_outputPath.Location = new Point(8, 76);
            caption_outputPath.Margin = new Padding(0);
            caption_outputPath.Name = "caption_outputPath";
            caption_outputPath.Size = new Size(112, 34);
            caption_outputPath.TabIndex = 6;
            caption_outputPath.Text = "修正输出";
            caption_outputPath.TextAlign = ContentAlignment.MiddleLeft;
            //
            // outputPath
            //
            outputPath.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            outputPath.BackColor = Color.White;
            outputPath.BorderStyle = BorderStyle.FixedSingle;
            outputPath.Location = new Point(120, 81);
            outputPath.Margin = new Padding(0);
            outputPath.Name = "outputPath";
            outputPath.ReadOnly = true;
            outputPath.Size = new Size(816, 23);
            outputPath.TabIndex = 7;
            //
            // outputBrowse
            //
            outputBrowse.BackColor = Color.FromArgb(248, 250, 252);
            outputBrowse.Dock = DockStyle.Fill;
            outputBrowse.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            outputBrowse.FlatStyle = FlatStyle.Flat;
            outputBrowse.Location = new Point(944, 77);
            outputBrowse.Margin = new Padding(8, 1, 0, 1);
            outputBrowse.Name = "outputBrowse";
            outputBrowse.Size = new Size(120, 32);
            outputBrowse.TabIndex = 8;
            outputBrowse.Text = "保存路径";
            outputBrowse.UseVisualStyleBackColor = false;
            //
            // parameters
            //
            parameters.ColumnCount = 5;
            options.SetColumnSpan(parameters, 3);
            parameters.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            parameters.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            parameters.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            parameters.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            parameters.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            parameters.Controls.Add(unitGroup, 0, 0);
            parameters.Controls.Add(rateGroup, 1, 0);
            parameters.Controls.Add(channelGroup, 2, 0);
            parameters.Controls.Add(passesGroup, 3, 0);
            parameters.Controls.Add(pointsGroup, 4, 0);
            parameters.Dock = DockStyle.Fill;
            parameters.Location = new Point(8, 110);
            parameters.Margin = new Padding(0);
            parameters.Name = "parameters";
            parameters.RowCount = 1;
            parameters.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            parameters.Size = new Size(1056, 56);
            parameters.TabIndex = 9;
            //
            // unitGroup
            //
            unitGroup.ColumnCount = 1;
            unitGroup.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            unitGroup.Controls.Add(caption_unitInput, 0, 0);
            unitGroup.Controls.Add(unitInput, 0, 1);
            unitGroup.Dock = DockStyle.Fill;
            unitGroup.Location = new Point(0, 0);
            unitGroup.Margin = new Padding(0, 0, 12, 0);
            unitGroup.Name = "unitGroup";
            unitGroup.RowCount = 2;
            unitGroup.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            unitGroup.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            unitGroup.Size = new Size(199, 56);
            unitGroup.TabIndex = 0;
            //
            // caption_unitInput
            //
            caption_unitInput.Dock = DockStyle.Fill;
            caption_unitInput.Location = new Point(0, 0);
            caption_unitInput.Margin = new Padding(0);
            caption_unitInput.Name = "caption_unitInput";
            caption_unitInput.Size = new Size(199, 24);
            caption_unitInput.TabIndex = 0;
            caption_unitInput.Text = "旧文件/程序单位";
            caption_unitInput.TextAlign = ContentAlignment.MiddleLeft;
            //
            // unitInput
            //
            unitInput.Dock = DockStyle.Fill;
            unitInput.DropDownStyle = ComboBoxStyle.DropDownList;
            unitInput.Items.AddRange(new object[] { "mm", "m" });
            unitInput.Location = new Point(0, 24);
            unitInput.Margin = new Padding(0, 0, 0, 4);
            unitInput.Name = "unitInput";
            unitInput.Size = new Size(199, 25);
            unitInput.TabIndex = 1;
            //
            // rateGroup
            //
            rateGroup.ColumnCount = 1;
            rateGroup.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            rateGroup.Controls.Add(caption_rateInput, 0, 0);
            rateGroup.Controls.Add(rateInput, 0, 1);
            rateGroup.Dock = DockStyle.Fill;
            rateGroup.Location = new Point(211, 0);
            rateGroup.Margin = new Padding(0, 0, 12, 0);
            rateGroup.Name = "rateGroup";
            rateGroup.RowCount = 2;
            rateGroup.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            rateGroup.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            rateGroup.Size = new Size(199, 56);
            rateGroup.TabIndex = 1;
            //
            // caption_rateInput
            //
            caption_rateInput.Dock = DockStyle.Fill;
            caption_rateInput.Location = new Point(0, 0);
            caption_rateInput.Margin = new Padding(0);
            caption_rateInput.Name = "caption_rateInput";
            caption_rateInput.Size = new Size(199, 24);
            caption_rateInput.TabIndex = 0;
            caption_rateInput.Text = "CSV频率(Hz)";
            caption_rateInput.TextAlign = ContentAlignment.MiddleLeft;
            //
            // rateInput
            //
            rateInput.DecimalPlaces = 2;
            rateInput.Dock = DockStyle.Fill;
            rateInput.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            rateInput.Location = new Point(0, 24);
            rateInput.Margin = new Padding(0, 0, 0, 4);
            rateInput.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            rateInput.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            rateInput.Name = "rateInput";
            rateInput.Size = new Size(199, 23);
            rateInput.TabIndex = 1;
            rateInput.Value = new decimal(new int[] { 50, 0, 0, 0 });
            //
            // channelGroup
            //
            channelGroup.ColumnCount = 1;
            channelGroup.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            channelGroup.Controls.Add(caption_channelInput, 0, 0);
            channelGroup.Controls.Add(channelInput, 0, 1);
            channelGroup.Dock = DockStyle.Fill;
            channelGroup.Location = new Point(422, 0);
            channelGroup.Margin = new Padding(0, 0, 12, 0);
            channelGroup.Name = "channelGroup";
            channelGroup.RowCount = 2;
            channelGroup.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            channelGroup.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            channelGroup.Size = new Size(199, 56);
            channelGroup.TabIndex = 2;
            //
            // caption_channelInput
            //
            caption_channelInput.Dock = DockStyle.Fill;
            caption_channelInput.Location = new Point(0, 0);
            caption_channelInput.Margin = new Padding(0);
            caption_channelInput.Name = "caption_channelInput";
            caption_channelInput.Size = new Size(199, 24);
            caption_channelInput.TabIndex = 0;
            caption_channelInput.Text = "依据通道";
            caption_channelInput.TextAlign = ContentAlignment.MiddleLeft;
            //
            // channelInput
            //
            channelInput.Dock = DockStyle.Fill;
            channelInput.Location = new Point(0, 24);
            channelInput.Margin = new Padding(0, 0, 0, 4);
            channelInput.Maximum = new decimal(new int[] { 6, 0, 0, 0 });
            channelInput.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            channelInput.Name = "channelInput";
            channelInput.Size = new Size(199, 23);
            channelInput.TabIndex = 1;
            channelInput.Value = new decimal(new int[] { 1, 0, 0, 0 });
            //
            // passesGroup
            //
            passesGroup.ColumnCount = 1;
            passesGroup.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            passesGroup.Controls.Add(caption_passesInput, 0, 0);
            passesGroup.Controls.Add(passesInput, 0, 1);
            passesGroup.Dock = DockStyle.Fill;
            passesGroup.Location = new Point(633, 0);
            passesGroup.Margin = new Padding(0, 0, 12, 0);
            passesGroup.Name = "passesGroup";
            passesGroup.RowCount = 2;
            passesGroup.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            passesGroup.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            passesGroup.Size = new Size(199, 56);
            passesGroup.TabIndex = 3;
            //
            // caption_passesInput
            //
            caption_passesInput.Dock = DockStyle.Fill;
            caption_passesInput.Location = new Point(0, 0);
            caption_passesInput.Margin = new Padding(0);
            caption_passesInput.Name = "caption_passesInput";
            caption_passesInput.Size = new Size(199, 24);
            caption_passesInput.TabIndex = 0;
            caption_passesInput.Text = "光滑次数";
            caption_passesInput.TextAlign = ContentAlignment.MiddleLeft;
            //
            // passesInput
            //
            passesInput.Dock = DockStyle.Fill;
            passesInput.Location = new Point(0, 24);
            passesInput.Margin = new Padding(0, 0, 0, 4);
            passesInput.Maximum = new decimal(new int[] { 20, 0, 0, 0 });
            passesInput.Name = "passesInput";
            passesInput.Size = new Size(199, 23);
            passesInput.TabIndex = 1;
            passesInput.Value = new decimal(new int[] { 7, 0, 0, 0 });
            //
            // pointsGroup
            //
            pointsGroup.ColumnCount = 1;
            pointsGroup.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            pointsGroup.Controls.Add(caption_pointsInput, 0, 0);
            pointsGroup.Controls.Add(pointsInput, 0, 1);
            pointsGroup.Dock = DockStyle.Fill;
            pointsGroup.Location = new Point(844, 0);
            pointsGroup.Margin = new Padding(0);
            pointsGroup.Name = "pointsGroup";
            pointsGroup.RowCount = 2;
            pointsGroup.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            pointsGroup.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            pointsGroup.Size = new Size(212, 56);
            pointsGroup.TabIndex = 4;
            //
            // caption_pointsInput
            //
            caption_pointsInput.Dock = DockStyle.Fill;
            caption_pointsInput.Location = new Point(0, 0);
            caption_pointsInput.Margin = new Padding(0);
            caption_pointsInput.Name = "caption_pointsInput";
            caption_pointsInput.Size = new Size(212, 24);
            caption_pointsInput.TabIndex = 0;
            caption_pointsInput.Text = "光滑点数";
            caption_pointsInput.TextAlign = ContentAlignment.MiddleLeft;
            //
            // pointsInput
            //
            pointsInput.Dock = DockStyle.Fill;
            pointsInput.Location = new Point(0, 24);
            pointsInput.Margin = new Padding(0, 0, 0, 4);
            pointsInput.Maximum = new decimal(new int[] { 101, 0, 0, 0 });
            pointsInput.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            pointsInput.Name = "pointsInput";
            pointsInput.Size = new Size(212, 23);
            pointsInput.TabIndex = 1;
            pointsInput.Value = new decimal(new int[] { 3, 0, 0, 0 });
            //
            // actions
            //
            actions.ColumnCount = 3;
            options.SetColumnSpan(actions, 3);
            actions.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            actions.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 136F));
            actions.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 136F));
            actions.Controls.Add(previewButton, 1, 0);
            actions.Controls.Add(runButton, 2, 0);
            actions.Dock = DockStyle.Fill;
            actions.Location = new Point(8, 166);
            actions.Margin = new Padding(0);
            actions.Name = "actions";
            actions.RowCount = 1;
            actions.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            actions.Size = new Size(1056, 36);
            actions.TabIndex = 10;
            //
            // previewButton
            //
            previewButton.BackColor = Color.FromArgb(248, 250, 252);
            previewButton.Dock = DockStyle.Fill;
            previewButton.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            previewButton.FlatStyle = FlatStyle.Flat;
            previewButton.Location = new Point(792, 2);
            previewButton.Margin = new Padding(8, 2, 0, 2);
            previewButton.Name = "previewButton";
            previewButton.Size = new Size(128, 32);
            previewButton.TabIndex = 0;
            previewButton.Text = "新旧信号对比";
            previewButton.UseVisualStyleBackColor = false;
            //
            // runButton
            //
            runButton.BackColor = Color.FromArgb(29, 78, 216);
            runButton.Dock = DockStyle.Fill;
            runButton.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            runButton.FlatStyle = FlatStyle.Flat;
            runButton.ForeColor = Color.White;
            runButton.Location = new Point(928, 2);
            runButton.Margin = new Padding(8, 2, 0, 2);
            runButton.Name = "runButton";
            runButton.Size = new Size(128, 32);
            runButton.TabIndex = 1;
            runButton.Text = "修正并保存";
            runButton.UseVisualStyleBackColor = false;
            //
            // body
            //
            body.ColumnCount = 1;
            body.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            body.Controls.Add(plot, 0, 0);
            body.Dock = DockStyle.Fill;
            body.Location = new Point(16, 274);
            body.Margin = new Padding(0, 8, 0, 0);
            body.Name = "body";
            body.RowCount = 1;
            body.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            body.Size = new Size(1072, 284);
            body.TabIndex = 2;
            //
            // status
            //
            status.AutoEllipsis = true;
            status.Dock = DockStyle.Fill;
            status.ForeColor = Color.FromArgb(71, 85, 105);
            status.Location = new Point(16, 562);
            status.Margin = new Padding(0, 4, 0, 0);
            status.Name = "status";
            status.Size = new Size(1072, 28);
            status.TabIndex = 3;
            status.Text = "调用旧WC-12.exe；仅处理文件，不自动下发PLC。失败时保留原文件。";
            status.TextAlign = ContentAlignment.MiddleLeft;
            //
            // SignalCorrectionPage
            //
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoScroll = true;
            AutoScrollMinSize = new Size(820, 560);
            Controls.Add(pagePanel);
            Font = new Font("Microsoft YaHei UI", 9F);
            Name = "SignalCorrectionPage";
            Size = new Size(1104, 606);
            pagePanel.ResumeLayout(false);
            root.ResumeLayout(false);
            options.ResumeLayout(false);
            options.PerformLayout();
            parameters.ResumeLayout(false);
            unitGroup.ResumeLayout(false);
            rateGroup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)rateInput).EndInit();
            channelGroup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)channelInput).EndInit();
            passesGroup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)passesInput).EndInit();
            pointsGroup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pointsInput).EndInit();
            actions.ResumeLayout(false);
            body.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}
