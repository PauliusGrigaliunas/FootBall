using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Football
{
    public partial class Tournament : Form
    {
        public string _name1 { get; set; }
        public string _name2 { get; set; }
        public string _name3 { get; set; }
        public string _name4 { get; set; }

        public int _TeamAScores { get; set; }
        public int _TeamBScores { get; set; }

        private bool _finished1 = false;
        private bool _finished2 = false;
        public Tournament()
        {
            InitializeComponent();
        }
        public Tournament(string name1, string name2, string name3, string name4)
        {
            _name1 = name1;
            _name2 = name2;
            _name3 = name3;
            _name4 = name4;
           
            InitializeComponent();

            label5.Text = Properties.Settings.Default.Playingfor3rd;
            label6.Text = Properties.Settings.Default.PlayingFor1st;
            label7.Text = Properties.Settings.Default.First;
            label8.Text = Properties.Settings.Default.Second;
            label9.Text = Properties.Settings.Default.Third;
            label10.Text = Properties.Settings.Default.Fourth;
            button1vs2.Text = Properties.Settings.Default.Versus;
            button3vs4.Text = Properties.Settings.Default.Versus;
            buttonloser34.Text = Properties.Settings.Default.Versus;
            buttonw1vsw2.Text = Properties.Settings.Default.Versus;
        }
        private void Form1_Load(object sender, EventArgs e)
        {

            label1.Text = _name1;
            label2.Text = _name2;
            label3.Text = _name3;
            label4.Text = _name4;
        }

        private void label9_Click(object sender, EventArgs e)
        {
     
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
        //first mach
        private void button1vs2_Click(object sender, EventArgs e)
        {
            StartGame(_name1, _name2);


            if (_TeamAScores > _TeamBScores)
            {
                winer12.Text = _name1;
                loser12.Text = _name2;

            }
            else
            {
                winer12.Text = _name2;
                loser12.Text = _name1;
            }

            labelgoal1.Text = _TeamAScores.ToString() + " : "+_TeamBScores.ToString();
            _finished1 = true;
        }
        //second mach
        private void button3vs4_Click(object sender, EventArgs e)
        {
            StartGame(_name3, _name4);

            if (_TeamAScores > _TeamBScores)
            {
                winer34.Text = _name3;
                loser34.Text = _name4;
            }
            else
            {
                winer34.Text = _name4;
                loser34.Text = _name3;
            }
            labelgoal2.Text = _TeamAScores.ToString() + " : " + _TeamBScores.ToString();
            _finished2 = true;
        }
        //for 3 place
        private void buttonloser34_Click(object sender, EventArgs e)
        {
            if (_finished1 && _finished2)
            {
                StartGame(loser12.Text, loser34.Text);

                if (_TeamAScores > _TeamBScores)
                {
                    place3.Text = loser12.Text;
                    place4.Text = loser34.Text;
                }
                else
                {
                    place4.Text = loser12.Text;
                    place3.Text = loser34.Text;
                }
                labelgoal3.Text = _TeamAScores.ToString() + " : " + _TeamBScores.ToString();
            }
            else MessageBox.Show("Begining games are not done yet");
           

        }
        //for 1 place
        private void buttonw1vsw2_Click(object sender, EventArgs e)
        {
            if (_finished1 && _finished2)
            {
                StartGame(winer12.Text, winer34.Text);

            if (_TeamAScores > _TeamBScores)
            {
                place1.Text = winer12.Text;
                place2.Text = winer34.Text;
                winer1.Text = winer12.Text;
            }
            else
            {
                winer1.Text = winer34.Text;
                place2.Text = winer12.Text;
                place1.Text = winer34.Text;
            }
                labelgoal4.Text = _TeamAScores.ToString() + " : " + _TeamBScores.ToString();
            }
            else MessageBox.Show("Begining games are not done yet");

        }

        private void place2_Click(object sender, EventArgs e)
        {

        }

        private void StartGame(String name1, String name2)
        {
            VideoScreen form = new VideoScreen(name1, name2);
            form.isTournament = true;
            form.ATeam = name1;
            form.BTeam = name2;
            form.ShowDialog();
            _TeamAScores = form._TeamAScores;
            _TeamBScores = form._TeamBScores;

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void place1_Click(object sender, EventArgs e)
        {

        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
