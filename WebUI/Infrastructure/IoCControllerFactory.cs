using System;
using System.Web;
using System.Web.Mvc;
using Castle.MicroKernel;

namespace WebUI.Infrastructure
{
    public class IoCControllerFactory : DefaultControllerFactory
    {
        private readonly IKernel kernel;

        public IoCControllerFactory(IKernel kernel)
        {
            this.kernel = kernel;
        }

        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
            {
                throw new HttpException(404, String.Format("the controller for path '{0}' could not be found.", requestContext.HttpContext.Request.Path));
            }
            return (IController)this.kernel.Resolve(controllerType);
        }

        public override void ReleaseController(IController controller)
        {
            this.kernel.ReleaseComponent(controller);
        }
    }
}