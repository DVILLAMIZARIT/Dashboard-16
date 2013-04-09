using System.Web.Mvc;
using AttributeRouting;
using AttributeRouting.Web.Mvc;

namespace WebUI.Controllers
{
    [RoutePrefix("Account")]
    public class AccountController : Controller
    {
        [GET("", RouteName = "Account")]
        public ActionResult Index()
        {
            return View();
        }
    }
}
