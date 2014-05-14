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
    public class EventoController : Controller
    {
        private readonly EventoAplicacao eventoApp;
        
        public EventoController()
        {
            eventoApp = Fabrica.EventoAplicacaoMongo();
            

        }
        public ActionResult Index(int? page)
        {
            var numeroDaPagina = page ?? 1;
            var listaPaginada = eventoApp.ListarTodos().OrderByDescending(x => x.Data).ToPagedList(numeroDaPagina, 10);

            return View(listaPaginada);
        }

        public ActionResult Editar(string id)
        {
            if (string.IsNullOrEmpty(id))
                return View(new EventoViewModel());

            var evento = eventoApp.ListarPorId(id);
            if (evento == null)
            {
                this.Flash("Evento não encontrado!", FlashEnum.Error);
                return View(new EventoViewModel());
            }

            var eventoViewModel = new EventoViewModel()
            {
                Id = evento.Id,
                Titulo = evento.Titulo,
                Descricao = evento.Descricao,
                Local = evento.Local,
                Data = evento.Data,
                Creditos = evento.Creditos,
                Publicado = evento.Publicado,
            };

            return View(eventoViewModel);
        }

        [HttpPost]
        public ActionResult Editar(EventoViewModel evento)
        {
            if (!ModelState.IsValid)
            {
                return View(evento);
            }

            var eventoSalvar = new Evento
            {
                Id = evento.Id,
                Titulo = evento.Titulo,
                Descricao = evento.Descricao,
                Local = evento.Local,
                Data = evento.Data,
                Creditos = evento.Creditos,
                Publicado = evento.Publicado,
            };

            if (!string.IsNullOrEmpty(evento.Id))
            {
                var eventoBanco = eventoApp.ListarPorId(evento.Id);
                eventoSalvar.Arquivos = eventoBanco.Arquivos;
                eventoSalvar.Hits = eventoBanco.Hits;
            }


            eventoApp.Salvar(eventoSalvar);
            this.Flash("Evento Salvo com Sucesso!");
            return RedirectToAction("Index");
        }
    }

    public class EventoViewModel
    {
        public string Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Local { get; set; }
        public DateTime Data { get; set; }
        public string Creditos { get; set; }
        public int Hits { get; set; }
        public bool Publicado { get; set; }

    }
}