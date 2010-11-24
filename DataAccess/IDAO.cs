using System;
using System.Collections.Generic;

namespace DataAccess
{
    //public interface IDao<TEntity> : IDisposable where TEntity : class
    //public interface IDAO<T>
    public interface IDAO<T> : IDisposable where T : class
    {
        T GetById(int id);
        T SaveOrUpdate(T entity);
        T Delete(T entity);

        IEnumerable<T> GetAll(); 
    }
}