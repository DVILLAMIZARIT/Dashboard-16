﻿using System.Web.Mvc;
using AttributeRouting;
using AttributeRouting.Web.Mvc;

namespace WebUI.Controllers
{
    [RoutePrefix("Home")]
    public class DefaultController : Controller
    {
        [Route("", ActionPrecedence = 1, RouteName = "Default", IsAbsoluteUrl = true)]
        public ActionResult Index()
        {
            return View();
        }
    }
}
