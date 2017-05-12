using System;
using System.Web;
using System.Web.Mvc;
using Wms12m.Security;
using System.Web.Routing;
using System.Web.Security;
using System.Web.Script.Serialization;

namespace Wms12m.Presentation
{
    public class MvcApplication : HttpApplication
    {
        /// <summary>
        /// app start
        /// </summary>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
        /// <summary>
        /// session start for user
        /// </summary>
        protected void Session_Start(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session["Identity"] == null || HttpContext.Current.Session["Identity"] as Identity == null)
            {
                Client.Initialize();
            }
        }
        /// <summary>
        /// after login
        /// </summary>
        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                //serialize
                var serializer = new JavaScriptSerializer();
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                var serializeModel = serializer.Deserialize<CustomPrincipalSerializeModel>(authTicket.UserData);
                //create user
                var newUser = new CustomPrincipal(authTicket.Name);
                newUser.AppIdentity = serializeModel.AppIdentity;
                HttpContext.Current.User = newUser;
            }
        }
    }
}
