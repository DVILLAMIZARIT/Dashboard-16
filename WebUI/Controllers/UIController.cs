using System.Web.Mvc;
using AttributeRouting;
using AttributeRouting.Web.Mvc;
using Infra.Interfaces.Services;
using WebUI.Models.UI;

namespace WebUI.Controllers
{
    [ChildActionOnly]
    [RoutePrefix("UI")]
    public class UIController : Controller
    {
        private readonly IUserProfileService userProfiles;

        public UIController(IUserProfileService userProfiles)
        {
            this.userProfiles = userProfiles;
        }

        [Route("UserDropdown")]
        public PartialViewResult UserDropdown()
        {
            UserDropdown model = new UserDropdown
            {
                Name = User.Identity.Name,
                EmailAddress = this.userProfiles.GetByUserName(User.Identity.Name).EmailAddress
            };
            return PartialView(model);
        }
    }
}
