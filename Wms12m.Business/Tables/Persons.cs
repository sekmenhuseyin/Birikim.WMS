using System;
using System.Collections.Generic;
using System.Linq;
using Wms12m.Entity;
using Wms12m.Entity.Models;
using Wms12m.Security;

namespace Wms12m.Business
{
    public class Persons : abstractTables<User>, IDisposable
    {
        Result _Result;
        WMSEntities db = new WMSEntities();
        /// <summary>
        /// giriş işlemleri
        /// </summary>
        public Result Login(User P)
        {
            _Result = new Result();
            _Result.Status = false;
            _Result.Message = "İşlem Hata !!!";
            _Result.Id = 0;
            try
            {

                var tbl = db.Users.Where(a => a.Kod.ToLower() == P.Kod.ToLower() && a.Aktif == true).FirstOrDefault();
                if(tbl!=null)
                {
                    if (P.Sifre == CryptographyExtension.Cozumle(tbl.Sifre))
                    {
                        _Result.Status = true;
                        _Result.Id = tbl.ID;
                        _Result.Message = "İşlem Başarılı";
                        _Result.Data = tbl;
                    }
                }
            }
            catch (Exception ex)
            {
                _Result.Message = "İşlem Hata !!!" + ex.Message;
            }
            return _Result;
        }
        /// <summary>
        /// bir kişinin ayrıntıları
        /// </summary>
        public override User Detail(int Id)
        {
            try
            {
                return db.Users.Where(m => m.ID == Id).FirstOrDefault();
            }
            catch (Exception)
            {
                return new User();
            }
        }
        /// <summary>
        /// liste
        /// </summary>
        public override List<User> GetList()
        {
            return db.Users.OrderBy(m => m.AdSoyad).ToList();
        }
        /// <summary>
        /// yetkiye sahip kişiler
        /// </summary>
        public override List<User> GetList(int ParentId)
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
                User tbl = db.Users.Where(m => m.ID == Id).FirstOrDefault();
                if (tbl != null)
                {
                    db.Users.Remove(tbl);
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
        public override Result Operation(User tbl)
        {
            throw new NotImplementedException();
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
