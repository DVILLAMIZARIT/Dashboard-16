using System.Web.Optimization;

namespace WebUI
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            //
            // Stylesheets
            //

            bundles.Add(new StyleBundle("~/Content/bootstrap").Include(
                        "~/Content/bootstrap.css",
                        "~/Content/bootstrap-responsive.css"));

            bundles.Add(new StyleBundle("~/Content/font-awesome").Include(
                        "~/Content/font-awesome/css/font-awesome.min.css"));

            bundles.Add(new StyleBundle("~/Content/metro").Include(
                        "~/Content/metro/style-metro.css",
                        "~/Content/metro/style.css",
                        "~/Content/metro/style-responsive.css"));
            bundles.Add(new StyleBundle("~/Content/metro/default-theme").Include(
                        "~/Content/metro/themes/default.css"));
            bundles.Add(new StyleBundle("~/Content/metro/blue-theme").Include(
                        "~/Content/metro/themes/blue.css"));
            bundles.Add(new StyleBundle("~/Content/metro/brown-theme").Include(
                        "~/Content/metro/themes/brown.css"));
            bundles.Add(new StyleBundle("~/Content/metro/light-theme").Include(
                        "~/Content/metro/themes/light.css"));
            bundles.Add(new StyleBundle("~/Content/metro/purple-theme").Include(
                        "~/Content/metro/themes/purple.css"));

            //bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
            //            "~/Content/themes/base/jquery.ui.core.css",
            //            "~/Content/themes/base/jquery.ui.resizable.css",
            //            "~/Content/themes/base/jquery.ui.selectable.css",
            //            "~/Content/themes/base/jquery.ui.accordion.css",
            //            "~/Content/themes/base/jquery.ui.autocomplete.css",
            //            "~/Content/themes/base/jquery.ui.button.css",
            //            "~/Content/themes/base/jquery.ui.dialog.css",
            //            "~/Content/themes/base/jquery.ui.slider.css",
            //            "~/Content/themes/base/jquery.ui.tabs.css",
            //            "~/Content/themes/base/jquery.ui.datepicker.css",
            //            "~/Content/themes/base/jquery.ui.progressbar.css",
            //            "~/Content/themes/base/jquery.ui.theme.css"));

            //
            // Scripts
            //

            bundles.Add(new ScriptBundle("~/Scripts/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/Scripts/bootstrap").Include(
                        "~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/Scripts/ltie9").Include(
                        "~/Scripts/ltie9/excanvas.js",
                        "~/Scripts/ltie9/respond.js"));

            //bundles.Add(new StyleBundle("~/bundles/breakpoints").Include(
            //            "~/Scripts/breakpoints/breakpoints.js"));

            bundles.Add(new ScriptBundle("~/Scripts/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));
            bundles.Add(new ScriptBundle("~/Scripts/jqueryval-unobtrusive").Include(
                        "~/Scripts/jquery.validate.unobtrusive*",
                        "~/Scripts/jquery.unobtrusive*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/Scripts/knockoutjs").Include(
                        "~/Scripts/knockout-{version}.js",
                        "~/Scripts/knockout-mapping-latest.js"));
        }
    }
}