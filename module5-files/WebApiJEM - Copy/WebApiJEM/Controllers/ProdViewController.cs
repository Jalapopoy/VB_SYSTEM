using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;

using WebApiJEM.Models;

namespace WebApiJEM.Controllers
{
    public class ProdViewController : Controller
    {
        //
        // GET: /ProdView/

        public ActionResult Index()
        {
            IEnumerable<ProductViewModel> products = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(
                    "https://pyawtbruh-001-site1.ktempurl.com/api/");

                var response = client.GetAsync("product").Result;

                if (response.IsSuccessStatusCode)
                {
                    products = response.Content
                        .ReadAsAsync<IList<ProductViewModel>>().Result;
                }
                else
                {
                    products = Enumerable.Empty<ProductViewModel>();
                }
            }

            return View(products);
        }


        public ActionResult ViewProduct(int id)
        {
            ProductViewModel product = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(
                    "https://pyawtbruh-001-site1.ktempurl.com/api/");

                var response = client.GetAsync("product?id=" + id).Result;

                if (response.IsSuccessStatusCode)
                {
                    product = response.Content
                        .ReadAsAsync<ProductViewModel>().Result;
                }
            }

            return View(product);
        }

        public ActionResult CreateProduct()
        {
            return View();
        }

        //GET METHOD - Create Button Action
        [System.Web.Mvc.HttpPost]
        public ActionResult CreateProduct(ProductViewModel product)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(
                    "https://pyawtbruh-001-site1.ktempurl.com/api/");

                var postTask = client.PostAsJsonAsync("product", product);
                postTask.Wait();

                var result = postTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("", "Failed to create product.");
                return View(product);
            }
        }

        //EDIT
        public ActionResult EditProduct(int id)
        {
            ProductViewModel product = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(
                    "https://pyawtbruh-001-site1.ktempurl.com/api/");

                var response = client.GetAsync("product?id=" + id).Result;

                if (response.IsSuccessStatusCode)
                {
                    product = response.Content
                        .ReadAsAsync<ProductViewModel>().Result;
                }
            }

            return View(product);
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult EditProduct(ProductViewModel product)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(
                    "https://pyawtbruh-001-site1.ktempurl.com/api/");

                var putTask = client.PutAsJsonAsync("product", product);
                putTask.Wait();

                var result = putTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("", "Failed to update product.");
                return View(product);
            }
        }

        //DELETE
        public ActionResult DeleteProduct(int id)
        {
            ProductViewModel product = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(
                    "https://pyawtbruh-001-site1.ktempurl.com/api/");

                var response = client.GetAsync("product?id=" + id).Result;

                if (response.IsSuccessStatusCode)
                {
                    product = response.Content
                        .ReadAsAsync<ProductViewModel>().Result;
                }
            }

            return View(product);
        }


        [System.Web.Mvc.HttpPost]
        public ActionResult DeleteProduct(int id, FormCollection collection)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(
                    "https://pyawtbruh-001-site1.ktempurl.com/api/");

                var deleteTask = client.DeleteAsync("product/" + id);
                deleteTask.Wait();

                var result = deleteTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError("", "Failed to delete product.");
            return RedirectToAction("Index");
        }
    }
}
