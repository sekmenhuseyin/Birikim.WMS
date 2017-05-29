using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Wms12m.Entity;

namespace Wms12m.Presentation.Controllers
{
    public class HomeController : RootController
    {
        /// <summary>
        /// Anasayfa
        /// </summary>
        public ActionResult Index()
        {
            var ozet = db.GetHomeSummary(vUser.UserName, vUser.Id).FirstOrDefault();
            ViewBag.SirketKodu = db.GetSirketDBs().FirstOrDefault();
            return View("Index", ozet);
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
        public PartialViewResult PartialGunlukSatis(string SirketKodu, int tarih)
        {
            if (CheckPerm("ChartGunlukSatis", PermTypes.Reading) == false) return null;
            var GSA = db.Database.SqlQuery<ChartGunlukSatisAnalizi>(string.Format("[FINSAT6{0}].[wms].[DB_GunlukSatisAnalizi] @Tarih = {1}", SirketKodu, tarih)).ToList();
            ViewBag.tarih = tarih;
            ViewBag.tarih2 = tarih.FromOADateInt();
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            return PartialView("_PartialGunlukSatis", GSA);
        }

        public PartialViewResult PartialGunlukSatisPie(string SirketKodu, int tarih)
        {
            if (CheckPerm("ChartGunlukSatisPie", PermTypes.Reading) == false) return null;
            var GSA = db.Database.SqlQuery<ChartGunlukSatisAnalizi>(string.Format("[FINSAT6{0}].[wms].[DB_GunlukSatisAnalizi] @Tarih = {1}", SirketKodu, tarih)).ToList();
            ViewBag.tarih = tarih;
            ViewBag.tarih2 = tarih.FromOADateInt();
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            return PartialView("_PartialGunlukSatisPie", GSA);
        }

        public PartialViewResult PartialGunlukSatisYearToDay(string SirketKodu)
        {
            if (CheckPerm("ChartGunlukSatisYearToDay", PermTypes.Reading) == false) return null;
            int tarih = fn.ToOADate();
            var GSA = db.Database.SqlQuery<ChartGunlukSatisAnalizi>(string.Format("[FINSAT6{0}].[wms].[DB_GunlukSatisAnaliziYearToDay] @Tarih = {1}", SirketKodu, tarih)).ToList();
            ViewBag.tarih = tarih;
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            return PartialView("_PartialGunlukSatısAnaliziYearToDay", GSA);
        }

        public PartialViewResult PartialGunlukSatisYearToDayPie(string SirketKodu)
        {
            if (CheckPerm("ChartGunlukSatisYearToDayPie", PermTypes.Reading) == false) return null;
            int tarih = fn.ToOADate();
            var GSA = db.Database.SqlQuery<ChartGunlukSatisAnalizi>(string.Format("[FINSAT6{0}].[wms].[DB_GunlukSatisAnaliziYearToDay] @Tarih = {1}", SirketKodu, tarih)).ToList();
            ViewBag.tarih = tarih;
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            return PartialView("_PartialGunlukSatısAnaliziYearToDayPie", GSA);
        }

        public PartialViewResult PartialGunlukSatisDoubleKriter(string SirketKodu, string kod, int islemtip, int tarih)
        {
            if (CheckPerm("ChartGunlukSatisDoubleKriter", PermTypes.Reading) == false) return null;
            var GSADK = db.Database.SqlQuery<ChartGunlukSatisAnalizi>(string.Format("[FINSAT6{0}].[wms].[DB_GunlukSatisAnaliziDoubleKriter] @Tarih = {1}, @IslemTip = {2}, @Grup = '{3}'", SirketKodu, tarih, islemtip, kod)).ToList();
            ViewBag.IslemTip = islemtip;
            ViewBag.Kriter = kod;
            ViewBag.tarih = tarih;
            ViewBag.tarih2 = tarih.FromOADateInt();
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            return PartialView("_PartialGunlukSatisAnaliziDoubleKriter", GSADK);
        }

        public PartialViewResult PartialGunlukSatisDoubleKriterPie(string SirketKodu, string kod, int islemtip, int tarih)
        {
            if (CheckPerm("ChartGunlukSatisDoubleKriterPie", PermTypes.Reading) == false) return null;
            var GSADK = db.Database.SqlQuery<ChartGunlukSatisAnalizi>(string.Format("[FINSAT6{0}].[wms].[DB_GunlukSatisAnaliziDoubleKriter] @Tarih = {1}, @IslemTip = {2}, @Grup = '{3}'", SirketKodu, tarih, islemtip, kod)).ToList();
            ViewBag.IslemTip = islemtip;
            ViewBag.Kriter = kod;
            ViewBag.tarih = tarih;
            ViewBag.tarih2 = tarih.FromOADateInt();
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            return PartialView("_PartialGunlukSatisAnaliziDoubleKriterPie", GSADK);
        }

        public PartialViewResult PartialAylikSatisAnaliziBar(string SirketKodu)
        {
            if (CheckPerm("ChartAylikSatisAnaliziBar", PermTypes.Reading) == false) return null;
            var ASA = db.Database.SqlQuery<ChartAylikSatisAnalizi>(string.Format("[FINSAT6{0}].[wms].[DB_Aylik_SatisAnalizi]", SirketKodu)).ToList();
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            return PartialView("_PartialAylikSatisAnaliziBar", ASA);
        }

        public PartialViewResult PartialAylikSatisCHKAnaliziBar(string SirketKodu, string chk)
        {
            if (CheckPerm("ChartAylikSatisCHKAnaliziBar", PermTypes.Reading) == false) return null;
            chk = chk.ToString2();
            var ASA = db.Database.SqlQuery<ChartAylikSatisAnalizi>(string.Format("[FINSAT6{0}].[wms].[DB_Aylik_SatisAnalizi_CHK] @chk='{1}'", SirketKodu, chk)).ToList();
            ViewBag.CHK = chk;
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            return PartialView("_PartialAylikSatisCHKAnaliziBar", ASA);
        }

        public PartialViewResult PartialAylikSatisAnaliziKodTipDovizBar(string SirketKodu, string kod, int islemtip, string doviz)
        {
            if (CheckPerm("ChartAylikSatisAnaliziKodTipDovizBar", PermTypes.Reading) == false) return null;
            List<ChartAylikSatisAnalizi> GSADK;
            try
            {
                GSADK = db.Database.SqlQuery<ChartAylikSatisAnalizi>(string.Format("[FINSAT6{0}].[wms].[DB_Aylik_SatisAnalizi_Tip_Kod_Doviz] @Grup = {1}, @Kriter = {2}, @IslemTip = '{3}'", SirketKodu, kod, doviz, islemtip)).ToList();
            }
            catch (Exception ex)
            {
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
            if (CheckPerm("ChartUrunGrubuSatis", PermTypes.Reading) == false) return null;
            var UGS = db.Database.SqlQuery<ChartUrunGrubuSatisAnalizi>(string.Format("[FINSAT6{0}].[wms].[DB_UrunGrubu_SatisAnalizi] @Ay = {1}", SirketKodu, tarih)).ToList();
            ViewBag.Tarih = tarih;
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            return PartialView("_PartialUrunGrubuSatis", UGS);
        }

        public PartialViewResult PartialUrunGrubuSatisKriter(string SirketKodu, short tarih, string kriter)
        {
            if (CheckPerm("ChartUrunGrubuSatisKriter", PermTypes.Reading) == false) return null;
            var UGSK = db.Database.SqlQuery<ChartUrunGrubuSatisAnalizi>(string.Format("[FINSAT6{0}].[wms].[DB_UrunGrubu_SatisAnalizi_Kriter] @Ay = {1}, @Kriter={2}", SirketKodu, tarih, kriter)).ToList();
            ViewBag.Tarih = tarih;
            ViewBag.Kriter = kriter;
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            return PartialView("_PartialUrunGrubuSatisKriter", UGSK);
        }

        public PartialViewResult PartialLokasyonSatis(string SirketKodu, short tarih)
        {
            if (CheckPerm("ChartLokasyonSatis", PermTypes.Reading) == false) return null;
            var UGS = db.Database.SqlQuery<ChartUrunGrubuSatisAnalizi>(string.Format("[FINSAT6{0}].[wms].[DB_LokasyonBazli_SatisAnalizi] @Ay = {1}", SirketKodu, tarih)).ToList();
            ViewBag.Tarih = tarih;
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            return PartialView("_PartialLokasyonSatis", UGS);
        }

        public PartialViewResult PartialLokasyonSatisKriter(string SirketKodu, short tarih, string kriter)
        {
            if (CheckPerm("ChartLokasyonSatisKriter", PermTypes.Reading) == false) return null;
            var UGSK = db.Database.SqlQuery<ChartUrunGrubuSatisAnalizi>(string.Format("[FINSAT6{0}].[wms].[DB_LokasyonBazli_SatisAnalizi_Kriter] @Ay = {1}, @Kriter={2}", SirketKodu, tarih, kriter)).ToList();
            ViewBag.Tarih = tarih;
            ViewBag.Kriter = kriter;
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            return PartialView("_PartialLokasyonSatisKriter", UGSK);
        }

        public PartialViewResult PartialBakiyeRiskAnalizi(string SirketKodu)
        {
            if (CheckPerm("ChartBakiyeRiskAnalizi", PermTypes.Reading) == false) return null;
            var BRA = db.Database.SqlQuery<ChartBakiyeRiskAnalizi>(string.Format("[FINSAT6{0}].[wms].[DB_BakiyeRiskAnalizi]", "33")).ToList();
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            return PartialView("_PartialBakiyeRiskAnalizi", BRA);
        }

        public PartialViewResult PartialBekleyenSiparisUrunGrubu(string SirketKodu, int bastarih, int bittarih)
        {
            if (CheckPerm("ChartBekleyenSiparisUrunGrubu", PermTypes.Reading) == false) return null;
            ViewBag.BasTarih = bastarih;
            ViewBag.BasTarih2 = bastarih.FromOADateInt();
            ViewBag.BitTarih = bittarih;
            ViewBag.BitTarih2 = bittarih.FromOADateInt();
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            var BSUG = db.Database.SqlQuery<ChartBekleyenSiparisUrunGrubu>(string.Format("[FINSAT6{0}].[wms].[DB_BekleyenSiparis_UrunGrubu] @BasTarih = {1}, @BitTarih = {2}", SirketKodu, bastarih, bittarih)).ToList();
            return PartialView("_PartialBekleyenSiparisUrunGrubu", BSUG);
        }

        public PartialViewResult PartialBekleyenSiparisUrunGrubuMiktar(string SirketKodu, bool miktarTutar)
        {
            if (CheckPerm("ChartBekleyenSiparisUrunGrubuMiktar", PermTypes.Reading) == false) return null;
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            if (miktarTutar == true)
            {
                var BSUG = db.Database.SqlQuery<ChartBekleyenSiparisUrunGrubu>(string.Format("[FINSAT6{0}].[wms].[DB_BekleyenSiparis_UrunGrubu_Miktar]", SirketKodu)).ToList();
                ViewBag.MiktarTutar = "Miktar";
                return PartialView("_PartialBekleyenSiparisUrunGrubuMiktar", BSUG);
            }
            else
            {
                var BSUG = db.Database.SqlQuery<ChartBekleyenSiparisUrunGrubu>(string.Format("[FINSAT6{0}].[wms].[DB_BekleyenSiparis_UrunGrubu_Fiyat]", SirketKodu)).ToList();
                ViewBag.MiktarTutar = "Tutar";
                return PartialView("_PartialBekleyenSiparisUrunGrubuMiktar", BSUG);
            }
        }

        public PartialViewResult PartialBekleyenSiparisUrunGrubuMiktarPie(string SirketKodu, bool miktarTutar)
        {
            if (CheckPerm("ChartBekleyenSiparisUrunGrubuMiktarPie", PermTypes.Reading) == false) return null;
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            if (miktarTutar == true)
            {
                var BSUG = db.Database.SqlQuery<ChartBekleyenSiparisUrunGrubu>(string.Format("[FINSAT6{0}].[wms].[DB_BekleyenSiparis_UrunGrubu_Miktar]", SirketKodu)).ToList();
                ViewBag.MiktarTutar = "Miktar";
                return PartialView("_PartialBekleyenSiparisUrunGrubuMiktarPie", BSUG);
            }
            else
            {
                var BSUG = db.Database.SqlQuery<ChartBekleyenSiparisUrunGrubu>(string.Format("[FINSAT6{0}].[wms].[DB_BekleyenSiparis_UrunGrubu_Fiyat]", SirketKodu)).ToList();
                ViewBag.MiktarTutar = "Tutar";
                return PartialView("_PartialBekleyenSiparisUrunGrubuMiktarPie", BSUG);
            }
        }

        public PartialViewResult PartialBekleyenSiparisUrunGrubuMiktarKriter(string SirketKodu, bool miktarTutar, string kriter)
        {
            if (CheckPerm("ChartBekleyenSiparisUrunGrubuMiktarKriter", PermTypes.Reading) == false) return null;
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            if (miktarTutar == true)
            {
                var BSUG = db.Database.SqlQuery<ChartBekleyenSiparisUrunGrubu>(string.Format("[FINSAT6{0}].[wms].[DB_BekleyenSiparis_UrunGrubu_Miktar]", SirketKodu)).ToList();
                ViewBag.MiktarTutar = "Miktar";
                ViewBag.Kriter = "";
                return PartialView("_PartialBekleyenSiparisUrunGrubuMiktarKriter", BSUG);
            }
            else
            {
                var BSUG = db.Database.SqlQuery<ChartBekleyenSiparisUrunGrubu>(string.Format("[FINSAT6{0}].[wms].[DB_BekleyenSiparis_UrunGrubu_Fiyat_Kriter] @Kriter='{1}'", SirketKodu, kriter)).ToList();
                ViewBag.MiktarTutar = "Tutar";
                ViewBag.Kriter = kriter;
                return PartialView("_PartialBekleyenSiparisUrunGrubuMiktarKriter", BSUG);
            }
        }

        public PartialViewResult PartialBekleyenSiparisUrunGrubuMiktarKriterPie(string SirketKodu, bool miktarTutar, string kriter)
        {
            if (CheckPerm("ChartBekleyenSiparisUrunGrubuMiktarKriterPie", PermTypes.Reading) == false) return null;
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            if (miktarTutar == true)
            {
                var BSUG = db.Database.SqlQuery<ChartBekleyenSiparisUrunGrubu>(string.Format("[FINSAT6{0}].[wms].[DB_BekleyenSiparis_UrunGrubu_Miktar]", SirketKodu)).ToList();
                ViewBag.MiktarTutar = "Miktar";
                ViewBag.Kriter = "";
                return PartialView("_PartialBekleyenSiparisUrunGrubuMiktarKriterPie", BSUG);
            }
            else
            {
                var BSUG = db.Database.SqlQuery<ChartBekleyenSiparisUrunGrubu>(string.Format("[FINSAT6{0}].[wms].[DB_BekleyenSiparis_UrunGrubu_Fiyat_Kriter] @Kriter='{1}'", SirketKodu, kriter)).ToList();
                ViewBag.MiktarTutar = "Tutar";
                ViewBag.Kriter = kriter;
                return PartialView("_PartialBekleyenSiparisUrunGrubuMiktarKriterPie", BSUG);
            }
        }

        public PartialViewResult PartialBekleyenSiparisMusteriAnalizi(string SirketKodu, string kod, string doviz)
        {
            if (CheckPerm("ChartBekleyenSiparisMusteriAnalizi", PermTypes.Reading) == false) return null;
            var BSMA = db.Database.SqlQuery<ChartBekleyenSiparisUrunGrubu>(string.Format("[FINSAT6{0}].[wms].[DB_BekleyenSiparis_Musteri_Analizi] @Kod = '{1}', @Kriter = '{2}'", SirketKodu, kod, doviz)).ToList();
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            ViewBag.Doviz = doviz;
            ViewBag.Kriter = kod;
            return PartialView("_PartialBekleyenSiparisMusteriAnalizi", BSMA);
        }

        public PartialViewResult PartialSatisTemsilcisiAylikSatisAnalizi(string SirketKodu, string kod, short tarih)
        {
            if (CheckPerm("ChartSatisTemsilcisiAylikSatisAnalizi", PermTypes.Reading) == false) return null;
            var STASA = db.Database.SqlQuery<ChartBekleyenSiparisUrunGrubu>(string.Format("[FINSAT6{0}].[wms].[SatisTemsilcisi_AylikSatisAnalizi] @Ay = '{1}', @Kriter = '{2}'", SirketKodu, tarih, kod)).ToList();
            ViewBag.Tarih = tarih;
            ViewBag.Kriter = kod;
            ViewBag.SirketKodu = SirketKodu;
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            return PartialView("_PartialSatisTemsilcisi_AylikSatisAnalizi", STASA);
        }

        public PartialViewResult PartialBaglantiUrunGrubu()
        {
            if (CheckPerm("ChartBaglantiUrunGrubu", PermTypes.Reading) == false) return null;
            List<ChartBaglantiUrunGrup> BUGS;
            try
            {
                BUGS = db.Database.SqlQuery<ChartBaglantiUrunGrup>(string.Format("[FINSAT6{0}].[wms].[DB_SatisBaglanti_UrunGrubu]", "33")).ToList();
            }
            catch (Exception)
            {
                BUGS = new List<ChartBaglantiUrunGrup>();
            }
            return PartialView("_PartialBaglantiUrunGrubu", BUGS);
        }

        public PartialViewResult PartialBaglantiUrunGrubuPie()
        {
            if (CheckPerm("ChartBaglantiUrunGrubuPie", PermTypes.Reading) == false) return null;
            List<ChartBaglantiUrunGrup> BUGS;
            try
            {
                BUGS = db.Database.SqlQuery<ChartBaglantiUrunGrup>(string.Format("[FINSAT6{0}].[wms].[DB_SatisBaglanti_UrunGrubu]", "33")).ToList();
            }
            catch (Exception)
            {
                BUGS = new List<ChartBaglantiUrunGrup>();
            }
            return PartialView("_PartialBaglantiUrunGrubuPie", BUGS);
        }

        public PartialViewResult PartialGunlukMDFUretimi(int? tarih)
        {
            if (CheckPerm("ChartGunlukMDFUretimi", PermTypes.Reading) == false) return null;
            var tarih2 = DateTime.Today.Date;
            if (tarih == null)
            {
                tarih = DateTime.Today.ToOADate().ToInt32();
            }
            else
            {
                tarih2 = tarih.Value.FromOaDate().Date;
            }
            var GSA = db.Database.SqlQuery<ChartGunlukMDFUretimi>(string.Format("[FINSAT6{0}].[wms].[MDF_UretimRapor_Chart] @BasTarih = {1}, @BitTarih = {2}, @Tip={3}", "33", tarih, tarih, 1)).ToList();


            ViewBag.Tarih = tarih2;
            return PartialView("_PartialGunlukMDFUretim", GSA);
        }

        public PartialViewResult PartialGunlukMDFUretimiPie(int? tarih)
        {
            if (CheckPerm("ChartGunlukMDFUretimiPie", PermTypes.Reading) == false) return null;
            var tarih2 = DateTime.Today.Date;
            if (tarih == null)
            {
                tarih = DateTime.Today.ToOADate().ToInt32();
            }
            else
            {
                tarih2 = tarih.Value.FromOaDate().Date;
            }
            string sql = string.Format("[FINSAT6{0}].[wms].[MDF_UretimRapor_Chart] @BasTarih = {1}, @BitTarih = {2}, @Tip={3}", "33", tarih, tarih, 1);
            var GSA = db.Database.SqlQuery<ChartGunlukMDFUretimi>(sql).ToList();


            ViewBag.Tarih = tarih2;
            return PartialView("_PartialGunlukMDFUretimPie", GSA);
        }

        public PartialViewResult PartialBaglantiZamanCizelgesi()
        {
            if (CheckPerm("ChartBaglantiZamanCizelgesi", PermTypes.Reading) == false) return null;
            try
            {
                var BUGS = db.Database.SqlQuery<ChartBaglantiZaman>(string.Format("[FINSAT6{0}].[wms].[DB_BaglantiLogGetir]", "33")).ToList();
                return PartialView("_PartialBaglantiZamanCizelgesi", BUGS);

            }
            catch (Exception)
            {
                return PartialView("_PartialBaglantiZamanCizelgesi", new List<ChartBaglantiZaman>());
            }
        }

        public PartialViewResult PartialBolgeBazliSatisAnalizi(int? ay, string kriter)
        {
            if (CheckPerm("ChartBolgeBazliSatisAnalizi", PermTypes.Reading) == false) return null;
            if (ay == null)
            {
                ay = 0;
            }

            if (kriter == null)
            {
                kriter = "TÜMÜ";
            }

            ViewBag.Ay = ay;
            ViewBag.Kriter = kriter;

            var BSUG = db.Database.SqlQuery<ChartBolgeBazliSatisAnalizi>(string.Format("[FINSAT6{0}].[wms].[DB_Bolge_Bazinda_SatisAnalizi] @Ay = {1}, @Kriter = '{2}'", "33", ay, kriter)).ToList();
            return PartialView("_PartialBolgeBazliSatisAnalizi", BSUG);
        }

        public PartialViewResult PartialBolgeBazliSatisAnaliziPie(int? ay, string kriter)
        {
            if (CheckPerm("ChartBolgeBazliSatisAnaliziPie", PermTypes.Reading) == false) return null;
            if (ay == null)
            {
                ay = 0;
            }

            if (kriter == null)
            {
                kriter = "TÜMÜ";
            }

            ViewBag.Ay = ay;
            ViewBag.Kriter = kriter;

            var BSUG = db.Database.SqlQuery<ChartBolgeBazliSatisAnalizi>(string.Format("[FINSAT6{0}].[wms].[DB_Bolge_Bazinda_SatisAnalizi] @Ay = {1}, @Kriter = '{2}'", "33", ay, kriter)).ToList();
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
        public string BekleyenSiparisMusteriKriter()
        {
            var Kriter = db.Database.SqlQuery<ChartBolgeBazliSatisAnaliziKriter>(string.Format("[FINSAT6{0}].[wms].[BekleyenSiparisMusteriKriterSelect]", "33")).ToList();
            var json = new JavaScriptSerializer().Serialize(Kriter);
            return json;
        }
    }
}