using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Web.Mvc;
using WebApiJEM.Models;

namespace WebApiJEM.Controllers
{
    public class ProdViewController : Controller
    {
        //
        // GET: /ProdView/
        public ActionResult Index()
        {
            IEnumerable<ProductViewModel> productsJEM = null;

            using (var clientJEM = new HttpClient())
            {
                clientJEM.BaseAddress = new Uri("http://localhost:50015/api/"); // Change the portal number
                //HTTP GET
                var responseTaskJEM = clientJEM.GetAsync("product");
                responseTaskJEM.Wait();

                var resultJEM = responseTaskJEM.Result;
                if (resultJEM.IsSuccessStatusCode)
                {
                    var readTaskJEM = resultJEM.Content.ReadAsAsync<IList<ProductViewModel>>();
                    readTaskJEM.Wait();

                    productsJEM = readTaskJEM.Result;
                }
                else // web api sent error response
                {
                    //log response status here

                    productsJEM = Enumerable.Empty<ProductViewModel>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(productsJEM);
        }

        public ActionResult ViewProduct(int id)
        {
            ProductViewModel productJEM = null;

            using (var clientJEM = new HttpClient())
            {
                clientJEM.BaseAddress = new Uri("http://localhost:50015/api/");
                //HTTP GET
                var responseTaskJEM = clientJEM.GetAsync("product?id=" + id.ToString());
                responseTaskJEM.Wait();

                var resultJEM = responseTaskJEM.Result;
                if (resultJEM.IsSuccessStatusCode)
                {
                    var readTaskJEM = resultJEM.Content.ReadAsAsync<ProductViewModel>();
                    responseTaskJEM.Wait();

                    productJEM = readTaskJEM.Result;
                }
            }

            return View(productJEM);
        }
        public ActionResult CreateProduct()
        {
            return View();
        }
        //GET METHOD - CREATE BUTTON ACTION
        [System.Web.Mvc.HttpPost]
        public ActionResult CreateProduct(ProductViewModel productJEM)
        {
            using (var clientJEM = new HttpClient())
            {
                clientJEM.BaseAddress = new Uri("http://localhost:50015/api/product/");

                //HTTP POST
                var postTaskJEM = clientJEM.PostAsJsonAsync<ProductViewModel>("product", productJEM);
                postTaskJEM.Wait();

                var resultJEM = postTaskJEM.Result;
                if (resultJEM.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(productJEM);
        }

        public ActionResult EditProduct(int id)
        {
            ProductViewModel productJEM = null;

            using (var clientJEM = new HttpClient())
            {
                clientJEM.BaseAddress = new Uri("http://localhost:50015/api/");
                //HTTP GET
                var responseTaskJEM = clientJEM.GetAsync("product?id=" + id.ToString());
                responseTaskJEM.Wait();

                var resultJEM = responseTaskJEM.Result;
                if (resultJEM.IsSuccessStatusCode)
                {
                    var readTaskJEM = resultJEM.Content.ReadAsAsync<ProductViewModel>();
                    readTaskJEM.Wait();

                    productJEM = readTaskJEM.Result;
                }
            }
            return View(productJEM);
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult EditProduct(ProductViewModel productJEM)
        {
            using (var clientJEM = new HttpClient())
            {
                clientJEM.BaseAddress = new Uri("http://localhost:50015/api/product");

                //HTTP POST
                var putTaskJEM = clientJEM.PutAsJsonAsync<ProductViewModel>("product", productJEM);
                putTaskJEM.Wait();

                var resultJEM = putTaskJEM.Result;
                if (resultJEM.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(productJEM);
        }

        public ActionResult DeleteProduct(int id)
        {
            ProductViewModel productJEM = null;

            using (var clientJEM = new HttpClient())
            {
                clientJEM.BaseAddress = new Uri("http://localhost:50015/api/");
                //HTTP GET
                var responseTaskJEM = clientJEM.GetAsync("product?id=" + id.ToString());
                responseTaskJEM.Wait();

                var resultJEM = responseTaskJEM.Result;
                if (resultJEM.IsSuccessStatusCode)
                {
                    var readTaskJEM = resultJEM.Content.ReadAsAsync<ProductViewModel>();
                    readTaskJEM.Wait();

                    productJEM = readTaskJEM.Result;
                }
            }
            return View(productJEM);
        }
        [System.Web.Mvc.HttpPost]
        public ActionResult DeleteProduct(int id, FormCollection collectionJEM)
        {
            using (var clientJEM = new HttpClient())
            {
                clientJEM.BaseAddress = new Uri("http://localhost:50015/api/");

                var deleteTaskJEM = clientJEM.DeleteAsync("product/" + id.ToString());

                var resultJEM = deleteTaskJEM.Result;
                if (resultJEM.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View();
        }
    }
}