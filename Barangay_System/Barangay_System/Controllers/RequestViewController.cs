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
    public class RequestViewController : Controller
    {
        [HttpPost]
        public ActionResult ChangeStatus(string id, string newStatus, string remarks)
        {
            RequestViewModel req = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50129/api/");

                // 1. get the request
                var getTask = client.GetAsync("request?id=" + id);
                getTask.Wait();

                if (getTask.Result.IsSuccessStatusCode)
                {
                    var readTask = getTask.Result.Content.ReadAsAsync<RequestViewModel>();
                    readTask.Wait();
                    req = readTask.Result;
                }

                if (req == null)
                {
                    TempData["Error"] = "Request not found.";
                    return RedirectToAction("Index");
                }

                // 2. only allow the steps of the workflow
                bool allowed =
                    (req.Status == "Pending" && (newStatus == "Approved" || newStatus == "Rejected")) ||
                    (req.Status == "Approved" && (newStatus == "Released" || newStatus == "Rejected"));

                if (!allowed)
                {
                    TempData["Error"] = "That status change is not allowed.";
                    return RedirectToAction("Index");
                }

                // 3. a request can be released only after it is paid
                if (newStatus == "Released")
                {
                    var payTask = client.GetAsync("payment");
                    payTask.Wait();

                    bool paid = false;
                    if (payTask.Result.IsSuccessStatusCode)
                    {
                        var payRead = payTask.Result.Content.ReadAsAsync<IList<PaymentViewModel>>();
                        payRead.Wait();
                        paid = payRead.Result.Any(p => p.RequestID == id && p.Status == "Paid");
                    }

                    if (!paid)
                    {
                        TempData["Error"] = "Record the payment first before releasing " + id + ".";
                        return RedirectToAction("Index");
                    }
                }

                // 4. save the new status
                req.Status = newStatus;
                req.ProcessedBy = Convert.ToString(Session["UserID"]);

                if (newStatus == "Rejected" && !string.IsNullOrWhiteSpace(remarks))
                {
                    req.Remarks = remarks;
                }

                var putTask = client.PutAsJsonAsync<RequestViewModel>("request", req);
                putTask.Wait();

                if (putTask.Result.IsSuccessStatusCode)
                    TempData["Message"] = id + " is now " + newStatus + ".";
                else
                    TempData["Error"] = "Could not update the request.";
            }

            return RedirectToAction("Index");
        }

        public ActionResult Index()
        {
            IEnumerable<RequestViewModel> req = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50129/api/"); // Change the portal number
                //HTTP GET
                var responseTask = client.GetAsync("request");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<RequestViewModel>>();
                    readTask.Wait();

                    req = readTask.Result;
                }
                else // web api sent error response
                {
                    //log response status here

                    req = Enumerable.Empty<RequestViewModel>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(req);
        }

        public ActionResult Details(string id)
        {
            RequestViewModel req = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50129/api/");
                //HTTP GET
                var responseTask = client.GetAsync("request?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<RequestViewModel>();
                    readTask.Wait();

                    req = readTask.Result;
                }
            }

            return View(req);
        }
        public ActionResult Create()
        {
            RequestViewModel req = new RequestViewModel();

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50129/api/");

                // Get all requests to generate the next RequestID
                var response = client.GetAsync("request").Result;

                if (response.IsSuccessStatusCode)
                {
                    var requests = response.Content.ReadAsAsync<IList<RequestViewModel>>().Result;

                    int maxID = 0;

                    foreach (var r in requests)
                    {
                        int number;
                        if (!string.IsNullOrEmpty(r.RequestID) && r.RequestID.StartsWith("REQ-") &&
                            Int32.TryParse(r.RequestID.Substring(4), out number))
                        {
                            if (number > maxID)
                                maxID = number;
                        }
                    }

                    req.RequestID = "REQ-" + (maxID + 1).ToString("D3");
                }
                else
                {
                    req.RequestID = "REQ-001";
                }

                // Get all residents
                var residentResponse = client.GetAsync("resident").Result;

                if (residentResponse.IsSuccessStatusCode)
                {
                    var residents = residentResponse.Content
                        .ReadAsAsync<IList<ResidentViewModel>>().Result;

                    ViewBag.Residents = new SelectList(
                        residents.Select(r => new
                        {
                            ResidentID = r.ResidentID,
                            FullName = r.FirstName + " " + r.MiddleName + " " + r.LastName
                        }),
                        "ResidentID",
                        "FullName"
                    );
                }

                // Get all document types
                var docResponse = client.GetAsync("documenttype").Result;

                if (docResponse.IsSuccessStatusCode)
                {
                    var docTypes = docResponse.Content
                        .ReadAsAsync<IList<DocumentTypeViewModel>>().Result;

                    ViewBag.DocTypes = new SelectList(
                        docTypes,
                        "DocTypeID",
                        "Name"
                    );
                }
            }

            return View(req);
        }
        //GET METHOD - CREATE BUTTON ACTION
        [System.Web.Mvc.HttpPost]
        public ActionResult Create(RequestViewModel req)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50129/api/");

                var postTask = client.PostAsJsonAsync<RequestViewModel>("request", req);
                postTask.Wait();

                var result = postTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                if (req.Status != "Pending" && req.Status != "Approved")
                {
                    TempData["Error"] = "Finished requests cannot be edited.";
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
            PopulateRequestDropdowns(req);
            return View("Create", req);
        }

        private void PopulateRequestDropdowns(RequestViewModel req)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50129/api/");

                // Get Residents
                var residentResponse = client.GetAsync("resident").Result;

                IList<ResidentViewModel> residents =
                    new List<ResidentViewModel>();

                if (residentResponse.IsSuccessStatusCode)
                {
                    residents = residentResponse.Content
                        .ReadAsAsync<IList<ResidentViewModel>>().Result;
                }

                ViewBag.Residents = new SelectList(
                    residents.Select(r => new
                    {
                        ResidentID = r.ResidentID,
                        FullName = r.FirstName + " " +
                                   r.MiddleName + " " + r.LastName
                    }),
                    "ResidentID",
                    "FullName",
                    req.ResidentID
                );

                // Get Document Types
                var docResponse = client.GetAsync("documenttype").Result;

                IList<DocumentTypeViewModel> docTypes =
                    new List<DocumentTypeViewModel>();

                if (docResponse.IsSuccessStatusCode)
                {
                    docTypes = docResponse.Content
                        .ReadAsAsync<IList<DocumentTypeViewModel>>().Result;
                }

                ViewBag.DocTypes = new SelectList(
                    docTypes,
                    "DocTypeID",
                    "Name",
                    req.DocTypeID
                );
            }
        }

        public ActionResult Edit(string id)
        {
            RequestViewModel req = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50129/api/");

                var response = client.GetAsync("request?id=" + id).Result;

                if (response.IsSuccessStatusCode)
                {
                    req = response.Content.ReadAsAsync<RequestViewModel>().Result;
                }
            }

            if (req == null)
            {
                return HttpNotFound();
            }

            PopulateRequestDropdowns(req);

            return View(req);
        }


        [System.Web.Mvc.HttpPost]
        public ActionResult Edit(RequestViewModel req)
        {
            if (req.Status != "Pending" &&
            string.IsNullOrEmpty(req.ProcessedBy))
                    {
                        req.ProcessedBy = Convert.ToString(Session["UserID"]);
                    }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50129/api/");

                var putTask = client.PutAsJsonAsync<RequestViewModel>("request", req);
                putTask.Wait();

                var result = putTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            PopulateRequestDropdowns(req);
            return View(req);
        }

        [AuthorizeRole("Admin")]
        public ActionResult Delete(string id)
        {
            RequestViewModel req = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50129/api/");
                //HTTP GET
                var responseTask = client.GetAsync("request?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<RequestViewModel>();
                    readTask.Wait();

                    req = readTask.Result;
                }
            }
            return View(req);
        }

        [AuthorizeRole("Admin")]
        [System.Web.Mvc.HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50129/api/");

                var deleteTask = client.DeleteAsync("request/" + id);
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
