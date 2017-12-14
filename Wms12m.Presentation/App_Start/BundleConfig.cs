using System.Web.Optimization;

namespace Wms12m.Presentation
{
    /// <summary>
    /// http://go.microsoft.com/fwlink/?LinkId=301862
    /// </summary>
    public class BundleConfig
    {
        public static string JsBundle = "~/Content/Scripts/JsBundle.js";
        public static string DtBundle = "~/Content/Scripts/DtBundle.js";
        public static string StBundle = "~/Content/Scripts/StBundle.js";
        public static string CssBundle = "~/Content/Styles/bundle.css";
        public static string LoginJs = "~/Content/Scripts/Login.js";
        public static string LoginCss = "~/Content/Scripts/Login.css";
        public static string jquery = "~/Content/Scripts/jquery.js";
        public static string jqueryUiJs = "~/Content/Scripts/jqueryUi.js";
        public static string jqueryUiCss = "~/Content/Styles/jqueryUi.css";
        public static string bootstrapJs = "~/Content/Scripts/bootstrap.js";
        public static string bootstrapCss = "~/Content/Styles/bootstrap.css";
        public static string FontAwesome = "~/Content/Styles/FontAwesome.css";
        public static string simpleLineIcons = "~/Content/Styles/simpleLineIcons.css";
        public static string toastrJs = "~/Content/Scripts/toastr.js";
        public static string toastrCss = "~/Content/Styles/toastr.css";
        public static string moment = "~/Content/Scripts/moment.js";
        public static string numeral = "~/Content/Scripts/numeral.js";
        public static string bootbox = "~/Content/Scripts/bootbox.js";
        public static string slimscroll = "~/Content/Scripts/slimscroll.js";
        public static string signalR = "~/Content/Scripts/signalR.js";
        public static string blockui = "~/Content/Scripts/blockui.js";
        /// <summary>
        /// register
        /// </summary>
        public static void RegisterBundles(BundleCollection bundles)
        {
            // js libraries
            bundles.Add(new ScriptBundle(jquery, "https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js") { CdnFallbackExpression = "window.jQuery" }.Include("~/Content/assets/global/plugins/jquery/jquery-3.1.1.min.js"));
            bundles.Add(new ScriptBundle(bootstrapJs, "https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js") { CdnFallbackExpression = "$.fn.modal" }.Include("~/Content/assets/global/plugins/bootstrap/js/bootstrap.min.js"));
            bundles.Add(new ScriptBundle(jqueryUiJs, "https://cdnjs.cloudflare.com/ajax/libs/jqueryui/1.12.1/jquery-ui.min.js") { CdnFallbackExpression = "window.jQuery.ui" }.Include("~/Content/assets/global/plugins/jquery-ui/jquery-ui.min.js"));
            bundles.Add(new ScriptBundle(toastrJs, "https://cdnjs.cloudflare.com/ajax/libs/toastr.js/2.1.3/toastr.min.js") { CdnFallbackExpression = "window.jQuery.ui" }.Include("~/Content/assets/global/plugins/toastr/toastr.min.js"));
            bundles.Add(new ScriptBundle(moment, "https://cdnjs.cloudflare.com/ajax/libs/moment.js/2.19.1/moment-with-locales.min.js") { CdnFallbackExpression = "window.jQuery.ui" }.Include("~/Content/assets/global/plugins/moment/moment-with-locales.min.js"));
            bundles.Add(new ScriptBundle(numeral, "https://cdnjs.cloudflare.com/ajax/libs/numeral.js/2.0.6/numeral.min.js") { CdnFallbackExpression = "window.jQuery.ui" }.Include("~/Content/assets/global/plugins/Numeral/min/numeral.min.js"));
            bundles.Add(new ScriptBundle(bootbox, "https://cdnjs.cloudflare.com/ajax/libs/bootbox.js/4.4.0/bootbox.min.js") { CdnFallbackExpression = "window.jQuery.ui" }.Include("~/Content/assets/global/plugins/bootbox/bootbox.min.js"));
            bundles.Add(new ScriptBundle(slimscroll, "https://cdnjs.cloudflare.com/ajax/libs/jQuery-slimScroll/1.3.8/jquery.slimscroll.min.js") { CdnFallbackExpression = "window.jQuery.ui" }.Include("~/Content/assets/global/plugins/jquery-slimscroll/jquery.slimscroll.min.js"));
            bundles.Add(new ScriptBundle(signalR, "https://cdnjs.cloudflare.com/ajax/libs/signalr.js/2.2.2/jquery.signalR.min.js") { CdnFallbackExpression = "window.jQuery.ui" }.Include("~/Content/assets/global/plugins/SignalR/jquery.signalR-2.2.2.min.js"));
            bundles.Add(new ScriptBundle(blockui, "https://cdnjs.cloudflare.com/ajax/libs/jquery.blockUI/2.70/jquery.blockUI.min.js") { CdnFallbackExpression = "window.jQuery.ui" }.Include("~/Content/assets/global/plugins/jquery.blockui.min.js"));
            // css libraries
            bundles.Add(new StyleBundle(bootstrapCss, "https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css").IncludeFallback("~/Content/assets/global/plugins/bootstrap/css/bootstrap.min.css", "sr-only", "width", "1px").Include("~/Content/assets/global/plugins/bootstrap/css/bootstrap.min.css"));
            bundles.Add(new StyleBundle(jqueryUiCss, "https://cdnjs.cloudflare.com/ajax/libs/jqueryui/1.12.1/jquery-ui.min.css").IncludeFallback("~/Content/assets/global/plugins/jquery-ui/jquery-ui.min.css", "ui-icon", "width", "16px").Include("~/Content/assets/global/plugins/jquery-ui/jquery-ui.min.css"));
            bundles.Add(new StyleBundle(toastrCss, "https://cdnjs.cloudflare.com/ajax/libs/toastr.js/2.1.3/toastr.min.css").IncludeFallback("~/Content/assets/global/plugins/toastr/toastr.min.css", "toast-title", "font-weight", "bold").Include("~/Content/assets/global/plugins/toastr/toastr.min.css"));
            bundles.Add(new StyleBundle(FontAwesome, "https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css").IncludeFallback("~/Content/assets/global/plugins/font-awesome/css/font-awesome.min.css", "fa", "text-rendering", "auto").Include("~/Content/assets/global/plugins/font-awesome/css/font-awesome.min.css"));
            bundles.Add(new StyleBundle(simpleLineIcons, "https://cdnjs.cloudflare.com/ajax/libs/simple-line-icons/2.4.1/css/simple-line-icons.css").IncludeFallback("~/Content/assets/global/plugins/simple-line-icons/css/simple-line-icons.min.css", "icon-user", "font-family", "simple-line-icons").Include("~/Content/assets/global/plugins/simple-line-icons/css/simple-line-icons.min.css"));
            // vars
            var scriptBundle = new ScriptBundle(JsBundle);
            var scriptBundle2 = new ScriptBundle(DtBundle);
            var scriptBundle3 = new ScriptBundle(StBundle);
            var styleBundle = new StyleBundle(CssBundle);
            var scriptBundleLogin = new ScriptBundle(LoginJs);
            var styleBundleLogin = new StyleBundle(LoginCss);
            // ///////////////////// scripts
            scriptBundle
                //jquery
                .Include("~/Content/assets/global/plugins/jquery/jquery-migrate-3.0.0.min.js")
                .Include("~/Content/assets/global/plugins/js.cookie.min.js")
                //validation
                .Include("~/Content/assets/global/plugins/jquery-validation/jquery.validate.min.js")
                .Include("~/Content/assets/global/plugins/jquery-validation/jquery.validate.unobtrusive.min.js")
                .Include("~/Content/assets/global/plugins/jquery-validation/jquery.unobtrusive-ajax.min.js")
                .Include("~/Content/assets/global/plugins/jquery-validation/additional-methods.min.js")
                //essentials
                .Include("~/Content/assets/global/plugins/jquery-ui/datepicker-tr.js")
                .Include("~/Content/assets/global/plugins/Numeral/locales.js")
                .Include("~/Content/assets/global/plugins/devextreme/js/dx.all.js")
                ;
            scriptBundle2
                //datatables
                .Include("~/Content/assets/global/plugins/datatables/dataTables.min.js")
                .Include("~/Content/assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.min.js")
                .Include("~/Content/assets/global/scripts/datatable.min.js")
                .Include("~/Content/assets/global/plugins/datatables/extensions/Buttons/js/dataTables.buttons.min.js")
                .Include("~/Content/assets/global/plugins/datatables/extensions/Buttons/js/buttons.print.min.js")
                // JSZip for client side export
                .Include("~/Content/assets/global/plugins/jszip.min.js")
                .Include("~/Content/assets/global/plugins/pdfmake.min.js")
                .Include("~/Content/assets/global/plugins/vfs_fonts.js")
                ;
            scriptBundle3
                //theme scripts
                .Include("~/Content/assets/global/scripts/app.min.js")
                .Include("~/Content/assets/layouts/layout/scripts/layout.min.js")
                .Include("~/Content/Scripts/quick-sidebar.min.js")
                .Include("~/Content/Scripts/Operation.min.js")
                .Include("~/Content/Scripts/MessageBox.min.js")
                .Include("~/Content/Scripts/Functions.min.js")
                .Include("~/Content/Scripts/Hub.min.js")
                ;
            // login bundle
            scriptBundleLogin
                .Include("~/Content/assets/global/plugins/jquery/jquery-migrate-3.0.0.min.js")
                .Include("~/Content/assets/global/plugins/jquery-validation/jquery.validate.min.js")
                .Include("~/Content/assets/global/plugins/jquery-validation/jquery.validate.unobtrusive.min.js")
                .Include("~/Content/assets/global/plugins/jquery-validation/jquery.unobtrusive-ajax.min.js")
                .Include("~/Content/assets/global/plugins/jquery-validation/additional-methods.min.js")
                .Include("~/Content/assets/global/scripts/app.min.js")
                .Include("~/Content/assets/layouts/layout/scripts/layout.min.js")
                .Include("~/Content/Scripts/Operation.min.js")
                .Include("~/Content/Scripts/MessageBox.min.js")
                ;
            // styles
            styleBundle
                // DevExtreme
                .Include("~/Content/assets/global/plugins/DevExtreme/css/dx.common.css")
                .Include("~/Content/assets/global/plugins/DevExtreme/css/dx.spa.css")
                .Include("~/Content/assets/global/plugins/DevExtreme/css/dx.light.css")
                //theme
                .Include("~/Content/assets/global/css/components-md.min.css")
                .Include("~/Content/assets/global/css/plugins-md.min.css")
                .Include("~/Content/assets/layouts/layout/css/layout.min.css")
                .Include("~/Content/assets/layouts/layout/css/themes/darkblue.min.css")
                .Include("~/Content/assets/layouts/layout/css/custom.min.css")
                //datatables
                .Include("~/Content/assets/global/plugins/datatables/dataTables.min.css")
                .Include("~/Content/assets/global/plugins/datatables/extensions/Buttons/css/buttons.dataTables.min.css")
                ;
            // login bundle
            styleBundleLogin
                //theme
                .Include("~/Content/assets/global/css/components-md.min.css")
                .Include("~/Content/assets/global/css/plugins-md.min.css")
                .Include("~/Content/assets/layouts/layout/css/custom.min.css")
                ;
            // bundle
            bundles.Add(scriptBundle);
            bundles.Add(scriptBundle2);
            bundles.Add(scriptBundle3);
            bundles.Add(styleBundle);
            bundles.Add(scriptBundleLogin);
            bundles.Add(styleBundleLogin);
            // EnableOptimizations
#if !DEBUG
            BundleTable.EnableOptimizations = true;
            bundles.UseCdn = false;   //enable CDN support
#endif
        }
    }
}