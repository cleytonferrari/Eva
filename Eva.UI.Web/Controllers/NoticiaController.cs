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
    public class NoticiaController : Controller
    {
        public ActionResult Index(int? page, string categoria = "")
        {
            var numeroDaPagina = page ?? 1;
            var ultimas = Fabrica.NoticiaAplicacaoMongo().ListarPublicadas();

            if (!string.IsNullOrEmpty(categoria) && categoria != "todas")
                ultimas = ultimas.Where(x => x.Categoria.Slug == categoria);

            ultimas = ultimas.OrderByDescending(x => x.Data).ToPagedList(numeroDaPagina, 6);
            return View(ultimas);
        }
    }
}