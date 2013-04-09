using System;
using System.Web.Mvc;

namespace WebUI.Helpers.Gravatar
{
    public static class GravatarExtensions
    {
        public static GravatarIcon Gravatar(this HtmlHelper htmlHelper, String emailAddress, Int32 imageSize = 80)
        {
            return new GravatarIcon(emailAddress, imageSize).UseSSL(htmlHelper.ViewContext.HttpContext.Request.IsSecureConnection);
        }
    }
}