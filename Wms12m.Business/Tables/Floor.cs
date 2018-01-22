using System;
using System.Collections.Generic;
using System.Linq;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m.Business
{
    public class Floor : abstractTables<Kat>
    {
        /// <summary>
        /// ekle ve güncelle
        /// </summary>
        public override Result Operation(Kat tbl)
        {
            _Result = new Result(); var eklemi = false;
            // boş mu
            if (tbl.KatAd == "" || tbl.BolumID == 0)
            {
                _Result.Message = "Eksik Bilgi Girdiniz";
                return _Result;
            }

            // uzun mu
            if (tbl.KatAd.Length > 100)
            {
                _Result.Message = "Daha kısa isim yazın";
                return _Result;
            }

            // daha önce yazılmış mı
            var kontrol = db.Kats.Where(m => m.KatAd == tbl.KatAd && m.BolumID == tbl.BolumID && m.ID != tbl.ID).FirstOrDefault();
            if (kontrol != null)
            {
                _Result.Message = "Bu isim kullanılıyor";
                return _Result;
            }

            // pasif yapmadan önce içinin boş olması lazım
            if (tbl.ID > 0 && tbl.Aktif == false)
            {
                var kontrol2 = db.Yers.Where(m => m.Miktar > 0 && m.KatID == tbl.ID).FirstOrDefault();
                if (kontrol2 != null)
                {
                    _Result.Message = "Bu yer kullanılırken pasif yapılamaz";
                    return _Result;
                }
            }

            // set details
            tbl.Degistiren = vUser.UserName;
            tbl.DegisTarih = DateTime.Today.ToOADateInt();
            if (tbl.ID == 0)
            {
                tbl.Kaydeden = vUser.UserName;
                tbl.KayitTarih = DateTime.Today.ToOADateInt();
                db.Kats.Add(tbl);
                eklemi = true;
            }
            else
            {
                var tmp = Detail(tbl.ID);
                tmp.KatAd = tbl.KatAd;
                tmp.En = tbl.En;
                tmp.Boy = tbl.Boy;
                tmp.Derinlik = tbl.Derinlik;
                tmp.AgirlikKapasite = tbl.AgirlikKapasite;
                tmp.OzellikID = tbl.OzellikID;
                tmp.Aciklama = tbl.Aciklama;
                tmp.BolumID = tbl.BolumID;
                tmp.SiraNo = tbl.SiraNo;
                tmp.Aktif = tbl.Aktif;
                tmp.Degistiren = tbl.Degistiren;
                tmp.DegisTarih = tbl.DegisTarih;
            }

            try
            {
                db.SaveChanges();
                LogActions("Business", "Floor", "Operation", eklemi == true ? ComboItems.alEkle : ComboItems.alDüzenle, tbl.ID, tbl.KatAd + ", H: " + tbl.En + "x" + tbl.Boy + "x" + tbl.Derinlik + ", A: " + tbl.AgirlikKapasite);
                // result
                _Result.Id = tbl.ID;
                _Result.Message = "İşlem Başarılı !!!";
                _Result.Status = true;
            }
            catch (Exception ex)
            {
                Logger(ex, "Business/Floor/Operation");
                _Result.Id = 0;
                _Result.Message = "İşlem Hatalı: " + ex.Message;
            }

            return _Result;
        }
        /// <summary>
        /// silme
        /// </summary>
        public override Result Delete(int Id)
        {
            _Result = new Result();
            // kaydı bul
            var tbl = db.Kats.Where(m => m.ID == Id).FirstOrDefault();
            var rkat = db.GetHucreKatID(tbl.Bolum.Raf.Koridor.DepoID, "R-ZR-V").FirstOrDefault();
            if (rkat != null)
                if (rkat.Value == tbl.ID)
                {
                    _Result.Message = "Rezerv katı silemezsiniz.";
                    return _Result;
                }

            if (tbl != null)
            {
                if (tbl.Yers.FirstOrDefault() == null)
                    db.Kats.Remove(tbl);
                else
                {
                    _Result.Message = "Buraya ait mal var";
                    return _Result;
                }
            }
            else
            {
                _Result.Message = "Kayıt Yok";
                return _Result;
            }

            // sil
            try
            {
                db.SaveChanges();
                LogActions("Business", "Floor", "Delete", ComboItems.alSil, tbl.ID);
                _Result.Id = Id;
                _Result.Message = "İşlem Başarılı !!!";
                _Result.Status = true;
            }
            catch (Exception ex)
            {
                Logger(ex, "Business/Floor/Delete");
                _Result.Message = ex.Message;
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
                Logger(ex, "Business/Floor/Detail");
                return new Kat();
            }
        }
        /// <summary>
        /// tüm listesi
        /// </summary>
        public override List<Kat> GetList() => db.Kats.OrderBy(m => m.KatAd).ToList();
        /// <summary>
        /// üst tabloya ait olanları getir
        /// </summary>
        public override List<Kat> GetList(int ParentId)
        {
            return db.Kats.Where(m => m.BolumID == ParentId).ToList();
        }
    }
}