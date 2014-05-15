using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eva.Dominio;
using Eva.Dominio.Contratos;

namespace Eva.Aplicacao
{
    public class CategoriaAplicacao
    {
        private readonly IRepository<Categoria> contexto;

        public CategoriaAplicacao(IRepository<Categoria> repositorio)
        {
            contexto = repositorio;
        }

        public void Salvar(Categoria entidade)
        {
            contexto.Save(entidade);
        }
        public void Excluir(string id)
        {
            contexto.Remove(id);
        }

        public IEnumerable<Categoria> ListarTodos()
        {
            return contexto.GetAll().OrderBy(x => x.Nome);
        }

        public Categoria ListarPorId(string id)
        {
            return contexto.Get(id);
        }
    }
}
