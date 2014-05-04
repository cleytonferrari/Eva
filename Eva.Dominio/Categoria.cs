
namespace Eva.Dominio
{
    public class Categoria : Entidade
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
        public string CorTexto { get; set; }
        public string CorFundo { get; set; }
    }
}
