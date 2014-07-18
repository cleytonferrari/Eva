using System;
using Eva.Dominio;
using Eva.Dominio.Contratos;
using System.Collections.Generic;
using System.Linq;

namespace Eva.Aplicacao
{
    public class BannerZonaAplicacao
    {
        private readonly IRepository<BannerZona> contexto;

        public BannerZonaAplicacao(IRepository<BannerZona> repositorio)
        {
            contexto = repositorio;
        }

        public void Salvar(BannerZona entidade)
        {
            contexto.Save(entidade);
        }
        public void Excluir(string id)
        {
            contexto.Remove(id);
        }

        public IEnumerable<BannerZona> ListarTodos()
        {
            return contexto.GetAll().OrderBy(x => x.Nome);
        }

        public BannerZona ListarPorNome(string nome)
        {
            return contexto.GetByFilter(x => String.Equals(x.Nome, nome, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
        }

        public BannerZona ListarPorId(string id)
        {
            return contexto.Get(id);
        }
    }
}
