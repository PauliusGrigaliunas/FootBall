﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

using Emgu;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Threading;
using Emgu.CV.UI;
using Emgu.CV.CvEnum;
using Emgu.CV.Cuda;
using System.Data.SqlClient;
namespace Football
{

    public partial class Form1 : Form
    {
        bool isBTeamScored = false;
        bool isATeamScored = false;
        int teamAScores;
        int teamBScores;
        private Stopwatch stopwatch = new Stopwatch();
        int _xBallPosition { get; set; }
        int _timeElapsed = 0;
        VideoCapture _capture { get; set; }
        Image<Bgr, byte> imgInput = null;
        Image<Bgr, byte> imgOriginal { get; set; }
        Image<Gray, byte> imgFiltered { get; set; }

        Teams team = new Teams();
        SqlCommand cmd;
        SqlDataAdapter sa;
        DataTable dt = new DataTable();
 
        String name1;
        String name2;
        System.Windows.Forms.Timer _timer;

        public Form1()
        {
            InitializeComponent();
        }

        public int TeamAScores { get => teamAScores; set => teamAScores = value; }
        public int TeamBScores { get => teamBScores; set => teamBScores = value; }
        
        public void setName1(String name)
        {
            this.name1 = name;
        }
        public void setName2(String name)
        {
            this.name2 = name;
        }

