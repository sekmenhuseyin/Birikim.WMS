using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wms12m.Entity;
using Wms12m.Entity.Models;
using Wms12m.Security;

namespace Wms12m.Business
{
    public class Corridor : abstractTables<Koridor>, IDisposable
    {
        Result _Result;
        WMSEntities db = new WMSEntities();
        CustomPrincipal Users = HttpContext.Current.User as CustomPrincipal;
        /// <summary>
        /// ekle ve güncelle
        /// </summary>
        public override Result Operation(Koridor tbl)
        {
            _Result = new Result();
            if (tbl.KoridorAd == "" || tbl.DepoID==0)
            {
                _Result.Id = 0;
                _Result.Message = "Eksik Bilgi Girdiniz";
                _Result.Status = false;
                return _Result;
            }
            var kontrol = db.Koridors.Where(m => m.KoridorAd == tbl.KoridorAd && m.DepoID == tbl.DepoID && m.ID != tbl.ID).FirstOrDefault();
            if (kontrol != null)
            {
                _Result.Id = 0;
                _Result.Message = "Bu isim kullanılıyor";
                _Result.Status = false;
                return _Result;
            }
            try
            {
                tbl.Degistiren = Users.AppIdentity.User.LogonUserName;
                tbl.DegisTarih = DateTime.Today.ToOADateInt();
                if (tbl.ID == 0)
                {
                    tbl.Kaydeden = Users.AppIdentity.User.LogonUserName;
                    tbl.KayitTarih = DateTime.Today.ToOADateInt();
                    db.Koridors.Add(tbl);
                }
                else
                {
                    var tmp = Detail(tbl.ID);
                    tmp.KoridorAd = tbl.KoridorAd;
                    tmp.DepoID = tbl.DepoID;
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
                Koridor tbl = db.Koridors.Where(m => m.ID == Id).FirstOrDefault();
                if (tbl != null)
                {
                    db.Koridors.Remove(tbl);
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
        /// <summary>
        /// bir tanesinin ayrıntıları
        /// </summary>
        public override Koridor Detail(int Id)
        {
            try
            {
                return db.Koridors.Where(m => m.ID == Id).FirstOrDefault();
            }
            catch (Exception)
            {
                return new Koridor();
            }
        }
        /// <summary>
        /// tüm listesi
        /// </summary>
        public override List<Koridor> GetList()
        {
            return db.Koridors.OrderBy(m => m.KoridorAd).ToList();
        }
        /// <summary>
        /// üst tabloya ait olanları getir
        /// </summary>
        public override List<Koridor> GetList(int ParentId)
        {
            return db.Koridors.Where(m => m.DepoID == ParentId).ToList();
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
