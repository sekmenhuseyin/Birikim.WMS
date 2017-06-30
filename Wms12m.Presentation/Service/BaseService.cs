using System;
using System.Web.Services;
using Wms12m.Entity.Models;

namespace Wms12m
{
    public class BaseService : WebService, IDisposable
    {
        public WMSEntities db = new WMSEntities();
        public YNSEntities dby = new YNSEntities();
        public string AuthPass = "http://www.12mconsulting.com.tr/";
        /// <summary>
        /// dispose override
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
                dby.Dispose();
            }
            base.Dispose(disposing);
        }
        /// <summary>
        /// hata kaydını tek yerden kontrol etmek için
        /// </summary>
        public void Logger(string user, string machine, Exception ex, string page)
        {
            string inner = "";
            if (ex.InnerException != null)
            {
                inner = ex.InnerException == null ? "" : ex.InnerException.Message;
                if (ex.InnerException.InnerException != null) inner += ": " + ex.InnerException.InnerException.Message;
            }
            db.Logger(user, machine, "", ex.Message, inner, page);
        }
        /// <summary>
        /// işlem kaydı
        /// </summary>
        public void LogActions(string user, string machine, string area, string controller, string action, ComboItems type, int ID, string details = "", string request = "")
        {
            db.LogActions("WMS", area, controller, action, type.ToInt32(), ID, request, details, user, machine);
        }
    }
}