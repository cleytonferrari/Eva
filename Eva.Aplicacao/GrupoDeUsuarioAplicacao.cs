using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eva.Dominio;
using Eva.Dominio.Contratos;

namespace Eva.Aplicacao
{
    public class GrupoDeUsuarioAplicacao
    {
        private readonly IRepository<GrupoDeUsuario> contexto;

        public GrupoDeUsuarioAplicacao(IRepository<GrupoDeUsuario> repositorio)
        {
            contexto = repositorio;
        }

        public void Salvar(GrupoDeUsuario entidade)
        {
            contexto.Save(entidade);
        }
        public void Excluir(string id)
        {
            contexto.Remove(id);
        }

        public IEnumerable<GrupoDeUsuario> ListarTodos()
        {
            return contexto.GetAll().OrderBy(x => x.Nome);
        }

        public GrupoDeUsuario ListarPorId(string id)
        {
            return contexto.Get(id);
        }

    }
}
