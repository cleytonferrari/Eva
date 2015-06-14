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
            //todo: corrigir os takes apos finalizar o layout
            //todo: implementar o graph do facebook
            //todo: arrumar o SEO, title, alt, meta tags, nome dos arquivos
            //todo: arrumar bug 1px do crop das imagens
            var vm = new HomeViewModel
            {
                Noticias =
                {
                    Urgente = Fabrica.NoticiaAplicacaoMongo().ListarPublicadas("Urgente").FirstOrDefault() ?? new Noticia(),
                    Destaques = Fabrica.NoticiaAplicacaoMongo().ListarPublicadas("Destaque").Take(5) ?? new List<Noticia>(),
                    AoLadoDoDestaque = Fabrica.NoticiaAplicacaoMongo().ListarPublicadas("Ao lado do destaque").Take(3) ?? new List<Noticia>(),
                    Centro = Fabrica.NoticiaAplicacaoMongo().ListarPublicadas("Centro").Take(11) ?? new List<Noticia>(),
                    Categoria01 = Fabrica.NoticiaAplicacaoMongo().ListarPublicadas("Categoria 01").Take(3) ?? new List<Noticia>(),
                    Categoria02 = Fabrica.NoticiaAplicacaoMongo().ListarPublicadas("Categoria 02").Take(3) ?? new List<Noticia>(),

                    EmbaixoDoDestaque = Fabrica.NoticiaAplicacaoMongo().ListarPublicadas("Embaixo do destaque").Take(10) ?? new List<Noticia>(),
                    
                    Slide = Fabrica.NoticiaAplicacaoMongo().ListarPublicadas("Slide").Take(6) ?? new List<Noticia>(),
                    
                    Rodape = Fabrica.NoticiaAplicacaoMongo().ListarPublicadas("Rodapé").Take(6) ?? new List<Noticia>(),

                    //todo: implementar os metodos para as mais lidas na aplicacao
                    MaisLidaDoDia = Fabrica.NoticiaAplicacaoMongo().MaisLidas().Take(4) ?? new List<Noticia>(),
                    MaisLidaDaSemana = Fabrica.NoticiaAplicacaoMongo().ListarPublicadas().Take(4) ?? new List<Noticia>(),
                    MaisLidaDoMes = Fabrica.NoticiaAplicacaoMongo().ListarPublicadas().Take(4) ?? new List<Noticia>(),
                    Ultimas = Fabrica.NoticiaAplicacaoMongo().ListarPublicadas().Take(11) ?? new List<Noticia>(),
                },
                Eventos =
                {
                    UltimosEventos = Fabrica.EventoAplicacaoMongo().ListarPublicadas().Take(16) ?? new List<Evento>()
                }
            };

            return View(vm);
        }

        [ChildActionOnly]
        public PartialViewResult CategoriasWidget()
        {
            var categorias = Fabrica.CategoriaAplicacaoMongo().ListarTodos().ToList();
            return PartialView(categorias);

        }
    }

    public class HomeViewModel
    {
        public HomeViewModel()
        {
            Noticias = new NoticiaViewModel();
            Eventos = new EventoViewModel();
        }
        public NoticiaViewModel Noticias { get; set; }

        public EventoViewModel Eventos { get; set; }


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
        public IEnumerable<Noticia> Slide { get; set; }
        public IEnumerable<Noticia> Rodape { get; set; }
        public IEnumerable<Noticia> MaisLidaDoDia { get; set; }
        public IEnumerable<Noticia> MaisLidaDaSemana { get; set; }
        public IEnumerable<Noticia> MaisLidaDoMes { get; set; }
        public IEnumerable<Noticia> Ultimas { get; set; }
    }

    public class EventoViewModel
    {
        public IEnumerable<Evento> UltimosEventos { get; set; }
    }

}