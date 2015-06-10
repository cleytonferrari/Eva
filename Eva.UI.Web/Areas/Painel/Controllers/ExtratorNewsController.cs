using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CsQuery;
using Eva.Aplicacao;
using Eva.Dominio;
using PagedList;
using Eva.UI.Web.Helpers;

namespace Eva.UI.Web.Areas.Painel.Controllers
{
    public class ExtratorNewsController : Controller
    {
        private readonly ExtratorNewsAplicacao extratorNewsApp;

        public ExtratorNewsController()
        {
            extratorNewsApp = Fabrica.ExtratorNewsAplicacaoMongo();
        }

        public ActionResult Index(int? page)
        {
            var numeroDaPagina = page ?? 1;
            var listaPaginada = extratorNewsApp.ListarTodos().ToPagedList(numeroDaPagina, 10);

            return View(listaPaginada);
        }

        public ActionResult Editar(string id)
        {
            if (string.IsNullOrEmpty(id))
                return View(new ExtratorNews());

            var extrator = extratorNewsApp.ListarPorId(id);
            if (extrator == null)
            {
                this.Flash("Extrator News não encontrado!", FlashEnum.Error);
                return View(new ExtratorNews());
            }

            return View(extrator);
        }

        [HttpPost]
        public ActionResult Editar(ExtratorNews extrator)
        {
            if (!ModelState.IsValid)
                return View(extrator);


            extratorNewsApp.Salvar(extrator);
            this.Flash("Extrator News Salvo com Sucesso!");
            return RedirectToAction("Index");
        }

        public JsonResult Excluir(string id)
        {
            extratorNewsApp.Excluir(id);
            return Json("", JsonRequestBehavior.AllowGet);
        }

        public ActionResult VerLista(string id)
        {
            var extrator = Fabrica.ExtratorNewsAplicacaoMongo().ListarPorId(id);
            if (extrator == null)
            {
                return RedirectToAction("Index");
            }

            var html = GetHtml(extrator.Url);
            var query = CQ.Create(html);

            var linhas = query.Select(extrator.SeletorLista);

            var listaNoticiaCrawler = new List<NoticiasCrawler>();

            foreach (var item in linhas)
            {
                var temp = new NoticiasCrawler();

                var elemento = CQ.Create(item.InnerHTML);

                temp.Titulo = elemento.Select(extrator.SeletorTitulo).Text();
                temp.Url = elemento.Select(extrator.SeletorLink).Attr("href");
                listaNoticiaCrawler.Add(temp);
            }

            ViewBag.ExtratorNews = extrator;

            return View(listaNoticiaCrawler);
        }

        public ActionResult VerNoticia(string url, string id)
        {
            ViewBag.Url = url;
            ViewBag.Id = id;

            var extrator = Fabrica.ExtratorNewsAplicacaoMongo().ListarPorId(id);
            var items = Fabrica.ExtratorNewsItemAplicacaoMongo().ListarTodosPorIdExtratorNews(extrator.Id);

            if (!items.Any())
            {
                return RedirectToAction("Index");
            }

            var dados = new ExtratorNewsItem();
            //items filtrar baseado no começo da url
            foreach (var item in items)
            {
                if (url.Contains(item.Url))
                {
                    dados = item;
                    break;
                }
            }

            //mostrar erro
            if (string.IsNullOrEmpty(dados.Url))
            {
                this.Flash("Não foi possivel achar o item extrator para esta URL!", FlashEnum.Error);
                return View(new NoticiasConteudoCrawler());
            }


            if (string.IsNullOrEmpty(url))
                return RedirectToAction("Index");

            var html = GetHtml(url);
            var query = CQ.Create(html);

            var noticia = new NoticiasConteudoCrawler();

            noticia.Autor = query.Select(dados.SeletorAutor).Text();
            noticia.Titulo = query.Select(dados.SeletorTitulo).Text();
            noticia.Conteudo = query.Select(dados.SeletorConteudo).Text();
            noticia.UrlFoto = query.Select(dados.SeletorFoto).Attr("src");



            return View(noticia);
        }

        public ActionResult SalvarNoticia(NoticiasConteudoCrawler item)
        {
            item.Conteudo = item.Conteudo.UnHtml();
            var noticia = new Noticia()
            {
                Titulo = item.Titulo.UnHtml(),
                Conteudo = item.Conteudo,
                Data = DateTime.UtcNow.ToLocalTime(),
                Fonte = new Fonte() { Nome = item.Autor.UnHtml() },
                Resumo = item.Conteudo.Limit(120, " ..."),
                Publicado = false,
                ExibirComentarios = false,
            };

            Fabrica.NoticiaAplicacaoMongo().Salvar(noticia);

            GetImage(item.UrlFoto, noticia.Id);

            return RedirectToAction("Editar", "Noticia", new { id = noticia.Id });
        }

        private void GetImage(string url, string id)
        {
            if (string.IsNullOrEmpty(url))
                return;

            var name = Guid.NewGuid().ToString("N") + ".jpg";

            var imageStream = new WebClient().OpenRead(url);
            var img = Image.FromStream(imageStream);

            img.Save(Imagem.MontaPath("noticia", name));

            var noticia = Fabrica.NoticiaAplicacaoMongo().ListarPorId(id);

            noticia.Arquivos.Add(new Arquivo() { Nome = name, Legenda = noticia.Titulo, Ordem = 1 });
            Fabrica.NoticiaAplicacaoMongo().Salvar(noticia);

            Imagem.GeraArquivosBaseadoEmListaDeTamanhos(name, "noticia", ImagensLayout.Noticias);

        }

        private static string GetHtml(string url)
        {
            string htmlStringSource = string.Empty;
            try
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                request.ContentType = "application/x-www-form-urlencoded";
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0";
                request.Method = "GET";
                request.Timeout = 30000;
                request.ReadWriteTimeout = 30000;
                request.KeepAlive = false;
                // make request for web page
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader htmlSource = new StreamReader(response.GetResponseStream());

                
                htmlStringSource = htmlSource.ReadToEnd();
                response.Close();
            }
            catch (Exception)
            {
                //Todo: mostrar o erro
                htmlStringSource = string.Empty;
            }

            return htmlStringSource;
        }
    }



    public class NoticiasCrawler
    {
        public string Titulo { get; set; }
        public string Url { get; set; }
    }


    public class NoticiasConteudoCrawler
    {
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string Conteudo { get; set; }
        public string UrlFoto { get; set; }
    }

}