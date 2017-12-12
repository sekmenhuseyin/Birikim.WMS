using System.Web.Mvc;

namespace Wms12m.Presentation.Areas.WMS
{
    public class WMSAreaRegistration : AreaRegistration
    {
        public override string AreaName => "WMS";

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "WMS_default",
                "WMS/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "Wms12m.Presentation.Areas.WMS.Controllers" }
            );
        }
    }
}