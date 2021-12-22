using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace PasswordAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class CategoryController : ControllerBase
    {
        public static List<CategoryItem> Categories = new List<CategoryItem> {
            new CategoryItem (1, "Redes Sociales"),
            new CategoryItem (2, "Herramientas de trabajo"),
            new CategoryItem (3, "Streaming"),
        };

        [HttpGet]
        public ActionResult<List<CategoryItem>> Get() {
            if (Categories == null) {
                return NotFound("No se han encontrado categorías.");
            } else {
                return Ok(Categories);
            }
        }

        [HttpPost]
        public ActionResult Post(CategoryItem categoryItem) {
            var existingCategoryItem = Categories.Find(x => x.Id == categoryItem.Id);
            if (existingCategoryItem != null) {
                return Conflict("Ya existe una categoría con esa ID.");
            } else {
                Categories.Add(categoryItem);
                var resourceUrl = Request.Path.ToString() + "/" + categoryItem.Id;
                return Created(resourceUrl, categoryItem);
            }
        }

        [HttpPut]
        public ActionResult Put(CategoryItem categoryItem) {
            var existingCategoryItem = Categories.Find(x => x.Id == categoryItem.Id);
            if (existingCategoryItem == null) {
                return Conflict("No existe una categoría con esa ID.");
            } else {
                existingCategoryItem.Name = categoryItem.Name;
                var resourceUrl = Request.Path.ToString() + "/" + categoryItem.Id;
                return Ok();
            }
        }

        [HttpDelete]
        public ActionResult Delete(int Id) {
            var categoryItem = Categories.Find(x => x.Id == Id);
            if (categoryItem == null) {
                return NotFound("No existe una categoría con esa ID.");
            } else {
                Categories.Remove(categoryItem);
                return NoContent();
            }
        }
    }
}