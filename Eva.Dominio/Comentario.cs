using System;

namespace Eva.Dominio
{
    public class Comentario
    {
        public string Autor { get; set; }
        public string Email { get; set; }
        public string Mensagem { get; set; }
        public string Ip { get; set; }
        public DateTime Data { get; set; }
        public bool Publicado { get; set; }
    }
}
