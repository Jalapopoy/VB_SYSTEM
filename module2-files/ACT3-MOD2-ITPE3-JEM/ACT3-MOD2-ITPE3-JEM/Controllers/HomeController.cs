using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ACT3_MOD2_ITPE3_JEM.Controllers
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
        public ActionResult ArrayLoopsJEM()
        {
            string[] arrLNameJEM = {"Di Magkamali", "Di Magilap", "Di Matulac", "Di Mahanap", "Di Mapakali"};
            string[] arrFNameJEM = {"Perfecto", "Hassan", "Dabiana", "Ditolado", "Albereto"};
            string[] arrLevelJEM = { "1st Year", "2nd Year", "3rd Year", "4th Year", "5th Year" };
            string[] arrCourseJEM = { "BSIT", "BSCS", "BSIS", "BSED", "BSHM" };

            double[] arrPreJEM = { 90, 91, 92, 93, 94 };
            double[] arrMidJEM = { 80, 81, 82, 83, 84 };
            double[] arrFinJEM = { 75, 80, 85, 90, 95 };

            ViewBag.LastNamesJEM = arrLNameJEM;
            ViewBag.FirstNamesJEM = arrFNameJEM;
            ViewBag.YearLevelsJEM = arrLevelJEM;
            ViewBag.CoursesJEM = arrCourseJEM;
            ViewBag.PreGradesJEM = arrPreJEM;
            ViewBag.MidGradesJEM = arrMidJEM;
            ViewBag.FinGradesJEM = arrFinJEM;

            return View();
        }
    }
}
