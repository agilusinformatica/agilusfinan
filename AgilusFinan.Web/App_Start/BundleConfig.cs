using System.Web;
using System.Web.Optimization;

namespace AgilusFinan.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //BundleTable.EnableOptimizations = true;

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.mask.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/methods_pt.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/bootstrap-notify.min.js"));
            
            bundles.Add(new ScriptBundle("~/bundles/agilusLibrary").Include(
                      "~/Scripts/Utils.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/datatables.min.css",
                      "~/Content/jquery-ui.css",
                      "~/Content/responsive.dataTables.min.css",
                      "~/Content/font-awesome.min.css"));


            bundles.Add(new ScriptBundle("~/bundles/datatables").Include(
                    "~/Scripts/datatables.min.js",
                    "~/Scripts/date-eu.js",
                    "~/Scripts/dataTables.responsive.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                    "~/Scripts/jquery-ui.js"));

        }
    }
}
