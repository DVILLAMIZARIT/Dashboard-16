using System;
using System.Web.Mvc;
using AttributeRouting;
using AttributeRouting.Web.Mvc;
using WebUI.Models.Account;

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

        [GET("Login", RouteName = "Account_Login", IsAbsoluteUrl = true)]
        public ActionResult Login()
        {
            return View();
        }

        [POST("Login", RouteName = "Account_Login_POST", IsAbsoluteUrl = true), ValidateAntiForgeryToken]
        public ActionResult Login(Login model, String returnUrl)
        {
            return View(model);
        }

        [GET("Register", RouteName = "Account_Register")]
        public ActionResult Register()
        {
            return View();
        }

        [POST("Register", RouteName = "Account_Register_POST"), ValidateAntiForgeryToken]
        public ActionResult Register(Register model, String returnUrl)
        {
            return View(model);
        }
    }
}
