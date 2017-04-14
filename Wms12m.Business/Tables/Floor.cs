using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wms12m.Entity;
using Wms12m.Entity.Models;
using Wms12m.Security;

namespace Wms12m.Business
{
    public class Floor : abstractTables<Kat>, IDisposable
    {
        Result _Result;
        WMSEntities db = new WMSEntities();
        CustomPrincipal Users = HttpContext.Current.User as CustomPrincipal;
        /// <summary>
        /// ekle ve güncelle
        /// </summary>
        public override Result Operation(Kat tbl)
        {
            _Result = new Result();
            if (tbl.KatAd == "" || tbl.BolumID == 0)
            {
                _Result.Id = 0;
                _Result.Message = "Eksik Bilgi Girdiniz";
                _Result.Status = false;
                return _Result;
            }
            var kontrol = db.Kats.Where(m => m.KatAd == tbl.KatAd && m.BolumID == tbl.BolumID && m.ID != tbl.ID).FirstOrDefault();
            if (kontrol != null)
            {
                _Result.Id = 0;
                _Result.Message = "Bu isim kullanılıyor";
                _Result.Status = false;
                return _Result;
            }
            try
            {
                tbl.Degistiren = Users.AppIdentity.User.Id;
                tbl.DegisTarih = DateTime.Today.ToOADateInt();
                if (tbl.ID == 0)
                {
                    tbl.Kaydeden = Users.AppIdentity.User.Id;
                    tbl.KayitTarih = DateTime.Today.ToOADateInt();
                    db.Kats.Add(tbl);
                }
                else
                {
                    var tmp = Detail(tbl.ID);
                    tmp.KatAd = tbl.KatAd;
                    tmp.En = tbl.En;
                    tmp.Boy = tbl.Boy;
                    tmp.Derinlik = tbl.Derinlik;
                    tmp.AgirlikKapasite = tbl.AgirlikKapasite;
                    tmp.Ozellik = tbl.Ozellik;
                    tmp.Aciklama = tbl.Aciklama;
                    tmp.BolumID = tbl.BolumID;
                    tmp.SiraNo = tbl.SiraNo;
                    tmp.Aktif = tbl.Aktif;
                    tmp.Degistiren = tbl.Degistiren;
                    tmp.DegisTarih = tbl.DegisTarih;
                }
                db.SaveChanges();
                //result
                _Result.Id = tbl.ID;
                _Result.Message = "İşlem Başarılı !!!";
                _Result.Status = true;
            }
            catch (Exception ex)
            {
                db.Logger(Users.AppIdentity.User.UserName, "", "", ex.Message, ex.InnerException != null ? ex.InnerException.Message : "", "Floor/Operation");
                _Result.Id = 0;
                _Result.Message = "İşlem Hatalı: " + ex.Message;
                _Result.Status = false;
            }
            return _Result;
        }
        /// <summary>
        /// silme
        /// </summary>
        public override Result Delete(int Id)
        {
            _Result = new Result();
            try
            {
                Kat tbl = db.Kats.Where(m => m.ID == Id).FirstOrDefault();
                if (tbl != null)
                {
                    db.Kats.Remove(tbl);
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
                db.Logger(Users.AppIdentity.User.UserName, "", "", ex.Message, ex.InnerException != null ? ex.InnerException.Message : "", "Floor/Delete");
                _Result.Message = ex.Message;
                _Result.Status = false;
            }
            return _Result;
        }
        /// <summary>
        /// bir tanesinin ayrıntıları
        /// </summary>
        public override Kat Detail(int Id)
        {
            try
            {
                return db.Kats.Where(m => m.ID == Id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                db.Logger(Users.AppIdentity.User.UserName, "", "", ex.Message, ex.InnerException != null ? ex.InnerException.Message : "", "Floor/Detail");
                return new Kat();
            }

        }
        /// <summary>
        /// tüm listesi
        /// </summary>
        public override List<Kat> GetList()
        {
            return db.Kats.OrderBy(m => m.KatAd).ToList();
        }
        /// <summary>
        /// üst tabloya ait olanları getir
        /// </summary>
        public override List<Kat> GetList(int ParentId)
        {
            return db.Kats.Where(m => m.BolumID == ParentId).ToList();
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
