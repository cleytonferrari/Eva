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
            //todo: checar se categoria existe ou notFound
            var numeroDaPagina = page ?? 1;
            var ultimas = Fabrica.NoticiaAplicacaoMongo().ListarPublicadas();

            if (categoria != "todas")
                ultimas = ultimas.Where(x => x.Categoria.Slug == categoria);

            ultimas = ultimas.OrderByDescending(x => x.Data).ToPagedList(numeroDaPagina, 6);
            return View(ultimas);
        }

        [Route("{slugCategoria}/{slugNoticia}/{slugLocal}")]
        public ActionResult Ler(string slugNoticia, string slugCategoria, string slugLocal)
        {
            //slugLocal, so para uso de SEO
            
            var noticia = Fabrica.NoticiaAplicacaoMongo().Ler(slugNoticia, slugCategoria);
            if (noticia == null)
                return HttpNotFound();

            var vm = new NoticiaLerViewModel
            {
                Noticia = noticia,
                //todo: ultimas noticias, e relacionadas, excluir a noticia atual, e excluir das relacionadas as ultimas
                Ultimas = Fabrica.NoticiaAplicacaoMongo().ListarPublicadas().Take(4),
                Relacionadas = Fabrica.NoticiaAplicacaoMongo().ListarPorCategoria(slugCategoria).Take(4)
            };

            return View(vm);
        }
    }

    public class NoticiaLerViewModel
    {
        public Noticia Noticia { get; set; }

        public IEnumerable<Noticia> Ultimas { get; set; }

        public IEnumerable<Noticia> Relacionadas { get; set; }

    }
}