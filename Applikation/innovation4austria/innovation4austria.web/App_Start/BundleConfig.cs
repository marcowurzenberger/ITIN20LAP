using System.Web;
using System.Web.Optimization;

namespace innovation4austria.web
{
    public class BundleConfig
    {
        // Weitere Informationen zu Bundling finden Sie unter "http://go.microsoft.com/fwlink/?LinkId=301862"
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/toastr").Include(
                        "~/Scripts/toastr.js",
                        "~/Scripts/w3.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery-ajax").Include(
                        "~/Scripts/jquery.unobtrusive-ajax.js"));

            bundles.Add(new ScriptBundle("~/bundles/datepicker").Include(
                        "~/Scripts/moment*",
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/daterangepicker.js"));

            bundles.Add(new ScriptBundle("~/bundles/chart").Include(
                        "~/Scripts/Chart.js"));

            // Verwenden Sie die Entwicklungsversion von Modernizr zum Entwickeln und Erweitern Ihrer Kenntnisse. Wenn Sie dann
            // für die Produktion bereit sind, verwenden Sie das Buildtool unter "http://modernizr.com", um nur die benötigten Tests auszuwählen.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-{version}.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/toastr").Include(
                "~/Content/toastr.css"));

            bundles.Add(new StyleBundle("~/Content/mystyle").Include(
                "~/Content/font-awesome.css",
                "~/Content/w3.css"));

            bundles.Add(new StyleBundle("~/Content/datepicker").Include(
                "~/Content/bootstrap*",
                "~/Content/daterangepicker.css"));

            bundles.Add(new StyleBundle("~/Content/imagepicker").Include(
                "~/Content/image-picker.css"));
        }
    }
}
