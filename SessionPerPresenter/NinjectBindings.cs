using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess;
using DataAccess.SessionHandling;
using Ninject.Extensions.NamedScope;
using Ninject.Modules;

namespace SessionPerPresenter
{
    public class NinjectBindings : NinjectModule
    {
        public override void Load()
        {
            const string presenterScope = "PresenterScope";
            var asm = GetType().Assembly;
            
            var presenters = from t in asm.GetTypes()
                             where typeof (IPresenter).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract
                             select t;
            
            foreach (var presenterType in presenters)
            {
                Kernel.Bind(presenterType).ToSelf().DefinesNamedScope(presenterScope);
            }

            Kernel.Bind<ISessionProvider>().To<SessionProvider>().InNamedScope(presenterScope);
            
            Kernel.Bind(typeof(IDAO<>)).To(typeof(DAOBase<>));
        }
    }
}
