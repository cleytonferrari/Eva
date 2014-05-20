using System;
using Eva.Dominio;
using Eva.Dominio.Contratos;
using System.Collections.Generic;
using System.Linq;

namespace Eva.Aplicacao
{
    public class NoticiaAplicacao
    {
        private readonly IRepository<Noticia> contexto;

        public NoticiaAplicacao(IRepository<Noticia> repositorio)
        {
            contexto = repositorio;
        }

        public void Salvar(Noticia entidade)
        {
            contexto.Save(entidade);
        }

        public void Excluir(string id)
        {
            contexto.Remove(id);
        }

        public IEnumerable<Noticia> ListarTodos()
        {
            return contexto.GetAll().OrderBy(x => x.Titulo);
        }

        public IEnumerable<Noticia> ListarPublicadas(string zona = "")
        {
            //todo: Setar a GMT baseado em alguma configuração
            var dataAtual = DateTime.Now;
            var retorno = contexto.GetAll().Where(x => x.Data <= dataAtual && x.Publicado);
            
            if (!string.IsNullOrEmpty(zona))
                retorno = retorno.Where(x => String.Equals(x.Zona.Nome, zona, StringComparison.CurrentCultureIgnoreCase));

            return retorno.OrderByDescending(x => x.Data);
        }

        public Noticia ListarPorId(string id)
        {
            return contexto.Get(id);
        }
    }
}
