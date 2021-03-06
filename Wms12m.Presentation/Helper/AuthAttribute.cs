﻿using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Wms12m.Security;

namespace Wms12m.Presentation
{
    public class AuthAttribute : AuthorizeAttribute
    {
        public string[] SecurityKey { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var user = HttpContext.Current.User as CustomPrincipal;
            return false;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = filterContext.HttpContext.User.Identity.IsAuthenticated
                ? new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "Security",
                    action = "Login"
                }))
                : new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "Security",
                    action = "Login",
                    returnUrl =
                        !string.IsNullOrEmpty(filterContext.HttpContext.Request.Url.PathAndQuery)
                            ? filterContext.HttpContext.Request.Url.PathAndQuery
                            : string.Empty
                }));
        }
    }
}