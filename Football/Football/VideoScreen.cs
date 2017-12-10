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

namespace Football
{
    delegate bool Source<in T>();

    public partial class VideoScreen : Form
    {
        Task sound;

        //objects
        Picture _picture = new Picture();
        Ball _ball = new Ball();
        Gates _gates = new Gates();
        Switch switcher = new Switch();
        ChooseColour chooseColour = new ChooseColour();
        ScoreEditor SE;
        CustomColorViewer CCC;
        ISource _video;

        GoalsChecker _gcheck;
        private Mat mat;
        private Stopwatch _stopwatch = new Stopwatch();
        Commentator comment = new Commentator();

        public bool isTournament = false;

        //variables
        public String _nameFirstTeam { get; set; }
        public String _nameSecondTeam { get; set; }

        public int _TeamAScores { get; set; }
        public int _TeamBScores { get; set; }
        public int _VictA { get; set; }
        public int _GoalA { get; set; }
        public int _VictB { get; set; }
        public int _GoalB { get; set; }
        public bool isRinged = false;

        public static bool isATeamScored = false;
        public static bool isBTeamScored = false;

        //picture variables
        Image<Gray, byte> _imgFiltered { get; set; }
        Image<Gray, byte> _ImgZones { get; set; }

        //variables
        private int _i = 0;
        public string ATeam, BTeam;
        private int CustomColorIndex = 2;
        private int GatesColorIndex = 3;
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
            _gcheck = new GoalsChecker(_stopwatch);
            aTeamLabel.Text = _gcheck.CheckForScoreA(aTeamLabel.Text);
            bTeamLabel.Text = _gcheck.CheckForScoreB(bTeamLabel.Text);

            
            //getting frames        
            mat = _video.Capture.QueryFrame();
            if (mat == null) return;
            try
            {
                _video.ImgOriginal = mat.ToImage<Bgr, byte>().Resize(OriginalPictureBox.Width, OriginalPictureBox.Height, Inter.Linear);

                OriginalPictureBox.Image = _video.ImgOriginal.Bitmap;
                BallDetection();

                
            }
            catch (NullReferenceException n)
            {
                MessageBox.Show(n.ToString());
            }
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


        public void BallDetection()
        {
            ColourStruct clr = _gates.chooseColour.Controler(GatesColorIndex);
            _ImgZones = _video.GetFilteredImageZones(clr);

            ColourStruct colour = _ball.chooseColour.Controler(comboBox2.SelectedIndex);
            Image<Bgr, byte> imgCircles = _video.ImgOriginal.CopyBlank();
            _ball.ImgFiltered = _video.GetFilteredImage(colour);
            _ball.ImgOriginal = _video.ImgOriginal;

            //System.Diagnostics.Debug.WriteLine(_ball.chooseColour.Controler(CustomColorIndex).Low + " <= low  |  high =>" + _ball.chooseColour.Controler(CustomColorIndex).High);

            setValues();
            _ball.BallPositionDraw(imgCircles);
            unifyValues();
            commentatorTextCompatibility();
        }

        private void commentatorTextCompatibility()
        {
            if (_ball.PositionComment != BallPos.Text) isRinged = false;
            BallPos.Text = _ball.PositionComment;

            addSoundEffects();
        }

        public async void addSoundEffects()
        {
            
            sound = new Task(() => Comment());
            sound.Start();
            await sound;
        }

