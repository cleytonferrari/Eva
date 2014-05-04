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
    public class CategoriaController : Controller
    {
       private readonly CategoriaAplicacao categoriaApp;

        public CategoriaController()
        {
            categoriaApp = Fabrica.CategoriaAplicacaoMongo();
        }
        public ActionResult Index(int? page)
        {

            var numeroDaPagina = page ?? 1;
            var listaPaginada = categoriaApp.ListarTodos().ToPagedList(numeroDaPagina, 10);

            return View(listaPaginada);
        }

        public ActionResult Editar(string id)
        {
            if (string.IsNullOrEmpty(id))
                return View(new CategoriaViewModel());

            var categoria = categoriaApp.ListarPorId(id);
            if (categoria == null)
            {
                this.Flash("Categoria não encontrado!", FlashEnum.Error);
                return View(new CategoriaViewModel());
            }

            var categoriaViewModel = new CategoriaViewModel()
            {
                Id = categoria.Id,
                Nome = categoria.Nome,
                CorFundo = categoria.CorFundo,
                CorTexto = categoria.CorTexto
            };

            return View(categoriaViewModel);
        }

        [HttpPost]
        public ActionResult Editar(CategoriaViewModel categoria)
        {
            if (!ModelState.IsValid) 
                return View(categoria);

            var categoriaSalvar = new Categoria
            {
                Id = categoria.Id,
                Nome = categoria.Nome,
                CorFundo = categoria.CorFundo,
                CorTexto = categoria.CorTexto
            };

            categoriaApp.Salvar(categoriaSalvar);
            this.Flash("Categoria Salva com Sucesso!");
            return RedirectToAction("Index");
        }
	}

    public class CategoriaViewModel
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string CorTexto { get; set; }
        public string CorFundo { get; set; }

    }
}