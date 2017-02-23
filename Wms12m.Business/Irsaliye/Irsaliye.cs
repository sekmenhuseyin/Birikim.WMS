using System;
using System.Web;
using System.Linq;
using Wms12m.Entity;
using Wms12m.Security;
using Wms12m.Entity.Models;

namespace Wms12m.Business
{
    public class Irsaliye
    {
        Result _Result;
        WMSEntities db = new WMSEntities();
        CustomPrincipal Users = HttpContext.Current.User as CustomPrincipal;
        /// <summary>
        /// yeni ekle
        /// </summary>
        public Result Insert(frmIrsaliye tbl)
        {
            _Result = new Result();
            DateTime dateValue = DateTime.Now;
            if (DateTime.TryParse(tbl.Tarih, out dateValue) == true)
            {
                try
                {
                    //add irsaliye table
                    WMS_IRS tablo = new WMS_IRS();
                    tablo.SirketKod = tbl.SirketID;
                    tablo.DepoID = tbl.DepoID;
                    tablo.EvrakNo = tbl.EvrakNo;
                    tablo.HesapKodu = tbl.HesapKodu;
                    tablo.Tarih = Convert.ToInt32(dateValue.ToOADate());
                    tablo.Kaydeden = SiteSessions.LoggedUserName;
                    tablo.KayitTarih = Convert.ToInt32(DateTime.Today.ToOADate());
                    db.WMS_IRS.Add(tablo);
                    db.SaveChanges();
                    //add görevlist table
                    GorevListesi gorev = new GorevListesi();
                    gorev.DepoID = tbl.DepoID;
                    gorev.GorevNo = DateTime.Today.ToString("ddMMyy") + "-1";
                    gorev.GorevTipiID = ComboNames.MalKabul.ToInt32();
                    gorev.DurumID = ComboNames.Açık.ToInt32();
                    gorev.OlusturanID = SiteSessions.LoggedUserNo;
                    gorev.OlusturmaTarihi = Convert.ToInt32(DateTime.Today.ToOADate());
                    gorev.IrsaliyeID = tablo.ID;
                    gorev.Bilgi = "IrsNo: " + tablo.ID.ToString();
                    db.GorevListesis.Add(gorev);
                    db.SaveChanges();
                    //result
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
            else
            {
                _Result.Message = "İşlem Hatalı !!!";
                _Result.Status = false;
                _Result.Id = 0;
            }
            return _Result;
        }
    }
}
