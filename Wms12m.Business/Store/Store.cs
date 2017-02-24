using System;
using System.Linq;
using System.Collections.Generic;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m.Business
{
    public class Store : abstractStore<TK_DEP>
    {
        Result _Result;
        WMSEntities db = new WMSEntities();
        /// <summary>
        /// güncelle
        /// </summary>
        public override Result Update(TK_DEP tbl)
        {
            _Result = new Result();
            if (tbl.Depo == "" || tbl.DepoKodu == "")
            {
                _Result.Id = 0;
                _Result.Message = "İşlem Hatalı !!!";
                _Result.Status = false;
            }
            else
            {
                try
                {
                    tbl.Degistiren = SiteSessions.LoggedUserName;
                    tbl.DegisTarih = DateTime.Today.ToOADateInt();
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
            }
            return _Result;
        }
        /// <summary>
        /// yeni ekle
        /// </summary>
        public override Result Insert(TK_DEP tbl)
        {
            _Result = new Result();
            if (tbl.Depo=="" || tbl.DepoKodu=="")
            {
            }
            else
            {
                try
                {
                    tbl.Degistiren = SiteSessions.LoggedUserName;
                    tbl.DegisTarih = DateTime.Today.ToOADateInt();
                    tbl.Kaydeden = SiteSessions.LoggedUserName;
                    tbl.KayitTarih = DateTime.Today.ToOADateInt();
                    db.TK_DEP.Add(tbl);
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
            }
            return _Result;
        }
        /// <summary>
        /// depo silme
        /// </summary>
        public override Result Delete(int Id)
        {
            _Result = new Result();
            try
            {
                TK_DEP tbl = db.TK_DEP.Where(m => m.ID == Id).FirstOrDefault();
                if (tbl != null)
                {
                    db.TK_DEP.Remove(tbl);
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
        /// depo bilgileri
        /// </summary>
        public override TK_DEP Detail(int Id)
        {
            try
            {
                return db.TK_DEP.Where(m => m.ID == Id).FirstOrDefault();
            }
            catch (Exception)
            {
                return new TK_DEP();
            }

        }
        /// <summary>
        /// depo listesi
        /// </summary>
        public override List<TK_DEP> GetList()
        {
            return db.TK_DEP.ToList();
        }
        /// <summary>
        /// üst tabloya ait olanları getir
        /// </summary>
        public override List<TK_DEP> GetList(int ParentId)
        {
            return GetList();
        }
        /// <summary>
        /// depo alt listesi
        /// </summary>
        public override List<TK_DEP> SubList(int Id)
        {
            return GetList();
        }
    }
}
