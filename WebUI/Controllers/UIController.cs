﻿using System.Linq;
using System.Web.Mvc;
using AttributeRouting;
using AttributeRouting.Web.Mvc;
using Infra.Interfaces.Services;
using WebUI.Helpers.Notifications;
using WebUI.Models.UI;

namespace WebUI.Controllers
{
    [ChildActionOnly, AllowAnonymous, RoutePrefix("UI")]
    public class UIController : Controller
    {
        private readonly IMembershipService membershipService;

        public UIController(IMembershipService membershipService)
        {
            this.membershipService = membershipService;
        }

        [Authorize, Route("Notifications", RouteName = "UI_Notifications")]
        public PartialViewResult Notifications()
        {
            var notifications = this.GetNotifications();
            return PartialView(notifications ?? Enumerable.Empty<Notification>());
        }

        [Authorize, Route("UserDropdown", RouteName = "UI_UserDropdown")]
        public PartialViewResult UserDropdown()
        {
            var profile = this.membershipService.GetProfile();
            if (profile != null)
            {
                UserDropdown model = new UserDropdown
                {
                    Name = profile.DisplayName,
                    EmailAddress = profile.EmailAddress
                };
                return PartialView(model);
            }
            return PartialView(new UserDropdown());
        }
    }
}
