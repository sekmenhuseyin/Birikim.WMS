﻿using System.Web.Mvc;

namespace Wms12m.Presentation.Areas.System
{
    public class SystemAreaRegistration : AreaRegistration
    {
        public override string AreaName => "System";

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "System_default",
                "System/{controller}/{action}/{id}",
                new { controller = "Settings", action = "Index", id = UrlParameter.Optional },
                new[] { "Wms12m.Presentation.Areas.System.Controllers" }
            );
        }
    }
}