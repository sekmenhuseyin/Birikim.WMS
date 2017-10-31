using System;
using System.Collections.Generic;
using System.Linq;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m.Business
{
    public class Yerlestirme : abstractTables<Yer>
    {
        /// <summary>
        /// stok ekleme
        /// </summary>
        public Result Insert(Yer tbl, int IrsID, int KullID)
        {
            _Result = new Result(); 
            //stok
            if (tbl.MakaraNo == "") tbl.MakaraNo = null;
            db.Yers.Add(tbl);
            //log
            Yer_Log yerLog = new Yer_Log()
            {
                KatID = tbl.KatID,
                MalKodu = tbl.MalKodu,
                Birim = tbl.Birim,
                Miktar = tbl.Miktar,
                GC = false,//false=girdi(+), true=çıktı(-)
                KayitTarihi = DateTime.Today.ToOADate().ToInt32(),
                KayitSaati = DateTime.Now.ToOaTime(),
                Kaydeden = db.Users.Where(m => m.ID == KullID).Select(m => m.Kod).FirstOrDefault()
            };
            if (tbl.MakaraNo != "" && tbl.MakaraNo != null) yerLog.MakaraNo = tbl.MakaraNo;
            if (IrsID > 0) yerLog.IrsaliyeID = IrsID;
            db.Yer_Log.Add(yerLog);
            //save
            try
            {
                db.SaveChanges();
                LogActions("Business", "Yerlestirme", "Insert", ComboItems.alEkle, tbl.ID, "KatID: " + tbl.KatID + ", MalKodu" + tbl.MalKodu + ", Miktar" + tbl.Miktar);
                _Result.Status = true;
                _Result.Message = "İşlem Başarılı !!!";
                _Result.Id = tbl.ID;
            }
            catch (Exception ex)
            {
                Logger(ex, "Business/Yerlestirme/Insert");
                _Result.Id = 0;
                _Result.Message = "İşlem Hatalı: " + ex.Message;
                _Result.Status = false;
            }
            //exit
            return _Result;
        }
        /// <summary>
        /// stok güncelleme
        /// </summary>
        public Result Update(Yer tbl, int IrsID, int KullID, bool gc, decimal miktar)
        {
            _Result = new Result();
            //log
            Yer_Log yerLog = new Yer_Log()
            {
                KatID = tbl.KatID,
                MalKodu = tbl.MalKodu,
                Birim = tbl.Birim,
                Miktar = miktar,
                GC = gc,//false=girdi(+), true=çıktı(-)
                KayitTarihi = DateTime.Today.ToOADate().ToInt32(),
                KayitSaati = DateTime.Now.ToOaTime(),
                Kaydeden = db.Users.Where(m => m.ID == KullID).Select(m => m.Kod).FirstOrDefault()
            };
            if (IrsID > 0) yerLog.IrsaliyeID = IrsID;
            db.Yer_Log.Add(yerLog);
            if (gc == true)
                tbl.MakaraDurum = false;
            //stok
            var log = Detail(tbl.ID);
            log.Miktar = tbl.Miktar;
            if (log.Miktar == 0)
            {
                log.MakaraNo = null;
                log.MakaraDurum = false;
            }
            //save
            try
            {
                db.SaveChanges();
                LogActions("Business", "Yerlestirme", "Update", ComboItems.alDüzenle, tbl.ID, "KatID: " + tbl.KatID + ", MalKodu" + tbl.MalKodu + ", Miktar" + tbl.Miktar);
                _Result.Status = true;
                _Result.Message = "İşlem Başarılı !!!";
                _Result.Id = tbl.ID;
            }
            catch (Exception ex)
            {
                Logger(ex, "Business/Yerlestirme/Update");
                _Result.Id = 0;
                _Result.Message = "İşlem Hatalı: " + ex.Message;
                _Result.Status = false;
            }
            //exit
            return _Result;
        }
        /// <summary>
        /// stok çıkarma
        /// </summary>
        public Result Remove(Yer tbl, int IrsID, int KullID)
        {
            _Result = new Result();
            var tmp = Detail(tbl.ID);
            if (tmp.Miktar == tbl.Miktar)
                db.Yers.Remove(tbl);
            else if (tmp.Miktar < tbl.Miktar)
            {
                _Result.Message = "Hatalı Kayıt !";
                _Result.Status = false;
                return _Result;
            }
            else
                tmp.Miktar -= tbl.Miktar;
            //makara
            if (tmp.Miktar == 0)
                tmp.MakaraNo = null;
            tmp.MakaraDurum = false;
            //log
            Yer_Log logs = new Yer_Log()
            {
                HucreAd = tbl.HucreAd,
                MalKodu = tbl.MalKodu,
                Birim = tbl.Birim,
                Miktar = tbl.Miktar,
                GC = true,
                Kaydeden = db.Users.Where(m => m.ID == KullID).Select(m => m.Kod).FirstOrDefault(),
                KayitTarihi = DateTime.Today.ToOADateInt(),
                KayitSaati = DateTime.Now.ToOaTime()
            };
            db.Yer_Log.Add(logs);
            try
            {
                db.SaveChanges();
                LogActions("Business", "Yerlestirme", "Update", ComboItems.alDüzenle, tbl.ID, "KatID: " + tbl.KatID + ", MalKodu" + tbl.MalKodu + ", Miktar" + tbl.Miktar);
                _Result.Status = true;
                _Result.Message = "İşlem Başarılı !!!";
                _Result.Id = tbl.ID;
            }
            catch (Exception ex)
            {
                Logger(ex, "Business/Yerlestirme/Remove");
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
            Yer tbl = db.Yers.Where(m => m.ID == Id).FirstOrDefault();
            if (tbl != null)
            {
                db.Yers.Remove(tbl);
                Yer_Log logs = new Yer_Log()
                {
                    KatID = tbl.KatID,
                    MalKodu = tbl.MalKodu,
                    Birim = tbl.Birim,
                    Miktar = tbl.Miktar,
                    GC = true,
                    Kaydeden = vUser.UserName,
                    KayitTarihi = DateTime.Today.ToOADateInt(),
                    KayitSaati = DateTime.Now.ToOaTime()
                };
                db.Yer_Log.Add(logs);
            }
            else
            {
                _Result.Message = "Kayıt Yok";
            }
            try
            {
                db.SaveChanges();
                LogActions("Business", "Yerlestirme", "Delete", ComboItems.alSil, tbl.ID);
                _Result.Id = Id;
                _Result.Message = "İşlem Başarılı !!!";
                _Result.Status = true;
            }
            catch (Exception ex)
            {
                Logger(ex, "Business/Yerlestirme/Delete");
                _Result.Message = ex.Message;
            }
            return _Result;
        }
        public Result Delete(int Id, int KullID)
        {
            _Result = new Result();
            Yer tbl = db.Yers.Where(m => m.ID == Id).FirstOrDefault();
            if (tbl != null)
            {
                db.Yers.Remove(tbl);
                Yer_Log logs = new Yer_Log()
                {
                    HucreAd = tbl.HucreAd,
                    MalKodu = tbl.MalKodu,
                    Birim = tbl.Birim,
                    Miktar = tbl.Miktar,
                    GC = true,
                    Kaydeden = db.Users.Where(m => m.ID == KullID).Select(m => m.Kod).FirstOrDefault(),
                    KayitTarihi = DateTime.Today.ToOADateInt(),
                    KayitSaati = DateTime.Now.ToOaTime()
                };
                db.Yer_Log.Add(logs);
            }
            else
            {
                _Result.Message = "Kayıt Yok";
                _Result.Status = false;
            }
            try
            {
                db.SaveChanges();
                LogActions("Business", "Yerlestirme", "Delete", ComboItems.alSil, tbl.ID);
                _Result.Id = Id;
                _Result.Message = "İşlem Başarılı !!!";
                _Result.Status = true;
            }
            catch (Exception ex)
            {
                Logger(ex, "Business/Yerlestirme/Delete");
                _Result.Message = ex.Message;
                _Result.Status = false;
            }
            return _Result;
        }
        /// <summary>
        /// depo bilgileri
        /// </summary>
        public override Yer Detail(int Id)
        {
            try
            {
                return db.Yers.Where(m => m.ID == Id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logger(ex, "Business/Yerlestirme/Detail");
                return new Yer();
            }
        }
        /// <summary>
        /// aynısından daha önce yüklenmiş mi kontrol eder
        /// </summary>
        public Yer Detail(int KatID, string MalKodu, string Birim)
        {
            try
            {
                return db.Yers.Where(m => m.KatID == KatID && m.MalKodu == MalKodu && m.Birim == Birim).FirstOrDefault();
            }
            catch (Exception)
            {
                return new Yer();
            }
        }
        /// <summary>
        /// depo listesi
        /// </summary>
        public override List<Yer> GetList()
        {
            return db.Yers.OrderBy(m => m.MalKodu).ToList();
        }
        /// <summary>
        /// üst tabloya ait olanları getir
        /// </summary>
        public override List<Yer> GetList(int ParentId)
        {
            return db.Yers.Where(m => m.KatID == ParentId && m.Miktar > 0).OrderBy(m => m.MalKodu).ToList();
        }
        public List<Yer> GetListFromDepo(int ParentId)
        {
            return db.Yers.Where(m => m.DepoID == ParentId && m.Miktar > 0).OrderBy(m => m.MalKodu).ToList();
        }
        public List<Yer> GetListFromKoridor(int ParentId)
        {
            return db.Yers.Where(m => m.Kat.Bolum.Raf.KoridorID == ParentId && m.Miktar > 0).OrderBy(m => m.MalKodu).ToList();
        }
        public List<Yer> GetListFromRaf(int ParentId)
        {
            return db.Yers.Where(m => m.Kat.Bolum.RafID == ParentId && m.Miktar > 0).OrderBy(m => m.MalKodu).ToList();
        }
        public List<Yer> GetMalListFromDepo(int ParentId, string MalKodu)
        {
            return db.Yers.Where(m => m.DepoID == ParentId && m.MalKodu == MalKodu && m.Miktar > 0).OrderBy(m => m.HucreAd).ToList();
        }
        /// <summary>
        /// burada yok
        /// </summary>
        public override Result Operation(Yer tbl)
        {
            return new Result();
        }
    }
}
