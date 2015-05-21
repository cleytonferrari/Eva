using System.Collections.Generic;
using System.Linq;
using Eva.Dominio;
using Eva.Dominio.Contratos;

namespace Eva.Aplicacao
{
    public class ExtratorNewsItemAplicacao
    {
        private readonly IRepository<ExtratorNewsItem> contexto;

        public ExtratorNewsItemAplicacao(IRepository<ExtratorNewsItem> repositorio)
        {
            contexto = repositorio;
        }

        public void Salvar(ExtratorNewsItem entidade)
        {
            contexto.Save(entidade);
        }
        public void Excluir(string id)
        {
            contexto.Remove(id);
        }

        public IEnumerable<ExtratorNewsItem> ListarTodos()
        {
            return contexto.GetAll().OrderBy(x => x.Id);
        }

        public IEnumerable<ExtratorNewsItem> ListarTodosPorIdExtratorNews(string idExtrator)
        {
            return contexto.GetAll().Where(x => x.IdExtratorNews == idExtrator);
        }

        public ExtratorNewsItem ListarPorId(string id)
        {
            return contexto.Get(id);
        }
    }
}
