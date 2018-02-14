using System;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;
using Wms12m.Entity.Models;
using Wms12m.Security;

namespace Wms12m
{
    public class Authentication
    {
        /// <summary>
        /// CreateAuth
        /// </summary>
        public static void CreateAuth(User person, bool rememberMe, GetSirkets_Result sirket)
        {
            var serializeModel = AuthIdentity(person, false, sirket);
            var expiration = DateTime.Now.AddMinutes(HttpContext.Current.Session.Timeout);
            if (rememberMe) expiration = DateTime.MaxValue;
            else expiration = DateTime.Now.AddDays(30);
            var serializer = new JavaScriptSerializer();
            var userData = serializer.Serialize(serializeModel);
            // new ticket
            var authTicket = new FormsAuthenticationTicket(1, person.Kod, DateTime.Now, expiration, rememberMe, userData);
            var encTicket = FormsAuthentication.Encrypt(authTicket);
            // cookie
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
            cookie.Path = FormsAuthentication.FormsCookiePath;
            cookie.Expires = expiration;
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        /// <summary>
        /// UpdateAuth
        /// </summary>
        public static void UpdateAuth(User person, GetSirkets_Result sirket)
        {
            var user = (HttpContext.Current.User as CustomPrincipal).AppIdentity.User;
            var serializeModel = AuthIdentity(person, true, sirket);
            var serializer = new JavaScriptSerializer();
            var userData = serializer.Serialize(serializeModel);
            var cookie = FormsAuthentication.GetAuthCookie(user.UserName, true);
            // get ticket
            var ticket = FormsAuthentication.Decrypt(cookie.Value);
            // set expiration
            var expiration = DateTime.Now.AddDays(HttpContext.Current.Session.Timeout);
            if (ticket.IsPersistent)
                expiration = DateTime.MaxValue;
            else
                expiration = DateTime.Now.AddDays(30);
            // new ticket
            var newticket = new FormsAuthenticationTicket(ticket.Version,
                                                          ticket.Name,
                                                          ticket.IssueDate,
                                                          expiration,
                                                          ticket.IsPersistent,
                                                          userData);
            // cookie
            cookie.Value = FormsAuthentication.Encrypt(newticket);
            cookie.Expires = ticket.Expiration;
            HttpContext.Current.Response.Cookies.Set(cookie);
        }

        /// <summary>
        /// AuthIdentity
        /// </summary>
        private static CustomPrincipalSerializeModel AuthIdentity(User person, bool isUpdate, GetSirkets_Result sirket)
        {
            var identity = HttpContext.Current.User as CustomPrincipal;
            var serializeModel = new CustomPrincipalSerializeModel();
            serializeModel.AppIdentity = new Identity()
            {
                Application = new PresentationIdentity()
                {
                    Application = isUpdate ? identity.AppIdentity.Application.Application : "Wms12m.Presentation",
                    Channel = isUpdate ? identity.AppIdentity.Application.Channel : "WEB-UI"
                },
                User = new UserIdentity()
                {
                    Id = person.ID,
                    Guid = person.Guid.ToString(),
                    FullName = person.AdSoyad,
                    Email = person.Email,
                    UserName = person.Kod,
                    RoleName = person.RoleName,
                    SirketKodu = sirket.Kod,
                    SirketAdi = sirket.Ad
                },
                Action = new ActionIdentity()
                {
                    TrackingCode = isUpdate ? identity.AppIdentity.Action.TrackingCode : Client.GenerateTrackingCode(),
                    Machine = isUpdate ? identity.AppIdentity.Action.Machine : Environment.MachineName,
                    IpAddress = isUpdate ? identity.AppIdentity.Action.IpAddress : HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"],
                    Url = isUpdate ? identity.AppIdentity.Action.Url : string.Empty,
                    Operation = isUpdate ? identity.AppIdentity.Action.Operation : string.Empty
                }
            };
            if (person.UserDetail != null) serializeModel.AppIdentity.User.DepoId = person.UserDetail.DepoID;
            return serializeModel;
        }
    }
}