        private void Comment()
        {
            if (_ball.PositionComment == ATeam + " Team Defenders" || _ball.PositionComment == BTeam + " Team Defenders")
            {
                if (isRinged == false)
                {
                    comment.StopAllTracks();
                    if (!enableSound.Checked)
                        comment.PlayRandomSound(16, 18);
                    isRinged = true;
                }
            }
            else if (_ball.PositionComment == ATeam + " Team Attackers" || _ball.PositionComment == BTeam + " Team Attackers")
            {
                if (isRinged == false)
                {
                    comment.StopAllTracks();
                    if (!enableSound.Checked)
                        comment.PlayRandomSound(14, 16);
                    isRinged = true;
                }
            }
            else if (_ball.PositionComment == ATeam + " Team Middle 5" || _ball.PositionComment == BTeam + " Team Middle 5")
            {
                if (isRinged == false)
                {
                    comment.StopAllTracks();
                    if (!enableSound.Checked)
                        comment.PlayRandomSound(14, 16);
                    isRinged = true;
                }
            }
            else
            {
                if (isRinged == false)
                {
                    comment.StopAllTracks();
                    if (!enableSound.Checked)
                        comment.PlayRandomSound(12, 14);
                    isRinged = true;
                }
            }
        }
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
            comment.StopAllTracks();
            aTeamLabel.Text = "0";
            bTeamLabel.Text = "0";
            comment.PlayIndexedSoundWithLoop(11);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _video.Stop();
            _TeamBScores = int.Parse(aTeamLabel.Text);
            _TeamAScores = int.Parse(bTeamLabel.Text);

            _VictA = 0;
            _GoalA = 0;
            _VictB = 0;
            _GoalB = 0;

            Teams team = new Teams();
            Predicate<String> compare = x => team.NameCheckIfExsist(x) == true;

            if (_TeamAScores > _TeamBScores)
            {
                _VictA = _VictA + 1;
                comment.PlayIndexedSound(9);
                DialogResult result = MessageBox.Show("Winner: " + _nameFirstTeam + "!\nScore: " + _TeamAScores + " : " + _TeamBScores);
                if (result == DialogResult.Cancel || result == DialogResult.OK)
                {
                    comment.StopAllTracks();
                }
            }
            else if (_TeamAScores < _TeamBScores)
            {
                _VictB = _VictB + 1;
                comment.PlayIndexedSound(9);
                DialogResult result = MessageBox.Show("Winner: " + _nameSecondTeam + "!\nScore: " + _TeamAScores + " : " + _TeamBScores);
                if (result == DialogResult.Cancel || result == DialogResult.OK)
                {
                    comment.StopAllTracks();
                }
            }
            else
            {
                comment.PlayIndexedSound(9);
                DialogResult result = MessageBox.Show("Draw!\nScore: " + _TeamAScores + " : " + _TeamBScores);
                if (result == DialogResult.Cancel || result == DialogResult.OK)
                {
                    comment.StopAllTracks();
                }
            }
            _GoalA = _TeamAScores;
            _GoalB = _TeamBScores;
            if (!compare(_nameFirstTeam))
            {
                team.AddToTableTeams(_nameFirstTeam, _VictA);
              
            }
            else
            {
                _VictA = _VictA + team.GetVictories(_nameFirstTeam);
                team.UpdateTableTeams(_nameFirstTeam, _VictA);
     
            }

            if (!compare(_nameSecondTeam))
            {
                team.AddToTableTeams(_nameSecondTeam, _VictB);
                
       
            }
            else
            {
                _VictB = _VictB + team.GetVictories(_nameSecondTeam);
                team.UpdateTableTeams(_nameSecondTeam, _VictB);

            }
            team.InsertGame(_nameFirstTeam, _nameSecondTeam, _GoalA, _GoalB);
            comment.PlayIndexedSound(11);
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

        private void teamAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormTeamA form = new FormTeamA();
            form.loadInfo(_nameFirstTeam, _VictA, _GoalA, _TeamAScores);
            form.Show();
        }

