using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Windsor;

namespace Core
{
    public static class IoC
    {
        private static readonly Object lockObject = new Object();
        private static IWindsorContainer instance = new WindsorContainer();

        public static IWindsorContainer Instance
        {
            get { return instance; }
            set
            {
                lock (lockObject)
                {
                    instance = value;
                }
            }
        }

        public static Object Resolve(Type service)
        {
            return instance.Resolve(service);
        }

        public static T Resolve<T>()
        {
            return instance.Resolve<T>();
        }

        public static IEnumerable<Object> ResolveAll(Type service)
        {
            return instance.ResolveAll(service).OfType<Object>().AsEnumerable();
        }

        public static IEnumerable<T> ResolveAll<T>()
        {
            return instance.ResolveAll<T>();
        }
    }
}
