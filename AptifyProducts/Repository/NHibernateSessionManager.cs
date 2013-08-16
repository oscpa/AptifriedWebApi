#region using

using System.Configuration;
using System.Reflection;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;

#endregion

namespace AptifyWebApi.Repository
{
    public sealed class NHibernateSessionManager
    {
        private const string TRANSACTION_KEY = "CONTEXT_TRANSACTION";
        private const string SESSION_KEY = "CONTEXT_SESSION";


        private ISessionFactory _factory;

        public static NHibernateSessionManager Instance
        {
            get { return nested.NhibernateSessionManager; }
        }

        public ISessionFactory CreateSessionFactory()
        {
            return _factory ?? (_factory = Fluently.Configure()
                                                   .Database(MsSqlConfiguration.MsSql2008
                                                                               .ConnectionString(c =>
                                                                                                 c.Server(
                                                                                                     ConfigurationManager
                                                                                                         .AppSettings[
                                                                                                             "AptifyDBServer"
                                                                                                         ])
                                                                                                  .Database(
                                                                                                      ConfigurationManager
                                                                                                          .AppSettings[
                                                                                                              "AptifyEntitiesDB"
                                                                                                          ])
                                                                                                  .TrustedConnection()))
                                                   .Mappings(x => x
                                                                      .FluentMappings.AddFromAssembly(
                                                                          Assembly.GetExecutingAssembly()))
                                                   .BuildSessionFactory());
        }

        private sealed class nested
        {
            internal static readonly NHibernateSessionManager NhibernateSessionManager =
                new NHibernateSessionManager();

            static nested()
            {
            }
        }
    }
}