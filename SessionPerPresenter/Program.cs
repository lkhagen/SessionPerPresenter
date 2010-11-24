using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess.SessionHandling;
using NHibernate;
using Ninject;
using Ninject.Modules;
using SessionPerPresenter.Data;

namespace SessionPerPresenter
{
    class Program
    {
        static void Main(string[] args)
        {
            var sessionFactory = SessionFactory.Instance.GetSessionFactory();
            var kernel = new StandardKernel();

            kernel.Load(new NinjectBindings());

            kernel.Bind<ISessionFactory>().ToConstant(sessionFactory);

            var customerPresenter = kernel.Get<CustomerPresenter>();
            var productPresenter = kernel.Get<ProductPresenter>();

            customerPresenter.ShowAllCustomers().Show();
            productPresenter.ShowAllProducts().Show();

            customerPresenter.Dispose();
            productPresenter.Dispose();

            Console.WriteLine("Press any key");
            Console.ReadKey();
        }
    }
}
