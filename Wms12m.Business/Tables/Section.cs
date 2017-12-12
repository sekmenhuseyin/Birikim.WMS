using System;
using System.Collections.Generic;
using System.Linq;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m.Business
{
    public class Section : abstractTables<Bolum>
    {
        /// <summary>
        /// ekle ve güncelle
        /// </summary>
        public override Result Operation(Bolum tbl)
        {
            _Result = new Result(); var eklemi = false;
            // boş mu
            if (tbl.BolumAd == "" || tbl.RafID == 0)
            {
                _Result.Message = "Eksik Bilgi Girdiniz";
                return _Result;
            }

            // uzun mu
            if (tbl.BolumAd.Length > 100)
            {
                _Result.Message = "Daha kısa isim yazın";
                return _Result;
            }

            // daha önce yazılmış mı
            var kontrol = db.Bolums.Where(m => m.BolumAd == tbl.BolumAd && m.RafID == tbl.RafID && m.ID != tbl.ID).FirstOrDefault();
            if (kontrol != null)
            {
                _Result.Message = "Bu isim kullanılıyor";
                return _Result;
            }

            // pasif yapmadan önce içinin boş olması lazım
            if (tbl.ID > 0 && tbl.Aktif == false)
            {
                var kontrol2 = db.Yers.Where(m => m.Miktar > 0 && m.Kat.BolumID == tbl.ID).FirstOrDefault();
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
                db.Bolums.Add(tbl);
                eklemi = true;
            }
            else
            {
                var tmp = Detail(tbl.ID);
                tmp.BolumAd = tbl.BolumAd;
                tmp.RafID = tbl.RafID;
                tmp.SiraNo = tbl.SiraNo;
                tmp.Aktif = tbl.Aktif;
                tmp.Degistiren = tbl.Degistiren;
                tmp.DegisTarih = tbl.DegisTarih;
            }

            try
            {
                db.SaveChanges();
                LogActions("Business", "Section", "Operation", eklemi == true ? ComboItems.alEkle : ComboItems.alDüzenle, tbl.ID, tbl.BolumAd);
                // result
                _Result.Id = tbl.ID;
                _Result.Message = "İşlem Başarılı !!!";
                _Result.Status = true;
            }
            catch (Exception ex)
            {
                Logger(ex, "Business/Section/Operation");
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
                var tbl = db.Bolums.Where(m => m.ID == Id).FirstOrDefault();
                if (tbl != null)
                {
                    db.Bolums.Remove(tbl);
                    db.SaveChanges();
                    LogActions("Business", "Section", "Delete", ComboItems.alSil, tbl.ID);
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
                Logger(ex, "Business/Section/Delete");
                _Result.Message = ex.Message;
                _Result.Status = false;
            }

            return _Result;
        }
        /// <summary>
        /// bir tanesinin ayrıntıları
        /// </summary>
        public override Bolum Detail(int Id)
        {
            try
            {
                return db.Bolums.Where(m => m.ID == Id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logger(ex, "Business/Section/Detail");
                return new Bolum();
            }
        }
        /// <summary>
        /// tüm listesi
        /// </summary>
        public override List<Bolum> GetList() => db.Bolums.OrderBy(m => m.BolumAd).ToList();

        /// <summary>
        /// üst tabloya ait olanları getir
        /// </summary>
        public override List<Bolum> GetList(int ParentId)
        {
            return db.Bolums.Where(m => m.RafID == ParentId).ToList();
        }
    }
}