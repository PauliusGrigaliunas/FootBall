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
    public class GameTablesController : Controller
    {
        private FootballEntities db = new FootballEntities();

        // GET: GameTables
        public ActionResult Index()
        {
            var gameTables = db.GameTables.Include(g => g.TeamsTable).Include(g => g.TeamsTable1);
            

           

            return View(gameTables.OrderByDescending(i => i.FirstTeamScore).OrderByDescending(i => i.SecondTeamScore).ToList());
        }

        // GET: GameTables/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GameTable gameTable = db.GameTables.Find(id);
            if (gameTable == null)
            {
                return HttpNotFound();
            }
            return View(gameTable);
        }

        // GET: GameTables/Create
        public ActionResult Create()
        {
            ViewBag.FirstTeam = new SelectList(db.TeamsTables, "Id", "Name");
            ViewBag.SecondTeam = new SelectList(db.TeamsTables, "Id", "Name");
            return View();
        }

        // POST: GameTables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstTeam,SecondTeam,FirstTeamScore,SecondTeamScore")] GameTable gameTable)
        {
            if (ModelState.IsValid)
            {
                db.GameTables.Add(gameTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FirstTeam = new SelectList(db.TeamsTables, "Id", "Name", gameTable.FirstTeam);
            ViewBag.SecondTeam = new SelectList(db.TeamsTables, "Id", "Name", gameTable.SecondTeam);
            return View(gameTable);
        }

        // GET: GameTables/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GameTable gameTable = db.GameTables.Find(id);
            if (gameTable == null)
            {
                return HttpNotFound();
            }
            ViewBag.FirstTeam = new SelectList(db.TeamsTables, "Id", "Name", gameTable.FirstTeam);
            ViewBag.SecondTeam = new SelectList(db.TeamsTables, "Id", "Name", gameTable.SecondTeam);
            return View(gameTable);
        }

        // POST: GameTables/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstTeam,SecondTeam,FirstTeamScore,SecondTeamScore")] GameTable gameTable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gameTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FirstTeam = new SelectList(db.TeamsTables, "Id", "Name", gameTable.FirstTeam);
            ViewBag.SecondTeam = new SelectList(db.TeamsTables, "Id", "Name", gameTable.SecondTeam);
            return View(gameTable);
        }

        // GET: GameTables/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GameTable gameTable = db.GameTables.Find(id);
            if (gameTable == null)
            {
                return HttpNotFound();
            }
            return View(gameTable);
        }

        // POST: GameTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GameTable gameTable = db.GameTables.Find(id);
            db.GameTables.Remove(gameTable);
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
