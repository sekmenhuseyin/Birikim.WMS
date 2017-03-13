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
            try
            {
                if (tbl.ID == 0)
                {
                    tbl.Kaydeden = Users.AppIdentity.User.LogonUserName;
                    tbl.KayitTarih = DateTime.Today.ToOADateInt();
                    db.IRS.Add(tbl);
                }
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
            catch (Exception)
            {
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
                    db.Gorevs.RemoveRange(db.Gorevs.Where(m => m.IrsaliyeID == tbl.ID));
                    db.IRS_Detay.RemoveRange(db.IRS_Detay.Where(m => m.IrsaliyeID == tbl.ID));
                    db.IRS.Remove(tbl);
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
        /// dispose
        /// </summary>
        public void Dispose()
        {
            db.Dispose();
        }
    }
}
