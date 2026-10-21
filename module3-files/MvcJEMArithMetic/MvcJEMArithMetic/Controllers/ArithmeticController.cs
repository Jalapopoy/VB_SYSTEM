using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcJEMArithMetic.Models;

namespace MvcJEMArithMetic.Controllers
{
    public class ArithmeticController : Controller
    {
        //
        // GET: /Arithmetic/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GradingSystem()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GradingSystem(Grade G)
        {
            //1.Get and Convert Inputs from Models
            var preJEM = Convert.ToDouble(G.PGradeJEM.ToString());
            var midJEM = Convert.ToDouble(G.MGradeJEM.ToString());
            var finJEM = Convert.ToDouble(G.FGradeJEM.ToString());

            //2.Compute for Subject Grade
            var sgradeJEM = (preJEM + midJEM + finJEM) / 3;

            //3.Store the result in ViewBag
            ViewBag.Grade = sgradeJEM.ToString();

            return View();
        }
    }
}
