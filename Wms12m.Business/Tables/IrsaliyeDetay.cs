using System;
using System.Collections.Generic;
using System.Linq;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m.Business
{
    public class IrsaliyeDetay : abstractTables<IRS_Detay>
    {
        /// <summary>
        /// silme işlemleri
        /// </summary>
        public override Result Delete(int Id)
        {
            _Result = new Result();
            try
            {
                var tbl = db.IRS_Detay.Where(m => m.ID == Id).FirstOrDefault();
                if (tbl != null)
                {
                    db.IRS_Detay.Remove(tbl);
                    db.SaveChanges();
                    LogActions("Business", "IrsaliyeDetay", "Delete", ComboItems.alSil, tbl.ID);
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
                Logger(ex, "Business/IrsaliyeDetay/Delete");
                _Result.Message = ex.Message;
                _Result.Status = false;
            }

            return _Result;
        }

        /// <summary>
        /// ayrıntı
        /// </summary>
        public override IRS_Detay Detail(int Id)
        {
            try
            {
                return db.IRS_Detay.Where(m => m.ID == Id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logger(ex, "Business/IrsaliyeDetay/Detail");
                return new IRS_Detay();
            }
        }

        /// <summary>
        /// liste
        /// </summary>
        public override List<IRS_Detay> GetList() => db.IRS_Detay.OrderBy(m => m.IrsaliyeID).ToList();

        /// <summary>
        /// liste
        /// </summary>
        public override List<IRS_Detay> GetList(int ParentId)
        {
            return db.IRS_Detay.Where(m => m.IrsaliyeID == ParentId).OrderByDescending(m => m.ID).ToList();
        }

        /// <summary>
        /// yeni ekle
        /// </summary>
        public Result Insert(frmMalzeme tbl, int DepoID)
        {
            _Result = new Result();
            if (tbl.Miktar <= 0)
            {
                _Result.Message = "Miktar hatalı";
                _Result.Status = false;
                _Result.Id = 0;
            }
            else
            {
                try
                {
                    var tablo = new IRS_Detay()
                    {
                        IrsaliyeID = tbl.IrsaliyeId,
                        MalKodu = tbl.MalKodu,
                        Birim = tbl.Birim,
                        Miktar = tbl.Miktar
                    };
                    if (tbl.MakaraNo != "" && tbl.MakaraNo != null)
                    {
                        var tmpx = db.Yers.Where(m => m.DepoID == DepoID && m.MakaraNo == tbl.MakaraNo).FirstOrDefault();
                        if (tmpx != null)
                        {
                            _Result.Message = "Bu makara no kullanılıyor";
                            return _Result;
                        }

                        tablo.MakaraNo = tbl.MakaraNo;
                    }

                    db.IRS_Detay.Add(tablo);
                    db.SaveChanges();
                    // log
                    LogActions("Business", "IrsaliyeDetay", "Operation", ComboItems.alEkle, tablo.ID, tbl.MalKodu + ", " + tbl.Miktar);
                    _Result.Message = "İşlem Başarılı !!!";
                    _Result.Status = true;
                    _Result.Id = tablo.ID;
                }
                catch (Exception ex)
                {
                    Logger(ex, "Business/IrsaliyeDetay/Insert");
                    _Result.Message = ex.Message;
                }
            }

            return _Result;
        }

        /// <summary>
        /// ekle/güncelle
        /// </summary>
        public override Result Operation(IRS_Detay tbl)
        {
            _Result = new Result(); var eklemi = false;
            if (tbl.Miktar <= 0)
            {
                _Result.Id = 0;
                _Result.Message = "Eksik Bilgi Girdiniz";
                _Result.Status = false;
                return _Result;
            }

            // set details
            if (tbl.ID == 0)
            {
                db.IRS_Detay.Add(tbl);
                eklemi = true;
            }
            else
            {
                var tmp = Detail(tbl.ID);
                tmp.IrsaliyeID = tbl.IrsaliyeID;
                tmp.MalKodu = tbl.MalKodu;
                tmp.Birim = tbl.Birim;
                tmp.Miktar = tbl.Miktar;
                if (tbl.MakaraNo != "") tmp.MakaraNo = tbl.MakaraNo;
                if (tbl.YerlestirmeMiktari != null) tmp.YerlestirmeMiktari = tbl.YerlestirmeMiktari;
            }

            try
            {
                db.SaveChanges();
                LogActions("Business", "IrsaliyeDetay", "Operation", eklemi == true ? ComboItems.alEkle : ComboItems.alDüzenle, tbl.ID, tbl.MalKodu + ", " + tbl.Miktar);
                // result
                _Result.Id = tbl.ID;
                _Result.Message = "İşlem Başarılı !!!";
                _Result.Status = true;
            }
            catch (Exception ex)
            {
                Logger(ex, "Business/IrsaliyeDetay/Operation");
                _Result.Id = 0;
                _Result.Message = "İşlem Hatalı: " + ex.Message;
                _Result.Status = false;
            }

            return _Result;
        }
    }
}