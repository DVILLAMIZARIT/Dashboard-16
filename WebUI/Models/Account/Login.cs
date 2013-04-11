using System;
using System.ComponentModel.DataAnnotations;

namespace WebUI.Models.Account
{
    public class Login
    {
        [Required, Display(Name = "Username", Prompt = "Username")]
        public String Username { get; set; }

        [Required, DataType(DataType.Password), Display(Name = "Password", Prompt = "Password")]
        public String Password { get; set; }

        [Display(Name = "Remember me", Prompt = "Remember me")]
        public Boolean RememberMe { get; set; }
    }
}