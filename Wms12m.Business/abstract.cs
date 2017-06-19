using System;
using System.Collections.Generic;
using System.Web;
using Wms12m.Entity;
using Wms12m.Entity.Models;
using Wms12m.Security;

namespace Wms12m.Business
{
    public abstract class abstractTables<T> : IDisposable
    {
        //public vars
        public Result _Result;
        public WMSEntities db = new WMSEntities();
        public Functions fn = new Functions();
        //abstracts
        public abstract T Detail(int Id);
        public abstract List<T> GetList();
        public abstract List<T> GetList(int ParentId);
        public abstract Result Delete(int Id);
        public abstract Result Operation(T tbl);
        /// <summary>
        /// user için kısayol
        /// </summary>
        protected virtual UserIdentity vUser
        {
            get
            {
                if (HttpContext.Current.User is CustomPrincipal u)
                    return u.AppIdentity.User;
                return new UserIdentity() { UserName = "" };
            }
        }
        /// <summary>
        /// hata kaydını tek yerden kontrol etmek için
        /// </summary>
        public void Logger(Exception ex, string page)
        {
            string inner = "";
            if (ex.InnerException != null)
            {
                inner = ex.InnerException == null ? "" : ex.InnerException.Message;
                if (ex.InnerException.InnerException != null) inner += ": " + ex.InnerException.InnerException.Message;
            }
            db.Logger(vUser.UserName, "", fn.GetIPAddress(), ex.Message, inner, page);
        }
        /// <summary>
        /// işlem kaydı
        /// </summary>
        public void LogActions(string area, string controller, string action, ComboItems type, int ID, string details = "", string request = "")
        {
            db.LogActions("WMS", area, controller, action, type.ToInt32(), ID, request, details, vUser.UserName, fn.GetIPAddress());
        }
        /// <summary>
        /// dispose
        /// </summary>
        public void Dispose()
        {
            db.Dispose();
        }
    }
}
