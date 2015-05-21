using System.Collections.Generic;
using System.Linq;
using Eva.Dominio;
using Eva.Dominio.Contratos;

namespace Eva.Aplicacao
{
    public class ExtratorNewsAplicacao
    {
        private readonly IRepository<ExtratorNews> contexto;

        public ExtratorNewsAplicacao(IRepository<ExtratorNews> repositorio)
        {
            contexto = repositorio;
        }

        public void Salvar(ExtratorNews entidade)
        {
            contexto.Save(entidade);
        }
        public void Excluir(string id)
        {
            contexto.Remove(id);
        }

        public IEnumerable<ExtratorNews> ListarTodos()
        {
            return contexto.GetAll().OrderBy(x => x.SeletorLink);
        }

        public ExtratorNews ListarPorId(string id)
        {
            return contexto.Get(id);
        }
    }
}
