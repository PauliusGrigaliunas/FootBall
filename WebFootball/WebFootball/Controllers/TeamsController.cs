﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebFootball.Models;

namespace WebFootball.Controllers
{
    public class TeamsController : Controller
    {
        // GET: Teams
        public ActionResult First()
        {
            var teams = new Teams()
            {
                Name = "AAAA"
            };
            return View();
        }
    }
}