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
    public class ResidentViewController : Controller
    {
        private void PopulateHouseholds(string selected)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50129/api/");

                var task = client.GetAsync("household");
                task.Wait();

                if (task.Result.IsSuccessStatusCode)
                {
                    var read = task.Result.Content.ReadAsAsync<IList<HouseholdViewModel>>();
                    read.Wait();

                    ViewBag.Households = new SelectList(
                        read.Result, "HouseholdID", "HouseholdNo", selected);
                }
            }
        }

        public ActionResult Index()
        {
            IEnumerable<ResidentViewModel> resi = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50129/api/"); // Change the portal number
                //HTTP GET
                var responseTask = client.GetAsync("resident");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<ResidentViewModel>>();
                    readTask.Wait();

                    resi = readTask.Result;
                }
                else // web api sent error response
                {
                    //log response status here

                    resi = Enumerable.Empty<ResidentViewModel>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(resi);
        }

        public ActionResult Details(string id)
        {
            ResidentViewModel resi = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50129/api/");
                //HTTP GET
                var responseTask = client.GetAsync("resident?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ResidentViewModel>();
                    readTask.Wait();

                    resi = readTask.Result;
                }
            }

            return View(resi);
        }

        public ActionResult Create()
        {
            ResidentViewModel resident = new ResidentViewModel();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50129/api/");

                // Get existing residents
                var responseTask = client.GetAsync("resident");
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<ResidentViewModel>>();
                    readTask.Wait();

                    var residents = readTask.Result;

                    int maxNumber = 0;

                    foreach (var r in residents)
                    {
                        int number;

                        if (!String.IsNullOrEmpty(r.ResidentID) &&
                            r.ResidentID.StartsWith("RES-") &&
                            Int32.TryParse(r.ResidentID.Substring(4), out number))
                        {
                            if (number > maxNumber)
                                maxNumber = number;
                        }
                    }

                    resident.ResidentID = "RES-" + (maxNumber + 1).ToString("D3");
                }

                // Get households
                var householdTask = client.GetAsync("household");
                householdTask.Wait();

                var householdResult = householdTask.Result;

                if (householdResult.IsSuccessStatusCode)
                {
                    var householdRead = householdResult.Content
                        .ReadAsAsync<IList<HouseholdViewModel>>();
                    householdRead.Wait();

                    ViewBag.Households = new SelectList(
                        householdRead.Result,
                        "HouseholdID",
                        "HouseholdNo"
                    );
                }
            }

            return View(resident);
        }

        //GET METHOD - CREATE BUTTON ACTION
        [System.Web.Mvc.HttpPost]
        public ActionResult Create(ResidentViewModel resi)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50129/api/");

                var postTask = client.PostAsJsonAsync<ResidentViewModel>("resident", resi);
                postTask.Wait();

                var result = postTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            TempData["Error"] = "Could not save. Please check the details and try again.";
            return RedirectToAction("Create");
        }

        public ActionResult Edit(string id)
        {
            ResidentViewModel resi = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50129/api/");
                //HTTP GET
                var responseTask = client.GetAsync("resident?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ResidentViewModel>();
                    readTask.Wait();

                    resi = readTask.Result;
                }

                // Get households
                var householdTask = client.GetAsync("household");
                householdTask.Wait();

                var householdResult = householdTask.Result;

                if (householdResult.IsSuccessStatusCode)
                {
                    var householdRead = householdResult.Content
                        .ReadAsAsync<IList<HouseholdViewModel>>();
                    householdRead.Wait();

                    ViewBag.Households = new SelectList(
                        householdRead.Result,
                        "HouseholdID",
                        "HouseholdNo"
                    );
                }
            }
            return View(resi);
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Edit(ResidentViewModel resi)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50129/api/");

                //HTTP POST
                var putTask = client.PutAsJsonAsync<ResidentViewModel>("resident", resi);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(resi);
        }

        [AuthorizeRole("Admin")]
        public ActionResult Delete(string id)
        {
            ResidentViewModel resi = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50129/api/");
                //HTTP GET
                var responseTask = client.GetAsync("resident?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ResidentViewModel>();
                    readTask.Wait();

                    resi = readTask.Result;
                }
            }
            return View(resi);
        }

        [AuthorizeRole("Admin")]
        [System.Web.Mvc.HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50129/api/");

                var deleteTask = client.DeleteAsync("resident/" + id);
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