        private void Video()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Video Files |*.mp4";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                _capture = new Emgu.CV.VideoCapture(ofd.FileName);
                _timer = new System.Windows.Forms.Timer();
                _timer.Interval = 1000 / 30;
                _timer.Tick += new EventHandler(TimeTick);
                _timer.Start();
                
            }

        }


        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (_timer != null)
            {
                _timer.Tick += new EventHandler(TimeTick);
                _timer.Start();

            }
            else Video();

        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            if (_timer != null)
            {
                _timer.Tick -= new EventHandler(TimeTick);
                _timer.Stop();
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (_timer != null)
            {
                _timer.Tick -= new EventHandler(TimeTick);
                _timer.Stop();
                _timer = null;
            }
        }


        private void TimeTick(object sender, EventArgs e)
        {
            CheckForScore();

            Mat mat = _capture.QueryFrame();       //getting frames            
            if (mat == null) return;

            imgOriginal = mat.ToImage<Bgr, byte>().Resize(pictureBox1.Width, pictureBox1.Height, Inter.Linear); ;
            pictureBox1.Image = imgOriginal.Bitmap;
            Image<Bgr, byte> imgCircles = imgOriginal.CopyBlank();     //copy parameters of original frame image

            var filter = new ImgFilter(imgOriginal);
            imgFiltered = filter.GetFilteredImage();

            BallPosition(imgCircles);

            pictureBox2.Image = imgCircles.Bitmap;
        }

        private void BallPosition(Image<Bgr, byte> imgCircles) {

            foreach (CircleF circle in GetCircles(imgFiltered))          //searching circles
            {
                if (textXYradius.Text != "") textXYradius.AppendText(Environment.NewLine);

                textXYradius.AppendText("ball position = x" + circle.Center.X.ToString().PadLeft(4) + ", y" + circle.Center.Y.ToString().PadLeft(4) + ", radius =" +
                circle.Radius.ToString("###,000").PadLeft(7));
                textXYradius.ScrollToCaret();

                //write coordinates to textbox

                _xBallPosition = (int)circle.Center.X;                          // get x coordinate(center of a ball)
                StartStopwatch(_xBallPosition);                                     //start stopwatch to check or it is scored or not
                imgCircles.Draw(circle, new Bgr(Color.Red), 3);                        //draw circles on smoothed image
            }
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_timer != null)
            {
                _timer.Tick -= new EventHandler(TimeTick);
                _timer.Stop();
            }
            Application.Exit();
        }
        //check for scoring and write in GUI
        private void CheckForScore()
        {
            int temp;
            //stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;
            _timeElapsed = ts.Seconds;
            if (_timeElapsed >= 3 && isBTeamScored == true)
            {
                temp = int.Parse(bTeamLabel.Text);
                temp = temp + 1;
                bTeamLabel.Text = temp.ToString();
                stopwatch.Reset();
                isBTeamScored = false;
            }       
            else if (_timeElapsed >= 3 && isATeamScored == true)
            {
                temp = int.Parse(aTeamLabel.Text);
                temp = temp + 1;
                aTeamLabel.Text = temp.ToString();
                stopwatch.Reset();
                isATeamScored = false;
            }
            
        }
        //start stopwatch
        private void StartStopwatch(int x)
        {
            if (x > 440)
            {
                isATeamScored = false;
                isBTeamScored = true;
                stopwatch.Reset();
                stopwatch.Start();
            }
            else if (x < 45)
            {
                isBTeamScored = false;
                isATeamScored = true;
                stopwatch.Reset();
                stopwatch.Start();
            }
            else
            {
                isBTeamScored = false;
                isATeamScored = false;
                stopwatch.Reset();
            }

        }
        //get a picture from local area
        private void takeAPicture(Image<Bgr, byte> imgInput)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    imgInput = new Image<Bgr, byte>(ofd.FileName);
                    pictureBox1.Image = imgInput.Bitmap;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_capture != null)
            {
                _capture = null;
            }
            if (MessageBox.Show("Are you sure you want to close?", "System message", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Close();
            }

        }
        //Picture
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            takeAPicture(imgInput);
        }
        //layers

        private void videoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        //Camera
        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_capture == null)
            {
                _capture = new Emgu.CV.VideoCapture(0);
            }
            _capture.ImageGrabbed += Capture_ImageGrabbed;
            _capture.Start();
        }

        private void Capture_ImageGrabbed(object sender, EventArgs e)
        {
            try
            {
                Mat mat = new Mat();
                _capture.Retrieve(mat);
                pictureBox1.Image = mat.ToImage<Bgr, byte>().Bitmap;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        // end camera
        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_capture != null)
            {
                _capture.Stop();
                _capture = null;
            }
        }

        private void pauseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_capture != null)
            {
                _capture.Pause();
            }
        }
        // Video
        private void startToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (_capture == null)
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "Video Files |*.mp4";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    _capture = new Emgu.CV.VideoCapture(ofd.FileName);
                }
                _capture.ImageGrabbed += Capture_ImageGrabbed1;
                _capture.Start();
            }
            MessageBox.Show("check");
        }

        private void Capture_ImageGrabbed1(object sender, EventArgs e)
        {

        }
        private CircleF[] GetCircles(Image<Gray, byte> imgGray)
        {
            Gray grayCircle = new Gray(12);
            Gray cannyThreshold = new Gray(26);
            double lAccumResolution = 2.0;
            double minDistanceBtwCircles = 1.0;
            int minRadius = 0;
            int maxRadius = 10;
            return imgGray.HoughCircles(grayCircle, cannyThreshold, lAccumResolution, minDistanceBtwCircles, minRadius, maxRadius)[0];
        }

        private void stopToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (_capture != null)
            {
                _capture.Stop();
            }
        }

        private void pauseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (_capture != null)
            {
                _capture.Pause();
            }
        }
        //coordinates
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)                          //checking coordinates of the video
        {
            MessageBox.Show(e.X.ToString() + e.Y.ToString());
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
            int lowBlue = Convert.ToInt32(label3.Text);
            int highBlue = Convert.ToInt32(label6.Text);
            int lowGreen = Convert.ToInt32(label2.Text);
            int highGreen = Convert.ToInt32(label5.Text);
            int lowRed = Convert.ToInt32(label1.Text);
            int highRed = Convert.ToInt32(label4.Text);


            if (imgInput == null) return;
            //Image<Gray, Byte> imgRange = new Image<Bgr, byte>(imgInput.Width, imgInput.Height, new Bgr(0,0,0)); 

            //Image<Gray, Byte> imgRange = imgInput.InRange(new Bgr(0, 0, 187), new Bgr(100, 255, 255));
            Image<Gray, Byte> imgRange = imgInput.InRange(new Bgr(lowBlue, lowGreen, lowRed), new Bgr(highBlue, highGreen, highRed));

            imgRange.SmoothGaussian(9);

            pictureBox2.Image = imgRange.Bitmap;

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            aTeamLabel.Text = "0";
            bTeamLabel.Text = "0";
        }

        private void scoreLabel_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.TeamBScores = int.Parse(bTeamLabel.Text);
            this.TeamAScores = int.Parse(aTeamLabel.Text);
            
            int victA=0;
            int goalA=0;
            int victB=0;
            int goalB=0;
            
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Emilija.DELL-EMILIJOS\Documents\GitHub\FootBall\Football\Football\database121.mdf;Integrated Security=True");
            con.Open();

            cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM teamTable";
            cmd.ExecuteNonQuery();
            sa = new SqlDataAdapter(cmd);
            sa.Fill(dt);
            //koia info lentelej
           
            victA = team.getVictories(dt, name1); 
            goalA = team.getGoals(dt, name1);
            victB = team.getVictories(dt, name2);
            goalB = team.getGoals(dt, name2);

            goalA = goalA + TeamAScores;
            goalB = goalB + TeamBScores;
            if(teamAScores>TeamBScores)
            {
                victA = victA + 1;
            }
            else if(teamAScores < TeamBScores)
            {
                victB = victB + 1;
            }
            con.Close();

            team.insertToTable(name1, victA, goalA);
            team.insertToTable(name2, victB, goalB);
         
            MessageBox.Show("Saved");

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
}