using Birikim.Models;
using Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Wms12m.Entity;

namespace Wms12m.Presentation.Areas.YN.Controllers
{
    public class OnayBekleyenlerController : RootController
    {
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
					ISNULL(YNS{0}.dbo.NotlariGetir(STK002_EvrakSeriNo, MIN(STK002_IslemTarihi), 5),'') as Notlar,
					(case when MAX(CONVERT(VARCHAR(15), CAST(STK002_Kod12 - 2 AS datetime), 104)) = '30.12.1899' then '' else MAX(CONVERT(VARCHAR(15), CAST(STK002_Kod12 - 2 AS datetime), 104)) END ) AS OnayRedTarih,
					MAX(STK002_Kod3) as OnaylayanReddeden
			FROM   YNS{0}.YNS{0}.STK002(NOLOCK)
			INNER JOIN
				   YNS{0}.YNS{0}.CAR002(NOLOCK) ON STK002_CariHesapKodu = CAR002_HesapKodu
			WHERE   STK002_GC = 1 AND STK002_SipDurumu = 0  AND STK002_Kod10 = '{1}'
			GROUP BY CAR002_BankaHesapKodu, CAR002_Unvan1, STK002_EvrakSeriNo, STK002_GirenKodu, CONVERT(VARCHAR(15), CAST(STK002_GirenTarih - 2 AS datetime), 104)
			ORDER BY STK002_EvrakSeriNo

