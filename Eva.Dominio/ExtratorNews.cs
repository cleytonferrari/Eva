using System.Collections.Generic;

namespace Eva.Dominio
{
    public class ExtratorNews : Entidade
    {
        public string Nome { get; set; }

        public string Url { get; set; }

        public string SeletorLista { get; set; }

        public string SeletorTitulo { get; set; }

        public string SeletorLink { get; set; }
    }
}
