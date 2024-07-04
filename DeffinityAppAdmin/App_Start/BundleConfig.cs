using System.Web;
using System.Web.Optimization;

namespace DeffinityAppDev
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundles/newcss").Include(
                     "~/assets/plugins/global/plugins.bundle.css",
                     "~/assets/css/style.bundle.css"
                     ));

            bundles.Add(new ScriptBundle("~/bundles/newjs").Include(
                     "~/assets/plugins/global/plugins.bundle.js",
                     "~/assets/js/scripts.bundle.js",
                     "~/assets/js/custom/authentication/sign-in/general.js"
                     ));
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.js"));
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                       "~/Content/assets/js/jquery-1.11.1.min.js"));

            bundles.Add(new StyleBundle("~/Content/jqueryuicss").Include(
                    "~/Content/jquery-ui-1.10.0.css"));
            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                   "~/Content/jquery-ui-1.10.0.min.js"));
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Content/modernizr-2.6.2.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/xenonjs").Include(
                     "~/Content/assets/js/bootstrap.min.js",
                     "~/Content/assets/js/TweenMax.min.js",
                     "~/Content/assets/js/resizeable.js",
                     "~/Content/assets/js/joinable.js",
                     "~/Content/assets/js/xenon-api.js",
                     "~/Content/assets/js/xenon-toggles.js",
                     "~/Content/assets/js/xenon-custom.js",
                     "~/Scripts/Utility.js"));
           

            bundles.Add(new StyleBundle("~/bundles/bootstarpcss").Include(
                      "~/Content/assets/css/fonts/linecons/css/linecons.css",
                      "~/Content/assets/css/fonts/fontawesome/css/font-awesome.min.css",
                      "~/Content/assets/css/bootstrap.css",
                      "~/Content/assets/css/xenon-core.css",
                      "~/Content/assets/css/xenon-forms.css",
                      "~/Content/assets/css/xenon-components.css",
                      "~/Content/assets/css/xenon-skins.css",
                      "~/Content/assets/css/custom.css"));

            bundles.Add(new StyleBundle("~/bundles/jtablecss").Include(
                     "~/Content/jtable/themes/lightcolor/gray/jtable.css"));
            bundles.Add(new StyleBundle("~/bundles/chitchatcss").Include(
                    "~/Content/assets/css/fonts/elusive/css/elusive.css"));

            bundles.Add(new ScriptBundle("~/bundles/grid").Include(
           "~/Scripts/respond.min.js",
           "~/Content/assets/js/rwd-table/js/rwd-table.min.js",
           "~/Scripts/GridDesingFix.js"));

            bundles.Add(new ScriptBundle("~/bundles/jtable").Include(
                "~/Content/jquery-ui-1.10.0.min.js",
                "~/Content/modernizr-2.6.2.js",
         "~/Content/jtablesite.js",
         "~/Content/jtable/external/json2.js",
         "~/Content/jtable/jquery.jtable.js",
            "~/Content/jtable/localization/jquery.jtable.tr.js",
            "~/Content/jtable/extensions/jquery.jtable.aspnetpagemethods.js",
            "~/Content/moment.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/tabs").Include(
                      "~/Scripts/tabactive.js"));

            bundles.Add(new ScriptBundle("~/bundles/subtabs").Include(
                      "~/Scripts/subtabactive.js"));

            bundles.Add(new ScriptBundle("~/bundles/sidemenu").Include(
                     "~/Scripts/sidemenuactive.js"));
            //bundles.Add(new ScriptBundle("~/bundles/chitchat").Include(
            //        "~/Scripts/ChitChat.js"));

            bundles.Add(new StyleBundle("~/bundles/formscss").Include(
                   "~/Content/HCstyle.css"));
            bundles.Add(new ScriptBundle("~/bundles/forms").Include(
                    "~/Scripts/HCform.js"));
            bundles.Add(new StyleBundle("~/bundles/fullcalendarcss").Include(
                  "~/Content/assets/js/fullcalendar/lib/fullcalendar.min.css",
                  "~/Content/assets/js/fullcalendar/lib/fullcalendar.print.css",
                  "~/Content/assets/js/fullcalendar/lib/scheduler.min.css"));
            bundles.Add(new ScriptBundle("~/bundles/fullcalendar").Include(
                    "~/Content/assets/js/fullcalendar/lib/moment.min.js",
                    "~/Content/assets/js/fullcalendar/lib/fullcalendar.min.js",
                    "~/Content/assets/js/fullcalendar/lib/scheduler.min.js",
                    "~/Scripts/HCform.js",
                    "~/Scripts/HCform.js"));
            //bundles.Add(new ScriptBundle("~/bundles/angularjs").Include(
            //         "~/Scripts/angular.min.js",
            //         "~/Scripts/angular-resource.min.js",
            //         "~/Scripts/angular-mocks.js"));
            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            //BundleTable.EnableOptimizations = false;

            //bundles.Add(new ScriptBundle("~/bundles/homecharts").Include(
            //          "~/Content/assets/js/xenon-widgets.js",
            //          "~/Content/assets/js/devexpress-web-14.1/js/globalize.min.js",
            //          "~/Content/assets/js/devexpress-web-14.1/js/dx.chartjs.js",
            //          "~/Content/assets/js/toastr/toastr.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/charts").Include(
                      "~/Content/assets/js/devexpress-web-14.1/js/globalize.min.js",
                      "~/Content/assets/js/devexpress-web-14.1/js/dx.chartjs.js",
                      "~/Content/assets/js/toastr/toastr.min.js",
                      "~/Scripts/DCCharts.js"));

            bundles.Add(new StyleBundle("~/bundles/metroniccss").Include(
                "~/assets/plugins/custom/leaflet/leaflet.bundle.css",
                "~/assets/plugins/global/plugins.bundle.css",
            "~/assets/css/style.bundle.css"));

            bundles.Add(new ScriptBundle("~/bundles/metronicjs").Include(
             "~/assets/plugins/global/plugins.bundle.js",
             "~/assets/js/scripts.bundle.js",
         "~/assets/plugins/custom/leaflet/leaflet.bundle.js"));
        }
    }
}
