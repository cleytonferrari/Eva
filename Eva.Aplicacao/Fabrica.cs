using Eva.Dominio;

namespace Eva.Aplicacao
{
    public class Fabrica
    {
        public static UsuarioAplicacao UsuarioAplicacaoMongo()
        {
            return new UsuarioAplicacao(new Repositorio.MongoDb.Repositorio<Usuario>());
        }

        public static GrupoDeUsuarioAplicacao GrupoDeUsuarioAplicacaoMongo()
        {
            return new GrupoDeUsuarioAplicacao(new Repositorio.MongoDb.Repositorio<GrupoDeUsuario>());
        }

        public static CategoriaAplicacao CategoriaAplicacaoMongo()
        {
            return new CategoriaAplicacao(new Repositorio.MongoDb.Repositorio<Categoria>());
        }
    }
}
