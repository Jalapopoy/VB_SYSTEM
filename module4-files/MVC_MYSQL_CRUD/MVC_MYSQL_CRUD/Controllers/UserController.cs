using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_MYSQL_CRUD.Models;
using System.Data;

namespace MVC_MYSQL_CRUD.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/

        dbcrudjemEntities dbConn = new dbcrudjemEntities();

        //1. CREATE VIEW METHOD
        public ActionResult CreateUser()
        {
            return View();
        }

        [HttpPost] //used for posting html data to the server.
        [ValidateAntiForgeryToken] //to prevent cross-site request forgery attacks
        public ActionResult CreateUser(tbluser U)
        {
            if (ModelState.IsValid)
            {
                dbConn.tblusers.Add(U);
                dbConn.SaveChanges();
                ModelState.Clear();
                U = null;
                return RedirectToAction("ReadAllUser"); // ViewUser will be created later
            }

            return View();
        }
        public ActionResult ReadAllUser()
        {
            var users = dbConn.tblusers.ToList();
            return View(users);
        }

        //3. READ ONE or DETAILS VIEW METHOD
        public ActionResult ReadOneUser(string id)
        {
            return View(dbConn.tblusers.Where(x => x.UserID == id).FirstOrDefault());
        }

        //4. UPDATE or EDIT VIEW
        public ActionResult UpdateUser(string id)
        {
            return View(dbConn.tblusers.Where(x => x.UserID == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult UpdateUser(string id, tbluser user)
        {
            try
            {
                dbConn.Entry(user).State = EntityState.Modified;
                dbConn.SaveChanges();
                return RedirectToAction("ReadAllUser");
            }
            catch
            {
                return View();
            }
        }

        //5. DELETE VIEW
        public ActionResult DeleteUser(string id)
        {
            return View(dbConn.tblusers.Where(x => x.UserID == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult DeleteUser(string id, FormCollection collection)
        {
            try
            {
                tbluser user = dbConn.tblusers.Where(x => x.UserID == id).FirstOrDefault();
                dbConn.tblusers.Remove(user);
                dbConn.SaveChanges();

                return RedirectToAction("ReadAllUser");
            }
            catch
            {
                return View();
            }
        }
    }
}
