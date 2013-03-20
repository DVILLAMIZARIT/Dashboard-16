using System;
using System.Web.Mvc;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Core;
using Core.MVC;

// http://stackoverflow.com/questions/5124393/setting-up-inversion-of-control-ioc-in-asp-net-mvc-with-castle-windsor
// http://prashantbrall.wordpress.com/2010/11/22/service-locator-pattern-with-windsor-castle/

namespace WebUI.Infrastructure
{
    public class BootstrapContainer : IDisposable
    {
        private IWindsorContainer container;

        public BootstrapContainer()
        {
            this.container = IoC.Instance.Install(FromAssembly.This());

            // http://stackoverflow.com/questions/6810438/dependencyresolver-vs-controllerfactory
            // see also the "what about IControlleractivator" note on: http://docs.castleproject.org/Windsor.Windsor-tutorial-part-two-plugging-Windsor-in.ashx
            //DependencyResolver.SetResolver(dependancyResolver);
            // tl;dr
            // Use the controller factory over the dependency resolver. though the dependancy resolver provides a higher
            // level of abstraction (includes actionfilters and more) it doesn't provide a way to perform cleanup. So,
            // until this gets changed we stick with the controllerfactory.
            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(this.container.Kernel));
        }

        ~BootstrapContainer()
        {
            this.Dispose(false);
            GC.SuppressFinalize(this);
        }

        #region IDisposable

        private Boolean disposed;
        
        public void Dispose()
        {
            this.Dispose(true);
        }

        private void Dispose(Boolean disposing)
        {
            if (!disposed && disposing)
            {
                this.disposed = true;
                if (this.container != null)
                {
                    this.container.Dispose();
                }
            }
        }

        #endregion
    }
}