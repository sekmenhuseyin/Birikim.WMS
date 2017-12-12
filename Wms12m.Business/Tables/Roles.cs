using System;
using System.Collections.Generic;
using System.Linq;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m.Business
{
    public class Roles : abstractTables<Role>
    {
        /// <summary>
        /// ekle ve güncelle
        /// </summary>
        public override Result Operation(Role tbl)
        {
            _Result = new Result();
            if (tbl.RoleName == "")
            {
                _Result.Id = 0;
                _Result.Message = "Eksik Bilgi Girdiniz";
                _Result.Status = false;
                return _Result;
            }

            // set details
            if (tbl.ID == 0)
                db.Roles.Add(tbl);
            try
            {
                db.SaveChanges();
                LogActions("Business", "Combo", "Operation", ComboItems.alEkle, tbl.ID, tbl.RoleName);
                // result
                _Result.Id = tbl.ID;
                _Result.Message = "İşlem Başarılı !!!";
                _Result.Status = true;
            }
            catch (Exception ex)
            {
                Logger(ex, "Business/Roles/Operation");
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
                var tbl = db.Roles.Where(m => m.ID == Id).FirstOrDefault();
                if (tbl != null)
                {
                    db.Roles.Remove(tbl);
                    db.SaveChanges();
                    LogActions("Business", "Roles", "Delete", ComboItems.alSil, tbl.ID);
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
                Logger(ex, "Business/Roles/Delete");
                _Result.Message = ex.Message;
                _Result.Status = false;
            }

            return _Result;
        }
        /// <summary>
        /// depo bilgileri
        /// </summary>
        public override Role Detail(int Id)
        {
            try
            {
                return db.Roles.Where(m => m.ID == Id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logger(ex, "Business/Roles/Detail");
                return new Role();
            }
        }
        /// <summary>
        /// depo listesi
        /// </summary>
        public override List<Role> GetList()
        {
            return db.Roles.Where(m => m.RoleName != "").OrderBy(m => m.RoleName).ToList();
        }
        /// <summary>
        /// üst tabloya ait olanları getir
        /// </summary>
        public override List<Role> GetList(int ParentId) => GetList();
    }
}