namespace Page_switching
{
    partial class Auto
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
            axis_Location = new TextBox();
            axis_Speed = new TextBox();
            label1 = new Label();
            labelSpeed = new Label();
            label2 = new Label();
            SuspendLayout();
            // 
            // axis_Location
            // 
            axis_Location.Location = new Point(717, 44);
            axis_Location.Name = "axis_Location";
            axis_Location.ReadOnly = true;
            axis_Location.Size = new Size(125, 27);
            axis_Location.TabIndex = 0;
            // 
            // axis_Speed
            // 
            axis_Speed.Location = new Point(717, 82);
            axis_Speed.Name = "axis_Speed";
            axis_Speed.ReadOnly = true;
            axis_Speed.Size = new Size(125, 27);
            axis_Speed.TabIndex = 2;
            axis_Speed.Text = "--";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(631, 47);
            label1.Name = "label1";
            label1.Size = new Size(69, 20);
            label1.TabIndex = 1;
            label1.Text = "当前位置";
            // 
            // labelSpeed
            // 
            labelSpeed.AutoSize = true;
            labelSpeed.Location = new Point(631, 85);
            labelSpeed.Name = "labelSpeed";
            labelSpeed.Size = new Size(69, 20);
            labelSpeed.TabIndex = 4;
            labelSpeed.Text = "当前速度";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(701, 12);
            label2.Name = "label2";
            label2.Size = new Size(45, 20);
            label2.TabIndex = 6;
            label2.Text = "axis1";
            // 
            // Auto
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(label2);
            Controls.Add(labelSpeed);
            Controls.Add(axis_Speed);
            Controls.Add(label1);
            Controls.Add(axis_Location);
            Name = "Auto";
            Size = new Size(905, 681);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox axis_Location;
        private TextBox axis_Speed;
        private Label label1;
        private Label labelSpeed;
        private Label label2;
    }
}
