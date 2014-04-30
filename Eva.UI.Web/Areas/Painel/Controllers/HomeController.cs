using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace Eva.UI.Web.Areas.Painel.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Login(LoginViewModel usuario)
        {
            if (usuario.Login != "cleyton") return View(usuario);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "Cleyton"),
                new Claim(ClaimTypes.Email, "cleyton@email.com"),
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim(ClaimTypes.Role, "Master")
            };

            var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);

            var ctx = Request.GetOwinContext();
            var authenticationManager = ctx.Authentication;
            authenticationManager.SignIn(new AuthenticationProperties { IsPersistent = usuario.Lembrar }, identity);

            return RedirectToAction("Index");
        }
    }

    public class LoginViewModel
    {
        public string Login { get; set; }
        public string Senha { get; set; }
        public bool Lembrar { get; set; }
    }
}