using System.Web.Mvc;

namespace Wms12m.Presentation.Areas.Approvals
{
    public class ApprovalAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Approvals";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Approval_default",
                "Approvals/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new[] { "Wms12m.Presentation.Areas.Approvals.Controllers" }
            );
        }
    }
}