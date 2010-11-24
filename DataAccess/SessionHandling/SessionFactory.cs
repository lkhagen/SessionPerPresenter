using DataObjects.Entities;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace DataAccess.SessionHandling
{
    /// <summary>
    /// Singleton class containing the NHibernate SessionFactory.
    /// Keeps a single instance of the expensive SessionFactory object.
    /// Provides methods relevant to SessionFactory usage, such as OpenSession.
    /// </summary>
    public class SessionFactory
    {
        private ISessionFactory _sessionFactoryNHibernate;
        private Configuration _configuration;


        /// <summary>
        /// Private constructor to enforce singleton
        /// </summary>
        private SessionFactory()
        {
            BuildConfiguration();
            BuildSessionFactoryNHibernate();
        }

        /// <summary>
        /// Open a new session from the NHibernate SessionFactory
        /// </summary>
        /// <returns></returns>
        public ISession OpenSession()
        {
            return _sessionFactoryNHibernate.OpenSession();
        }

        // !!!! NEW !!!!
        public ISessionFactory GetSessionFactory()
        {
            return _sessionFactoryNHibernate;
        }

        /// <summary>
        /// Export the schema of the mapped model to the configured database.
        /// </summary>
        public void ExportSchema()
        {
            SchemaExport schemaExporter = new SchemaExport(_configuration);
            
            // Export schema ( no script, export to DB)
            schemaExporter.Create(false, true);
        }

        /// <summary>
        /// Builds the NHibernate SessionFactory and assigns it to instance variable.
        /// </summary>
        private void BuildSessionFactoryNHibernate()
        {
            if (_sessionFactoryNHibernate == null)
            {
                _sessionFactoryNHibernate = _configuration.BuildSessionFactory();
            }
        }

        /// <summary>
        /// Builds the NHibernate Configuration and assigns it to the instance variable.
        /// </summary>
        private void BuildConfiguration()
        {
            _configuration = Fluently.Configure()
                .Database(MySQLConfiguration.Standard.ConnectionString
                              (c => c.Database("nh3test")
                                        .Username("root")
                                        .Password("enginering")
                                        .Server("localhost")))
                .Mappings(x => x.FluentMappings.AddFromAssemblyOf<Customer>())
            .BuildConfiguration();
        }


        // ------------------------------------------------------------------
        /// <summary>
        /// This is a thread-safe, lazy singleton.  See http://www.yoda.arachsys.com/csharp/singleton.html
        /// for more details about its implementation.
        /// </summary>
        public static SessionFactory Instance
        {
            get
            {
                return Nested.SessionFactory;
            }
        }

        /// <summary>
        /// Assists with ensuring thread-safe, lazy singleton
        /// </summary>
        private class Nested
        {
            static Nested() { }
            internal static readonly SessionFactory SessionFactory = new SessionFactory();
        }
    }
}
