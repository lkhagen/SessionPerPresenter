using System;
using System.Collections.Generic;

namespace DataAccess
{
    //public interface IDAO<T>
    public interface IDAO<T> : IDisposable where T : class
    {
        T GetById(int id);
        T SaveOrUpdate(T entity);
        T Delete(T entity);

        IEnumerable<T> GetAll(); 
    }
}