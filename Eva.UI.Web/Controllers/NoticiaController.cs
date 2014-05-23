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
    [RoutePrefix("noticia")]
    public class NoticiaController : Controller
    {
        [Route("{categoria}/{page:int?}")]
        public ActionResult Index(int? page, string categoria)
        {
            var numeroDaPagina = page ?? 1;
            var ultimas = Fabrica.NoticiaAplicacaoMongo().ListarPublicadas();

            if (categoria != "todas")
                ultimas = ultimas.Where(x => x.Categoria.Slug == categoria);

            ultimas = ultimas.OrderByDescending(x => x.Data).ToPagedList(numeroDaPagina, 6);
            return View(ultimas);
        }

        [Route("{slugCategoria}/{slugNoticia}")]
        public ActionResult Ler(string slugNoticia, string slugCategoria)
        {
            var noticia = Fabrica.NoticiaAplicacaoMongo().Ler(slugNoticia, slugCategoria);
            if (noticia == null)
                return HttpNotFound();

            return View(noticia);
        }
    }
}