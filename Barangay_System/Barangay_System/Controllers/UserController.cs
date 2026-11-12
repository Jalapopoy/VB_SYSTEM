using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Barangay_System.Models;
using System.Data;

namespace Barangay_System.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/

        db_barangay_systemEntities dbConn = new db_barangay_systemEntities();

        //1. CREATE VIEW METHOD
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost] //used for posting html data to the server.
        [ValidateAntiForgeryToken]
        public ActionResult Create(tbluser U)
        {
            if (ModelState.IsValid)
            {
                U.created_at = DateTime.Now;
                dbConn.tblusers.Add(U);
                dbConn.SaveChanges();
                ModelState.Clear();
                U = null;
                return RedirectToAction("Index"); // ViewUser will be created later
            }

            return View();
        }
        public ActionResult Index()
        {
            var users = dbConn.tblusers.ToList();
            return View(users);
        }

        //3. READ ONE or DETAILS VIEW METHOD
        public ActionResult Details(string id)
        {
            return View(dbConn.tblusers.Where(x => x.UserID == id).FirstOrDefault());
        }

        //4. UPDATE or EDIT VIEW
        public ActionResult Edit(string id)
        {
            return View(dbConn.tblusers.Where(x => x.UserID == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult Edit(string id, tbluser user)
        {
            try
            {
                dbConn.Entry(user).State = EntityState.Modified;
                dbConn.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //5. DELETE VIEW
        public ActionResult Delete(string id)
        {
            return View(dbConn.tblusers.Where(x => x.UserID == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                tbluser user = dbConn.tblusers.Where(x => x.UserID == id).FirstOrDefault();
                dbConn.tblusers.Remove(user);
                dbConn.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

    }
}
