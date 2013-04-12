using System;
using System.ComponentModel.DataAnnotations;

namespace WebUI.Models.Default
{
    public class Search
    {
        [Display(Name = "Search Term")]
        public String Query { get; set; }
    }
}