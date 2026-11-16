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
    [AuthorizeRole("Admin", "Staff")]
    public class HouseholdViewController : Controller
    {
        public ActionResult Index()
        {
            IEnumerable<HouseholdViewModel> house = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50129/api/"); // Change the portal number
                //HTTP GET
                var responseTask = client.GetAsync("household");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<HouseholdViewModel>>();
                    readTask.Wait();

                    house = readTask.Result;
                }
                else // web api sent error response
                {
                    //log response status here

                    house = Enumerable.Empty<HouseholdViewModel>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(house);
        }

        public ActionResult Details(string id)
        {
            HouseholdViewModel house = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50129/api/");
                //HTTP GET
                var responseTask = client.GetAsync("household?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<HouseholdViewModel>();
                    readTask.Wait();

                    house = readTask.Result;
                }
            }

            return View(house);
        }


        public ActionResult Create()
        {
            HouseholdViewModel house = new HouseholdViewModel();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50129/api/");

                var responseTask = client.GetAsync("household");
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<HouseholdViewModel>>();
                    readTask.Wait();

                    var households = readTask.Result;

                    int maxNumber = 0;

                    foreach (var h in households)
                    {
                        int number;

                        if (!String.IsNullOrEmpty(h.HouseholdID) &&
                            h.HouseholdID.StartsWith("HH-") &&
                            Int32.TryParse(h.HouseholdID.Substring(3), out number))
                        {
                            if (number > maxNumber)
                                maxNumber = number;
                        }
                    }

                    house.HouseholdID = "HH-" + (maxNumber + 1).ToString("D3");
                }
            }

            return View(house);
        }


        //GET METHOD - CREATE BUTTON ACTION
        [System.Web.Mvc.HttpPost]
        public ActionResult Create(HouseholdViewModel house)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50129/api/");

                var postTask = client.PostAsJsonAsync<HouseholdViewModel>("household", house);
                postTask.Wait();

                var result = postTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View("Create", house);
        }

        public ActionResult Edit(string id)
        {
            HouseholdViewModel house = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50129/api/");

                var responseTask = client.GetAsync("household?id=" + id);
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<HouseholdViewModel>();
                    readTask.Wait();

                    house = readTask.Result;
                }
            }

            return View(house);
        }

        [HttpPost]
        public ActionResult Edit(HouseholdViewModel house)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50129/api/");

                var putTask = client.PutAsJsonAsync("household", house);
                putTask.Wait();

                var result = putTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(house);
        }

        [AuthorizeRole("Admin")]
        public ActionResult Delete(string id)
        {
            HouseholdViewModel house = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50129/api/");
                //HTTP GET
                var responseTask = client.GetAsync("household?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<HouseholdViewModel>();
                    readTask.Wait();

                    house = readTask.Result;
                }
            }
            return View(house);
        }

        [AuthorizeRole("Admin")]
        [System.Web.Mvc.HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
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
