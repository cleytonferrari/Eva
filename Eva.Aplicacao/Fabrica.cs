using System;
using System.Collections.Generic;
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

        public static FonteAplicacao FonteAplicacaoMongo()
        {
            return new FonteAplicacao(new Repositorio.MongoDb.Repositorio<Fonte>());
        }

        public static LocalAplicacao LocalAplicacaoMongo()
        {
            return new LocalAplicacao(new Repositorio.MongoDb.Repositorio<Local>());
        }

        public static NoticiaZonaAplicacao NoticiaZonaAplicacaoMongo()
        {
            return new NoticiaZonaAplicacao(new Repositorio.MongoDb.Repositorio<NoticiaZona>());
        }

        public static NoticiaAplicacao NoticiaAplicacaoMongo()
        {
            return new NoticiaAplicacao(new Repositorio.MongoDb.Repositorio<Noticia>());
        }

        public static LogoAplicacao LogoAplicacaoMongo()
        {
            return new LogoAplicacao(new Repositorio.MongoDb.Repositorio<Logo>());
        }

        public static EventoAplicacao EventoAplicacaoMongo()
        {
            return new EventoAplicacao(new Repositorio.MongoDb.Repositorio<Evento>());
        }

    }
}
