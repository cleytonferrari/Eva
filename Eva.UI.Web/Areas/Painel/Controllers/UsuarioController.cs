using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eva.Dominio;

namespace Eva.UI.Web.Areas.Painel.Controllers
{
    public class UsuarioController : Controller
    {
        public ActionResult Index()
        {
            return View(new List<Usuario>());
        }

        public ActionResult Editar()
        {
            return View(new Usuario());
        }

        [HttpPost]
        public ActionResult Editar(Usuario usuario)
        {
            return View(new Usuario());
        }
	}
}