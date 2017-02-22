using System;
using System.Web;
using System.Linq;
using Wms12m.Entity;
using Wms12m.Entity.Models;
using Wms12m.Security;
using Wms12m.Configuration;

namespace Wms12m.Business
{
    public class Gorev
    {
        Result _Result;
        WMSEntities db = new WMSEntities();
        CustomPrincipal Users = HttpContext.Current.User as CustomPrincipal;
        /// <summary>
        /// yeni ekle
        /// </summary>
        public Result Insert(frmIrsaliye tbl)
        {
            return _Result;
        }
    }
}
