using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.IO;

namespace AptifyWebApi.Repository {
    public sealed class NHibernateSessionManager {

        private const string TRANSACTION_KEY = "CONTEXT_TRANSACTION";
        private const string SESSION_KEY = "CONTEXT_SESSION";

        private sealed class nested {
            static nested() { }
            internal static readonly NHibernateSessionManager NhibernateSessionManager =
                new NHibernateSessionManager();
        }

        public static NHibernateSessionManager Instance {
            get {
                return nested.NhibernateSessionManager;
            }
        }



        private ISessionFactory _factory;
        public ISessionFactory CreateSessionFactory() {
            if (_factory == null) {

                _factory = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2008
                    .ConnectionString(c => 
                        c.Server(ConfigurationManager.AppSettings["AptifyDBServer"])
                        .Database(ConfigurationManager.AppSettings["AptifyEntitiesDB"])
                        .TrustedConnection()))
                .Mappings(x => x
                    .FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly()))
                .BuildSessionFactory();

            }

            return _factory;
        }

    }
}