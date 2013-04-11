using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.WebPages;

namespace WebUI.Helpers
{
    public static class BundleExtensions
    {
        private const String ScriptBundleKey = @"Dashboard.Bundles.Script";
        private const String StyleSheetBundleKey = @"Dashboard.Bundles.StyleSheets";
        private const String ScriptKey = @"Dashboard.Scripts";
        private enum BundleType { Script, StyleSheet };

        // Registration

        private static void RegisterBundles(HttpContextBase httpContext, params String[] virtualPaths)
        {
            if (virtualPaths == null)
            {
                throw new ArgumentNullException("virtualPaths");
            }
            foreach (String virtualPath in virtualPaths)
            {
                if (String.IsNullOrEmpty(virtualPath))
                {
                    continue;
                }

                var bundle = BundleTable.Bundles.GetBundleFor(virtualPath);
                if (bundle == null)
                {
                    throw new ArgumentException(String.Format("The bundle '{0}' does not exist and cannot be registered.", virtualPath), "virtualPath");
                }

                String bundleKey = bundle is ScriptBundle ? ScriptBundleKey : StyleSheetBundleKey;
                IList<String> bundles = (httpContext.Items[bundleKey] as IList<String>);
                if (bundles == null)
                {
                    bundles = new List<String>();
                    httpContext.Items.Add(bundleKey, bundles);
                }
                bundles.Insert(0, virtualPath);
            }
        }

        public static void RegisterScriptBundles(this HtmlHelper htmlHelper, params String[] virtualPaths)
        {
            RegisterBundles(htmlHelper.ViewContext.HttpContext, virtualPaths);
        }

        public static void RegisterStyleBundles(this HtmlHelper htmlHelper, params String[] virtualPaths)
        {
            RegisterBundles(htmlHelper.ViewContext.HttpContext, virtualPaths);
        }

        private static void RegisterScript(this HtmlHelper htmlHelper, Func<Object, HelperResult> scriptMarkup)
        {
            if (scriptMarkup == null)
            {
                throw new ArgumentNullException("scriptMarkup");
            }

            String scriptCode = scriptMarkup(null).ToString();
            if (!String.IsNullOrEmpty(scriptCode))
            {
                IList<String> scripts = htmlHelper.ViewContext.HttpContext.Items[ScriptKey] as IList<String>;
                if (scripts == null)
                {
                    scripts = new List<String>();
                    htmlHelper.ViewContext.HttpContext.Items.Add(ScriptKey, scripts);
                }
                scripts.Insert(0, scriptCode);
            }
        }

        // Rendering

        public static IHtmlString RenderScriptBundles(this HtmlHelper htmlHelper)
        {
            HttpContextBase httpContextBase = htmlHelper.ViewContext.HttpContext;
            IList<String> scriptBundles = httpContextBase.Items[ScriptBundleKey] as IList<String>;
            if (scriptBundles != null)
            {
                return Scripts.Render(scriptBundles.ToArray());
            }
            return MvcHtmlString.Empty;
        }

        public static IHtmlString RenderStyleBundles(this HtmlHelper htmlHelper)
        {
            HttpContextBase httpContextBase = htmlHelper.ViewContext.HttpContext;
            IList<String> scriptBundles = httpContextBase.Items[StyleSheetBundleKey] as IList<String>;
            if (scriptBundles != null)
            {
                return Styles.Render(scriptBundles.ToArray());
            }
            return MvcHtmlString.Empty;
        }
    }
}