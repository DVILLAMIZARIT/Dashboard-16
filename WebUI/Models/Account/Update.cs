using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace WebUI.Models.Account
{
    [Bind(Prefix = "Update")]
    public class Update
    {
        [Required, HiddenInput(DisplayValue = false)]
        public Int32 Id { get; set; }

        [Required, Display(Name = "Display Name", Prompt = "Display Name"), StringLength(50)]
        public String DisplayName { get; set; }

        [Required, Display(Name = "Email", Prompt = "Email"), DataType(DataType.EmailAddress), StringLength(255)]
        public String EmailAddress { get; set; }

        [HiddenInput(DisplayValue = true)]
        public String Username { get; set; }
    }
}