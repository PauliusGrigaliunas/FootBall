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

namespace Football
{
    public partial class MainMenu : Form
    {
        String name1;
        String name2;
       
        public MainMenu()
        {
            InitializeComponent();
        }

        public string Name1 { get => name1; set => name1 = value; }
        public string Name2 { get => name2; set => name2 = value; }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();

            Teams team = new Teams();
            if((Name1!=null)&&(Name2!=null))
            {
                if ((IfMatch(Name1)) && (IfMatch(Name2)))
                {
                    if (team.NameCheckIfExsist(Name1) != true)
                    {
                        team.AddToTable(Name1, 0, 0);
                    }

                    if (team.NameCheckIfExsist(Name2) != true)
                    {
                        team.AddToTable(Name2, 0, 0);
                    }
                    form.setName1(Name1);
                    form.setName2(Name2);
                    form.Show();

                }
                else
                {
                    MessageBox.Show("Team names must be at least 4 charachters long ");

                }
            }
            else
            {
                MessageBox.Show("Insert team names ");
            }
        
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox1_TextChange = (TextBox)sender;
            Name1 = textBox1_TextChange.Text;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox2_TextChange = (TextBox)sender;
            Name2 = textBox2_TextChange.Text;
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {

        }

        private bool IfMatch(String data)
        {
            bool mtch = false;
            String pattern1 = @"([a-zA-Z]{4,50})";
            Match match = Regex.Match(data, pattern1);
        
            
            if (match.Success)
            {
                mtch = true;
            }


            return mtch;
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
