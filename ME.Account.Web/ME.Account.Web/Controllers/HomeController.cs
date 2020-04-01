using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ME.Account.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "This is just a demo.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "You can contact if you want to...";

            return View();
        }
    }
}