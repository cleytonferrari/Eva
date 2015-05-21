using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eva.Aplicacao;
using Eva.Dominio;
using PagedList;

namespace Eva.UI.Web.Areas.Painel.Controllers
{
    public class ExtratorNewsItemController : Controller
    {
        private readonly ExtratorNewsItemAplicacao extratorNewsItemApp;
        private readonly ExtratorNewsAplicacao extratorNewsApp;


        public ExtratorNewsItemController()
        {
            extratorNewsItemApp = Fabrica.ExtratorNewsItemAplicacaoMongo();
            extratorNewsApp = Fabrica.ExtratorNewsAplicacaoMongo();

        }
        public ActionResult Index(string id, int? page)
        {
            var numeroDaPagina = page ?? 1;
            var listaPaginada = extratorNewsItemApp.ListarTodosPorIdExtratorNews(id).ToPagedList(numeroDaPagina, 10);
            var extratorNews = extratorNewsApp.ListarPorId(id);
            if (extratorNews == null)
                return HttpNotFound();
            ViewBag.ExtratorNews = extratorNews;

            return View(listaPaginada);
        }

        public ActionResult Editar(string id, string idExtrator)
        {
            var extratorNews = extratorNewsApp.ListarPorId(idExtrator);
            if (extratorNews == null)
                return HttpNotFound();
            ViewBag.ExtratorNews = extratorNews;

            if (string.IsNullOrEmpty(id))
                return View(new ExtratorNewsItem());

            var extratorNewsItem = extratorNewsItemApp.ListarPorId(id);
            if (extratorNewsItem == null)
            {
                this.Flash("Item não encontrado!", FlashEnum.Error);
                return View(new ExtratorNewsItem());
            }

            return View(extratorNewsItem);
        }

        [HttpPost]
        public ActionResult Editar(ExtratorNewsItem extratorNewsItem)
        {
            if (!ModelState.IsValid)
            {
                var extratorNews = extratorNewsApp.ListarPorId(extratorNewsItem.IdExtratorNews);
                ViewBag.ExtratorNews = extratorNews;
                return View(extratorNewsItem);
            }

            extratorNewsItemApp.Salvar(extratorNewsItem);
            this.Flash("Item Salvo com Sucesso!");
            return RedirectToAction("Index", new { Id = extratorNewsItem.IdExtratorNews });
        }

        public JsonResult Excluir(string id)
        {
            extratorNewsItemApp.Excluir(id);
            return Json("", JsonRequestBehavior.AllowGet);
        }
    }
}