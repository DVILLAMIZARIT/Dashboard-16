using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebUI.Helpers
{
    public static class ChildActionExtensions
    {
        // RenderRoute

        public static void RenderRoute(this HtmlHelper htmlHelper, String routeName)
        {
            RenderRoute(htmlHelper, routeName, null);
        }
        public static void RenderRoute(this HtmlHelper htmlHelper, String routeName, Object routeValues)
        {
            RenderRoute(htmlHelper, routeName, new RouteValueDictionary(routeValues));
        }
        public static void RenderRoute(this HtmlHelper htmlHelper, String routeName, RouteValueDictionary routeValues)
        {
            RouteHelper(htmlHelper, routeName, routeValues, htmlHelper.ViewContext.Writer);
        }

        // Helpers

        internal static void RouteHelper(HtmlHelper htmlHelper, String routeName, RouteValueDictionary routeValues, TextWriter textWriter)
        {
            Contract.Requires<ArgumentNullException>(htmlHelper != null);
            Contract.Requires<ArgumentException>(!String.IsNullOrEmpty(routeName));

            RouteValueDictionary additionalRouteValues = routeValues;
            routeValues = MergeDictionaries(routeValues, htmlHelper.ViewContext.RouteData.Values);

            //TODO Get action &controller from routeName then hand off to generate code
        }

        private static RouteValueDictionary MergeDictionaries(params RouteValueDictionary[] dictionaries)
        {
            // Merge existing route values with the user provided values
            var result = new RouteValueDictionary();

            foreach (RouteValueDictionary dictionary in dictionaries.Where(d => d != null))
            {
                foreach (KeyValuePair<string, object> kvp in dictionary)
                {
                    if (!result.ContainsKey(kvp.Key))
                    {
                        result.Add(kvp.Key, kvp.Value);
                    }
                }
            }

            return result;
        }
    }
}