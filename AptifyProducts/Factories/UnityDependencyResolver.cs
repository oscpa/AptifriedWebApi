﻿using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Web.Mvc;


namespace AptifyWebApi.Factories {
    public class UnityDependencyResolver : IDependencyResolver {
        private IUnityContainer container;

        public UnityDependencyResolver(IUnityContainer container) {
            this.container = container;
        }

        public object GetService(Type serviceType) {
            if (!container.IsRegistered(serviceType)) {
                if (serviceType.IsAbstract || serviceType.IsInterface) {
                    return null;
                }
            }
            return container.Resolve(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType) {
            return container.ResolveAll(serviceType);
        }
    }
}