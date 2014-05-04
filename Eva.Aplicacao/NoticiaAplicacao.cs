using Eva.Dominio;
using Eva.Dominio.Contratos;
using System.Collections.Generic;
using System.Linq;

namespace Eva.Aplicacao
{
    public class NoticiaAplicacao
    {
        private readonly IRepository<Noticia> contexto;

        public NoticiaAplicacao(IRepository<Noticia> repositorio)
        {
            contexto = repositorio;
        }

        public void Salvar(Noticia entidade)
        {
            contexto.Save(entidade);
        }

        public IEnumerable<Noticia> ListarTodos()
        {
            return contexto.GetAll().OrderBy(x => x.Titulo);
        }

        public Noticia ListarPorId(string id)
        {
            return contexto.Get(id);
        }
    }
}
