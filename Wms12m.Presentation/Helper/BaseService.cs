using System;
using System.Web.Services;
using Wms12m.Entity.Models;

namespace Wms12m
{
    public class BaseService : WebService, IDisposable
    {
        public WMSEntities db = new WMSEntities();
        /// <summary>
        /// dispose override
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();
            base.Dispose(disposing);
        }
        /// <summary>
        /// hata kaydını tek yerden kontrol etmek için
        /// </summary>
        public void Logger(Exception ex, string page, string user)
        {
            string inner = "";
            if (ex.InnerException != null)
            {
                inner = ex.InnerException == null ? "" : ex.InnerException.Message;
                if (ex.InnerException.InnerException != null) inner += ": " + ex.InnerException.InnerException.Message;
            }
            db.Logger(user, "Android", "", ex.Message, inner, page);
        }
    }
}