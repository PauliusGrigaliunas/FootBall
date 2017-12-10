using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication;


namespace WebApplication.Controllers
{
    public class TeamsTablesController : Controller
    {
        private FootballEntities db = new FootballEntities();

        // GET: TeamsTables
        public ActionResult Index()
        {
           
            return View(db.TeamsTables.OrderByDescending(i => i.Victories).ToList());
        }

        // GET: TeamsTables/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeamsTable teamsTable = db.TeamsTables.Find(id);
            if (teamsTable == null)
            {
                return HttpNotFound();
            }
            return View(teamsTable);
        }

        // GET: TeamsTables/Create
        public ActionResult Goals()
        {
            var teams = db.TeamsTables.ToList();
            var games = db.GameTables.ToList();
            var team1 = (from i in games
                         let Name = i.FirstTeam
                         let Score = i.FirstTeamScore
                         select new { Name, Score }).ToList();

            var team2 = (from i in games
                         let Name = i.SecondTeam
                         let Score = i.SecondTeamScore
                         select new { Name, Score }).ToList();

            var list2 = team1.Concat(team2).Join(teams,
                                                  i => i.Name,
                                                  j => j.Id,
                                                  (i, j) => new { j.Name, i.Score }).GroupBy(i => i.Name).
                                                  Select(i => new TeamsTable
                                                  {
                                                      Name = i.First().Name,
                                                      Victories = i.Sum(j => j.Score),

                                                  }).ToList();
          
            return View(list2);
        }

        // POST: TeamsTables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Victories")] TeamsTable teamsTable)
        {
            if (ModelState.IsValid)
            {
                db.TeamsTables.Add(teamsTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(teamsTable);
        }

        // GET: TeamsTables/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeamsTable teamsTable = db.TeamsTables.Find(id);
            if (teamsTable == null)
            {
                return HttpNotFound();
            }
            return View(teamsTable);
        }

        // POST: TeamsTables/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Victories")] TeamsTable teamsTable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(teamsTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(teamsTable);
        }

        // GET: TeamsTables/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeamsTable teamsTable = db.TeamsTables.Find(id);
            if (teamsTable == null)
            {
                return HttpNotFound();
            }
            return View(teamsTable);
        }

        // POST: TeamsTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TeamsTable teamsTable = db.TeamsTables.Find(id);
            db.TeamsTables.Remove(teamsTable);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
