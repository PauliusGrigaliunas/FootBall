using System;
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
using static Football.ColourPalet;
using System.Timers;

namespace Football
{
    delegate bool Source<in T>();

    public partial class VideoScreen : Form
    {
        Task sound;

        //objects
        Picture _picture = new Picture();
        Ball _ball = new Ball();
        Switch switcher = new Switch();
        ChooseColour chooseColour = new ChooseColour();
        ScoreEditor SE;
        CustomColorViewer CCC;
        ISource _video;

        private Mat mat;
        Commentator comment = new Commentator();
        System.Timers.Timer aTimer = new System.Timers.Timer();
        
      
        public bool isTournament = false;
        
        public int _TeamAScores { get; set; }
        public int _TeamBScores { get; set; }
        public static bool isATeamScored = false;
        public static bool isBTeamScored = false;

        public bool isRinged = false;

        //variables
        private int _i = 0;
        public string ATeam, BTeam;
        //
        public List<int> _xCoordList = new List<int>();

        public VideoScreen(String teamA, String teamB)
        {
            InitializeComponent();

            Source._home = this; // neištrint
            _video = new Video(); // tik dėl stop pause mygtukų
            comboBox1.DataSource = Enum.GetValues(typeof(Switch.Sources)); // inisialijuojam source pagal pasirinkimą!
            comboBox2.DataSource = Enum.GetValues(typeof(ChooseColour.Choices));// pnš į praeitą.

            ButtonDisabler();

            TeamALabel.Text = teamA;
            TeamBLabel.Text = teamB;

            ATeam = TeamALabel.Text;
            BTeam = TeamBLabel.Text;
            SaveTeams();

            


        }
        private void OnTimedEvent(object source, EventArgs e)
        {
            SaveScore();
        }
        private void SaveTeams()
        {
            DateTime date = DateTime.Now;

            Teams team = new Teams();
            Predicate<String> compare = x => team.NameCheckIfExsist(x) == true;
            if (!compare(ATeam))
            {
                team.AddTeamToTable(ATeam);
            }
            if (!compare(BTeam))
            {
                team.AddTeamToTable(BTeam);
            }
            team.AddGameToTable(ATeam, BTeam, date);
     
        }
        private void ButtonDisabler()
        {
            btnStartLast.Enabled = false;
            btnStopp.Enabled = false;
            btnReset.Enabled = false;
            button2.Enabled = false;
            StartToolStripMenuItem.Enabled = false;
            PauseToolStripMenuItem.Enabled = false;
            StopToolStripMenuItem.Enabled = false;
            StopCameraToolStripMenuItem.Enabled = false;
            PauseCameraToolStripMenuItem.Enabled = false;
            StopVideoToolStripMenuItem.Enabled = false;
            PauseVideoToolStripMenuItem.Enabled = false;
            lastUsedToolStripMenuItem.Enabled = false;
            OriginalPictureBox.Enabled = false;
            setCustomColor.Enabled = false;
        }

        private void ButtonEnabler()
        {
            btnStartLast.Enabled = true;
            btnStopp.Enabled = true;
            btnReset.Enabled = true;
            button2.Enabled = true;
            StartToolStripMenuItem.Enabled = true;
            PauseToolStripMenuItem.Enabled = true;
            StopToolStripMenuItem.Enabled = true;
            StopCameraToolStripMenuItem.Enabled = true;
            PauseCameraToolStripMenuItem.Enabled = true;
            StopVideoToolStripMenuItem.Enabled = true;
            PauseVideoToolStripMenuItem.Enabled = true;
            lastUsedToolStripMenuItem.Enabled = true;
            OriginalPictureBox.Enabled = true;
            setCustomColor.Enabled = true;
        }

