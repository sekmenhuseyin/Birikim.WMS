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
        public Result _Result;
        public WMSEntities db = new WMSEntities();
        public Helpers helper = new Helpers();
        public CustomPrincipal Users = HttpContext.Current.User as CustomPrincipal;
        public Functions fn = new Functions();

        public abstract T Detail(int Id);
        public abstract List<T> GetList();
        public abstract List<T> GetList(int ParentId);
        public abstract Result Delete(int Id);
        public abstract Result Operation(T tbl);
        /// <summary>
        /// dispose
        /// </summary>
        public void Dispose()
        {
            db.Dispose();
        }
    }
}
