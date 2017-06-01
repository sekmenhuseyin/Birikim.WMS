using System;
using Wms12m.Entity.Models;

namespace Wms12m
{
    public class Helpers
    {
        /// <summary>
        /// hata kaydını tek yerden kontrol etmek için
        /// </summary>
        public void Logger(string username, Exception ex, string page)
        {
            WMSEntities db = new WMSEntities();
            Functions fn = new Functions();
            string inner = "";
            if (ex.InnerException != null)
            {
                inner = ex.InnerException == null ? "" : ex.InnerException.Message;
                if (ex.InnerException.InnerException != null) inner += ": " + ex.InnerException.InnerException.Message;
            }
            db.Logger(username, "", fn.GetIPAddress(), ex.Message, inner, page);
            db.Dispose();
        }
    }
}