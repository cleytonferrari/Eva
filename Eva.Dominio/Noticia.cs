﻿using System;
using System.Collections.Generic;

namespace Eva.Dominio
{
    public class Noticia : Entidade
    {
        public Noticia()
        {
            Data = new DateTime();
            Arquivos = new List<Arquivo>();
            Categoria = new Categoria();
            Local = new Local();
            Fonte = new Fonte();
            Zona = new NoticiaZona();
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
        public Categoria Categoria { get; set; }
        public NoticiaZona Zona { get; set; }
        public Local Local { get; set; }
        public string Antetitulo { get; set; }
        public string Resumo { get; set; }
        public string Conteudo { get; set; }
        public DateTime Data { get; set; }
        public Fonte Fonte { get; set; }
        public bool Publicado { get; set; }
        public int Hits { get; set; }
        public bool ExibirComentarios { get; set; }
        public IEnumerable<Comentario> Comentarios { get; set; }
        public List<Arquivo> Arquivos { get; set; }
    }

    
}
