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
        };

        [HttpGet]
        public ActionResult<List<CategoryItem>> Get() {
            return Ok(Categories);
        }
    }
}