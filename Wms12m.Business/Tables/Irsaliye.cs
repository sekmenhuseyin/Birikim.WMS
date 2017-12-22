using System;
using System.Collections.Generic;
using System.Linq;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m.Business
{
    public class Irsaliye : abstractTables<IR>
    {
        /// <summary>
        /// ekle/güncelle
        /// </summary>
        public override Result Operation(IR tbl)
        {
            _Result = new Result(); var eklemi = false;
            if (tbl.ID == 0)
            {
                db.IRS.Add(tbl);
                eklemi = true;
            }

            try
            {
                db.SaveChanges();
                LogActions("Business", "Irsaliye", "Operation", eklemi == true ? ComboItems.alEkle : ComboItems.alDüzenle, tbl.ID, "EvrakNo: " + tbl.EvrakNo + ", Şirket:" + tbl.SirketKod + ", Depo:" + tbl.Depo.DepoKodu);
                // result
                _Result.Id = tbl.ID;
                _Result.Message = "İşlem Başarılı !!!";
                _Result.Status = true;
            }
            catch (Exception ex)
            {
                Logger(ex, "Business/Irsaliye/Operation");
                _Result.Id = 0;
                _Result.Message = "İşlem Hatalı: " + ex.Message;
            }

            return _Result;
        }
        /// <summary>
        /// silme
        /// </summary>
        public override Result Delete(int Id)
        {
            _Result = new Result();
            try
            {
                var tbl = db.IRS.Where(m => m.ID == Id).FirstOrDefault();
                if (tbl != null)
                {
                    db.DeleteIrsaliye(Id);
                    LogActions("Business", "Irsaliye", "Delete", ComboItems.alSil, tbl.ID);
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
                Logger(ex, "Business/Irsaliye/Delete");
                _Result.Message = ex.Message;
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
                Logger(ex, "Business/Irsaliye/Detail");
                return new IR();
            }
        }
        /// <summary>
        /// liste
        /// </summary>
        public override List<IR> GetList() => db.IRS.OrderBy(m => m.EvrakNo).ToList();
        /// <summary>
        /// üst tabloya ait olanları getir
        /// </summary>
        public override List<IR> GetList(int ParentId) => GetList();
        /// <summary>
        /// irsaliye onayı bul
        /// </summary>
        public bool GetOnay(int ID)
        {
            return db.IRS.Where(m => m.ID == ID).Select(m => m.Onay).FirstOrDefault();
        }
    }
}