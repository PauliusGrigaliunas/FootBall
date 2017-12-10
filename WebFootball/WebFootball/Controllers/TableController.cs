using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using WebFootball.Models;


namespace WebFootball.Controllers
{
    public class TableController:Controller
    {
        //GET : BestTeams
 
       public void getList()
        {
            

        }
        public ActionResult BestTeams()
        {


            var table = new Team
            {
                Id = 1,
                Name = "sss",
                Victories = 1
                
            };
            var table1 = new Team
            {
                Id = 2,
                Name = "aaa",
                Victories=2

            };
            List<Team> list = new List<Team>();
            list.Add(table);
            list.Add(table1);
            ViewBag.Id = list.Select(i => i.Id).ToList();
            ViewBag.Team1 = list.Select(i => i.Name).ToList();
            ViewBag.Team1 = list.Select(i => i.Victories).ToList();


            return View(list);
        }



    }
}