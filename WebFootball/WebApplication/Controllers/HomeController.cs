using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Title = "Information Page";

            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
        public ActionResult Start()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
