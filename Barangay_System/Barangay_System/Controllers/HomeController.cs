using System;
using System.Linq;
using System.Web.Mvc;
using Barangay_System.Models;
using Barangay_System.Filters;

namespace Barangay_System.Controllers
{
    public class HomeController : Controller
    {
        [AuthorizeRole("Admin", "Staff")]
        public ActionResult Homepage()
        {
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(tbluser u)
        {
            using (var db = new db_barangay_systemEntities())
            {
                var user = db.tblusers.FirstOrDefault(x => x.Username == u.Username && x.Password == u.Password);

                if (user == null)
                {
                    ViewBag.Message = "Invalid username or password";
                    return View();
                }
                if (!string.Equals(user.Status, "Active", StringComparison.OrdinalIgnoreCase))
                {
                    ViewBag.Message = "This account is not active.";
                    return View();
                }
                if (string.Equals(user.Role, "Resident", StringComparison.OrdinalIgnoreCase))
                {
                    ViewBag.Message = "Resident accounts use the resident client application.";
                    return View();
                }

                Session["UserID"] = user.UserID;
                Session["Username"] = user.Username;
                Session["Role"] = user.Role;
                return RedirectToAction("Homepage");
            }
        }

        public ActionResult AccessDenied()
        {
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index");
        }
    }
}