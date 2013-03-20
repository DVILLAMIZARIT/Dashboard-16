using System.Web.Mvc;
using WebUI.Filters;

namespace WebUI.Controllers
{
    [InitializeSimpleMembership]
    public class DefaultController : Controller
    {
        //
        // GET: /Default/
        public ActionResult Index()
        {
            return Content("Home");
        }
    }
}
