using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MP2_MOD4_ITPE3_JEM.Models;
using System.Data;

namespace MP2_MOD4_ITPE3_JEM.Controllers
{
    public class EmployeeController : Controller
    {
        //
        // GET: /Employee/
        dbcrudjemEntities dbConn = new dbcrudjemEntities();

        //1. CREATE VIEW METHOD
        public ActionResult CreateEmployee()
        {
            return View();
        }

        [HttpPost] //used for posting html data to the server.
        public ActionResult CreateEmployee(tblemployee E)
        {
            if (ModelState.IsValid)
            {
                dbConn.tblemployees.Add(E);
                dbConn.SaveChanges();
                ModelState.Clear();
                E = null;
                return RedirectToAction("ReadAllEmployee");
            }

            return View();
        }
        //2. READ ALL or LISTVIEW METHOD
        public ActionResult ReadAllEmployee()
        {
            var employees = dbConn.tblemployees.ToList();
            return View(employees);
        }

        //3. READ ONE or DETAILS VIEW METHOD
        public ActionResult ReadOneEmployee(string id)
        {
            return View(dbConn.tblemployees.Where(x => x.EmployeeID == id).FirstOrDefault());
        }

        //4. UPDATE or EDIT VIEW
        public ActionResult UpdateEmployee(string id)
        {
            return View(dbConn.tblemployees.Where(x => x.EmployeeID == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult UpdateEmployee(string id, tblemployee Employee)
        {
            try
            {
                dbConn.Entry(Employee).State = EntityState.Modified;
                dbConn.SaveChanges();
                return RedirectToAction("ReadAllEmployee");
            }
            catch
            {
                return View();
            }
        }

        //5. DELETE VIEW
        public ActionResult DeleteEmployee(string id)
        {
            return View(dbConn.tblemployees.Where(x => x.EmployeeID == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult DeleteEmployee(string id, FormCollection collection)
        {
            try
            {
                tblemployee employee = dbConn.tblemployees.Where(x => x.EmployeeID == id).FirstOrDefault();
                dbConn.tblemployees.Remove(employee);
                dbConn.SaveChanges();

                return RedirectToAction("ReadAllEmployee");
            }
            catch
            {
                return View();
            }
        }

    }
}
