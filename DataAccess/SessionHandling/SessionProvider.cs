using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;

namespace DataAccess.SessionHandling
{
    public class SessionProvider : ISessionProvider
    {
        private readonly ISessionFactory _sessionFactory;
        private ISession _currentSession;
        
        public SessionProvider(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }
        
        public ISession GetCurrentSession()
        {
            if (null == _currentSession)
            {
                _currentSession = _sessionFactory.OpenSession();
            }
            return _currentSession;
        }
        
        public void DisposeCurrentSession()
        {
            _currentSession.Dispose();
            _currentSession = null;
        }
        
        public void Dispose()
        {
            if(_currentSession != null)
            {
                _currentSession.Dispose();
            }
            _currentSession = null;
        }
    }
}
