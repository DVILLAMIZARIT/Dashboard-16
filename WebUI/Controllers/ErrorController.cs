using System;
using System.Web.Mvc;
using AttributeRouting;
using AttributeRouting.Web.Mvc;
using WebUI.Models.Error;

namespace WebUI.Controllers
{
    [RoutePrefix("Error")]
    public class ErrorController : Controller
    {
        [Route("General", RouteName = "Error_General")]
        public ActionResult General(Exception exception)
        {
            //StringBuilder sb = new StringBuilder();
            //sb.AppendLine("General Error").AppendLine();
            //String indent = String.Empty;
            //while (exception != null)
            //{
            //    indent += "\t";
            //    sb.Append(indent).AppendLine("Exception:")
            //        .Append(indent).AppendLine(exception.Message)
            //        .AppendLine();
            //    sb.Append(indent).AppendLine("Stack Trace:")
            //        .Append(indent).AppendLine(exception.StackTrace)
            //        .AppendLine();

            //    exception = exception.InnerException;
            //}
            ErrorDetails model = new ErrorDetails
            {
                Code = 500,
                Exception = exception
            };
            return View(model);
        }

        [Route("HttpError404", RouteName = "Error_404")]
        public ActionResult HttpError404(Exception exception)
        {
            return Content("404: File not found" + (exception != null ? Environment.NewLine + exception.Message : String.Empty));
        }

        [Route("HttpError500", RouteName = "Error_500")]
        public ActionResult HttpError500(Exception exception)
        {
            return Content("500: Server error" + (exception != null ? Environment.NewLine + exception.Message : String.Empty));
        }
    }
}
