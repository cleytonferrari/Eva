namespace Eva.Dominio
{
    public class ExtratorNewsItem:Entidade
    {
        public string Url { get; set; }

        public string SeletorAutor { get; set; }

        public string SeletorTitulo { get; set; }

        public string SeletorConteudo { get; set; }

        public string SeletorFoto { get; set; }

        public string IdExtratorNews { get; set; }
    }
}
