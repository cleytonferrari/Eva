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
            vm.Noticias.Urgente = Fabrica.NoticiaAplicacaoMongo().ListarPublicadas("Urgente").FirstOrDefault()?? new Noticia();
            vm.Noticias.Destaques = Fabrica.NoticiaAplicacaoMongo().ListarPublicadas("Destaque").Take(5) ?? new List<Noticia>();
            vm.Noticias.AoLadoDoDestaque = Fabrica.NoticiaAplicacaoMongo().ListarPublicadas("Ao lado do destaque").Take(4) ?? new List<Noticia>();
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
        public IEnumerable<Noticia> Destaques { get; set; }
        public IEnumerable<Noticia> AoLadoDoDestaque { get; set; }
    }
}