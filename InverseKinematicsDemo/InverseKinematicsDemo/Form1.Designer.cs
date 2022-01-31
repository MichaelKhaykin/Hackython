
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
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
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
            this.rotationSpeedBox.Location = new System.Drawing.Point(668, 7);
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
            this.rotationSpeedBox.Size = new System.Drawing.Size(120, 23);
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
            this.label1.Location = new System.Drawing.Point(572, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Rotation Speed:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(705, 32);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(82, 22);
            this.button1.TabIndex = 2;
            this.button1.Text = "Add Point";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // anglebox
            // 
            this.anglebox.Location = new System.Drawing.Point(680, 168);
            this.anglebox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.anglebox.Name = "anglebox";
            this.anglebox.Size = new System.Drawing.Size(110, 23);
            this.anglebox.TabIndex = 3;
            // 
            // Submit
            // 
            this.Submit.Location = new System.Drawing.Point(682, 84);
            this.Submit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Submit.Name = "Submit";
            this.Submit.Size = new System.Drawing.Size(108, 22);
            this.Submit.TabIndex = 4;
            this.Submit.Text = "Submit";
            this.Submit.UseVisualStyleBackColor = true;
            this.Submit.Click += new System.EventHandler(this.Submit_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(680, 111);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "Mn";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(680, 140);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 6;
            this.button3.Text = "Mf";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(695, 196);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 46);
            this.button4.TabIndex = 7;
            this.button4.Text = "Move to angle";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.Submit);
            this.Controls.Add(this.anglebox);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rotationSpeedBox);
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
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
    }
}

