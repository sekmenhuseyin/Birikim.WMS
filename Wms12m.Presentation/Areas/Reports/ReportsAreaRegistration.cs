using System.Web.Mvc;

namespace Wms12m.Presentation.Areas.Reports
{
    public class ReportsAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Reports";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Reports_default",
                "Reports/{controller}/{action}/{id}",
                new { controller = "Financial", action = "Bakiye", id = UrlParameter.Optional },
                new[] { "Wms12m.Presentation.Areas.Reports.Controllers" }
            );
        }
    }
}