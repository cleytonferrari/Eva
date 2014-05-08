using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eva.Dominio;
using Eva.Dominio.Contratos;

namespace Eva.Aplicacao
{
    public class LogoAplicacao
    {
        private readonly IRepository<Logo> contexto;

        public LogoAplicacao(IRepository<Logo> repositorio)
        {
            contexto = repositorio;
        }

        public void Salvar(Logo entidade)
        {
            contexto.Save(entidade);
        }

        public IEnumerable<Logo> ListarTodos()
        {
            return contexto.GetAll().OrderBy(x => x.Nome);
        }

        public Logo ListarPorId(string id)
        {
            return contexto.Get(id);
        }
    }
}
