using System;
using System.Collections.Generic;
using System.IO;
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

        /*Action usada pelo plupload para enviar os arquivos*/
        [HttpPost]
        public ActionResult Upload(int? chunk, string name, string plugin, string id)
        {
            //Arquivo que o PlUpload envia.
            var fileUpload = Request.Files[0];
            //Local onde vai ficar as fotos enviadas.
            var uploadPath = Server.MapPath("~/Content/Uploads");
            //Faz um checagem se o arquivo veio correto.
            chunk = chunk ?? 0;

            var uploadedFilePath = Path.Combine(uploadPath, name);

            //faz o upload literalmetne do arquivo.
            using (var fs = new FileStream(uploadedFilePath, chunk == 0 ? FileMode.Create : FileMode.Append))
            {
                var buffer = new byte[fileUpload.InputStream.Length];
                fileUpload.InputStream.Read(buffer, 0, buffer.Length);
                fs.Write(buffer, 0, buffer.Length);
            }

            switch (plugin.ToLower())
            {
                case "noticia":
                    var noticia = Fabrica.NoticiaAplicacaoMongo().ListarPorId(id);
                    noticia.Arquivos.Add(new Arquivo() { Nome = name, Legenda = noticia.Titulo, Ordem = 0});
                    Fabrica.NoticiaAplicacaoMongo().Salvar(noticia);
                    break;
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
