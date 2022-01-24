
namespace InverseKinematicsDemo
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
            this.LerpTimer = new System.Windows.Forms.Timer(this.components);
            this.rotationSpeedBox = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.anglebox = new System.Windows.Forms.TextBox();
            this.Submit = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.rotationSpeedBox)).BeginInit();
            this.SuspendLayout();
            // 
            // LerpTimer
            // 
            this.LerpTimer.Enabled = true;
            this.LerpTimer.Interval = 50;
            this.LerpTimer.Tick += new System.EventHandler(this.LerpTimer_Tick);
            // 
            // rotationSpeedBox
            // 
            this.rotationSpeedBox.DecimalPlaces = 5;
            this.rotationSpeedBox.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.rotationSpeedBox.Location = new System.Drawing.Point(763, 9);
            this.rotationSpeedBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rotationSpeedBox.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.rotationSpeedBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.rotationSpeedBox.Name = "rotationSpeedBox";
            this.rotationSpeedBox.Size = new System.Drawing.Size(137, 27);
            this.rotationSpeedBox.TabIndex = 0;
            this.rotationSpeedBox.Value = new decimal(new int[] {
            3,
            0,
            0,
            65536});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(654, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Rotation Speed:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(806, 43);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(94, 29);
            this.button1.TabIndex = 2;
            this.button1.Text = "Add Point";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // anglebox
            // 
            this.anglebox.Location = new System.Drawing.Point(777, 78);
            this.anglebox.Name = "anglebox";
            this.anglebox.Size = new System.Drawing.Size(125, 27);
            this.anglebox.TabIndex = 3;
            // 
            // Submit
            // 
            this.Submit.Location = new System.Drawing.Point(777, 111);
            this.Submit.Name = "Submit";
            this.Submit.Size = new System.Drawing.Size(123, 29);
            this.Submit.TabIndex = 4;
            this.Submit.Text = "Submit";
            this.Submit.UseVisualStyleBackColor = true;
            this.Submit.Click += new System.EventHandler(this.Submit_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(914, 600);
            this.Controls.Add(this.Submit);
            this.Controls.Add(this.anglebox);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rotationSpeedBox);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.rotationSpeedBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer LerpTimer;
        private System.Windows.Forms.NumericUpDown rotationSpeedBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox anglebox;
        private System.Windows.Forms.Button Submit;
    }
}

