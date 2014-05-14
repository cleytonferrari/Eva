using System;
using System.Collections.Generic;

namespace Eva.Dominio
{
    public class Evento : Entidade
    {
         public Evento()
        {
            Arquivos = new List<Arquivo>();
        }
        private string titulo;
        public string Titulo
        {
            get { return titulo; }
            set
            {
                Slug = value.ToSlug();
                titulo = value;
            }
        }
        public string Slug { get; set; }
        public string Descricao { get; set; }
        public string Local { get; set; }
        public DateTime Data { get; set; }
        public string Creditos { get; set; }
        public int Hits { get; set; }
        public bool Publicado { get; set; }
        public List<Arquivo> Arquivos { get; set; }

    }
}
