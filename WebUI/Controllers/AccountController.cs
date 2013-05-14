using System;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using AttributeRouting;
using AttributeRouting.Web.Mvc;
using Infra.Interfaces.Services;
using Infra.Model;
using WebUI.Helpers.Notifications;
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
        public ActionResult ChangePassword([Bind(Prefix = "ChangePassword")]ChangePassword model)
        {
            if (ModelState.IsValid)
            {
                var user = this.membershipService.GetProfileById(model.Id);
                if (user != null)
                {
                    if (this.membershipService.ChangePassword(user.UserName, model.CurrentPassword, model.NewPassword))
                    {
                        this.AddNotification(NotificationType.Success, "Password changed successfully.");
                        model = new ChangePassword();
                    }
                    else
                    {
                        ModelState.AddModelError(String.Empty, "Unable to update password.");
                    }
                }
                else
                {
                    ModelState.AddModelError(String.Empty, "Unable to locate user in the database.");
                }
            }
            else
            {
                ModelState.AddModelError(String.Empty, "Unable to process your request.");
            }
            if (Request.IsAjaxRequest())
            {
                return PartialView(model);
            }
            return Redirect(Url.RouteUrl("Account_Profile") + "#!account/account_password");
        }

        [GET("Profile", RouteName = "Account_Profile")]
        public ActionResult Index()
        {
            var profile = this.membershipService.GetProfile();
            Boolean isAdmin = this.membershipService.GetRoles().Contains("Administrator");
            Index model = new Index
            {
                Username = profile.UserName,
                DisplayName = profile.DisplayName,
                EmailAddress = profile.EmailAddress,
                IsAdministrator = isAdmin,

                ChangePassword = new ChangePassword
                {
                    Id = profile.Id
                },
                UpdateProfile = new Update
                {
                    Id = profile.Id,
                    DisplayName = profile.DisplayName,
                    EmailAddress = profile.EmailAddress,
                    Username = profile.UserName,
                }
            };
            return View(model);
        }

        //[GET("ForgotPassword", RouteName = "Account_ForgotPassword")]
        //public ActionResult ForgotPassword(String returnUrl)
        //{
        //    if (this.membershipService.IsAuthenticated)
        //    {
        //        if (!String.IsNullOrEmpty(return) && Url.IsLocalUrl(returnUrl))
        //        {
        //            return Redirect(returnUrl);
        //        }
        //        return RedirectToRoute("Account_Profile");
        //    }
        //}

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

            HttpCookie usernameCookie = Request.Cookies["Dashboard.Username"];
            Login model = new Login
            {
                Username = usernameCookie != null ? usernameCookie.Value : String.Empty,
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
                    if (this.membershipService.Login(username, model.Password, model.RememberMe))
                    {
                        HttpCookie usernameCookie = new HttpCookie("Dashboard.Username", username)
                        {
                            Expires = DateTime.Now.AddDays(30)
                        };
                        Response.Cookies.Add(usernameCookie);
                        if (!String.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }
                        return RedirectToRoute("Account_Profile");
                    }
                    else
                    {
                        ModelState.AddModelError(String.Empty, "Invalid email or password");
                    }
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
        public ActionResult Register(String returnUrl)
        {
            if (this.membershipService.IsAuthenticated)
            {
                if (!String.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                return RedirectToRoute("Account_Profile");
            }
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
                    if (this.membershipService.Login(model.Username, model.Password))
                    {
                        if (!String.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }
                        return RedirectToRoute("Account_Profile");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(String.Empty, ex.Message);
                }
            }
            return View(model);
        }

        [POST("Update", RouteName = "Account_Update"), ValidateAntiForgeryToken]
        public ActionResult Update([Bind(Prefix = "UpdateProfile")]Update model)
        {
            if (ModelState.IsValid)
            {
                var user = this.membershipService.GetProfileById(model.Id);
                if (user != null)
                {
                    user.DisplayName = model.DisplayName;
                    user.EmailAddress = model.EmailAddress;
                    if (this.membershipService.UpdateProfile(user))
                    {
                        this.AddNotification(NotificationType.Success, "Profile successfully updated.");
                    }
                    else
                    {
                        ModelState.AddModelError(String.Empty, "Unable to update profile.");
                    }
                }
                else
                {
                    ModelState.AddModelError(String.Empty, "Unable to locate user in the database.");
                }
            }
            else
            {
                ModelState.AddModelError(String.Empty, "Unable to process your request.");
            }
            if (Request.IsAjaxRequest())
            {
                return PartialView(model);
            }
            return Redirect(Url.RouteUrl("Account_Profile") + "#!account/account_details");
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
                catch (Exception)
                {
                }
            }
            return false;
        }

        #endregion
    }
}
