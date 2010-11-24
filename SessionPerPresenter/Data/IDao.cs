using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SessionPerPresenter.Data
{
    public interface IDao<TEntity> : IDisposable where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
    }
}
