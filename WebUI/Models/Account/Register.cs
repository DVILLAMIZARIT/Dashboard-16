using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace WebUI.Models.Account
{
    public class Register
    {
        [Required, Display(Name = "Username", Prompt = "Username"), StringLength(50)]
        public String Username { get; set; }

        [Required, Display(Name = "Password", Prompt = "Password"), DataType(DataType.Password)]
        public String Password { get; set; }

        [Required, Display(Name = "Confirm Password", Prompt = "Re-type your password"), DataType(DataType.Password), Compare("Password")]
        public String ConfirmPassword { get; set; }

        [Required, Display(Name = "Email", Prompt = "Email"), DataType(DataType.EmailAddress), StringLength(255)]
        public String EmailAddress { get; set; }
    }
}