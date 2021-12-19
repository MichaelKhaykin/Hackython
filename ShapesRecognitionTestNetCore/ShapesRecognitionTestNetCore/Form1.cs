using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO.Ports;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace ShapesRecognitionTestNetCore
{
    public enum DisplayTypes
    {
        HSVInRange,
        GrayScaled,
        Blurred,
        Dilated,
        Canny,
        Edges
    }

    public partial class Form1 : Form
    {
        VideoCapture capture = new VideoCapture(1);

        SerialPort cerialPort = new SerialPort("COM6", 115200);
        Mat inputHSV;

        public Form1()
        {
            InitializeComponent();

            //  cerialPort.Open();

            //cerialPort.DtrEnable = true;

            candice.MouseClick += candice_MouseClick;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;

            minHueVal.Value = 60;
            maxHue.Value = 100;

            minSVal.Value = 50;
            maxSVal.Value = 255;

            minVVal.Value = 50;
            maxVVal.Value = 255;

            minCanny.Value = 180;
            maxCanny.Value = 120;

            var arr = Enum.GetValues(typeof(DisplayTypes));
            foreach (var item in arr)
            {
                displayTypeBox.Items.Add(item);
            }

            displayTypeBox.SelectedItem = DisplayTypes.Edges;

            Application.Idle += new EventHandler(Capture_ImageGrabbed);

        }

        private void Capture_ImageGrabbed(object sender, EventArgs e)
        {
            var capturedImg = capture.QueryFrame();

            CvInvoke.GaussianBlur(capturedImg, capturedImg, new Size(3, 3), 1);

            Mat capturedImgHSV = new Mat();
            CvInvoke.CvtColor(capturedImg, capturedImgHSV, ColorConversion.Bgr2Hsv);
            inputHSV = capturedImgHSV.Clone();

            int mins = (int)minSVal.Value;
            int maxs = (int)maxSVal.Value;

            int minv = (int)minVVal.Value;
            int maxv = (int)maxVVal.Value;

            Mat rangedHSVMask1 = new Mat();
            CvInvoke.InRange(capturedImgHSV, (ScalarArray)new MCvScalar((int)minHueVal.Value, mins, minv), (ScalarArray)new MCvScalar((int)maxHue.Value, maxs, maxv), rangedHSVMask1);

            Mat rangedHSVMask2 = new Mat();
            CvInvoke.InRange(capturedImgHSV, (ScalarArray)new MCvScalar(175, mins, minv), (ScalarArray)new MCvScalar(180, maxs, maxv), rangedHSVMask2);

            Mat rangedHSVMask = new Mat();
            CvInvoke.BitwiseOr(rangedHSVMask1, rangedHSVMask2, rangedHSVMask);


            if (type == DisplayTypes.HSVInRange)
            {
                candice.Image = rangedHSVMask.ToBitmap();
                return;
            }

            Image<Gray, byte> cont1Gray = rangedHSVMask.ToImage<Gray, byte>();

            if (type == DisplayTypes.GrayScaled)
            {
                candice.Image = cont1Gray.ToBitmap();
                return;
            }

            //Skipping this in the python port, if shit doesn't work maybe this is the reason!
            Mat blurred = new Mat();
            CvInvoke.GaussianBlur(cont1Gray, blurred, new Size(15, 15), 0, 0, BorderType.Default);

            if (type == DisplayTypes.Blurred)
            {
                candice.Image = blurred.ToBitmap();
                return;
            }

            Mat dilated = new Mat();
            Mat element = CvInvoke.GetStructuringElement(ElementShape.Ellipse, new Size(3, 3), new Point(-1, -1));

            CvInvoke.Dilate(blurred
                     , dilated
                     , element
                     , new Point(-1, -1)
                     , 6
                     , BorderType.Default
                     , new MCvScalar(0, 0, 0));

            if(type == DisplayTypes.Dilated)
            {
                candice.Image = dilated.ToBitmap();
                return;
            }

            Mat edges = new Mat();

            int minc = (int)minCanny.Value;
            int maxc = (int)maxCanny.Value;

            CvInvoke.Canny(cont1Gray, edges, minc, maxc);

            if (type == DisplayTypes.Canny)
            {
                candice.Image = edges.ToBitmap();
                return;
            }

            VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();
            CvInvoke.FindContours(edges, contours, new Mat(), RetrType.List, ChainApproxMethod.ChainApproxSimple);

            int minArea = (int)minAreaBox.Value;

            bool closed = true;
            bool wentIn = false;
            for (int i = 0; i < contours.Size; i++)
            {
                if (CvInvoke.ContourArea(contours[i]) < minArea) continue;
                wentIn = true;
                //if (contours[i].Size < 10) continue;

                double arcLength = CvInvoke.ArcLength(contours[i], closed);
                VectorOfPoint approx = new VectorOfPoint();
                CvInvoke.ApproxPolyDP(contours[i], approx, 0.01 * arcLength, closed);

                var moments = CvInvoke.Moments(contours[i]);
                int x = (int)(moments.M10 / moments.M00);
                int y = (int)(moments.M01 / moments.M00);

                string text = GetText(approx.Size);
                if (text == "") continue;

                string fullText = $"{text} ({x}, {y})";
                //TextRenderer.MeasureText(fullText);
                CvInvoke.PutText(capturedImg, fullText, new Point(x, y), FontFace.HersheyComplex, 0.5, new MCvScalar(0, 255, 0));

                if (x > capturedImg.Width / 2)
                {
                    Text = "To the right";
                    Write("r\0");
                }
                else if (x < capturedImg.Width / 2)
                {
                    Text = "To the left";
                    Write("l\0");
                }
                else
                {
                    Text = "Go lorenzo!!";
                    Write("n\0");
                }

                CvInvoke.DrawContours(capturedImg, contours, i, new MCvScalar(255, 0, 0), 3);
            }

            if (!wentIn)
            {
                Text = "";
                Write("x\0");
            }

            candice.Image = capturedImg.ToBitmap();

        }

        void Write(string message)
        {
            if (cerialPort.IsOpen == false) return;
            cerialPort.Write(message);
        }


        Dictionary<int, string> map = new Dictionary<int, string>()
        {
            [3] = "Triangle",
            [4] = "Square",
            [15] = "Circle"
        };

        private string GetText(int points)
        {
            if (map.ContainsKey(points)) return map[points];
            if (points >= 15) return map[15];
            return "";
        }

        private void candice_MouseClick(object sender, MouseEventArgs e)
        {
            var pix = inputHSV.ToImage<Hsv, byte>()[e.Y, e.X];
            //var pix = ((Bitmap)candice.Image).GetPixel(e.X, e.Y);

            debugLabel.Text = pix.Hue + ", " + pix.Satuation + ", " + pix.Value;
        }

        DisplayTypes type = DisplayTypes.Edges;
        private void displayTypeBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            type = (DisplayTypes)(displayTypeBox.SelectedItem);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                cerialPort.Write($"100,200");
            }
        }

        private void SerialTimer_Tick(object sender, EventArgs e)
        {
            if (cerialPort.IsOpen == false) return;

            richTextBox1.Clear();

            string msg = "";
            while (cerialPort.BytesToRead > 0)
            {
                char cur = (char)cerialPort.ReadChar();
                if (cur == '\0')
                {
                    richTextBox1.AppendText(msg + "\n");
                    msg = "";
                }
                else
                {
                    msg += cur;
                }
            }

        }
    }
}
