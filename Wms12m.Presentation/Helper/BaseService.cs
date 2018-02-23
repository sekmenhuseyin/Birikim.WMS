using System;
using System.Web.Services;
using Wms12m.Business;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m.Presentation
{
    public class BaseService : WebService
    {
        public string AuthPass = "http://www.12mconsulting.com.tr/";
        public WMSEntities db = new WMSEntities();
        public IrsaliyeDetay IrsaliyeDetay = new IrsaliyeDetay();
        public Yerlestirme Yerlestirme = new Yerlestirme();

        /// <summary>
        /// işlem kaydı
        /// </summary>
        public void LogActions(string user, string machine, string area, string controller, string action, ComboItems type, int ID, string details = "", string request = "")
        {
            db.LogActions("WMS", area, controller, action, type.ToInt32(), ID, request, details, user, machine);
        }

        /// <summary>
        /// hata kaydını tek yerden kontrol etmek için
        /// </summary>
        public void Logger(string user, string machine, Exception ex, string page)
        {
            var inner = "";
            if (ex.InnerException != null)
            {
                inner = ex.InnerException == null ? "" : ex.InnerException.Message;
                if (ex.InnerException.InnerException != null) inner += ": " + ex.InnerException.InnerException.Message;
            }

            db.Logger(user, machine, "", ex.Message, inner, page);
        }

        /// <summary>
        /// dispose override
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
                IrsaliyeDetay.Dispose();
                Yerlestirme.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}