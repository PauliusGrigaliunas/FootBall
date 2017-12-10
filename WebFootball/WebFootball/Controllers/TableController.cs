using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using WebFootball.Models;
using Football;


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
            TeamController ctn = new TeamController();
            var list1 = ctn.Get().ToList();
      
            list = list1.Select(i=>new Team {
                Id = i.Id,
                Name = i.Name,
                Victories = (int)i.Victories
            }).ToList();
           

            return View(list);
        }



    }
}