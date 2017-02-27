using System;
using System.Linq;
using Wms12m.Entity;
using Wms12m.Security;
using Wms12m.Entity.Models;
using System.Collections.Generic;

namespace Wms12m.Business
{
    public class Persons : abstractTables<USR01>
    {
        Result _Result;
        WMSEntities db = new WMSEntities();
        /// <summary>
        /// giriş işlemleri
        /// </summary>
        public Result Login(USR01 P)
        {
            P.Sifre = CryptographyExtension.Sifrele(P.Sifre);
            _Result = new Result();
            try
            {

                var tbl = db.USR01.Where(a => a.Kod.ToLower() == P.Kod.ToLower() && a.Sifre == P.Sifre && a.Aktif == true).FirstOrDefault();
                if(tbl!=null)
                {
                    _Result.Status = true;
                    _Result.Id = tbl.ID;
                    _Result.Message = "İşlem Başarılı";
                    _Result.Data = tbl;
                    SiteSessions.LoggedUserNo = tbl.ID;
                    SiteSessions.LoggedUserName = tbl.Kod;
                    SiteSessions.LoggedRealName = tbl.AdSoyad;

                }
                else
                {
                    _Result.Status = false;
                    _Result.Message = "İşlem Hata !!!";
                    _Result.Id = 0;
                }
            }
            catch (Exception ex)
            {
                _Result.Status = false;
                _Result.Message = "İşlem Hata !!!"+ex.Message;
                _Result.Id = 0;
            }
            return _Result;
        }
        /// <summary>
        /// bir kişinin ayrıntıları
        /// </summary>
        public override USR01 Detail(int Id)
        {
            try
            {
                return db.USR01.Where(m => m.ID == Id).FirstOrDefault();
            }
            catch (Exception)
            {
                return new USR01();
            }
        }
        /// <summary>
        /// liste
        /// </summary>
        public override List<USR01> GetList()
        {
            return db.USR01.ToList();
        }
        /// <summary>
        /// yetkiye sahip kişiler
        /// </summary>
        public override List<USR01> GetList(int ParentId)
        {
            return GetList();
        }
        /// <summary>
        /// sil
        /// </summary>
        public override Result Delete(int Id)
        {
            _Result = new Result();
            try
            {
                USR01 tbl = db.USR01.Where(m => m.ID == Id).FirstOrDefault();
                if (tbl != null)
                {
                    db.USR01.Remove(tbl);
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
        /// ekle, güncelle
        /// </summary>
        public override Result Operation(USR01 tbl)
        {
            throw new NotImplementedException();
        }
    }
}
