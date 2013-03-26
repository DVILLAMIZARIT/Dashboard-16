using System;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult General(Exception exception)
        {
            return Content("General Error");
        }

        //
        // GET: /Error/HttpError404
        public ActionResult HttpError404(Exception exception)
        {
            return Content("404: File not found");
        }

        //
        // GET: /Error/HttpError500
        public ActionResult HttpError500(Exception exception)
        {
            return Content("500: Server error");
        }
    }
}
