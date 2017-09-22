using KinoGuide.DbModels;
using KinoGuide.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace KinoGuide.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly SiteDbContext _db;

        public UserController(SiteDbContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if (String.IsNullOrWhiteSpace(returnUrl))
            {
                ViewBag.returnUrl = Request.UrlReferrer.AbsoluteUri;
            } else
            {
                ViewBag.returnUrl = returnUrl;
            }            
            return View(new LoginViewModel());
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            var user = _db.Users.SingleOrDefault(u => u.Login == model.Login && u.Password == model.Password);
            if (user == null)
            {
                ModelState.AddModelError("", "Неверный пароль");
            }

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name)
            };
            var identity = new ClaimsIdentity(claims, "Cookie");
            Request.GetOwinContext().Authentication.SignIn(identity);


            if (!String.IsNullOrWhiteSpace(returnUrl) && Uri.IsWellFormedUriString(returnUrl, UriKind.RelativeOrAbsolute))
            {
                return Redirect(returnUrl);
            } else
            {
                return RedirectToAction("Index", "Film");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            var ctx = Request.GetOwinContext();
            var authenticationManager = ctx.Authentication;
            authenticationManager.SignOut();
            return RedirectToAction("Index", "Film");
        }
    }
}