using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ACT1_MOD2_ITPE3_JEM.Controllers
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
        public ActionResult Decision()
        {
            string strLNameJEM = "Di Magkamali";
            string strFNameJEM = "Perfecto";
            string strMNameJEM = "Mahusay";
            string strRemarksJEM = "";
            string strRatingsJEM = "";
            string strCorsCodeJEM = "";
            string strCorsDescJEM = "";

            double dblPreJEM = 80;
            double dblMidJEM = 85;
            double dblFinJEM = 90;

            string strFullNameJEM = strLNameJEM + ", " + strFNameJEM + " " + strMNameJEM;
            double dblSGradeJEM = (dblPreJEM + dblMidJEM + dblFinJEM) / 3;

            ViewBag.FullNameJEM = strFullNameJEM;
            ViewBag.PrelimJEM = dblPreJEM;
            ViewBag.MidtermJEM = dblMidJEM;
            ViewBag.FinalJEM = dblFinJEM;
            ViewBag.SGradeJEM = dblSGradeJEM;

            if (dblSGradeJEM > 74)
            {
                strRemarksJEM = "Passed!";
            }
            else
            {
                strRemarksJEM = "Failed!";
            }
            ViewBag.RemarksJEM = strRemarksJEM;

            if (dblSGradeJEM >= 98 && dblSGradeJEM <= 100)
            {
                strRatingsJEM = "Excellent!";
            }
            else if (dblSGradeJEM >= 92 && dblSGradeJEM <= 98)
            {
                strRatingsJEM = "Very Good!";
            }
            else if (dblSGradeJEM >= 83 && dblSGradeJEM <= 92)
            {
                strRatingsJEM = "Good!";
            }
            else if (dblSGradeJEM >= 78 && dblSGradeJEM <= 83)
            {
                strRatingsJEM = "Fair!";
            }
            else if (dblSGradeJEM >= 75 && dblSGradeJEM <= 78)
            {
                strRatingsJEM = "Satisfactory!";
            }
            else
            {
                strRatingsJEM = "Poor!";
            }
            ViewBag.RatingsJEM = strRatingsJEM;

            strCorsCodeJEM = "BSIT";
            switch (strCorsCodeJEM)
            {
                case "BSCS":
                    strCorsDescJEM = "Bachelor of Science in Computer Science";
                    break;
                case "BSIT":
                    strCorsDescJEM = "Bachelor of Science in Information Technology";
                    break;
                case "BSIS":
                    strCorsDescJEM = "Bachelor of Science in Information Systems";
                    break;
            }
            ViewBag.CourseJEM = strCorsDescJEM;

            return View();
        }
    }
}
