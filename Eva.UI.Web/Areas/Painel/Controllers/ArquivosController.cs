using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eva.Aplicacao;
using Eva.Dominio;
using Eva.UI.Web.Helpers;

namespace Eva.UI.Web.Areas.Painel.Controllers
{
    public class ArquivosController : Controller
    {
        public ActionResult Index(string id, string plugin)
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(plugin))
                return RedirectToAction("Index", "Home", new { area = "Painel" });

            var arquivoViewModel = new ArquivoViewModel();

            switch (plugin.ToLower())
            {
                case "noticia":
                    var noticia = Fabrica.NoticiaAplicacaoMongo().ListarPorId(id);
                    arquivoViewModel.Arquivos = noticia.Arquivos ?? new List<Arquivo>();
                    arquivoViewModel.Id = noticia.Id;
                    arquivoViewModel.Titulo = noticia.Titulo;
                    arquivoViewModel.Plugin = plugin;
                    break;
            }

            return View(arquivoViewModel);
        }

        public ActionResult Enviar(string id, string plugin)
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(plugin))
                return RedirectToAction("Index", "Home", new { area = "Painel" });

            var arquivoViewModel = new ArquivoViewModel();

            switch (plugin.ToLower())
            {
                case "noticia":
                    var noticia = Fabrica.NoticiaAplicacaoMongo().ListarPorId(id);
                    arquivoViewModel.Arquivos = noticia.Arquivos ?? new List<Arquivo>();
                    arquivoViewModel.Id = noticia.Id;
                    arquivoViewModel.Titulo = noticia.Titulo;
                    arquivoViewModel.Plugin = plugin;
                    break;
            }
            return View(arquivoViewModel);
        }

        [HttpPost]
        public ActionResult Upload(int? chunk, int? chunks, string name, string plugin, string id)
        {
            var fileUpload = Request.Files[0];

            if (Imagem.Upload(fileUpload, "Noticia", name, chunk, chunks))
            {
                //Aplico o logo, ou redimensiona, salva
                switch (plugin.ToLower())
                {
                    case "noticia":
                        var noticia = Fabrica.NoticiaAplicacaoMongo().ListarPorId(id);

                        var ordem = (noticia.Arquivos.Any()) ? noticia.Arquivos.Max(x => x.Ordem) + 1 : 1;

                        noticia.Arquivos.Add(new Arquivo() { Nome = name, Legenda = noticia.Titulo, Ordem = ordem });
                        Fabrica.NoticiaAplicacaoMongo().Salvar(noticia);
                        
                        if (ordem == 1)//foto da capa
                            Imagem.CropFile(name, "Noticia", ImagensLayout.Noticias);

                        break;
                }
            }

            return Content("Success", "text/plain");
        }
    }

    public class ArquivoViewModel
    {
        public string Id { get; set; }
        public string Titulo { get; set; }
        public string Plugin { get; set; }
        public IEnumerable<Arquivo> Arquivos { get; set; }
    }
}
