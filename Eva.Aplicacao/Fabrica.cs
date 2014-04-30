using Eva.Dominio;

namespace Eva.Aplicacao
{
    public class Fabrica
    {
        public static UsuarioAplicacao PalestraAplicacaoMongo()
        {
            return new UsuarioAplicacao(new Repositorio.MongoDb.Repositorio<Usuario>());
        }
    }
}
