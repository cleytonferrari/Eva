using Eva.Dominio;
using Eva.Dominio.Contratos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eva.Aplicacao
{
    public class EventoAplicacao
    {
        private readonly IRepository<Evento> contexto;

        public EventoAplicacao(IRepository<Evento> repositorio)
        {
            contexto = repositorio;
        }

        public void Salvar(Evento entidade)
        {
            contexto.Save(entidade);
        }

        public void Excluir(string id)
        {
            contexto.Remove(id);
        }

        public IEnumerable<Evento> ListarTodos()
        {
            return contexto.GetAll().OrderBy(x => x.Titulo);
        }

        public Evento ListarPorId(string id)
        {
            return contexto.Get(id);
        }
        public IEnumerable<Evento> ListarPublicadas()
        {
            //todo: Setar a GMT baseado em alguma configuração
            var dataAtual = DateTime.Now;
            var retorno = contexto.GetAll().Where(x => x.Data <= dataAtual && x.Publicado);

            return retorno.OrderByDescending(x => x.Data);
        }


        public Evento Ler(string slug, DateTime data)
        {
            return contexto.GetAll().FirstOrDefault(x => x.Slug == slug && x.Data.ToShortDateString() == data.ToShortDateString());
        }

    }
}
