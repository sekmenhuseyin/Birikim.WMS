using System;
using System.Linq;
using Wms12m.Entity;
using Wms12m.Entity.Models;
using System.Collections.Generic;

namespace Wms12m.Business
{
    public class Shelf : abstractTables<TK_RAF>
    {
        Result _Result;
        WMSEntities db = new WMSEntities();
        /// <summary>
        /// ekle ve güncelle
        /// </summary>
        public override Result Operation(TK_RAF tbl)
        {
            _Result = new Result();
            if (tbl.Raf == "")
            {
                _Result.Id = 0;
                _Result.Message = "İşlem Hatalı !!!";
                _Result.Status = false;
            }
            else
            {
                var kontrol = db.TK_RAF.Where(m => m.Raf == tbl.Raf && m.KoridorID == tbl.KoridorID && m.ID != tbl.ID).FirstOrDefault();
                if (kontrol != null)
                {
                    _Result.Id = 0;
                    _Result.Message = "Bu isim kullanılıyor";
                    _Result.Status = false;
                    return _Result;
                }
                try
                {
                    tbl.Degistiren = SiteSessions.LoggedUserName;
                    tbl.DegisTarih = DateTime.Today.ToOADateInt();
                    if (tbl.ID == 0)
                    {
                        tbl.Kaydeden = SiteSessions.LoggedUserName;
                        tbl.KayitTarih = DateTime.Today.ToOADateInt();
                        db.TK_RAF.Add(tbl);
                    }
                    else
                    {
                        var tmp = Detail(tbl.ID);
                        tmp.Raf = tbl.Raf;
                        tmp.KoridorID = tbl.KoridorID;
                        tmp.SiraNo = tbl.SiraNo;
                        tmp.Aktif = tbl.Aktif;
                        tmp.Degistiren = tbl.Degistiren;
                        tmp.DegisTarih = tbl.DegisTarih;
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
                TK_RAF tbl = db.TK_RAF.Where(m => m.ID == Id).FirstOrDefault();
                if (tbl != null)
                {
                    db.TK_RAF.Remove(tbl);
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
        /// bir tanesinin ayrıntıları
        /// </summary>
        public override TK_RAF Detail(int Id)
        {
            try
            {
                return db.TK_RAF.Where(m => m.ID == Id).FirstOrDefault();
            }
            catch (Exception)
            {
                return new TK_RAF();
            }

        }
        /// <summary>
        /// tüm listesi
        /// </summary>
        public override List<TK_RAF> GetList()
        {
            return db.TK_RAF.ToList();
        }
        /// <summary>
        /// üst tabloya ait olanları getir
        /// </summary>
        public override List<TK_RAF> GetList(int ParentId)
        {
            return db.TK_RAF.Where(m=>m.KoridorID==ParentId).ToList();
        }
    }
}