        private void teamBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormTeamB form = new FormTeamB();
            form.loadInfo(_nameSecondTeam, _VictB, _GoalB, _TeamBScores);
            form.Show();
        }

        private void lastUsedToolStripMenuItem_Click(object sender, EventArgs e) // open last used video
        {
            _video.StartLastUsedVideo();
        }

        private void button2_Click(object sender, EventArgs e) // save score
        {
            _video.Stop();
            _TeamBScores = int.Parse(aTeamLabel.Text);
            _TeamAScores = int.Parse(bTeamLabel.Text);

            _VictA = 0;
            _GoalA = 0;
            _VictB = 0;
            _GoalB = 0;

            Teams team = new Teams();
            Predicate<String> compare = x => team.NameCheckIfExsist(x) == true;

            if (_TeamAScores > _TeamBScores)
            {
                _VictA = _VictA + 1;
                comment.PlayIndexedSound(9);
                DialogResult result = MessageBox.Show("Winner: " + _nameFirstTeam + "!\nScore: " + _TeamAScores + " : " + _TeamBScores);
                if (result == DialogResult.Cancel || result == DialogResult.OK)
                {
                    comment.StopAllTracks();
                }
            }
            else if (_TeamAScores < _TeamBScores)
            {
                _VictB = _VictB + 1;
                comment.PlayIndexedSound(9);
                DialogResult result = MessageBox.Show("Winner: " + _nameSecondTeam + "!\nScore: " + _TeamAScores + " : " + _TeamBScores);
                if (result == DialogResult.Cancel || result == DialogResult.OK)
                {
                    comment.StopAllTracks();
                }
            }
            else
            {
                comment.PlayIndexedSound(9);
                DialogResult result = MessageBox.Show("Draw!\nScore: " + _TeamAScores + " : " + _TeamBScores);
                if (result == DialogResult.Cancel || result == DialogResult.OK)
                {
                    comment.StopAllTracks();
                }
            }
            _GoalA = _TeamAScores;
            _GoalB = _TeamBScores;
            if (!compare(_nameFirstTeam))
            {
                team.AddToTableTeams(_nameFirstTeam, _VictA);
                
               
            }
            else
            {
                _VictA = _VictA + team.GetVictories(_nameFirstTeam);
              
                team.UpdateTableTeams(_nameFirstTeam, _VictA);
              
            }

            if (!compare(_nameSecondTeam))
            {
                team.AddToTableTeams(_nameSecondTeam, _VictB);             
               
            }
            else
            {
                _VictB = _VictB + team.GetVictories(_nameSecondTeam);        
                team.UpdateTableTeams(_nameSecondTeam, _VictB);
                
            }
            team.InsertGame(_nameFirstTeam, _nameSecondTeam, _GoalA, _GoalB);
            comment.PlayIndexedSound(11);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (_video == null || _video.Capture == null)
            {
                _video = switcher.Controler(comboBox1.SelectedIndex);
            }

            if (btnStart.Text == "Start")
            {
                if (_video.StartVideo())
                {
                    ButtonEnabler();
                    btnStart.Text = "Pause";
                    btnStartLast.Text = "Pause";
                }
            }
            else
            {
                _video.Pause();
                _gcheck.setStopwatch(false);
                btnStart.Text = "Start";
                btnStartLast.Text = "Load last used video";
            }
            comment.StopAllTracks();
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
                _gcheck.setStopwatch(false);
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

        private void setValues()
        {
            _ball.ImgGates = _ImgZones;
            _ball.at = ATeam;
            _ball.bt = BTeam;
            _ball.Gcheck = _gcheck;
            _ball.xCoordList = _xCoordList;
            _ball.Index = _i;
        }

        private void unifyValues()
        {
            _i = _ball.Index;
            _xCoordList = _ball.xCoordList;
            _gcheck = _ball.Gcheck;
        }

        private void editScore_Click(object sender, EventArgs e)
        {
            _video.Pause();
            _gcheck.setStopwatch(false);

            SE = new ScoreEditor(ATeam, BTeam, bTeamLabel.Text, aTeamLabel.Text);
            SE.ShowDialog();

            int AP = SE.returnA();
            int BP = SE.returnB();
            bTeamLabel.Text = AP.ToString();
            aTeamLabel.Text = BP.ToString();

            _gcheck.setStopwatch(true);
            _video.StartVideo();
            SE.Dispose();
        }

        private void setCustomColor_Click(object sender, EventArgs e)

        {
            _video.Pause();
            _gcheck.setStopwatch(false);

            CCC = new CustomColorViewer(_video.ImgOriginal);
            CCC.ShowDialog();

            _ball.setCustomColor(CCC.newLow, CCC.newHigh);

            _gcheck.setStopwatch(true);
            _video.StartVideo();
            CCC.Dispose();
        }

    }
}