        public void TimeTick(object sender, EventArgs e)
        {
            mat = _video.Capture.QueryFrame();
            if (mat == null)
            {
                btnStopp_Click(sender, e);
            return;  
            }

            try
            {
                _video.ImgOriginal = mat.ToImage<Bgr, byte>().Resize(OriginalPictureBox.Width, OriginalPictureBox.Height, Inter.Linear);

                OriginalPictureBox.Image = _video.ImgOriginal.Bitmap;

                //BallDetection();

                _ball.BallPositionDraw(_video, ATeam, BTeam, comboBox2.SelectedIndex);
                if (_ball.PositionComment != BallPos.Text) isRinged = false;
                BallPos.Text = _ball.PositionComment;

                AddSoundEffects();


                aTeamLabel.Text = _ball.Gcheck.CheckForScoreA(aTeamLabel.Text);
                bTeamLabel.Text = _ball.Gcheck.CheckForScoreB(bTeamLabel.Text);
            }
            catch (NullReferenceException n)
            {
                MessageBox.Show(n.ToString());
            }
        }

        public async void AddSoundEffects()
        {
            sound = new Task(() => isRinged = comment.CommentPlayGround(_ball.PositionComment, ATeam, BTeam, isRinged));
            sound.Start();
            await sound;
        }

        //menu strip tool items
        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _video = switcher.Controler(1);
            Source<Camera> source = _video.TakeASource;
            if (source())
            {
                _video.StartVideo();
                ButtonEnabler();
            }
        }

