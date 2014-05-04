using Eva.Dominio;
using Eva.Dominio.Contratos;
using System.Collections.Generic;
using System.Linq;

namespace Eva.Aplicacao
{
    public class NoticiaZonaAplicacao
    {
        private readonly IRepository<NoticiaZona> contexto;

        public NoticiaZonaAplicacao(IRepository<NoticiaZona> repositorio)
        {
            contexto = repositorio;
        }

        public void Salvar(NoticiaZona entidade)
        {
            contexto.Save(entidade);
        }

        public IEnumerable<NoticiaZona> ListarTodos()
        {
            return contexto.GetAll().OrderBy(x => x.Nome);
        }

        public NoticiaZona ListarPorId(string id)
        {
            return contexto.Get(id);
        }
    }
}
