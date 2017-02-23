using System;
using System.Web;
using System.Linq;
using Wms12m.Entity;
using Wms12m.Entity.Models;
using Wms12m.Security;

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
            if (tbl.Miktar<=0)
            {
                _Result.Message = "Miktar hatalı";
                _Result.Status = false;
                _Result.Id = 0;
            }
            else
            {
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
                    _Result.Message = ex.Message + ": " + ex.InnerException.Message;
                    _Result.Status = false;
                    _Result.Id = 0;
                }
            }
            return _Result;
        }
        /// <summary>
        /// silme işlemleri
        /// </summary>
        public Result Delete(int ID)
        {
            _Result = new Result();
            try
            {
                WMS_STI tbl = db.WMS_STI.Where(m => m.ID == ID).FirstOrDefault();
                if (tbl != null)
                {
                    db.WMS_STI.Remove(tbl);
                    db.SaveChanges();
                    _Result.Id = ID;
                    _Result.Message = "İşlem Başarılı !!!";
                    _Result.Status = true;
                }
                else
                {
                    _Result.Message = "Kayıt Yok";
                    _Result.Status = false;
                }
            }
            catch (Exception ex)
            {
                _Result.Message = ex.Message + ": " + ex.InnerException.Message;
                _Result.Status = false;
            }
            return _Result;
        }
    }
}
