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
        //
        // GET: /Product/

        public IHttpActionResult GetAllProducts()
        {
            IList<ProductViewModel> products = null;

            using (var ctx = new api_dbEntities())
            {
                products = ctx.products.Select(p => new ProductViewModel()
                {
                    id = p.id,
                    name = p.name,
                    description = p.description,
                    price = p.price,
                    category_id = p.category_id
                }).ToList<ProductViewModel>();
            }
            if (products.Count == 0)
            {
                return NotFound();
            }
            return Ok(products);
        }

        //Read One Method
        public IHttpActionResult GetProductById(int id)
        {
            ProductViewModel prod = null;

            using (var ctx = new api_dbEntities())
            {
                prod = ctx.products.Where(s => s.id == id)
                    .Select(s => new ProductViewModel()
                    {
                        id = s.id,
                        name = s.name,
                        description = s.description,
                        price = s.price,
                        category_id = s.category_id
                    }).FirstOrDefault<ProductViewModel>();
            }
            if (prod == null)
            {
                return NotFound();
            }
            return Ok(prod);
        }

        //Create Product Method
        public IHttpActionResult PostNewProduct(ProductViewModel prod)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid Data");

            using (var ctx = new api_dbEntities())
            {
                ctx.products.Add(new product()
                {
                    id = prod.id,
                    name = prod.name,
                    description = prod.description,
                    price = prod.price,
                    category_id = prod.category_id
                });
                ctx.SaveChanges();
            }
            return Ok();
        }

        //Update Method
        public IHttpActionResult  Put(ProductViewModel prod)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            using (var ctx = new api_dbEntities())
            {
                var existingProduct = ctx.products.Where(s => s.id == prod.id)
                            .FirstOrDefault<product>();

                if (existingProduct != null)
                {
                    existingProduct.name = prod.name;
                    existingProduct.description = prod.description;
                    existingProduct.price = prod.price;
                    existingProduct.category_id = prod.category_id;
                    ctx.SaveChanges();
                }
                else
                {
                    return NotFound();
                }
            }

            return Ok();
        }

        //Delete Method
        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid id");

            using (var ctx = new api_dbEntities())
            {
                var prod = ctx.products
                    .Where(s => s.id == id)
                    .FirstOrDefault();

                ctx.products.Remove(prod);
                ctx.SaveChanges();
            }

            return Ok();
        }
    }
}
