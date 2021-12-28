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

    public class CategoriesController : ControllerBase
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

        [HttpGet]
        [Route("{CategoryId}")]
        public ActionResult<List<CategoryItem>> Get(int CategoryId) {
            var categoryItem = Categories.FindAll(x => x.Id == CategoryId);
            return categoryItem == null ? NotFound("No existe una categoría con esa ID.") : Ok(categoryItem);
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
                return Ok("Categoría actualizada con éxito.");
            }
        }

        [HttpDelete]
        [Route("{CategoryId}")]
        public ActionResult Delete(int CategoryId) {
            var categoryItem = Categories.Find(x => x.Id == CategoryId);
            if (categoryItem == null) {
                return NotFound("No existe una categoría con esa ID.");
            } else {
                Categories.Remove(categoryItem);
                return NoContent();
            }
        }
    }
}