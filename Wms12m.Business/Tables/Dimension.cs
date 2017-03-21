﻿using System;
using System.Collections.Generic;
using System.Linq;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m.Business
{
    public class Dimension : abstractTables<Olcu>, IDisposable
    {
        Result _Result;
        WMSEntities db = new WMSEntities();
        /// <summary>
        /// ekle-kaydet
        /// </summary>
        public override Result Operation(Olcu tbl)
        {
            _Result = new Result();
            if (tbl.MalKodu == "" || tbl.Birim == "" || (tbl.Boy == 0 && tbl.En == 0 && tbl.Derinlik == 0 && tbl.Agirlik == 0))
            {
                _Result.Id = 0;
                _Result.Message = "Eksik Bilgi Girdiniz";
                _Result.Status = false;
                return _Result;
            }
            bool kontrol = true;
            var tmp = db.Database.SqlQuery<string>("SELECT MalKodu FROM FINSAT6{0}.FINSAT6{0}.STK WHERE (MalKodu = '{1}') AND ((Birim1 = '{2}') OR (Birim2 = '{2}') OR (Birim3 = '{2}') OR (Birim4 = '{2}'))", tbl.SirketKod, tbl.MalKodu, tbl.Birim);
            if (tmp.ToString2() == "")
                kontrol = false;
            var tmp2 = db.Olcus.Where(m => m.SirketKod == tbl.SirketKod && m.MalKodu == tbl.MalKodu && m.Birim == tbl.Birim && m.ID != tbl.ID).FirstOrDefault();
            if (tmp2 != null)
                kontrol = false;
            if (kontrol == false)
            {
                _Result.Id = 0;
                _Result.Message = "Bu ölçü daha önce eklendi";
                _Result.Status = false;
                return _Result;
            }
            try
            {
                if (tbl.ID == 0)
                {
                    db.Olcus.Add(tbl);
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
        /// ayrıntılar
        /// </summary>

        public override Olcu Detail(int Id)
        {
            try
            {
                return db.Olcus.Where(m => m.ID == Id).FirstOrDefault();
            }
            catch (Exception)
            {
                return new Olcu();
            }
        }
        /// <summary>
        /// tüm liste
        /// </summary>
        public override List<Olcu> GetList()
        {
            return db.Olcus.OrderBy(m => m.MalKodu).ToList();
        }
        /// <summary>
        /// şirkete göre lsite
        /// </summary>
        public List<Olcu> GetList(string ParentId)
        {
            return db.Olcus.Where(m => m.SirketKod == ParentId).OrderBy(m => m.MalKodu).ToList();
        }
        public override List<Olcu> GetList(int ParentId)
        {
            return GetList();
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
