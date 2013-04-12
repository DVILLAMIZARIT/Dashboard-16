using System;
using System.Web.Mvc;
using AttributeRouting;
using AttributeRouting.Web.Mvc;

namespace WebUI.Controllers
{
    [Authorize, RoutePrefix("Home")]
    public class DefaultController : Controller
    {
        [Route("", ActionPrecedence = 1, RouteName = "Default", IsAbsoluteUrl = true)]
        public ActionResult Index()
        {
            return View();
        }

        [Route("Search", RouteName = "Search", IsAbsoluteUrl = true)]
        public ActionResult Search(String query)
        {
            return Content(String.Format("Showing results for '{0}'...", query));
        }
    }
}
