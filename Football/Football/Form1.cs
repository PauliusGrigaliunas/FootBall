using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Emgu;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Threading;
using Emgu.CV.UI;
using Emgu.CV.CvEnum;
using Emgu.CV.Cuda;

namespace Football
{
    public partial class Form1 : Form
    {
        VideoCapture capture;
        Image<Bgr, byte> imgInput;
        Image<Gray, byte> imgGray;
        Image<Ycc, byte> imgYcc;
        Image<Gray, byte> imgSmoothed;
        Image<Bgr, byte> imgLines;
        Gray grayCircle = new Gray(100);
        Gray cannyThreshold = new Gray(160);
        Image<Bgr, byte> imgCircles;
        double lAccumRes = 2.0;
        double minDistanceBtwCircles;
        int minRadius = 10;
        int maxRadius = 400;
        CircleF[] circles;
        Gray grayThresLinkings = new Gray(60);
        Mat mat;
        int lowBlue;
        int highBlue;
        int lowGreen;
        int highGreen;
        int lowRed;
        int highRed;

        public Form1()
        {
            InitializeComponent();
        }

        private void takeAPicture()
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    //searching for cirxle shapes
                    imgInput = new Image<Bgr, byte>(ofd.FileName);
                    pictureBox1.Image = imgInput.Bitmap;
                    imgSmoothed = imgInput.InRange(new Bgr(0, 0, 140), new Bgr(80, 255, 255));
                    imgSmoothed = imgSmoothed.PyrDown().PyrUp();
                    imgSmoothed._SmoothGaussian(3);
                    imgSmoothed = imgSmoothed.Convert<Gray, byte>();
                    pictureBox3.Image = imgSmoothed.Bitmap;

                    imgCircles = imgInput.CopyBlank();
                    imgLines = imgInput.CopyBlank();

                    minDistanceBtwCircles = imgSmoothed.Height / 4;
                    circles = imgSmoothed.HoughCircles(cannyThreshold, grayCircle, lAccumRes, minDistanceBtwCircles, minRadius, maxRadius)[0];

