using System.Web.Mvc;

namespace Wms12m.Presentation.Areas.UYS
{
    public class UYSAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "UYS";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "UYS_default",
                "UYS/{controller}/{action}/{id}",
                new { controller = "Transfer", action = "Index", id = UrlParameter.Optional },
                new[] { "Wms12m.Presentation.Areas.UYS.Controllers" }
            );
        }
    }
}