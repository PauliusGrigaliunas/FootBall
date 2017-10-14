using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;



namespace Football
{
    public class Teams
    {
        private int teamNumber;
        private string teamName;
        //  private int victories;
        // private int goals;

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Emilija.DELL-EMILIJOS\Documents\GitHub\FootBall\Football\Football\database121.mdf;Integrated Security=True");
        SqlCommand cmd;
        SqlDataAdapter sa;
        DataTable dt = new DataTable();


        public Teams() { }


        public void addToTable(String name, int victories, int goals)
        {
            //prideti komandos informacija i lentele
            try
            {
                con.Open();

                cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM teamTable";
                cmd.ExecuteNonQuery();

                sa = new SqlDataAdapter(cmd);
                sa.Fill(dt);

                cmd.CommandText = "INSERT INTO teamTable (Name,Victories,Goals) VALUES('" + name + "','" + victories + "','" + goals + "')";
                cmd.ExecuteNonQuery();              //vykdom savo uzklausa virsuj parasyta

              
                con.Close();
              //  insertToTable("Namas", 2, 6);
                
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }

        public bool name_CheckIfExsist(DataTable dt, String data)
        {//patikrinti ar tokia komanda jau yra zaidus
            bool exsists = false;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                try
                {
                    if (data == dt.Rows[i][1].ToString())
                    {
                        exsists = true;
                    }
                }
                catch (System.IndexOutOfRangeException ex)
                {

                    MessageBox.Show(ex.Message);
                }

            }
            return exsists;

        }

        public int getVictories(DataTable dt, String name)
        {
            int victories = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                try
                {
                    if (0 != Int32.Parse(dt.Rows[i][2].ToString()) && (name == dt.Rows[i][1].ToString()))
                    {
                        victories = Int32.Parse(dt.Rows[i][2].ToString());
                    }
                }
                catch (System.IndexOutOfRangeException ex)
                {

                    MessageBox.Show(ex.Message);
                }

            }
            return victories;

        }

        public int getGoals(DataTable dt, String name)
        {
            int goals = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                try
                {
                    if (0 != Int32.Parse(dt.Rows[i][3].ToString())&&(name==dt.Rows[i][1].ToString()))
                    {
                        goals = Int32.Parse(dt.Rows[i][3].ToString());
                    }
                }
                catch (System.IndexOutOfRangeException ex)
                {

                    MessageBox.Show(ex.Message);
                }

            }
            return goals;

        }

        public void insertToTable(String name, int victories, int goals)
        {
            //irasyti rezultata
            try
            {
                con.Open();

                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM teamTable";
                cmd.ExecuteNonQuery();

                sa = new SqlDataAdapter(cmd);
                sa.Fill(dt);

            
                con.Close();
                cmd.CommandText = "UPDATE teamTable SET [Victories] = @Victories,[Goals] = @Goals WHERE [Name] = @Name";
                cmd.Parameters.AddWithValue("@Victories", victories);
                cmd.Parameters.AddWithValue("@Goals", goals);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }
        public void deleateTable()
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[5].Delete();

            }
        }




    }
}
