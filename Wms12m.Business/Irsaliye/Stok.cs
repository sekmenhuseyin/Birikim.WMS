using System;
using System.Web;
using System.Linq;
using Wms12m.Entity;
using Wms12m.Entity.Models;
using Wms12m.Security;
using Wms12m.Configuration;

namespace Wms12m.Business
{
    public class Stok
    {
        Result _Result;
        WMSEntities db = new WMSEntities();
        CustomPrincipal Users = HttpContext.Current.User as CustomPrincipal;
        /// <summary>
        /// yeni ekle
        /// </summary>
        public Result Insert(frmMalzeme tbl)
        {
            _Result = new Result();
            UserIdentity User= Users.AppIdentity.User;
            try
            {
                WMS_STI tablo = new WMS_STI();
                tablo.IrsaliyeID = tbl.IrsaliyeId;
                tablo.MalKodu = tbl.MalKodu;
                tablo.Birim = tbl.Birim;
                tablo.Miktar = tbl.Miktar;
                db.WMS_STI.Add(tablo);
                db.SaveChanges();
                _Result.Message = "İşlem Başarılı !!!";
                _Result.Status = true;
                _Result.Id = tablo.ID;
            }
            catch (Exception ex)
            {
                _Result.Message = "İşlem Hatalı !!!";
                _Result.Status = false;
                _Result.Id = 0;
            }
            return _Result;
        }
    }
}
