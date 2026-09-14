namespace Page_switching
{
    partial class Manual
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            s7ipaddress = new TextBox();
            addressLabel = new Label();
            typeLabel = new Label();
            dataTypeComboBox = new ComboBox();
            valueLabel = new Label();
            writeValueTextBox = new TextBox();
            manualReadButton = new Button();
            manualWriteButton = new Button();
            display_log = new TextBox();
            SuspendLayout();
            //
            // addressLabel
            //
            addressLabel.AutoSize = true;
            addressLabel.Location = new Point(125, 95);
            addressLabel.Name = "addressLabel";
            addressLabel.Size = new Size(76, 20);
            addressLabel.TabIndex = 0;
            addressLabel.Text = "变量地址";
            //
            // s7ipaddress
            //
            s7ipaddress.Location = new Point(210, 92);
            s7ipaddress.Name = "s7ipaddress";
            s7ipaddress.Size = new Size(193, 27);
            s7ipaddress.TabIndex = 1;
            //
            // typeLabel
            //
            typeLabel.AutoSize = true;
            typeLabel.Location = new Point(430, 95);
            typeLabel.Name = "typeLabel";
            typeLabel.Size = new Size(38, 20);
            typeLabel.TabIndex = 2;
            typeLabel.Text = "类型";
            //
            // dataTypeComboBox
            //
            dataTypeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            dataTypeComboBox.FormattingEnabled = true;
            dataTypeComboBox.Items.AddRange(new object[] { "Bool", "Int16", "UInt16", "Int32", "UInt32", "Float", "Double", "Byte" });
            dataTypeComboBox.Location = new Point(474, 92);
            dataTypeComboBox.Name = "dataTypeComboBox";
            dataTypeComboBox.Size = new Size(100, 28);
            dataTypeComboBox.TabIndex = 3;
            dataTypeComboBox.SelectedIndex = 0;
            //
            // valueLabel
            //
            valueLabel.AutoSize = true;
            valueLabel.Location = new Point(590, 95);
            valueLabel.Name = "valueLabel";
            valueLabel.Size = new Size(53, 20);
            valueLabel.TabIndex = 4;
            valueLabel.Text = "写入值";
            //
            // writeValueTextBox
            //
            writeValueTextBox.Location = new Point(648, 92);
            writeValueTextBox.Name = "writeValueTextBox";
            writeValueTextBox.Size = new Size(100, 27);
            writeValueTextBox.TabIndex = 5;
            writeValueTextBox.Text = "false";
            //
            // manualReadButton
            //
            manualReadButton.Location = new Point(765, 90);
            manualReadButton.Name = "manualReadButton";
            manualReadButton.Size = new Size(62, 32);
            manualReadButton.TabIndex = 6;
            manualReadButton.Text = "读取";
            manualReadButton.UseVisualStyleBackColor = true;
            manualReadButton.Click += ManualReadButton_Click;
            //
            // manualWriteButton
            //
            manualWriteButton.Location = new Point(835, 90);
            manualWriteButton.Name = "manualWriteButton";
            manualWriteButton.Size = new Size(62, 32);
            manualWriteButton.TabIndex = 7;
            manualWriteButton.Text = "写入";
            manualWriteButton.UseVisualStyleBackColor = true;
            manualWriteButton.Click += ManualWriteButton_Click;
            //
            // display_log
            //
            display_log.Location = new Point(138, 295);
            display_log.Multiline = true;
            display_log.Name = "display_log";
            display_log.Size = new Size(507, 320);
            display_log.TabIndex = 8;
            //
            // Manual
            //
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(display_log);
            Controls.Add(manualWriteButton);
            Controls.Add(manualReadButton);
            Controls.Add(writeValueTextBox);
            Controls.Add(valueLabel);
            Controls.Add(dataTypeComboBox);
            Controls.Add(typeLabel);
            Controls.Add(s7ipaddress);
            Controls.Add(addressLabel);
            Name = "Manual";
            Size = new Size(905, 681);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label addressLabel;
        private TextBox s7ipaddress;
        private Label typeLabel;
        private ComboBox dataTypeComboBox;
        private Label valueLabel;
        private TextBox writeValueTextBox;
        private Button manualReadButton;
        private Button manualWriteButton;
        private TextBox display_log;
    }
}
