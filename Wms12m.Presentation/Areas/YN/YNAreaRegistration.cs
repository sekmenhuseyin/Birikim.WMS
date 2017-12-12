using System.Web.Mvc;

namespace Wms12m.Presentation.Areas.YN
{
    public class YNAreaRegistration : AreaRegistration
    {
        public override string AreaName => "YN";

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "YN_default",
                "YN/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "Wms12m.Presentation.Areas.YN.Controllers" }
            );
        }
    }
}