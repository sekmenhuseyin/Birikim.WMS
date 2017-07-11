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
                // CLDR scripts
                //.Include("~/Content/assets/global/plugins/cldr/cldr.min.js")
                //.Include("~/Content/assets/global/plugins/cldr/event.min.js")
                //.Include("~/Content/assets/global/plugins/cldr/supplemental.min.js")
                //.Include("~/Content/assets/global/plugins/cldr/unresolved.min.js")
                // Globalize 1.x
                //.Include("~/Content/assets/global/plugins/globalize/globalize.min.js")
                //.Include("~/Content/assets/global/plugins/globalize/number.min.js")
                //.Include("~/Content/assets/global/plugins/globalize/currency.min.js")
                //.Include("~/Content/assets/global/plugins/globalize/date.min.js")
                //.Include("~/Content/assets/global/plugins/globalize/message.min.js")
                //jquery
                .Include("~/Content/assets/global/plugins/jquery/jquery-3.1.1.min.js")
                .Include("~/Content/assets/global/plugins/jquery/jquery-migrate-3.0.0.min.js")
                .Include("~/Content/assets/global/plugins/jquery-ui/jquery-ui.min.js")
                .Include("~/Content/assets/global/plugins/jquery-ui/datepicker-tr.js")

                //plugins
                .Include("~/Content/assets/global/plugins/jquery-validation/jquery.validate.min.js")
                .Include("~/Content/assets/global/plugins/jquery-validation/jquery.validate.unobtrusive.min.js")
                .Include("~/Content/assets/global/plugins/jquery-validation/jquery.unobtrusive-ajax.min.js")
                .Include("~/Content/assets/global/plugins/jquery-validation/additional-methods.min.js")
                .Include("~/Content/assets/global/plugins/bootstrap/js/bootstrap.min.js")
                .Include("~/Content/assets/global/plugins/bootbox/bootbox.min.js")
                .Include("~/Content/assets/global/plugins/jquery-slimscroll/jquery.slimscroll.min.js")
                .Include("~/Content/assets/global/plugins/js.cookie.min.js")
                // DevExtreme + extensions
                .Include("~/Content/assets/global/plugins/DevExtreme/js/dx.web.js")
                //.Include("~/Content/assets/global/plugins/DevExtreme/js/dx.aspnet.data.js")
                //.Include("~/Content/assets/global/plugins/DevExtreme/js/dx.aspnet.mvc.js")
                //datatables
                .Include("~/Content/assets/global/plugins/datatables/dataTables.min.js")
                .Include("~/Content/assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.min.js")
                .Include("~/Content/assets/global/scripts/datatable.min.js")
                .Include("~/Content/assets/global/plugins/datatables/extensions/Buttons/js/dataTables.buttons.min.js")
                .Include("~/Content/assets/global/plugins/datatables/extensions/Buttons/js/buttons.flash.min.js")
                .Include("~/Content/assets/global/plugins/datatables/extensions/Buttons/js/buttons.html5.min.js")
                .Include("~/Content/assets/global/plugins/datatables/extensions/Buttons/js/buttons.print.min.js")
                // JSZip for client side export
                .Include("~/Content/assets/global/plugins/jszip.min.js")
                .Include("~/Content/assets/global/plugins/pdfmake.min.js")
                .Include("~/Content/assets/global/plugins/vfs_fonts.js")
                //theme scripts
                .Include("~/Content/assets/global/scripts/app.min.js")
                .Include("~/Content/assets/layouts/layout/scripts/layout.min.js")
                .Include("~/Content/Scripts/Operation.min.js")
                .Include("~/Content/Scripts/MessageBox.min.js")
                .Include("~/Content/Scripts/Functions.js")
                ;
            // styles
            styleBundle
                //jquery
                .Include("~/Content/assets/global/plugins/jquery-ui/jquery-ui.min.css")
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
                .Include("~/Content/assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.min.css")
                .Include("~/Content/assets/global/plugins/datatables/extensions/Buttons/css/buttons.dataTables.min.css")
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
