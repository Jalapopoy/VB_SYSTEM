using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Barangay_System.Models;
using System.Net.Http;
using Barangay_System.Filters;

namespace Barangay_System.Controllers
{
    [AuthorizeRole("Admin", "Staff")]
    public class DocumentTypeViewController : Controller
    {
        public ActionResult Index()
        {
            IEnumerable<DocumentTypeViewModel> doc = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50129/api/"); // Change the portal number
                //HTTP GET
                var responseTask = client.GetAsync("documenttype");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<DocumentTypeViewModel>>();
                    readTask.Wait();

                    doc = readTask.Result;
                }
                else // web api sent error response
                {
                    //log response status here

                    doc = Enumerable.Empty<DocumentTypeViewModel>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(doc);
        }

        public ActionResult Details(string id)
        {
            DocumentTypeViewModel doc = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50129/api/");
                //HTTP GET
                var responseTask = client.GetAsync("documenttype?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<DocumentTypeViewModel>();
                    readTask.Wait();

                    doc = readTask.Result;
                }
            }

            return View(doc);
        }

        //GET METHOD - CREATE BUTTON ACTION
        public ActionResult Create()
        {
            DocumentTypeViewModel doc = new DocumentTypeViewModel();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50129/api/");

                var responseTask = client.GetAsync("documenttype");
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<DocumentTypeViewModel>>();
                    readTask.Wait();

                    var docs = readTask.Result;

                    int maxNumber = 0;

                    foreach (var d in docs)
                    {
                        int number;

                        if (!String.IsNullOrEmpty(d.DocTypeID) &&
                            d.DocTypeID.StartsWith("DOC-") &&
                            Int32.TryParse(d.DocTypeID.Substring(4), out number))
                        {
                            if (number > maxNumber)
                                maxNumber = number;
                        }
                    }

                    doc.DocTypeID = "DOC-" + (maxNumber + 1).ToString("D3");
                }
                else
                {
                    doc.DocTypeID = "DOC-001";
                }
            }

            return View(doc);
        }

        //GET METHOD - CREATE BUTTON ACTION
        [System.Web.Mvc.HttpPost]
        public ActionResult Create(DocumentTypeViewModel doctype)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50129/api/");

                var postTask = client.PostAsJsonAsync<DocumentTypeViewModel>("documenttype", doctype);
                postTask.Wait();

                var result = postTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View("Create", doctype);
        }

        [AuthorizeRole("Admin")]
        public ActionResult Edit(string id)
        {
            DocumentTypeViewModel doc = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50129/api/");

                var responseTask = client.GetAsync("documenttype?id=" + id);
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<DocumentTypeViewModel>();
                    readTask.Wait();

                    doc = readTask.Result;
                }
            }

            return View(doc);
        }

        [AuthorizeRole("Admin")]
        [HttpPost]
        public ActionResult Edit(DocumentTypeViewModel doc)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50129/api/");

                var putTask = client.PutAsJsonAsync("documenttype", doc);
                putTask.Wait();

                var result = putTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(doc);
        }

        [AuthorizeRole("Admin")]
        public ActionResult Delete(string id)
        {
            DocumentTypeViewModel doc = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50129/api/");
                //HTTP GET
                var responseTask = client.GetAsync("documenttype?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<DocumentTypeViewModel>();
                    readTask.Wait();

                    doc = readTask.Result;
                }
            }
            return View(doc);
        }
        [AuthorizeRole("Admin")]
        [System.Web.Mvc.HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50129/api/");

                var deleteTask = client.DeleteAsync("documenttype/" + id);
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

