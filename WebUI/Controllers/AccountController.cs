using System;
using System.Web.Mvc;
using AttributeRouting;
using AttributeRouting.Web.Mvc;
using Infra.Interfaces.Services;
using WebUI.Models.Account;

namespace WebUI.Controllers
{
    [Authorize, RoutePrefix("Account")]
    public class AccountController : Controller
    {
        private readonly IMembershipService membershipService;

        public AccountController(IMembershipService membershipService)
        {
            this.membershipService = membershipService;
        }

        [GET("", RouteName = "Account_Profile")]
        public ActionResult Index()
        {
            var profile = this.membershipService.GetProfileById();
            Index model = new Index
            {
                DisplayName = profile.DisplayName,
                EmailAddress = profile.EmailAddress
            };
            return View(model);
        }

        [AllowAnonymous, GET("Login", RouteName = "Account_Login", IsAbsoluteUrl = true)]
        public ActionResult Login(String returnUrl)
        {
            if (this.membershipService.IsAuthenticated)
            {
                if (!String.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                RedirectToRoute("Default");
            }
            Login model = new Login
            {
                RememberMe = true
            };
            return View(model);
        }

        [AllowAnonymous, POST("Login", RouteName = "Account_Login_POST", IsAbsoluteUrl = true), ValidateAntiForgeryToken]
        public ActionResult Login(Login model, String returnUrl)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    this.membershipService.Login(model.Username, model.Password, model.RememberMe);
                    if (!String.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToRoute("Account_Profile");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(String.Empty, ex.Message);
                }
            }
            return View(model);
        }

        [GET("Logout", RouteName = "Account_Logout", IsAbsoluteUrl = true)]
        public ActionResult Logout()
        {
            if (this.membershipService.IsAuthenticated)
            {
                this.membershipService.Logout();
            }
            return RedirectToRoute("Account_Login");
        }

        [AllowAnonymous, GET("Register", RouteName = "Account_Register")]
        public ActionResult Register()
        {
            return View(new Register());
        }

        [AllowAnonymous, POST("Register", RouteName = "Account_Register_POST"), ValidateAntiForgeryToken]
        public ActionResult Register(Register model, String returnUrl)
        {
            return View(model);
        }
    }
}
