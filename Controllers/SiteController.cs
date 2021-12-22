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
        public static List<SiteItem> Sites = new List<SiteItem> {
            new SiteItem { Id = 1, Name = "Facebook", Url = "https://www.facebook.com/", CategoryId = 1, CreationDate="05/12/2021", User="facebook", Password = "1234", Description = "Sitio web"},
            new SiteItem { Id = 2, Name = "Twitter", Url = "https://www.twitter.com/", CategoryId = 1, CreationDate="05/12/2021", User="twitter", Password = "1234", Description = "Sitio web"},
            new SiteItem { Id = 3, Name = "Instagram", Url = "https://www.instagram.com/", CategoryId = 1, CreationDate="05/12/2021", User="instagram", Password = "1234", Description = "Sitio web"},
            new SiteItem { Id = 4, Name = "LinkedIn", Url = "https://www.linkedin.com/", CategoryId = 1, CreationDate="05/12/2021", User="linkedin", Password = "1234", Description = "Sitio web"},
            new SiteItem { Id = 5, Name = "Notion", Url = "https://www.notion.so/", CategoryId = 2, CreationDate="12/12/2021", User="notion", Password = "1234", Description = "Sitio web"},
            new SiteItem { Id = 6, Name = "Office 365", Url = "https://www.office.com/", CategoryId = 2, CreationDate="12/12/2021", User="office", Password = "1234", Description = "Sitio web"},
            new SiteItem { Id = 7, Name = "Slack", Url = "https://slack.com/", CategoryId = 2, CreationDate="12/12/2021", User="slack", Password = "1234", Description = "Sitio web"},
            new SiteItem { Id = 8, Name = "Twitch", Url = "https://www.twitch.tv/", CategoryId = 3, CreationDate="12/12/2021", User="twitch", Password = "1234", Description = "Sitio web"},
            new SiteItem { Id = 9, Name = "Youtube", Url = "https://www.youtube.com/", CategoryId = 3, CreationDate="12/12/2021", User="youtube", Password = "1234", Description = "Sitio web"},
        };

        [HttpGet]
        public ActionResult<List<SiteItem>> Get() {
            if (Sites == null) {
                return NotFound("No se han encontrado sitios.");
            } else {
                return Ok(Sites);
            }
        }

        [HttpGet]
        [Route("GetByCategoryID/{CategoryId}")]
        public ActionResult<List<SiteItem>> GetByCategoryId(int CategoryId) {
            var siteItem = Sites.FindAll(x => x.CategoryId == CategoryId);
            return siteItem == null ? NotFound("No existe una categoría con esa ID.") : Ok(siteItem);
        }

        [HttpGet]
        [Route("GetBySiteID/{SiteId}")]
        public ActionResult GetBySiteId(int SiteId) {
            var siteItem = Sites.FindAll(x => x.Id == SiteId);
            return siteItem == null ? NotFound("No existe un sitio con esa ID.") : Ok(siteItem);
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
                return Ok("Sitio actualizado con éxito.");
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
