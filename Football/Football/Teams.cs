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

        public void AddTeamToTable(String name)
        {
            using (Football1Entities contex = new Football1Entities())
            {
                Team team = new Team()
                {
                    Name = name,
                 
                };         
 
                contex.Teams.Add(team);
                contex.SaveChanges();
            }
        }
        public void AddGameToTable(String firstTeam, String secondTeam, DateTime date)
        {
            using (Football1Entities contex = new Football1Entities())
            {
                Team team = contex.Teams.FirstOrDefault(r => r.Name == firstTeam);
                Team team1 = contex.Teams.FirstOrDefault(r => r.Name == secondTeam);
                Game game = new Game()
                {
                    FirstTeam = team.Id,
                    SecondTeam = team1.Id,
                    Date = date,
                };

                contex.Games.Add(game);
                contex.SaveChanges();
            }
        }

        public bool NameCheckIfExsist(String data)
       {
            bool exsists = false;
            try
            {
                using (Football1Entities contex = new Football1Entities())
                {
                    var team = contex.Teams.FirstOrDefault(r => r.Name == data);

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

            using (Football1Entities contex = new Football1Entities())
            {
                
                var team = contex.Teams.FirstOrDefault(r => r.Name == data);
                vict = (int)team.GamesWon;
            }
            return vict;
       }


        public void UpdateTableTeam(String data, int victories, int scores)
        {
           
            using (Football1Entities contex = new Football1Entities())
            {

                Team team = contex.Teams.FirstOrDefault(r => r.Name == data);
                team.GamesPlayed++;
                team.GamesWon = team.GamesWon + victories;
                team.TotalGoals = team.TotalGoals + scores;

                contex.SaveChanges();
            }

        }
 

        public void UpdateTableGame(String name1, String name2, int goal1, int goal2, bool isFinished)
        {
            using (Football1Entities contex = new Football1Entities())
            {
                int team1;
                int team2;

                //getting id's of teams
                var team = contex.Teams.FirstOrDefault(r => r.Name == name1);
                team1 = team.Id;

                team = contex.Teams.FirstOrDefault(r => r.Name == name2);
                team2 = team.Id;

                var game = contex.Games.FirstOrDefault(i => i.FirstTeam == team1 && i.SecondTeam == team2);
                game.FirstTeamScore = goal1;
                game.SecondTeamScore =  goal2;
                if(isFinished)
                {
                    game.IsFinished = true;
                }
             
                contex.SaveChanges();
            }


        }
        //database loaded to list
        public List<Team> AllDataToList() 
        {
            using (Football1Entities context = new Football1Entities())
            {
                var teams = from i in context.Teams
                            select i;
                var list = teams.ToList();

                return list;
            }
        
        }

        public List<Team> OrderByVictories()
        {
            using (Football1Entities context = new Football1Entities())
            {
                var teams = from i in context.Teams
                            orderby i.GamesWon descending
                            select i;
                var list = teams.ToList();
            
            return list;
            }
        }
        public List<Game> AllGamesToList()
        {
            using (Football1Entities context = new Football1Entities())
            {
                var teams = from i in context.Games
                            select i;
                var list = teams.ToList();

                return list;
            }
        }




    }
}
