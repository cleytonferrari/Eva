using Eva.Dominio;
using Eva.Dominio.Contratos;
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

        public IEnumerable<Evento> ListarTodos()
        {
            return contexto.GetAll().OrderBy(x => x.Titulo);
        }

        public Evento ListarPorId(string id)
        {
            return contexto.Get(id);
        }
    }
}
