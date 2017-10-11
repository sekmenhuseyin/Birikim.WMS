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
            var tbl = db.GetHomeSummary(vUser.UserName, vUser.Id).FirstOrDefault();
            //Bekleyen Onaylar
            if (setts.OnayCek == true || setts.OnayFiyat == true || setts.OnayRisk == true || setts.OnaySiparis == true || setts.OnaySozlesme == true || setts.OnayStok == true || setts.OnayTekno == true || setts.OnayTeminat == true)
                try
                {
                    var tmp = new Charts();
                    bo = tmp.BekleyenOnaylar(SirketKodu, tbl.yetki.Contains("'DashboardKasa'"));
                }
                catch (Exception ex)
                {
                    Logger(ex, "Home/Index");
                }
            //return
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.BekleyenOnaylar = bo;
            ViewBag.RoleName = vUser.RoleName;
            return View("Index", tbl);
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
        #region Satış Raporları
        public PartialViewResult PartialGunlukSatisZamanCizelgesi(string SirketKodu)
        {
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            if (CheckPerm(Perms.ChartGunlukSatis, PermTypes.Reading) == false) return PartialView("Satis/GunlukSatisZamanCizelgesi", new List<ChartBaglantiZaman>());
            List<ChartBaglantiZaman> liste;
            try
            {
                var tmp = new Charts();
                liste = tmp.ChartBaglantiZaman(SirketKodu);
            }
            catch (Exception ex)
            {
                Logger(ex, "Home/PartialGunlukSatisZamanCizelgesi");
                liste = new List<ChartBaglantiZaman>();
            }
            return PartialView("Satis/GunlukSatisZamanCizelgesi", liste);
        }
        public PartialViewResult PartialGunlukSatis(string SirketKodu, int tarih)
        {
            ViewBag.tarih = tarih;
            ViewBag.tarih2 = tarih.FromOADateInt();
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            if (CheckPerm(Perms.ChartGunlukSatis, PermTypes.Reading) == false) return PartialView("Satis/GunlukSatis", new List<ChartGunlukSatisAnalizi>());
            List<ChartGunlukSatisAnalizi> liste;
            try
            {
                var tmp = new Charts();
                liste = tmp.ChartGunlukSatisAnalizi(SirketKodu, tarih);
            }
            catch (Exception)
            {
                liste = new List<ChartGunlukSatisAnalizi>();
            }
            return PartialView("Satis/GunlukSatis", liste);
        }
        public PartialViewResult PartialGunlukSatisPie(string SirketKodu, int tarih)
        {
            ViewBag.tarih = tarih;
            ViewBag.tarih2 = tarih.FromOADateInt();
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            if (CheckPerm(Perms.ChartGunlukSatis, PermTypes.Reading) == false) return PartialView("Satis/GunlukSatisPie", new List<ChartGunlukSatisAnalizi>());
            List<ChartGunlukSatisAnalizi> liste;
            try
            {
                var tmp = new Charts();
                liste = tmp.ChartGunlukSatisAnalizi(SirketKodu, tarih);
            }
            catch (Exception)
            {
                liste = new List<ChartGunlukSatisAnalizi>();
            }
            return PartialView("Satis/GunlukSatisPie", liste);
        }
        public PartialViewResult PartialGunlukSatisYearToDay(string SirketKodu)
        {
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            if (CheckPerm(Perms.ChartGunlukSatis, PermTypes.Reading) == false) return PartialView("Satis/GunlukSatısAnaliziYearToDay", new List<GetCachedChartYear2Day_Result>());
            var liste = db.GetCachedChartYear2Day(SirketKodu).ToList();
            if (liste.Count == 0)
                try
                {
                    var tmp = new Charts();
                    liste = tmp.GetCachedChartYear2Day_Result(SirketKodu);
                }
                catch (Exception ex)
                {
                    Logger(ex, "Home/ChartGunlukSatisYearToDay");
                    liste = new List<GetCachedChartYear2Day_Result>();
                }
            return PartialView("Satis/GunlukSatısAnaliziYearToDay", liste);
        }
        public PartialViewResult PartialGunlukSatisYearToDayPie(string SirketKodu)
        {
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            if (CheckPerm(Perms.ChartGunlukSatis, PermTypes.Reading) == false) return PartialView("PartialGunlukSatisYearToDayPie", new List<GetCachedChartYear2Day_Result>());
            var liste = db.GetCachedChartYear2Day(SirketKodu).ToList();
            if (liste.Count == 0)
                try
                {
                    var tmp = new Charts();
                    liste = tmp.GetCachedChartYear2Day_Result(SirketKodu);
                }
                catch (Exception ex)
                {
                    Logger(ex, "Home/PartialGunlukSatisYearToDayPie");
                    liste = new List<GetCachedChartYear2Day_Result>();
                }
            return PartialView("Satis/GunlukSatısAnaliziYearToDayPie", liste);
        }
        public PartialViewResult PartialGunlukSatisDoubleKriter(string SirketKodu, string kod, int islemtip, int tarih)
        {
            ViewBag.IslemTip = islemtip;
            ViewBag.Kriter = kod;
            ViewBag.tarih = tarih;
            ViewBag.tarih2 = tarih.FromOADateInt();
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            if (CheckPerm(Perms.ChartGunlukSatis, PermTypes.Reading) == false) return PartialView("Satis/GunlukSatisAnaliziDoubleKriter", new List<ChartGunlukSatisAnalizi>());
            List<ChartGunlukSatisAnalizi> liste;
            try
            {
                var tmp = new Charts();
                liste = tmp.ChartGunlukSatisAnalizi(SirketKodu, kod, islemtip, tarih);
            }
            catch (Exception)
            {
                liste = new List<ChartGunlukSatisAnalizi>();
            }
            return PartialView("Satis/GunlukSatisAnaliziDoubleKriter", liste);
        }
        public PartialViewResult PartialGunlukSatisDoubleKriterPie(string SirketKodu, string kod, int islemtip, int tarih)
        {
            ViewBag.IslemTip = islemtip;
            ViewBag.Kriter = kod;
            ViewBag.tarih = tarih;
            ViewBag.tarih2 = tarih.FromOADateInt();
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            if (CheckPerm(Perms.ChartGunlukSatis, PermTypes.Reading) == false) return PartialView("Satis/GunlukSatisAnaliziDoubleKriterPie", new List<ChartGunlukSatisAnalizi>());
            List<ChartGunlukSatisAnalizi> liste;
            try
            {
                var tmp = new Charts();
                liste = tmp.ChartGunlukSatisAnalizi(SirketKodu, kod, islemtip, tarih);
            }
            catch (Exception)
            {
                liste = new List<ChartGunlukSatisAnalizi>();
            }
            return PartialView("Satis/GunlukSatisAnaliziDoubleKriterPie", liste);
        }

        public PartialViewResult PartialAylikSatisAnaliziBar(string SirketKodu)
        {
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            if (CheckPerm(Perms.ChartAylikSatisAnaliziBar, PermTypes.Reading) == false) return PartialView("Satis/AylikSatisAnaliziBar", new List<GetCachedChartMonthly_Result>());
            var liste = db.GetCachedChartMonthly(SirketKodu).ToList();
            if (liste.Count == 0)
                try
                {
                    var tmp = new Charts();
                    liste = tmp.GetCachedChartMonthly_Result(SirketKodu);
                }
                catch (Exception ex)
                {
                    Logger(ex, "Home/PartialAylikSatisAnaliziBar");
                    liste = new List<GetCachedChartMonthly_Result>();
                }
            return PartialView("Satis/AylikSatisAnaliziBar", liste);
        }
        public PartialViewResult PartialAylikSatisCHKAnaliziBar(string SirketKodu, string chk)
        {
            ViewBag.CHK = chk;
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            if (CheckPerm(Perms.ChartAylikSatisAnaliziBar, PermTypes.Reading) == false) return PartialView("Satis/AylikSatisCHKAnaliziBar", new List<ChartAylikSatisAnalizi>());
            List<ChartAylikSatisAnalizi> liste;
            if (chk == "")
            {
                liste = new List<ChartAylikSatisAnalizi>
                {
                    new ChartAylikSatisAnalizi() { Ay = "0", Yil2015 = 0, Yil2016 = 0, Yil2017 = 0 }
                };
            }
            else
                try
                {
                    var tmp = new Charts();
                    liste = tmp.ChartAylikSatisAnalizi(SirketKodu, chk);
                }
                catch (Exception)
                {
                    liste = new List<ChartAylikSatisAnalizi>
                    {
                        new ChartAylikSatisAnalizi() { Ay = "0", Yil2015 = 0, Yil2016 = 0, Yil2017 = 0 }
                    };
                }
            return PartialView("Satis/AylikSatisCHKAnaliziBar", liste);
        }
        public PartialViewResult PartialAylikSatisAnaliziKodTipDovizBar(string SirketKodu, string kod, int islemtip, string doviz)
        {
            ViewBag.Doviz = doviz;
            ViewBag.IslemTip = islemtip;
            ViewBag.Kriter = kod;
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            if (CheckPerm(Perms.ChartAylikSatisAnaliziBar, PermTypes.Reading) == false) return PartialView("Satis/AylikSatisAnaliziKodTipDovizBar", new List<GetCachedChartMonthlyByKriter_Result>());
            var liste = db.GetCachedChartMonthlyByKriter(SirketKodu, kod, doviz, islemtip.ToShort()).ToList();
            if (liste.Count == 0)
                try
                {
                    var tmp = new Charts();
                    liste = tmp.GetCachedChartMonthlyByKriter_Result(SirketKodu, kod, islemtip, doviz);
                }
                catch (Exception ex)
                {
                    Logger(ex, "Home/PartialAylikSatisAnaliziKodTipDovizBar");
                    liste = new List<GetCachedChartMonthlyByKriter_Result>();
                }
            return PartialView("Satis/AylikSatisAnaliziKodTipDovizBar", liste);
        }

        public PartialViewResult PartialUrunGrubuSatis(string SirketKodu, short tarih)
        {
            ViewBag.Tarih = tarih;
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            if (CheckPerm(Perms.ChartUrunGrubuSatis, PermTypes.Reading) == false) return PartialView("Satis/UrunGrubuSatis", new List<GetCachedChartUrunGrubu_Result>());
            var liste = db.GetCachedChartUrunGrubu(SirketKodu, tarih).ToList();
            if (liste.Count == 0)
                try
                {
                    var tmp = new Charts();
                    liste = tmp.GetCachedChartUrunGrubu_Result(SirketKodu, tarih);
                }
                catch (Exception)
                {
                    liste = new List<GetCachedChartUrunGrubu_Result>();
                }
            return PartialView("Satis/UrunGrubuSatis", liste);
        }
        public PartialViewResult PartialUrunGrubuSatisKriter(string SirketKodu, short tarih, string kriter)
        {
            ViewBag.Tarih = tarih;
            ViewBag.Kriter = kriter;
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            if (CheckPerm(Perms.ChartUrunGrubuSatis, PermTypes.Reading) == false) return PartialView("Satis/UrunGrubuSatisKriter", new List<GetCachedChartUrunGrubuKriter_Result>());
            var liste = db.GetCachedChartUrunGrubuKriter(SirketKodu, tarih, kriter).ToList();
            if (liste.Count == 0)
                try
                {
                    var tmp = new Charts();
                    liste = tmp.GetCachedChartUrunGrubuKriter_Result(SirketKodu, tarih, kriter);
                }
                catch (Exception)
                {
                    liste = new List<GetCachedChartUrunGrubuKriter_Result>();
                }
            return PartialView("Satis/UrunGrubuSatisKriter", liste);
        }

        public PartialViewResult PartialLokasyonSatis(string SirketKodu, short tarih)
        {
            ViewBag.Tarih = tarih;
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            if (CheckPerm(Perms.ChartLokasyonSatis, PermTypes.Reading) == false) return PartialView("Satis/LokasyonSatis", new List<GetCachedChartLocation_Result>());
            var liste = db.GetCachedChartLocation(SirketKodu, tarih).ToList();
            if (liste.Count == 0)
                try
                {
                    var tmp = new Charts();
                    liste = tmp.GetCachedChartLocation_Result(SirketKodu, tarih);
                }
                catch (Exception)
                {
                    liste = new List<GetCachedChartLocation_Result>();
                }
            return PartialView("Satis/LokasyonSatis", liste);
        }
        public PartialViewResult PartialLokasyonSatisKriter(string SirketKodu, int tarih, string kriter)
        {
            ViewBag.Tarih = tarih;
            ViewBag.Kriter = kriter;
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            if (CheckPerm(Perms.ChartLokasyonSatis, PermTypes.Reading) == false) return PartialView("Satis/LokasyonSatisKriter", new List<GetCachedChartLocationKriter_Result>());
            var liste = db.GetCachedChartLocationKriter(SirketKodu, tarih, kriter).ToList();
            if (liste.Count == 0)
                try
                {
                    var tmp = new Charts();
                    liste = tmp.GetCachedChartLocationKriter_Result(SirketKodu, tarih, kriter);
                }
                catch (Exception)
                {
                    liste = new List<GetCachedChartLocationKriter_Result>();
                }
            return PartialView("Satis/LokasyonSatisKriter", liste);
        }

        public PartialViewResult PartialBekleyenSiparisUrunGrubu(string SirketKodu, int bastarih, int bittarih)
        {
            ViewBag.BasTarih = bastarih;
            ViewBag.BasTarih2 = bastarih.FromOADateInt();
            ViewBag.BitTarih = bittarih;
            ViewBag.BitTarih2 = bittarih.FromOADateInt();
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            if (CheckPerm(Perms.ChartBekleyenSiparisUrunGrubu, PermTypes.Reading) == false) return PartialView("Satis/BekleyenSiparisUrunGrubu", new List<ChartBekleyenSiparisUrunGrubu>());
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
            return PartialView("Satis/BekleyenSiparisUrunGrubu", BSUG);
        }
        public PartialViewResult PartialBekleyenSiparisUrunGrubuMiktar(string SirketKodu, bool miktarTutar)
        {
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            ViewBag.MiktarTutar = "Miktar";
            if (CheckPerm(Perms.ChartBekleyenSiparisUrunGrubu, PermTypes.Reading) == false) return PartialView("Satis/BekleyenSiparisUrunGrubuMiktar", new List<GetCachedChartBekleyenUrunMiktarFiyat_Result>());
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
            return PartialView("Satis/BekleyenSiparisUrunGrubuMiktar", BSUG);
        }
        public PartialViewResult PartialBekleyenSiparisUrunGrubuMiktarPie(string SirketKodu, bool miktarTutar)
        {
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            ViewBag.MiktarTutar = "Miktar";
            if (CheckPerm(Perms.ChartBekleyenSiparisUrunGrubu, PermTypes.Reading) == false) return PartialView("Satis/BekleyenSiparisUrunGrubuMiktarPie", new List<GetCachedChartBekleyenUrunMiktarFiyat_Result>());
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
            return PartialView("Satis/BekleyenSiparisUrunGrubuMiktarPie", BSUG);
        }
        public PartialViewResult PartialBekleyenSiparisUrunGrubuMiktarKriter(string SirketKodu, string kriter)
        {
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            ViewBag.MiktarTutar = "Tutar";
            ViewBag.Kriter = kriter;
            if (CheckPerm(Perms.ChartBekleyenSiparisUrunGrubu, PermTypes.Reading) == false) return PartialView("Satis/BekleyenSiparisUrunGrubuMiktarKriter", new List<GetCachedChartBekleyenUrunMiktarFiyat_Result>());
            List<GetCachedChartBekleyenUrunMiktarFiyat_Result> BSUG;
            try
            {
                BSUG = db.Database.SqlQuery<GetCachedChartBekleyenUrunMiktarFiyat_Result>(string.Format("[FINSAT6{0}].[wms].[DB_BekleyenSiparis_UrunGrubu_Fiyat_Kriter] @Kriter='{1}'", SirketKodu, kriter)).ToList();
            }
            catch (Exception)
            {
                BSUG = new List<GetCachedChartBekleyenUrunMiktarFiyat_Result>();
            }
            return PartialView("Satis/BekleyenSiparisUrunGrubuMiktarKriter", BSUG);
        }
        public PartialViewResult PartialBekleyenSiparisUrunGrubuMiktarKriterPie(string SirketKodu, string kriter)
        {
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            ViewBag.MiktarTutar = "Tutar";
            ViewBag.Kriter = kriter;
            if (CheckPerm(Perms.ChartBekleyenSiparisUrunGrubu, PermTypes.Reading) == false) return PartialView("Satis/BekleyenSiparisUrunGrubuMiktarKriterPie", new List<GetCachedChartBekleyenUrunMiktarFiyat_Result>());
            List<GetCachedChartBekleyenUrunMiktarFiyat_Result> BSUG;
            try
            {
                BSUG = db.Database.SqlQuery<GetCachedChartBekleyenUrunMiktarFiyat_Result>(string.Format("[FINSAT6{0}].[wms].[DB_BekleyenSiparis_UrunGrubu_Fiyat_Kriter] @Kriter='{1}'", SirketKodu, kriter)).ToList();
            }
            catch (Exception)
            {
                BSUG = new List<GetCachedChartBekleyenUrunMiktarFiyat_Result>();
            }
            return PartialView("Satis/BekleyenSiparisUrunGrubuMiktarKriterPie", BSUG);
        }
        public PartialViewResult PartialBekleyenSiparisMusteriAnalizi(string SirketKodu, string kod, string doviz)
        {
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            ViewBag.Doviz = doviz;
            ViewBag.Kriter = kod;
            if (CheckPerm(Perms.ChartBekleyenSiparisUrunGrubu, PermTypes.Reading) == false) return PartialView("Satis/BekleyenSiparisMusteriAnalizi", new List<ChartBekleyenSiparisUrunGrubu>());
            List<ChartBekleyenSiparisUrunGrubu> liste;
            try
            {
                liste = db.Database.SqlQuery<ChartBekleyenSiparisUrunGrubu>(string.Format("[FINSAT6{0}].[wms].[DB_BekleyenSiparis_Musteri_Analizi] @Kod = '{1}', @Kriter = '{2}'", SirketKodu, kod, doviz)).ToList();
            }
            catch (Exception)
            {
                liste = new List<ChartBekleyenSiparisUrunGrubu>();
            }
            return PartialView("Satis/BekleyenSiparisMusteriAnalizi", liste);
        }

        public PartialViewResult PartialBaglantiUrunGrubu(string SirketKodu)
        {
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            if (CheckPerm(Perms.ChartBaglantiUrunGrubu, PermTypes.Reading) == false) return PartialView("Satis/BaglantiUrunGrubu", new List<GetCachedChartSatisBaglanti_Result>());
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
            return PartialView("Satis/BaglantiUrunGrubu", BUGS);
        }
        public PartialViewResult PartialBaglantiUrunGrubuPie(string SirketKodu)
        {
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            if (CheckPerm(Perms.ChartBaglantiUrunGrubu, PermTypes.Reading) == false) return PartialView("Satis/BaglantiUrunGrubuPie", new List<GetCachedChartSatisBaglanti_Result>());
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
            return PartialView("Satis/BaglantiUrunGrubuPie", BUGS);
        }

        public PartialViewResult PartialGunlukMDFUretimi(string SirketKodu, int tarih)
        {
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            ViewBag.tarih = tarih;
            ViewBag.tarih2 = tarih.FromOADateInt();
            if (CheckPerm(Perms.ChartGunlukMDFUretimi, PermTypes.Reading) == false) return PartialView("Satis/GunlukMDFUretim", new List<ChartGunlukMDFUretimi>());
            List<ChartGunlukMDFUretimi> UGS;
            try
            {
                UGS = db.Database.SqlQuery<ChartGunlukMDFUretimi>(string.Format("[FINSAT6{0}].[wms].[MDF_UretimRapor_Chart] @BasTarih = {1}, @BitTarih = {2}, @Tip={3}", SirketKodu, tarih, tarih, 1)).ToList();
            }
            catch (Exception)
            {
                UGS = new List<ChartGunlukMDFUretimi>();
            }
            return PartialView("Satis/GunlukMDFUretim", UGS);
        }
        public PartialViewResult PartialGunlukMDFUretimiPie(string SirketKodu, int tarih)
        {
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            ViewBag.tarih = tarih;
            ViewBag.tarih2 = tarih.FromOADateInt();
            if (CheckPerm(Perms.ChartGunlukMDFUretimi, PermTypes.Reading) == false) return PartialView("Satis/GunlukMDFUretimPie", new List<ChartGunlukMDFUretimi>());
            List<ChartGunlukMDFUretimi> GSA;
            try
            {
                GSA = db.Database.SqlQuery<ChartGunlukMDFUretimi>(string.Format("[FINSAT6{0}].[wms].[MDF_UretimRapor_Chart] @BasTarih = {1}, @BitTarih = {2}, @Tip={3}", SirketKodu, tarih, tarih, 1)).ToList();
            }
            catch (Exception)
            {
                GSA = new List<ChartGunlukMDFUretimi>();
            }
            return PartialView("Satis/GunlukMDFUretimPie", GSA);
        }


        public PartialViewResult PartialBolgeBazliSatisAnalizi(string SirketKodu, int ay, string kriter)
        {
            ViewBag.Ay = ay;
            ViewBag.Kriter = kriter;
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            if (CheckPerm(Perms.ChartBolgeBazliSatisAnalizi, PermTypes.Reading) == false) return PartialView("Satis/BolgeBazliSatisAnalizi", new List<ChartBolgeBazliSatisAnalizi>());
            List<ChartBolgeBazliSatisAnalizi> BSUG;
            try
            {
                BSUG = db.Database.SqlQuery<ChartBolgeBazliSatisAnalizi>(string.Format("[FINSAT6{0}].[wms].[DB_Bolge_Bazinda_SatisAnalizi] @Ay = {1}, @Kriter = '{2}'", SirketKodu, ay, kriter)).ToList();
            }
            catch (Exception)
            {
                BSUG = new List<ChartBolgeBazliSatisAnalizi>();
            }
            return PartialView("Satis/BolgeBazliSatisAnalizi", BSUG);
        }
        public PartialViewResult PartialBolgeBazliSatisAnaliziPie(string SirketKodu, int ay, string kriter)
        {
            ViewBag.Ay = ay;
            ViewBag.Kriter = kriter;
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            if (CheckPerm(Perms.ChartBolgeBazliSatisAnalizi, PermTypes.Reading) == false) return PartialView("Satis/BolgeBazliSatisAnaliziPie", new List<ChartBolgeBazliSatisAnalizi>());
            List<ChartBolgeBazliSatisAnalizi> BSUG;
            try
            {
                BSUG = db.Database.SqlQuery<ChartBolgeBazliSatisAnalizi>(string.Format("[FINSAT6{0}].[wms].[DB_Bolge_Bazinda_SatisAnalizi] @Ay = {1}, @Kriter = '{2}'", SirketKodu, ay, kriter)).ToList();
            }
            catch (Exception)
            {
                BSUG = new List<ChartBolgeBazliSatisAnalizi>();
            }
            return PartialView("Satis/BolgeBazliSatisAnaliziPie", BSUG);
        }

        public PartialViewResult PartialBaglantiZamanCizelgesi(string SirketKodu)
        {
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            if (CheckPerm(Perms.ChartBaglantiZamanCizelgesi, PermTypes.Reading) == false) return PartialView("Satis/BaglantiZamanCizelgesi", new List<ChartBaglantiZaman>());
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
            return PartialView("Satis/BaglantiZamanCizelgesi", BUGS);
        }
        public PartialViewResult PartialBakiyeRiskAnalizi(string SirketKodu)
        {
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            if (CheckPerm(Perms.ChartBakiyeRiskAnalizi, PermTypes.Reading) == false) return PartialView("Satis/BakiyeRiskAnalizi", new List<GetCachedChartBakiyeRisk_Result>());
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
            return PartialView("Satis/BakiyeRiskAnalizi", BRA);
        }
        public PartialViewResult PartialSatisTemsilcisiAylikSatisAnalizi(string SirketKodu, string kod, short tarih)
        {
            ViewBag.Tarih = tarih;
            ViewBag.Kriter = kod;
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            if (CheckPerm(Perms.ChartSatisTemsilcisiAylikSatisAnalizi, PermTypes.Reading) == false) return PartialView("Satis/SatisTemsilcisi_AylikSatisAnalizi", new List<ChartSatisTemsilcisiAylikSatisAnalizi>());
            List<ChartSatisTemsilcisiAylikSatisAnalizi> list;
            try
            {
                list = db.Database.SqlQuery<ChartSatisTemsilcisiAylikSatisAnalizi>(string.Format("[FINSAT6{0}].[wms].[SatisTemsilcisi_AylikSatisAnalizi] @Ay = '{1}', @Kriter = '{2}'", SirketKodu, tarih, kod)).ToList();
            }
            catch (Exception)
            {
                list = new List<ChartSatisTemsilcisiAylikSatisAnalizi>();
            }
            return PartialView("Satis/SatisTemsilcisi_AylikSatisAnalizi", list);
        }
        /// <summary>
        /// xtras
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
        #endregion
        #region Görev Raporları
        public PartialViewResult GorevGunlukCalisma(string user, int tarih, string tip)
        {
            ViewBag.tarih = tarih;
            ViewBag.tarih2 = tarih.FromOADateInt();
            ViewBag.user = user;
            ViewBag.userID = new SelectList(Persons.GetList(), "Kod", "AdSoyad", user);
            var liste = db.Database.SqlQuery<chartGorevCalisma>(string.Format(@"SELECT ong.ProjeForm.Proje, SUM(ong.GorevlerCalisma.Sure) AS Sure
                    FROM ong.GorevlerCalisma INNER JOIN ong.Gorevler ON ong.GorevlerCalisma.GorevID = ong.Gorevler.ID INNER JOIN ong.ProjeForm ON ong.Gorevler.ProjeFormID = ong.ProjeForm.ID
                    WHERE (ong.GorevlerCalisma.Tarih = '{0}') AND (case when '{1}' <> '' then case when (ong.GorevlerCalisma.Kaydeden = '{1}') then 1 else 0 end else 1 end = 1) 
                    GROUP BY ong.ProjeForm.Proje", tarih.FromOaDate().ToString("yyyy-MM-dd"), user)).ToList();
            if (tip == "Pie")
                return PartialView("GorevProjesi/GunlukCalismaPie", liste);
            else
                return PartialView("GorevProjesi/GunlukCalisma", liste);
        }
        public PartialViewResult GorevMusteriAnalizi(int musteri, int proje, int tarihStart, int tarihEnd, string tip)
        {
            ViewBag.tarihStart = tarihStart;
            ViewBag.tarihStart2 = tarihStart.FromOADateInt();
            ViewBag.tarihEnd = tarihEnd;
            ViewBag.tarihEnd2 = tarihEnd.FromOADateInt();
            ViewBag.musteri = musteri;
            ViewBag.proje = proje;
            ViewBag.MusteriID = new SelectList(db.Musteris.OrderBy(m => m.Unvan).ToList(), "ID", "Unvan", musteri);
            if (musteri == 0)
                ViewBag.ProjeID = new SelectList(new List<ProjeForm>(), "ID", "Proje");
            else
                ViewBag.ProjeID = new SelectList(db.ProjeForms.Where(m => m.MusteriID == musteri && m.PID == null).OrderBy(m => m.Proje).ToList(), "ID", "Proje", proje);
            string sql = "";
            if (musteri > 0) sql += " AND ong.ProjeForm.MusteriID = " + musteri;
            if (proje > 0) sql += " AND ong.ProjeForm.PID = " + proje;
            var liste = db.Database.SqlQuery<chartGorevCalisma>(string.Format(@"SELECT ong.GorevlerCalisma.Kaydeden AS Proje, SUM(ong.GorevlerCalisma.Sure) AS Sure
                    FROM ong.GorevlerCalisma INNER JOIN ong.Gorevler ON ong.GorevlerCalisma.GorevID = ong.Gorevler.ID INNER JOIN ong.ProjeForm ON ong.Gorevler.ProjeFormID = ong.ProjeForm.ID
                    WHERE (ong.GorevlerCalisma.Tarih >= '{0}') AND (ong.GorevlerCalisma.Tarih <= '{1}'){2}
                    GROUP BY ong.GorevlerCalisma.Kaydeden", tarihStart.FromOaDate().ToString("yyyy-MM-dd"), tarihEnd.FromOaDate().ToString("yyyy-MM-dd"), sql)).ToList();
            if (tip == "Pie")
                return PartialView("GorevProjesi/MusteriAnaliziPie", liste);
            else
                return PartialView("GorevProjesi/MusteriAnalizi", liste);
        }
        public PartialViewResult GorevCalismaAnalizi(int tarihStart, int tarihEnd)
        {
            ViewBag.tarihStart = tarihStart;
            ViewBag.tarihStart2 = tarihStart.FromOADateInt();
            ViewBag.tarihEnd = tarihEnd;
            ViewBag.tarihEnd2 = tarihEnd.FromOADateInt();
            var liste = db.Database.SqlQuery<chartGorevCalismaAnaliz>(string.Format(@"SELECT ong.Musteri.Unvan, ong.ProjeForm.Proje, ong.Gorevler.Gorev, ong.GorevlerToDoList.Aciklama, ong.GorevlerCalisma.Kaydeden, SUM(ong.GorevlerCalisma.Sure) AS Sure, ong.GorevlerCalisma.Tarih
                    FROM ong.GorevlerCalisma INNER JOIN ong.Gorevler ON ong.GorevlerCalisma.GorevID = ong.Gorevler.ID INNER JOIN ong.ProjeForm ON ong.Gorevler.ProjeFormID = ong.ProjeForm.ID INNER JOIN ong.Musteri ON ong.ProjeForm.MusteriID = ong.Musteri.ID INNER JOIN ong.GorevlerToDoList ON ong.Gorevler.ID = ong.GorevlerToDoList.GorevID AND ong.GorevlerCalisma.Calisma = ong.GorevlerToDoList.Aciklama
                    WHERE (ong.GorevlerCalisma.Tarih >= '{0}') AND (ong.GorevlerCalisma.Tarih <= '{1}')
                    GROUP BY ong.GorevlerCalisma.Kaydeden, ong.Musteri.Unvan, ong.ProjeForm.Proje, ong.Gorevler.Gorev, ong.GorevlerToDoList.Aciklama, ong.GorevlerCalisma.Tarih", tarihStart.FromOaDate().ToString("yyyy-MM-dd"), tarihEnd.FromOaDate().ToString("yyyy-MM-dd"))).ToList();
            return PartialView("GorevProjesi/CalismaAnalizi", liste);
        }
        public PartialViewResult GorevProjeAnalizi(int musteri, int proje)
        {
            ViewBag.musteri = musteri;
            ViewBag.proje = proje;
            ViewBag.MusteriID = new SelectList(db.Musteris.OrderBy(m => m.Unvan).ToList(), "ID", "Unvan", musteri);
            if (musteri == 0)
                ViewBag.ProjeID = new SelectList(new List<ProjeForm>(), "ID", "Proje");
            else
                ViewBag.ProjeID = new SelectList(db.ProjeForms.Where(m => m.MusteriID == musteri && m.PID == null).OrderBy(m => m.Proje).ToList(), "ID", "Proje", proje);
            string sql = "";
            if (musteri > 0) sql += "AND ong.ProjeForm.MusteriID = " + musteri;
            if (proje > 0) sql += "AND ong.ProjeForm.PID = " + proje;
            //listeyi getir
            var liste = db.Database.SqlQuery<chartGorevProje1>(string.Format(@"SELECT YEAR(ong.GorevlerCalisma.Tarih) AS Yil, MONTH(ong.GorevlerCalisma.Tarih) AS Ay, SUM(ong.GorevlerCalisma.Sure) AS Sure
                        FROM ong.GorevlerCalisma INNER JOIN ong.Gorevler ON ong.GorevlerCalisma.GorevID = ong.Gorevler.ID INNER JOIN ong.ProjeForm ON ong.Gorevler.ProjeFormID = ong.ProjeForm.ID
                        WHERE        (ong.GorevlerCalisma.ID > 0) {1}
                        GROUP BY YEAR(ong.GorevlerCalisma.Tarih), MONTH(ong.GorevlerCalisma.Tarih)
                        HAVING        (YEAR(ong.GorevlerCalisma.Tarih) > {0})
                        ORDER BY Yil, Ay", DateTime.Today.Year - 2, sql)).ToList();
            //yeni liste oluştur
            var sonliste = new List<chartGorevProje>();
            for (int i = 0; i < 12; i++)
            {
                sonliste.Add(new chartGorevProje() { Ay = ((Aylar)i + 1).ToString(), GecenYil = 0, BuYil = 0 });
            }
            //ilk listeyi yeni listeye yaz
            foreach (var item in liste)
            {
                if (item.Yil == DateTime.Today.Year - 1)
                    sonliste[item.Ay - 1].GecenYil = item.Sure;
                else
                    sonliste[item.Ay - 1].BuYil = item.Sure;
            }
            return PartialView("GorevProjesi/ProjeAnalizi", sonliste);
        }
        public PartialViewResult GorevAylikCalisma(string user, int tarihStart, string tip)
        {
            ViewBag.user = user;
            ViewBag.tarihStart = tarihStart;
            ViewBag.userID = new SelectList(Persons.GetList(), "Kod", "AdSoyad", user);
            ViewBag.gorevcalismatarih7 = EnumHelperExtension.SelectListFor((Aylar)tarihStart);
            var yil = DateTime.Today.Year;
            if (tarihStart > DateTime.Today.Month) yil--;
            string sql = "";
            if (user != "") sql += "AND ong.GorevlerCalisma.Kaydeden = '" + user + "'";
            var liste = db.Database.SqlQuery<chartGorevCalisma>(string.Format(@"SELECT ong.ProjeForm.Proje, SUM(ong.GorevlerCalisma.Sure) AS Sure
                    FROM ong.GorevlerCalisma INNER JOIN ong.Gorevler ON ong.GorevlerCalisma.GorevID = ong.Gorevler.ID INNER JOIN ong.ProjeForm ON ong.Gorevler.ProjeFormID = ong.ProjeForm.ID
                    WHERE (ong.GorevlerCalisma.Tarih >= '{0}') AND (ong.GorevlerCalisma.Tarih < '{1}') {2} 
                    GROUP BY ong.ProjeForm.Proje", new DateTime(yil, tarihStart, 1).ToString("yyyy-MM-dd"), new DateTime(yil, tarihStart, 1).AddMonths(1).ToString("yyyy-MM-dd"), sql)).ToList();
            if (tip == "Pie")
                return PartialView("GorevProjesi/AylikCalismaPie", liste);
            else
                return PartialView("GorevProjesi/AylikCalisma", liste);
        }
        #endregion
    }
}