using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace WebUI.Models.Account
{
    [Bind(Prefix = "Unlock")]
    public class Unlock
    {
        [Display(Name = "Name", Prompt = "Name"), DataType(DataType.Text)]
        public String DisplayName { get; set; }

        [Display(Name = "Email", Prompt = "Email")]
        public String EmailAddress { get; set; }

        [Required, Display(Name = "Username", Prompt = "Username")]
        public String Username { get; set; }

        [Required, DataType(DataType.Password), Display(Name = "Password", Prompt = "Password")]
        public String Password { get; set; }
    }
}