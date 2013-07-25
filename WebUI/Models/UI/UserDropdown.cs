using System;

namespace WebUI.Models.UI
{
    public class UserDropdown
    {
        public Boolean IsAuthenticated { get; set; }
        public String UserName { get; set; }
        public String EmailAddress { get; set; }

        public UserDropdown()
        {
            this.IsAuthenticated = false;
            this.UserName = "Anonymous";
            this.EmailAddress = String.Empty;
        }
    }
}