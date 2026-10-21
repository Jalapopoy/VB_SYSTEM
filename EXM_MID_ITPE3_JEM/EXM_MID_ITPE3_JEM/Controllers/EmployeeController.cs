using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EXM_MID_ITPE3_JEM.Models;
using System.Data;

namespace EXM_MID_CODE_INITIALS.Controllers
{
    public class EmployeeController : Controller
    {
        //
        // GET: /Employee/
        db_exam_itpe3_jemEntities dbConn = new db_exam_itpe3_jemEntities();

        // Shared computation logic used by Create and Update
        private void ComputePayroll(tblemployee E)
        {
            E.GrossPay = ((E.HoursWork + E.HoursOvertime) - E.HoursLate) * E.RatePerHour;
            E.Tax = (int)(E.GrossPay * .10);
            E.SSS = (int)(E.GrossPay * .12);
            E.PagIbig = (int)(E.GrossPay * .14);
            E.PhilHealth = (int)(E.GrossPay * .16);
            E.TotalDeduction = E.Tax + E.SSS + E.PagIbig + E.PhilHealth;
            E.NetPay = E.GrossPay - E.TotalDeduction;
        }

        //1. CREATE VIEW METHOD
        public ActionResult CreateEmployee()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateEmployee(tblemployee E)
        {
            if (ModelState.IsValid)
            {
                ComputePayroll(E);
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
            return View(dbConn.tblemployees.Where(x => x.EmployeeNumber == id).FirstOrDefault());
        }

        //4. UPDATE or EDIT VIEW
        public ActionResult UpdateEmployee(string id)
        {
            return View(dbConn.tblemployees.Where(x => x.EmployeeNumber == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult UpdateEmployee(string id, tblemployee Employee)
        {
            try
            {
                ComputePayroll(Employee);
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
            return View(dbConn.tblemployees.Where(x => x.EmployeeNumber == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult DeleteEmployee(string id, FormCollection collection)
        {
            try
            {
                tblemployee employee = dbConn.tblemployees.Where(x => x.EmployeeNumber == id).FirstOrDefault();
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