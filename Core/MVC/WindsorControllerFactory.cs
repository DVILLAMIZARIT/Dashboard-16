using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.MicroKernel;

// http://docs.castleproject.org/Windsor.Windsor-tutorial-part-two-plugging-Windsor-in.ashx

namespace Core.MVC
{
    public class WindsorControllerFactory : DefaultControllerFactory
    {
        private readonly IKernel kernel;

        public WindsorControllerFactory(IKernel kernel)
        {
            this.kernel = kernel;
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
            {
                throw new HttpException(404, String.Format("The controller for path '{0}' could not be found.", requestContext.HttpContext.Request.Path));
            }
            if (!typeof(IController).IsAssignableFrom(controllerType))
            {
                throw new HttpException(404, String.Format("Invalid controller type for path '{0}', unable to process.", requestContext.HttpContext.Request.Path));
            }
            try
            {
                IController controller = (IController)this.kernel.Resolve(controllerType);
                return this.SetTempDataProvider(controller);
            }
            catch (Exception)
            {
                throw new HttpException(500, String.Format("Unable to resolve controller '{0}' for '{1}'", controllerType, requestContext.HttpContext.Request.Path));
            }
        }

        public override void ReleaseController(IController controller)
        {
            IDisposable disposable = controller as IDisposable;
            if (disposable != null)
            {
                disposable.Dispose();
            }
            this.kernel.ReleaseComponent(controller);
        }

        private IController SetTempDataProvider(IController controller)
        {
            Controller concreteController = controller as Controller;
            if (concreteController != null)
            {
                //concreteController.TempDataProvider = new CustomITempDataProvider();
            }
            return controller;
        }
    }
}
