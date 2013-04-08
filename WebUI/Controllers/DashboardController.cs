using System.Web.Mvc;
using Infra.Interfaces.Services;
using WebUI.Models.Dashboard;

namespace WebUI.Controllers
{
    [ChildActionOnly]
    public class DashboardController : Controller
    {
        private readonly IUserProfileService userProfiles;

        public DashboardController(IUserProfileService userProfiles)
        {
            this.userProfiles = userProfiles;
        }

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
