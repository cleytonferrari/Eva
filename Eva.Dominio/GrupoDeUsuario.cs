using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eva.Dominio
{
    public class GrupoDeUsuario : Entidade
    {
        public string Nome { get; set; }

        public IEnumerable<string> Permissoes { get; set; }
    }
}
