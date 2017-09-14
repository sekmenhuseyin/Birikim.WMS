using System;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m.Presentation.Areas.YN.Controllers
{
    public class OnayBekleyenlerController : RootController
    {
        #region Sipariş
        /// <summary>
        /// sipariş onayı bekleyenler sayfası
        /// </summary>
        public ActionResult Siparis()
        {
            return View("Siparis");
        }
        /// <summary>
        /// sipariş onayı bekleyenler listesi
        /// </summary>
        public string Siparis_List()
        {
            using (YNSEntities dby = new YNSEntities())
            {
                var list = dby.Database.SqlQuery<frmOnaySiparisList>(string.Format(@"SELECT        YNS{0}.STK002.STK002_EvrakSeriNo AS EvrakSeriNo, YNS{0}.STK002.STK002_CariHesapKodu AS HesapKodu, YNS{0}.CAR002.CAR002_Unvan1 AS Unvan, COUNT(YNS{0}.STK002.STK002_MalKodu) 
                                                                                             AS Cesit, SUM(YNS{0}.STK002.STK002_Miktari) AS Miktar, YNS{0}.STK002.STK002_GirenKodu AS Kaydeden, CONVERT(VARCHAR(15), CAST(YNS{0}.STK002.STK002_GirenTarih - 2 AS datetime), 104) 
                                                                                             AS Tarih
                                                                    FROM            YNS{0}.STK002 INNER JOIN
                                                                                             YNS{0}.CAR002 ON YNS{0}.STK002.STK002_CariHesapKodu = YNS{0}.CAR002.CAR002_HesapKodu
                                                                    WHERE        (YNS{0}.STK002.STK002_GC = 1) AND (YNS{0}.STK002.STK002_SipDurumu = 0)
                                                                    GROUP BY YNS{0}.STK002.STK002_CariHesapKodu, YNS{0}.CAR002.CAR002_Unvan1, YNS{0}.STK002.STK002_EvrakSeriNo, YNS{0}.STK002.STK002_GirenKodu, CONVERT(VARCHAR(15), 
                                                                                             CAST(YNS{0}.STK002.STK002_GirenTarih - 2 AS datetime), 104)", "0TEST")).ToList();
                var json = new JavaScriptSerializer().Serialize(list);
                return json;
            }
        }
        /// <summary>
        /// liste detayı
        /// </summary>
        [HttpPost]
        public PartialViewResult Siparis_Details(string ID)
        {
            using (YNSEntities dby = new YNSEntities())
            {
                var list = dby.Database.SqlQuery<frmOnaySiparisList>(string.Format(@"SELECT        YNS{0}.STK002.STK002_MalKodu AS MalKodu, YNS{0}.STK004.STK004_Aciklama AS MalAdi, YNS{0}.STK002.STK002_CariHesapKodu AS HesapKodu, YNS{0}.CAR002.CAR002_Unvan1 AS Unvan, 
                                                                                            YNS{0}.STK002.STK002_EvrakSeriNo AS EvrakSeriNo, YNS{0}.STK002.STK002_Depo AS Depo, YNS{0}.STK002.STK002_Miktari AS Miktar, YNS{0}.STK002.STK002_BirimFiyati AS BirimFiyat, 
                                                                                            YNS{0}.STK002.STK002_Tutari AS Tutar, YNS{0}.STK002.STK002_DovizCinsi AS DovizCinsi, YNS{0}.STK002.STK002_GirenKodu AS Kaydeden, CONVERT(VARCHAR(15), CAST(YNS{0}.STK002.STK002_GirenTarih - 2 AS datetime), 104) AS Tarih
                                                                FROM            YNS{0}.STK002 INNER JOIN
                                                                                            YNS{0}.CAR002 ON YNS{0}.STK002.STK002_CariHesapKodu = YNS{0}.CAR002.CAR002_HesapKodu INNER JOIN
                                                                                            YNS{0}.STK004 ON YNS{0}.STK002.STK002_MalKodu = YNS{0}.STK004.STK004_MalKodu
                                                                WHERE        (YNS{0}.STK002.STK002_GC = 1) AND (YNS{0}.STK002.STK002_SipDurumu = 0) AND YNS{0}.STK002.STK002_EvrakSeriNo = '{1}'", "0TEST", ID)).ToList();
                return PartialView("Siparis_Details", list);
            }
        }
        /// <summary>
        /// onayla veya reddet
        /// </summary>
        [HttpPost]
        public JsonResult Siparis_Onay(string ID, bool Onay)
        {
            try
            {
                using (YNSEntities dby = new YNSEntities())
                {
                }
                return Json(new Result(true), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger(ex, "YN/OnayBekleyenler/Siparis_Onay");
                return Json(new Result(false, "Hata Oluştu"), JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
        #region Teklif
        /// <summary>
        /// teklif onay lsayfası
        /// </summary>
        public ActionResult Teklif()
        {
            return View("Teklif");
        }
        /// <summary>
        /// teklif onay listesi
        /// </summary>
        public string Teklif_List()
        {
            using (YNSEntities dby = new YNSEntities())
            {
                var list = dby.Database.SqlQuery<frmOnayTeklifList>(string.Format(@"SELECT TeklifNo,HesapKodu,COUNT(MalKodu) AS Cesit,
                                                                                    SUM(Miktar) AS Miktar,Kaydeden,
                                                                                    CONVERT(VARCHAR(15), CAST(KayitTarih - 2 AS datetime), 104) AS KayitTarihi,
                                                                                    CONVERT(VARCHAR(15), CAST(TeklifTarihi - 2 AS datetime), 104) AS TeklifTarihi
                                                                                      FROM [YNS{0}].[YNS{0}].[Teklif]
                                                                                      GROUP BY TeklifNo,TeklifTarihi,HesapKodu,KayitTarih,Kaydeden", "0TEST")).ToList();
                var json = new JavaScriptSerializer().Serialize(list);
                return json;
            }
        }
        /// <summary>
        /// teklif detayları
        /// </summary>
        [HttpPost]
        public PartialViewResult Teklif_Details(string ID)
        {
            using (YNSEntities dby = new YNSEntities())
            {
                var list = dby.Database.SqlQuery<frmOnayTeklifListDetay>(string.Format(@"SELECT TeklifNo,HesapKodu,
                                                                                YNS{0}.CAR002.CAR002_Unvan1 AS Unvan,
                                                                                Miktar AS Miktar,Kaydeden,MalKodu,STK004_Aciklama AS MalAdi,
                                                                                Fiyat,Tutar,DovizCinsi,
                                                                                CONVERT(VARCHAR(15), CAST(KayitTarih - 2 AS datetime), 104) AS KayitTarihi
                                                                                  FROM [YNS{0}].[YNS{0}].[Teklif] 
                                                                                  INNER JOIN
                                                                                  YNS{0}.CAR002 ON HesapKodu = YNS{0}.CAR002.CAR002_HesapKodu
                                                                                  INNER JOIN
                                                                                  YNS{0}.STK004 ON MalKodu = YNS{0}.STK004.STK004_MalKodu WHERE TeklifNo='{1}'", "0TEST", ID)).ToList();
                return PartialView("Teklif_Details", list);
            }
        }
        /// <summary>
        /// teklif onay
        /// </summary>
        [HttpPost]
        public JsonResult Teklif_Onay(string ID, bool Onay)
        {
            try
            {
                using (YNSEntities dby = new YNSEntities())
                    dby.Database.ExecuteSqlCommand(string.Format("UPDATE [YNS{0}].[YNS{0}].[Teklif] SET OnayDurumu = {1} WHERE TeklifNo='{2}'", "0TEST", Onay == true ? 1 : 2, ID));
                return Json(new Result(true, 1), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger(ex, "YN/OnayBekleyenler/Teklif_Onay");
                return Json(new Result(false, "Hata Oluştu"), JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
        #region Transfer
        /// <summary>
        /// transfer onayı bekleyenler sayfası
        /// </summary>
        public ActionResult Transfer()
        {
            return View("Transfer");
        }
        /// <summary>
        /// transfer onayı bekleyenler listesi
        /// </summary>
        public string Transfer_List()
        {
            using (YNSEntities dby = new YNSEntities())
            {
                var list = dby.Database.SqlQuery<frmOnayTransferList>(string.Format(@"SELECT        TransferNo, Kaydeden, CONVERT(VARCHAR(15), CAST(TransferTarihi - 2 AS datetime), 104) AS Tarih
                                                                    FROM            YNS{0}.TransferDepo
                                                                    WHERE        (OnayDurumu = 0)
                                                                    GROUP BY TransferNo, Kaydeden, CONVERT(VARCHAR(15), CAST(TransferTarihi - 2 AS datetime), 104)", "0TEST")).ToList();
                var json = new JavaScriptSerializer().Serialize(list);
                return json;
            }
        }
        /// <summary>
        /// liste detayı
        /// </summary>
        [HttpPost]
        public PartialViewResult Transfer_Details(string ID)
        {
            using (YNSEntities dby = new YNSEntities())
            {
                var list = dby.Database.SqlQuery<frmOnayTransferList>(string.Format(@"SELECT        YNS{0}.TransferDepo.ID, YNS{0}.TransferDepo.TransferNo, YNS{0}.TransferDepo.TransferTarihi, YNS{0}.TransferDepo.SiraNo, YNS{0}.TransferDepo.MalKodu, YNS{0}.STK004.STK004_Aciklama AS MalAdi,
                                                                                                  YNS{0}.TransferDepo.Miktar, YNS{0}.TransferDepo.Birim, YNS{0}.TransferDepo.CikisDepo, YNS{0}.TransferDepo.GirisDepo, YNS{0}.TransferDepo.TalepEden, YNS{0}.TransferDepo.OnayDurumu, 
                                                                                                 YNS{0}.TransferDepo.Kaydeden, YNS{0}.TransferDepo.KayitTarih
                                                                        FROM            YNS{0}.TransferDepo INNER JOIN
                                                                                                 YNS{0}.STK004 ON YNS{0}.TransferDepo.MalKodu = YNS{0}.STK004.STK004_MalKodu
                                                                        WHERE        YNS{0}.TransferDepo.TransferNo = '{1}'", "0TEST", ID)).ToList();
                return PartialView("Transfer_Details", list);
            }
        }
        /// <summary>
        /// onayla veya reddet
        /// </summary>
        [HttpPost]
        public JsonResult Transfer_Onay(string ID, bool Onay)
        {
            try
            {
                using (YNSEntities dby = new YNSEntities())
                    dby.Database.ExecuteSqlCommand(string.Format(@"UPDATE YNS{0}.TransferDepo SET OnayDurumu = {1} WHERE (TransferNo = '{2}')", "0TEST", Onay == true ? 1 : 2, ID));
                return Json(new Result(true, 1), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger(ex, "YN/OnayBekleyenler/Transfer_Onay");
                return Json(new Result(false, "Hata Oluştu"), JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
        #region Fatura
        /// <summary>
        /// Fatura onayı bekleyenler sayfası
        /// </summary>
        public ActionResult Fatura()
        {
            return View("Fatura");
        }
        /// <summary>
        /// Fatura onayı bekleyenler listesi
        /// </summary>
        public string Fatura_List()
        {
            using (YNSEntities dby = new YNSEntities())
            {
                var list = dby.Database.SqlQuery<frmOnayFatura>(string.Format(@"SELECT        YNS{0}.TempFatura.EvrakNo, YNS{0}.TempFatura.HesapKodu, YNS{0}.TempFatura.Depo, COUNT(YNS{0}.TempFatura.ID) AS Cesit, 
                                                                                                 SUM(YNS{0}.TempFatura.Miktar) AS Miktar, YNS{0}.TempFatura.Kaydeden, YNS{0}.TempFatura.KayitTarih, YNS{0}.CAR002.CAR002_Unvan1 AS Unvan
                                                                        FROM            YNS{0}.TempFatura INNER JOIN
                                                                                                 YNS{0}.CAR002 ON YNS{0}.TempFatura.HesapKodu = YNS{0}.CAR002.CAR002_HesapKodu
                                                                        WHERE        (YNS{0}.TempFatura.IslemDurumu = 0)
                                                                        GROUP BY YNS{0}.TempFatura.EvrakNo, YNS{0}.TempFatura.HesapKodu, YNS{0}.TempFatura.Depo, YNS{0}.TempFatura.Kaydeden, YNS{0}.TempFatura.KayitTarih, YNS{0}.CAR002.CAR002_Unvan1", "0TEST")).ToList();
                var json = new JavaScriptSerializer().Serialize(list);
                return json;
            }
        }
        /// <summary>
        /// Fatura onay
        /// </summary>
        [HttpPost]
        public JsonResult Fatura_Onay(string ID, bool Onay)
        {
            var result = new Result();
            try
            {
                using (YNSEntities dby = new YNSEntities())
                {
                    if (Onay == true)
                    {
                        var list = dby.TempFaturas.Where(m => m.IslemDurumu == 0 && m.EvrakNo == ID).Select(m => new SepetUrun { SatirTip = 1, UrunKodu = m.UrunKodu, HesapKodu = m.HesapKodu, Birim = m.Birim, Fiyat = m.Fiyat.ToString(), Miktar = m.Miktar.ToString(), Depo = m.Depo, ParaCinsi = m.ParaCinsi, KullaniciKodu = vUser.UserName }).ToList();
                        var yns = new YeniNesil(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, "0TEST");
                        var sepetIslemleri = yns.FaturaKaydet(list);
                        result = new Result(true, 1);
                    }
                    if (result.Status == true) dby.Database.ExecuteSqlCommand(string.Format("UPDATE [YNS{0}].[YNS{0}].[TempFatura] SET IslemDurumu={1} WHERE EvrakNo='{2}'", "0TEST", Onay == true ? 1 : 2, ID));
                }
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger(ex, "YN/OnayBekleyenler/Fatura_Onay");
                return Json(new Result(false, "Hata Oluştu"), JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// liste detayı
        /// </summary>
        [HttpPost]
        public PartialViewResult Fatura_Details(string ID)
        {
            using (YNSEntities dby = new YNSEntities())
            {
                var list = dby.Database.SqlQuery<frmOnayFatura>(string.Format(@"SELECT        YNS{0}.TempFatura.ID, YNS{0}.TempFatura.EvrakNo, YNS{0}.TempFatura.SiraNo, YNS{0}.TempFatura.HesapKodu, YNS{0}.TempFatura.UrunKodu AS MalKodu, YNS{0}.TempFatura.Depo, 
                                                                                                         YNS{0}.TempFatura.ParaCinsi, YNS{0}.TempFatura.Birim, YNS{0}.TempFatura.Miktar, YNS{0}.TempFatura.Fiyat, YNS{0}.TempFatura.IslemDurumu, YNS{0}.TempFatura.Kaydeden, 
                                                                                                         YNS{0}.TempFatura.KayitTarih, YNS{0}.TempFatura.KayitSaat, YNS{0}.TempFatura.Degistiren, YNS{0}.TempFatura.DegisTarih, YNS{0}.TempFatura.DegisSaat, YNS{0}.CAR002.CAR002_Unvan1 AS Unvan, 
                                                                                                         YNS{0}.STK004.STK004_Aciklama AS MalAdi
                                                                                FROM            YNS{0}.TempFatura INNER JOIN
                                                                                                         YNS{0}.CAR002 ON YNS{0}.TempFatura.HesapKodu = YNS{0}.CAR002.CAR002_HesapKodu INNER JOIN
                                                                                                         YNS{0}.STK004 ON YNS{0}.TempFatura.UrunKodu = YNS{0}.STK004.STK004_MalKodu
                                                                                WHERE        (YNS{0}.TempFatura.EvrakNo = '{1}')", "0TEST", ID)).ToList();
                return PartialView("Fatura_Details", list);
            }
        }
        #endregion
    }
}