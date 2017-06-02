using System.Web.Mvc;

namespace Wms12m.Presentation.Areas.Approval
{
    public class ApprovalAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Approval";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Approval_default",
                "Approval/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new[] { "Wms12m.Presentation.Areas.Approval.Controllers" }
            );
        }
    }
}