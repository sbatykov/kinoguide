using KinoGuide.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KinoGuide.Controllers
{
    public class ImageController : Controller
    {
        private readonly SiteDbContext _db;

        public ImageController(SiteDbContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        // GET: Image
        public ActionResult Index(int id)
        {
            var film = _db.Films.Find(id);
            if (film == null)
            {
                return View("Error404");
            }
            return File(film.Poster, "image/jpeg");
        }
    }
}