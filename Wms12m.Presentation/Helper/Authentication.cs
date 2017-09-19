using System;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;
using Wms12m.Entity;
using Wms12m.Entity.Models;
using Wms12m.Security;

namespace Wms12m
{
    public class Authentication
    {
        public static void CreateAuth(User person, bool rememberMe)
        {
            var serializeModel = AuthIdentity(person, false);
            DateTime expiration = DateTime.Now.AddMinutes(HttpContext.Current.Session.Timeout);
            if (rememberMe)
                expiration = DateTime.MaxValue;
            else
                expiration = DateTime.Now.AddMinutes(30);
            var serializer = new JavaScriptSerializer();
            string userData = serializer.Serialize(serializeModel);
            var authTicket = new FormsAuthenticationTicket(1,
                                                           person.Kod,
                                                           DateTime.Now,
                                                           expiration,
                                                           rememberMe,
                                                           userData);
            string encTicket = FormsAuthentication.Encrypt(authTicket);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
            cookie.Path = FormsAuthentication.FormsCookiePath;
            cookie.Expires = expiration;
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
        public static void UpdateAuth(User person)
        {
            var user = (HttpContext.Current.User as CustomPrincipal).AppIdentity.User;
            var serializeModel = AuthIdentity(person, true);
            var serializer = new JavaScriptSerializer();
            string userData = serializer.Serialize(serializeModel);
            HttpCookie cookie = FormsAuthentication.GetAuthCookie(user.UserName, true);
            var ticket = FormsAuthentication.Decrypt(cookie.Value);
            var newticket = new FormsAuthenticationTicket(ticket.Version,
                                                          ticket.Name,
                                                          ticket.IssueDate,
                                                          ticket.Expiration,
                                                          ticket.IsPersistent,
                                                          userData);
            cookie.Value = FormsAuthentication.Encrypt(newticket);
            cookie.Expires = ticket.Expiration;
            HttpContext.Current.Response.Cookies.Set(cookie);
        }
        private static CustomPrincipalSerializeModel AuthIdentity(User person, bool isUpdate)
        {
            var identity = HttpContext.Current.User as CustomPrincipal;
            var serializeModel = new CustomPrincipalSerializeModel();
            serializeModel.AppIdentity = new Identity()
            {
                Application = new PresentationIdentity()
                {
                    Application = isUpdate ? identity.AppIdentity.Application.Application : BaseConfigurationSection.Current.Presentation.Application,
                    Channel = isUpdate ? identity.AppIdentity.Application.Channel : BaseConfigurationSection.Current.Presentation.Channel
                },
                User = new UserIdentity()
                {
                    UserName = person.Kod,
                    RoleName = person.RoleName,
                    LogonUserName = person.AdSoyad,
                    FirstName = person.AdSoyad,
                    Id = person.ID
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