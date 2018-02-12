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
        /// depo silme
        /// </summary>
        public override Result Delete(int Id)
        {
            _Result = new Result();
            var tbl = db.Yers.Where(m => m.ID == Id).FirstOrDefault();
            if (tbl != null)
            {
                db.Yers.Remove(tbl);
                var logs = new Yer_Log()
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

        public Result Delete(int Id, int KullID, string IslemTipi, int? IrsID = null, int? IrsDetayID = null)
        {
            _Result = new Result();
            var tbl = db.Yers.Where(m => m.ID == Id).FirstOrDefault();
            if (tbl != null)
            {
                db.Yers.Remove(tbl);
                var logs = new Yer_Log()
                {
                    HucreAd = tbl.HucreAd,
                    MalKodu = tbl.MalKodu,
                    Birim = tbl.Birim,
                    Miktar = tbl.Miktar,
                    GC = true,
                    Kaydeden = db.Users.Where(m => m.ID == KullID).Select(m => m.Kod).FirstOrDefault(),
                    KayitTarihi = DateTime.Today.ToOADateInt(),
                    KayitSaati = DateTime.Now.ToOaTime(),
                    IslemTipi = IslemTipi,
                    IrsaliyeID = IrsID,
                    IRSDetayID = IrsDetayID
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
        public Yer Detail(int KatID, string MalKodu, string Birim, string MakaraNo = null)
        {
            var satir = db.Yers.Where(m => m.KatID == KatID && m.MalKodu == MalKodu && m.Birim == Birim);
            // makara no varsa onu da filtreye ekle
            if (MakaraNo != null && MakaraNo != "") satir = satir.Where(m => m.MakaraNo == MakaraNo);
            // return
            try
            {
                return satir.FirstOrDefault();
            }
            catch (Exception)
            {
                return new Yer();
            }
        }

        /// <summary>
        /// depo listesi
        /// </summary>
        public override List<Yer> GetList() => db.Yers.OrderBy(m => m.MalKodu).ToList();

        /// <summary>
        /// üst tabloya ait olanları getir
        /// </summary>
        public override List<Yer> GetList(int ParentId) => new List<Yer>();

        public List<frmStokYer> GetList(int DepoID, int RafID = 0, int BolumID = 0, int KatID = 0)
        {
            //sql
            var sql = string.Format(@"SELECT wms.Yer.ID, wms.Yer.HucreAd, wms.Yer.MalKodu, STK.MalAdi, wms.Yer.Miktar, wms.Yer.Birim, wms.Yer.MakaraNo, wms.Yer.MakaraDurum
                                    FROM            wms.Raf INNER JOIN
                                                                wms.Bolum ON wms.Raf.ID = wms.Bolum.RafID INNER JOIN
                                                                wms.Kat ON wms.Bolum.ID = wms.Kat.BolumID INNER JOIN
                                                                wms.Yer ON wms.Kat.ID = wms.Yer.KatID INNER JOIN
                                                                FINSAT6{0}.FINSAT6{0}.STK WITH (NOLOCK) ON wms.Yer.MalKodu = FINSAT6{0}.FINSAT6{0}.STK.MalKodu
                                    WHERE        wms.Yer.Miktar > 0", vUser.SirketKodu);
            //filters
            if (KatID > 0)
                sql += " AND wms.Kat.ID = " + KatID;
            else if (BolumID > 0)
                sql += " AND wms.Bolum.ID = " + BolumID;
            else if (RafID > 0)
                sql += " AND wms.Raf.ID = " + RafID;
            else if (DepoID > 0)
                sql += " AND wms.Yer.DepoID = " + DepoID;
            //return
            return db.Database.SqlQuery<frmStokYer>(sql).ToList();
        }

        public List<Yer> GetList(int DepoID, string MalKodu)
        {
            return db.Yers.Where(m => m.DepoID == DepoID && m.MalKodu == MalKodu && m.Miktar > 0).OrderBy(m => m.HucreAd).ToList();
        }

        public List<frmCableStok> GetListCable(int DepoID, int RafID = 0, int BolumID = 0, int KatID = 0)
        {
            //sql
            var sql = string.Format(@"SELECT        wms.Yer.ID, wms.Yer.HucreAd, wms.Yer.MalKodu, STK.MalAdi, wms.Yer.Miktar, wms.Yer.Birim, wms.Yer.MakaraNo, STK.MalAdi4 AS Marka,
                                                             STK.Nesne2 AS Cins, STK.Kod15 AS Kesit
                                    FROM            wms.Yer WITH (NOLOCK) INNER JOIN
                                                             wms.Kat WITH (NOLOCK) ON wms.Yer.KatID = wms.Kat.ID INNER JOIN
                                                             wms.Bolum WITH (NOLOCK) ON wms.Kat.BolumID = wms.Bolum.ID INNER JOIN
                                                             wms.Raf WITH (NOLOCK) ON wms.Bolum.RafID = wms.Raf.ID INNER JOIN
                                                             wms.Koridor WITH (NOLOCK) ON wms.Raf.KoridorID = wms.Koridor.ID INNER JOIN
                                                             FINSAT6{0}.FINSAT6{0}.STK WITH (NOLOCK) ON wms.Yer.MalKodu = FINSAT6{0}.FINSAT6{0}.STK.MalKodu
                                    WHERE        (STK.Kod1 = 'KKABLO') AND (wms.Yer.Miktar > 0)", vUser.SirketKodu);
            //filters
            if (KatID > 0)
                sql += " AND wms.Kat.ID = " + KatID;
            else if (BolumID > 0)
                sql += " AND wms.Bolum.ID = " + BolumID;
            else if (RafID > 0)
                sql += " AND wms.Raf.ID = " + RafID;
            else if (DepoID > 0)
                sql += " AND wms.Yer.DepoID = " + DepoID;
            //return
            return db.Database.SqlQuery<frmCableStok>(sql).ToList();
        }

        public List<frmRezerveStok> GetListRezerve(string MalKodu, int DepoID, string DepoKodu)
        {
            var sql = string.Format(@"
                        SELECT        wms.Gorev.GorevNo, wms.Gorev.OlusturmaTarihi, wms.Gorev.Bilgi, wms.IRS.SirketKod, wms.IRS.EvrakNo, wms.IRS.HesapKodu, wms.IRS_Detay.MalKodu, wms.IRS_Detay.Miktar, wms.IRS_Detay.Birim,
                                                 wms.IRS_Detay.MakaraNo, wms.IRS_Detay.KynkSiparisID, wms.IRS_Detay.KynkSiparisNo, wms.IRS_Detay.KynkSiparisSiraNo, wms.IRS_Detay.KynkSiparisTarih, wms.IRS_Detay.KynkSiparisMiktar, wms.Depo.DepoKodu,
                                                 STK.MalAdi, CHK.Unvan1 as Unvan
                        FROM            wms.IRS INNER JOIN
                                                 wms.IRS_Detay ON wms.IRS.ID = wms.IRS_Detay.IrsaliyeID INNER JOIN
                                                 wms.GorevIRS ON wms.IRS.ID = wms.GorevIRS.IrsaliyeID INNER JOIN
                                                 wms.Gorev ON wms.GorevIRS.GorevID = wms.Gorev.ID INNER JOIN
                                                 wms.Depo ON wms.Gorev.DepoID = wms.Depo.ID INNER JOIN
                                                 FINSAT6{0}.FINSAT6{0}.STK WITH (NOLOCK) ON wms.IRS_Detay.MalKodu = FINSAT6{0}.FINSAT6{0}.STK.MalKodu INNER JOIN
                                                 FINSAT6{0}.FINSAT6{0}.CHK WITH (NOLOCK) ON wms.IRS.HesapKodu = FINSAT6{0}.FINSAT6{0}.CHK.HesapKodu
                        WHERE        (wms.Gorev.GorevTipiID = 3) AND (wms.Gorev.DurumID = 9) AND (wms.IRS_Detay.MalKodu = '{1}')", vUser.SirketKodu, MalKodu);
            if (DepoID > 0)
                sql += " AND wms.Depo.ID = " + DepoID;
            else if (DepoKodu != "")
                sql += " AND wms.Depo.DepoKodu = '" + DepoKodu + "'";
            return db.Database.SqlQuery<frmRezerveStok>(sql).ToList();
        }

        /// <summary>
        /// stok ekleme
        /// </summary>
        public Result Insert(Yer tbl, int kullID, string islemTipi, int? irsID = null, int? irsDetayID = null)
        {
            _Result = new Result();
            // stok
            if (tbl.MakaraNo == "") tbl.MakaraNo = null;
            db.Yers.Add(tbl);
            // log
            var yerLog = new Yer_Log()
            {
                KatID = tbl.KatID,
                MalKodu = tbl.MalKodu,
                Birim = tbl.Birim,
                Miktar = tbl.Miktar,
                GC = false,//false=girdi(+), true=çıktı(-)
                KayitTarihi = DateTime.Today.ToOADate().ToInt32(),
                KayitSaati = DateTime.Now.ToOaTime(),
                Kaydeden = db.Users.Where(m => m.ID == kullID).Select(m => m.Kod).FirstOrDefault(),
                IslemTipi = islemTipi,
                IrsaliyeID = irsID,
                IRSDetayID = irsDetayID
            };
            if (tbl.MakaraNo != "" && tbl.MakaraNo != null) yerLog.MakaraNo = tbl.MakaraNo;
            if (irsID > 0) yerLog.IrsaliyeID = irsID;
            db.Yer_Log.Add(yerLog);
            // save
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

            // exit
            return _Result;
        }

        /// <summary>
        /// burada yok
        /// </summary>
        public override Result Operation(Yer tbl) => new Result();

        /// <summary>
        /// stok çıkarma
        /// </summary>
        public Result Remove(Yer tbl, int KullID, string IslemTipi, int? IrsID = null, int? IrsDetayID = null)
        {
            _Result = new Result();
            var tmp = Detail(tbl.ID);
            if (tmp.Miktar == tbl.Miktar) db.Yers.Remove(tbl);
            else if (tmp.Miktar < tbl.Miktar)
            {
                _Result.Message = "Hatalı Kayıt !";
                _Result.Status = false;
                return _Result;
            }
            else
                tmp.Miktar -= tbl.Miktar;
            // makara
            if (tmp.Miktar == 0)
                tmp.MakaraNo = null;
            tmp.MakaraDurum = false;
            // log
            var logs = new Yer_Log()
            {
                HucreAd = tbl.HucreAd,
                MalKodu = tbl.MalKodu,
                Birim = tbl.Birim,
                Miktar = tbl.Miktar,
                GC = true,
                Kaydeden = db.Users.Where(m => m.ID == KullID).Select(m => m.Kod).FirstOrDefault(),
                KayitTarihi = DateTime.Today.ToOADateInt(),
                KayitSaati = DateTime.Now.ToOaTime(),
                IslemTipi = IslemTipi,
                IrsaliyeID = IrsID,
                IRSDetayID = IrsDetayID
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
        /// stok güncelleme
        /// </summary>
        public Result Update(Yer tbl, int KullID, string IslemTipi, decimal miktar, bool gc, int? IrsID = null, int? IrsDetayID = null)
        {
            _Result = new Result();
            // log
            var yerLog = new Yer_Log()
            {
                KatID = tbl.KatID,
                MalKodu = tbl.MalKodu,
                Birim = tbl.Birim,
                Miktar = miktar,
                GC = gc,//false=girdi(+), true=çıktı(-)
                KayitTarihi = DateTime.Today.ToOADate().ToInt32(),
                KayitSaati = DateTime.Now.ToOaTime(),
                Kaydeden = db.Users.Where(m => m.ID == KullID).Select(m => m.Kod).FirstOrDefault(),
                IslemTipi = IslemTipi,
                IrsaliyeID = IrsID,
                IRSDetayID = IrsDetayID
            };
            db.Yer_Log.Add(yerLog);
            if (gc == true) tbl.MakaraDurum = false;
            // stok
            var log = Detail(tbl.ID);
            log.Miktar = tbl.Miktar;
            if (log.Miktar == 0)
            {
                log.MakaraNo = null;
                log.MakaraDurum = false;
            }

            // save
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

            // exit
            return _Result;
        }
    }
}