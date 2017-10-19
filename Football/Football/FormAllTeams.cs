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

namespace Football
{
    public partial class FormAllTeams : Form
    {

        

        public FormAllTeams()
        {
            InitializeComponent();
         
        }
      
        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
         
          
        }
        
        public void fillData()
        {
           
            FootballEntities context = new FootballEntities();

              var team = from i in context.teamTables
                         orderby i.Victories descending
                         select new { i.Name, i.Victories, i.Goals };


            dataGridView1.DataSource = team.ToList();
 
        }

        private void FormAllTeams_Load(object sender, EventArgs e)
        {
            fillData();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void Statistics_Click(object sender, EventArgs e)
        {

        }

        private void victoriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FootballEntities context = new FootballEntities();

            var team = from i in context.teamTables
                       orderby i.Victories descending
                       select new{i.Name, i.Victories};
                       
            dataGridView1.DataSource = team.ToList();

        }

        private void goalsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FootballEntities context = new FootballEntities();

            var team = from i in context.teamTables
                       orderby i.Goals descending
                       select new { i.Name, i.Goals };


            dataGridView1.DataSource = team.ToList();

        }

        private void victoriesGoals0ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FootballEntities context = new FootballEntities();

            var team = from i in context.teamTables
                       where i.Victories != 0
                       where i.Goals != 0
                       orderby i.Victories descending
                       select new { i.Name, i.Victories, i.Goals };

            dataGridView1.DataSource = team.ToList();
        }

        private void allToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fillData();
        }
    }
}
