using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PRE_EXAM_ITPE3_JEM.Controllers
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
        [HttpGet]
        public ActionResult PaySlipJEM()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PaySlipJEM(double dblNHWorkJEM, double dblNHOTJEM, double dblNHLateJEM, double dblRPHourJEM)
        {
            double dblGPayJEM = ((dblNHWorkJEM + dblNHOTJEM) - dblNHLateJEM) * dblRPHourJEM;
            double dblTaxJEM = dblGPayJEM * .10;
            double dblSSSJEM = dblGPayJEM * .12;
            double dblPIbigJEM = dblGPayJEM * .14;
            double dblPHealthJEM = dblGPayJEM * .16;
            double dblTDeducJEM = dblTaxJEM + dblSSSJEM + dblPIbigJEM + dblPHealthJEM;
            double dblNPayJEM = dblGPayJEM - dblTDeducJEM;

            ViewBag.HWorkJEM = dblNHWorkJEM;
            ViewBag.HOTJEM = dblNHOTJEM;
            ViewBag.HLateJEM = dblNHLateJEM;
            ViewBag.RPHourJEM = dblRPHourJEM;

            ViewBag.GPayJEM = dblGPayJEM;
            ViewBag.TaxJEM = dblTaxJEM;
            ViewBag.SSSJEM = dblSSSJEM;
            ViewBag.PIbigJEM = dblPIbigJEM;
            ViewBag.PHealthJEM = dblPHealthJEM;
            ViewBag.TDeducJEM = dblTDeducJEM;
            ViewBag.NPayJEM = dblNPayJEM;

            ViewBag.IsSubmitted = true;

            return View();
        }
    }
}
