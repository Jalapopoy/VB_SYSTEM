using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_MYSQL_CRUD.Models;


namespace MVC_MYSQL_CRUD.Controllers
{
    public class HomeController : Controller
    {
        //Database Connectivity
        dbcrudjemEntities dbConnJEM = new dbcrudjemEntities();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(tbluser userJEM)
        {
            var loginUserJEM = dbConnJEM.tblusers.Where(a => a.UserID.Equals(userJEM.UserID) && a.Password.Equals(userJEM.Password)).FirstOrDefault();
            if (loginUserJEM != null)
            {
                ViewBag.loggedin = loginUserJEM.UserID.ToString();
                return View("HomePage");
            }
            else
            {
                ViewBag.ErrorMessage = "Invalid User ID and Password!";
                return View("Index");
            }
        }

        public ActionResult HomePage()
        {
            if (ViewBag.loggedin != null)
            {
                return View();
            }
            else
            {
                return View("HomePage");
            }
        }

    }
}