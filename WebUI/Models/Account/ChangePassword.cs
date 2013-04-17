﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace WebUI.Models.Account
{
    public class ChangePassword
    {
        [HiddenInput(DisplayValue = false)]
        public Int32 Id { get; set; }

        [HiddenInput(DisplayValue = false)]
        public String Username { get; set; }

        [Required, Display(Name = "Confirm Password", Prompt = "Re-type your password"), DataType(DataType.Password), Compare("NewPassword")]
        public String ConfirmNewPassword { get; set; }

        [Required, Display(Name = "Current Password", Prompt = "Current Password"), DataType(DataType.Password)]
        public String CurrentPassword { get; set; }

        [Required, Display(Name = "New Password", Prompt = "New Password"), DataType(DataType.Password)]
        public String NewPassword { get; set; }
    }
}