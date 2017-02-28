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
        /// <summary>
        /// user için kısayol
        /// </summary>
        protected virtual new UserIdentity User
        {
            get
            {
                var u = HttpContext.User as CustomPrincipal;
                if (u != null)
                    return u.AppIdentity.User;
                return null;
            }
        }
        /// <summary>
        /// actiona olmadan hemen önce
        /// </summary>
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
            else
            {
                SiteSessions.LoggedUserNo = User.Id;
                SiteSessions.LoggedRealName = User.FirstName;
                SiteSessions.LoggedUserName = User.UserName;
            }
            logWatch = new Stopwatch();
            logWatch.Start();
            base.OnActionExecuting(filterContext);
        }
        /// <summary>
        /// actiondan sonra
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            logWatch.Stop();
            base.OnActionExecuted(filterContext);
        }
        /// <summary>
        /// dispose override
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();
            base.Dispose(disposing);
        }
    }
}