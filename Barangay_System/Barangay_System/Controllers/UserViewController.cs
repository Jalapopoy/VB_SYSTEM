using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using Barangay_System.Models;
using Barangay_System.Filters;

namespace Barangay_System.Controllers
{
    [AuthorizeRole("Admin")]
    public class UserViewController : Controller
    {
        public ActionResult Index()
        {
            IEnumerable<UserViewModel> user = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50129/api/"); // Change the portal number
                //HTTP GET
                var responseTask = client.GetAsync("user");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<UserViewModel>>();
                    readTask.Wait();

                    user = readTask.Result;
                }
                else // web api sent error response
                {
                    //log response status here

                    user = Enumerable.Empty<UserViewModel>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(user);
        }

        public ActionResult Details(string id)
        {
            UserViewModel user = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50129/api/");
                //HTTP GET
                var responseTask = client.GetAsync("user?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<UserViewModel>();
                    readTask.Wait();

                    user = readTask.Result;
                }
            }

            return View(user);
        }

        private void PopulateResidents(string selected)
        {
            IList<ResidentViewModel> residents = new List<ResidentViewModel>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50129/api/");

                var task = client.GetAsync("resident");
                task.Wait();

                if (task.Result.IsSuccessStatusCode)
                {
                    var read = task.Result.Content.ReadAsAsync<IList<ResidentViewModel>>();
                    read.Wait();
                    residents = read.Result;
                }
            }

            ViewBag.Residents = new SelectList(
                residents.Select(r => new
                {
                    ResidentID = r.ResidentID,
                    FullName = r.FirstName + " " + r.LastName
                }),
                "ResidentID", "FullName", selected);
        }

        public ActionResult Create()
        {
            UserViewModel user = new UserViewModel();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50129/api/");

                var responseTask = client.GetAsync("user");
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<UserViewModel>>();
                    readTask.Wait();

                    int maxNumber = 0;

                    foreach (var u in readTask.Result)
                    {
                        int number;

                        if (!String.IsNullOrEmpty(u.UserID) &&
                            u.UserID.StartsWith("USR-") &&
                            Int32.TryParse(u.UserID.Substring(4), out number))
                        {
                            if (number > maxNumber)
                                maxNumber = number;
                        }
                    }

                    user.UserID = "USR-" + (maxNumber + 1).ToString("D3");
                }
                else
                {
                    user.UserID = "USR-001";
                }
            }

            PopulateResidents(user.ResidentID);
            return View(user);
        }
        //GET METHOD - CREATE BUTTON ACTION
        [System.Web.Mvc.HttpPost]
        public ActionResult Create(UserViewModel user)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50129/api/");

                var postTask = client.PostAsJsonAsync<UserViewModel>("user", user);
                postTask.Wait();

                var result = postTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View("Create", user);
        }

        public ActionResult Edit(string id)
        {
            UserViewModel user = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50129/api/");
                //HTTP GET
                var responseTask = client.GetAsync("user?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<UserViewModel>();
                    readTask.Wait();

                    user = readTask.Result;
                }
            }
            return View(user);
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Edit(UserViewModel user)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50129/api/");

                //HTTP POST
                var putTask = client.PutAsJsonAsync<UserViewModel>("user", user);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(user);
        }

        public ActionResult Delete(string id)
        {
            UserViewModel user = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50129/api/");
                //HTTP GET
                var responseTask = client.GetAsync("user?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<UserViewModel>();
                    readTask.Wait();

                    user = readTask.Result;
                }
            }
            return View(user);
        }

        [AuthorizeRole("Admin")]
        [System.Web.Mvc.HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            if (id == Convert.ToString(Session["UserID"]))
            {
                TempData["Error"] = "You cannot delete the account you are signed in with.";
                return RedirectToAction("Index");
            }
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50129/api/");

                var deleteTask = client.DeleteAsync("household/" + id);
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                TempData["Error"] = "This record cannot be deleted because other records use it.";
                return RedirectToAction("Index");
            }
        }

    }
}
