using System;
using System.Web;
using System.Linq;
using Wms12m.Entity;
using Wms12m.Security;
using Wms12m.Entity.Models;
using System.Collections.Generic;

namespace Wms12m.Business
{
    public class Floor : abstractTables<TK_KAT>
    {
        Result _Result;
        WMSEntities db = new WMSEntities();
        CustomPrincipal Users = HttpContext.Current.User as CustomPrincipal;
        /// <summary>
        /// ekle ve güncelle
        /// </summary>
        public override Result Operation(TK_KAT tbl)
        {
            _Result = new Result();
            if (tbl.Kat == "" || tbl.BolumID == 0)
            {
                _Result.Id = 0;
                _Result.Message = "Eksik Bilgi Girdiniz";
                _Result.Status = false;
                return _Result;
            }
            var kontrol = db.TK_KAT.Where(m => m.Kat == tbl.Kat && m.BolumID == tbl.BolumID && m.ID != tbl.ID).FirstOrDefault();
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
                    db.TK_KAT.Add(tbl);
                }
                else
                {
                    var tmp = Detail(tbl.ID);
                    tmp.Kat = tbl.Kat;
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
                TK_KAT tbl = db.TK_KAT.Where(m => m.ID == Id).FirstOrDefault();
                if (tbl != null)
                {
                    db.TK_KAT.Remove(tbl);
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
        public override TK_KAT Detail(int Id)
        {
            try
            {
                return db.TK_KAT.Where(m => m.ID == Id).FirstOrDefault();
            }
            catch (Exception)
            {
                return new TK_KAT();
            }

        }
        /// <summary>
        /// tüm listesi
        /// </summary>
        public override List<TK_KAT> GetList()
        {
            return db.TK_KAT.OrderBy(m => m.Kat).ToList();
        }
        /// <summary>
        /// üst tabloya ait olanları getir
        /// </summary>
        public override List<TK_KAT> GetList(int ParentId)
        {
            return db.TK_KAT.Where(m => m.BolumID == ParentId).ToList();
        }
    }
}
