using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using MP1_MOD4_ITPE3_JEM.Models;

namespace MP1_MOD4_ITPE3_JEM.Controllers
{
    public class EmployeeController : Controller
    {
        //
        // GET: /Employee/
        dbcrudEntities dbConn = new dbcrudEntities();

        //1. CREATE VIEW METHOD
        public ActionResult CreateEmployee()
        {
            return View();
        }

        [HttpPost] //used for posting html data to the server.
        public ActionResult CreateEmployee(Employee E)
        {
            if (ModelState.IsValid)
            {
                dbConn.Employees.Add(E);
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
            var employees = dbConn.Employees.ToList();
            return View(employees);
        }

        //3. READ ONE or DETAILS VIEW METHOD
        public ActionResult ReadOneEmployee(string id)
        {
            return View(dbConn.Employees.Where(x => x.EmployeeID == id).FirstOrDefault());
        }

        //4. UPDATE or EDIT VIEW
        public ActionResult UpdateEmployee(string id)
        {
            return View(dbConn.Employees.Where(x => x.EmployeeID == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult UpdateEmployee(string id, Employee Employee)
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
            return View(dbConn.Employees.Where(x => x.EmployeeID == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult DeleteEmployee(string id, FormCollection collection)
        {
            try
            {
                Employee employee = dbConn.Employees.Where(x => x.EmployeeID == id).FirstOrDefault();
                dbConn.Employees.Remove(employee);
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