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
            vm.Noticias.EmbaixoDoDestaque = Fabrica.NoticiaAplicacaoMongo().ListarPublicadas("Embaixo do destaque").Take(5) ?? new List<Noticia>();
            vm.Noticias.Centro = Fabrica.NoticiaAplicacaoMongo().ListarPublicadas("Centro").Take(6) ?? new List<Noticia>();
            vm.Noticias.Categoria01 = Fabrica.NoticiaAplicacaoMongo().ListarPublicadas("Categoria 01").Take(3) ?? new List<Noticia>();
            vm.Noticias.Categoria02 = Fabrica.NoticiaAplicacaoMongo().ListarPublicadas("Categoria 02").Take(3) ?? new List<Noticia>();
            vm.Noticias.Categoria03 = Fabrica.NoticiaAplicacaoMongo().ListarPublicadas("Categoria 03").Take(3) ?? new List<Noticia>();

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
        public IEnumerable<Noticia> EmbaixoDoDestaque { get; set; }
        public IEnumerable<Noticia> Centro { get; set; }
        public IEnumerable<Noticia> Categoria01 { get; set; }
        public IEnumerable<Noticia> Categoria02 { get; set; }
        public IEnumerable<Noticia> Categoria03 { get; set; }
    }
}