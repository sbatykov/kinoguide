using KinoGuide.DbModels;
using KinoGuide.Identity;
using KinoGuide.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using X.PagedList;

namespace KinoGuide.Controllers
{
    public class FilmController : Controller
    {
        private readonly SiteDbContext _db;

        public FilmController(SiteDbContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        // GET: Home
        [ActionName("Index")]
        public ActionResult Index(int? page, int pageSize = 10)
        {
            var films = _db.Films.OrderBy(f => f.Id).Select(film => new FilmViewModel
            {
                Id = film.Id,
                Name = film.Name,
                Director = film.Director,
                Description = film.Description,
                Year = film.Year,
                Author = film.User.Name,
                AuthorId = film.User.Id
            }).ToPagedList(page ?? 1, pageSize);
            return View(films);
        }

        public ActionResult View(int Id)
        {
            var film = _db.Films.Find(Id);
            if (film == null)
            {
                return View("Error404");
            }
            return View(new FilmViewModel
            {
                Id = film.Id,
                Name = film.Name,
                Director = film.Director,
                Description = film.Description,
                Year = film.Year,
                Author = film.User.Name,
                AuthorId = film.User.Id
            });
        }

        [Authorize]
        public ActionResult Edit(int Id)
        {
            var film = _db.Films.Find(Id);
            if (film == null)
            {
                return View("Error404");
            }

            if (User.Identity.GetUserId() != film.User.Id)
            {
                return RedirectToAction("View", new { Id });
            }

            return View(new FilmViewModel
            {
                Id = film.Id,
                Name = film.Name,
                Director = film.Director,
                Description = film.Description,
                Year = film.Year,
                Author = film.User.Name,
                AuthorId = film.User.Id
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit(FilmViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var film = _db.Films.Find(model.Id);
            if (film == null)
            {
                return View("Error404");
            }

            if (User.Identity.GetUserId() != film.User.Id)
            {
                return RedirectToAction("View", new { Id = film.Id });
            }

            film.Name = model.Name;
            film.Year = model.Year;
            film.Director = model.Director;
            film.Description = model.Description;
            if (model.FilePoster != null)
            {
                using (var binaryReader = new BinaryReader(model.FilePoster.InputStream))
                {
                    film.Poster = binaryReader.ReadBytes(model.FilePoster.ContentLength);
                }
            }

            _db.SaveChanges();

            return RedirectToAction("View", new { id = film.Id });
        }

        public ActionResult New()
        {
            return View(new NewFilmViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult New(NewFilmViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var film = new Film
            {
                Name = model.Name,
                Director = model.Director,
                Description = model.Description,
                User = _db.Users.Single(x => x.Login == "Admin"),
                Year = model.Year
            };
            using (var binaryReader = new BinaryReader(model.FilePoster.InputStream))
            {
                film.Poster = binaryReader.ReadBytes(model.FilePoster.ContentLength);
            }

            _db.Films.Add(film);
            _db.SaveChanges();
            return RedirectToAction("View", new { id = film.Id });
        }
    }
}