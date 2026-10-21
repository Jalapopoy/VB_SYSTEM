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
    public class StudentController : Controller
    {
        //
        // GET: /Student/

        //Entity Connection - Database and Table Connection
        dbcrudEntities dbConn = new dbcrudEntities();

        //1. CREATE VIEW METHOD
        public ActionResult CreateStudent()
        {
            return View();
        }

        [HttpPost] //used for posting html data to the server.
        public ActionResult CreateStudent(Student S)
        {
            if (ModelState.IsValid)
            {
                dbConn.Students.Add(S);
                dbConn.SaveChanges();
                ModelState.Clear();
                S = null;
                return RedirectToAction("ReadAllStudent"); 
            }

            return View();
        }
        //READ ALL or LISTVIEW METHOD
        public ActionResult ReadAllStudent()
        {
            var students = dbConn.Students.ToList();
            return View(students);
        }

        //3. READ ONE or DETAILS VIEW METHOD
        public ActionResult ReadOneStudent(string id)
        {
            return View(dbConn.Students.Where(x => x.StudentID == id).FirstOrDefault());
        }

        //4. UPDATE or EDIT VIEW
        public ActionResult UpdateStudent(string id)
        {
            return View(dbConn.Students.Where(x => x.StudentID == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult UpdateStudent(string id, Student Student)
        {
            try
            {
                dbConn.Entry(Student).State = EntityState.Modified;
                dbConn.SaveChanges();
                return RedirectToAction("ReadAllStudent");
            }
            catch
            {
                return View();
            }
        }

        //5. DELETE VIEW
        public ActionResult DeleteStudent(string id)
        {
            return View(dbConn.Students.Where(x => x.StudentID == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult DeleteStudent(string id, FormCollection collection)
        {
            try
            {
                Student student = dbConn.Students.Where(x => x.StudentID == id).FirstOrDefault();
                dbConn.Students.Remove(student);
                dbConn.SaveChanges();

                return RedirectToAction("ReadAllStudent");
            }
            catch
            {
                return View();
            }
        }
    }
}
