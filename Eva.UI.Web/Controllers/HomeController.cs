using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eva.Aplicacao;
using Eva.Dominio;

namespace Eva.UI.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var vm = new HomeViewModel();
            vm.Noticias.Urgente = Fabrica.NoticiaAplicacaoMongo().ListarTodos().FirstOrDefault(x => x.Zona.Nome == "Urgente")?? new Noticia();
            return View(vm);
        }
    }

    public class HomeViewModel
    {
        public HomeViewModel()
        {
            Noticias = new NoticiaViewModel();
        }
        public NoticiaViewModel  Noticias { get; set; }

    }

    public class NoticiaViewModel
    {
        public Noticia Urgente { get; set; }
    }
}