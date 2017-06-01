using System;
using System.Web;
using System.Web.Mvc;
using Wms12m.Security;
using System.Web.Routing;
using System.Web.Security;
using System.Web.Script.Serialization;
using System.Collections.Generic;

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
            // DataTables.AspNet registration with default options.
            var options = new DataTables.AspNet.Mvc5.Options()
                .EnableRequestAdditionalParameters()
                .EnableResponseAdditionalParameters();
            //ParseAdditionalParameters
            var binder = new DataTables.AspNet.Mvc5.ModelBinder();
            binder.ParseAdditionalParameters = MyParseFunction;
            //register
            DataTables.AspNet.Mvc5.Configuration.RegisterDataTables(options, binder);
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
        // Parses custom parameters sent with the request.
        public IDictionary<string, object> MyParseFunction(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var p1 = Convert.ToString(bindingContext.ValueProvider.GetValue("param1").AttemptedValue);
            var p2 = Convert.ToInt32(bindingContext.ValueProvider.GetValue("param2").AttemptedValue);
            return new Dictionary<string, object>()
            {
                { "param1", p1 },
                { "param2", p2 }
            };
        }
    }
}
