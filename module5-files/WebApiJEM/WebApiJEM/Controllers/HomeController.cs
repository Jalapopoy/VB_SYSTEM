using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using WebApiJEM.Models;
using System.Data;

namespace WebApiJEM.Controllers
{
    public class HomeController : Controller
    {

        api_dbEntities dbConn = new api_dbEntities();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(User U)
        {
            if (ModelState.IsValid)
            {
                var user = dbConn.Users.Where(a => a.UserID.Equals(U.UserID) && a.Password.Equals(U.Password)).FirstOrDefault();
                
                if (user != null)
                {
                    ViewBag.loggedin = user.UserID.ToString();
                    return RedirectToAction("Homepage");
                }
                else
                {
                    ViewBag.Errormessage = "Invalid User ID and Password!";
                    return View("Index");
                }
            }
            return View(U);
        }

        public ActionResult Homepage()
        {
            if (ViewBag.loggedin != null)
            {
                return View();
            }
            else
            {
                return View("Homepage");
            }
        }
    }
}
