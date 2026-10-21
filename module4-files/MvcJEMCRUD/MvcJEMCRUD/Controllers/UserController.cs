using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//imports
using MvcJEMCRUD.Models;
using System.Data;

namespace MvcJEMCRUD.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/

        //Entity Connection - Database and Table Connection
        dbcrudEntities dbConn = new dbcrudEntities();

        //1. CREATE VIEW METHOD
        public ActionResult CreateUser()
        {
            return View();
        }

        [HttpPost] //used for posting html data to the server.
        [ValidateAntiForgeryToken] //to prevent cross-site request forgery attacks
        public ActionResult CreateUser(User U)
        {
            if (ModelState.IsValid)
            {
                dbConn.Users.Add(U);
                dbConn.SaveChanges();
                ModelState.Clear();
                U = null;
                return RedirectToAction("ReadAllUser"); // ViewUser will be created later
            }

            return View();
        }
        //2. READ ALL or LISTVIEW METHOD
        public ActionResult ReadAllUser()
        {
            var users = dbConn.Users.ToList();
            return View(users);
        }

        //3. READ ONE or DETAILS VIEW METHOD
        public ActionResult ReadOneUser(string id)
        {
            return View(dbConn.Users.Where(x => x.UserID == id).FirstOrDefault());
        }

        //4. UPDATE or EDIT VIEW
        public ActionResult UpdateUser(string id)
        {
            return View(dbConn.Users.Where(x => x.UserID == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult UpdateUser(string id, User user)
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
            return View(dbConn.Users.Where(x => x.UserID == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult DeleteUser(string id, FormCollection collection)
        {
            try
            {
                User user = dbConn.Users.Where(x => x.UserID == id).FirstOrDefault();
                dbConn.Users.Remove(user);
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
