using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;

using WebApiJEM.Models;

namespace WebApiJEM.Controllers
{
    public class CatViewController : Controller
    {
        //
        // GET: /CatView/

        public ActionResult Index()
        {
            IEnumerable<CategoryViewModel> categories = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://pyawtbruh-001-site1.ktempurl.com/api/"); // change the portal number
                //HTTP Get
                var responseTask = client.GetAsync("category");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<CategoryViewModel>>();
                    readTask.Wait();

                    categories = readTask.Result;
                }

                else //web api sent error message
                {
                    //log response status here

                    categories = Enumerable.Empty<CategoryViewModel>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");

                }
            }
            return View(categories);
        }

        public ActionResult ViewCategory(int id)
        {
            CategoryViewModel category = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://pyawtbruh-001-site1.ktempurl.com/api/");
                //HTTP GET
                var responseTask = client.GetAsync("category?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<CategoryViewModel>();
                    readTask.Wait();

                    category = readTask.Result;
                }
            }
            return View(category);
        }

        public ActionResult CreateCategory()
        {
            return View();
        }

        //GET METHOD - Create Button Action
        [System.Web.Mvc.HttpPost]
        public ActionResult CreateCategory(CategoryViewModel category)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://pyawtbruh-001-site1.ktempurl.com/api/category");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<CategoryViewModel>("category", category);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

                return View(category);
            }
        }

        //EDIT
        public ActionResult EditCategory(int id)
        {
            CategoryViewModel category = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://pyawtbruh-001-site1.ktempurl.com/api/");
                //HTTP GET
                var responseTask = client.GetAsync("category?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<CategoryViewModel>();
                    readTask.Wait();

                    category = readTask.Result;
                }
            }
            return View(category);
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult EditCategory(CategoryViewModel category)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://pyawtbruh-001-site1.ktempurl.com/api/category");

                //HTTP POST 
                var putTask = client.PutAsJsonAsync<CategoryViewModel>("category", category);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(category);
        }

        //DELETE
        public ActionResult DeleteCategory(int id)
        {
            CategoryViewModel category = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://pyawtbruh-001-site1.ktempurl.com/api/");
                //HTTP GET
                var responseTask = client.GetAsync("category?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<CategoryViewModel>();
                    readTask.Wait();

                    category = readTask.Result;
                }
            }
            return View(category);
        }


        [HttpPost]
        public ActionResult DeleteCategory(int id, FormCollection collection)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://pyawtbruh-001-site1.ktempurl.com/api/");

                var deleteTask = client.DeleteAsync("category/" + id.ToString());
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }
    }
}
