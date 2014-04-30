using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eva.Dominio.Contratos;
using MongoDB.Driver.Linq;

namespace Eva.Repositorio.MongoDb
{
    public class Repositorio<T> : IRepository<T> where T : class
    {
        private readonly Contexto<T> contexto;

        public Repositorio()
        {
            contexto = new Contexto<T>();
        }

        public void Save(T entity)
        {
            contexto.Collection.Save(entity);
        }

        public void Remove(string id)
        {
            contexto.Collection.Remove(MongoDB.Driver.Builders.Query.EQ("_id", id));
        }

        public T Get(string id)
        {
            return contexto.Collection.FindOneByIdAs<T>(id);
        }

        public IEnumerable<T> GetAll()
        {
            return contexto.Collection.AsQueryable();
        }

        public IEnumerable<T> GetByFilter(Func<T, bool> filter)
        {
            return contexto.Collection.AsQueryable().Where(filter);
        }
    }
}
