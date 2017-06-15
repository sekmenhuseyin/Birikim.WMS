using System;
using System.Collections.Generic;
using System.Linq;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m.Business
{
    public class Transfers : abstractTables<Transfer>
    {
        /// <summary>
        /// save
        /// </summary>
        public override Result Operation(Transfer tbl)
        {
            _Result = new Result();
            //set details
            if (tbl.ID == 0)
            {
                tbl.Onay = false;
                db.Transfers.Add(tbl);
            }
            else
            {
                var tmp = Detail(tbl.ID);
                tmp.Onay = tbl.Onay;
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
                helper.Logger(Users.AppIdentity.User.UserName, ex, "Business/Transfers/Operation");
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
                helper.Logger(Users.AppIdentity.User.UserName, ex, "Business/Transfers/AddDetay");
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
                helper.Logger(Users.AppIdentity.User.UserName, ex, "Business/Transfers/Delete");
                _Result.Message = ex.Message;
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
        public List<Transfer> GetList(bool onay, int? DepoId)
        {
            if (DepoId == null)
                return db.Transfers.Where(m => m.Onay == onay).ToList();
            else
                return db.Transfers.Where(m => m.Onay == onay && m.CikisDepoID == DepoId).ToList();
        }
    }
}
