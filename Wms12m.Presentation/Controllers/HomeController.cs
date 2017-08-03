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
            ViewBag.MevcudBanka = db.Database.SqlQuery<decimal>(string.Format("[FINSAT6{0}].[wms].[MevcudBanka]", SirketKodu)).FirstOrDefault();
            ViewBag.MevcudCek = db.Database.SqlQuery<decimal>(string.Format("[FINSAT6{0}].[wms].[MevcudCek]", SirketKodu)).FirstOrDefault();
            ViewBag.MevcudKasa = db.Database.SqlQuery<decimal>(string.Format("[FINSAT6{0}].[wms].[MevcudKasa]", SirketKodu)).FirstOrDefault();
            ViewBag.MevcudPOS = db.Database.SqlQuery<decimal>(string.Format("[FINSAT6{0}].[wms].[MevcudPOS]", SirketKodu)).FirstOrDefault();
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
            if (CheckPerm(Perms.ChartGunlukSatis, PermTypes.Reading) == false) return null;
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
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            return PartialView("_PartialGunlukSatisZamanCizelgesi", BUGS);
        }

        public PartialViewResult PartialGunlukSatis(string SirketKodu, int tarih)
        {
            if (CheckPerm(Perms.ChartGunlukSatis, PermTypes.Reading) == false) return null;
            List<ChartGunlukSatisAnalizi> liste;
            try
            {
                liste = db.Database.SqlQuery<ChartGunlukSatisAnalizi>(string.Format("[FINSAT6{0}].[wms].[DB_GunlukSatisAnalizi] @Tarih = {1}", SirketKodu, tarih)).ToList();
            }
            catch (Exception)
            {
                liste = new List<ChartGunlukSatisAnalizi>();
            }
            ViewBag.tarih = tarih;
            ViewBag.tarih2 = tarih.FromOADateInt();
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            return PartialView("_PartialGunlukSatis", liste);
        }

        public PartialViewResult PartialGunlukSatisPie(string SirketKodu, int tarih)
        {
            if (CheckPerm(Perms.ChartGunlukSatis, PermTypes.Reading) == false) return null;
            List<ChartGunlukSatisAnalizi> liste;
            try
            {
                liste = db.Database.SqlQuery<ChartGunlukSatisAnalizi>(string.Format("[FINSAT6{0}].[wms].[DB_GunlukSatisAnalizi] @Tarih = {1}", SirketKodu, tarih)).ToList();
            }
            catch (Exception)
            {
                liste = new List<ChartGunlukSatisAnalizi>();
            }
            ViewBag.tarih = tarih;
            ViewBag.tarih2 = tarih.FromOADateInt();
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            return PartialView("_PartialGunlukSatisPie", liste);
        }

        public PartialViewResult PartialGunlukSatisYearToDay(string SirketKodu)
        {
            if (CheckPerm(Perms.ChartGunlukSatisYearToDay, PermTypes.Reading) == false) return null;
            int tarih = fn.ToOADate();
            var GSA = db.GetCachedChartYear2Day(SirketKodu).ToList();
            if (GSA.Count == 0)
                try
                {
                    GSA = db.Database.SqlQuery<GetCachedChartYear2Day_Result>(string.Format("[FINSAT6{0}].[wms].[DB_GunlukSatisAnaliziYearToDay] @Tarih = {1}", SirketKodu, tarih)).ToList();
                }
                catch (Exception ex)
                {
                    Logger(ex, "Home/ChartGunlukSatisYearToDay");
                    GSA = new List<GetCachedChartYear2Day_Result>();
                }
            ViewBag.tarih = tarih;
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            return PartialView("_PartialGunlukSatısAnaliziYearToDay", GSA);
        }

        public PartialViewResult PartialGunlukSatisYearToDayPie(string SirketKodu)
        {
            if (CheckPerm(Perms.ChartGunlukSatisYearToDay, PermTypes.Reading) == false) return null;
            int tarih = fn.ToOADate();
            var GSA = db.GetCachedChartYear2Day(SirketKodu).ToList();
            if (GSA.Count == 0)
                try
                {
                    GSA = db.Database.SqlQuery<GetCachedChartYear2Day_Result>(string.Format("[FINSAT6{0}].[wms].[DB_GunlukSatisAnaliziYearToDay] @Tarih = {1}", SirketKodu, tarih)).ToList();
                }
                catch (Exception ex)
                {
                    Logger(ex, "Home/PartialGunlukSatisYearToDayPie");
                    GSA = new List<GetCachedChartYear2Day_Result>();
                }
            ViewBag.tarih = tarih;
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            return PartialView("_PartialGunlukSatısAnaliziYearToDayPie", GSA);
        }

        public PartialViewResult PartialGunlukSatisDoubleKriter(string SirketKodu, string kod, int islemtip, int tarih)
        {
            if (CheckPerm(Perms.ChartGunlukSatisDoubleKriter, PermTypes.Reading) == false) return null;
            List<ChartGunlukSatisAnalizi> liste;
            try
            {
                liste = db.Database.SqlQuery<ChartGunlukSatisAnalizi>(string.Format("[FINSAT6{0}].[wms].[DB_GunlukSatisAnaliziDoubleKriter] @Tarih = {1}, @IslemTip = {2}, @Grup = '{3}'", SirketKodu, tarih, islemtip, kod)).ToList();
            }
            catch (Exception)
            {
                liste = new List<ChartGunlukSatisAnalizi>();
            }
            ViewBag.IslemTip = islemtip;
            ViewBag.Kriter = kod;
            ViewBag.tarih = tarih;
            ViewBag.tarih2 = tarih.FromOADateInt();
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            return PartialView("_PartialGunlukSatisAnaliziDoubleKriter", liste);
        }

        public PartialViewResult PartialGunlukSatisDoubleKriterPie(string SirketKodu, string kod, int islemtip, int tarih)
        {
            if (CheckPerm(Perms.ChartGunlukSatisDoubleKriter, PermTypes.Reading) == false) return null;
            List<ChartGunlukSatisAnalizi> liste;
            try
            {
                liste = db.Database.SqlQuery<ChartGunlukSatisAnalizi>(string.Format("[FINSAT6{0}].[wms].[DB_GunlukSatisAnaliziDoubleKriter] @Tarih = {1}, @IslemTip = {2}, @Grup = '{3}'", SirketKodu, tarih, islemtip, kod)).ToList();
            }
            catch (Exception)
            {
                liste = new List<ChartGunlukSatisAnalizi>();
            }
            ViewBag.IslemTip = islemtip;
            ViewBag.Kriter = kod;
            ViewBag.tarih = tarih;
            ViewBag.tarih2 = tarih.FromOADateInt();
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            return PartialView("_PartialGunlukSatisAnaliziDoubleKriterPie", liste);
        }

        public PartialViewResult PartialAylikSatisAnaliziBar(string SirketKodu)
        {
            if (CheckPerm(Perms.ChartAylikSatisAnaliziBar, PermTypes.Reading) == false) return null;
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
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            return PartialView("_PartialAylikSatisAnaliziBar", ASA);
        }

        public PartialViewResult PartialAylikSatisCHKAnaliziBar(string SirketKodu, string chk)
        {
            if (CheckPerm(Perms.ChartAylikSatisCHKAnaliziBar, PermTypes.Reading) == false) return null;
            chk = chk.ToString2();
            List<ChartAylikSatisAnalizi> liste;
            try
            {
                liste = db.Database.SqlQuery<ChartAylikSatisAnalizi>(string.Format("[FINSAT6{0}].[wms].[DB_Aylik_SatisAnalizi_CHK] @chk='{1}'", SirketKodu, chk)).ToList();
            }
            catch (Exception)
            {
                liste = new List<ChartAylikSatisAnalizi>();
            }
            ViewBag.CHK = chk;
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            return PartialView("_PartialAylikSatisCHKAnaliziBar", liste);
        }

        public PartialViewResult PartialAylikSatisAnaliziKodTipDovizBar(string SirketKodu, string kod, int islemtip, string doviz)
        {
            if (CheckPerm(Perms.ChartAylikSatisAnaliziKodTipDovizBar, PermTypes.Reading) == false) return null;
            List<ChartAylikSatisAnalizi> GSADK;
            try
            {
                GSADK = db.Database.SqlQuery<ChartAylikSatisAnalizi>(string.Format("[FINSAT6{0}].[wms].[DB_Aylik_SatisAnalizi_Tip_Kod_Doviz] @Grup = '{1}', @Kriter = '{2}', @IslemTip = '{3}'", SirketKodu, kod, doviz, islemtip)).ToList();
            }
            catch (Exception ex)
            {
                Logger(ex, "Home/PartialAylikSatisAnaliziKodTipDovizBar");
                GSADK = new List<ChartAylikSatisAnalizi>();
            }
            ViewBag.Doviz = doviz;
            ViewBag.IslemTip = islemtip;
            ViewBag.Kriter = kod;
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            return PartialView("_PartialAylikSatisAnaliziKodTipDovizBar", GSADK);
        }

        public PartialViewResult PartialUrunGrubuSatis(string SirketKodu, short tarih)
        {
            if (CheckPerm(Perms.ChartUrunGrubuSatis, PermTypes.Reading) == false) return null;
            List<ChartUrunGrubuSatisAnalizi> UGS;
            try
            {
                UGS = db.Database.SqlQuery<ChartUrunGrubuSatisAnalizi>(string.Format("[FINSAT6{0}].[wms].[DB_UrunGrubu_SatisAnalizi] @Ay = {1}", SirketKodu, tarih)).ToList();
            }
            catch (Exception)
            {
                UGS = new List<ChartUrunGrubuSatisAnalizi>();
            }
            ViewBag.Tarih = tarih;
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            return PartialView("_PartialUrunGrubuSatis", UGS);
        }

        public PartialViewResult PartialUrunGrubuSatisKriter(string SirketKodu, short tarih, string kriter)
        {
            if (CheckPerm(Perms.ChartUrunGrubuSatisKriter, PermTypes.Reading) == false) return null;
            List<ChartUrunGrubuSatisAnalizi> UGS;
            try
            {
                UGS = db.Database.SqlQuery<ChartUrunGrubuSatisAnalizi>(string.Format("[FINSAT6{0}].[wms].[DB_UrunGrubu_SatisAnalizi_Kriter] @Ay = {1}, @Kriter={2}", SirketKodu, tarih, kriter)).ToList();
            }
            catch (Exception)
            {
                UGS = new List<ChartUrunGrubuSatisAnalizi>();
            }
            ViewBag.Tarih = tarih;
            ViewBag.Kriter = kriter;
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            return PartialView("_PartialUrunGrubuSatisKriter", UGS);
        }

        public PartialViewResult PartialLokasyonSatis(string SirketKodu, short tarih)
        {
            if (CheckPerm(Perms.ChartLokasyonSatis, PermTypes.Reading) == false) return null;
            List<ChartUrunGrubuSatisAnalizi> UGS;
            try
            {
                UGS = db.Database.SqlQuery<ChartUrunGrubuSatisAnalizi>(string.Format("[FINSAT6{0}].[wms].[DB_LokasyonBazli_SatisAnalizi] @Ay = {1}", SirketKodu, tarih)).ToList();
            }
            catch (Exception)
            {
                UGS = new List<ChartUrunGrubuSatisAnalizi>();
            }
            ViewBag.Tarih = tarih;
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            return PartialView("_PartialLokasyonSatis", UGS);
        }

        public PartialViewResult PartialLokasyonSatisKriter(string SirketKodu, short tarih, string kriter)
        {
            if (CheckPerm(Perms.ChartLokasyonSatisKriter, PermTypes.Reading) == false) return null;
            List<ChartUrunGrubuSatisAnalizi> UGS;
            try
            {
                UGS = db.Database.SqlQuery<ChartUrunGrubuSatisAnalizi>(string.Format("[FINSAT6{0}].[wms].[DB_LokasyonBazli_SatisAnalizi_Kriter] @Ay = {1}, @Kriter={2}", SirketKodu, tarih, kriter)).ToList();
            }
            catch (Exception)
            {
                UGS = new List<ChartUrunGrubuSatisAnalizi>();
            }
            ViewBag.Tarih = tarih;
            ViewBag.Kriter = kriter;
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            return PartialView("_PartialLokasyonSatisKriter", UGS);
        }

        public PartialViewResult PartialBakiyeRiskAnalizi(string SirketKodu)
        {
            if (CheckPerm(Perms.ChartBakiyeRiskAnalizi, PermTypes.Reading) == false) return null;
            var BRA = db.GetCachedChartBakiyeRisk(SirketKodu).ToList();
            if (BRA == null)
                try
                {
                    BRA = db.Database.SqlQuery<GetCachedChartBakiyeRisk_Result>(string.Format("[FINSAT6{0}].[wms].[DB_BakiyeRiskAnalizi]", SirketKodu)).ToList();
                }
                catch (Exception)
                {
                    BRA = new List<GetCachedChartBakiyeRisk_Result>();
                }
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            return PartialView("_PartialBakiyeRiskAnalizi", BRA);
        }

        public PartialViewResult PartialBekleyenSiparisUrunGrubu(string SirketKodu, int bastarih, int bittarih)
        {
            if (CheckPerm(Perms.ChartBekleyenSiparisUrunGrubu, PermTypes.Reading) == false) return null;
            ViewBag.BasTarih = bastarih;
            ViewBag.BasTarih2 = bastarih.FromOADateInt();
            ViewBag.BitTarih = bittarih;
            ViewBag.BitTarih2 = bittarih.FromOADateInt();
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
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
            if (CheckPerm(Perms.ChartBekleyenSiparisUrunGrubuMiktar, PermTypes.Reading) == false) return null;
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
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
            if (CheckPerm(Perms.ChartBekleyenSiparisUrunGrubuMiktar, PermTypes.Reading) == false) return null;
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
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
            if (CheckPerm(Perms.ChartBekleyenSiparisUrunGrubuMiktarKriter, PermTypes.Reading) == false) return null;
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            List<GetCachedChartBekleyenUrunMiktarFiyat_Result> BSUG;
            try
            {
                BSUG = db.Database.SqlQuery<GetCachedChartBekleyenUrunMiktarFiyat_Result>(string.Format("[FINSAT6{0}].[wms].[DB_BekleyenSiparis_UrunGrubu_Fiyat_Kriter] @Kriter='{1}'", SirketKodu, kriter)).ToList();
            }
            catch (Exception)
            {
                BSUG = new List<GetCachedChartBekleyenUrunMiktarFiyat_Result>();
            }
            ViewBag.MiktarTutar = "Tutar";
            ViewBag.Kriter = kriter;
            return PartialView("_PartialBekleyenSiparisUrunGrubuMiktarKriter", BSUG);
        }

        public PartialViewResult PartialBekleyenSiparisUrunGrubuMiktarKriterPie(string SirketKodu, string kriter)
        {
            if (CheckPerm(Perms.ChartBekleyenSiparisUrunGrubuMiktarKriter, PermTypes.Reading) == false) return null;
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            List<GetCachedChartBekleyenUrunMiktarFiyat_Result> BSUG;
            try
            {
                BSUG = db.Database.SqlQuery<GetCachedChartBekleyenUrunMiktarFiyat_Result>(string.Format("[FINSAT6{0}].[wms].[DB_BekleyenSiparis_UrunGrubu_Fiyat_Kriter] @Kriter='{1}'", SirketKodu, kriter)).ToList();
            }
            catch (Exception)
            {
                BSUG = new List<GetCachedChartBekleyenUrunMiktarFiyat_Result>();
            }
            ViewBag.MiktarTutar = "Tutar";
            ViewBag.Kriter = kriter;
            return PartialView("_PartialBekleyenSiparisUrunGrubuMiktarKriterPie", BSUG);
        }

        public PartialViewResult PartialBekleyenSiparisMusteriAnalizi(string SirketKodu, string kod, string doviz)
        {
            if (CheckPerm(Perms.ChartBekleyenSiparisMusteriAnalizi, PermTypes.Reading) == false) return null;
            List<ChartBekleyenSiparisUrunGrubu> liste;
            try
            {
                liste = db.Database.SqlQuery<ChartBekleyenSiparisUrunGrubu>(string.Format("[FINSAT6{0}].[wms].[DB_BekleyenSiparis_Musteri_Analizi] @Kod = '{1}', @Kriter = '{2}'", SirketKodu, kod, doviz)).ToList();
            }
            catch (Exception)
            {
                liste = new List<ChartBekleyenSiparisUrunGrubu>();
            }
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            ViewBag.Doviz = doviz;
            ViewBag.Kriter = kod;
            return PartialView("_PartialBekleyenSiparisMusteriAnalizi", liste);
        }

        public PartialViewResult PartialSatisTemsilcisiAylikSatisAnalizi(string SirketKodu, string kod, short tarih)
        {
            if (CheckPerm(Perms.ChartSatisTemsilcisiAylikSatisAnalizi, PermTypes.Reading) == false) return null;
            List<ChartSatisTemsilcisiAylikSatisAnalizi> list;
            try
            {
                list = db.Database.SqlQuery<ChartSatisTemsilcisiAylikSatisAnalizi>(string.Format("[FINSAT6{0}].[wms].[SatisTemsilcisi_AylikSatisAnalizi] @Ay = '{1}', @Kriter = '{2}'", SirketKodu, tarih, kod)).ToList();
            }
            catch (Exception)
            {
                list = new List<ChartSatisTemsilcisiAylikSatisAnalizi>();
            }
            ViewBag.Tarih = tarih;
            ViewBag.Kriter = kod;
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            return PartialView("_PartialSatisTemsilcisi_AylikSatisAnalizi", list);
        }

        public PartialViewResult PartialBaglantiUrunGrubu(string SirketKodu)
        {
            if (CheckPerm(Perms.ChartBaglantiUrunGrubu, PermTypes.Reading) == false) return null;
            var BUGS = db.GetCachedChartSatisBaglanti(SirketKodu).ToList();
            if (BUGS == null)
                try
                {
                    BUGS = db.Database.SqlQuery<GetCachedChartSatisBaglanti_Result>(string.Format("[FINSAT6{0}].[wms].[DB_SatisBaglanti_UrunGrubu]", SirketKodu)).ToList();
                }
                catch (Exception ex)
                {
                    Logger(ex, "Home/PartialBaglantiUrunGrubu");
                    BUGS = new List<GetCachedChartSatisBaglanti_Result>();
                }
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            return PartialView("_PartialBaglantiUrunGrubu", BUGS);
        }

        public PartialViewResult PartialBaglantiUrunGrubuPie(string SirketKodu)
        {
            if (CheckPerm(Perms.ChartBaglantiUrunGrubu, PermTypes.Reading) == false) return null;
            var BUGS = db.GetCachedChartSatisBaglanti(SirketKodu).ToList();
            if (BUGS == null)
                try
                {
                    BUGS = db.Database.SqlQuery<GetCachedChartSatisBaglanti_Result>(string.Format("[FINSAT6{0}].[wms].[DB_SatisBaglanti_UrunGrubu]", SirketKodu)).ToList();
                }
                catch (Exception ex)
                {
                    Logger(ex, "Home/PartialBaglantiUrunGrubuPie");
                    BUGS = new List<GetCachedChartSatisBaglanti_Result>();
                }
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            return PartialView("_PartialBaglantiUrunGrubuPie", BUGS);
        }

        public PartialViewResult PartialGunlukMDFUretimi(string SirketKodu, int tarih)
        {
            if (CheckPerm(Perms.ChartGunlukMDFUretimi, PermTypes.Reading) == false) return null;
            List<ChartGunlukMDFUretimi> UGS;
            try
            {
                UGS = db.Database.SqlQuery<ChartGunlukMDFUretimi>(string.Format("[FINSAT6{0}].[wms].[MDF_UretimRapor_Chart] @BasTarih = {1}, @BitTarih = {2}, @Tip={3}", SirketKodu, tarih, tarih, 1)).ToList();
            }
            catch (Exception)
            {
                UGS = new List<ChartGunlukMDFUretimi>();
            }
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            ViewBag.tarih = tarih;
            ViewBag.tarih2 = tarih.FromOADateInt();
            return PartialView("_PartialGunlukMDFUretim", UGS);
        }

        public PartialViewResult PartialGunlukMDFUretimiPie(string SirketKodu, int tarih)
        {
            if (CheckPerm(Perms.ChartGunlukMDFUretimi, PermTypes.Reading) == false) return null;
            List<ChartGunlukMDFUretimi> GSA;
            try
            {
                GSA = db.Database.SqlQuery<ChartGunlukMDFUretimi>(string.Format("[FINSAT6{0}].[wms].[MDF_UretimRapor_Chart] @BasTarih = {1}, @BitTarih = {2}, @Tip={3}", SirketKodu, tarih, tarih, 1)).ToList();
            }
            catch (Exception)
            {
                GSA = new List<ChartGunlukMDFUretimi>();
            }
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            ViewBag.tarih = tarih;
            ViewBag.tarih2 = tarih.FromOADateInt();
            return PartialView("_PartialGunlukMDFUretimPie", GSA);
        }

        public PartialViewResult PartialBaglantiZamanCizelgesi(string SirketKodu)
        {
            if (CheckPerm(Perms.ChartBaglantiZamanCizelgesi, PermTypes.Reading) == false) return null;
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
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            return PartialView("_PartialBaglantiZamanCizelgesi", BUGS);
        }

        public PartialViewResult PartialBolgeBazliSatisAnalizi(string SirketKodu, int ay, string kriter)
        {
            if (CheckPerm(Perms.ChartBolgeBazliSatisAnalizi, PermTypes.Reading) == false) return null;
            ViewBag.Ay = ay;
            ViewBag.Kriter = kriter;
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
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
            if (CheckPerm(Perms.ChartBolgeBazliSatisAnalizi, PermTypes.Reading) == false) return null;
            ViewBag.Ay = ay;
            ViewBag.Kriter = kriter;
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
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
        public string BolgeBazliSatisAnaliziKriter()
        {
            var Kriter = db.Database.SqlQuery<ChartBolgeBazliSatisAnaliziKriter>(string.Format("[FINSAT6{0}].[wms].[BolgeBazliSatisAnaliziKriterSelect]", "33")).ToList();
            var json = new JavaScriptSerializer().Serialize(Kriter);
            return json;
        }
        public string GunlukSatisAnaliziDoubleKriter(string SirketKodu)
        {
            var Kriter = db.Database.SqlQuery<ChartBolgeBazliSatisAnaliziKriter>(string.Format("[FINSAT6{0}].[wms].[GunlukSatisAnaliziKriterSelect]", SirketKodu)).ToList();
            var json = new JavaScriptSerializer().Serialize(Kriter);
            return json;
        }
        public string AylikSatisAnaliziKodTipDovizKriter(string SirketKodu)
        {
            var Kriter = db.Database.SqlQuery<ChartBolgeBazliSatisAnaliziKriter>(string.Format("[FINSAT6{0}].[wms].[GunlukSatisAnaliziKriterSelect]", SirketKodu)).ToList();
            var json = new JavaScriptSerializer().Serialize(Kriter);
            return json;
        }
        public string BekleyenSiparisMusteriKriter(string SirketKodu)
        {
            var Kriter = db.Database.SqlQuery<ChartBolgeBazliSatisAnaliziKriter>(string.Format("[FINSAT6{0}].[wms].[BekleyenSiparisMusteriKriterSelect]", SirketKodu)).ToList();
            var json = new JavaScriptSerializer().Serialize(Kriter);
            return json;
        }
        public string Connection()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString;
        }
    }
}