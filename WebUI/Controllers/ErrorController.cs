using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class ErrorController : Controller
    {
        //
        // GET: /Error/HttpError404
        public ActionResult HttpError404()
        {
            return Content("404: File not found");
        }

        //
        // GET: /Error/HttpError500
        public ActionResult HttpError500()
        {
            return Content("500: Server error");
        }
    }
}
