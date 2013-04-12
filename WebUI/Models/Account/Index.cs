using System;
using System.ComponentModel.DataAnnotations;

namespace WebUI.Models.Account
{
    public class Index
    {
        [Display(Name = "Display Name")]
        public String DisplayName { get; set; }

        [Display(Name = "Email"), DataType(DataType.EmailAddress)]
        public String EmailAddress { get; set; }

        [Display(Name = "Username")]
        public String Username { get; set; }
    }
}