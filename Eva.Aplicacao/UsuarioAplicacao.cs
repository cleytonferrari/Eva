using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eva.Dominio;
using Eva.Dominio.Contratos;

namespace Eva.Aplicacao
{
    public class UsuarioAplicacao
    {
        private readonly IRepository<Usuario> contexto;
        
        public UsuarioAplicacao(IRepository<Usuario> repositorio )
        {
            contexto = repositorio;
        }

        public void Salvar(Usuario entidade)
        {
            contexto.Save(entidade);
        }

        public IEnumerable<Usuario> ListarTodos()
        {
            return contexto.GetAll();
        }
    }
}
