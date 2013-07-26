using System;
using System.Web.Optimization;

namespace WebUI
{
    public static class StyleBundleExtension
    {
        public static StyleBundle Include2(this StyleBundle styleBudle, params String[] virtualPaths)
        {
            if (virtualPaths != null)
            {
                IItemTransform transform = new CssRewriteUrlTransform();
                foreach (var virtualPath in virtualPaths)
                {
                    styleBudle.Include(virtualPath, transform);
                }
            }
            return styleBudle;
        }
    }

    public static class BundleNames
    {
        public const String Bootstrap_ScriptBundle = "~/js/bootstrap";
        public const String Bootstrap_StyleBundle = "~/css/bootstrap";
        public const String ExCanvas_ScriptBundle = "~/js/excanvas";
        public const String FontAwesome_StyleBundle = "~/css/font-awesome";
        public const String FontAwesomeIE7_StyleBundle = "~/css/font-awesome/ie7";
        public const String IE7_ScriptBundle = "~/js/ie7";
        public const String IE7_StyleBundle = "~/css/ie7";
        public const String jQuery_ScriptBundle = "~/js/jquery";
        public const String jQuery_Backstretch_ScriptBundle = "~/js/backstretch";
        public const String jQuery_BlockUI_ScriptBundle = "~/js/jquery/blockui";
        public const String jQuery_Cookie_ScriptBundle = "~/js/jquery/cookie";
        public const String jQuery_FullCalendar_StyleBundle = "~/css/jquery/fullcalendar";
        public const String jQuery_FullCalendar_ScriptBundle = "~/js/jquery/fullcalendar";
        public const String jQuery_Gritter_StyleBundle = "~/css/jquery/gritter";
        public const String jQuery_Gritter_ScriptBundle = "~/js/jquery/gritter";
        public const String jQuery_Migrate_ScriptBundle = "~/js/jquery/migrate";
        public const String jQuery_SlimScroll_ScriptBundle = "~/js/jquery/slimscroll";
        public const String jQuery_Uniform_ScriptBundle = "~/js/jquery/uniform";
        public const String jQuery_Uniform_StyleBundle = "~/css/jquery/uniform";
        public const String jQuery_Unobtrusive_ScriptBundle = "~/js/jquery-unobtrusive";
        public const String jQuery_Validate_ScriptBundle = "~/js/jquery/validate";
        public const String jQueryUI_ScriptBundle = "~/js/jquery-ui";
        public const String jQueryUI_StyleBundle = "~/css/jquery-ui";
        public const String KockoutJS_ScriptBundle = "~/js/knockout";
        public const String Metronic_ScriptBundle = "~/js/metronic";
        public const String Metronic_StyleBundle = "~/css/metronic";
        public const String Metronic_DefaultTheme_StyleBundle = "~/css/metronic/default";
        public const String Metronic_BlueTheme_StyleBundle = "~/css/metronic/blue";
        public const String Metronic_BrownTheme_StyleBundle = "~/css/metronic/brown";
        public const String Metronic_LightTheme_StyleBundle = "~/css/metronic/light";
        public const String Metronic_PurpleTheme_StyleBundle = "~/css/metronic/purple";
        public const String Modernizr_ScriptBundle = "~/js/modernizr";
        public const String Respond_ScriptBundle = "~/js/respond";
    }

    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            RegisterDefaultBundles(bundles);
            RegisterThemeBundles(bundles);

#if !DEBUG
            BundleTable.EnableOptimizations = true;
#endif
        }

        private static void RegisterDefaultBundles(BundleCollection bundles)
        {
            //
            // Explorer Canvas
            //
            bundles.Add(new ScriptBundle(BundleNames.ExCanvas_ScriptBundle).Include(
                "~/Scripts/excanvas.js"
            ));

            //
            // Font Awesome
            //
            bundles.Add(new StyleBundle(BundleNames.FontAwesome_StyleBundle).Include2(
                "~/Content/font-awesome.css"
            ));
            bundles.Add(new StyleBundle(BundleNames.FontAwesomeIE7_StyleBundle).Include2(
                "~/Content/font-awesome-ie7.css"
            ));

            //
            // IE 7
            //
            bundles.Add(new StyleBundle(BundleNames.IE7_StyleBundle).Include2(
                "~/Content/font-awesome-ie7.css"
            ));
            bundles.Add(new ScriptBundle(BundleNames.IE7_ScriptBundle).Include(
                "~/Scripts/excanvas.js",
                "~/Scripts/html5shiv.js",
                "~/Scripts/json2.js"
            ));

            //
            // jQuery
            //
            bundles.Add(new ScriptBundle(BundleNames.jQuery_ScriptBundle).Include(
                "~/Scripts/jquery-{version}.js"
            ));

            //
            // jQuery Backstretch
            //
            bundles.Add(new ScriptBundle(BundleNames.jQuery_Backstretch_ScriptBundle).Include(
                "~/Assets/plugins/backstretch/jquery.backstretch.js"
            ));

            //
            // jQuery BlockUI
            //
            bundles.Add(new ScriptBundle(BundleNames.jQuery_BlockUI_ScriptBundle).Include(
                "~/Scripts/jquery.blockUI.js"
            ));

            //
            // jQuery Cookie
            //
            bundles.Add(new ScriptBundle(BundleNames.jQuery_Cookie_ScriptBundle).Include(
                "~/Scripts/jquery.cookie.js"
            ));

            //
            // jQuery Fullcalendar
            //
            bundles.Add(new StyleBundle(BundleNames.jQuery_FullCalendar_StyleBundle).Include2(
                "~/Content/fullcalendar.css"
            ));
            bundles.Add(new ScriptBundle(BundleNames.jQuery_FullCalendar_ScriptBundle).Include(
                "~/Scripts/fullcalendar.js"
            ));

            //
            // jQuery Gritter
            //
            bundles.Add(new StyleBundle(BundleNames.jQuery_Gritter_StyleBundle).Include2(
                "~/Content/jquery.gritter.css"
            ));
            bundles.Add(new ScriptBundle(BundleNames.jQuery_Gritter_ScriptBundle).Include(
                "~/Scripts/jquery.gritter.js"
            ));

            //
            // jQuery Migrate
            //
            bundles.Add(new ScriptBundle(BundleNames.jQuery_Migrate_ScriptBundle).Include(
                "~/Scripts/jquery-migrate-{version}.js"
            ));

            //
            // jQuery SlimScroll
            //
            bundles.Add(new ScriptBundle(BundleNames.jQuery_SlimScroll_ScriptBundle).Include(
                "~/Scripts/jquery.slimscroll.js"
            ));

            //
            // jQuery UI
            //
            bundles.Add(new StyleBundle(BundleNames.jQueryUI_StyleBundle).Include2(
                "~/Content/themes/base/jquery.ui.core.css",
                "~/Content/themes/base/jquery.ui.resizable.css",
                "~/Content/themes/base/jquery.ui.selectabke.css",
                "~/Content/themes/base/jquery.ui.accordian.css",
                "~/Content/themes/base/jquery.ui.autocomplete.css",
                "~/Content/themes/base/jquery.ui.button.css",
                "~/Content/themes/base/jquery.ui.dialog.css",
                "~/Content/themes/base/jquery.ui.slider.css",
                "~/Content/themes/base/jquery.ui.tabs.css",
                "~/Content/themes/base/jquery.ui.datepicker.css",
                "~/Content/themes/base/jquery.ui.progressbar.css",
                "~/Content/themes/base/jquery.ui.theme.css"
            ));
            bundles.Add(new ScriptBundle(BundleNames.jQueryUI_ScriptBundle).Include(
                "~/Scripts/jquery-ui-{version}.js"
            ));

            //
            // jQuery Uniform
            //
            bundles.Add(new StyleBundle(BundleNames.jQuery_Uniform_StyleBundle).Include2(
                "~/Content/themes/uniformjs/default/css/uniform.default.css"
            ));
            bundles.Add(new ScriptBundle(BundleNames.jQuery_Uniform_ScriptBundle).Include(
                "~/Scripts/jquery.uniform.js"
            ));

            //
            // jQuery Unobtrusive
            //
            bundles.Add(new ScriptBundle(BundleNames.jQuery_Unobtrusive_ScriptBundle).Include(
                "~/Scripts/jquery.validate.unobtrusive.js",
                "~/Scripts/jquery.unobtrusive-ajax.js"
            ));

            //
            // jQuery Validate
            //
            bundles.Add(new ScriptBundle(BundleNames.jQuery_Validate_ScriptBundle).Include(
                "~/Scripts/jquery.validate.js"
            ));

            //
            // Knockout JS
            //
            bundles.Add(new ScriptBundle(BundleNames.KockoutJS_ScriptBundle).Include(
                "~/Scripts/knockout-{version}.js",
                "~/Scripts/knockout.mapping-latest.js"
            ));

            //
            // Modernizr
            //
            bundles.Add(new ScriptBundle(BundleNames.Modernizr_ScriptBundle).Include(
                "~/Scripts/modernizr-{version}.js"
            ));

            //
            // Respond JS
            //
            bundles.Add(new ScriptBundle(BundleNames.Respond_ScriptBundle).Include(
                "~/Scripts/respond.js"
            ));

            //
            // Twitter Bootstrap
            //
            bundles.Add(new StyleBundle(BundleNames.Bootstrap_StyleBundle).Include2(
                "~/Content/bootstrap.css",
                "~/Content/bootstrap-responsive.css"
            ));
            bundles.Add(new ScriptBundle(BundleNames.Bootstrap_ScriptBundle).Include(
                "~/Scripts/bootstrap.js"
            ));
        }

        private static void RegisterThemeBundles(BundleCollection bundles)
        {
            String themeBase = "~/Assets/";

            //
            // Metronic Themes
            //
            bundles.Add(new StyleBundle(BundleNames.Metronic_DefaultTheme_StyleBundle).Include2(
                themeBase + "css/themes/default.css"
            ));
            bundles.Add(new StyleBundle(BundleNames.Metronic_BlueTheme_StyleBundle).Include2(
                themeBase + "css/themes/blue.css"
            ));
            bundles.Add(new StyleBundle(BundleNames.Metronic_BrownTheme_StyleBundle).Include2(
                themeBase + "css/themes/brown.css"
            ));
            bundles.Add(new StyleBundle(BundleNames.Metronic_LightTheme_StyleBundle).Include2(
                themeBase + "css/themes/light.css"
            ));
            bundles.Add(new StyleBundle(BundleNames.Metronic_PurpleTheme_StyleBundle).Include2(
                themeBase + "css/themes/purple.css"
            ));

            //
            // Metronic Core
            //
            var metronicScriptBundle = new ScriptBundle(BundleNames.Metronic_ScriptBundle).Include(
                "~/Scripts/jquery-{version}.js", // jQuery_ScriptBundle,
                "~/Scripts/jquery-migrate-{version}.js", // jQuery_Migrate_ScriptBundle,
                "~/Scripts/jquery-ui-{version}.js", // jQueryUI_ScriptBundle,
                "~/Scripts/bootstrap.js", // Bootstrap_ScriptBundle,
                "~/Scripts/jquery.blockUI.js", // jQuery_BlockUI_ScriptBundle,
                "~/Scripts/jquery.cookie.js", // jQuery_Cookie_ScriptBundle,
                "~/Scripts/jquery.gritter.js", // jQuery_Gritter_ScriptBundle,
                "~/Scripts/jquery.slimscroll.js", // jQuery_SlimScroll_ScriptBundle,
                "~/Scripts/jquery.uniform.js", // jQuery_Uniform_ScriptBundle
                "~/Scripts/md5js",
                "~/Scripts/moment.js", // http://momentjs.com/

                "~/Scripts/metronic/metronic.js"
            );
            metronicScriptBundle.Transforms.Clear();
            metronicScriptBundle.Transforms.Add(new BundleTransformer.Core.Transformers.JsTransformer());
            bundles.Add(metronicScriptBundle);
            bundles.Add(new StyleBundle(BundleNames.Metronic_StyleBundle).Include2(
                "~/Content/bootstrap.css", "~/Content/bootstrap-responsive.css", // Bootstrap_StyleBundle,
                "~/Content/font-awesome.css",
                "~/Content/themes/base/jquery.ui.core.css", "~/Content/themes/base/jquery.ui.resizable.css", "~/Content/themes/base/jquery.ui.selectable.css", "~/Content/themes/base/jquery.ui.accordian.css", "~/Content/themes/base/jquery.ui.autocomplete.css", "~/Content/themes/base/jquery.ui.button.css", "~/Content/themes/base/jquery.ui.dialog.css", "~/Content/themes/base/jquery.ui.slider.css", "~/Content/themes/base/jquery.ui.tabs.css", "~/Content/themes/base/jquery.ui.datepicker.css", "~/Content/themes/base/jquery.ui.progressbar.css", "~/Content/themes/base/jquery.ui.theme.css", // jQueryUI_StyleBundle,
                themeBase + "css/style-metro.css",
                themeBase + "css/style.css",
                themeBase + "css/style-responsive.css",
                themeBase + "css/themes/default.css", // Metronic_DefaultTheme_StyleBundle,
                "~/Content/jquery.gritter.css", // jQuery_Gritter_StyleBundle,
                "~/Content/themes/uniformjs/default/css/uniform.default.css" // jQuery_Uniform_StyleBundle
            ));

            //
            // Metronic Base
            // Core + Theme
            //
            //bundles.Add(new ScriptBundle("~/js/metronic").Include(
            //    Metronic_ScriptBundle,
            //    jQuery_SlimScroll_ScriptBundle,
            //    jQuery_Gritter_ScriptBundle,
            //    jQuery_Uniform_ScriptBundle
            //));
            //bundles.Add(new StyleBundle("~/css/metronic").Include2(
            //    Metronic_StyleBundle,
            //    Metronic_DefaultTheme_StyleBundle,
            //    jQuery_Uniform_StyleBundle
            //));

            //
            // Metronic Pages
            //
            foreach (String page in new[] { "about-us", "blog", "coming-soon", "email", "error", "inbox", "invoice", "lock", "login-soft", "login", "news", "pricing-tables", "profile", "promo", "search", "timeline" })
            {
                bundles.Add(new StyleBundle(String.Format("~/css/metronic/{0}", page)).Include2(
                    themeBase + String.Format("css/pages/{0}.css", page)
                ));
            }
        }
    }
}