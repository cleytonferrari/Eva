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
    public class NoticiaZonaController : Controller
    {
       private readonly NoticiaZonaAplicacao noticiaZonaApp;

        public NoticiaZonaController()
        {
            noticiaZonaApp = Fabrica.NoticiaZonaAplicacaoMongo();
        }
        public ActionResult Index(int? page)
        {

            var numeroDaPagina = page ?? 1;
            var listaPaginada = noticiaZonaApp.ListarTodos().ToPagedList(numeroDaPagina, 10);

            return View(listaPaginada);
        }

        public ActionResult Editar(string id)
        {
            if (string.IsNullOrEmpty(id))
                return View(new NoticiaZonaViewModel());

            var noticiaZona = noticiaZonaApp.ListarPorId(id);
            if (noticiaZona == null)
            {
                this.Flash("Zona da Notícia não encontrada!", FlashEnum.Error);
                return View(new NoticiaZonaViewModel());
            }

            var noticiaZonaViewModel = new NoticiaZonaViewModel()
            {
                Id = noticiaZona.Id,
                Nome = noticiaZona.Nome
            };

            return View(noticiaZonaViewModel);
        }

        [HttpPost]
        public ActionResult Editar(NoticiaZonaViewModel noticiaZona)
        {
            if (!ModelState.IsValid) 
                return View(noticiaZona);

            var noticiaZonaSalvar = new NoticiaZona
            {
                Id = noticiaZona.Id,
                Nome = noticiaZona.Nome
            };

            noticiaZonaApp.Salvar(noticiaZonaSalvar);
            this.Flash("Zona da Notícia Salva com Sucesso!");
            return RedirectToAction("Index");
        }
	}

    public class NoticiaZonaViewModel
    {
        public string Id { get; set; }
        [Required]
        public string Nome { get; set; }

    }
}