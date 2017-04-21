using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wms12m.Entity;
using Wms12m.Entity.Models;
using Wms12m.Security;

namespace Wms12m.Business
{
    public class Irsaliye : abstractTables<IR>, IDisposable
    {
        Result _Result;
        WMSEntities db = new WMSEntities();
        CustomPrincipal Users = HttpContext.Current.User as CustomPrincipal;
        /// <summary>
        /// ekle/güncelle
        /// </summary>
        public override Result Operation(IR tbl)
        {
            _Result = new Result();
            if (tbl.ID == 0)
            {
                tbl.Kaydeden = Users.AppIdentity.User.Id;
                tbl.KayitTarih = DateTime.Today.ToOADateInt();
                db.IRS.Add(tbl);
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
                db.Logger(Users.AppIdentity.User.UserName, "", "", ex.Message + ex.InnerException != null ? ": " + ex.InnerException : "", ex.InnerException != null ? ex.InnerException.InnerException != null ? ex.InnerException.InnerException.Message : "" : "", "Irsaliye/Operation");
                _Result.Id = 0;
                _Result.Message = "İşlem Hatalı: " + ex.Message;
                _Result.Status = false;
            }
            return _Result;
        }
        /// <summary>
        /// ayrıntılar
        /// </summary>
        public override IR Detail(int Id)
        {
            try
            {
                return db.IRS.Where(m => m.ID == Id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                db.Logger(Users.AppIdentity.User.UserName, "", "", ex.Message + ex.InnerException != null ? ": " + ex.InnerException : "", ex.InnerException != null ? ex.InnerException.InnerException != null ? ex.InnerException.InnerException.Message : "" : "", "Irsaliye/Detail");
                return new IR();
            }
        }
        /// <summary>
        /// liste
        /// </summary>
        public override List<IR> GetList()
        {
            return db.IRS.OrderBy(m => m.EvrakNo).ToList();
        }
        /// <summary>
        /// üst tabloya ait olanları getir
        /// </summary>
        public override List<IR> GetList(int ParentId)
        {
            return GetList();
        }
        /// <summary>
        /// üst tabloya ait olanları getir
        /// </summary>
        public List<IR> GetList(string SirketKod, bool IslemTur, string HesapKodu, int DepoID)
        {
            return db.IRS.Where(m => m.SirketKod == SirketKod && m.IslemTur == IslemTur && m.HesapKodu == HesapKodu && m.DepoID == DepoID).OrderByDescending(m => m.ID).ToList();
        }
        /// <summary>
        /// silme
        /// </summary>
        public override Result Delete(int Id)
        {
            _Result = new Result();
            try
            {
                IR tbl = db.IRS.Where(m => m.ID == Id).FirstOrDefault();
                if (tbl != null)
                {
                    int gid = db.Gorevs.Where(m => m.IrsaliyeID == Id).Select(m => m.ID).FirstOrDefault();
                    db.DeleteFromGorev(gid);
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
                db.Logger(Users.AppIdentity.User.UserName, "", "", ex.Message + ex.InnerException != null ? ": " + ex.InnerException : "", ex.InnerException != null ? ex.InnerException.InnerException != null ? ex.InnerException.InnerException.Message : "" : "", "Irsaliye/Delete");
                _Result.Message = ex.Message;
                _Result.Status = false;
            }
            return _Result;
        }
        /// <summary>
        /// irsaliye onayı bul
        /// </summary>
        public bool GetOnay(int ID)
        {
            return db.IRS.Where(m => m.ID == ID).Select(m => m.Onay).FirstOrDefault();
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
