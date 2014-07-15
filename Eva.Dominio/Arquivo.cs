using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eva.Dominio
{
    public class Arquivo : Entidade
    {
        public string Nome { get; set; }
        public string Legenda { get; set; }
        public int Ordem { get; set; }
    }
}
