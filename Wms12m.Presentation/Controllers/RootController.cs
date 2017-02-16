using System.Diagnostics;
using System.Net;
using System.Web.Mvc;
using System.Web.Routing;
using Wms12m.Entity.Models;
using Wms12m.Security;

namespace Wms12m.Presentation.Controllers
{
    public class RootController : Controller
    {
        // GET: Root
        Stopwatch logWatch;
        public WMSEntities db = new WMSEntities();
    
        protected virtual new UserIdentity User
        {
            get
            {
                var u = HttpContext.User as CustomPrincipal;
                if (u != null)
                {
                    return u.AppIdentity.User;
                }

                return null;
            }
        }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (User == null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "Security",
                    action = "Login",
                    status = (int)HttpStatusCode.NotImplemented //Security Attribute koyulmalı!!!
                }));
                return;
            }
            logWatch = new Stopwatch();
            logWatch.Start();
            base.OnActionExecuting(filterContext);
        }
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            logWatch.Stop();
            base.OnActionExecuted(filterContext);
        }
    }
}