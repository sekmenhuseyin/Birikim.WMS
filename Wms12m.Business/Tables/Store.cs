using System;
using System.Collections.Generic;
using System.Linq;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m.Business
{
    public class Store : abstractTables<Depo>
    {
        /// <summary>
        /// ekle ve güncelle
        /// </summary>
        public override Result Operation(Depo tbl)
        {
            _Result = new Result(); bool eklemi = false;
            //boş mu
            if (tbl.DepoAd == "" || tbl.DepoKodu == "")
            {
                _Result.Message = "Eksik Bilgi Girdiniz";
                return _Result;
            }
            //uzun mu
            if (tbl.DepoAd.Length > 100 || tbl.DepoKodu.Length > 5)
            {
                _Result.Message = "Daha kısa isim yazın";
                return _Result;
            }
            //daha önce yazılmış mı
            var kontrol = db.Depoes.Where(m => (m.DepoAd == tbl.DepoAd && m.ID != tbl.ID) || (m.DepoKodu == tbl.DepoKodu && m.ID != tbl.ID)).FirstOrDefault();
            if (kontrol != null)
            {
                _Result.Message = "Bu isim kullanılıyor";
                return _Result;
            }
            //pasif yapmadan önce içinin boş olması lazım
            if (tbl.ID > 0 && tbl.Aktif == false)
            {
                var kontrol2 = db.Yers.Where(m => m.Miktar > 0 && m.Kat.Bolum.Raf.Koridor.DepoID == tbl.ID).FirstOrDefault();
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
                db.Depoes.Add(tbl);
                eklemi = true;
            }
            else
            {
                var tmp = Detail(tbl.ID);
                tmp.DepoAd = tbl.DepoAd;
                tmp.DepoKodu = tbl.DepoKodu;
                tmp.KabloDepoID = tbl.KabloDepoID;
                tmp.SiraNo = tbl.SiraNo;
                tmp.Aktif = tbl.Aktif;
                tmp.Degistiren = tbl.Degistiren;
                tmp.DegisTarih = tbl.DegisTarih;
            }
            try
            {
                db.SaveChanges();
                LogActions("Business", "Store", "Operation", eklemi == true ? ComboItems.alEkle : ComboItems.alDüzenle, tbl.ID, tbl.DepoAd + ", " + tbl.DepoKodu);
                //result
                _Result.Id = tbl.ID;
                _Result.Message = "İşlem Başarılı !!!";
                _Result.Status = true;
            }
            catch (Exception ex)
            {
                Logger(ex, "Business/Store/Operation");
                _Result.Message = "İşlem Hatalı: " + ex.Message;
            }
            return _Result;
        }
        /// <summary>
        /// depo silme
        /// </summary>
        public override Result Delete(int Id)
        {
            _Result = new Result();
            //kaydı bul
            var tbl = db.Depoes.Where(m => m.ID == Id).FirstOrDefault();
            if (tbl != null)
            {
                if (tbl.Koridors.FirstOrDefault() == null)
                    db.Depoes.Remove(tbl);
                else
                {
                    _Result.Message = "Buraya ait koridor var";
                    return _Result;
                }
            }
            else
            {
                _Result.Message = "Kayıt Yok";
                return _Result;
            }
            //sil
            try
            {
                db.SaveChanges();
                LogActions("Business", "Store", "Delete", ComboItems.alSil, tbl.ID);
                _Result.Id = Id;
                _Result.Message = "İşlem Başarılı !!!";
                _Result.Status = true;
            }
            catch (Exception ex)
            {
                Logger(ex, "Business/Store/Delete");
                _Result.Message = ex.Message;
            }
            return _Result;
        }
        /// <summary>
        /// depo bilgileri
        /// </summary>
        public override Depo Detail(int Id)
        {
            try
            {
                return db.Depoes.Where(m => m.ID == Id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logger(ex, "Business/Store/Detail");
                return new Depo();
            }
        }
        public Depo Detail(string Kod)
        {
            try
            {
                return db.Depoes.Where(m => m.DepoKodu == Kod).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logger(ex, "Business/Store/Detail");
                return new Depo();
            }
        }
        /// <summary>
        /// depo listesi
        /// </summary>
        public override List<Depo> GetList()
        {
            return db.Depoes.OrderBy(m => m.DepoAd).ToList();
        }
        /// <summary>
        /// kablo siparişi için liste
        /// </summary>
        public List<Depo> GetListCable(int? Id)
        {
            if (Id == null)
                return db.Depoes.Where(m => m.KabloDepoID != null).OrderBy(m => m.DepoAd).ToList();
            else
                return db.Depoes.Where(m => m.KabloDepoID != null && m.ID == Id).ToList();
        }
        /// <summary>
        /// üst tabloya ait olanları getir
        /// </summary>
        public override List<Depo> GetList(int ParentId)
        {
            return GetList();
        }
        public List<Depo> GetList(int? Id)
        {
            if (Id == null)
                return GetList();
            else
                return db.Depoes.Where(m => m.ID == Id).ToList();
        }
    }
}
