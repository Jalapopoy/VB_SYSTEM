using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;

using WebApiJEM.Models;

namespace WebApiJEM.Controllers
{
    public class ProductController : ApiController
    {
        // GET: /Product/
        public IHttpActionResult GetAllProductsJEM()
        {
            IList<ProductViewModel> productsJEM = null;

            using (var ctxJEM = new api_dbEntities())
            {
                productsJEM = ctxJEM.products.Select(p => new ProductViewModel()
                {
                    id = p.id,
                    name = p.name,
                    description = p.description,
                    price = p.price,
                    category_id = p.category_id
                }).ToList<ProductViewModel>();
            }
            if (productsJEM.Count == 0)
            {
                return NotFound();
            }
            return Ok(productsJEM);
        }

        //Read One Method
        public IHttpActionResult GetProductByIdJEM(int id)
        {
            ProductViewModel prodJEM = null;

            using (var ctxJEM = new api_dbEntities())
            {
                prodJEM = ctxJEM.products.Where(s => s.id == id)
                    .Select(s => new ProductViewModel()
                    {
                        id = s.id,
                        name = s.name,
                        description = s.description,
                        price = s.price,
                        category_id = s.category_id
                    }).FirstOrDefault<ProductViewModel>();
            }
            if (prodJEM == null)
            {
                return NotFound();
            }
            return Ok(prodJEM);
        }

        //Create Product Method
        public IHttpActionResult PostNewProductJEM(ProductViewModel prodJEM)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid Data");

            using (var ctxJEM = new api_dbEntities())
            {
                ctxJEM.products.Add(new product()
                {
                    id = prodJEM.id,
                    name = prodJEM.name,
                    description = prodJEM.description,
                    price = prodJEM.price,
                    category_id = prodJEM.category_id
                });
                ctxJEM.SaveChanges();
            }
            return Ok();
        }

        //Update Method
        public IHttpActionResult PutProductJEM(ProductViewModel prodJEM)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            using (var ctxJEM = new api_dbEntities())
            {
                var existingProductJEM = ctxJEM.products.Where(s => s.id == prodJEM.id)
                            .FirstOrDefault<product>();

                if (existingProductJEM != null)
                {
                    existingProductJEM.name = prodJEM.name;
                    existingProductJEM.description = prodJEM.description;
                    existingProductJEM.price = prodJEM.price;
                    existingProductJEM.category_id = prodJEM.category_id;
                    ctxJEM.SaveChanges();
                }
                else
                {
                    return NotFound();
                }
            }

            return Ok();
        }

        //Delete Method
        public IHttpActionResult DeleteProductJEM(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid id");

            using (var ctxJEM = new api_dbEntities())
            {
                var prodJEM = ctxJEM.products
                    .Where(s => s.id == id)
                    .FirstOrDefault();

                ctxJEM.products.Remove(prodJEM);
                ctxJEM.SaveChanges();
            }

            return Ok();
        }
    }
}