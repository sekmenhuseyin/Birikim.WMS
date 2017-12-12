using System.Web.Mvc;

namespace Wms12m.Presentation.Areas.Constants
{
    public class ConstantsAreaRegistration : AreaRegistration
    {
        public override string AreaName => "Constants";

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Constants_default",
                "Constants/{controller}/{action}/{id}",
                new { controller = "Storage", action = "Index", id = UrlParameter.Optional },
                new[] { "Wms12m.Presentation.Areas.Constants.Controllers" }
            );
        }
    }
}