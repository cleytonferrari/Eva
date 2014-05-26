using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eva.Aplicacao;
using Eva.Dominio;
using PagedList;

namespace Eva.UI.Web.Controllers
{
    [RoutePrefix("evento")]
    public class EventoController : Controller
    {
        [Route("{page:int?}")]
        public ActionResult Index(int? page)
        {
            //todo: checar se categoria existe ou notFound
            var numeroDaPagina = page ?? 1;
            var ultimas = Fabrica.EventoAplicacaoMongo().ListarPublicadas();

            ultimas = ultimas.OrderByDescending(x => x.Data).ToPagedList(numeroDaPagina, 10);
            return View(ultimas);
        }

        [Route("{ano}/{mes}/{dia}/{slug}/{page:int?}")]
        public ActionResult Ler(string slug, int ano, int mes, int dia,int? page)
        {
            var data = new DateTime(ano, mes, dia);
            var evento = Fabrica.EventoAplicacaoMongo().Ler(slug,data);
            if (evento == null)
                return HttpNotFound();

            var numeroDaPagina = page ?? 1;
            var vm = new EventoLerViewModel
            {
                Evento = evento,
                Arquivos = evento.Arquivos.OrderBy(x=>x.Ordem).ToPagedList(numeroDaPagina,10)
            };

            return View(vm);
        }
    }

    public class EventoLerViewModel
    {
        public Evento Evento { get; set; }
        public IPagedList<Arquivo> Arquivos { get; set; }
    }
}