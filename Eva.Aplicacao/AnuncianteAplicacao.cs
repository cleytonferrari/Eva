using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eva.Dominio;
using Eva.Dominio.Contratos;

namespace Eva.Aplicacao
{
    public class AnuncianteAplicacao
    {
        private readonly IRepository<Anunciante> contexto;

        public AnuncianteAplicacao(IRepository<Anunciante> repositorio)
        {
            contexto = repositorio;
        }

        public void Salvar(Anunciante entidade)
        {
            contexto.Save(entidade);
        }
        public void Excluir(string id)
        {
            contexto.Remove(id);
        }

        public IEnumerable<Anunciante> ListarTodos()
        {
            return contexto.GetAll().OrderBy(x => x.Nome);
        }

        public Anunciante ListarPorId(string id)
        {
            return contexto.Get(id);
        }
    }
}
