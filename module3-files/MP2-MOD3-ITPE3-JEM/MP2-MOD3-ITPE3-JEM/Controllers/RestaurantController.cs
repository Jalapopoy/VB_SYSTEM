using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MP2_MOD3_ITPE3_JEM.Models;

namespace MP2_MOD3_ITPE3_JEM.Controllers
{
    public class RestaurantController : Controller
    {
        //
        // GET: /Customer/

        public ActionResult Billing()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Billing(RestaurantBill R)
        {
            var CNumJEM = R.CNumJEM.ToString();
            var LNameJEM = R.LNameJEM.ToString();
            var FNameJEM = R.FNameJEM.ToString();
            var CTypeJEM = R.CTypeJEM.ToString();
            var TAmountJEM = R.TAmountJEM.ToString();

            var TChargesJEM = Convert.ToDouble(TAmountJEM) * .10;
            var SChargesJEM = Convert.ToDouble(TAmountJEM) * .03;
            var TotChargesJEM = Convert.ToDouble(TChargesJEM) + Convert.ToDouble(SChargesJEM);
            var TDiscountJEM = Convert.ToDouble(TAmountJEM) * .05;
            var APayJEM = (Convert.ToDouble(TAmountJEM) + Convert.ToDouble(TotChargesJEM)) - Convert.ToDouble(TDiscountJEM);

            ViewBag.CNumJEM = CNumJEM;
            ViewBag.LNameJEM = LNameJEM;
            ViewBag.FNameJEM = FNameJEM;
            ViewBag.CTypeJEM = CTypeJEM;
            ViewBag.TAmountJEM = TAmountJEM;
            ViewBag.TChargesJEM = TChargesJEM;
            ViewBag.SChargesJEM = SChargesJEM;
            ViewBag.TotChargesJEM = TotChargesJEM;
            ViewBag.TDiscountJEM = TDiscountJEM;
            ViewBag.APayJEM = APayJEM;

            return View();
        }
    }
}