			", vUser.SirketKodu, secimParam)).ToList();
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
			WHERE  (STK002_GC = 1) AND (STK002_SipDurumu = 0)  AND (STK002_Kod10 = '{1}') AND  STK002_EvrakSeriNo = '{2}'",
            vUser.SirketKodu, secimParam, ID)).ToList();
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

                db.Database.ExecuteSqlCommand(string.Format("UPDATE YNS{0}.YNS{0}.[STK002] SET STK002_Kod12=datediff(d,0,getdate()+2), STK002_Kod10 = '{1}', STK002_Kod3 = '{3}' WHERE STK002_EvrakSeriNo='{2}'", vUser.SirketKodu, Onay == true ? "Onaylandı" : "Reddedildi", ID, vUser.UserName));

                //var evrakvarmi = db.Database.SqlQuery<string>(string.Format("SELECT STK005_EvrakSeriNo from YNS{0}.YNS{0}.[STK005] WHERE STK005_EvrakSeriNo = '{1}' ", vUser.SirketKodu, ID)).FirstOrDefault();
                //db.Database.ExecuteSqlCommand(string.Format("UPDATE YNS{0}.YNS{0}.[STK005] SET STK005_Kod3='{1}' WHERE STK005_EvrakSeriNo='{2}'", vUser.SirketKodu, vUser.UserName, ID));
                //            if (evrakvarmi == null || evrakvarmi == "")
                //            {
                //                db.Database.ExecuteSqlCommand(string.Format(@"
                // INSERT INTO YNS{0}.YNS{0}.[STK005] (STK005_Kod3, STK005_EvrakSeriNo)
                // VALUES ('{1}','{2}')
                //", vUser.SirketKodu, vUser.UserName, ID));
                //            }
                //            else
                //            {
                //                db.Database.ExecuteSqlCommand(string.Format("UPDATE YNS{0}.YNS{0}.[STK005] SET STK005_Kod3='{1}' WHERE STK005_EvrakSeriNo='{2}'", vUser.SirketKodu, vUser.UserName, ID));
                //            }

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
																					  GROUP BY TeklifNo,TeklifTarihi,HesapKodu,KayitTarih,Kaydeden", vUser.SirketKodu, Secim)).ToList();
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
																						YNS{0}.YNS{0}.STK004 ON MalKodu = YNS{0}.YNS{0}.STK004.STK004_MalKodu WHERE TeklifNo='{1}' AND OnayDurumu={2}", vUser.SirketKodu, ID, Secim)).ToList();
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
                db.Database.ExecuteSqlCommand(string.Format("UPDATE YNS{0}.YNS{0}.[Teklif] SET OnayDurumu = {1} WHERE TeklifNo='{2}'", vUser.SirketKodu, Onay == true ? 1 : 2, ID));
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
																	GROUP BY TransferNo, Kaydeden, CONVERT(VARCHAR(15), CAST(TransferTarihi - 2 AS datetime), 104)", vUser.SirketKodu, Secim)).ToList();
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
																		WHERE        YNS{0}.YNS{0}.TransferDepo.TransferNo = '{1}'", vUser.SirketKodu, ID)).ToList();
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
                db.Database.ExecuteSqlCommand(string.Format(@"UPDATE YNS{0}.YNS{0}.TransferDepo SET OnayDurumu = {1} WHERE (TransferNo = '{2}')", vUser.SirketKodu, Onay == true ? 1 : 2, ID));
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
			GROUP BY TempFatura.EvrakNo, TempFatura.HesapKodu, TempFatura.Depo, TempFatura.Kaydeden, TempFatura.KayitTarih", vUser.SirketKodu, Secim)).ToList();

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
                    var list = db.Database.SqlQuery<SepetUrun>(string.Format("SELECT 1 AS SatirTip, HesapKodu, UrunKodu, Birim, CONVERT(varchar(50), Miktar) as Miktar, CONVERT(varchar(50), Fiyat) as Fiyat, Depo, ParaCinsi, '{2}' AS KullaniciKodu, Kaydeden FROM YNS{0}.YNS{0}.TempFatura WHERE (EvrakNo = '{1}') AND (IslemDurumu = 0)", vUser.SirketKodu, ID, vUser.UserName)).ToList();
                    var yns = new YeniNesil(SqlExper, vUser.SirketKodu);
                    var sepetIslemleri = yns.FaturaKaydet(list);
                    result = new Result(true, 1);
                }

                if (result.Status == true)
                {

                    db.Database.ExecuteSqlCommand(string.Format("UPDATE YNS{0}.YNS{0}.[TempFatura] SET IslemDurumu={1} WHERE EvrakNo='{2}'", vUser.SirketKodu, Onay == true ? 1 : 2, ID));
                    db.Database.ExecuteSqlCommand(string.Format("UPDATE YNS{0}.YNS{0}.[STK005] SET STK005_Kod12=datediff(d,0,getdate()+2), STK005_Kod3='{1}' WHERE STK005_EvrakSeriNo='{2}'", vUser.SirketKodu, vUser.UserName, ID));

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
            var list = db.Database.SqlQuery<frmOnayFatura>(string.Format(@"SELECT        YNS{0}.YNS{0}.TempFatura.ID, YNS{0}.YNS{0}.TempFatura.EvrakNo, YNS{0}.YNS{0}.TempFatura.SiraNo, YNS{0}.YNS{0}.TempFatura.HesapKodu, YNS{0}.YNS{0}.TempFatura.UrunKodu AS MalKodu, YNS{0}.YNS{0}.TempFatura.Depo, 
																										 YNS{0}.YNS{0}.TempFatura.ParaCinsi, YNS{0}.YNS{0}.TempFatura.Birim, YNS{0}.YNS{0}.TempFatura.Miktar, YNS{0}.YNS{0}.TempFatura.Fiyat, YNS{0}.YNS{0}.TempFatura.IslemDurumu, YNS{0}.YNS{0}.TempFatura.Kaydeden, 
																										 CONVERT(VARCHAR(15), CAST(YNS{0}.YNS{0}.TempFatura.KayitTarih - 2 AS datetime), 104) as KayitTarih, YNS{0}.YNS{0}.TempFatura.KayitSaat, YNS{0}.YNS{0}.TempFatura.Degistiren, 
																										 CONVERT(VARCHAR(15), CAST(YNS{0}.YNS{0}.TempFatura.DegisTarih - 2 AS datetime), 104) as DegisTarih,
																										  YNS{0}.YNS{0}.TempFatura.DegisSaat, YNS{0}.YNS{0}.CAR002.CAR002_Unvan1 AS Unvan,  TempFatura.EsikFiyat,
																										 YNS{0}.YNS{0}.STK004.STK004_Aciklama AS MalAdi
																				FROM            YNS{0}.YNS{0}.TempFatura INNER JOIN
																										 YNS{0}.YNS{0}.CAR002 ON YNS{0}.YNS{0}.TempFatura.HesapKodu = YNS{0}.YNS{0}.CAR002.CAR002_HesapKodu INNER JOIN
																										 YNS{0}.YNS{0}.STK004 ON YNS{0}.YNS{0}.TempFatura.UrunKodu = YNS{0}.YNS{0}.STK004.STK004_MalKodu
																				WHERE        (YNS{0}.YNS{0}.TempFatura.EvrakNo = '{1}')", vUser.SirketKodu, ID)).ToList();
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
            var list = db.Database.SqlQuery<SatisIadeIcmal>(string.Format(SatisIadeIcmal.Sorgu, vUser.SirketKodu)).ToList();

            var json = new JavaScriptSerializer().Serialize(list);
            return json;
        }
        /// <summary>
        /// SatisIade onay
        /// </summary>
        [HttpPost]
        public JsonResult SatisIade_Onay(SatisIadeOnayList tbl)
        {
            var result = new Result();
            try
            {
                string[] ids = tbl.ID.Split(',');
                var yns = new YeniNesil(SqlExper, vUser.SirketKodu);
                yns.SatisIadeOnay(new SatisIadeOnay
                {
                    IadeNo = ids[0],
                    IadeTarih = ids[1],
                    Onay = true,
                    Kaydeden = vUser.UserName,
                    tbl = tbl
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
        /// SatisIade_Red
        /// </summary>
        [HttpPost]
        public JsonResult SatisIade_Red(string ID)
        {
            var result = new Result();
            try
            {
                string[] ids = ID.Split(',');
                var yns = new YeniNesil(SqlExper, vUser.SirketKodu);
                yns.SatisIadeOnay(new SatisIadeOnay
                {
                    IadeNo = ids[0],
                    IadeTarih = ids[1],
                    Onay = false,
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
            var list = db.Database.SqlQuery<SatisIadeDetay>(string.Format(SatisIadeDetay.Sorgu, vUser.SirketKodu, ids[0], ids[1])).ToList();
            ViewBag.ID = ID;
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
						Tutar, DovizCinsi, KapatilanTL, KapatilanUSD, KapatilanEUR, Kaydeden, Aciklama,
						(CASE WHEN KapatilanUSD>0 THEN ISNULL(DvzEfektisSatis1, 0) ELSE 0 END) AS USDKur,
						(CASE WHEN KapatilanEUR>0 THEN ISNULL(DvzEfektisSatis2, 0) ELSE 0 END) AS EURKur
				FROM YNS{0}.YNS{0}.TahsilatMobil(NOLOCK) 
				LEFT JOIN YNS{0}.YNS{0}.CAR002 ON TahsilatMobil.HesapKodu = CAR002_HesapKodu
				WHERE IslemDurumu=0", vUser.SirketKodu)).ToList();

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
						Tutar, DovizCinsi, KapatilanTL, KapatilanUSD, KapatilanEUR, Kaydeden, Aciklama,
						ISNULL(DvzEfektisSatis1,0) AS USDKur , ISNULL(DvzEfektisSatis2,0) AS EURKur
					FROM YNS{0}.YNS{0}.TahsilatMobil(NOLOCK)
					LEFT JOIN YNS{0}.YNS{0}.CAR002 ON TahsilatMobil.HesapKodu = CAR002_HesapKodu
					WHERE TahsilatNo='{1}'", vUser.SirketKodu, ID)).FirstOrDefault();

                    var yns = new YeniNesil(SqlExper, vUser.SirketKodu);
                    result = yns.TahsilatKaydet(item, vUser.UserName);
                }

                if (result.Status == true)
                {
                    db.Database.ExecuteSqlCommand(string.Format("UPDATE YNS{0}.YNS{0}.[TahsilatMobil] SET IslemDurumu = '{1}' WHERE TahsilatNo='{2}'", vUser.SirketKodu, Onay == true ? 1 : 2, ID));
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

        #region IskontroListAktarim
        public ActionResult IskontoListAktarim()
        {
            return View("IskontoListAktarim");
        }
        public string IskontoListAktarimSelect()
        {
            JavaScriptSerializer json = new JavaScriptSerializer() { MaxJsonLength = int.MaxValue };
            List<IskontoList> ist;
            string sorgu = "";
            sorgu = String.Format(IskontoList.SelectSorgu, vUser.SirketKodu);
            try
            {
                ist = db.Database.SqlQuery<IskontoList>(sorgu).ToList();
                foreach (IskontoList item in ist)
                {
                    item.BasTarihDate = item.BasTarih.IntToDate();
                    item.BitTarihDate = item.BitTarih.IntToDate();
                    item.BasTarihStr = item.BasTarihDate.ToString("dd-MM-yyyy");
                    item.BitTarihStr = item.BasTarihDate.ToString("dd-MM-yyyy");
                }
            }
            catch (Exception ex)
            {
                Logger(ex, "YN/OnayBekleyenler/IskontoListAktarim");
                ist = new List<IskontoList>();
            }
            return json.Serialize(ist);
        }
        public JsonResult Kat(HttpPostedFileBase file)
        {
            if (CheckPerm(Perms.KatKartı, PermTypes.Writing) == false) { return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet); }
            Result _Result = new Result(false, "Hatalı dosya!");
            if (file.IsNull() || file.ContentLength == 0) { return Json(_Result, JsonRequestBehavior.AllowGet); }
            // gelen dosyayı oku
            var stream = file.InputStream;
            IExcelDataReader reader;
            // dosya tipini bul
            if (file.FileName.EndsWith(".xlsx")) { reader = ExcelReaderFactory.CreateOpenXmlReader(stream); }
            else { return Json(_Result, JsonRequestBehavior.AllowGet); }
            // ilk satır başlık
            reader.IsFirstRowAsColumnNames = true;
            // exceldeki bilgileri datasete aktar
            DataSet result = reader.AsDataSet();
            // kontrol
            if (result.Tables.Count == 0) { return Json(_Result, JsonRequestBehavior.AllowGet); }
            if (result.Tables[0].Rows == null) { return Json(_Result, JsonRequestBehavior.AllowGet); }
            // her satırı tek tek kaydet
            string listeNo = "", basTar = "", listeAdi = "", bitTar = "", malKodu = "", iskOran1 = "", iskOran2 = "", iskOran3 = "", hatalilar = "";
            int basarili = 0, hatali = 0;
            for (int i = 0; i < result.Tables[0].Rows.Count; i++)
            {
                DataRow dr = result.Tables[0].Rows[i];
                // kontrol
                try
                {
                    listeNo = dr["List No"].ToString2();
                    basTar = dr["Baslangic Tarih"].ToString2();
                    listeAdi = dr["Liste Adi"].ToString2();
                    bitTar = dr["Bitis Tarih"].ToString2();
                    malKodu = dr["Mal Kodu"].ToString2();
                    iskOran1 = dr["Iskonto Oran 1"].ToString2();
                    iskOran2 = dr["Iskonto Oran 2"].ToString2();
                    iskOran3 = dr["Iskonto Oran 3"].ToString2();
                    if (listeNo.IsNotNullEmpty()
                        && listeAdi.IsNotNullEmpty()
                        && malKodu.IsNotNullEmpty()
                        && basTar.IsNotNullEmpty()
                        && bitTar.IsNotNullEmpty()
                        && basTar.IsDate()
                        && bitTar.IsDate())
                    {
                        iskOran1 = (iskOran1.IsNullEmpty() ? "0" : iskOran1);
                        iskOran2 = (iskOran2.IsNullEmpty() ? "0" : iskOran2);
                        iskOran3 = (iskOran3.IsNullEmpty() ? "0" : iskOran3);
                        string delSorgu = "", insertSorgu = "";
                        delSorgu = String.Format(IskontoList.DeleteSorgu, vUser.SirketKodu);
                        insertSorgu = String.Format(IskontoList.InsertSorgu, vUser.SirketKodu);
                        if (i == 0) { db.Database.ExecuteSqlCommand(delSorgu); }
                        db.Database.ExecuteSqlCommand(insertSorgu
                            , new SqlParameter("LISTENO", listeNo)
                            , new SqlParameter("BASTARIH", basTar.ToDatetime().Date.ToOADateInt())
                            , new SqlParameter("SIRANO", i.ToShort())
                            , new SqlParameter("LISTEADI", listeAdi)
                            , new SqlParameter("BITTARIH", bitTar.ToDatetime().Date.ToOADateInt())

                            , new SqlParameter("HESAPKODU", "")
                            , new SqlParameter("MALKODGRUP", 0)
                            , new SqlParameter("MALKOD", malKodu)
                            , new SqlParameter("ISKORAN1", iskOran1.ToDecimal())
                            , new SqlParameter("ISKORAN2", iskOran2.ToDecimal())

                            , new SqlParameter("ISKORAN3", iskOran3.ToDecimal())
                            , new SqlParameter("ACIKLAMA", "")
                            , new SqlParameter("AKTIFMI", true)
                            , new SqlParameter("KAYDEDEN", vUser.UserName)
                            , new SqlParameter("KAYITTARIH", DateTime.Today.ToOADateInt())

                            , new SqlParameter("KAYITSAAT", DateTime.Now.ToOaTime())
                            , new SqlParameter("DEGISTIREN", vUser.UserName)
                            , new SqlParameter("DEGISTARIH", DateTime.Today.ToOADateInt())
                            , new SqlParameter("DEGISSAAT", DateTime.Now.ToOaTime()));
                        basarili++;
                    }
                    else
                    {
                        hatali++;
                        if (hatalilar.IsNotNullEmpty()) { hatalilar = String.Concat(hatalilar, ", "); }
                        hatalilar = String.Concat(hatalilar, (i + 1));
                    }
                }
                catch (Exception ex)
                {
                    hatali++;
                    if (hatalilar.IsNotNullEmpty()) { hatalilar = String.Concat(hatalilar, ", "); }
                    hatalilar = String.Concat(hatalilar, (i + 1));
                    Logger(ex, "OnayBekleyenler/Kat");
                }
            }
            reader.Close();
            if (basarili > 0)
            {
                _Result.Message = basarili + " adet satır eklendi";
                LogActions("", "OnayBekleyenler", "Kat", ComboItems.alYükle, 0, "Satır Sayısı: " + basarili);
            }
            else { _Result.Message = ""; }
            if (basarili > 0 && hatali > 0) { _Result.Message = String.Concat(_Result.Message, ", "); }
            if (hatali > 0) { _Result.Message = String.Concat(_Result.Message, String.Format(@"{0} satır hata verdi. Hatalı satırlar: \n{1}", hatali, hatalilar)); }
            else { _Result.Status = true; }
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}