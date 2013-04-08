using System;
using System.Text;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class ErrorController : Controller
    {
        //
        // GET: /Error/General
        public ActionResult General(Exception exception)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("General Error").AppendLine();
            String indent = String.Empty;
            while (exception != null)
            {
                indent += "\t";
                sb.Append(indent).AppendLine("Exception:")
                    .Append(indent).AppendLine(exception.Message)
                    .AppendLine();
                sb.Append(indent).AppendLine("Stack Trace:")
                    .Append(indent).AppendLine(exception.StackTrace)
                    .AppendLine();

                exception = exception.InnerException;
            }
            return Content(sb.ToString(), "text/plain");
        }

        //
        // GET: /Error/HttpError404
        public ActionResult HttpError404(Exception exception)
        {
            return Content("404: File not found" + (exception != null ? Environment.NewLine + exception.Message : String.Empty));
        }

        //
        // GET: /Error/HttpError500
        public ActionResult HttpError500(Exception exception)
        {
            return Content("500: Server error" + (exception != null ? Environment.NewLine + exception.Message : String.Empty));
        }
    }
}
