using System;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Wms12m.Entity;

namespace Wms12m.Presentation.Areas.YN.Controllers
{
    public class OnayBekleyenlerController : RootController
    {
        string YnsSirketKodu = ConfigurationManager.AppSettings["YeniNesilSirket"].ToString();
        #region Sipariş
        /// <summary>
        /// sipariş onayı bekleyenler sayfası
        /// </summary>
        public ActionResult Siparis() => View("Siparis");

        /// <summary>
        /// sipariş onayı bekleyenler listesi
        /// </summary>
        public string Siparis_List(int Secim)
        {
            string secimParam = "";
            if (Secim == 0)
                secimParam = "Onay Bekliyor";
            else if (Secim == 1)
                secimParam = "Onaylandı";
            else if (Secim == 2)
                secimParam = "Reddedildi";
            else if (Secim == 3)
                secimParam = "Normal";

            var list = db.Database.SqlQuery<frmOnaySiparisList>(string.Format(@"
            SELECT  STK002_EvrakSeriNo AS EvrakSeriNo, CAR002_BankaHesapKodu AS HesapKodu, CAR002_Unvan1 AS Unvan, COUNT(STK002_MalKodu) 
		            AS Cesit, SUM(STK002_Miktari) AS Miktar, STK002_GirenKodu AS Kaydeden, 
                    CONVERT(VARCHAR(15), CAST(STK002_GirenTarih - 2 AS datetime), 104) AS Tarih,
                    ISNULL(YNS{0}.dbo.NotlariGetir(STK002_EvrakSeriNo, MIN(STK002_IslemTarihi), 5),'') as Notlar 
            FROM   YNS{0}.YNS{0}.STK002(NOLOCK) INNER JOIN
		           YNS{0}.YNS{0}.CAR002(NOLOCK) ON STK002_CariHesapKodu = CAR002_HesapKodu
            WHERE   STK002_GC = 1 AND STK002_SipDurumu = 0  AND STK002_Kod10 = '{1}'
            GROUP BY CAR002_BankaHesapKodu, CAR002_Unvan1, STK002_EvrakSeriNo, STK002_GirenKodu, CONVERT(VARCHAR(15), CAST(STK002_GirenTarih - 2 AS datetime), 104)
            ORDER BY STK002_EvrakSeriNo", YnsSirketKodu, secimParam)).ToList();
            var json = new JavaScriptSerializer().Serialize(list);
            return json;
        }
        /// <summary>
        /// liste detayı
        /// </summary>
        [HttpPost]
        public PartialViewResult Siparis_Details(string ID, int Secim)
        {
            string secimParam = "";
            if (Secim == 0)
                secimParam = "Onay Bekliyor";
            else if (Secim == 1)
                secimParam = "Onaylandı";
            else if (Secim == 2)
                secimParam = "Reddedildi";
            else if (Secim == 3)
                secimParam = "Normal";

            var list = db.Database.SqlQuery<frmOnaySiparisList>(string.Format(@"
            SELECT  STK002_MalKodu AS MalKodu, STK004.STK004_Aciklama AS MalAdi, STK002_CariHesapKodu AS HesapKodu, CAR002_Unvan1 AS Unvan, 
		            STK002_EvrakSeriNo AS EvrakSeriNo, STK002_Depo AS Depo, STK002_Miktari AS Miktar, STK002_BirimFiyati AS BirimFiyat, 
		            STK002_Tutari AS Tutar, STK002_DovizCinsi AS DovizCinsi, STK002_GirenKodu AS Kaydeden, CONVERT(VARCHAR(15), 
                    CAST(STK002_GirenTarih - 2 AS datetime), 104) AS Tarih,	
		            CASE WHEN ISNULL(STK002_Kod8,'')='' THEN 0.0 ELSE CONVERT(DECIMAL, STK002_Kod8) END AS EsikFiyat
																
            FROM   YNS{0}.YNS{0}.STK002(NOLOCK) 
            INNER JOIN  YNS{0}.YNS{0}.CAR002(NOLOCK) ON STK002_CariHesapKodu = CAR002_HesapKodu 
            INNER JOIN	YNS{0}.YNS{0}.STK004(NOLOCK) ON  STK002_MalKodu = STK004_MalKodu
            WHERE  (STK002_GC = 1) AND (STK002_SipDurumu = 0)  AND (STK002_Kod10 = 'Normal') AND  STK002_EvrakSeriNo = '{1}'", 
            YnsSirketKodu, ID, secimParam)).ToList();
            return PartialView("Siparis_Details", list);
        }
        /// <summary>
        /// onayla veya reddet
        /// </summary>
        [HttpPost]
        public JsonResult Siparis_Onay(string ID, bool Onay)
        {
            try
            {
                db.Database.ExecuteSqlCommand(string.Format("UPDATE YNS{0}.YNS{0}.[STK002] SET STK002_Kod10 = '{1}' WHERE STK002_EvrakSeriNo='{2}'", YnsSirketKodu, Onay == true ? "Onaylandı" : "Reddedildi", ID));
                return Json(new Result(true, 1), JsonRequestBehavior.AllowGet);
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
        public ActionResult Teklif() => View("Teklif");

        /// <summary>
        /// teklif onay listesi
        /// </summary>
        public string Teklif_List(int Secim)
        {
            var list = db.Database.SqlQuery<frmOnayTeklifList>(string.Format(@"SELECT TeklifNo,HesapKodu,COUNT(MalKodu) AS Cesit,
																					SUM(Miktar) AS Miktar,Kaydeden,
																					CONVERT(VARCHAR(15), CAST(KayitTarih - 2 AS datetime), 104) AS KayitTarihi,
																					CONVERT(VARCHAR(15), CAST(TeklifTarihi - 2 AS datetime), 104) AS TeklifTarihi
																					FROM YNS{0}.YNS{0}.[Teklif]
																					WHERE OnayDurumu={1}
																					  GROUP BY TeklifNo,TeklifTarihi,HesapKodu,KayitTarih,Kaydeden", YnsSirketKodu, Secim)).ToList();
            var json = new JavaScriptSerializer().Serialize(list);
            return json;
        }
        /// <summary>
        /// teklif detayları
        /// </summary>
        [HttpPost]
        public PartialViewResult Teklif_Details(string ID, int Secim)
        {
            var list = db.Database.SqlQuery<frmOnayTeklifListDetay>(string.Format(@"SELECT TeklifNo,HesapKodu,
																				YNS{0}.YNS{0}.CAR002.CAR002_Unvan1 AS Unvan,
																				Miktar AS Miktar,Kaydeden,MalKodu,STK004_Aciklama AS MalAdi,
																				Fiyat,Tutar,DovizCinsi, EsikFiyat,
																				CONVERT(VARCHAR(15), CAST(KayitTarih - 2 AS datetime), 104) AS KayitTarihi
																				  FROM YNS{0}.YNS{0}.[Teklif] INNER JOIN 
                                                                                        YNS{0}.YNS{0}.CAR002 ON HesapKodu = YNS{0}.YNS{0}.CAR002.CAR002_HesapKodu INNER JOIN
    																				    YNS{0}.YNS{0}.STK004 ON MalKodu = YNS{0}.YNS{0}.STK004.STK004_MalKodu WHERE TeklifNo='{1}' AND OnayDurumu={2}", YnsSirketKodu, ID, Secim)).ToList();
            return PartialView("Teklif_Details", list);
        }
        /// <summary>
        /// teklif onay
        /// </summary>
        [HttpPost]
        public JsonResult Teklif_Onay(string ID, bool Onay)
        {
            try
            {
                db.Database.ExecuteSqlCommand(string.Format("UPDATE YNS{0}.YNS{0}.[Teklif] SET OnayDurumu = {1} WHERE TeklifNo='{2}'", YnsSirketKodu, Onay == true ? 1 : 2, ID));
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
        public ActionResult Transfer() => View("Transfer");

        /// <summary>
        /// transfer onayı bekleyenler listesi
        /// </summary>
        public string Transfer_List(int Secim)
        {
            var list = db.Database.SqlQuery<frmOnayTransferList>(string.Format(@"SELECT        TransferNo, Kaydeden, CONVERT(VARCHAR(15), CAST(TransferTarihi - 2 AS datetime), 104) AS Tarih
																	FROM            YNS{0}.YNS{0}.TransferDepo
																	WHERE        (OnayDurumu = {1})
																	GROUP BY TransferNo, Kaydeden, CONVERT(VARCHAR(15), CAST(TransferTarihi - 2 AS datetime), 104)", YnsSirketKodu, Secim)).ToList();
            var json = new JavaScriptSerializer().Serialize(list);
            return json;
        }
        /// <summary>
        /// liste detayı
        /// </summary>
        [HttpPost]
        public PartialViewResult Transfer_Details(string ID)
        {
            var list = db.Database.SqlQuery<frmOnayTransferList>(string.Format(@"SELECT        YNS{0}.YNS{0}.TransferDepo.ID, YNS{0}.YNS{0}.TransferDepo.TransferNo, YNS{0}.YNS{0}.TransferDepo.TransferTarihi, YNS{0}.YNS{0}.TransferDepo.SiraNo, YNS{0}.YNS{0}.TransferDepo.MalKodu, YNS{0}.YNS{0}.STK004.STK004_Aciklama AS MalAdi,
																								  YNS{0}.YNS{0}.TransferDepo.Miktar, YNS{0}.YNS{0}.TransferDepo.Birim, YNS{0}.YNS{0}.TransferDepo.CikisDepo, YNS{0}.YNS{0}.TransferDepo.GirisDepo, YNS{0}.YNS{0}.TransferDepo.TalepEden, YNS{0}.YNS{0}.TransferDepo.OnayDurumu, 
																								 YNS{0}.YNS{0}.TransferDepo.Kaydeden, YNS{0}.YNS{0}.TransferDepo.KayitTarih
																		FROM            YNS{0}.YNS{0}.TransferDepo INNER JOIN
																								 YNS{0}.YNS{0}.STK004 ON YNS{0}.YNS{0}.TransferDepo.MalKodu = YNS{0}.YNS{0}.STK004.STK004_MalKodu
																		WHERE        YNS{0}.YNS{0}.TransferDepo.TransferNo = '{1}'", YnsSirketKodu, ID)).ToList();
            return PartialView("Transfer_Details", list);
        }
        /// <summary>
        /// onayla veya reddet
        /// </summary>
        [HttpPost]
        public JsonResult Transfer_Onay(string ID, bool Onay)
        {
            var result = new Result();
            try
            {
                db.Database.ExecuteSqlCommand(string.Format(@"UPDATE YNS{0}.YNS{0}.TransferDepo SET OnayDurumu = {1} WHERE (TransferNo = '{2}')", YnsSirketKodu, Onay == true ? 1 : 2, ID));
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
        public ActionResult Fatura() => View("Fatura");

        /// <summary>
        /// Fatura onayı bekleyenler listesi
        /// </summary>
        public string Fatura_List(int Secim)
        {
            /// Secim => 0 Onay Bekleyenler  1 Onaylanmış  2 Reddedilmiş  3 Normal (Direkt)Onaylı
            var list = db.Database.SqlQuery<frmOnayFatura>(string.Format(@"
            SELECT  TempFatura.EvrakNo, TempFatura.HesapKodu, TempFatura.Depo, COUNT(TempFatura.ID) AS Cesit, 
		            SUM(TempFatura.Miktar) AS Miktar, TempFatura.Kaydeden, CONVERT(VARCHAR(15), 
		            CAST(TempFatura.KayitTarih - 2 AS datetime), 104) as KayitTarih, MIN(CAR002.CAR002_Unvan1) AS Unvan,
                    MIN(Adres1+' '+Adres2+' '+Adres3+' '+Aciklama1+' '+Aciklama2+' '+Aciklama3) as Notlar
            FROM  YNS{0}.YNS{0}.TempFatura(NOLOCK) 
            INNER JOIN YNS{0}.YNS{0}.CAR002(NOLOCK) ON TempFatura.HesapKodu = CAR002.CAR002_HesapKodu
            WHERE TempFatura.IslemDurumu = {1}
            GROUP BY TempFatura.EvrakNo, TempFatura.HesapKodu, TempFatura.Depo, TempFatura.Kaydeden, TempFatura.KayitTarih", YnsSirketKodu, Secim)).ToList();

            var json = new JavaScriptSerializer().Serialize(list);
            return json;
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
                if (Onay == true)
                {
                    var list = db.Database.SqlQuery<SepetUrun>(string.Format("SELECT 1 AS SatirTip, HesapKodu, UrunKodu, Birim, CONVERT(varchar(10), Miktar) as Miktar, CONVERT(varchar(10), Fiyat) as Fiyat, Depo, ParaCinsi, '{2}' AS KullaniciKodu, Kaydeden FROM YNS{0}.YNS{0}.TempFatura WHERE (EvrakNo = '{1}') AND (IslemDurumu = 0)", YnsSirketKodu, ID, vUser.UserName)).ToList();
                    var yns = new YeniNesil(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, YnsSirketKodu);
                    var sepetIslemleri = yns.FaturaKaydet(list);
                    result = new Result(true, 1);
                }

                if (result.Status == true) db.Database.ExecuteSqlCommand(string.Format("UPDATE YNS{0}.YNS{0}.[TempFatura] SET IslemDurumu={1} WHERE EvrakNo='{2}'", YnsSirketKodu, Onay == true ? 1 : 2, ID));
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
            var list = db.Database.SqlQuery<frmOnayFatura>(string.Format(@"SELECT        YNS{0}.YNS{0}.TempFatura.ID, YNS{0}.YNS{0}.TempFatura.EvrakNo, YNS{0}.YNS{0}.TempFatura.SiraNo, YNS{0}.YNS{0}.TempFatura.HesapKodu, YNS{0}.YNS{0}.TempFatura.UrunKodu AS MalKodu, YNS{0}.YNS{0}.TempFatura.Depo, 
																										 YNS{0}.YNS{0}.TempFatura.ParaCinsi, YNS{0}.YNS{0}.TempFatura.Birim, YNS{0}.YNS{0}.TempFatura.Miktar, YNS{0}.YNS{0}.TempFatura.Fiyat, YNS{0}.YNS{0}.TempFatura.IslemDurumu, YNS{0}.YNS{0}.TempFatura.Kaydeden, 
																										 CONVERT(VARCHAR(15), CAST(YNS{0}.YNS{0}.TempFatura.KayitTarih - 2 AS datetime), 104) as KayitTarih, YNS{0}.YNS{0}.TempFatura.KayitSaat, YNS{0}.YNS{0}.TempFatura.Degistiren, 
																										 CONVERT(VARCHAR(15), CAST(YNS{0}.YNS{0}.TempFatura.DegisTarih - 2 AS datetime), 104) as DegisTarih,
																										  YNS{0}.YNS{0}.TempFatura.DegisSaat, YNS{0}.YNS{0}.CAR002.CAR002_Unvan1 AS Unvan,  TempFatura.EsikFiyat,
																										 YNS{0}.YNS{0}.STK004.STK004_Aciklama AS MalAdi
																				FROM            YNS{0}.YNS{0}.TempFatura INNER JOIN
																										 YNS{0}.YNS{0}.CAR002 ON YNS{0}.YNS{0}.TempFatura.HesapKodu = YNS{0}.YNS{0}.CAR002.CAR002_HesapKodu INNER JOIN
																										 YNS{0}.YNS{0}.STK004 ON YNS{0}.YNS{0}.TempFatura.UrunKodu = YNS{0}.YNS{0}.STK004.STK004_MalKodu
																				WHERE        (YNS{0}.YNS{0}.TempFatura.EvrakNo = '{1}')", YnsSirketKodu, ID)).ToList();
            return PartialView("Fatura_Details", list);
        }
        #endregion
        #region Satış İade
        /// <summary>
        /// Fatura onayı bekleyenler sayfası
        /// </summary>
        public ActionResult SatisIade() => View("SatisIade");

        /// <summary>
        /// Fatura onayı bekleyenler listesi
        /// </summary>
        public string SatisIade_List()
        {
            var list = db.Database.SqlQuery<SatisIadeIcmal>(string.Format(SatisIadeIcmal.Sorgu, YnsSirketKodu)).ToList();

            var json = new JavaScriptSerializer().Serialize(list);
            return json;
        }
        /// <summary>
        /// Fatura onay
        /// </summary>
        [HttpPost]
        public JsonResult SatisIade_Onay(string ID, bool Onay)
        {
            var result = new Result();
            try
            {
                string[] ids = ID.Split(',');
                var yns = new YeniNesil(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, YnsSirketKodu);
                yns.SatisIadeOnay(new SatisIadeOnay
                {
                    IadeNo = ids[0],
                    IadeTarih = ids[1],
                    Onay = Onay,
                    Kaydeden = vUser.UserName
                });
                result = new Result(true, 1);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger(ex, "YN/OnayBekleyenler/SatisIade_Onay");
                return Json(new Result(false, "Hata Oluştu"), JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// liste detayı
        /// </summary>
        [HttpPost]
        public PartialViewResult SatisIade_Details(string ID)
        {
            string[] ids = ID.Split(',');
            var list = db.Database.SqlQuery<SatisIadeDetay>(string.Format(SatisIadeDetay.Sorgu, YnsSirketKodu, ids[0], ids[1])).ToList();
            return PartialView("SatisIade_Details", list);
        }
        #endregion
        #region Tahsilat
        /// <summary>
        /// tahsilat onayı bekleyenler sayfası
        /// </summary>
        public ActionResult Tahsilat() => View("Tahsilat");
        // <summary>
        // tahsilat onayı bekleyenler listesi
        // </summary>
        public string Tahsilat_List()
        {
            var list = db.Database.SqlQuery<frmOnayTahsilatList>(string.Format(@"
				SELECT TahsilatNo, HesapKodu, CAR002_Unvan1 AS Unvan,
						CONVERT(VARCHAR(15), CAST(TahsilatTarihi - 2 AS datetime), 104) as Tarih,
						CASE IslemTuru WHEN 0 THEN 'Tahsilat' WHEN 1 THEN 'İskonto' END as IslemTuru,
						CASE OdemeTuru WHEN 0 THEN 'Nakit' WHEN 1 THEN 'Kredi Kartı' END as OdemeTuru,
						Tutar, DovizCinsi, KapatilanTL, KapatilanUSD, KapatilanEUR, Kaydeden, Aciklama
				FROM YNS{0}.YNS{0}.TahsilatMobil(NOLOCK) 
                LEFT JOIN YNS{0}.YNS{0}.CAR002 ON TahsilatMobil.HesapKodu = CAR002_HesapKodu
				WHERE IslemDurumu=0", YnsSirketKodu)).ToList();

            var json = new JavaScriptSerializer().Serialize(list);
            return json;
        }
        /// <summary>
        /// onayla veya reddet
        /// </summary>
        [HttpPost]
        public JsonResult Tahsilat_Onay(string ID, bool Onay)
        {
            var result = new Result();
            try
            {
                if (Onay == true)
                {
                    var item = db.Database.SqlQuery<frmOnayTahsilatList>(string.Format(@"
                    SELECT TahsilatNo, HesapKodu, CAR002_Unvan1 AS Unvan,
                        CONVERT(VARCHAR(15), CAST(TahsilatTarihi - 2 AS datetime), 104) as Tarih,
                        CASE IslemTuru WHEN 0 THEN 'Tahsilat' WHEN 1 THEN 'İskonto' END as IslemTuru,
                        CASE OdemeTuru WHEN 0 THEN 'Nakit' WHEN 1 THEN 'Kredi Kartı' END as OdemeTuru,
                        Tutar, DovizCinsi, KapatilanTL, KapatilanUSD, KapatilanEUR, Kaydeden, Aciklama

                    FROM YNS{0}.YNS{0}.TahsilatMobil(NOLOCK)
                    LEFT JOIN YNS{0}.YNS{0}.CAR002 ON TahsilatMobil.HesapKodu = CAR002_HesapKodu
                    WHERE TahsilatNo='{1}'", YnsSirketKodu, ID)).FirstOrDefault();

                    var yns = new YeniNesil(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, YnsSirketKodu);
                    var sepetIslemleri = yns.TahsilatKaydet(item, vUser.UserName);
                    result = new Result(true, 1);
                }

                if (result.Status == true)
                {
                    db.Database.ExecuteSqlCommand(string.Format("UPDATE YNS{0}.YNS{0}.[TahsilatMobil] SET IslemDurumu = '{1}' WHERE TahsilatNo='{2}'", YnsSirketKodu, Onay == true ? 1 : 2, ID));
                }
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger(ex, "YN/OnayBekleyenler/Tahsilat_Onay");
                return Json(new Result(false, "Hata Oluştu"), JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
    }
}