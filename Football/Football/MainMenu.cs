﻿using System;
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
        public string _nameFirstTeam { get; set; }
        public string _nameSecondTeam { get; set; }

        Predicate<String> compare = x => (x != null) && (Regex.IsMatch(x, @"([a-zA-Z0-9]{4,50})"));

        public MainMenu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            VideoScreen form = new VideoScreen(_nameFirstTeam, _nameSecondTeam);
            Teams team = new Teams();        

            if ((compare(_nameFirstTeam)) && (compare(_nameSecondTeam)))
            {
                if (_nameFirstTeam != _nameSecondTeam)
                {                
                    form._nameFirstTeam = _nameFirstTeam;
                    form._nameSecondTeam = _nameSecondTeam;
                    form.Show();
                }
                else
                {
                    MessageBox.Show("Team names must be different ");
                }
            }
            else
            {
                MessageBox.Show("Team names must be at least 4 charachters long ");
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

        private void MainMenu_Load(object sender, EventArgs e)
        {

        }

      
        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormAllTeams form = new FormAllTeams();
        
            form.ShowDialog();
        }
    }
}
