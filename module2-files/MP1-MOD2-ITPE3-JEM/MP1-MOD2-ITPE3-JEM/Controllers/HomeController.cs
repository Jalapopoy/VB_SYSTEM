using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MP1_MOD2_ITPE3_JEM.Controllers
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
            //Variables Declaration
            string strENumJEM = "001";
            string strLNameJEM = "Mallari";
            string strFNameJEM = "John Efraim";
            string strMNameJEM = "Macalino";
            string strIClassJEM = "";
            string strTMDescJEM = "";
            string strTMCodeJEM = "";

            double dblHWorkJEM = 30;
            double dblHOTJEM = 10;
            double dblHLateJEM = 5;
            double dblRPHourJEM = 5000;

            //Process - Concatination and Computation
            string strFullNameJEM = strLNameJEM + ", " + strFNameJEM + " " + strMNameJEM;
            double dblGPayJEM = ((dblHWorkJEM + dblHOTJEM) - dblHLateJEM) * dblRPHourJEM;
            double dblTaxJEM = dblGPayJEM * .10;
            double dblSSSJEM = dblGPayJEM * .11;
            double dblPHealthJEM = dblGPayJEM * .12;
            double dblPIbigJEM = dblGPayJEM * .13;
            double dblNPayJEM = dblGPayJEM - (dblTaxJEM + dblSSSJEM + dblPHealthJEM + dblPIbigJEM);

            //Output - Use ViewBag to Display output in the View
            ViewBag.ENumJEM = strENumJEM;
            ViewBag.FullNameJEM = strFullNameJEM;
            ViewBag.HWorkJEM = dblHWorkJEM;
            ViewBag.HOTJEM = dblHOTJEM;
            ViewBag.HLateJEM = dblHLateJEM;
            ViewBag.RPHourJEM = dblRPHourJEM;
            ViewBag.GPayJEM = dblGPayJEM;
            ViewBag.TaxJEM = dblTaxJEM;
            ViewBag.SSSJEM = dblSSSJEM;
            ViewBag.PHealthJEM = dblPHealthJEM;
            ViewBag.PIbigJEM = dblPIbigJEM;
            ViewBag.NPayJEM = dblNPayJEM;
                    
            //DECISION - Apply If and Else If Statement to determine Ratings
            if (dblNPayJEM < 10957)
            {
                strIClassJEM = "Poor!";
            }
            else if (dblNPayJEM >= 10958 && dblNPayJEM <= 21914)
            {
                strIClassJEM = "Low Income!";
            }
            else if (dblNPayJEM >= 21915 && dblNPayJEM <= 43828)
            {
                strIClassJEM = "Lower Middle!";
            }
            else if (dblNPayJEM >= 43829 && dblNPayJEM <= 76669)
            {
                strIClassJEM = "Middle Income";
            }
            else if (dblNPayJEM >= 76670 && dblNPayJEM <= 131484)
            {
                strIClassJEM = "Upper Middle";
            }
            else if (dblNPayJEM >= 131485 && dblNPayJEM <= 219141)
            {
                strIClassJEM = "Upper But Not Rich";
            }
            else
            {
                strIClassJEM = "Rich";
            }
            ViewBag.IClassJEM = strIClassJEM;

            //DECISION - Apply Switch-Case to display Tax Marital Code
            strTMCodeJEM = "M2";
            switch (strTMCodeJEM)
            {
                case "S":
                    strTMDescJEM = "Single";
                    break;
                case "S1":
                    strTMDescJEM = "Single - 1 Dependent";
                    break;
                case "S2":
                    strTMDescJEM = "Single - 2 Dependent";
                    break;
                case "S3":
                    strTMDescJEM = "Single - 3 Dependent";
                    break;
                case "S4":
                    strTMDescJEM = "Single - 4 Dependent";
                    break;
                case "M":
                    strTMDescJEM = "Married";
                    break;
                case "M1":
                    strTMDescJEM = "Married - 1 Dependent";
                    break;
                case "M2":
                    strTMDescJEM = "Married - 2 Dependent";
                    break;
                case "M3":
                    strTMDescJEM = "Married - 3 Dependent";
                    break;
                case "M4":
                    strTMDescJEM = "Married - 4 Dependent";
                    break;
                case "Z":
                    strTMDescJEM = "No Exemptions";
                    break;
                default:
                    strTMDescJEM = "Unavailable Code";
                    break;
            }
            ViewBag.MTDescJEM = strTMDescJEM;

            return View();
        }
    }
}
