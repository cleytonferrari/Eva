using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Web;
using Eva.Dominio;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security;

namespace Eva.UI.Web.Helpers
{
    public static class Seguranca
    {
        public static void GerearSessaoDeUsuario(Usuario usuarioLogado)
        {
            var claims = new List<Claim>
            {
                new Claim("Id", usuarioLogado.Id),
                new Claim("Foto", usuarioLogado.Foto ?? ""),
                new Claim(ClaimTypes.Name, usuarioLogado.Nome),
                new Claim(ClaimTypes.Email, usuarioLogado.Email),
                new Claim(ClaimTypes.Role, usuarioLogado.Grupo.Nome ?? ""),
            };

            var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);

            var ctx = HttpContext.Current.Request.GetOwinContext();
            var authenticationManager = ctx.Authentication;
            authenticationManager.SignIn(new AuthenticationProperties { IsPersistent = false }, identity);
        }

        public static void DestruirSessaoDeUsuario()
        {
            var ctx = HttpContext.Current.Request.GetOwinContext();
            var authenticationManager = ctx.Authentication;
            authenticationManager.SignOut();
        }

        public static Usuario Usuario()
        {

            var ctx = (OwinContext)HttpContext.Current.Request.GetOwinContext();
            var user = ctx.Authentication.User;
            
            return new Usuario()
            {
                Id = user.FindFirst("Id").Value,
                Email = user.FindFirst(ClaimTypes.Email).Value,
                Nome = user.FindFirst(ClaimTypes.Name).Value,
                Foto = user.FindFirst("Foto").Value,
            };

        }
    }
}