using System;
using System.Collections.Generic;
using DataAccess.SessionHandling;

namespace DataAccess
{
    //public abstract class DAOBase<T> : IDAO<T>
    public class DAOBase<T> : IDAO<T> where T : class
    {
        private readonly ISessionProvider _sessionProvider;

        public DAOBase(ISessionProvider sessionProvider)
        {
            _sessionProvider = sessionProvider;
            Console.WriteLine("Session hashCode: " + _sessionProvider.GetCurrentSession().GetHashCode());
        }

        public T GetById(int id)
        {
            var session = _sessionProvider.GetCurrentSession();
            return session.Get<T>(id);
        }

        public T SaveOrUpdate(T entity)
        {
            var session = _sessionProvider.GetCurrentSession();
            session.SaveOrUpdate(entity);
            return entity;
        }

        public T Delete(T entity)
        {
            var session = _sessionProvider.GetCurrentSession();
            session.Delete(entity);
            return entity;
        }

        public IEnumerable<T> GetAll()
        {
            var session = _sessionProvider.GetCurrentSession();
            return session.CreateCriteria(typeof(T)).List<T>();
        }

        public void Dispose()
        {
            _sessionProvider.Dispose();
        }
    }
}
