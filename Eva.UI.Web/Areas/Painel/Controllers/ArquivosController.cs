using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eva.Aplicacao;
using Eva.Dominio;

namespace Eva.UI.Web.Areas.Painel.Controllers
{
    public class ArquivosController : Controller
    {
        public ActionResult Index(string id, string plugin)
        {
            this.Flash("Plugin: " + plugin + " Id:" + id);

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
            this.Flash("Plugin: " + plugin + " Id:" + id);
            return View();
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
