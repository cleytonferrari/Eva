using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eva.Aplicacao;
using Eva.Dominio;
using System.ComponentModel.DataAnnotations;
using Eva.UI.Web.Helpers;
using PagedList;

namespace Eva.UI.Web.Areas.Painel.Controllers
{
    public class NoticiaController : Controller
    {
        private readonly NoticiaAplicacao noticiaApp;
        private readonly CategoriaAplicacao categoriaApp;
        private readonly NoticiaZonaAplicacao noticiaZonaApp;
        private readonly FonteAplicacao fonteApp;

        public NoticiaController()
        {
            noticiaApp = Fabrica.NoticiaAplicacaoMongo();
            categoriaApp = Fabrica.CategoriaAplicacaoMongo();
            noticiaZonaApp = Fabrica.NoticiaZonaAplicacaoMongo();
            fonteApp = Fabrica.FonteAplicacaoMongo();

        }
        public ActionResult Index(int? page)
        {
            var numeroDaPagina = page ?? 1;
            var listaPaginada = noticiaApp.ListarTodos().OrderByDescending(x=>x.Data).ToPagedList(numeroDaPagina, 10);

            return View(listaPaginada);
        }

        public ActionResult Editar(string id)
        {
            ViewBag.Categorias = categoriaApp.ListarTodos().ToList();
            ViewBag.Zonas = noticiaZonaApp.ListarTodos().ToList();

            if (string.IsNullOrEmpty(id))
                return View(new NoticiaViewModel());

            var noticia = noticiaApp.ListarPorId(id);
            if (noticia == null)
            {
                this.Flash("Noticia não encontrado!", FlashEnum.Error);
                return View(new NoticiaViewModel());
            }

            var noticiaViewModel = new NoticiaViewModel()
            {
                Id = noticia.Id,
                Titulo = noticia.Titulo,
                Antetitulo = noticia.Antetitulo,
                Categoria = noticia.Categoria,
                CategoriaId = noticia.Categoria.Id,
                Zona = noticia.Zona,
                ZonaId = noticia.Zona.Id,
                Conteudo = noticia.Conteudo,
                Data = noticia.Data,
                ExibirComentarios = noticia.ExibirComentarios,
                Publicado = noticia.Publicado,
                Fonte = noticia.Fonte,
                FonteNome = noticia.Fonte.Nome,
                Resumo = noticia.Resumo
            };

            return View(noticiaViewModel);
        }

        [HttpPost]
        public ActionResult Editar(NoticiaViewModel noticia)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categorias = categoriaApp.ListarTodos().ToList();
                ViewBag.Zonas = noticiaZonaApp.ListarTodos().ToList();
                return View(noticia);
            }

            var noticiaSalvar = new Noticia
            {
                Id = noticia.Id,
                Titulo = noticia.Titulo,
                Antetitulo = noticia.Antetitulo,
                Categoria = noticia.Categoria,
                Conteudo = noticia.Conteudo,
                Data = noticia.Data,
                ExibirComentarios = noticia.ExibirComentarios,
                Publicado = noticia.Publicado,
                Fonte = noticia.Fonte,
                Resumo = noticia.Resumo,
                Zona = noticia.Zona
            };

            noticiaSalvar.Categoria = categoriaApp.ListarPorId(noticia.CategoriaId);
            noticiaSalvar.Zona = noticiaZonaApp.ListarPorId(noticia.ZonaId);
            
            var fonte = fonteApp.ListarPorNome(noticia.FonteNome);
            if (fonte != null)
            {
                noticiaSalvar.Fonte = fonte;
            }
            else
            {
                var fonteNova = new Fonte() {Nome = noticia.FonteNome};
                fonteApp.Salvar(fonteNova);
                noticiaSalvar.Fonte = fonteNova;
            }

            if (!string.IsNullOrEmpty(noticia.Id))
            {
                var noticiaBanco = noticiaApp.ListarPorId(noticia.Id);
                noticiaSalvar.Arquivos = noticiaBanco.Arquivos;
                noticiaSalvar.Hits = noticiaBanco.Hits;
            }


            noticiaApp.Salvar(noticiaSalvar);
            this.Flash("Noticia Salva com Sucesso!");
            return RedirectToAction("Index");
        }

        public JsonResult Excluir(string id)
        {
            var item = Fabrica.NoticiaAplicacaoMongo().ListarPorId(id);

            if (item == null) return Json("", JsonRequestBehavior.AllowGet);

            Imagem.ExcluirArquivo(item.Arquivos, "noticia");
            Fabrica.NoticiaAplicacaoMongo().Excluir(id);

            return Json("", JsonRequestBehavior.AllowGet);
        }
    }

    public class NoticiaViewModel
    {
        public string Id { get; set; }
        public string Titulo { get; set; }
        public string CategoriaId { get; set; }
        public Categoria Categoria { get; set; }
        public string ZonaId { get; set; }
        public NoticiaZona Zona { get; set; }
        public string Antetitulo { get; set; }
        public string Resumo { get; set; }
        public string Conteudo { get; set; }
        public DateTime Data { get; set; }
        public Fonte Fonte { get; set; }
        public string FonteNome { get; set; }
        public bool Publicado { get; set; }
        public bool ExibirComentarios { get; set; }

    }
}