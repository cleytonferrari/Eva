
namespace Eva.Dominio
{
    public class Local:Entidade
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
    }
}