        private void pauseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _video.Pause();
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _video.Stop();
        }

        private void startToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //Covariance
            _video = switcher.Controler(0);
            Source<Video> source = _video.TakeASource;
            if (source())
            {
                _video.StartVideo();
                ButtonEnabler();
            }
        }

        private void startToolStripMenuItem2_Click(object sender, EventArgs e) // start/pause/stop
        {
            _video.StartVideo();
        }

        private void pauseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            _video.Pause();
        }

        private void stopToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            _video.Stop();
        }

        private void pauseToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            _video.Pause();
        }

        private void stopToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            _video.Stop();
        }

        // End Menu items------------


        // End Buttons------------

        //closing form
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_video != null) _video.Pause();
            if (!isTournament) Application.Exit();
        }

        //Picture
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Source<Picture> source = _picture.TakeASource;
            bool isCorrect = source();
            if (isCorrect)
                OriginalPictureBox.Image = _picture.GetImage.Bitmap;
        }

        //Coordinates
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)                          //checking coordinates of the video
        {
            Bitmap b = new Bitmap(OriginalPictureBox.Image);
            Color color = b.GetPixel(e.X, e.Y);
            float hue = color.GetHue();
            float saturation = color.GetSaturation();
            float lightness = color.GetBrightness();

            MessageBox.Show("X=" + e.X.ToString() + ";  Y=" + e.Y.ToString() + "\nB=" + color.B + " G=" + color.G + " R=" + color.R + "\nH=" + hue + "\nS=" + saturation + "\nL=" + lightness + ";");
        }

        //+----------------------
        private void Form1_Load(object sender, EventArgs e)
        {
            Commentator.isMuted = false;
            comment.StopAllTracks();
            aTeamLabel.Text = "0";
            bTeamLabel.Text = "0";
            comment.PlayIndexedSoundWithLoop(11);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveScore();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            Application.Exit();
        }

        private void allToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAllTeams form = new FormAllTeams();
            form.Show();
        }

   
        private void lastUsedToolStripMenuItem_Click(object sender, EventArgs e) // open last used video
        {
            _video.StartLastUsedVideo();
        }

        private void button2_Click(object sender, EventArgs e) // save score
        {
            _video.Stop();
            GameFinished();
            aTimer.Enabled = false;

        }

        private void SaveScore()
        {
            _TeamBScores = int.Parse(aTeamLabel.Text);
            _TeamAScores = int.Parse(bTeamLabel.Text);      

            Teams team = new Teams();

            team.UpdateTableGame(ATeam,BTeam,_TeamAScores,_TeamBScores,false);

          
        }
        private void GameFinished()
        {
            _TeamBScores = int.Parse(aTeamLabel.Text);
            _TeamAScores = int.Parse(bTeamLabel.Text);

            Teams team = new Teams();

            if (_TeamAScores > _TeamBScores)
            {
                team.UpdateTableGame(ATeam, BTeam, _TeamAScores, _TeamBScores, true);
                team.UpdateTableTeam(ATeam, 1, _TeamAScores);
                team.UpdateTableTeam(BTeam, 0, _TeamBScores);
                comment.PlayIndexedSound(9);
                DialogResult result = MessageBox.Show("Winner: " +ATeam + "!\nScore: " + _TeamAScores + " : " + _TeamBScores);
                if (result == DialogResult.Cancel || result == DialogResult.OK)
                {
                    comment.StopAllTracks();
                }
            }
            else if (_TeamAScores < _TeamBScores)
            {
                team.UpdateTableGame(ATeam, BTeam, _TeamAScores, _TeamBScores, true);
                team.UpdateTableTeam(BTeam, 1, _TeamBScores);
                team.UpdateTableTeam(ATeam, 0, _TeamAScores);
                comment.PlayIndexedSound(9);
                DialogResult result = MessageBox.Show("Winner: " + BTeam + "!\nScore: " + _TeamAScores + " : " + _TeamBScores);
                if (result == DialogResult.Cancel || result == DialogResult.OK)
                {
                    comment.StopAllTracks();
                }
            }
            else
            {
                team.UpdateTableGame(ATeam, BTeam, _TeamAScores, _TeamBScores, true);
                comment.PlayIndexedSound(9);
                DialogResult result = MessageBox.Show("Draw!\nScore: " + _TeamAScores + " : " + _TeamBScores);
                if (result == DialogResult.Cancel || result == DialogResult.OK)
                {
                    comment.StopAllTracks();
                }
            }
            comment.PlayIndexedSound(11);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {

            if (_video?.Capture == null)

            {
                _video = switcher.Controler(comboBox1.SelectedIndex);
            }

            if (btnStart.Text == @"Start")
            {
                if (_video.StartVideo())
                {
                    ButtonEnabler();
                    btnStart.Text = @"Pause";
                    btnStartLast.Text = @"Pause";
                }
            }
            else
            {
                _video.Pause();
                _ball.Gcheck.SetStopwatch(false);
                btnStart.Text = "Start";
                btnStartLast.Text = "Load last used video";
            }

            comment.StopAllTracks();
            //db timetick
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Interval = 10000;
            aTimer.Enabled = true;
        }

        private void btnStartLast_Click(object sender, EventArgs e)
        {
            if (btnStartLast.Text == "Load last used video")
            {
                _video.StartLastUsedVideo();
                btnStart.Text = "Pause";
                btnStartLast.Text = "Pause";
            }
            else
            {
                _video.Pause();
                _ball.Gcheck.SetStopwatch(false);
                btnStart.Text = "Start";
                btnStartLast.Text = "Load last used video";
            }
            comment.StopAllTracks();
        }

        private void btnStopp_Click(object sender, EventArgs e)
        {
            _video.Stop();
            btnStart.Text = "Start";
            btnStartLast.Text = "Load last used video";
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            if (_video.Check())
            {
                aTeamLabel.Text = "0";
                bTeamLabel.Text = "0";
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (_video.Check())
            {
                int temp = int.Parse(aTeamLabel.Text);
                temp = 0;
                aTeamLabel.Text = temp.ToString();

                temp = int.Parse(bTeamLabel.Text);
                temp = 0;
                bTeamLabel.Text = temp.ToString();
            }
        }


        private void editScore_Click(object sender, EventArgs e)
        {
            _video.Pause();
            _ball.Gcheck.SetStopwatch(false);

            SE = new ScoreEditor(ATeam, BTeam, bTeamLabel.Text, aTeamLabel.Text);
            SE.ShowDialog();

            int AP = SE.returnA();
            int BP = SE.returnB();
            bTeamLabel.Text = AP.ToString();
            aTeamLabel.Text = BP.ToString();

            _ball.Gcheck.SetStopwatch(true);
            _video.StartVideo();
            SE.Dispose();
        }

        private void enableSound_CheckedChanged(object sender, EventArgs e)
        {
            comment.Mute();
        }
        private void setCustomColor_Click(object sender, EventArgs e)
        {
            _video.Pause();
            _ball.Gcheck.SetStopwatch(false);

            CCC = new CustomColorViewer(_video.ImgOriginal);
            CCC.ShowDialog();

            _ball.SetCustomColor(CCC.newLow, CCC.newHigh);

            _ball.Gcheck.SetStopwatch(true);
            _video.StartVideo();
            CCC.Dispose();
        }
    }
}