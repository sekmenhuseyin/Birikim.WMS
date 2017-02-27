using System;
using System.Web;
using Wms12m.Entity;
using Wms12m.Security;
using System.Web.Security;
using Wms12m.Entity.Models;
using System.Web.Script.Serialization;

namespace Wms12m
{
    public class Authentication
    {
        public static void CreateAuth(USR01 person, bool rememberMe)
        {
            var serializeModel = AuthIdentity(person, rememberMe);
            DateTime expiration = DateTime.Now.AddMinutes(HttpContext.Current.Session.Timeout);
            if (rememberMe)
            {
                expiration = DateTime.MaxValue;
            }
            else
            {
                expiration = DateTime.Now.AddHours(1);
            }
            expiration = DateTime.MaxValue;
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
        public static void UpdateAuth(USR01 person)
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
        private static CustomPrincipalSerializeModel AuthIdentity(USR01 person, bool isUpdate)
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
            return serializeModel;
        }
    }
}