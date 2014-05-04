using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eva.Aplicacao;
using Eva.Dominio;
using System.ComponentModel.DataAnnotations;
using PagedList;

namespace Eva.UI.Web.Areas.Painel.Controllers
{
    public class FonteController : Controller
    {
       private readonly FonteAplicacao fonteApp;

        public FonteController()
        {
            fonteApp = Fabrica.FonteAplicacaoMongo();
        }
        public ActionResult Index(int? page)
        {

            var numeroDaPagina = page ?? 1;
            var listaPaginada = fonteApp.ListarTodos().ToPagedList(numeroDaPagina, 10);

            return View(listaPaginada);
        }

        public ActionResult Editar(string id)
        {
            if (string.IsNullOrEmpty(id))
                return View(new FonteViewModel());

            var fonte = fonteApp.ListarPorId(id);
            if (fonte == null)
            {
                this.Flash("Fonte não encontrada!", FlashEnum.Error);
                return View(new FonteViewModel());
            }

            var fonteViewModel = new FonteViewModel()
            {
                Id = fonte.Id,
                Nome = fonte.Nome
            };

            return View(fonteViewModel);
        }

        [HttpPost]
        public ActionResult Editar(FonteViewModel fonte)
        {
            if (!ModelState.IsValid) 
                return View(fonte);

            var fonteSalvar = new Fonte
            {
                Id = fonte.Id,
                Nome = fonte.Nome
            };

            fonteApp.Salvar(fonteSalvar);
            this.Flash("Fonte Salva com Sucesso!");
            return RedirectToAction("Index");
        }
	}

    public class FonteViewModel
    {
        public string Id { get; set; }
        [Required]
        public string Nome { get; set; }

    }
}