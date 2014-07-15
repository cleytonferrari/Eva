using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Eva.Aplicacao;
using Eva.Dominio;
using Eva.UI.Web.Helpers;
using PagedList;

namespace Eva.UI.Web.Areas.Painel.Controllers
{
    [Authorize]
    public class LogoController : Controller
    {
        private readonly LogoAplicacao logoApp;
        

        public LogoController()
        {
            logoApp = Fabrica.LogoAplicacaoMongo();
            
        }
        public ActionResult Index(int? page)
        {
            var numeroDaPagina = page ?? 1;
            var listaPaginada = logoApp.ListarTodos().ToPagedList(numeroDaPagina, 10);

            return View(listaPaginada);
        }

        public ActionResult Editar(string id)
        {
            if (string.IsNullOrEmpty(id))
                return View(new LogoViewModel());

            var logo = logoApp.ListarPorId(id);
            if (logo == null)
            {
                this.Flash("Logo não encontrado!", FlashEnum.Error);
                return View(new LogoViewModel());
            }

            var logoViewModel = new LogoViewModel()
            {
                Id = logo.Id,
                Nome = logo.Nome
            };

            return View(logoViewModel);
        }

        [HttpPost]
        public ActionResult Editar(LogoViewModel logo)
        {
            if (!ModelState.IsValid)
            {
                return View(logo);
            }

            
            var logoBd = new Logo()
            {
                Id = logo.Id,
                Nome = logo.Nome
            };

            logoBd.Imagem = (logo.Imagem != null) ? Imagem.Upload(logo.Imagem, "Logo") : logo.PathImagem;


            logoApp.Salvar(logoBd);
            this.Flash("Logo Salvo com Sucesso!");
            return RedirectToAction("Index");
        }
        public JsonResult Excluir(string id)
        {
            var item = Fabrica.LogoAplicacaoMongo().ListarPorId(id);

            if (item == null) return Json("", JsonRequestBehavior.AllowGet);

            Imagem.ExcluirArquivo(item.Imagem, "logo");
            Fabrica.LogoAplicacaoMongo().Excluir(id);

            return Json("", JsonRequestBehavior.AllowGet);
        }



    }

    public class LogoViewModel
    {
        public string Id { get; set; }
        [Required]
        public string Nome { get; set; }
        public HttpPostedFileBase Imagem { get; set; }
        public string PathImagem { get; set; }
    }
}