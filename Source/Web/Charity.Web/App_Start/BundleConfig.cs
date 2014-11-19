using System.Web.Optimization;

namespace Charity.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));


            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                    "~/Scripts/jquery.validate*",
                    "~/Scripts/Kendo/kendo-validate-date.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryajax").Include(
                        "~/Scripts/jquery.unobtrusive-ajax.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                    "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                    "~/Scripts/bootstrap.js",
                    "~/Scripts/jasny-bootstrap.js",
                    "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/kendo").Include(
                    "~/Scripts/Kendo/kendo.all.min.js",
                    "~/Scripts/Kendo/kendo.aspnetmvc.min.js",
                    "~/Scripts/Kendo/Cultures/kendo.culture.en-GB.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap-cosmo.css",
                      "~/Content/jasny-bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/PagedList.css",
                      "~/Content/flash.css"));

            bundles.Add(new StyleBundle("~/Content/kendo").Include(
                    "~/Content/Kendo/kendo.common-bootstrap.min.css",
                    "~/Content/Kendo/kendo.bootstrap.min.css"));

            bundles.IgnoreList.Clear();

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = false;
        }
    }
}
