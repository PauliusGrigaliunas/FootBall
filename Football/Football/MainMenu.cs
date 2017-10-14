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
    public partial class MainMenu : Form
    {
        String name1;
        String name2;
        SqlCommand cmd;
        SqlDataAdapter sa;
        DataTable dt = new DataTable();
        Teams team = new Teams();
        public MainMenu()
        {
            InitializeComponent();
        }

        public string Name1 { get => name1; set => name1 = value; }
        public string Name2 { get => name2; set => name2 = value; }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Emilija.DELL-EMILIJOS\Documents\GitHub\FootBall\Football\Football\database121.mdf;Integrated Security=True");
            con.Open();

            cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM teamTable";
            cmd.ExecuteNonQuery();
            sa = new SqlDataAdapter(cmd);
            sa.Fill(dt);
           
            if (team.name_CheckIfExsist(dt,Name1) == false)
            {
                team.addToTable(Name1, 0, 0);
            }

     
            if (team.name_CheckIfExsist(dt,Name2) == false)
            {
                team.addToTable(Name2, 0, 0);
            }
            con.Close();

            if (Name1 == "a")
            {
                MessageBox.Show("it is a");
            }
            MessageBox.Show("Teams successfully registered!");
            form.Show();
            form.setName1(Name1);
            form.setName2(Name2);
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
    }
}
