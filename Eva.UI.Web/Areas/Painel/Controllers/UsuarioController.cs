using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eva.Aplicacao;
using Eva.Dominio;

namespace Eva.UI.Web.Areas.Painel.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly UsuarioAplicacao usuarioApp;

        public UsuarioController()
        {
            usuarioApp = Fabrica.UsuarioAplicacaoMongo();
        }
        public ActionResult Index()
        {
            return View(usuarioApp.ListarTodos().ToList());
        }

        public ActionResult Editar()
        {
            return View(new UsuarioViewModel());
        }

        [HttpPost]
        public ActionResult Editar(UsuarioViewModel usuario)
        {
            if (ModelState.IsValid)
            {
                var caminhoSalvar = "";
                if (usuario.Foto != null)
                {
                   caminhoSalvar = Path.Combine(Server.MapPath("~/App_Data/Arquivos/Usuario/"), Path.GetFileName(usuario.Foto.FileName));
                    usuario.Foto.SaveAs(caminhoSalvar);
                }

                var user = new Usuario()
                {
                    Id = usuario.Id,
                    Nome = usuario.Nome,
                    Email = usuario.Email,
                    Foto = caminhoSalvar,
                    Grupo = usuario.Grupo,
                    Senha = usuario.Senha

                };
                usuarioApp.Salvar(user);
                this.Flash("Usuário Salvo com Sucesso!");
                return RedirectToAction("Index");
            }
            return View(usuario);
        }
    }

    public class UsuarioViewModel
    {
        public string Id { get; set; }
        [Required]
        public string Nome { get; set; }
        
        [Required]
        public string Email { get; set; }
        
        [Required]
        public string Senha { get; set; }

        [System.ComponentModel.DataAnnotations.Compare("Senha", ErrorMessage = "As senhas não conferem")]
        public string ConfirmarSenha { get; set; }

        //[FileExtensions(Extensions = "jpg,gif,png,jpeg", ErrorMessage = "Extensões suportadas *.jpg, *.jpeg, *.png, *.gif")]
        public HttpPostedFileBase Foto { get; set; }
        
        public string Grupo { get; set; }
    }
}