using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace WebUI.Models.Account
{
    [Bind(Prefix = "forgotPassword")]
    public class ForgotPassword
    {
        [Required, Display(Name = "Email", Prompt = "Email"), DataType(DataType.EmailAddress), StringLength(255)]
        public String EmailAddress { get; set; }
    }
}