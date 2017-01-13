using System.Web;
using System.Web.Optimization;

namespace innovations4austria
{
    public class BundleConfig
    {
        // Weitere Informationen zu Bundling finden Sie unter "http://go.microsoft.com/fwlink/?LinkId=301862"
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/assets/js").Include(
                        "~/assets/js/main.js",
                        "~/assets/js/util.js",
                        "~/assets/js/jquery.min.js",
                        "~/assets/js/jquery.dropotron.min.js",
                        "~/assets/js/skel.min.js",
                        "~/assets/js/ie/html5shiv.js",
                        "~/assets/js/ie/respond.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Verwenden Sie die Entwicklungsversion von Modernizr zum Entwickeln und Erweitern Ihrer Kenntnisse. Wenn Sie dann
            // für die Produktion bereit sind, verwenden Sie das Buildtool unter "http://modernizr.com", um nur die benötigten Tests auszuwählen.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/assets/css").Include(
                "~/assets/css/main.css",
                "~/assets/css/ie8.css",
                "~/assets/css/font-awesome.min.css"));
        }
    }
}