                    foreach (CircleF circle in circles)
                    {
                        imgCircles.Draw(circle, new Bgr(Color.Red), 2);
                    }
                    pictureBox2.Image = imgCircles.Bitmap;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to close?", "System message", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Close();
            }

        }
        //Picture
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            takeAPicture();
        }
        //layers
        private void cannyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (imgInput == null)
            {
                return;
            }
            Image<Gray, byte> imgCanny = new Image<Gray, byte>(imgInput.Width, imgInput.Height, new Gray(0));
            imgCanny = imgInput.Canny(50, 20);
            pictureBox2.Image = imgCanny.Bitmap;
        }

        private void sobelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (imgInput == null)
            {
                return;
            }
            Image<Gray, byte> imgGray = imgInput.Convert<Gray, byte>();
            Image<Gray, float> imgSobel = new Image<Gray, float>(imgInput.Width, imgInput.Height, new Gray(0));
            imgSobel = imgGray.Sobel(1, 1, 3);
            pictureBox2.Image = imgSobel.Bitmap;
        }

        private void laplasianToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (imgInput == null)
            {
                return;
            }
            Image<Gray, byte> imgGray = imgInput.Convert<Gray, byte>();
            Image<Gray, float> imgLaplasian = new Image<Gray, float>(imgInput.Width, imgInput.Height, new Gray(0));
            imgLaplasian = imgGray.Laplace(3);
            pictureBox2.Image = imgLaplasian.Bitmap;
        }

        private void videoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        //Camera
        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (capture == null)
            {
                capture = new Emgu.CV.VideoCapture(0);
            }
            capture.ImageGrabbed += Capture_ImageGrabbed;
            capture.Start();
        }

        private void Capture_ImageGrabbed(object sender, EventArgs e)
        {
            try
            {
                Mat mat = new Mat();
                capture.Retrieve(mat);
                pictureBox1.Image = mat.ToImage<Bgr, byte>().Bitmap;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (capture != null)
            {
                capture.Stop();
                capture = null;
            }
        }

        private void pauseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (capture != null)
            {
                capture.Pause();
            }
        }
        // Video
        private void startToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (capture == null)
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "Video Files |*.mp4";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    capture = new Emgu.CV.VideoCapture(ofd.FileName);
                }
                capture.ImageGrabbed += Capture_ImageGrabbed1;
                capture.Start();
            }
        }

        private void Capture_ImageGrabbed1(object sender, EventArgs e)
        {
            try
            {
                mat = new Mat();
                capture.Retrieve(mat);
                pictureBox1.Image = mat.ToImage<Bgr, byte>().Bitmap;
                Image<Gray, Byte> imgRange = mat.ToImage<Bgr, byte>().InRange(new Bgr(0, 0, 140), new Bgr(80, 255, 255));
                pictureBox2.Image = imgRange.Bitmap;
                // Thread.Sleep((int)capture.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.Fps));
                Thread.Sleep(1);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }

        }

        private void stopToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (capture != null)
            {
                capture.Stop();
                capture = null;
            }
        }

        private void pauseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (capture != null)
            {
                capture.Pause();
            }
        }

        private void grayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (capture != null || imgInput != null)
            {
                imgGray = imgInput.Convert<Gray, byte>();
                pictureBox2.Image = imgGray.Bitmap;
            }

        }

        private void iccToolStripMenuItem_Click(object sender, EventArgs e)
        {
            imgYcc = imgInput.Convert<Ycc, byte>();
            pictureBox2.Image = imgYcc.Bitmap;
        }

        //coordinates
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            Bitmap bitmap = new Bitmap(pictureBox1.Image);
            Color pixelColor = bitmap.GetPixel(e.X, e.Y);
            MessageBox.Show(pixelColor.ToString());
        }

        private string TrackBarSetting(TrackBar trackBar)
        {
            trackBar.Maximum = 255;         // max value
            trackBar.Minimum = 0;           // min value
            trackBar.TickFrequency = 10;    // distance between tick-mark
            trackBar.LargeChange = 5;       // when clicked on a side of a slider move by X
            trackBar.SmallChange = 1;       // move using keyboard arrows

            return trackBar.Value.ToString();

        }
        // colors:
        //low red
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label1.Text = TrackBarSetting(trackBar1);
            redToolStripMenuItem_Click(sender, e);
        }

        //low green
        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            label2.Text = TrackBarSetting(trackBar2);
            redToolStripMenuItem_Click(sender, e);
        }

        //low blue
        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            label3.Text = TrackBarSetting(trackBar3);
            redToolStripMenuItem_Click(sender, e);
        }

        //high red
        private void trackBar4_Scroll(object sender, EventArgs e)
        {
            label4.Text = TrackBarSetting(trackBar4);
            redToolStripMenuItem_Click(sender, e);
        }

        //high green
        private void trackBar5_Scroll(object sender, EventArgs e)
        {
            label5.Text = TrackBarSetting(trackBar5);
            redToolStripMenuItem_Click(sender, e);
        }

        //high red
        private void trackBar6_Scroll(object sender, EventArgs e)
        {
            label6.Text = TrackBarSetting(trackBar6);
            redToolStripMenuItem_Click(sender, e);
        }

        //ColorFilter
        private void redToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lowBlue = Convert.ToInt32(label3.Text);
            highBlue = Convert.ToInt32(label6.Text);
            lowGreen = Convert.ToInt32(label2.Text);
            highGreen = Convert.ToInt32(label5.Text);
            lowRed = Convert.ToInt32(label1.Text);
            highRed = Convert.ToInt32(label4.Text);


            if (imgInput != null)
            {
                Image<Gray, Byte> imgRange = imgInput.InRange(new Bgr(lowBlue, lowGreen, lowRed), new Bgr(highBlue, highGreen, highRed));

                imgRange.SmoothGaussian(9);

                pictureBox3.Image = imgRange.Bitmap;
            }
            else if (capture != null)
            {
                capture.ImageGrabbed += Capture_ImageGrabbed2;


            }

            else return;
        }

        private void Capture_ImageGrabbed2(object sender, EventArgs e)
        {
            try
            {
                //Mat mat2 = new Mat();
                //capture.Retrieve(mat2);
                Image<Gray, Byte> imgRange = mat.ToImage<Bgr, byte>().InRange(new Bgr(lowBlue, lowGreen, lowRed), new Bgr(highBlue, highGreen, highRed));

                imgRange.SmoothGaussian(9);

                pictureBox3.Image = imgRange.Bitmap;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }

        }
    }
}