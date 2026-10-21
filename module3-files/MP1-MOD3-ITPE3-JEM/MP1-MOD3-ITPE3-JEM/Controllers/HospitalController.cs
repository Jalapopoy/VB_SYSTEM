using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MP1_MOD3_ITPE3_JEM.Models;

namespace MP1_MOD3_ITPE3_JEM.Controllers
{
    public class HospitalController : Controller
    {
        //
        // GET: /Hospital/

        public ActionResult Billing()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Billing(HospitalBill H)
        {
            var PNumJEM = H.PNumJEM.ToString();
            var LNameJEM = H.LNameJEM.ToString();
            var FNameJEM = H.FNameJEM.ToString();
            var AgeJEM = Convert.ToDouble(H.AgeJEM.ToString());
            var RTypeJEM = H.RTypeJEM.ToString();

            var RPDayJEM = Convert.ToDouble(H.RPDayJEM.ToString());
            var NDaysJEM = Convert.ToDouble(H.NDaysJEM.ToString());
            var LChargesJEM = Convert.ToDouble(H.LChargesJEM.ToString());
            var EChargesJEM = Convert.ToDouble(H.EChargesJEM.ToString());

            var RChargeJEM = RPDayJEM * NDaysJEM;
            var TAmountJEM = RChargeJEM + LChargesJEM + EChargesJEM;
            var HInsuranceJEM = TAmountJEM * 0.05;
            var PHealthJEM = TAmountJEM * 0.06;
            var SSSJEM = TAmountJEM * 0.07;
            var TDiscountJEM = HInsuranceJEM + PHealthJEM + SSSJEM;
            var APayJEM = TAmountJEM - TDiscountJEM;
                    
            ViewBag.RCharge = RChargeJEM.ToString();
            ViewBag.TAmount = TAmountJEM.ToString();
            ViewBag.HInsurance = HInsuranceJEM.ToString();
            ViewBag.PHealth = PHealthJEM.ToString();
            ViewBag.SSS = SSSJEM.ToString();
            ViewBag.TDiscount = TDiscountJEM.ToString();
            ViewBag.APay = APayJEM.ToString();

            return View(H);
        }

    }
}
