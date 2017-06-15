using System;
using System.Collections.Generic;
using System.Linq;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m.Business
{
    public class Combo : abstractTables<Combo_Name>, IDisposable
    {
        /// <summary>
        /// ekle ve güncelle
        /// </summary>
        public override Result Operation(Combo_Name tbl)
        {
            _Result = new Result();
            if (tbl.ComboName == "")
            {
                _Result.Id = 0;
                _Result.Message = "Eksik Bilgi Girdiniz";
                _Result.Status = false;
                return _Result;
            }
            //set details
            if (tbl.ID == 0)
            {
                db.Combo_Name.Add(tbl);
            }
            else
            {
                var tmp = Detail(tbl.ID);
                tmp.ComboName = tbl.ComboName;
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
                helper.Logger(Users.AppIdentity.User.UserName, ex, "Business/Combo/Operation");
                _Result.Id = 0;
                _Result.Message = "İşlem Hatalı: " + ex.Message;
                _Result.Status = false;
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
                Combo_Name tbl = db.Combo_Name.Where(m => m.ID == Id).FirstOrDefault();
                if (tbl != null)
                {
                    db.Combo_Name.Remove(tbl);
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
                helper.Logger(Users.AppIdentity.User.UserName, ex, "Business/Combo/Delete");
                _Result.Message = ex.Message;
                _Result.Status = false;
            }
            return _Result;
        }
        /// <summary>
        /// depo bilgileri
        /// </summary>
        public override Combo_Name Detail(int Id)
        {
            try
            {
                return db.Combo_Name.Where(m => m.ID == Id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                helper.Logger(Users.AppIdentity.User.UserName, ex, "Business/Combo/Detail");
                return new Combo_Name();
            }
        }
        /// <summary>
        /// depo listesi
        /// </summary>
        public override List<Combo_Name> GetList()
        {
            return db.Combo_Name.OrderBy(m => m.ComboName).ToList();
        }
        /// <summary>
        /// üst tabloya ait olanları getir
        /// </summary>
        public override List<Combo_Name> GetList(int ParentId)
        {
            return GetList();
        }
    }
}
