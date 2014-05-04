using System;

namespace Eva.Dominio
{
    public class Noticia : Entidade
    {
        public string Titulo { get; set; }
        public string Slug { get { return Titulo.ToSlug(); } }
        public Categoria Categoria { get; set; }
        public ZonaNoticia Zona { get; set; }
        public string Antetitulo { get; set; }
        public string Resumo { get; set; }
        public string Conteudo { get; set; }
        public DateTime Data { get; set; }
        public Fonte Fonte { get; set; }
        public bool Publicado { get; set; }
        public int Hits { get; set; }
        public bool Comentarios { get; set; }
    }

    
}
