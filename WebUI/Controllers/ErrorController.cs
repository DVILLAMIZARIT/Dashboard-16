using System;
using System.Web.Mvc;
using AttributeRouting;
using AttributeRouting.Web.Mvc;
using WebUI.Models.Error;

namespace WebUI.Controllers
{
    [AllowAnonymous, RoutePrefix("Error")]
    public class ErrorController : Controller
    {
        [Route("General", RouteName = "Error_General")]
        public ActionResult General(Exception exception)
        {
            ErrorDetails model = new ErrorDetails
            {
                Exception = exception
            };
            return View(model);
        }

        [Route("403", RouteName = "Error_403")]
        public ActionResult HttpError403(Exception exception)
        {
            this.Response.StatusCode = 403;
            this.Response.StatusDescription = "Forbidden";
            ErrorDetails model = new ErrorDetails
            {
                Exception = exception
            };
            return View(model);
        }

        [Route("404", RouteName = "Error_404")]
        public ActionResult HttpError404(Exception exception)
        {
            this.Response.StatusCode = 404;
            this.Response.StatusDescription = "Not Found";
            ErrorDetails model = new ErrorDetails
            {
                Exception = exception
            };
            return View(model);
        }

        [Route("500", RouteName = "Error_500")]
        public ActionResult HttpError500(Exception exception)
        {
            this.Response.StatusCode = 500;
            this.Response.StatusDescription = "Internal Server Error";
            ErrorDetails model = new ErrorDetails
            {
                Exception = exception
            };
            return View(model);
        }
    }
}
