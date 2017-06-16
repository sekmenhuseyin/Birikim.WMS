using System.Web.Optimization;

namespace Wms12m.Presentation
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static string JsBundle = "~/Content/Scripts/bundle";
        public static string CssBundle = "~/Content/Styles/bundle";
        /// <summary>
        /// register
        /// </summary>
        public static void RegisterBundles(BundleCollection bundles)
        {
            var scriptBundle = new ScriptBundle(JsBundle);
            var styleBundle = new StyleBundle(CssBundle);
            // scripts
            scriptBundle
                .Include("~/Content/assets/global/plugins/jquery/jquery-3.1.1.min.js")
                .Include("~/Content/assets/global/plugins/jquery/jquery-migrate-3.0.0.min.js")
                .Include("~/Content/assets/global/plugins/jquery-validation/jquery.validate.min.js")
                .Include("~/Content/assets/global/plugins/jquery-validation/jquery.validate.unobtrusive.min.js")
                .Include("~/Content/assets/global/plugins/jquery-validation/jquery.unobtrusive-ajax.min.js")
                .Include("~/Content/assets/global/plugins/jquery-validation/additional-methods.min.js")
                .Include("~/Content/assets/global/plugins/bootstrap/js/bootstrap.min.js")
                .Include("~/Content/assets/global/plugins/bootbox/bootbox.min.js")
                .Include("~/Content/assets/global/plugins/js.cookie.min.js")
                .Include("~/Content/assets/global/scripts/app.min.js")
                .Include("~/Content/assets/layouts/layout/scripts/layout.min.js")
                .Include("~/Content/Scripts/Operation.min.js")
                .Include("~/Content/Scripts/MessageBox.min.js")
                .Include("~/Content/Scripts/Functions.min.js")
                ;
            // styles
            styleBundle
                .Include("~/Content/assets/global/css/components-md.min.css")
                .Include("~/Content/assets/global/css/plugins-md.min.css")
                .Include("~/Content/assets/layouts/layout/css/layout.min.css")
                .Include("~/Content/assets/layouts/layout/css/themes/darkblue.min.css")
                .Include("~/Content/assets/layouts/layout/css/custom.min.css")
                ;
            // bundle
            bundles.Add(scriptBundle);
            bundles.Add(styleBundle);
            // EnableOptimizations
#if DEBUG
            BundleTable.EnableOptimizations = false;
#else
            BundleTable.EnableOptimizations = true;
#endif
        }
    }
}
