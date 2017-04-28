﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wms12m.Entity;
using Wms12m.Entity.Models;
using Wms12m.Security;

namespace Wms12m.Business
{
    public class Shelf : abstractTables<Raf>, IDisposable
    {
        Result _Result;
        WMSEntities db = new WMSEntities();
        CustomPrincipal Users = HttpContext.Current.User as CustomPrincipal;
        /// <summary>
        /// ekle ve güncelle
        /// </summary>
        public override Result Operation(Raf tbl)
        {
            _Result = new Result(false, 0);
            //boş mu
            if (tbl.RafAd == "" || tbl.KoridorID == 0)
            {
                _Result.Message = "Eksik Bilgi Girdiniz";
                return _Result;
            }
            //daha önce yazılmış mı
            var kontrol = db.Rafs.Where(m => m.RafAd == tbl.RafAd && m.KoridorID == tbl.KoridorID && m.ID != tbl.ID).FirstOrDefault();
            if (kontrol != null)
            {
                _Result.Message = "Bu isim kullanılıyor";
                return _Result;
            }
            //pasif yapmadan önce içinin boş olması lazım
            if (tbl.ID > 0 && tbl.Aktif == false)
            {
                var kontrol2 = db.Yers.Where(m => m.Miktar > 0 && m.Kat.Bolum.RafID == tbl.ID).FirstOrDefault();
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
                db.Rafs.Add(tbl);
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
                //result
                _Result.Id = tbl.ID;
                _Result.Message = "İşlem Başarılı !!!";
                _Result.Status = true;
            }
            catch (Exception ex)
            {
                db.Logger(Users.AppIdentity.User.UserName, "", "", ex.Message + ex.InnerException != null ? ": " + ex.InnerException : "", ex.InnerException != null ? ex.InnerException.InnerException != null ? ex.InnerException.InnerException.Message : "" : "", "Shelf/Operation");
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
            Raf tbl = db.Rafs.Where(m => m.ID == Id).FirstOrDefault();
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
                db.Logger(Users.AppIdentity.User.UserName, "", "", ex.Message + ex.InnerException != null ? ": " + ex.InnerException : "", ex.InnerException != null ? ex.InnerException.InnerException != null ? ex.InnerException.InnerException.Message : "" : "", "Shelf/Delete");
                _Result.Message = ex.Message;
                _Result.Status = false;
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
                db.Logger(Users.AppIdentity.User.UserName, "", "", ex.Message + ex.InnerException != null ? ": " + ex.InnerException : "", ex.InnerException != null ? ex.InnerException.InnerException != null ? ex.InnerException.InnerException.Message : "" : "", "Shelf/Detail");
                return new Raf();
            }

        }
        /// <summary>
        /// tüm listesi
        /// </summary>
        public override List<Raf> GetList()
        {
            return db.Rafs.OrderBy(m => m.RafAd).ToList();
        }
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
        /// dispose
        /// </summary>
        public void Dispose()
        {
            db.Dispose();
        }
    }
}
