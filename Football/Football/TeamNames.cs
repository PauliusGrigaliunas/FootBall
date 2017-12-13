using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Football
{
    public partial class TeamNames : Form
    {

        private string _name1;
        private string _name2;
        private string _name3;
        private string _name4;
        
        public TeamNames()
        {
            InitializeComponent();

            label1.Text = Properties.Settings.Default.Team1;
            label2.Text = Properties.Settings.Default.Team2;
            label3.Text = Properties.Settings.Default.Team3;
            label4.Text = Properties.Settings.Default.Team4;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            _name1 = textBox1.Text;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            _name2 = textBox2.Text;

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            _name3 = textBox3.Text;

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            _name4 = textBox4.Text;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Predicate<String> compare = x => (x != null) && (Regex.IsMatch(x, @"([a-zA-Z0-9]{4,50})"));

          try
            {
                if (!(compare(_name1)) && !(compare(_name2)) && !(compare(_name3)) && !(compare(_name4)))
                    throw new IsTooShortException("First Team & Second Team & Third Team & Fourth Team");
                if (!compare(_name1))
                    throw new IsTooShortException("First Team ");
                if (!compare(_name2))
                    throw new IsTooShortException("Second Team ");
                if (!compare(_name3))
                    throw new IsTooShortException("Third Team ");
                if (!compare(_name4))
                    throw new IsTooShortException("Fourth Team ");
                if (_name1 == _name2)
                    throw new IsEqualException("First & Second ");
                if (_name1 == _name3)
                    throw new IsEqualException("First & Third ");
                if (_name1 == _name4)
                    throw new IsEqualException("First & Fourth ");
                if (_name2 == _name3)
                    throw new IsEqualException("Second & Third ");
                if (_name2 == _name4)
                    throw new IsEqualException("Second & TFourth ");
                if (_name3 == _name4)
                    throw new IsEqualException("Third & Fourth ");

                Tournament names = new Tournament(_name1, _name2, _name3, _name4);
                names.Show();
                this.Close();


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

        private void TeamNames_Load(object sender, EventArgs e)
        {

        }
    }
}
