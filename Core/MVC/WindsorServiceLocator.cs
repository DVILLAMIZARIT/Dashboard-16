using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Castle.Windsor;

namespace Core.MVC
{
    public class WindsorServiceLocator : IDependencyResolver
    {
        private readonly IWindsorContainer container;

        public WindsorServiceLocator()
            : this(IoC.Instance)
        {
        }
        public WindsorServiceLocator(IWindsorContainer container)
        {
            this.container = container;
        }

        public Object GetService(Type serviceType)
        {
            return this.container.Resolve(serviceType);
        }

        public IEnumerable<Object> GetServices(Type serviceType)
        {
            return this.container.ResolveAll(serviceType).OfType<Object>().AsEnumerable();
        }
    }
}
