using System;
using System.Collections.Generic;
using System.Linq;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m.Business
{
    public class TaskYer : abstractTables<GorevYer>, IDisposable
    {
        Result _Result;
        WMSEntities db = new WMSEntities();
        /// <summary>
        /// ekle ve güncelle
        /// </summary>
        public override Result Operation(GorevYer tbl)
        {
            _Result = new Result();
            try
            {
                if (tbl.ID == 0)
                {
                    db.GorevYers.Add(tbl);
                }
                else
                {
                    var tmp = Detail(tbl.ID);
                    tmp.Sira = tbl.Sira;
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
        /// depo silme
        /// </summary>
        public override Result Delete(int Id)
        {
            _Result = new Result();
            try
            {
                GorevYer tbl = db.GorevYers.Where(m => m.ID == Id).FirstOrDefault();
                if (tbl != null)
                {
                    db.GorevYers.Remove(tbl);
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
        /// depo bilgileri
        /// </summary>
        public override GorevYer Detail(int Id)
        {
            try
            {
                return db.GorevYers.Where(m => m.ID == Id).FirstOrDefault();
            }
            catch (Exception)
            {
                return new GorevYer();
            }
        }
        /// <summary>
        /// depo listesi
        /// </summary>
        public override List<GorevYer> GetList()
        {
            return db.GorevYers.OrderBy(m=>m.Sira).ThenBy(m=>m.MalKodu).ToList();
        }
        /// <summary>
        /// üst tabloya ait olanları getir
        /// </summary>
        public override List<GorevYer> GetList(int ParentId)
        {
            return db.GorevYers.Where(m => m.GorevID == ParentId).OrderBy(m => m.Sira).ThenBy(m => m.MalKodu).ToList();
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
