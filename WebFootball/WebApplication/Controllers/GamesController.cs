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
    public class GamesController : Controller
    {
        private FootballEntities3 db = new FootballEntities3();

        // GET: Games
        public ActionResult Index()
        {
            var games = db.Games.Include(g => g.Team).Include(g => g.Team1);
            return View(games.OrderByDescending(i=>i.FirstTeamScore).
                OrderByDescending(i=>i.SecondTeamScore).ToList());
        }

        // GET: Games/Details/5
        public ActionResult Details()
        {
            var games = db.Games.Include(g => g.Team).Include(g => g.Team1);


            return View(games.OrderByDescending(i => i.Id).ToList());
        }
        public ActionResult GetTeam()
        {
            var games = db.Games.Include(g => g.Team).Include(g => g.Team1);


            return View(games.OrderByDescending(i => i.Id).Last());
        }

        // GET: Games/Create
        public ActionResult Create()
        {
            ViewBag.FirstTeam = new SelectList(db.Teams, "Id", "Name");
            ViewBag.SecondTeam = new SelectList(db.Teams, "Id", "Name");
            return View();
        }

        // POST: Games/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstTeam,SecondTeam,FirstTeamScore,SecondTeamScore,Date,IsFinished")] Game game)
        {
            if (ModelState.IsValid)
            {
                db.Games.Add(game);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FirstTeam = new SelectList(db.Teams, "Id", "Name", game.FirstTeam);
            ViewBag.SecondTeam = new SelectList(db.Teams, "Id", "Name", game.SecondTeam);
            return View(game);
        }

        // GET: Games/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            ViewBag.FirstTeam = new SelectList(db.Teams, "Id", "Name", game.FirstTeam);
            ViewBag.SecondTeam = new SelectList(db.Teams, "Id", "Name", game.SecondTeam);
            return View(game);
        }

        // POST: Games/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstTeam,SecondTeam,FirstTeamScore,SecondTeamScore,Date,IsFinished")] Game game)
        {
            if (ModelState.IsValid)
            {
                db.Entry(game).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FirstTeam = new SelectList(db.Teams, "Id", "Name", game.FirstTeam);
            ViewBag.SecondTeam = new SelectList(db.Teams, "Id", "Name", game.SecondTeam);
            return View(game);
        }

        // GET: Games/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Game game = db.Games.Find(id);
            db.Games.Remove(game);
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
