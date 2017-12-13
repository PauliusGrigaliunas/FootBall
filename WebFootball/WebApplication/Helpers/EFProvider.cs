using Football;
using System.Collections.Generic;
using System.Linq;

namespace WebApplication.Helpers
{
    public class EFProvider
    {/*
        public void InsertNewGame(Game game)
        {
            using (var db = new Football1Entities())
            {
                db.Games.Add(game);
                db.SaveChanges();
            }
        }

        public Game GetCurrentGame(string leftTeam, string rightTeam)
        {
            Game activeGame;
            using (var db = new Football1Entities())
            {
                var team = db.Teams.FirstOrDefault(i => i.Name == leftTeam);
                var team1 = db.Teams.FirstOrDefault(i => i.Name == rightTeam);
                activeGame = db.Games.Where(g => g.FirstTeam == team.Id && g.SecondTeam == team1.Id && g.IsFinished == false).First();
            }
            return activeGame;
        }

        public Game GetActiveGame()
        {
            Game game;
            using (var db = new Football1Entities())
            {
                var result = db.Games.ToList().Last();
                game = result;
            }
            return game;
        }

        public void UpdateGame(Game game)
        {
            using (var db = new Football1Entities())
            {
                db.Entry(game).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void InsertUser(Team user)
        {
            using (var db = new Football1Entities())
            {
                if (db.Teams.Where(x => x.Name == user.Name).Count() == 0)
                {
                    db.Teams.Add(user);
                    db.SaveChanges();
                }
            }
        }
        //Does not update user somehow
        public void UpdateUser(Team userToUpdate)
        {
            using (var db = new Football1Entities())
            {
                List<Team> users = db.Teams.Where(x => x.Name == userToUpdate.Name).ToList();
                if (users.Count() == 1)
                {
                    users[0] = userToUpdate;
                    db.Entry(users[0]).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
            }
        }

        public List<Team> GetUsers()
        {
            List<Team> data = new List<Team>();
            using (var db = new Football1Entities())
            {
                data = db.Teams.ToList();
            }
            return data;
        }

        public Team GetUser(string username)
        {
            Team user;
            using (var db = new Football1Entities())
            {
                user = db.Teams.Where(x => x.Name == username).First();
            }
            return user;
        }
        */

    }
}