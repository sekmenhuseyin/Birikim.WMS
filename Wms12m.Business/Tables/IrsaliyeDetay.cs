﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wms12m.Entity;
using Wms12m.Entity.Models;
using Wms12m.Security;

namespace Wms12m.Business
{
    public class IrsaliyeDetay : abstractTables<IRS_Detay>, IDisposable
    {
        Result _Result;
        WMSEntities db = new WMSEntities();
        CustomPrincipal Users = HttpContext.Current.User as CustomPrincipal;
        /// <summary>
        /// ekle/güncelle
        /// </summary>
        public override Result Operation(IRS_Detay tbl)
        {
            _Result = new Result();
            if (tbl.Miktar <= 0)
            {
                _Result.Id = 0;
                _Result.Message = "Eksik Bilgi Girdiniz";
                _Result.Status = false;
                return _Result;
            }
            //set details
            if (tbl.ID == 0)
                db.IRS_Detay.Add(tbl);
            else
            {
                var tmp = Detail(tbl.ID);
                tmp.IrsaliyeID = tbl.IrsaliyeID;
                tmp.MalKodu = tbl.MalKodu;
                tmp.Birim = tbl.Birim;
                tmp.Miktar = tbl.Miktar;
                if (tbl.YerlestirmeMiktari != null) tmp.YerlestirmeMiktari = tbl.YerlestirmeMiktari;
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
                db.Logger(Users.AppIdentity.User.UserName, "", "", ex.Message + ex.InnerException != null ? ": " + ex.InnerException : "", ex.InnerException != null ? ex.InnerException.InnerException != null ? ex.InnerException.InnerException.Message : "" : "", "IrsaliyeDetay/Operation");
                _Result.Id = 0;
                _Result.Message = "İşlem Hatalı: " + ex.Message;
                _Result.Status = false;
            }
            return _Result;
        }
        /// <summary>
        /// yeni ekle
        /// </summary>
        public Result Insert(frmMalzeme tbl)
        {
            _Result = new Result();
            if (tbl.Miktar <= 0)
            {
                _Result.Message = "Miktar hatalı";
                _Result.Status = false;
                _Result.Id = 0;
            }
            else
            {
                try
                {
                    IRS_Detay tablo = new IRS_Detay()
                    {
                        IrsaliyeID = tbl.IrsaliyeId,
                        MalKodu = tbl.MalKodu,
                        Birim = tbl.Birim,
                        Miktar = tbl.Miktar
                    };
                    db.IRS_Detay.Add(tablo);
                    db.SaveChanges();
                    _Result.Message = "İşlem Başarılı !!!";
                    _Result.Status = true;
                    _Result.Id = tablo.ID;
                }
                catch (Exception ex)
                {
                    db.Logger(Users.AppIdentity.User.UserName, "", "", ex.Message + ex.InnerException != null ? ": " + ex.InnerException : "", ex.InnerException != null ? ex.InnerException.InnerException != null ? ex.InnerException.InnerException.Message : "" : "", "IrsaliyeDetay/Insert");
                    _Result.Message = ex.Message;
                    _Result.Status = false;
                    _Result.Id = 0;
                }
            }
            return _Result;
        }
        /// <summary>
        /// silme işlemleri
        /// </summary>
        public override Result Delete(int Id)
        {
            _Result = new Result();
            try
            {
                IRS_Detay tbl = db.IRS_Detay.Where(m => m.ID == Id).FirstOrDefault();
                if (tbl != null)
                {
                    db.IRS_Detay.Remove(tbl);
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
                db.Logger(Users.AppIdentity.User.UserName, "", "", ex.Message + ex.InnerException != null ? ": " + ex.InnerException : "", ex.InnerException != null ? ex.InnerException.InnerException != null ? ex.InnerException.InnerException.Message : "" : "", "IrsaliyeDetay/Delete");
                _Result.Message = ex.Message;
                _Result.Status = false;
            }
            return _Result;
        }
        /// <summary>
        /// ayrıntı
        /// </summary>
        public override IRS_Detay Detail(int Id)
        {
            try
            {
                return db.IRS_Detay.Where(m => m.ID == Id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                db.Logger(Users.AppIdentity.User.UserName, "", "", ex.Message + ex.InnerException != null ? ": " + ex.InnerException : "", ex.InnerException != null ? ex.InnerException.InnerException != null ? ex.InnerException.InnerException.Message : "" : "", "IrsaliyeDetay/Detail");
                return new IRS_Detay();
            }

        }
        /// <summary>
        /// liste
        /// </summary>
        public override List<IRS_Detay> GetList()
        {
            return db.IRS_Detay.OrderBy(m => m.IrsaliyeID).ToList();
        }
        /// <summary>
        /// liste
        /// </summary>
        public override List<IRS_Detay> GetList(int ParentId)
        {
            return db.IRS_Detay.Where(m => m.IrsaliyeID == ParentId).OrderByDescending(m => m.ID).ToList();
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