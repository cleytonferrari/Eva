using Eva.Dominio;
using Eva.Dominio.Contratos;
using System.Collections.Generic;
using System.Linq;

namespace Eva.Aplicacao
{
    public class FonteAplicacao
    {
        private readonly IRepository<Fonte> contexto;

        public FonteAplicacao(IRepository<Fonte> repositorio)
        {
            contexto = repositorio;
        }

        public void Salvar(Fonte entidade)
        {
            contexto.Save(entidade);
        }

        public IEnumerable<Fonte> ListarTodos()
        {
            return contexto.GetAll().OrderBy(x => x.Nome);
        }

        public Fonte ListarPorNome(string nome)
        {
            return contexto.GetByFilter(x => x.Nome.ToLower() == nome.ToLower()).FirstOrDefault();
        }

        public Fonte ListarPorId(string id)
        {
            return contexto.Get(id);
        }
    }
}
