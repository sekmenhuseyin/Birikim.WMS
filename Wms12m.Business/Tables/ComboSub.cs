using System;
using System.Collections.Generic;
using System.Linq;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m.Business
{
    public class ComboSub : abstractTables<ComboItem_Name>
    {
        /// <summary>
        /// ekle ve güncelle
        /// </summary>
        public override Result Operation(ComboItem_Name tbl)
        {
            _Result = new Result(); bool eklemi = false;
            if (tbl.Name == "" || tbl.ComboID == 0)
            {
                _Result.Id = 0;
                _Result.Message = "Eksik Bilgi Girdiniz";
                _Result.Status = false;
                return _Result;
            }
            //set details
            if (tbl.ID == 0)
            {
                db.ComboItem_Name.Add(tbl);
                eklemi = true;
            }
            else
            {
                var tmp = Detail(tbl.ID);
                tmp.Name = tbl.Name;
                tmp.ComboID = tbl.ComboID;
                tmp.Visible = tbl.Visible;
            }
            try
            {
                db.SaveChanges();
                LogActions("Business", "ComboSub", "Operation", eklemi == true ? ComboItems.alEkle : ComboItems.alDüzenle, tbl.ID, tbl.Name + ", CID: " + tbl.ComboID);
                //result
                _Result.Id = tbl.ID;
                _Result.Message = "İşlem Başarılı !!!";
                _Result.Status = true;
            }
            catch (Exception ex)
            {
                Logger(ex, "Business/ComboSub/Operation");
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
                ComboItem_Name tbl = db.ComboItem_Name.Where(m => m.ID == Id).FirstOrDefault();
                if (tbl != null)
                {
                    db.ComboItem_Name.Remove(tbl);
                    db.SaveChanges();
                    LogActions("Business", "Combo", "Delete", ComboItems.alSil, tbl.ID);
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
                Logger(ex, "Business/ComboSub/Delete");
                _Result.Message = ex.Message;
                _Result.Status = false;
            }
            return _Result;
        }
        /// <summary>
        /// depo bilgileri
        /// </summary>
        public override ComboItem_Name Detail(int Id)
        {
            try
            {
                return db.ComboItem_Name.Where(m => m.ID == Id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logger(ex, "Business/ComboSub/Detail");
                return new ComboItem_Name();
            }
        }
        /// <summary>
        /// depo listesi
        /// </summary>
        public override List<ComboItem_Name> GetList()
        {
            return db.ComboItem_Name.OrderBy(m => m.Name).ToList();
        }
        public List<ComboItem_Name> GetList(bool visible)
        {
            return db.ComboItem_Name.Where(m => m.Visible == visible).OrderBy(m => m.Name).ToList();
        }
        /// <summary>
        /// üst tabloya ait olanları getir
        /// </summary>
        public override List<ComboItem_Name> GetList(int ParentId)
        {
            return db.ComboItem_Name.Where(m => m.Visible == true).Where(m => m.ComboID == ParentId).OrderBy(m => m.Name).ToList();
        }
        public List<ComboItem_Name> GetList(int ParentId, bool visible)
        {
            return db.ComboItem_Name.Where(m => m.Visible == visible).Where(m => m.ComboID == ParentId).OrderBy(m => m.Name).ToList();
        }
    }
}
