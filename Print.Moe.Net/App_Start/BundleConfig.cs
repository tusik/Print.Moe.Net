using System.Web;
using System.Web.Optimization;

namespace Print.Moe.Net
{
    public class BundleConfig
    {
        // 有关捆绑的详细信息，请访问 https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备就绪，请使用 https://modernizr.com 上的生成工具仅选择所需的测试。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
            bundles.Add(new StyleBundle("~/main/css").Include(
                "~/Content/main/css/main.css",
                "~/Content/main/css/normalize.min.css"));
            bundles.Add(new ScriptBundle("~/main/js").Include(
                "~/Content/main/js/*.js"
                ));
            bundles.Add(new ScriptBundle("~/login/css").Include(
                "~/Content/login/css/style.css"
                ));
            bundles.Add(new StyleBundle("~/user/css").Include(
                "~/Content/user/css/demo.css",
                "~/Content/user/css/paper-dashboard.css",
                "~/Content/user/css/themify-icons.css",
                "~/Content/user/css/animate.min.css",
                "~/Content/user/css/bootstrap.min.css"
                ));
            bundles.Add(new ScriptBundle("~/user/js").Include(
                "~/Content/user/js/*.js",
                "~/Content/user/js/bootstrap.min.js",
                "~/Content/user/js/chartist.min.js"
                ));
        }
    }
}
