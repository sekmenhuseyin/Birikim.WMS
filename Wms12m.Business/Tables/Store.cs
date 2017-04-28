using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wms12m.Entity;
using Wms12m.Entity.Models;
using Wms12m.Security;

namespace Wms12m.Business
{
    public class Store : abstractTables<Depo>, IDisposable
    {
        Result _Result;
        WMSEntities db = new WMSEntities();
        CustomPrincipal Users = HttpContext.Current.User as CustomPrincipal;
        /// <summary>
        /// ekle ve güncelle
        /// </summary>
        public override Result Operation(Depo tbl)
        {
            _Result = new Result(false, 0);
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
            tbl.Degistiren = Users.AppIdentity.User.UserName;
            tbl.DegisTarih = DateTime.Today.ToOADateInt();
            if (tbl.ID == 0)
            {
                tbl.Kaydeden = Users.AppIdentity.User.UserName;
                tbl.KayitTarih = DateTime.Today.ToOADateInt();
                db.Depoes.Add(tbl);
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
                //result
                _Result.Id = tbl.ID;
                _Result.Message = "İşlem Başarılı !!!";
                _Result.Status = true;
            }
            catch (Exception ex)
            {
                db.Logger(Users.AppIdentity.User.UserName, "", "", ex.Message + ex.InnerException != null ? ": " + ex.InnerException : "", ex.InnerException != null ? ex.InnerException.InnerException != null ? ex.InnerException.InnerException.Message : "" : "", "Store/Operation");
                _Result.Id = 0;
                _Result.Message = "İşlem Hatalı: " + ex.Message;
                _Result.Status = false;
            }
            return _Result;
        }
        /// <summary>
        /// depo silme
        /// </summary>
        public override Result Delete(int Id)
        {
            _Result = new Result(false, 0);
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
                _Result.Id = Id;
                _Result.Message = "İşlem Başarılı !!!";
                _Result.Status = true;
            }
            catch (Exception ex)
            {
                db.Logger(Users.AppIdentity.User.UserName, "", "", ex.Message + ex.InnerException != null ? ": " + ex.InnerException : "", ex.InnerException != null ? ex.InnerException.InnerException != null ? ex.InnerException.InnerException.Message : "" : "", "Store/Delete");
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
                db.Logger(Users.AppIdentity.User.UserName, "", "", ex.Message + ex.InnerException != null ? ": " + ex.InnerException : "", ex.InnerException != null ? ex.InnerException.InnerException != null ? ex.InnerException.InnerException.Message : "" : "", "Store/Detail");
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
                db.Logger(Users.AppIdentity.User.UserName, "", "", ex.Message + ex.InnerException != null ? ": " + ex.InnerException : "", ex.InnerException != null ? ex.InnerException.InnerException != null ? ex.InnerException.InnerException.Message : "" : "", "Store/Detail");
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
        public List<Depo> GetListCable()
        {
            return db.Depoes.Where(m => m.KabloDepoID != null).OrderBy(m => m.DepoAd).ToList();
        }
        /// <summary>
        /// üst tabloya ait olanları getir
        /// </summary>
        public override List<Depo> GetList(int ParentId)
        {
            return GetList();
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
