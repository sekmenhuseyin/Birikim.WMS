using System;
using System.Collections.Generic;
using System.Linq;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m.Business
{
    public class Corridor : abstractTables<Koridor>
    {
        /// <summary>
        /// ekle ve güncelle
        /// </summary>
        public override Result Operation(Koridor tbl)
        {
            _Result = new Result(false, 0);
            //boş mu
            if (tbl.KoridorAd == "" || tbl.DepoID == 0)
            {
                _Result.Message = "Eksik Bilgi Girdiniz";
                return _Result;
            }
            //uzun mu
            if (tbl.KoridorAd.Length > 100)
            {
                _Result.Message = "Daha kısa isim yazın";
                return _Result;
            }
            //daha önce yazılmış mı
            var kontrol = db.Koridors.Where(m => m.KoridorAd == tbl.KoridorAd && m.DepoID == tbl.DepoID && m.ID != tbl.ID).FirstOrDefault();
            if (kontrol != null)
            {
                _Result.Message = "Bu isim kullanılıyor";
                return _Result;
            }
            //pasif yapmadan önce içinin boş olması lazım
            if (tbl.ID > 0 && tbl.Aktif == false)
            {
                var kontrol2 = db.Yers.Where(m => m.Miktar > 0 && m.Kat.Bolum.Raf.KoridorID == tbl.ID).FirstOrDefault();
                if (kontrol2 != null)
                {
                    _Result.Message = "Bu yer kullanılırken pasif yapılamaz";
                    return _Result;
                }
            }
            //set details
            tbl.Degistiren = vUser.UserName;
            tbl.DegisTarih = DateTime.Today.ToOADateInt();
            if (tbl.ID == 0)
            {
                tbl.Kaydeden = vUser.UserName;
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
            try
            {
                db.SaveChanges();
                //result
                _Result.Id = tbl.ID;
                _Result.Message = "İşlem Başarılı !!!";
                _Result.Status = true;
            }
            catch (Exception ex)
            {
                Logger(ex, "Business/Corridor/Operation");
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
            _Result = new Result(false, 0);
            //kaydı bul
            Koridor tbl = db.Koridors.Where(m => m.ID == Id).FirstOrDefault();
            if (tbl != null)
            {
                if (tbl.Rafs.FirstOrDefault() == null)
                    db.Koridors.Remove(tbl);
                else
                {
                    _Result.Message = "Buraya ait raf var";
                    return _Result;
                }
            }
            else
            {
                _Result.Message = "Kayıt Yok";
                _Result.Status = false;
            }
            //sil
            try
            {
                db.SaveChanges();
                _Result.Id = Id;
                _Result.Message = "İşlem Başarılı !!!";
                _Result.Status = true;
            }
            catch (Exception ex)
            {
                Logger(ex, "Business/Corridor/Delete");
                _Result.Message = ex.Message;
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
            catch (Exception ex)
            {
                Logger(ex, "Business/Corridor/Detail");
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
    }
}
