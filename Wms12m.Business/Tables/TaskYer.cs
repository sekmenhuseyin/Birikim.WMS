using System;
using System.Collections.Generic;
using System.Linq;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m.Business
{
    public class TaskYer : abstractTables<GorevYer>
    {
        /// <summary>
        /// ekle ve güncelle
        /// </summary>
        public override Result Operation(GorevYer tbl)
        {
            _Result = new Result(); var eklemi = false;
            if (tbl.ID == 0)
            {
                db.GorevYers.Add(tbl);
                eklemi = true;
            }
            else
            {
                var tmp = Detail(tbl.ID);
                tmp.Sira = tbl.Sira;
            }

            try
            {
                db.SaveChanges();
                LogActions("Business", "TaskYer", "Operation", eklemi == true ? ComboItems.alEkle : ComboItems.alDüzenle, tbl.ID, "GorevID: " + tbl.GorevID + ", YerID: " + tbl.YerID + ", MalKodu: " + tbl.MalKodu + ", Miktar: " + tbl.Miktar);
                // result
                _Result.Id = tbl.ID;
                _Result.Message = "İşlem Başarılı !!!";
                _Result.Status = true;
            }
            catch (Exception ex)
            {
                Logger(ex, "Business/TaskYer/Operation");
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
                var tbl = db.GorevYers.Where(m => m.ID == Id).FirstOrDefault();
                if (tbl != null)
                {
                    db.GorevYers.Remove(tbl);
                    db.SaveChanges();
                    LogActions("Business", "TaskYer", "Delete", ComboItems.alSil, tbl.ID);
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
                Logger(ex, "Business/TaskYer/Delete");
                _Result.Message = ex.Message;
                _Result.Status = false;
            }

            return _Result;
        }
        /// <summary>
        /// depo bilgileri
        /// </summary>
        public override GorevYer Detail(int Id)
        {
            try
            {
                return db.GorevYers.Where(m => m.ID == Id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logger(ex, "Business/TaskYer/Detail");
                return new GorevYer();
            }
        }
        /// <summary>
        /// depo listesi
        /// </summary>
        public override List<GorevYer> GetList()
        {
            return db.GorevYers.OrderBy(m => m.Sira).ThenBy(m => m.MalKodu).ToList();
        }
        /// <summary>
        /// üst tabloya ait olanları getir
        /// </summary>
        public override List<GorevYer> GetList(int ParentId)
        {
            return db.GorevYers.Where(m => m.GorevID == ParentId).OrderBy(m => m.Sira).ThenBy(m => m.MalKodu).ToList();
        }
    }
}