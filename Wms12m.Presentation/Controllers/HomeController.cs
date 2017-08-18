using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m.Presentation.Controllers
{
    public class HomeController : RootController
    {
        /// <summary>
        /// Anasayfa
        /// </summary>
        public ActionResult Index()
        {
            var SirketKodu = db.GetSirketDBs().FirstOrDefault();
            Setting setts = ViewBag.settings;
            BekleyenOnaylar bo = new BekleyenOnaylar();
            //Bekleyen Onaylar
            if (setts.OnayCek == true || setts.OnayFiyat == true || setts.OnayRisk == true || setts.OnaySiparis == true || setts.OnaySozlesme == true || setts.OnayStok == true || setts.OnayTekno == true || setts.OnayTeminat == true)
                try
                {
                    bo = db.Database.SqlQuery<BekleyenOnaylar>(string.Format("[FINSAT6{0}].[wms].[DB_BekleyenOnaylar]", SirketKodu)).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    Logger(ex, "Home/Index");
                }
            //kasa miktarları
            try
            {
                ViewBag.MevcudBanka = db.Database.SqlQuery<decimal>(string.Format("[FINSAT6{0}].[wms].[MevcudBanka]", SirketKodu)).FirstOrDefault();
                ViewBag.MevcudCek = db.Database.SqlQuery<decimal>(string.Format("[FINSAT6{0}].[wms].[MevcudCek]", SirketKodu)).FirstOrDefault();
                ViewBag.MevcudKasa = db.Database.SqlQuery<decimal>(string.Format("[FINSAT6{0}].[wms].[MevcudKasa]", SirketKodu)).FirstOrDefault();
                ViewBag.MevcudPOS = db.Database.SqlQuery<decimal>(string.Format("[FINSAT6{0}].[wms].[MevcudPOS]", SirketKodu)).FirstOrDefault();
            }
            catch (Exception)
            {
                ViewBag.MevcudBanka = 0;
                ViewBag.MevcudCek = 0;
                ViewBag.MevcudKasa = 0;
                ViewBag.MevcudPOS = 0;
            }
            //return
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.Settings = setts;
            ViewBag.BekleyenOnaylar = bo;
            ViewBag.RoleName = vUser.RoleName;
            ViewBag.Id = vUser.Id;
            return View("Index", db.GetHomeSummary(vUser.UserName, vUser.Id).FirstOrDefault());
        }
        /// <summary>
        /// child view for menu
        /// </summary>
        /// <param name="mYeri">menu yeri no</param>
        /// <param name="mUstNo">üst menu no</param>
        /// <param name="url">current url</param>
        [ChildActionOnly]
        public PartialViewResult Menu(byte mYeri, short mUstNo, string url)
        {
            for (int i = 0; i < 3; i++)
            {
                var ind = url.IndexOf("/");
                url = url.Right(url.Length - ind - 1);
            }
            url = "/" + url;
            ViewBag.ustMenu = mUstNo;
            var tablo = db.MenuGetirici(ComboItems.WMS.ToInt32(), mYeri, vUser.RoleName, mUstNo, url).ToList();
            return PartialView("../Shared/_MenuList", tablo);
        }
        /// <summary>
        /////////////////////////////////////////////// partials
        /// </summary>
        public PartialViewResult PartialGunlukSatisZamanCizelgesi(string SirketKodu)
        {
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            if (CheckPerm(Perms.ChartGunlukSatis, PermTypes.Reading) == false) return PartialView("_PartialGunlukSatisZamanCizelgesi", new List<ChartBaglantiZaman>());
            List<ChartBaglantiZaman> BUGS;
            try
            {
                BUGS = db.Database.SqlQuery<ChartBaglantiZaman>(string.Format("[FINSAT6{0}].[wms].[DB_GunlukSatisGetir]", SirketKodu)).ToList();
            }
            catch (Exception ex)
            {
                Logger(ex, "Home/PartialGunlukSatisZamanCizelgesi");
                BUGS = new List<ChartBaglantiZaman>();
            }
            return PartialView("_PartialGunlukSatisZamanCizelgesi", BUGS);
        }

        public PartialViewResult PartialGunlukSatis(string SirketKodu, int tarih)
        {
            ViewBag.tarih = tarih;
            ViewBag.tarih2 = tarih.FromOADateInt();
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            if (CheckPerm(Perms.ChartGunlukSatis, PermTypes.Reading) == false) return PartialView("_PartialGunlukSatis", new List<ChartGunlukSatisAnalizi>());
            List<ChartGunlukSatisAnalizi> liste;
            try
            {
                liste = db.Database.SqlQuery<ChartGunlukSatisAnalizi>(string.Format("[FINSAT6{0}].[wms].[DB_GunlukSatisAnalizi] @Tarih = {1}", SirketKodu, tarih)).ToList();
            }
            catch (Exception)
            {
                liste = new List<ChartGunlukSatisAnalizi>();
            }
            return PartialView("_PartialGunlukSatis", liste);
        }

        public PartialViewResult PartialGunlukSatisPie(string SirketKodu, int tarih)
        {
            ViewBag.tarih = tarih;
            ViewBag.tarih2 = tarih.FromOADateInt();
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            if (CheckPerm(Perms.ChartGunlukSatis, PermTypes.Reading) == false) return PartialView("_PartialGunlukSatisPie", new List<ChartGunlukSatisAnalizi>());
            List<ChartGunlukSatisAnalizi> liste;
            try
            {
                liste = db.Database.SqlQuery<ChartGunlukSatisAnalizi>(string.Format("[FINSAT6{0}].[wms].[DB_GunlukSatisAnalizi] @Tarih = {1}", SirketKodu, tarih)).ToList();
            }
            catch (Exception)
            {
                liste = new List<ChartGunlukSatisAnalizi>();
            }
            return PartialView("_PartialGunlukSatisPie", liste);
        }

        public PartialViewResult PartialGunlukSatisYearToDay(string SirketKodu)
        {
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            if (CheckPerm(Perms.ChartGunlukSatis, PermTypes.Reading) == false) return PartialView("_PartialGunlukSatısAnaliziYearToDay", new List<GetCachedChartYear2Day_Result>());
            var GSA = db.GetCachedChartYear2Day(SirketKodu).ToList();
            if (GSA.Count == 0)
                try
                {
                    GSA = db.Database.SqlQuery<GetCachedChartYear2Day_Result>(string.Format("[FINSAT6{0}].[wms].[DB_GunlukSatisAnaliziYearToDay]", SirketKodu)).ToList();
                }
                catch (Exception ex)
                {
                    Logger(ex, "Home/ChartGunlukSatisYearToDay");
                    GSA = new List<GetCachedChartYear2Day_Result>();
                }
            return PartialView("_PartialGunlukSatısAnaliziYearToDay", GSA);
        }

        public PartialViewResult PartialGunlukSatisYearToDayPie(string SirketKodu)
        {
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            if (CheckPerm(Perms.ChartGunlukSatis, PermTypes.Reading) == false) return PartialView("PartialGunlukSatisYearToDayPie", new List<GetCachedChartYear2Day_Result>());
            var GSA = db.GetCachedChartYear2Day(SirketKodu).ToList();
            if (GSA.Count == 0)
                try
                {
                    GSA = db.Database.SqlQuery<GetCachedChartYear2Day_Result>(string.Format("[FINSAT6{0}].[wms].[DB_GunlukSatisAnaliziYearToDay]", SirketKodu)).ToList();
                }
                catch (Exception ex)
                {
                    Logger(ex, "Home/PartialGunlukSatisYearToDayPie");
                    GSA = new List<GetCachedChartYear2Day_Result>();
                }
            return PartialView("_PartialGunlukSatısAnaliziYearToDayPie", GSA);
        }

        public PartialViewResult PartialGunlukSatisDoubleKriter(string SirketKodu, string kod, int islemtip, int tarih)
        {
            ViewBag.IslemTip = islemtip;
            ViewBag.Kriter = kod;
            ViewBag.tarih = tarih;
            ViewBag.tarih2 = tarih.FromOADateInt();
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            if (CheckPerm(Perms.ChartGunlukSatis, PermTypes.Reading) == false) return PartialView("_PartialGunlukSatisAnaliziDoubleKriter", new List<ChartGunlukSatisAnalizi>());
            List<ChartGunlukSatisAnalizi> liste;
            try
            {
                liste = db.Database.SqlQuery<ChartGunlukSatisAnalizi>(string.Format("[FINSAT6{0}].[wms].[DB_GunlukSatisAnaliziDoubleKriter] @Tarih = {1}, @IslemTip = {2}, @Grup = '{3}'", SirketKodu, tarih, islemtip, kod)).ToList();
            }
            catch (Exception)
            {
                liste = new List<ChartGunlukSatisAnalizi>();
            }
            return PartialView("_PartialGunlukSatisAnaliziDoubleKriter", liste);
        }

        public PartialViewResult PartialGunlukSatisDoubleKriterPie(string SirketKodu, string kod, int islemtip, int tarih)
        {
            ViewBag.IslemTip = islemtip;
            ViewBag.Kriter = kod;
            ViewBag.tarih = tarih;
            ViewBag.tarih2 = tarih.FromOADateInt();
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            if (CheckPerm(Perms.ChartGunlukSatis, PermTypes.Reading) == false) return PartialView("_PartialGunlukSatisAnaliziDoubleKriterPie", new List<ChartGunlukSatisAnalizi>());
            List<ChartGunlukSatisAnalizi> liste;
            try
            {
                liste = db.Database.SqlQuery<ChartGunlukSatisAnalizi>(string.Format("[FINSAT6{0}].[wms].[DB_GunlukSatisAnaliziDoubleKriter] @Tarih = {1}, @IslemTip = {2}, @Grup = '{3}'", SirketKodu, tarih, islemtip, kod)).ToList();
            }
            catch (Exception)
            {
                liste = new List<ChartGunlukSatisAnalizi>();
            }
            return PartialView("_PartialGunlukSatisAnaliziDoubleKriterPie", liste);
        }

        public PartialViewResult PartialAylikSatisAnaliziBar(string SirketKodu)
        {
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            if (CheckPerm(Perms.ChartAylikSatisAnaliziBar, PermTypes.Reading) == false) return PartialView("_PartialAylikSatisAnaliziBar", new List<GetCachedChartMonthly_Result>());
            var ASA = db.GetCachedChartMonthly(SirketKodu).ToList();
            if (ASA.Count == 0)
                try
                {
                    ASA = db.Database.SqlQuery<GetCachedChartMonthly_Result>(string.Format("[FINSAT6{0}].[wms].[DB_Aylik_SatisAnalizi]", SirketKodu)).ToList();
                }
                catch (Exception ex)
                {
                    Logger(ex, "Home/PartialAylikSatisAnaliziBar");
                    ASA = new List<GetCachedChartMonthly_Result>();
                }
            return PartialView("_PartialAylikSatisAnaliziBar", ASA);
        }

        public PartialViewResult PartialAylikSatisCHKAnaliziBar(string SirketKodu, string chk)
        {
            ViewBag.CHK = chk;
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            if (CheckPerm(Perms.ChartAylikSatisAnaliziBar, PermTypes.Reading) == false) return PartialView("_PartialAylikSatisCHKAnaliziBar", new List<ChartAylikSatisAnalizi>());
            List<ChartAylikSatisAnalizi> liste;
            if (chk == "")
            {
                liste = new List<ChartAylikSatisAnalizi>();
                liste.Add(new ChartAylikSatisAnalizi() { Ay = "0", Yil2015 = 0, Yil2016 = 0, Yil2017 = 0 });
            }
            else
                try
                {
                    liste = db.Database.SqlQuery<ChartAylikSatisAnalizi>(string.Format("[FINSAT6{0}].[wms].[DB_Aylik_SatisAnalizi_CHK] @chk='{1}'", SirketKodu, chk)).ToList();
                }
                catch (Exception)
                {
                    liste = new List<ChartAylikSatisAnalizi>();
                    liste.Add(new ChartAylikSatisAnalizi() { Ay = "0", Yil2015 = 0, Yil2016 = 0, Yil2017 = 0 });
                }
            return PartialView("_PartialAylikSatisCHKAnaliziBar", liste);
        }
        
        public PartialViewResult PartialAylikSatisAnaliziKodTipDovizBar(string SirketKodu, string kod, int islemtip, string doviz)
        {
            ViewBag.Doviz = doviz;
            ViewBag.IslemTip = islemtip;
            ViewBag.Kriter = kod;
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            if (CheckPerm(Perms.ChartAylikSatisAnaliziBar, PermTypes.Reading) == false) return PartialView("_PartialAylikSatisAnaliziKodTipDovizBar", new List<GetCachedChartMonthlyByKriter_Result>());
            var GSADK = db.GetCachedChartMonthlyByKriter(SirketKodu, kod, doviz, islemtip.ToShort()).ToList();
            if (GSADK.Count == 0)
                try
                {
                    GSADK = db.Database.SqlQuery<GetCachedChartMonthlyByKriter_Result>(string.Format("[FINSAT6{0}].[wms].[DB_Aylik_SatisAnalizi_Tip_Kod_Doviz] @Grup = '{1}', @Kriter = '{2}', @IslemTip = {3}", SirketKodu, kod, doviz, islemtip)).ToList();
                }
                catch (Exception ex)
                {
                    Logger(ex, "Home/PartialAylikSatisAnaliziKodTipDovizBar");
                    GSADK = new List<GetCachedChartMonthlyByKriter_Result>();
                }
            return PartialView("_PartialAylikSatisAnaliziKodTipDovizBar", GSADK);
        }

        public PartialViewResult PartialUrunGrubuSatis(string SirketKodu, short tarih)
        {
            ViewBag.Tarih = tarih;
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            if (CheckPerm(Perms.ChartUrunGrubuSatis, PermTypes.Reading) == false) return PartialView("_PartialUrunGrubuSatis", new List<GetCachedChartUrunGrubu_Result>());
            var UGS = db.GetCachedChartUrunGrubu(SirketKodu, tarih).ToList();
            if (UGS.Count == 0)
                try
                {
                    UGS = db.Database.SqlQuery<GetCachedChartUrunGrubu_Result>(string.Format("[FINSAT6{0}].[wms].[DB_UrunGrubu_SatisAnalizi] @Ay = {1}", SirketKodu, tarih)).ToList();
                }
                catch (Exception)
                {
                    UGS = new List<GetCachedChartUrunGrubu_Result>();
                }
            return PartialView("_PartialUrunGrubuSatis", UGS);
        }

        public PartialViewResult PartialUrunGrubuSatisKriter(string SirketKodu, short tarih, string kriter)
        {
            ViewBag.Tarih = tarih;
            ViewBag.Kriter = kriter;
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            if (CheckPerm(Perms.ChartUrunGrubuSatis, PermTypes.Reading) == false) return PartialView("_PartialUrunGrubuSatisKriter", new List<GetCachedChartUrunGrubuKriter_Result>());
            var UGS = db.GetCachedChartUrunGrubuKriter(SirketKodu, tarih, kriter).ToList();
            if (UGS.Count == 0)
                try
                {
                    UGS = db.Database.SqlQuery<GetCachedChartUrunGrubuKriter_Result>(string.Format("[FINSAT6{0}].[wms].[DB_UrunGrubu_SatisAnalizi_Kriter] @Ay = {1}, @Kriter='{2}'", SirketKodu, tarih, kriter)).ToList();
                }
                catch (Exception)
                {
                    UGS = new List<GetCachedChartUrunGrubuKriter_Result>();
                }
            return PartialView("_PartialUrunGrubuSatisKriter", UGS);
        }

        public PartialViewResult PartialLokasyonSatis(string SirketKodu, short tarih)
        {
            ViewBag.Tarih = tarih;
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            if (CheckPerm(Perms.ChartLokasyonSatis, PermTypes.Reading) == false) return PartialView("_PartialLokasyonSatis", new List<GetCachedChartLocation_Result>());
            var UGS = db.GetCachedChartLocation(SirketKodu, tarih).ToList();
            if (UGS.Count == 0)
                try
                {
                    UGS = db.Database.SqlQuery<GetCachedChartLocation_Result>(string.Format("[FINSAT6{0}].[wms].[DB_LokasyonBazli_SatisAnalizi] @Ay = {1}", SirketKodu, tarih)).ToList();
                }
                catch (Exception)
                {
                    UGS = new List<GetCachedChartLocation_Result>();
                }
            return PartialView("_PartialLokasyonSatis", UGS);
        }

        public PartialViewResult PartialLokasyonSatisKriter(string SirketKodu, int tarih, string kriter)
        {
            ViewBag.Tarih = tarih;
            ViewBag.Kriter = kriter;
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            if (CheckPerm(Perms.ChartLokasyonSatis, PermTypes.Reading) == false) return PartialView("_PartialLokasyonSatisKriter", new List<GetCachedChartLocationKriter_Result>());
            var UGS = db.GetCachedChartLocationKriter(SirketKodu, tarih, kriter).ToList();
            if (UGS.Count == 0)
                try
                {
                    UGS = db.Database.SqlQuery<GetCachedChartLocationKriter_Result>(string.Format("[FINSAT6{0}].[wms].[DB_LokasyonBazli_SatisAnalizi_Kriter] @Ay = {1}, @Kriter='{2}'", SirketKodu, tarih, kriter)).ToList();
                }
                catch (Exception)
                {
                    UGS = new List<GetCachedChartLocationKriter_Result>();
                }
            return PartialView("_PartialLokasyonSatisKriter", UGS);
        }

        public PartialViewResult PartialBakiyeRiskAnalizi(string SirketKodu)
        {
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            if (CheckPerm(Perms.ChartBakiyeRiskAnalizi, PermTypes.Reading) == false) return PartialView("_PartialBakiyeRiskAnalizi", new List<GetCachedChartBakiyeRisk_Result>());
            var BRA = db.GetCachedChartBakiyeRisk(SirketKodu).ToList();
            if (BRA.Count == 0)
                try
                {
                    BRA = db.Database.SqlQuery<GetCachedChartBakiyeRisk_Result>(string.Format("[FINSAT6{0}].[wms].[DB_BakiyeRiskAnalizi]", SirketKodu)).ToList();
                }
                catch (Exception)
                {
                    BRA = new List<GetCachedChartBakiyeRisk_Result>();
                }
            return PartialView("_PartialBakiyeRiskAnalizi", BRA);
        }

        public PartialViewResult PartialBekleyenSiparisUrunGrubu(string SirketKodu, int bastarih, int bittarih)
        {
            ViewBag.BasTarih = bastarih;
            ViewBag.BasTarih2 = bastarih.FromOADateInt();
            ViewBag.BitTarih = bittarih;
            ViewBag.BitTarih2 = bittarih.FromOADateInt();
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            if (CheckPerm(Perms.ChartBekleyenSiparisUrunGrubu, PermTypes.Reading) == false) return PartialView("_PartialBekleyenSiparisUrunGrubu", new List<ChartBekleyenSiparisUrunGrubu>());
            List<ChartBekleyenSiparisUrunGrubu> BSUG;
            try
            {
                BSUG = db.Database.SqlQuery<ChartBekleyenSiparisUrunGrubu>(string.Format("[FINSAT6{0}].[wms].[DB_BekleyenSiparis_UrunGrubu] @BasTarih = {1}, @BitTarih = {2}", SirketKodu, bastarih, bittarih)).ToList();
            }
            catch (Exception ex)
            {
                Logger(ex, "Home/PartialBekleyenSiparisUrunGrubu");
                BSUG = new List<ChartBekleyenSiparisUrunGrubu>();
            }
            return PartialView("_PartialBekleyenSiparisUrunGrubu", BSUG);
        }

        public PartialViewResult PartialBekleyenSiparisUrunGrubuMiktar(string SirketKodu, bool miktarTutar)
        {
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
                ViewBag.MiktarTutar = "Miktar";
            if (CheckPerm(Perms.ChartBekleyenSiparisUrunGrubu, PermTypes.Reading) == false) return PartialView("_PartialBekleyenSiparisUrunGrubuMiktar", new List<GetCachedChartBekleyenUrunMiktarFiyat_Result>());
            List<GetCachedChartBekleyenUrunMiktarFiyat_Result> BSUG;
            if (miktarTutar == true)
            {
                BSUG = db.GetCachedChartBekleyenUrunMiktarFiyat(SirketKodu, true).ToList();
                ViewBag.MiktarTutar = "Miktar";
                if (BSUG.Count == 0)
                    try
                    {
                        BSUG = db.Database.SqlQuery<GetCachedChartBekleyenUrunMiktarFiyat_Result>(string.Format("[FINSAT6{0}].[wms].[DB_BekleyenSiparis_UrunGrubu_Miktar]", SirketKodu)).ToList();
                    }
                    catch (Exception)
                    {
                        BSUG = new List<GetCachedChartBekleyenUrunMiktarFiyat_Result>();
                    }
            }
            else
            {
                BSUG = db.GetCachedChartBekleyenUrunMiktarFiyat(SirketKodu, false).ToList();
                ViewBag.MiktarTutar = "Tutar";
                if (BSUG.Count == 0)
                    try
                    {
                        BSUG = db.Database.SqlQuery<GetCachedChartBekleyenUrunMiktarFiyat_Result>(string.Format("[FINSAT6{0}].[wms].[DB_BekleyenSiparis_UrunGrubu_Fiyat]", SirketKodu)).ToList();
                    }
                    catch (Exception)
                    {
                        BSUG = new List<GetCachedChartBekleyenUrunMiktarFiyat_Result>();
                    }
            }
            return PartialView("_PartialBekleyenSiparisUrunGrubuMiktar", BSUG);
        }

        public PartialViewResult PartialBekleyenSiparisUrunGrubuMiktarPie(string SirketKodu, bool miktarTutar)
        {
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
                ViewBag.MiktarTutar = "Miktar";
            if (CheckPerm(Perms.ChartBekleyenSiparisUrunGrubu, PermTypes.Reading) == false) return PartialView("_PartialBekleyenSiparisUrunGrubuMiktarPie", new List<GetCachedChartBekleyenUrunMiktarFiyat_Result>());
            List<GetCachedChartBekleyenUrunMiktarFiyat_Result> BSUG;
            if (miktarTutar == true)
            {
                BSUG = db.GetCachedChartBekleyenUrunMiktarFiyat(SirketKodu, true).ToList();
                if (BSUG.Count == 0)
                    try
                    {
                        BSUG = db.Database.SqlQuery<GetCachedChartBekleyenUrunMiktarFiyat_Result>(string.Format("[FINSAT6{0}].[wms].[DB_BekleyenSiparis_UrunGrubu_Miktar]", SirketKodu)).ToList();
                    }
                    catch (Exception)
                    {
                        BSUG = new List<GetCachedChartBekleyenUrunMiktarFiyat_Result>();
                    }
                ViewBag.MiktarTutar = "Miktar";
            }
            else
            {
                BSUG = db.GetCachedChartBekleyenUrunMiktarFiyat(SirketKodu, false).ToList();
                if (BSUG.Count == 0)
                    try
                    {
                        BSUG = db.Database.SqlQuery<GetCachedChartBekleyenUrunMiktarFiyat_Result>(string.Format("[FINSAT6{0}].[wms].[DB_BekleyenSiparis_UrunGrubu_Fiyat]", SirketKodu)).ToList();
                    }
                    catch (Exception)
                    {
                        BSUG = new List<GetCachedChartBekleyenUrunMiktarFiyat_Result>();
                    }
                ViewBag.MiktarTutar = "Tutar";
            }
            return PartialView("_PartialBekleyenSiparisUrunGrubuMiktarPie", BSUG);
        }

        public PartialViewResult PartialBekleyenSiparisUrunGrubuMiktarKriter(string SirketKodu, string kriter)
        {
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            ViewBag.MiktarTutar = "Tutar";
            ViewBag.Kriter = kriter;
            if (CheckPerm(Perms.ChartBekleyenSiparisUrunGrubu, PermTypes.Reading) == false) return PartialView("_PartialBekleyenSiparisUrunGrubuMiktarKriter", new List<GetCachedChartBekleyenUrunMiktarFiyat_Result>());
            List<GetCachedChartBekleyenUrunMiktarFiyat_Result> BSUG;
            try
            {
                BSUG = db.Database.SqlQuery<GetCachedChartBekleyenUrunMiktarFiyat_Result>(string.Format("[FINSAT6{0}].[wms].[DB_BekleyenSiparis_UrunGrubu_Fiyat_Kriter] @Kriter='{1}'", SirketKodu, kriter)).ToList();
            }
            catch (Exception)
            {
                BSUG = new List<GetCachedChartBekleyenUrunMiktarFiyat_Result>();
            }
            return PartialView("_PartialBekleyenSiparisUrunGrubuMiktarKriter", BSUG);
        }

        public PartialViewResult PartialBekleyenSiparisUrunGrubuMiktarKriterPie(string SirketKodu, string kriter)
        {
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            ViewBag.MiktarTutar = "Tutar";
            ViewBag.Kriter = kriter;
            if (CheckPerm(Perms.ChartBekleyenSiparisUrunGrubu, PermTypes.Reading) == false) return PartialView("_PartialBekleyenSiparisUrunGrubuMiktarKriterPie", new List<GetCachedChartBekleyenUrunMiktarFiyat_Result>());
            List<GetCachedChartBekleyenUrunMiktarFiyat_Result> BSUG;
            try
            {
                BSUG = db.Database.SqlQuery<GetCachedChartBekleyenUrunMiktarFiyat_Result>(string.Format("[FINSAT6{0}].[wms].[DB_BekleyenSiparis_UrunGrubu_Fiyat_Kriter] @Kriter='{1}'", SirketKodu, kriter)).ToList();
            }
            catch (Exception)
            {
                BSUG = new List<GetCachedChartBekleyenUrunMiktarFiyat_Result>();
            }
            return PartialView("_PartialBekleyenSiparisUrunGrubuMiktarKriterPie", BSUG);
        }

        public PartialViewResult PartialBekleyenSiparisMusteriAnalizi(string SirketKodu, string kod, string doviz)
        {
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            ViewBag.Doviz = doviz;
            ViewBag.Kriter = kod;
            if (CheckPerm(Perms.ChartBekleyenSiparisUrunGrubu, PermTypes.Reading) == false) return PartialView("_PartialBekleyenSiparisMusteriAnalizi", new List<ChartBekleyenSiparisUrunGrubu>());
            List<ChartBekleyenSiparisUrunGrubu> liste;
            try
            {
                liste = db.Database.SqlQuery<ChartBekleyenSiparisUrunGrubu>(string.Format("[FINSAT6{0}].[wms].[DB_BekleyenSiparis_Musteri_Analizi] @Kod = '{1}', @Kriter = '{2}'", SirketKodu, kod, doviz)).ToList();
            }
            catch (Exception)
            {
                liste = new List<ChartBekleyenSiparisUrunGrubu>();
            }
            return PartialView("_PartialBekleyenSiparisMusteriAnalizi", liste);
        }

        public PartialViewResult PartialSatisTemsilcisiAylikSatisAnalizi(string SirketKodu, string kod, short tarih)
        {
            ViewBag.Tarih = tarih;
            ViewBag.Kriter = kod;
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            if (CheckPerm(Perms.ChartSatisTemsilcisiAylikSatisAnalizi, PermTypes.Reading) == false) return PartialView("_PartialSatisTemsilcisi_AylikSatisAnalizi", new List<ChartSatisTemsilcisiAylikSatisAnalizi>());
            List<ChartSatisTemsilcisiAylikSatisAnalizi> list;
            try
            {
                list = db.Database.SqlQuery<ChartSatisTemsilcisiAylikSatisAnalizi>(string.Format("[FINSAT6{0}].[wms].[SatisTemsilcisi_AylikSatisAnalizi] @Ay = '{1}', @Kriter = '{2}'", SirketKodu, tarih, kod)).ToList();
            }
            catch (Exception)
            {
                list = new List<ChartSatisTemsilcisiAylikSatisAnalizi>();
            }
            return PartialView("_PartialSatisTemsilcisi_AylikSatisAnalizi", list);
        }

        public PartialViewResult PartialBaglantiUrunGrubu(string SirketKodu)
        {
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            if (CheckPerm(Perms.ChartBaglantiUrunGrubu, PermTypes.Reading) == false) return PartialView("_PartialBaglantiUrunGrubu", new List<GetCachedChartSatisBaglanti_Result>());
            var BUGS = db.GetCachedChartSatisBaglanti(SirketKodu).ToList();
            if (BUGS.Count == 0)
                try
                {
                    BUGS = db.Database.SqlQuery<GetCachedChartSatisBaglanti_Result>(string.Format("[FINSAT6{0}].[wms].[DB_SatisBaglanti_UrunGrubu]", SirketKodu)).ToList();
                }
                catch (Exception ex)
                {
                    Logger(ex, "Home/PartialBaglantiUrunGrubu");
                    BUGS = new List<GetCachedChartSatisBaglanti_Result>();
                }
            return PartialView("_PartialBaglantiUrunGrubu", BUGS);
        }

        public PartialViewResult PartialBaglantiUrunGrubuPie(string SirketKodu)
        {
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            if (CheckPerm(Perms.ChartBaglantiUrunGrubu, PermTypes.Reading) == false) return PartialView("_PartialBaglantiUrunGrubuPie", new List<GetCachedChartSatisBaglanti_Result>());
            var BUGS = db.GetCachedChartSatisBaglanti(SirketKodu).ToList();
            if (BUGS.Count == 0)
                try
                {
                    BUGS = db.Database.SqlQuery<GetCachedChartSatisBaglanti_Result>(string.Format("[FINSAT6{0}].[wms].[DB_SatisBaglanti_UrunGrubu]", SirketKodu)).ToList();
                }
                catch (Exception ex)
                {
                    Logger(ex, "Home/PartialBaglantiUrunGrubuPie");
                    BUGS = new List<GetCachedChartSatisBaglanti_Result>();
                }
            return PartialView("_PartialBaglantiUrunGrubuPie", BUGS);
        }

        public PartialViewResult PartialGunlukMDFUretimi(string SirketKodu, int tarih)
        {
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            ViewBag.tarih = tarih;
            ViewBag.tarih2 = tarih.FromOADateInt();
            if (CheckPerm(Perms.ChartGunlukMDFUretimi, PermTypes.Reading) == false) return PartialView("_PartialGunlukMDFUretim", new List<ChartGunlukMDFUretimi>());
            List<ChartGunlukMDFUretimi> UGS;
            try
            {
                UGS = db.Database.SqlQuery<ChartGunlukMDFUretimi>(string.Format("[FINSAT6{0}].[wms].[MDF_UretimRapor_Chart] @BasTarih = {1}, @BitTarih = {2}, @Tip={3}", SirketKodu, tarih, tarih, 1)).ToList();
            }
            catch (Exception)
            {
                UGS = new List<ChartGunlukMDFUretimi>();
            }
            return PartialView("_PartialGunlukMDFUretim", UGS);
        }

        public PartialViewResult PartialGunlukMDFUretimiPie(string SirketKodu, int tarih)
        {
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            ViewBag.tarih = tarih;
            ViewBag.tarih2 = tarih.FromOADateInt();
            if (CheckPerm(Perms.ChartGunlukMDFUretimi, PermTypes.Reading) == false) return PartialView("_PartialGunlukMDFUretimPie", new List<ChartGunlukMDFUretimi>());
            List<ChartGunlukMDFUretimi> GSA;
            try
            {
                GSA = db.Database.SqlQuery<ChartGunlukMDFUretimi>(string.Format("[FINSAT6{0}].[wms].[MDF_UretimRapor_Chart] @BasTarih = {1}, @BitTarih = {2}, @Tip={3}", SirketKodu, tarih, tarih, 1)).ToList();
            }
            catch (Exception)
            {
                GSA = new List<ChartGunlukMDFUretimi>();
            }
            return PartialView("_PartialGunlukMDFUretimPie", GSA);
        }

        public PartialViewResult PartialBaglantiZamanCizelgesi(string SirketKodu)
        {
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            if (CheckPerm(Perms.ChartBaglantiZamanCizelgesi, PermTypes.Reading) == false) return PartialView("_PartialBaglantiZamanCizelgesi", new List<ChartBaglantiZaman>());
            List<ChartBaglantiZaman> BUGS;
            try
            {
                BUGS = db.Database.SqlQuery<ChartBaglantiZaman>(string.Format("[FINSAT6{0}].[wms].[DB_BaglantiLogGetir]", SirketKodu)).ToList();
            }
            catch (Exception ex)
            {
                Logger(ex, "Home/PartialBaglantiZamanCizelgesi");
                BUGS = new List<ChartBaglantiZaman>();
            }
            return PartialView("_PartialBaglantiZamanCizelgesi", BUGS);
        }

        public PartialViewResult PartialBolgeBazliSatisAnalizi(string SirketKodu, int ay, string kriter)
        {
            ViewBag.Ay = ay;
            ViewBag.Kriter = kriter;
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            if (CheckPerm(Perms.ChartBolgeBazliSatisAnalizi, PermTypes.Reading) == false) return PartialView("_PartialBolgeBazliSatisAnalizi", new List<ChartBolgeBazliSatisAnalizi>());
            List<ChartBolgeBazliSatisAnalizi> BSUG;
            try
            {
                BSUG = db.Database.SqlQuery<ChartBolgeBazliSatisAnalizi>(string.Format("[FINSAT6{0}].[wms].[DB_Bolge_Bazinda_SatisAnalizi] @Ay = {1}, @Kriter = '{2}'", SirketKodu, ay, kriter)).ToList();
            }
            catch (Exception)
            {
                BSUG = new List<ChartBolgeBazliSatisAnalizi>();
            }
            return PartialView("_PartialBolgeBazliSatisAnalizi", BSUG);
        }

        public PartialViewResult PartialBolgeBazliSatisAnaliziPie(string SirketKodu, int ay, string kriter)
        {
            ViewBag.Ay = ay;
            ViewBag.Kriter = kriter;
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            if (CheckPerm(Perms.ChartBolgeBazliSatisAnalizi, PermTypes.Reading) == false) return PartialView("_PartialBolgeBazliSatisAnaliziPie", new List<ChartBolgeBazliSatisAnalizi>());
            List<ChartBolgeBazliSatisAnalizi> BSUG;
            try
            {
                BSUG = db.Database.SqlQuery<ChartBolgeBazliSatisAnalizi>(string.Format("[FINSAT6{0}].[wms].[DB_Bolge_Bazinda_SatisAnalizi] @Ay = {1}, @Kriter = '{2}'", SirketKodu, ay, kriter)).ToList();
            }
            catch (Exception)
            {
                BSUG = new List<ChartBolgeBazliSatisAnalizi>();
            }
            return PartialView("_PartialBolgeBazliSatisAnaliziPie", BSUG);
        }

        /// <summary>
        /// xrtas
        /// </summary>
        public string CHKSelect(string SirketKodu)
        {
            var CHK = db.Database.SqlQuery<RaporCHKSelect>(string.Format("[FINSAT6{0}].[wms].[CHKSelectKartTip]", SirketKodu)).ToList();
            var json = new JavaScriptSerializer().Serialize(CHK);
            return json;
        }
        public string BolgeBazliSatisAnaliziKriter(string SirketKodu)
        {
            var Kriter = db.Database.SqlQuery<ChartBolgeBazliSatisAnaliziKriter>(string.Format("[FINSAT6{0}].[wms].[BolgeBazliSatisAnaliziKriterSelect]", SirketKodu)).ToList();
            var json = new JavaScriptSerializer().Serialize(Kriter);
            return json;
        }
        public string GunlukSatisAnaliziKriterSelect(string SirketKodu)
        {
            var Kriter = db.Database.SqlQuery<ChartBolgeBazliSatisAnaliziKriter>(string.Format("[FINSAT6{0}].[wms].[GunlukSatisAnaliziKriterSelect]", SirketKodu)).ToList();
            var json = new JavaScriptSerializer().Serialize(Kriter);
            return json;
        }
        public string Connection()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString;
        }
    }
}