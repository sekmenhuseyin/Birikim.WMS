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
        CustomPrincipal Users = HttpContext.Current.User as CustomPrincipal;
        /// <summary>
        /// ekle, güncelle
        /// </summary>
        public override Result Operation(UserDetail tbl)
        {
            _Result = new Result();
            if (tbl.UserID == 0 || tbl.DepoID == 0 || tbl.SatisIrsaliyeSeri < 1 || tbl.SatisIrsaliyeSeri > 199 || tbl.SatisFaturaSeri < 1 || tbl.SatisFaturaSeri > 199 || tbl.TransferInSeri < 1 || tbl.TransferInSeri > 199 || tbl.TransferOutSeri < 1 || tbl.TransferOutSeri > 199)
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
                db.Logger(Users.AppIdentity.User.UserName, "", "", ex.Message + ex.InnerException != null ? ": " + ex.InnerException : "", ex.InnerException != null ? ex.InnerException.InnerException != null ? ex.InnerException.InnerException.Message : "" : "", "Persons/Operation");
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
                db.Logger(Users.AppIdentity.User.UserName, "", "", ex.Message + ex.InnerException != null ? ": " + ex.InnerException : "", ex.InnerException != null ? ex.InnerException.InnerException != null ? ex.InnerException.InnerException.Message : "" : "", "Persons/Delete");
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
