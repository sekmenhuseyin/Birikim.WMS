using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Script.Serialization;
using System.Web.Security;
using Wms12m.Security;

namespace Wms12m.Presentation
{
    public class MvcApplication : HttpApplication
    {
        /// <summary>
        /// app start
        /// </summary>
        protected void Application_Start()
        {
            //area kayıt
            AreaRegistration.RegisterAllAreas();
            //route kayıt
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //bundle create
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //herkesi offline yap bir seferlik
            Statics.PutUsersOffline();
            //remove the MVC header
            MvcHandler.DisableMvcResponseHeader = true;
        }
        /// <summary>
        /// session start for user
        /// </summary>
        protected void Session_Start(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session["Identity"] == null || HttpContext.Current.Session["Identity"] as Identity == null)
                Client.Initialize();
        }
        /// <summary>
        /// header göndermeden önce bazılarını sil
        /// </summary>
        protected void Application_PreSendRequestHeaders(object sender, EventArgs e)
        {
            if (HttpContext.Current == null) return;
            HttpContext.Current.Response.Headers.Remove("X-Powered-By");
            HttpContext.Current.Response.Headers.Remove("X-AspNet-Version");
            HttpContext.Current.Response.Headers.Remove("X-AspNetMvc-Version");
            HttpContext.Current.Response.Headers.Remove("Server");
        }
        /// <summary>
        /// after login
        /// </summary>
        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
                try
                {
                    //serialize
                    var serializer = new JavaScriptSerializer();
                    FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                    var serializeModel = serializer.Deserialize<CustomPrincipalSerializeModel>(authTicket.UserData);
                    //create user
                    var newUser = new CustomPrincipal(authTicket.Name) { AppIdentity = serializeModel.AppIdentity };
                    HttpContext.Current.User = newUser;
                }
                catch (Exception)
                {
                    authCookie = null;
                    HttpContext.Current.User = null;
                }
        }
    }
}
