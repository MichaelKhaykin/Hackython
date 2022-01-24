using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace InverseKinematicsDemo
{
    public partial class Form1 : Form
    {
        //https://appliedgo.net/roboticarm/
        //https://robotacademy.net.au/lesson/inverse-kinematics-for-a-2-joint-robot-arm-using-geometry/

        PictureBox img;
        Bitmap canvas;
        Graphics gfx;

        double curA = 0;
        double curB = 0;
        double initialA = 0;
        double initialB = 0;
        double goalA = 0;
        double goalB = 0;

        double aPercent = 0;
        double bPercent = 0;

        Point msClickLocation;

        SerialPort serial = new SerialPort("COM6", 115200);

        double firstArmLength;
        double secondArmLength;


        List<(int angle1, int angle2)> m = new List<(int angle1, int angle2)>();

        float armLengthInInches = 5.5f;
        float arm2LengthInInches = 8.5f;
        public Form1()
        {
            InitializeComponent();

         //   serial.Open();
         //   serial.Write("0,0,4000\n");
            ;

            firstArmLength = armLengthInInches * inchesToPixels;
            secondArmLength = arm2LengthInInches * inchesToPixels;

            circlePos = new Point(this.Width / 2, this.Height / 2);

            img = new PictureBox()
            {
                Location = new Point(0, 0),
                Size = new Size(this.Width, this.Height)
            };

            this.FormClosed += Form1_FormClosed;    

            canvas = new Bitmap(img.Width, img.Height);
            gfx = Graphics.FromImage(canvas);

            img.MouseClick += Img_MouseClick;

            LerpTimer.Enabled = false;

            (goalA, goalB) = CalculateAngles(new Point((int)(firstArmLength + secondArmLength), 0));
            Text = $"A1:{(int)(goalA * 180 / Math.PI)}, A2:{(int)(goalB * 180 / Math.PI)}";

            initialA = curA = goalA;
            initialB = curB = goalB;

            LerpTimer.Enabled = true;

            Controls.Add(img);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.IO.File.WriteAllLines("SetPoints.txt", m.Select(x => x.ToString()).ToArray());
        }

        private void Img_MouseClick(object sender, MouseEventArgs e)
        {
            if (LerpTimer.Enabled == true) return;

            (goalA, goalB) = CalculateAngles(new Point(e.Location.X - circlePos.X, circlePos.Y - e.Location.Y));

            if (goalA == double.PositiveInfinity && goalB == double.PositiveInfinity) return;
            if (double.IsNaN(goalA) || double.IsNaN(goalB))
            {
                MessageBox.Show("Robot cannot reach that area");
                return;
            }

            if (serial.IsOpen)
            {
                serial.Write($"{(int)(goalA * 180 / Math.PI)},{(int)(goalB * 180 / Math.PI)}\n");
            }

            Text = $"A1:{(int)(goalA * 180 / Math.PI)}, A2:{(int)(goalB * 180 / Math.PI)}";

            LerpTimer.Enabled = true;
            aPercent = 0;
            bPercent = 0;

            msClickLocation = e.Location;
        }

        private (double a1, double a2) CalculateAngles(Point pos)
        {
            var x = pos.X;
            var y = pos.Y;
            var dist = Math.Sqrt(x * x + y * y);

            if (dist > firstArmLength + secondArmLength)
            {
                MessageBox.Show("Robot cannot reach that area");
                return (double.PositiveInfinity, double.PositiveInfinity);
            }

            double q2 = Math.PI - LawOfCosines(dist, firstArmLength, secondArmLength);
            double q1 = Math.Atan2(y, x) - Math.Atan2(secondArmLength * Math.Sin(q2), firstArmLength + secondArmLength * Math.Cos(q2));

            return (q1, q2);
        }

        private double LawOfCosines(double a, double b, double c) //Solves for angle opposite side a
        {
            return Math.Acos((b * b + c * c - a * a) / (2 * b * c));
        }

        Point circlePos;
        double inchesToPixels = 5;
        private void Logic()
        {
            gfx.Clear(this.BackColor);

            double firstArmAngle = curA;
            Point firstArmEndPoint = new Point((int)(circlePos.X + firstArmLength * Math.Cos(firstArmAngle)), (int)(circlePos.Y - firstArmLength * Math.Sin(firstArmAngle)));

            double secondArmAngle = curB;
            Point secondArmEndPoint = new Point((int)(firstArmEndPoint.X + secondArmLength * Math.Cos(firstArmAngle + secondArmAngle)), (int)(firstArmEndPoint.Y - secondArmLength * Math.Sin(firstArmAngle + secondArmAngle)));


            int circleRadius = 15; //inches
            gfx.FillEllipse(Brushes.Black, new Rectangle(circlePos.X - circleRadius, circlePos.Y - circleRadius, circleRadius * 2, circleRadius * 2));
            gfx.DrawLine(new Pen(Color.Yellow, 5), circlePos, firstArmEndPoint);
            gfx.DrawLine(new Pen(Color.Pink, 5), firstArmEndPoint, secondArmEndPoint);

            int largeRad = (int)(firstArmLength + secondArmLength);
            gfx.DrawEllipse(new Pen(Color.Red, 2), new Rectangle(circlePos.X - largeRad, circlePos.Y - largeRad, largeRad * 2, largeRad * 2));
            
            int mouseCircleRadius = 5;
            gfx.FillEllipse(Brushes.Blue, new Rectangle(msClickLocation.X - mouseCircleRadius, msClickLocation.Y - mouseCircleRadius, mouseCircleRadius * 2, mouseCircleRadius * 2));

            img.Image = canvas;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Logic();
        }

        private void Value_Changed(object sender, EventArgs e)
        {
            Logic();
        }

        private void LerpTimer_Tick(object sender, EventArgs e)
        {
            bool wentIn = false;

            if (aPercent < 1)
            {
                curA = Lerp(initialA, goalA, aPercent);
                aPercent += (double)rotationSpeedBox.Value;
                wentIn = true;
            }
            if (bPercent < 1)
            {
                curB = Lerp(initialB, goalB, bPercent);
                bPercent += (double)rotationSpeedBox.Value;
                wentIn = true;
            }

            if (!wentIn)
            {
                initialA = goalA;
                initialB = goalB;

                curA = goalA;
                curB = goalB;
                Logic();

                LerpTimer.Enabled = false;
                return;
            }
            Logic();
        }

        private double Lerp(double start, double end, double percent)
        {
            return (start + (end - start) * percent);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            m.Add(((int)(goalA * 180 / Math.PI), (int)(goalB * 180 / Math.PI)));
        }

        private void Submit_Click(object sender, EventArgs e)
        {

        }
    }
}
