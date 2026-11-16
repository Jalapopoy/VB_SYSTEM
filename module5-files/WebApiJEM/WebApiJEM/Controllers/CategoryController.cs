using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using WebApiJEM.Models;

namespace WebApiJEM.Controllers
{
    public class CategoryController : ApiController
    {
        // GET: /Category/

        public IHttpActionResult GetAllCategoriesJEM()
        {
            IList<CategoryViewModel> categoriesJEM = null;

            using (var ctxJEM = new api_dbEntities())
            {
                categoriesJEM = ctxJEM.categories.Select(p => new CategoryViewModel()
                {
                    id = p.id,
                    name = p.name,
                    description = p.description
                }).ToList<CategoryViewModel>();
            }
            if (categoriesJEM.Count == 0)
            {
                return NotFound();
            }
            return Ok(categoriesJEM);
        }

        //Read One Method
        public IHttpActionResult GetCategoryByIdJEM(int id)
        {
            CategoryViewModel catJEM = null;

            using (var ctxJEM = new api_dbEntities())
            {
                catJEM = ctxJEM.categories.Where(s => s.id == id)
                    .Select(s => new CategoryViewModel()
                    {
                        id = s.id,
                        name = s.name,
                        description = s.description
                    }).FirstOrDefault<CategoryViewModel>();
            }
            if (catJEM == null)
            {
                return NotFound();
            }
            return Ok(catJEM);
        }

        //Create Category Method
        public IHttpActionResult PostNewCategoryJEM(CategoryViewModel catJEM)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid Data");

            using (var ctxJEM = new api_dbEntities())
            {
                ctxJEM.categories.Add(new category()
                {
                    id = catJEM.id,
                    name = catJEM.name,
                    description = catJEM.description
                });
                ctxJEM.SaveChanges();
            }
            return Ok();
        }

        //Update Method
        public IHttpActionResult PutCategoryJEM(CategoryViewModel catJEM)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            using (var ctxJEM = new api_dbEntities())
            {
                var existingCategoryJEM = ctxJEM.categories.Where(s => s.id == catJEM.id)
                            .FirstOrDefault<category>();

                if (existingCategoryJEM != null)
                {
                    existingCategoryJEM.name = catJEM.name;
                    existingCategoryJEM.description = catJEM.description;
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
        public IHttpActionResult DeleteCategoryJEM(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid id");

            using (var ctxJEM = new api_dbEntities())
            {
                var catJEM = ctxJEM.categories
                    .Where(s => s.id == id)
                    .FirstOrDefault();

                ctxJEM.categories.Remove(catJEM);
                ctxJEM.SaveChanges();
            }

            return Ok();
        }
    }
}