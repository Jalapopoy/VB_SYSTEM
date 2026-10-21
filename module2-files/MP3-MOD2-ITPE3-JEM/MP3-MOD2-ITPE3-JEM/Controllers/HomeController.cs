using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MP3_MOD2_ITPE3_JEM.Controllers
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
        public ActionResult Payroll()
        {
            string[] arrENumJEM = { "001", "002", "003", "004", "005" };
            string[] arrLNameJEM = { "Di Magkamali", "Di Magilap", "Di Matulac", "Di Mapacali", "Di Mapasaya" };
            string[] arrFNameJEM = { "Perfecto", "Hassan", "Dabiana", "Alberuto", "Sadner" };
            string[] arrMTCodeJEM = { "S1", "S1", "S1", "S1", "S1" };

            double[] arrHWorkJEM = { 20, 25, 30, 35, 40 };
            double[] arrHOTJEM = { 10, 12, 14, 16, 20 };
            double[] arrHLateJEM = { 2, 5, 7, 9, 11 };
            double[] arrRPHourJEM = { 150, 200, 250, 300, 350 };
            double[] arrTDeducJEM = { 500, 550, 600, 650, 700 };

            ViewBag.ENumJEM = arrENumJEM;
            ViewBag.LNameJEM = arrLNameJEM;
            ViewBag.FNameJEM = arrFNameJEM;
            ViewBag.MTCodeJEM = arrMTCodeJEM;
            ViewBag.HWorkJEM = arrHWorkJEM;
            ViewBag.HOTJEM = arrHOTJEM;
            ViewBag.HLateJEM = arrHLateJEM;
            ViewBag.RPHourJEM = arrRPHourJEM;
            ViewBag.TDeducJEM = arrTDeducJEM;

            return View();
        }
    }
}
