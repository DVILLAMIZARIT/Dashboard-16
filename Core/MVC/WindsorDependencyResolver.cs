using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Castle.Windsor;

namespace Core.MVC
{
    public class WindsorDependencyResolver : IDependencyResolver
    {
        private readonly IWindsorContainer container;

        public WindsorDependencyResolver()
            : this(IoC.Instance)
        {
        }
        public WindsorDependencyResolver(IWindsorContainer container)
        {
            this.container = container;
        }

        public Object GetService(Type serviceType)
        {
            return this.container.Kernel.HasComponent(serviceType) ? this.container.Resolve(serviceType) : null;
        }

        public IEnumerable<Object> GetServices(Type serviceType)
        {
            return this.container.Kernel.HasComponent(serviceType) ? this.container.ResolveAll(serviceType).Cast<Object>() : Enumerable.Empty<Object>();
        }
    }
}
