using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Net.Http;

using WebApiJEM.Models;

namespace WebApiJEM.Controllers
{
    public class CategoryController : ApiController
    {
        //
        // GET: /Category/

        public IHttpActionResult GetAllCategory()
        {
            IList<CategoryViewModel> categories = null;

            using (var ctx = new api_dbEntities())
            {
                categories = ctx.categories.Select(c => new CategoryViewModel()
                {
                    id = c.id,
                    name = c.name,
                    description = c.description
                }).ToList<CategoryViewModel>();
            }
            if (categories.Count == 0)
            {
                return NotFound();
            }
            return Ok(categories);
        }

        //Read One Method
        public IHttpActionResult GetCategoryById(int id)
        {
            CategoryViewModel cat = null;

            using (var ctx = new api_dbEntities())
            {
                cat = ctx.categories.Where(s => s.id == id)
                    .Select(s => new CategoryViewModel()
                    {
                        id = s.id,
                        name = s.name,
                        description = s.description
                    }).FirstOrDefault<CategoryViewModel>();
            }
            if (cat == null)
            {
                return NotFound();
            }
            return Ok(cat);
        }

        //Create Product Method
        public IHttpActionResult PostNewCategory(CategoryViewModel cat)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid Data");

            using (var ctx = new api_dbEntities())
            {
                ctx.categories.Add(new category()
                {
                    id = cat.id,
                    name = cat.name,
                    description = cat.description
                });
                ctx.SaveChanges();
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
                var cat = ctx.categories
                    .Where(s => s.id == id)
                    .FirstOrDefault();

                ctx.categories.Remove(cat);
                ctx.SaveChanges();
            }

            return Ok();
        }
    }
}
