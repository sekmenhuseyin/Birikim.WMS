using System;
using System.Web;
using System.Linq;
using Wms12m.Entity;
using Wms12m.Entity.Models;
using Wms12m.Security;
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
        public Result Insert(frmGorev tbl)
        {
            _Result = new Result();
            //add görevlist table
            try
            {
                GorevListesi gorev = new GorevListesi();
                gorev.DepoID = tbl.DepoID;
                gorev.GorevNo = DateTime.Today.ToString("ddMMyy") + "-1";
                gorev.GorevTipiID = ComboNames.MalKabul.ToInt32();
                gorev.DurumID = ComboNames.Açık.ToInt32();
                gorev.OlusturanID = SiteSessions.LoggedUserNo;
                gorev.OlusturmaTarihi = Convert.ToInt32(DateTime.Today.ToOADate());
                gorev.IrsaliyeID = tbl.IrsaliyeID;
                gorev.Bilgi = "IrsNo: " + tbl.IrsaliyeID.ToString();
                db.GorevListesis.Add(gorev);
                db.SaveChanges();
                _Result.Message = "İşlem Başarılı !!!";
                _Result.Status = true;
                _Result.Id = gorev.ID;
            }
            catch (Exception ex)
            {
                _Result.Message = ex.Message + ": " + ex.InnerException.Message;
                _Result.Status = false;
                _Result.Id = 0;
            }
            return _Result;
        }
        /// <summary>
        /// Güncelle
        /// </summary>
        public Result Update(frmGorev tbl)
        {
            _Result = new Result();
            var tmp = db.GorevListesis.Where(m => m.ID == tbl.ID).FirstOrDefault();
            if (tmp != null)
            {
                try
                {
                    tmp.GorevliID = tbl.GorevliID;
                    tmp.Aciklama = tbl.Aciklama;
                    tmp.Bilgi = tbl.Bilgi;
                    tmp.DurumID = tbl.DurumID;
                    db.SaveChanges();
                    _Result.Message = "İşlem Başarılı !!!";
                    _Result.Status = true;
                    _Result.Id = tmp.ID;
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
        /// silme işlemi
        /// </summary>
        public Result Delete(int ID)
        {
            _Result = new Result();
            try
            {
                GorevListesi tbl = db.GorevListesis.Where(m => m.ID == ID).FirstOrDefault();
                if (tbl != null)
                {
                    db.GorevListesis.Remove(tbl);
                    db.SaveChanges();
                    _Result.Message = "İşlem Başarılı !!!";
                    _Result.Id = ID;
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
