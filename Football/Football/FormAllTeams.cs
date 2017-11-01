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
     
        
        public void FillData()
        {
           
            FootballEntities context = new FootballEntities();
            var team = context.teamTables.OrderByDescending(x => x.Victories)
                .Select(x => new { x.Name, x.Victories, x.Goals });

            /*var team = from i in context.teamTables
                         orderby i.Victories descending
                         select new { i.Name, i.Victories, i.Goals };*/

            dataAllTeamsGrid.DataSource = team.ToList(); 
        }

        private void FormAllTeams_Load(object sender, EventArgs e)
        {
            FillData();
        }

        private void VictoriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FootballEntities context = new FootballEntities();

            var team = from i in context.teamTables
                       orderby i.Victories descending
                       select new{i.Name, i.Victories};
                       
            dataAllTeamsGrid.DataSource = team.ToList();

        }

        private void GoalsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FootballEntities context = new FootballEntities();

            var team = from i in context.teamTables
                       orderby i.Goals descending
                       select new { i.Name, i.Goals };


            dataAllTeamsGrid.DataSource = team.ToList();

        }

        private void VictoriesGoals0ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FootballEntities context = new FootballEntities();

            var team = from i in context.teamTables
                       where i.Victories != 0
                       where i.Goals != 0
                       orderby i.Victories descending
                       select new { i.Name, i.Victories, i.Goals };

            dataAllTeamsGrid.DataSource = team.ToList();
        }

        private void allToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FillData();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }

}
