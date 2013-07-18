using AptifyWebApi.Membership;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Principal;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;
using System.Web.Routing;

namespace AptifyWebApi.Factories{
    public class UnityControllerFactory : DefaultControllerFactory, IHttpControllerActivator {

        IUnityContainer container;

        public UnityControllerFactory(IUnityContainer container) {
            this.container = container;
        }

        protected override IController GetControllerInstance
            (RequestContext requestContext, Type controllerType) {
            
            IController controller = null;
            if (controllerType != null) {
                controller = this.container.Resolve(controllerType) as IController;
                
            }
            return controller;
        }


        IHttpController IHttpControllerActivator.Create(
            HttpRequestMessage request, 
            HttpControllerDescriptor controllerDescriptor, 
            Type controllerType) {
            
                IHttpController controller = null;
                if (controllerType != null) {
                    controller = this.container.Resolve(controllerType) as IHttpController;
                }
                return controller;
        }
    }
}