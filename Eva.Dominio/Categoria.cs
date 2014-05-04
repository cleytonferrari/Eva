using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eva.Dominio
{
    public class Categoria : Entidade
    {
        public string Nome { get; set; }

        public string Slug { get { return Nome.ToSlug(); } }

        public string CorTexto { get; set; }
        public string CorFundo { get; set; }
    }
}
