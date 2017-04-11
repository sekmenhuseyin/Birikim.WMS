using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wms12m.Entity;
using Wms12m.Entity.Models;
using Wms12m.Security;

namespace Wms12m.Business
{
    public class Transfers : abstractTables<Transfer>, IDisposable
    {
        Result _Result;
        WMSEntities db = new WMSEntities();
        CustomPrincipal Users = HttpContext.Current.User as CustomPrincipal;
        /// <summary>
        /// save
        /// </summary>
        public override Result Operation(Transfer tbl)
        {
            _Result = new Result();
            try
            {
                if (tbl.ID == 0)
                {
                    tbl.Onay = false;
                    tbl.OlusturanID = Users.AppIdentity.User.Id;
                    tbl.OlusturmaTarihi = DateTime.Today.ToOADateInt();
                    tbl.OlusturmaSaati = DateTime.Now.SaatiAl();
                    db.Transfers.Add(tbl);
                }
                else
                {
                    var tmp = Detail(tbl.ID);
                    tmp.Onay = tbl.Onay;
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
        public Result AddDetay(Transfer_Detay tbl)
        {
            _Result = new Result();
            try
            {
                db.Transfer_Detay.Add(tbl);
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
        /// delete
        /// </summary>
        public override Result Delete(int Id)
        {
            _Result = new Result();
            try
            {
                var tbl = db.Transfers.Where(m => m.ID == Id).FirstOrDefault();
                if (tbl != null)
                {
                    db.Transfers.Remove(tbl);
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
        /// details
        /// </summary>
        public override Transfer Detail(int Id)
        {
            return db.Transfers.Where(m => m.ID == Id).FirstOrDefault();
        }
        /// <summary>
        /// get list
        /// </summary>
        /// <returns></returns>
        public override List<Transfer> GetList()
        {
            return db.Transfers.ToList();
        }
        /// <summary>
        /// get list from parent
        /// </summary>
        public override List<Transfer> GetList(int ParentId)
        {
            return GetList();
        }
        public List<Transfer> GetList(bool onay)
        {
            return db.Transfers.Where(m => m.Onay == onay).ToList();
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
