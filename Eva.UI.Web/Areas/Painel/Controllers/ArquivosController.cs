using System;
using System.Collections.Generic;
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
            Session["logos"] = null;

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

            ViewBag.Logos = Fabrica.LogoAplicacaoMongo().ListarTodos().ToList();

            return View(arquivoViewModel);
        }

        public JsonResult Excluir(string id, string plugin, string idArquivo)
        {

            switch (plugin.ToLower())
            {
                case "noticia":
                    var noticia = Fabrica.NoticiaAplicacaoMongo().ListarPorId(id);
                    var arquivo = noticia.Arquivos.FirstOrDefault(x => x.Id == idArquivo);
                    noticia.Arquivos.Remove(arquivo);

                    var arquivos = Imagem.OrdenarArquivos(noticia.Arquivos.OrderBy(x => x.Ordem).Select(x => x.Id), noticia.Arquivos);
                    noticia.Arquivos = arquivos;

                    var arquivoCapa = arquivos.FirstOrDefault(x => x.Ordem == 1);
                    if (arquivoCapa != null)
                        Imagem.CropFile(arquivoCapa.Nome, "Noticia", ImagensLayout.Noticias);


                    Fabrica.NoticiaAplicacaoMongo().Salvar(noticia);
                    Imagem.ExcluirArquivo(arquivo.Nome, "noticia");
                    break;
            }

            return Json("", JsonRequestBehavior.AllowGet);
        }

        public JsonResult ConfigurarLogo(LogoAplicarViewModel logos)
        {
            Session["logos"] = logos;
            return Json("", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Upload(int? chunk, int? chunks, string name, string plugin, string id)
        {
            var fileUpload = Request.Files[0];

            if (Imagem.Upload(fileUpload, "Noticia", name, chunk, chunks))
            {
                switch (plugin.ToLower())
                {
                    case "noticia":
                        var noticia = Fabrica.NoticiaAplicacaoMongo().ListarPorId(id);

                        var ordem = (noticia.Arquivos.Any()) ? noticia.Arquivos.Max(x => x.Ordem) + 1 : 1;

                        noticia.Arquivos.Add(new Arquivo() { Nome = name, Legenda = noticia.Titulo, Ordem = ordem });
                        Fabrica.NoticiaAplicacaoMongo().Salvar(noticia);

                        if (ordem == 1)
                            Imagem.CropFile(name, "Noticia", ImagensLayout.Noticias);

                        break;
                }

                //Aplico o logo, ou redimensiona, salva
                var posicaoLogo = (LogoAplicarViewModel)Session["logos"] ?? new LogoAplicarViewModel();

                var pLogo = new List<PosicaoLogo>();

                #region Todo: Refatorar isso

                if (posicaoLogo.Logo1 != null)
                {
                    pLogo.Add(new PosicaoLogo()
                    {
                        IdLogo = posicaoLogo.Logo1,
                        PosicaoHorizontal = "Left", //"Left", "Right", or "Center".
                        PosicaoVertical = "Top" //"Top", "Middle", or "Bottom"
                    });
                }
                if (posicaoLogo.Logo2 != null)
                {
                    pLogo.Add(new PosicaoLogo()
                    {
                        IdLogo = posicaoLogo.Logo2,
                        PosicaoHorizontal = "Center",
                        PosicaoVertical = "Top"
                    });
                }

                if (posicaoLogo.Logo3 != null)
                {
                    pLogo.Add(new PosicaoLogo()
                    {
                        IdLogo = posicaoLogo.Logo3,
                        PosicaoHorizontal = "Right",
                        PosicaoVertical = "Top"
                    });
                }

                if (posicaoLogo.Logo4 != null)
                {
                    pLogo.Add(new PosicaoLogo()
                    {
                        IdLogo = posicaoLogo.Logo4,
                        PosicaoHorizontal = "Left",
                        PosicaoVertical = "Middle"
                    });
                }

                if (posicaoLogo.Logo5 != null)
                {
                    pLogo.Add(new PosicaoLogo()
                    {
                        IdLogo = posicaoLogo.Logo5,
                        PosicaoHorizontal = "Center",
                        PosicaoVertical = "Middle"
                    });
                }

                if (posicaoLogo.Logo6 != null)
                {
                    pLogo.Add(new PosicaoLogo()
                    {
                        IdLogo = posicaoLogo.Logo6,
                        PosicaoHorizontal = "Right",
                        PosicaoVertical = "Middle"
                    });
                }

                if (posicaoLogo.Logo7 != null)
                {
                    pLogo.Add(new PosicaoLogo()
                    {
                        IdLogo = posicaoLogo.Logo7,
                        PosicaoHorizontal = "Left",
                        PosicaoVertical = "Bottom"
                    });
                }

                if (posicaoLogo.Logo8 != null)
                {
                    pLogo.Add(new PosicaoLogo()
                    {
                        IdLogo = posicaoLogo.Logo8,
                        PosicaoHorizontal = "Center",
                        PosicaoVertical = "Bottom"
                    });
                }

                if (posicaoLogo.Logo9 != null)
                {
                    pLogo.Add(new PosicaoLogo()
                    {
                        IdLogo = posicaoLogo.Logo9,
                        PosicaoHorizontal = "Right",
                        PosicaoVertical = "Bottom"
                    });
                }
                #endregion

                Imagem.Logo(name, "Noticia", pLogo);

            }

            return Content("Success", "text/plain");
        }

        public JsonResult Ordenar(string id, string plugin, string[] items)
        {
            switch (plugin.ToLower())
            {
                case "noticia":
                    var noticia = Fabrica.NoticiaAplicacaoMongo().ListarPorId(id);

                    var arquivoCapa = noticia.Arquivos.FirstOrDefault(x => x.Ordem == 1);
                    if (arquivoCapa != null)
                        Imagem.LimparMiniaturaCapa(arquivoCapa.Nome, "Noticia");

                    var arquivos = Imagem.OrdenarArquivos(items, noticia.Arquivos);
                    noticia.Arquivos = arquivos;

                    arquivoCapa = arquivos.FirstOrDefault(x => x.Ordem == 1);
                    if (arquivoCapa != null)
                        Imagem.CropFile(arquivoCapa.Nome, "Noticia", ImagensLayout.Noticias);

                    Fabrica.NoticiaAplicacaoMongo().Salvar(noticia);
                    break;
            }

            return Json("", JsonRequestBehavior.AllowGet);
        }
    }

    public class ArquivoViewModel
    {
        public string Id { get; set; }
        public string Titulo { get; set; }
        public string Plugin { get; set; }
        public IEnumerable<Arquivo> Arquivos { get; set; }
    }

    public class LogoAplicarViewModel
    {
        public string Logo1 { get; set; }
        public string Logo2 { get; set; }
        public string Logo3 { get; set; }
        public string Logo4 { get; set; }
        public string Logo5 { get; set; }
        public string Logo6 { get; set; }
        public string Logo7 { get; set; }
        public string Logo8 { get; set; }
        public string Logo9 { get; set; }

    }
}
