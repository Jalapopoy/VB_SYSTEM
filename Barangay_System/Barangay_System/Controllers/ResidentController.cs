using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using Barangay_System.Models;

namespace Barangay_System.Controllers
{
    public class ResidentController : Controller
    {
        //
        // GET: /Resident/

        db_barangay_systemEntities dbConn = new db_barangay_systemEntities();

        //1. CREATE VIEW METHOD
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost] //used for posting html data to the server.
        public ActionResult Create(tblresident R)
        {
            if (ModelState.IsValid)
            {
                dbConn.tblresidents.Add(R);
                dbConn.SaveChanges();
                ModelState.Clear();
                R = null;
                return RedirectToAction("Index"); // ViewUser will be created later
            }

            return View();
        }
        public ActionResult Index()
        {
            var residents = dbConn.tblresidents.ToList();
            return View(residents);
        }

        //3. READ ONE or DETAILS VIEW METHOD
        public ActionResult Details(string id)
        {
            return View(dbConn.tblresidents.Where(x => x.ResidentID == id).FirstOrDefault());
        }

        //4. UPDATE or EDIT VIEW
        public ActionResult Edit(string id)
        {
            return View(dbConn.tblresidents.Where(x => x.ResidentID == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult Edit(string id, tblresident resident)
        {
            try
            {
                dbConn.Entry(resident).State = EntityState.Modified;
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
            return View(dbConn.tblusers.Where(x => x.ResidentID == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                tblresident resident = dbConn.tblresidents.Where(x => x.ResidentID == id).FirstOrDefault();
                dbConn.tblresidents.Remove(resident);
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
