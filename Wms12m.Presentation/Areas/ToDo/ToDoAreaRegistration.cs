﻿using System.Web.Mvc;

namespace Wms12m.Presentation.Areas.ToDo
{
    public class ToDoAreaRegistration : AreaRegistration
    {
        public override string AreaName => "ToDo";

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "ToDo_default",
                "ToDo/{controller}/{action}/{id}",
                new { controller = "DutyWork", action = "Index", id = UrlParameter.Optional },
                new[] { "Wms12m.Presentation.Areas.ToDo.Controllers" }
            );
        }
    }
}