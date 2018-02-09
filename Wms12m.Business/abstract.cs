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
        /// <summary>
        /// result class
        /// </summary>
        public Result _Result;

        /// <summary>
        /// db context
        /// </summary>
        public WMSEntities db = new WMSEntities();

        /// <summary>
        /// functions
        /// </summary>
        public Functions fn = new Functions();

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
        /// delete actions
        /// </summary>
        public abstract Result Delete(int Id);

        /// <summary>
        /// detail
        /// </summary>
        public abstract T Detail(int id);

        /// <summary>
        /// dispose
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(true);  // Violates rule
        }

        /// <summary>
        /// get list
        /// </summary>
        public abstract List<T> GetList();

        /// <summary>
        /// get list from same parent
        /// </summary>
        /// <returns></returns>
        public abstract List<T> GetList(int ParentId);

        /// <summary>
        /// işlem kaydı
        /// </summary>
        public void LogActions(string area, string controller, string action, ComboItems type, int ID, string details = "", string request = "")
        {
            db.LogActions("WMS", area, controller, action, type.ToInt32(), ID, request, details, vUser.UserName, fn.GetIPAddress());
        }

        /// <summary>
        /// hata kaydını tek yerden kontrol etmek için
        /// </summary>
        public void Logger(Exception ex, string page)
        {
            var inner = "";
            if (ex.InnerException != null)
            {
                inner = ex.InnerException == null ? "" : ex.InnerException.Message;
                if (ex.InnerException.InnerException != null) inner += ": " + ex.InnerException.InnerException.Message;
            }

            db.Logger(vUser.UserName, "", fn.GetIPAddress(), ex.Message, inner, page);
        }

        /// <summary>
        /// save and update operations
        /// </summary>
        public abstract Result Operation(T tbl);

        /// <summary>
        /// dispose
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }
        }
    }
}