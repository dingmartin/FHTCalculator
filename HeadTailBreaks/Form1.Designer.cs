namespace HeadTailBreaks
{
    partial class Form1
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.button1 = new System.Windows.Forms.Button();
            this.InputFileAddress = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.HeadPercentage = new System.Windows.Forms.TextBox();
            this.TailPercentage = new System.Windows.Forms.Label();
            this.BreakResult = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.htIndex = new System.Windows.Forms.Label();
            this.data = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.hts = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.clearBox = new System.Windows.Forms.Button();
            this.CRGIndex = new System.Windows.Forms.Label();
            this.Copy = new System.Windows.Forms.Button();
            this.ResultTemp = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(280, 40);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 29);
            this.button1.TabIndex = 0;
            this.button1.Text = "Go";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // InputFileAddress
            // 
            this.InputFileAddress.AllowDrop = true;
            this.InputFileAddress.Enabled = false;
            this.InputFileAddress.Location = new System.Drawing.Point(164, 360);
            this.InputFileAddress.Margin = new System.Windows.Forms.Padding(4);
            this.InputFileAddress.Name = "InputFileAddress";
            this.InputFileAddress.Size = new System.Drawing.Size(41, 25);
            this.InputFileAddress.TabIndex = 2;
            this.InputFileAddress.Visible = false;
            this.InputFileAddress.TextChanged += new System.EventHandler(this.InputFileAddress_TextChanged);
            this.InputFileAddress.DragDrop += new System.Windows.Forms.DragEventHandler(this.txt_ObjDragDrop);
            this.InputFileAddress.DragEnter += new System.Windows.Forms.DragEventHandler(this.txt_ObjDragEnter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(265, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(151, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "Set the threshold:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(421, 9);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "Head:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(533, 9);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "Tail:";
            // 
            // HeadPercentage
            // 
            this.HeadPercentage.Location = new System.Drawing.Point(476, 6);
            this.HeadPercentage.Margin = new System.Windows.Forms.Padding(4);
            this.HeadPercentage.Name = "HeadPercentage";
            this.HeadPercentage.Size = new System.Drawing.Size(37, 25);
            this.HeadPercentage.TabIndex = 6;
            this.HeadPercentage.Text = "40";
            this.HeadPercentage.TextChanged += new System.EventHandler(this.HeadPercentage_TextChanged);
            this.HeadPercentage.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.HeadPercentage_KeyPress);
            this.HeadPercentage.MouseLeave += new System.EventHandler(this.HeadPercentage_MouseLeave);
            // 
            // TailPercentage
            // 
            this.TailPercentage.AutoSize = true;
            this.TailPercentage.Location = new System.Drawing.Point(583, 9);
            this.TailPercentage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.TailPercentage.Name = "TailPercentage";
            this.TailPercentage.Size = new System.Drawing.Size(0, 15);
            this.TailPercentage.TabIndex = 7;
            // 
            // BreakResult
            // 
            this.BreakResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.BreakResult.Location = new System.Drawing.Point(236, 77);
            this.BreakResult.Margin = new System.Windows.Forms.Padding(4);
            this.BreakResult.Multiline = true;
            this.BreakResult.Name = "BreakResult";
            this.BreakResult.ReadOnly = true;
            this.BreakResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.BreakResult.Size = new System.Drawing.Size(585, 279);
            this.BreakResult.TabIndex = 8;
            this.BreakResult.TextChanged += new System.EventHandler(this.BreakResult_TextChanged);
            this.BreakResult.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.BreakResult_MouseDoubleClick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(410, 47);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 15);
            this.label4.TabIndex = 9;
            this.label4.Text = "Ht-index:";
            // 
            // htIndex
            // 
            this.htIndex.AutoSize = true;
            this.htIndex.Location = new System.Drawing.Point(508, 47);
            this.htIndex.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.htIndex.Name = "htIndex";
            this.htIndex.Size = new System.Drawing.Size(0, 15);
            this.htIndex.TabIndex = 10;
            // 
            // data
            // 
            this.data.Location = new System.Drawing.Point(27, 28);
            this.data.Margin = new System.Windows.Forms.Padding(4);
            this.data.MaxLength = 0;
            this.data.Multiline = true;
            this.data.Name = "data";
            this.data.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.data.Size = new System.Drawing.Size(187, 328);
            this.data.TabIndex = 11;
            this.data.MouseClick += new System.Windows.Forms.MouseEventHandler(this.data_MouseClick);
            this.data.TextChanged += new System.EventHandler(this.data_TextChanged);
            this.data.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.data_KeyPress);
            this.data.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.data_MouseDoubleClick);
            this.data.MouseEnter += new System.EventHandler(this.data_MouseEnter);
            this.data.MouseLeave += new System.EventHandler(this.data_MouseLeave);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(533, 47);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(87, 15);
            this.label7.TabIndex = 14;
            this.label7.Text = "FHt-index:";
            // 
            // hts
            // 
            this.hts.AutoSize = true;
            this.hts.Location = new System.Drawing.Point(644, 47);
            this.hts.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.hts.Name = "hts";
            this.hts.Size = new System.Drawing.Size(0, 15);
            this.hts.TabIndex = 15;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(24, 9);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(191, 15);
            this.label8.TabIndex = 16;
            this.label8.Text = "Paste your data here...";
            // 
            // clearBox
            // 
            this.clearBox.Location = new System.Drawing.Point(69, 356);
            this.clearBox.Margin = new System.Windows.Forms.Padding(4);
            this.clearBox.Name = "clearBox";
            this.clearBox.Size = new System.Drawing.Size(57, 29);
            this.clearBox.TabIndex = 17;
            this.clearBox.Text = "clear";
            this.clearBox.UseVisualStyleBackColor = true;
            this.clearBox.Click += new System.EventHandler(this.clearBox_Click);
            // 
            // CRGIndex
            // 
            this.CRGIndex.AutoSize = true;
            this.CRGIndex.Location = new System.Drawing.Point(742, 16);
            this.CRGIndex.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.CRGIndex.Name = "CRGIndex";
            this.CRGIndex.Size = new System.Drawing.Size(0, 15);
            this.CRGIndex.TabIndex = 13;
            this.CRGIndex.Visible = false;
            // 
            // Copy
            // 
            this.Copy.Enabled = false;
            this.Copy.Location = new System.Drawing.Point(453, 358);
            this.Copy.Name = "Copy";
            this.Copy.Size = new System.Drawing.Size(57, 29);
            this.Copy.TabIndex = 18;
            this.Copy.Text = "Copy";
            this.Copy.UseVisualStyleBackColor = true;
            this.Copy.Click += new System.EventHandler(this.Copy_Click);
            // 
            // ResultTemp
            // 
            this.ResultTemp.Location = new System.Drawing.Point(745, 28);
            this.ResultTemp.Name = "ResultTemp";
            this.ResultTemp.Size = new System.Drawing.Size(100, 25);
            this.ResultTemp.TabIndex = 19;
            this.ResultTemp.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(878, 389);
            this.Controls.Add(this.ResultTemp);
            this.Controls.Add(this.Copy);
            this.Controls.Add(this.clearBox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.hts);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.CRGIndex);
            this.Controls.Add(this.data);
            this.Controls.Add(this.htIndex);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.BreakResult);
            this.Controls.Add(this.TailPercentage);
            this.Controls.Add(this.HeadPercentage);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.InputFileAddress);
            this.Controls.Add(this.button1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "FHTCalculator";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox InputFileAddress;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox HeadPercentage;
        private System.Windows.Forms.Label TailPercentage;
        private System.Windows.Forms.TextBox BreakResult;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label htIndex;
        private System.Windows.Forms.TextBox data;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label hts;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button clearBox;
        private System.Windows.Forms.Label CRGIndex;
        private System.Windows.Forms.Button Copy;
        private System.Windows.Forms.TextBox ResultTemp;
    }
}

