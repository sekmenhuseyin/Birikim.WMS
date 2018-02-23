using Birikim.Models;
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
        /// depo silme
        /// </summary>
        public override Result Delete(int Id)
        {
            _Result = new Result();
            try
            {
                var tbl = db.Combo_Name.Where(m => m.ID == Id).FirstOrDefault();
                if (tbl != null)
                {
                    db.Combo_Name.Remove(tbl);
                    db.SaveChanges();
                    LogActions("Business", "Combo", "Delete", ComboItems.alSil, tbl.ID);
                    _Result.Id = Id;
                    _Result.Message = "İşlem Başarılı !!!";
                    _Result.Status = true;
                }
                else
                {
                    _Result.Message = "Kayıt Yok";
                }
            }
            catch (Exception ex)
            {
                Logger(ex, "Business/Combo/Delete");
                _Result.Message = ex.Message;
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
                Logger(ex, "Business/Combo/Detail");
                return new Combo_Name();
            }
        }

        /// <summary>
        /// depo listesi
        /// </summary>
        public override List<Combo_Name> GetList() => db.Combo_Name.OrderBy(m => m.ComboName).ToList();

        /// <summary>
        /// üst tabloya ait olanları getir
        /// </summary>
        public override List<Combo_Name> GetList(int ParentId) => GetList();

        /// <summary>
        /// ekle ve güncelle
        /// </summary>
        public override Result Operation(Combo_Name tbl)
        {
            _Result = new Result(); var eklemi = false;
            // boş mu
            if (tbl.ComboName == "")
            {
                _Result.Message = "Eksik Bilgi Girdiniz";
                return _Result;
            }

            // set details
            if (tbl.ID == 0)
            {
                db.Combo_Name.Add(tbl);
                eklemi = true;
            }
            else
            {
                var tmp = Detail(tbl.ID);
                tmp.ComboName = tbl.ComboName;
            }

            try
            {
                db.SaveChanges();
                LogActions("Business", "Combo", "Operation", eklemi == true ? ComboItems.alEkle : ComboItems.alDüzenle, tbl.ID, tbl.ComboName);
                // result
                _Result.Id = tbl.ID;
                _Result.Message = "İşlem Başarılı !!!";
                _Result.Status = true;
            }
            catch (Exception ex)
            {
                Logger(ex, "Business/Combo/Operation");
                _Result.Message = "İşlem Hatalı: " + ex.Message;
            }

            return _Result;
        }
    }
}