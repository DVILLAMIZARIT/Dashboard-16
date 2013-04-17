using System;
using System.Net.Mail;
using System.Web.Mvc;
using AttributeRouting;
using AttributeRouting.Web.Mvc;
using Infra.Interfaces.Services;
using Infra.Model;
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

        [POST("ChangePassword", RouteName = "Account_ChangePassword"), ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePassword model)
        {
            if (ModelState.IsValid)
            {
                if (this.membershipService.ChangePassword(this.membershipService.CurrentUserName, model.CurrentPassword, model.NewPassword))
                {
                    return RedirectToRoute("Account_Profile");
                }
            }
            return RedirectToRoute("Account_Profile");
        }

        [GET("Profile", RouteName = "Account_Profile")]
        public ActionResult Index()
        {
            var profile = this.membershipService.GetProfile();
            Index model = new Index
            {
                Username = profile.UserName,
                DisplayName = profile.DisplayName,
                EmailAddress = profile.EmailAddress,

                UpdateProfile = new Update
                {
                    Id = profile.Id,
                    DisplayName = profile.DisplayName,
                    EmailAddress = profile.EmailAddress,
                    Username = profile.UserName,
                },
                ChangePassword = new ChangePassword()
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
                return RedirectToRoute("Account_Profile");
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
                    String username = model.Username;
                    if (this.IsValidEmail(model.Username))
                    {
                        UserProfile profile = this.membershipService.GetProfileByEmail(model.Username);
                        if (profile != null)
                        {
                            username = profile.UserName;
                        }
                    }
                    this.membershipService.Login(username, model.Password, model.RememberMe);
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

        [GET("Logout", RouteName = "Account_Logout")]
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
            if (ModelState.IsValid)
            {
                try
                {
                    this.membershipService.CreateAccount(model.Username, model.Password, model.EmailAddress, model.Username);
                    this.membershipService.Login(model.Username, model.Password);
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

        [POST("Update", RouteName = "Account_Update"), ValidateAntiForgeryToken]
        public ActionResult Update(Update model)
        {
            return RedirectToRoute("Account_Profile");
        }

        #region helpers

        private Boolean IsValidEmail(String input)
        {
            if (!String.IsNullOrEmpty(input))
            {
                try
                {
                    MailAddress address = new MailAddress(input);
                    return true;
                }
                catch (Exception ex)
                {
                }
            }
            return false;
        }

        #endregion
    }
}
