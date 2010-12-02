using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;

namespace DataAccess.SessionHandling
{
    public interface ISessionProvider : IDisposable
    {
        ISession GetCurrentSession();
        void DisposeCurrentSession();
    }
}
