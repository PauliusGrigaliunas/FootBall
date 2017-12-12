using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Threading;

namespace Football
{
    public partial class MainMenu : Form
    {
        Commentator player = new Commentator();
        public string _nameFirstTeam { get; set; }
        public string _nameSecondTeam { get; set; }


        Predicate<String> compare = x => (x != null) && (Regex.IsMatch(x, @"([a-zA-Z0-9]{4,50})"));

        public MainMenu()
        {
            InitializeComponent();
            //InputThread inputThread = InputThread.Instance;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            VideoScreen form = new VideoScreen(_nameFirstTeam, _nameSecondTeam);

            Teams team = new Teams();

            try
            {
                if (!compare(_nameFirstTeam) && !compare(_nameFirstTeam))
                    throw new IsTooShortException("First Team & Second Team");
                if (!compare(_nameFirstTeam))
                    throw new IsTooShortException("First Team");
                if (!compare(_nameSecondTeam))
                    throw new IsTooShortException("Second Team");
                if (_nameFirstTeam == _nameSecondTeam)
                    throw new IsEqualException();

                form._nameFirstTeam = _nameFirstTeam;
                form._nameSecondTeam = _nameSecondTeam;
                form.Show();


            }
            catch (IsTooShortException itse)
            {
                MessageBox.Show(itse.Message);
            }
            catch (IsEqualException iee)
            {
                MessageBox.Show(iee.Message);
            }
        }

    private void textBox1_TextChanged(object sender, EventArgs e)
    {
        TextBox textBox1_TextChange = (TextBox)sender;
        _nameFirstTeam = textBox1_TextChange.Text;
    }

    private void textBox2_TextChanged(object sender, EventArgs e)
    {
        TextBox textBox2_TextChange = (TextBox)sender;
        _nameSecondTeam = textBox2_TextChange.Text;
    }


    private void button2_Click(object sender, EventArgs e)
    {
        FormAllTeams form = new FormAllTeams();
        form.ShowDialog();
    }

    private void button3_Click(object sender, EventArgs e)
    {
        TeamNames names = new TeamNames();
        names.Show();
    }

    private void MainMenu_Load(object sender, EventArgs e)
    {
         player.PlayIndexedSoundWithLoop(10);
    }

    private void MainMenu_FormClosed(object sender, FormClosedEventArgs e)
    {
         player.StopAllTracks();
    }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            player.Mute();
        }
    }
}
