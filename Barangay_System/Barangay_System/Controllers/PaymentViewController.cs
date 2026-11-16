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
    public class PaymentViewController : Controller
    {
        public ActionResult Index()
        {
            IEnumerable<PaymentViewModel> pay = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50129/api/"); // Change the portal number
                //HTTP GET
                var responseTask = client.GetAsync("payment");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<PaymentViewModel>>();
                    readTask.Wait();

                    pay = readTask.Result;
                }
                else // web api sent error response
                {
                    //log response status here

                    pay = Enumerable.Empty<PaymentViewModel>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(pay);
        }

        public ActionResult Details(string id)
        {
            PaymentViewModel pay = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50129/api/");
                //HTTP GET
                var responseTask = client.GetAsync("payment?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<PaymentViewModel>();
                    readTask.Wait();

                    pay = readTask.Result;
                }
                if (pay == null)
                {
                    return HttpNotFound();
                }
            }

            return View(pay);
        }


        private void PopulateRequestDropdown()
        {
            List<RequestViewModel> requests = new List<RequestViewModel>();
            List<DocumentTypeViewModel> docTypes = new List<DocumentTypeViewModel>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50129/api/");

                // Get approved requests
                var responseTask = client.GetAsync("request");
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<RequestViewModel>>();
                    readTask.Wait();

                    requests = readTask.Result.ToList();
                }

                // Get document types and their fees
                var docTask = client.GetAsync("documenttype");
                docTask.Wait();

                var docResult = docTask.Result;

                if (docResult.IsSuccessStatusCode)
                {
                    var docReadTask = docResult.Content.ReadAsAsync<IList<DocumentTypeViewModel>>();
                    docReadTask.Wait();

                    docTypes = docReadTask.Result.ToList();
                }
            }

            var approvedRequests = requests
                .Where(r => r.Status == "Approved")
                .Select(r =>
                {
                    var doc = docTypes.FirstOrDefault(d => d.DocTypeID == r.DocTypeID);

                    return new SelectListItem
                    {
                        Value = r.RequestID,
                        Text = r.RequestID + " - " + r.Purpose,
                        // Store the document fee in the option
                        // We'll use this value in JavaScript
                    };
                })
                .ToList();

            ViewBag.RequestFees = requests
            .Where(r => r.Status == "Approved")
            .Select(r => new
            {
                RequestID = r.RequestID,
                Fee = docTypes.FirstOrDefault(d => d.DocTypeID == r.DocTypeID) == null
                    ? 0
                    : docTypes.FirstOrDefault(d => d.DocTypeID == r.DocTypeID).Fee
            })
            .ToList();
            ViewBag.Requests = approvedRequests;
        }

        public ActionResult Create()
        {
            PaymentViewModel pay = new PaymentViewModel();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50129/api/");

                var responseTask = client.GetAsync("payment");
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<PaymentViewModel>>();
                    readTask.Wait();

                    int maxOR = 0;

                    foreach (var p in readTask.Result)
                    {
                        int number;

                        if (!string.IsNullOrEmpty(p.ORNumber) &&
                            p.ORNumber.StartsWith("OR-") &&
                            Int32.TryParse(p.ORNumber.Substring(3), out number))
                        {
                            if (number > maxOR)
                                maxOR = number;
                        }
                    }

                    pay.ORNumber = "OR-" + (maxOR + 1).ToString("D6");
                }
                else
                {
                    pay.ORNumber = "OR-000001";
                }
            }

            PopulateRequestDropdown();
            return View(pay);
        }

        //GET METHOD - CREATE BUTTON ACTION
        [System.Web.Mvc.HttpPost]
        public ActionResult Create(PaymentViewModel pay)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50129/api/");

                var postTask = client.PostAsJsonAsync<PaymentViewModel>("payment", pay);
                postTask.Wait();

                var result = postTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                var errorTask = result.Content.ReadAsStringAsync();
                errorTask.Wait();

                string errorMessage = errorTask.Result;

                try
                {
                    var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

                    var errorData = serializer.Deserialize<Dictionary<string, string>>(errorMessage);

                    if (errorData.ContainsKey("Message"))
                    {
                        errorMessage = errorData["Message"];
                    }
                }
                catch
                {
                    // Keep the original error message
                }

                ModelState.AddModelError(string.Empty, errorMessage);

                PopulateRequestDropdown();

                return View("Create", pay);
            }
        }

        public ActionResult Edit(string id)
        {
            PaymentViewModel pay = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50129/api/");
                //HTTP GET
                var responseTask = client.GetAsync("payment?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<PaymentViewModel>();
                    readTask.Wait();

                    pay = readTask.Result;
                }
            }
            return View(pay);
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Edit(PaymentViewModel pay)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50129/api/");

                //HTTP POST
                var putTask = client.PutAsJsonAsync<PaymentViewModel>("payment", pay);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(pay);
        }

        [AuthorizeRole("Admin")]
        public ActionResult Delete(string id)
        {
            PaymentViewModel pay = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50129/api/");
                //HTTP GET
                var responseTask = client.GetAsync("payment?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<PaymentViewModel>();
                    readTask.Wait();

                    pay = readTask.Result;
                }
            }
            return View(pay);
        }

        [AuthorizeRole("Admin")]
        [System.Web.Mvc.HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50129/api/");

                var deleteTask = client.DeleteAsync("payment/" + id);
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
