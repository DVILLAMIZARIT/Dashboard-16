using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace WebUI.Models.Account
{
    [Bind(Prefix = "Index")]
    public class Index
    {
        [Display(Name = "Display Name"), UIHint("Profile_DisplayName")]
        public String DisplayName { get; set; }

        [Display(Name = "Email"), DataType(DataType.EmailAddress), UIHint("Profile_EmailAddress")]
        public String EmailAddress { get; set; }

        [Display(Name = "Administrator?")]
        public Boolean IsAdministrator { get; set; }

        public Boolean IsEditable
        {
            get { return this.UpdateProfile != null && this.ChangePassword != null; }
        }

        [Display(Name = "Username")]
        public String Username { get; set; }

        public Update UpdateProfile { get; set; }

        public ChangePassword ChangePassword { get; set; }
    }
}