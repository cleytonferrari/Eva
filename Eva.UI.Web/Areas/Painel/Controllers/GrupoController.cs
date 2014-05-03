using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eva.Aplicacao;
using Eva.Dominio;
using System.ComponentModel.DataAnnotations;

namespace Eva.UI.Web.Areas.Painel.Controllers
{
    public class GrupoDeUsuarioController : Controller
    {
       private readonly GrupoDeUsuarioAplicacao grupoDeUsuarioApp;

        public GrupoDeUsuarioController()
        {
            grupoDeUsuarioApp = Fabrica.GrupoDeUsuarioAplicacaoMongo();
        }
        public ActionResult Index()
        {
            return View(grupoDeUsuarioApp.ListarTodos().ToList());
        }

        public ActionResult Editar(string id)
        {
            if (string.IsNullOrEmpty(id))
                return View(new GrupoViewModel());

            var usuario = grupoDeUsuarioApp.ListarPorId(id);
            if (usuario == null)
            {
                this.Flash("Usuário não encontrado!", FlashEnum.Error);
                return View(new GrupoViewModel());
            }

            var grupo = new GrupoViewModel()
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Permissoes = usuario.Permissoes
            };

            return View(grupo);
        }

        [HttpPost]
        public ActionResult Editar(GrupoViewModel grupo)
        {
            if (!ModelState.IsValid) 
                return View(grupo);

            var grupoSalvar = new GrupoDeUsuario()
            {
                Id = grupo.Id,
                Nome = grupo.Nome,
                Permissoes = grupo.Permissoes
            };

            grupoDeUsuarioApp.Salvar(grupoSalvar);
            this.Flash("Grupo Salvo com Sucesso!");
            return RedirectToAction("Index");
        }
	}

    public class GrupoViewModel
    {
        public GrupoViewModel()
        {
            Permissoes = new[] {""};
        }
        public string Id { get; set; }
        [Required]
        public string Nome { get; set; }

        public IEnumerable<string> Permissoes { get; set; }

    }
}