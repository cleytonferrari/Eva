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

namespace Eva.UI.Web.Areas.Painel.Controllers
{
    [Authorize]
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

        public ActionResult Editar(string id)
        {
            if (string.IsNullOrEmpty(id))
                return View(new UsuarioViewModel());

            var usuario = usuarioApp.ListarPorId(id);
            if (usuario == null)
            {
                this.Flash("Usuário não encontrado!", FlashEnum.Error);
                return View(new UsuarioViewModel());
            }

            var user = new UsuarioViewModel()
            {
                Id = usuario.Id,
                Email = usuario.Email,
                Nome = usuario.Nome,
                Grupo = usuario.Grupo,
                PathFoto = usuario.Foto
            };

            return View(user);
        }

        [HttpPost]
        public ActionResult Editar(UsuarioViewModel usuario)
        {
            if (ModelState.IsValid)
            {
                var senha = usuario.Senha;
                if (string.IsNullOrEmpty(usuario.Id))
                {
                    if (string.IsNullOrEmpty(senha))
                    {
                        ModelState.AddModelError("Senha", "O campo senha é obrigatório!");
                        return View(usuario);
                    }
                }
                else if (string.IsNullOrEmpty(senha))
                {
                    var usuarioBanco = usuarioApp.ListarPorId(usuario.Id);
                    senha = usuarioBanco.Senha;
                }

                var user = new Usuario()
                {
                    Id = usuario.Id,
                    Nome = usuario.Nome,
                    Email = usuario.Email,
                    Grupo = usuario.Grupo,
                    Senha = senha
                };

                user.Foto = (usuario.Foto != null) ? Imagem.Upload(usuario.Foto, "Usuario") : usuario.PathFoto;


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

        public string Senha { get; set; }

        [System.ComponentModel.DataAnnotations.Compare("Senha", ErrorMessage = "As senhas não conferem")]
        public string ConfirmarSenha { get; set; }

        //[FileExtensions(Extensions = "jpg,gif,png,jpeg", ErrorMessage = "Extensões suportadas *.jpg, *.jpeg, *.png, *.gif")]
        public HttpPostedFileBase Foto { get; set; }

        public string PathFoto { get; set; }

        public string Grupo { get; set; }
    }
}