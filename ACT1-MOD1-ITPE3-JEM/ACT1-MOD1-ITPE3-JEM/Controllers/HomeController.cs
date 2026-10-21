using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ACT1_MOD1_ITPE3_JEM.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult MyInfo()
        {
            ViewBag.Name = "My Name is Perfecti Di Magkamali and I am 21 years old!";
            ViewBag.Address = "I am currently residing at Magliman City of San Fernando, Pampanga.";
            ViewBag.Motto = "My motto in life is Practice Makes Perfect but nobody's perfect, so why practice";
            ViewBag.Course = "I am taking up Bachelor of Science in Information Technology at City College of San Fernando Pampanga";

            return View();
        }
    }
}
