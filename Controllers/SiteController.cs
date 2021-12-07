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
    public class SiteController : ControllerBase
    {
        public static CategoryItem category = new CategoryItem(1, "Redes Sociales");
        public static List<SiteItem> Sites = new List<SiteItem> {
            new SiteItem { Id = 1, Name = "Facebook", Url = "https://www.facebook.com/", Category = category, CreationDate="05/12/21", User="facebook", Password = "1234", Description = "Sitio web"},
            new SiteItem { Id = 2, Name = "Twitter", Url = "https://www.twitter.com/", Category = category, CreationDate="05/12/21", User="twitter", Password = "1234", Description = "Sitio web"},
            new SiteItem { Id = 3, Name = "Instagram", Url = "https://www.instagram.com/", Category = category, CreationDate="05/12/21", User="instagram", Password = "1234", Description = "Sitio web"},
            new SiteItem { Id = 4, Name = "LinkedIn", Url = "https://www.linkedin.com/", Category = category, CreationDate="05/12/21", User="linkedin", Password = "1234", Description = "Sitio web"},
        };

        [HttpGet]
        public ActionResult<List<SiteItem>> Get() {
            return Ok(Sites);
        }

        [HttpGet]
        [Route("{Id}")]
        public ActionResult<SiteItem> Get(int Id) {
            var siteItem = Sites.Find(x => x.Id == Id);
            return siteItem == null ? NotFound() : Ok(siteItem);
        }

        [HttpPost]
        public ActionResult Post(SiteItem siteItem) {
            var existingSiteItem = Sites.Find(x => x.Id == siteItem.Id);
            if (existingSiteItem != null) {
                return Conflict("Ya existe un sitio con esa ID.");
            } else {
                Sites.Add(siteItem);
                var resourceUrl = Request.Path.ToString() + "/" + siteItem.Id;
                return Created(resourceUrl, siteItem);
            }
        }

        [HttpPut]
        public ActionResult Put(SiteItem siteItem) {
            var existingSiteItem = Sites.Find(x => x.Id == siteItem.Id);
            if (existingSiteItem == null) {
                return Conflict("No existe un sitio con esa ID.");
            } else {
                existingSiteItem.Name = siteItem.Name;
                var resourceUrl = Request.Path.ToString() + "/" + siteItem.Id;
                return Ok();
            }
        }

        [HttpDelete]
        public ActionResult Delete(int Id) {
            var siteItem = Sites.Find(x => x.Id == Id);
            if (siteItem == null) {
                return NotFound("No existe un sitio con esa ID.");
            } else {
                Sites.Remove(siteItem);
                return NoContent();
            }
        }
    }
}
