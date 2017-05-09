using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wms12m.Entity;
using Wms12m.Entity.Models;
using Wms12m.Security;

namespace Wms12m.Business
{
    public class PersonPerms : abstractTables<UserDetail>, IDisposable
    {
        Result _Result;
        WMSEntities db = new WMSEntities();
        Helpers helper = new Helpers();
        CustomPrincipal Users = HttpContext.Current.User as CustomPrincipal;
        /// <summary>
        /// ekle, güncelle
        /// </summary>
        public override Result Operation(UserDetail tbl)
        {
            _Result = new Result();
            if (tbl.UserID == 0 || tbl.DepoID == 0)
            {
                _Result.Id = 0;
                _Result.Message = "Eksik Bilgi Girdiniz";
                _Result.Status = false;
                return _Result;
            }
            var tmp = Detail(tbl.UserID);
            if (tmp == null)
                db.UserDetails.Add(tbl);
            else
            {
                tmp.DepoID = tbl.DepoID;
                tmp.SatisIrsaliyeSeri = tbl.SatisIrsaliyeSeri;
                tmp.SatisFaturaSeri = tbl.SatisFaturaSeri;
                tmp.TransferInSeri = tbl.TransferInSeri;
                tmp.TransferOutSeri = tbl.TransferOutSeri;
            }
            try
            {
                db.SaveChanges();
                //result
                _Result.Id = tbl.UserID;
                _Result.Message = "İşlem Başarılı !!!";
                _Result.Status = true;
            }
            catch (Exception ex)
            {
                helper.Logger(Users.AppIdentity.User.UserName, ex, "Business/PersonPerms/Operation");
                _Result.Id = 0;
                _Result.Message = "İşlem Hatalı: " + ex.Message;
                _Result.Status = false;
            }
            return _Result;
        }
        /// <summary>
        /// bir kişinin ayrıntıları
        /// </summary>
        public override UserDetail Detail(int Id)
        {
            return db.UserDetails.Where(m => m.UserID == Id).FirstOrDefault();
        }
        /// <summary>
        /// liste
        /// </summary>
        public override List<UserDetail> GetList()
        {
            return db.UserDetails.ToList();
        }
        /// <summary>
        /// yetkiye sahip kişiler
        /// </summary>
        public override List<UserDetail> GetList(int ParentId)
        {
            return db.UserDetails.ToList();
        }
        /// <summary>
        /// sil
        /// </summary>
        public override Result Delete(int Id)
        {
            _Result = new Result();
            try
            {
                var tbl = db.UserDetails.Where(m => m.UserID == Id).FirstOrDefault();
                if (tbl != null)
                {
                    db.UserDetails.Remove(tbl);
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
                helper.Logger(Users.AppIdentity.User.UserName, ex, "Business/PersonPerms/Delete");
                _Result.Message = ex.Message;
                _Result.Status = false;
            }
            return _Result;
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
