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
      

        public void AddToTableTeams(String name, int victories)
        {
            using (FootballEntities1 contex = new FootballEntities1())
            {
                TeamsTable team = new TeamsTable()
                {
                    Name = name,
                    Victories = victories,
                   
                };         
 
                contex.TeamsTables.Add(team);
                contex.SaveChanges();
            }
        }
        public void AddToTableGames(int firstTeam, int secondTeam, int goals1, int goals2)
        {
            using (FootballEntities1 contex = new FootballEntities1())
            {
                GameTable team = new GameTable()
                {
                    FirstTeam = firstTeam,
                    SecondTeam = secondTeam,
                    FirstTeamScore = goals1,
                    SecondTeamScore = goals2,

                };

                contex.GameTables.Add(team);
                contex.SaveChanges();
            }
        }

        public bool NameCheckIfExsist(String data)
       {
            bool exsists = false;
            try
            {
                using (FootballEntities1 contex = new FootballEntities1())
                {
                    var team = contex.TeamsTables.FirstOrDefault(r => r.Name == data);

                    if (team.Name == data)
                    {
                        exsists = true;
                    }
                }
            }
            catch (System.NullReferenceException ex)
            {
                ex.ToString();
            }  
           
               return exsists;
            
       }

       public int GetVictories(String data)
       {
            int vict = 0;

            using (FootballEntities1 contex = new FootballEntities1())
            {
                
                var team = contex.TeamsTables.FirstOrDefault(r => r.Name == data);
                vict = (int)team.Victories;
            }
            return vict;
       }


        public void UpdateTableTeams(String data, int victories)
        {
           
            using (FootballEntities1 contex = new FootballEntities1())
            {

                TeamsTable team = contex.TeamsTables.FirstOrDefault(r => r.Name == data);
                team.Victories = victories;
         
                contex.SaveChanges();
            }

        }


        public void InsertGame(String name1, String name2, int goal1, int goal2)
        {
            using (FootballEntities1 contex = new FootballEntities1())
            {
                int team1;
                int team2;

                //getting id's of teams
                var team = contex.TeamsTables.FirstOrDefault(r => r.Name == name1);
                team1 = team.Id;

                team = contex.TeamsTables.FirstOrDefault(r => r.Name == name2);
                team2 = team.Id;


                AddToTableGames(team1, team2, goal1, goal2);
                contex.SaveChanges();
            }


        }
        //database loaded to list
        public List<TeamsTable> AllDataToList() 
        {
            using (FootballEntities1 context = new FootballEntities1())
            {
                var teams = from i in context.TeamsTables
                            select i;
                var list = teams.ToList();

                return list;
            }
        
        }

        public List<TeamsTable> OrderByVictories()
        {
            using (FootballEntities1 context = new FootballEntities1())
            {
                var teams = from i in context.TeamsTables
                            orderby i.Victories descending
                            select i;
                var list = teams.ToList();
            
            return list;
            }
        }
        public List<GameTable> AllGamesToList()
        {
            using (FootballEntities1 context = new FootballEntities1())
            {
                var teams = from i in context.GameTables
                            select i;
                var list = teams.ToList();

                return list;
            }
        }




    }
}
