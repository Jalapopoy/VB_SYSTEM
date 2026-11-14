using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using Barangay_System.Models;

namespace Barangay_System.Controllers
{
    public class ResidentViewController : Controller
    {
        //
        // GET: /ReadView/

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
            return View();
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

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View("Create", resi);
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
        [System.Web.Mvc.HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50129/api/");

                var deleteTask = client.DeleteAsync("resident/" + id.ToString());
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View();
        }

    }
}
