using System;
using System.Collections.Generic;
using System.Linq;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m.Business
{
    public class Dimension : abstractTables<Olcu>
    {
        /// <summary>
        /// ekle-kaydet
        /// </summary>
        public override Result Operation(Olcu tbl)
        {
            _Result = new Result(); bool eklemi = false;
            //kontrol
            if (tbl.MalKodu == "" || tbl.Birim == "" || (tbl.Boy == 0 && tbl.En == 0 && tbl.Derinlik == 0 && tbl.Agirlik == 0))
            {
                _Result.Message = "Eksik Bilgi Girdiniz";
                return _Result;
            }
            //var mı
            var tmp2 = db.Olcus.Where(m => m.MalKodu == tbl.MalKodu && m.Birim == tbl.Birim && m.ID != tbl.ID).FirstOrDefault();
            if (tmp2 != null)
            {
                _Result.Message = "Bu ölçü daha önce eklendi";
                return _Result;
            }
            //set details
            tbl.Degistiren = vUser.UserName;
            tbl.DegisTarih = DateTime.Today.ToOADateInt();
            if (tbl.ID == 0)
            {
                tbl.Kaydeden = vUser.UserName;
                tbl.KayitTarih = DateTime.Today.ToOADateInt();
                db.Olcus.Add(tbl);
                eklemi = true;
            }
            else
            {
                var tmp3 = Detail(tbl.ID);
                tmp3.MalKodu = tbl.MalKodu;
                tmp3.Birim = tbl.Birim;
                tmp3.En = tbl.En;
                tmp3.Boy = tbl.Boy;
                tmp3.Derinlik = tbl.Derinlik;
                tmp3.Agirlik = tbl.Agirlik;
            }
            //kaydet
            try
            {
                db.SaveChanges();
                LogActions("Business", "Dimension", "Operation", eklemi == true ? ComboItems.alEkle : ComboItems.alDüzenle, tbl.ID, tbl.MalKodu + ", H: " + tbl.En + "x" + tbl.Boy + "x" + tbl.Derinlik + ", A: " + tbl.Agirlik);
                //result
                _Result.Id = tbl.ID;
                _Result.Message = "İşlem Başarılı !!!";
                _Result.Status = true;
            }
            catch (Exception ex)
            {
                Logger(ex, "Business/Dimension/Operation");
                _Result.Message = "İşlem Hatalı: " + ex.Message;
            }
            return _Result;
        }
        /// <summary>
        /// sil
        /// </summary>
        public override Result Delete(int Id)
        {
            _Result = new Result();
            try
            {
                Olcu tbl = db.Olcus.Where(m => m.ID == Id).FirstOrDefault();
                if (tbl != null)
                {
                    db.Olcus.Remove(tbl);
                    db.SaveChanges();
                    LogActions("Business", "Dimension", "Delete", ComboItems.alSil, tbl.ID);
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
                Logger(ex, "Business/Dimension/Delete");
                _Result.Message = ex.Message;
            }
            return _Result;
        }
        /// <summary>
        /// ayrıntılar
        /// </summary>
        public override Olcu Detail(int Id)
        {
            try
            {
                return db.Olcus.Where(m => m.ID == Id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logger(ex, "Business/Dimension/Detail");
                return new Olcu();
            }
        }
        /// <summary>
        /// tüm liste
        /// </summary>
        public override List<Olcu> GetList()
        {
            return db.Olcus.OrderBy(m => m.MalKodu).Take(50).ToList();
        }
        public override List<Olcu> GetList(int ParentId)
        {
            return GetList();
        }
    }
}
