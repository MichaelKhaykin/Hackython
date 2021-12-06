
namespace ShapesRecognitionTestNetCore
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.minSVal = new System.Windows.Forms.NumericUpDown();
            this.maxSVal = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.minVVal = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.maxVVal = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.maxCanny = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.minCanny = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.minAreaBox = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.debugLabel = new System.Windows.Forms.Label();
            this.candice = new System.Windows.Forms.PictureBox();
            this.displayTypeBox = new System.Windows.Forms.ComboBox();
            this.SerialTimer = new System.Windows.Forms.Timer(this.components);
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.minSVal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxSVal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minVVal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxVVal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxCanny)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minCanny)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minAreaBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.candice)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(794, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Min Saturation:";
            // 
            // minSVal
            // 
            this.minSVal.Location = new System.Drawing.Point(909, 10);
            this.minSVal.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.minSVal.Name = "minSVal";
            this.minSVal.Size = new System.Drawing.Size(63, 27);
            this.minSVal.TabIndex = 4;
            // 
            // maxSVal
            // 
            this.maxSVal.Location = new System.Drawing.Point(909, 45);
            this.maxSVal.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.maxSVal.Name = "maxSVal";
            this.maxSVal.Size = new System.Drawing.Size(63, 27);
            this.maxSVal.TabIndex = 6;
            this.maxSVal.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(794, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "Max Saturation:";
            // 
            // minVVal
            // 
            this.minVVal.Location = new System.Drawing.Point(909, 80);
            this.minVVal.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.minVVal.Name = "minVVal";
            this.minVVal.Size = new System.Drawing.Size(63, 27);
            this.minVVal.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(794, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 20);
            this.label3.TabIndex = 7;
            this.label3.Text = "Min Value:";
            // 
            // maxVVal
            // 
            this.maxVVal.Location = new System.Drawing.Point(909, 116);
            this.maxVVal.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.maxVVal.Name = "maxVVal";
            this.maxVVal.Size = new System.Drawing.Size(63, 27);
            this.maxVVal.TabIndex = 10;
            this.maxVVal.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(794, 118);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 20);
            this.label4.TabIndex = 9;
            this.label4.Text = "Max Value:";
            // 
            // maxCanny
            // 
            this.maxCanny.Location = new System.Drawing.Point(912, 191);
            this.maxCanny.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.maxCanny.Name = "maxCanny";
            this.maxCanny.Size = new System.Drawing.Size(63, 27);
            this.maxCanny.TabIndex = 14;
            this.maxCanny.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(797, 193);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 20);
            this.label5.TabIndex = 13;
            this.label5.Text = "Max Canny:";
            // 
            // minCanny
            // 
            this.minCanny.Location = new System.Drawing.Point(912, 155);
            this.minCanny.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.minCanny.Name = "minCanny";
            this.minCanny.Size = new System.Drawing.Size(63, 27);
            this.minCanny.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(797, 157);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 20);
            this.label6.TabIndex = 11;
            this.label6.Text = "Min Canny:";
            // 
            // minAreaBox
            // 
            this.minAreaBox.Location = new System.Drawing.Point(909, 224);
            this.minAreaBox.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.minAreaBox.Name = "minAreaBox";
            this.minAreaBox.Size = new System.Drawing.Size(63, 27);
            this.minAreaBox.TabIndex = 16;
            this.minAreaBox.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(794, 226);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 20);
            this.label7.TabIndex = 15;
            this.label7.Text = "Min Area:";
            // 
            // debugLabel
            // 
            this.debugLabel.AutoSize = true;
            this.debugLabel.Location = new System.Drawing.Point(806, 362);
            this.debugLabel.Name = "debugLabel";
            this.debugLabel.Size = new System.Drawing.Size(40, 20);
            this.debugLabel.TabIndex = 17;
            this.debugLabel.Text = "HSV:";
            // 
            // candice
            // 
            this.candice.Location = new System.Drawing.Point(12, 12);
            this.candice.Name = "candice";
            this.candice.Size = new System.Drawing.Size(697, 426);
            this.candice.TabIndex = 18;
            this.candice.TabStop = false;
            // 
            // displayTypeBox
            // 
            this.displayTypeBox.FormattingEnabled = true;
            this.displayTypeBox.Location = new System.Drawing.Point(715, 269);
            this.displayTypeBox.Name = "displayTypeBox";
            this.displayTypeBox.Size = new System.Drawing.Size(151, 28);
            this.displayTypeBox.TabIndex = 19;
            this.displayTypeBox.SelectedIndexChanged += new System.EventHandler(this.displayTypeBox_SelectedIndexChanged);
            // 
            // SerialTimer
            // 
            this.SerialTimer.Enabled = true;
            this.SerialTimer.Interval = 50;
            this.SerialTimer.Tick += new System.EventHandler(this.SerialTimer_Tick);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(872, 269);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(172, 120);
            this.richTextBox1.TabIndex = 20;
            this.richTextBox1.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1056, 450);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.displayTypeBox);
            this.Controls.Add(this.candice);
            this.Controls.Add(this.debugLabel);
            this.Controls.Add(this.minAreaBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.maxCanny);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.minCanny);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.maxVVal);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.minVVal);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.maxSVal);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.minSVal);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.minSVal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxSVal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minVVal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxVVal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxCanny)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minCanny)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minAreaBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.candice)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown minSVal;
        private System.Windows.Forms.NumericUpDown maxSVal;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown minVVal;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown maxVVal;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown maxCanny;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown minCanny;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown minAreaBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label debugLabel;
        private System.Windows.Forms.PictureBox candice;
        private System.Windows.Forms.ComboBox displayTypeBox;
        private System.Windows.Forms.Timer SerialTimer;
        private System.Windows.Forms.RichTextBox richTextBox1;
    }
}

