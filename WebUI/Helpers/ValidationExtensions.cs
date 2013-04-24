
namespace WebUI.Helpers
{
    public static class ValidationExtensions
    {
        /*public static IHtmlString CustomValidationSummary(this HtmlHelper htmlhelper, Boolean excludePropertyErrors = false)
        {
            FormContext formContext = htmlhelper.ViewContext.FormContext;
            if (htmlhelper.ViewData.ModelState.IsValid)
            {
                if (formContext == null)
                {
                    return MvcHtmlString.Empty;
                }
                if (htmlhelper.ViewContext.UnobtrusiveJavaScriptEnabled && excludePropertyErrors)
                {
                    return MvcHtmlString.Empty;
                }
            }

            StringBuilder htmlSummary = new StringBuilder();
            IEnumerable<ModelState> modelStates = null;
            if (excludePropertyErrors)
            {
                ModelState ms;
                htmlhelper.ViewData.ModelState.TryGetValue(htmlhelper.ViewData.TemplateInfo.HtmlFieldPrefix, out ms);
                if (ms != null)
                {
                    modelStates = new ModelState[] { ms };
                }
            }
            else
            {
                modelStates = htmlhelper.ViewData.ModelState.Values;
            }

            if (modelStates != null)
            {
                foreach (ModelState modelState in modelStates)
                {
                    foreach (ModelError modelError in modelState.Errors)
                    {
                        String errorText = GetUse
                    }
                }
            }


            if (htmlhelper.ViewData.ModelState.IsValid)
            {
                return MvcHtmlString.Empty;
            }
            List<TagBuilder> errorTags = new List<TagBuilder>();
            foreach (var modelState in htmlhelper.ViewData.ModelState.Values)
            {
                foreach (var error in modelState.Errors)
                {
                    if (!String.IsNullOrEmpty(error.ErrorMessage))
                    {
                        TagBuilder errorTag = new TagBuilder("div");
                        errorTag.AddCssClass("alert alert-error");

                        TagBuilder closeButton = new TagBuilder("button");
                        closeButton.AddCssClass("close");
                        closeButton.Attributes.Add("data-dismiss", "alert");
                        errorTag.InnerHtml += closeButton.ToString();

                        TagBuilder content = new TagBuilder("span");
                        content.SetInnerText(error.ErrorMessage);
                        errorTag.InnerHtml += content.ToString();

                        errorTags.Add(errorTag);
                    }
                }
            }
            return errorTags.Count > 0 ? MvcHtmlString.Create(String.Join(String.Empty, errorTags.Select(x => x.ToString()))) : MvcHtmlString.Empty;
        }

        #region helpers

        private static string GetInvalidPropertyValueResource(HttpContextBase httpContext)
        {
            string resourceValue = null;
            if (!String.IsNullOrEmpty(ResourceClassKey) && (httpContext != null))
            {
                // If the user specified a ResourceClassKey try to load the resource they specified.
                // If the class key is invalid, an exception will be thrown.
                // If the class key is valid but the resource is not found, it returns null, in which
                // case it will fall back to the MVC default error message.
                resourceValue = httpContext.GetGlobalResourceObject(ResourceClassKey, "InvalidPropertyValue", CultureInfo.CurrentUICulture) as string;
            }
            return resourceValue ?? MvcResources.Common_ValueNotValidForProperty;
        }

        private static string GetUserErrorMessageOrDefault(HttpContextBase httpContext, ModelError error, ModelState modelState)
        {
            if (!String.IsNullOrEmpty(error.ErrorMessage))
            {
                return error.ErrorMessage;
            }
            if (modelState == null)
            {
                return null;
            }

            string attemptedValue = (modelState.Value != null) ? modelState.Value.AttemptedValue : null;
            return String.Format(CultureInfo.CurrentCulture, GetInvalidPropertyValueResource(httpContext), attemptedValue);
        }

        #endregion*/
    }
}