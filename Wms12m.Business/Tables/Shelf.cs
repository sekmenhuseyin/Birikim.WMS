using System;
using System.Collections.Generic;
using System.Linq;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m.Business
{
    public class Shelf : abstractTables<Raf>
    {
        /// <summary>
        /// silme
        /// </summary>
        public override Result Delete(int Id)
        {
            _Result = new Result();
            // kaydı bul
            var tbl = db.Rafs.Where(m => m.ID == Id).FirstOrDefault();
            if (tbl != null)
            {
                if (tbl.Bolums.FirstOrDefault() == null)
                    db.Rafs.Remove(tbl);
                else
                {
                    _Result.Message = "Buraya ait bölüm var";
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
                LogActions("Business", "Shelf", "Delete", ComboItems.alSil, tbl.ID);
                _Result.Id = Id;
                _Result.Message = "İşlem Başarılı !!!";
                _Result.Status = true;
            }
            catch (Exception ex)
            {
                Logger(ex, "Business/Shelf/Delete");
                _Result.Message = ex.Message;
            }

            return _Result;
        }

        /// <summary>
        /// bir tanesinin ayrıntıları
        /// </summary>
        public override Raf Detail(int Id)
        {
            try
            {
                return db.Rafs.Where(m => m.ID == Id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logger(ex, "Business/Shelf/Detail");
                return new Raf();
            }
        }

        /// <summary>
        /// tüm listesi
        /// </summary>
        public override List<Raf> GetList() => db.Rafs.OrderBy(m => m.RafAd).ToList();

        /// <summary>
        /// üst tabloya ait olanları getir
        /// </summary>
        public override List<Raf> GetList(int ParentId)
        {
            return db.Rafs.Where(m => m.KoridorID == ParentId).ToList();
        }

        public List<Raf> GetListByDepo(int DepoID)
        {
            return db.Rafs.Where(m => m.Koridor.DepoID == DepoID).ToList();
        }

        /// <summary>
        /// ekle ve güncelle
        /// </summary>
        public override Result Operation(Raf tbl)
        {
            _Result = new Result(); var eklemi = false;
            // boş mu
            if (tbl.RafAd == "" || tbl.KoridorID == 0)
            {
                _Result.Message = "Eksik Bilgi Girdiniz";
                return _Result;
            }

            // uzun mu
            if (tbl.RafAd.Length > 100)
            {
                _Result.Message = "Daha kısa isim yazın";
                return _Result;
            }

            // daha önce yazılmış mı
            var kontrol = db.Rafs.Where(m => m.RafAd == tbl.RafAd && m.KoridorID == tbl.KoridorID && m.ID != tbl.ID).FirstOrDefault();
            if (kontrol != null)
            {
                _Result.Message = "Bu isim kullanılıyor";
                return _Result;
            }

            // pasif yapmadan önce içinin boş olması lazım
            if (tbl.ID > 0 && tbl.Aktif == false)
            {
                var kontrol2 = db.Yers.Where(m => m.Miktar > 0 && m.Kat.Bolum.RafID == tbl.ID).FirstOrDefault();
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
                db.Rafs.Add(tbl);
                eklemi = true;
            }
            else
            {
                var tmp = Detail(tbl.ID);
                tmp.RafAd = tbl.RafAd;
                tmp.KoridorID = tbl.KoridorID;
                tmp.SiraNo = tbl.SiraNo;
                tmp.Aktif = tbl.Aktif;
                tmp.Degistiren = tbl.Degistiren;
                tmp.DegisTarih = tbl.DegisTarih;
            }

            try
            {
                db.SaveChanges();
                LogActions("Business", "Shelf", "Operation", eklemi == true ? ComboItems.alEkle : ComboItems.alDüzenle, tbl.ID, tbl.RafAd);
                // result
                _Result.Id = tbl.ID;
                _Result.Message = "İşlem Başarılı !!!";
                _Result.Status = true;
            }
            catch (Exception ex)
            {
                Logger(ex, "Business/Shelf/Operation");
                _Result.Id = 0;
                _Result.Message = "İşlem Hatalı: " + ex.Message;
                _Result.Status = false;
            }

            return _Result;
        }
    }
}