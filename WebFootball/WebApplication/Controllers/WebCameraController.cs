using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication.Controllers
{
    public class WebCameraController : Controller
    {
        // GET: WebCamera
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}
