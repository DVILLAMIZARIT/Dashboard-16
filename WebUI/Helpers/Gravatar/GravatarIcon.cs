using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebUI.Helpers.Gravatar
{ // original from https://raw.github.com/AndrewFreemantle/Gravatar-HtmlHelper/master/GravatarHtmlHelper.cs but modified heavily
    public enum GravatarDefaultImage
    {
        [Description("")]
        Default,

        [Description("404")]
        Http404,

        [Description("mm")]
        MysteryMan,

        [Description("identicon")]
        GeneratedPattern,

        [Description("monsterid")]
        GeneratedMonster,

        [Description("wavatar")]
        GeneratedFace,

        [Description("retro")]
        Pixelated
    }

    public enum GravatarRating
    {
        [Description("g")]
        General,

        [Description("pg")]
        ParentalGuidance,

        [Description("r")]
        Restricted,

        [Description("x")]
        Explicit
    }

    public class GravatarIcon : IHtmlString
    {
        private IDictionary<String, Object> htmlAttributes = null;
        private String defaultImage = String.Empty;
        private String emailAddress = String.Empty;
        private Boolean forceDefaultImage = false;
        private Int32 imageSize = 80;
        private GravatarRating rating = GravatarRating.General;
        private Boolean sslEnabled = false;

        public GravatarIcon(String emailAddress, Int32 imageSize = 80)
        {
            Contract.Requires<ArgumentNullException>(!String.IsNullOrWhiteSpace(emailAddress));
            Contract.Requires<ArgumentOutOfRangeException>(1 <= imageSize && imageSize <= 2048);

            this.emailAddress = emailAddress.Trim().ToLower();
            this.imageSize = imageSize;
        }

        public GravatarIcon HtmlAttributes(Object additionalAttributes)
        {
            return HtmlAttributes(new RouteValueDictionary(additionalAttributes));
        }
        public GravatarIcon HtmlAttributes(IDictionary<String, Object> additionalAttributes)
        {
            this.htmlAttributes = additionalAttributes;

            return this;
        }

        public GravatarIcon DefaultImage(String imageUrl, Boolean forceDefaultImage = false)
        {
            Contract.Requires<ArgumentNullException>(!String.IsNullOrEmpty(imageUrl));
            Contract.Requires<ArgumentException>(!Uri.IsWellFormedUriString(imageUrl, UriKind.Absolute));
            
            this.defaultImage = imageUrl;
            this.forceDefaultImage = forceDefaultImage;

            return this;
        }

        public GravatarIcon DefaultImage(GravatarDefaultImage image = GravatarDefaultImage.Default, Boolean forceDefaultImage = false)
        {
            this.defaultImage = GetDescription(image);
            this.forceDefaultImage = forceDefaultImage;

            return this;
        }

        public GravatarIcon Rating(GravatarRating rating = GravatarRating.General)
        {
            this.rating = rating;

            return this;
        }

        public GravatarIcon Size(Int32 size)
        {
            Contract.Requires<ArgumentOutOfRangeException>(size < 1 || size > 2048);

            this.imageSize = size;

            return this;
        }

        public GravatarIcon UseSSL(Boolean enabled = true)
        {
            this.sslEnabled = enabled;

            return this;
        }

        #region IHtmlString

        public String ToHtmlString()
        {
            TagBuilder img = new TagBuilder("img");

            StringBuilder gravatarUrl = new StringBuilder(
                String.Format("{0}://{1}.gravatar.com/avatar/{2}",
                    sslEnabled ? "https" : "http",
                    sslEnabled ? "secure" : "www",
                    GenerateMD5Hash(this.emailAddress)
                )
            );
            gravatarUrl.AppendFormat("?s={0}&r={1}", this.imageSize, HttpUtility.UrlEncode(GetDescription(this.rating)));
            if (!String.IsNullOrEmpty(this.defaultImage))
            {
                gravatarUrl.AppendFormat("&d={0}", HttpUtility.UrlEncode(this.defaultImage));
            }
            if (this.forceDefaultImage)
            {
                gravatarUrl.Append("&f=y");
            }
            img.Attributes.Add("src", gravatarUrl.ToString());
            img.Attributes.Add("width", this.imageSize.ToString());
            img.Attributes.Add("height", this.imageSize.ToString());

            if (this.htmlAttributes != null)
            {
                img.MergeAttributes(this.htmlAttributes);
            }

            return img.ToString(TagRenderMode.SelfClosing);
        }

        #endregion

        #region Helpers

        private static String GenerateMD5Hash(String input)
        {
            Byte[] data = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder result = new StringBuilder();
            for (Int32 i = 0; i < data.Length; i++)
            {
                result.Append(data[i].ToString("x2"));
            }
            return result.ToString();
        }

        private static String GetDescription(Enum e)
        {
            String enumAsString = e.ToString();
            Type type = e.GetType();
            MemberInfo[] members = type.GetMember(enumAsString);
            if (members != null && members.Length > 0)
            {
                Object[] attributes = members[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attributes != null && attributes.Length > 0)
                {
                    enumAsString = ((DescriptionAttribute)attributes[0]).Description;
                }
            }
            return enumAsString;
        }

        #endregion
    }
}