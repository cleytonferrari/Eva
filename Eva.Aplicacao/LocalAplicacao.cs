using Eva.Dominio;
using Eva.Dominio.Contratos;
using System.Collections.Generic;
using System.Linq;

namespace Eva.Aplicacao
{
    public class LocalAplicacao
    {
        private readonly IRepository<Local> contexto;

        public LocalAplicacao(IRepository<Local> repositorio)
        {
            contexto = repositorio;
        }

        public void Salvar(Local entidade)
        {
            contexto.Save(entidade);
        }
        public void Excluir(string id)
        {
            contexto.Remove(id);
        }

        public IEnumerable<Local> ListarTodos()
        {
            return contexto.GetAll().OrderBy(x => x.Nome);
        }

        public Local ListarPorNome(string nome)
        {
            return contexto.GetByFilter(x => x.Nome.ToLower() == nome.ToLower()).FirstOrDefault();
        }

        public Local ListarPorId(string id)
        {
            return contexto.Get(id);
        }
    }
}
