using System;
using System.Collections.Generic;
using System.Linq;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m.Business
{
    public class Stok : abstractTables<WMS_STI>
    {
        Result _Result;
        WMSEntities db = new WMSEntities();
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
        public override Result Delete(int Id)
        {
            _Result = new Result();
            try
            {
                WMS_STI tbl = db.WMS_STI.Where(m => m.ID == Id).FirstOrDefault();
                if (tbl != null)
                {
                    db.WMS_STI.Remove(tbl);
                    db.SaveChanges();
                    _Result.Id = Id;
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

        public override WMS_STI Detail(int Id)
        {
            throw new NotImplementedException();
        }

        public override List<WMS_STI> GetList()
        {
            throw new NotImplementedException();
        }

        public override List<WMS_STI> GetList(int ParentId)
        {
            throw new NotImplementedException();
        }

        public override Result Operation(WMS_STI tbl)
        {
            throw new NotImplementedException();
        }
    }
}
