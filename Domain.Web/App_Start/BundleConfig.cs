using System.Web;
using System.Web.Optimization;

namespace Domain.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/bundle/dataTableCSS").Include("~/App_Themes/assets/bs4/plugins/datatables/datatables.min.css",
                                                                         "~/App_Themes/assets/bs4/plugins/datatables/plugins/bootstrap/datatables.bootstrap.css"));

            bundles.Add(new ScriptBundle("~/bundles/dataTableScripts").Include("~/App_Themes/assets/bs4/plugins/datatables/datatables.all.min.js",
                                                                              "~/App_Themes/assets/bs4/plugins/datatables/plugins/input-paging/input.js",
                                                                              "~/App_Themes/assets/bs4/plugins/datatables/plugins/bootstrap/datatables.bootstrap.js",
                                                                              "~/Scripts/Site/datatable-1.0.1.js",
                                                                              "~/Scripts/Site/searchfilter-1.0.0.js"));
        }
    }
}
