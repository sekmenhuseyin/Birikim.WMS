using System;
using System.Linq;
using Wms12m.Entity;
using Wms12m.Entity.Models;
using System.Collections.Generic;

namespace Wms12m.Business
{
    public class Dimension : abstractTables<TK_OLCU>
    {
        Result _Result;
        WMSEntities db = new WMSEntities();
        /// <summary>
        /// ekle-kaydet
        /// </summary>
        public override Result Operation(TK_OLCU tbl)
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
            using (DinamikModelContext Dinamik = new DinamikModelContext(tbl.SirketKod))
            {
                var tmp = Dinamik.Context.STKs.Where(m => m.MalKodu == tbl.MalKodu && (m.Birim1 == tbl.Birim || m.Birim2 == tbl.Birim || m.Birim3 == tbl.Birim || m.Birim4 == tbl.Birim)).FirstOrDefault();
                if (tmp == null)
                    kontrol = false;
            }
            var tmp2 = db.TK_OLCU.Where(m => m.SirketKod == tbl.SirketKod && m.MalKodu == tbl.MalKodu && m.Birim == tbl.Birim && m.ID != tbl.ID).FirstOrDefault();
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
                    db.TK_OLCU.Add(tbl);
                }
                else
                {
                    var tmp = Detail(tbl.ID);
                    tmp.MalKodu = tbl.MalKodu;
                    tmp.Birim = tbl.Birim;
                    tmp.En = tbl.En;
                    tmp.Boy = tbl.Boy;
                    tmp.Derinlik = tbl.Derinlik;
                    tmp.Agirlik = tbl.Agirlik;
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
                TK_OLCU tbl = db.TK_OLCU.Where(m => m.ID == Id).FirstOrDefault();
                if (tbl != null)
                {
                    db.TK_OLCU.Remove(tbl);
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

        public override TK_OLCU Detail(int Id)
        {
            try
            {
                return db.TK_OLCU.Where(m => m.ID == Id).FirstOrDefault();
            }
            catch (Exception)
            {
                return new TK_OLCU();
            }
        }
        /// <summary>
        /// tüm liste
        /// </summary>
        public override List<TK_OLCU> GetList()
        {
            return db.TK_OLCU.ToList();
        }
        /// <summary>
        /// şirkete göre lsite
        /// </summary>
        public List<TK_OLCU> GetList(string ParentId)
        {
            return db.TK_OLCU.Where(m=>m.SirketKod==ParentId).ToList();
        }
        public override List<TK_OLCU> GetList(int ParentId)
        {
            return GetList();
        }
    }
}
