using System;
using System.Collections.Generic;

namespace Eva.Dominio.Contratos
{
    public interface IRepository<T> where T : class
    {

        void Save(T entity);

        void Remove(string id);

        T Get(string id);

        IEnumerable<T> GetAll();

        IEnumerable<T> GetByFilter(Func<T, bool> filter);
    }
}
