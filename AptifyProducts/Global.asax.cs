using Aptify.Framework.Web.eBusiness;
using AptifyWebApi.Factories;
using AptifyWebApi.Managers;
using AptifyWebApi.Membership;
using AptifyWebApi.Models;
using AptifyWebApi.Repository;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.Practices.Unity;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;


namespace AptifyWebApi {
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class AptifyWebApiApplication : HttpApplication {

        private UnityContainer container;

        protected void Application_Start() {
            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            FormatterConfig.RegisterFormatters(GlobalConfiguration.Configuration.Formatters);

            // wire up our config for automapper
            AutomapperConfig.InitializeAutoMapper();

            // registers with mvc that when we ask for ISession, to 
            // pull it though our helper class and store it in a per
            // request container.
            this.container = new UnityContainer();
            container.RegisterType<ISession>(
                new PerRequestLifetimeManager(),
                new InjectionFactory(c => {
                    ISessionFactory sessionFactory =
                        NHibernateSessionManager.Instance.CreateSessionFactory();
                    return sessionFactory.OpenSession();
                }
            ));

            // retigster with mvc that when we ask for a controller,
            // then use our own factory to hydrate the controller and 
            // get the request's ISession 
            ControllerBuilder.Current.SetControllerFactory(
                new UnityControllerFactory(this.container));

            GlobalConfiguration.Configuration.Services.Replace(
                typeof(IHttpControllerActivator),
                new UnityControllerFactory(this.container));

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            GlobalConfiguration.Configuration.MessageHandlers.Add(
                new AptifriedAuthenticationDelegatingHandler());

            // TODO: This should be remove and made localonly when we roll out to production
            GlobalConfiguration.Configuration.IncludeErrorDetailPolicy = 
                IncludeErrorDetailPolicy.Always;
            // exception stack 
            
        }
        
        protected void Application_EndRequest(object sender, EventArgs e) {
            Object sessionObject = HttpContext.Current.Items[PerRequestLifetimeManager.Key];
            if (sessionObject != null) {
                ISession currentSession = sessionObject as ISession;
                if (currentSession != null) {
                    currentSession.Close();
                }
            }
        }         
    }
}