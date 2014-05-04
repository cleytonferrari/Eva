using System;

namespace Eva.Dominio
{
    public class Evento : Entidade
    {
        private string nome;
        public string Nome
        {
            get { return nome; }
            set
            {
                Slug = value.ToSlug();
                nome = value;
            }
        }
        public string Slug { get; set; }
        public string Descricao { get; set; }
        public string Local { get; set; }
        public DateTime Data { get; set; }
        public string Creditos { get; set; }
        public int Hits { get; set; }
        public bool Publicado { get; set; }

    }
}
