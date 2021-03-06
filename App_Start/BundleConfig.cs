using System.Web;
using System.Web.Optimization;

namespace ECommerce
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));






            /* ************************************************************************************************* */


            bundles.Add(new StyleBundle("~/HomeCSS").Include("~/css/bootstrap.min.css",
                "~/Home/css/bootstrap.min.css",
                "~/Home/css/plugins.css",
                "~/Home/style.css",
                "~/Home/css/custom.css"
                ));

            bundles.Add(new ScriptBundle("~/HomeJS1").Include(
                "~/Home/js/vendor/jquery-3.2.1.min.js",
                "~/Home/js/popper.min.js",
                "~/Home/js/bootstrap.min.js",
                "~/Home/js/plugins.js",
                "~/Home/js/active.js"
                ));
            bundles.Add(new ScriptBundle("~/HomeJS2").Include("~/Home/js/vendor/modernizr-3.5.0.min.js"
                ));



            bundles.Add(new StyleBundle("~/LoginCSS").Include("~/css/bootstrap.min.css",
               "~/Login/vendor/bootstrap/css/bootstrap.min.css",
               "~/Login/fonts/font-awesome-4.7.0/css/font-awesome.min.css",
               "~/Login/fonts/Linearicons-Free-v1.0.0/icon-font.min.css",
               "~/Login/vendor/animate/animate.css",
               "~/Login/vendor/css-hamburgers/hamburgers.min.css",
               "~/Login/vendor/animsition/css/animsition.min.css",
               "~/Login/vendor/select2/select2.min.css",
               "~/Login/vendor/daterangepicker/daterangepicker.css",
               "~/Login/css/util.css",
               "~/Login/css/main.css"
               ));

            bundles.Add(new ScriptBundle("~/LoginJS1").Include(
                "~/Login/vendor/jquery/jquery-3.2.1.min.js",
                "~/Login/vendor/animsition/js/animsition.min.js",
                "~/Login/vendor/bootstrap/js/popper.js",
                "~/Login/vendor/bootstrap/js/bootstrap.min.js",
                "~/Login/vendor/select2/select2.min.js",
                "~/Login/vendor/daterangepicker/moment.min.js",
                "~/Login/vendor/daterangepicker/daterangepicker.js",
                "~/Login/vendor/countdowntime/countdowntime.js",
                "~/Login/js/main.js"
                ));



            bundles.Add(new StyleBundle("~/RegisterCSS").Include("~/Registration/fonts/material-icon/css/material-design-iconic-font.min.css",
                "~/Registration/css/style.css"
                ));

            bundles.Add(new ScriptBundle("~/RegisterJS1").Include(
                "~/Registration/vendor/jquery/jquery.min.js",
                "~/Registration/js/main.js"
                ));


            bundles.Add(new StyleBundle("~/AdminCSS").Include("~/css/bootstrap.min.css",
                "~/Admin/styles/shards-dashboards.1.1.0.min.css",
                "~/Admin/styles/extras.1.1.0.min.css"
                ));

            bundles.Add(new ScriptBundle("~/AdminJS1").Include(
                "~/Admin/scripts/extras.1.1.0.min.js",
                "~/Admin/scripts/shards-dashboards.1.1.0.min.js",
                "~/Admin/scripts/app/app-blog-overview.1.1.0.js"
                ));





        }
    }
}
