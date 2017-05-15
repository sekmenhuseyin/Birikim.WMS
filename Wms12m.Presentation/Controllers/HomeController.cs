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
            var ozet = db.GetHomeSummary().FirstOrDefault();
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
            ViewBag.aktifNo = db.MenuFindAktif(ComboItems.WMS.ToInt32(), mYeri, vUser.RoleName, mUstNo, url).FirstOrDefault();
            var tablo = db.MenuGetirici(ComboItems.WMS.ToInt32(), mYeri, vUser.RoleName, mUstNo).ToList();
            return PartialView("../Shared/_MenuList", tablo);
        }
        /// <summary>
        /////////////////////////////////////////////// partials
        /// </summary>
        public PartialViewResult PartialGunlukSatis(string SirketKodu, int tarih, string type = "serial")
        {
            var tarih2 = tarih.FromOADateInt();
            var GSA = db.Database.SqlQuery<ChartGunlukSatisAnalizi>(string.Format("[FINSAT6{0}].[wms].[DB_GunlukSatisAnalizi] @Tarih = {1}", SirketKodu, tarih)).ToList();
            ViewBag.tarih = tarih;
            ViewBag.tarih2 = tarih2;
            ViewBag.type = type;
            ViewBag.SirketKodu = SirketKodu;
            return PartialView("_PartialGunlukSatis", GSA);
        }

        public PartialViewResult PartialGunlukSatisPie(string SirketKodu, int tarih, string type = "serial")
        {
            var tarih2 = tarih.FromOADateInt();
            var GSA = db.Database.SqlQuery<ChartGunlukSatisAnalizi>(string.Format("[FINSAT6{0}].[wms].[DB_GunlukSatisAnalizi] @Tarih = {1}", SirketKodu, tarih)).ToList();
            ViewBag.tarih = tarih;
            ViewBag.tarih2 = tarih2;
            ViewBag.type = type;
            ViewBag.SirketKodu = SirketKodu;
            return PartialView("_PartialGunlukSatisPie", GSA);
        }

        public PartialViewResult PartialGunlukSatisYearToDay(string SirketKodu, int tarih)
        {
            var GSA = db.Database.SqlQuery<ChartGunlukSatisAnalizi>(string.Format("[FINSAT6{0}].[wms].[DB_GunlukSatisAnaliziYearToDay] @Tarih = {1}", SirketKodu, tarih)).ToList();
            ViewBag.tarih = tarih;
            ViewBag.SirketKodu = SirketKodu;
            return PartialView("_PartialGunlukSatısAnaliziYearToDay", GSA);
        }

        public PartialViewResult PartialGunlukSatisYearToDayPie(string SirketKodu, int tarih)
        {

            var GSA = db.Database.SqlQuery<ChartGunlukSatisAnalizi>(string.Format("[FINSAT6{0}].[wms].[DB_GunlukSatisAnaliziYearToDay] @Tarih = {1}", SirketKodu, tarih)).ToList();
            ViewBag.tarih = tarih;
            ViewBag.SirketKodu = SirketKodu;
            return PartialView("_PartialGunlukSatısAnaliziYearToDayPie", GSA);
        }

        public PartialViewResult PartialGunlukSatisDoubleKriter(string kod, int islemtip, int? tarih)
        {
            var tarih2 = DateTime.Today.Date;
            if (tarih == null)
            {
                tarih = DateTime.Today.ToOADate().ToInt32();
            }
            else
            {
                tarih2 = tarih.Value.FromOaDate().Date;
            }
            var GSADK = db.Database.SqlQuery<ChartGunlukSatisAnalizi>(string.Format("[FINSAT6{0}].[wms].[DB_GunlukSatisAnaliziDoubleKriter] @Tarih = {1}, @IslemTip = {2}, @Grup = '{3}'", "01", tarih, islemtip, kod)).ToList();


            ViewBag.Tarih = tarih2;
            ViewBag.IslemTip = islemtip;
            ViewBag.Kriter = kod;
            return PartialView("_PartialGunlukSatisAnaliziDoubleKriter", GSADK);
        }

        public PartialViewResult PartialGunlukSatisDoubleKriterPie(string kod, int islemtip, int? tarih)
        {
            var tarih2 = DateTime.Today.Date;
            if (tarih == null)
            {
                tarih = DateTime.Today.ToOADate().ToInt32();
            }
            else
            {
                tarih2 = tarih.Value.FromOaDate().Date;
            }
            var GSADK = db.Database.SqlQuery<ChartGunlukSatisAnalizi>(string.Format("[FINSAT6{0}].[wms].[DB_GunlukSatisAnaliziDoubleKriter] @Tarih = {1}, @IslemTip = {2}, @Grup = '{3}'", "01", tarih, islemtip, kod)).ToList();


            ViewBag.Tarih = tarih2;
            ViewBag.IslemTip = islemtip;
            ViewBag.Kriter = kod;
            return PartialView("_PartialGunlukSatisAnaliziDoubleKriterPie", GSADK);
        }

        public PartialViewResult PartialAylikSatis()
        {
            var ASA = db.Database.SqlQuery<ChartAylikSatisAnalizi>(string.Format("[FINSAT6{0}].[wms].[DB_Aylik_SatisAnalizi]", "01")).ToList();
            return PartialView("_PartialAylikSatis", ASA);
        }

        public PartialViewResult PartialAylikSatisAnaliziBar()
        {
            var ASA = db.Database.SqlQuery<ChartAylikSatisAnalizi>(string.Format("[FINSAT6{0}].[wms].[DB_Aylik_SatisAnalizi]", "01")).ToList();
            return PartialView("_PartialAylikSatisAnaliziBar", ASA);
        }

        public PartialViewResult PartialAylikSatisCHKAnaliziBar(string chk)
        {
            //var ASA = new ChartAylikSatisAnalizi();
            var chk2 = "";
            if (chk == null)
            {
                chk = "";
            }
            else
            {
                chk2 = chk;
            }

            var ASA = db.Database.SqlQuery<ChartAylikSatisAnalizi>(string.Format("[FINSAT6{0}].[wms].[DB_Aylik_SatisAnalizi_CHK] @chk='{1}'", "01", chk)).ToList();
            ViewBag.CHK = chk2;
            return PartialView("_PartialAylikSatisCHKAnaliziBar", ASA);
        }

        public PartialViewResult PartialAylikSatisAnaliziKodTipDovizBar(string kod, int islemtip, string doviz)
        {
            List<ChartAylikSatisAnalizi> GSADK;
            try
            {
                GSADK = db.Database.SqlQuery<ChartAylikSatisAnalizi>(string.Format("[FINSAT6{0}].[wms].[DB_Aylik_SatisAnalizi_Tip_Kod_Doviz] @Grup = {1}, @Kriter = {2}, @IslemTip = '{3}'", "01", kod, doviz, islemtip)).ToList();
            }
            catch (Exception)
            {
                GSADK = new List<ChartAylikSatisAnalizi>();
            }
            ViewBag.Doviz = doviz;
            ViewBag.IslemTip = islemtip;
            ViewBag.Kriter = kod;
            return PartialView("_PartialAylikSatisAnaliziKodTipDovizBar", GSADK);
        }

        public PartialViewResult PartialUrunGrubuSatis(short? tarih)
        {
            if (tarih == null)
                tarih = (short)DateTime.Today.Month;
            var UGS = db.Database.SqlQuery<ChartUrunGrubuSatisAnalizi>(string.Format("[FINSAT6{0}].[wms].[DB_UrunGrubu_SatisAnalizi] @Ay = {1}", "01", tarih)).ToList();
            ViewBag.Tarih = tarih;
            return PartialView("_PartialUrunGrubuSatis", UGS);
        }

        public PartialViewResult PartialUrunGrubuSatisKriter(short? tarih, string kriter)
        {
            if (tarih == null)
                tarih = (short)DateTime.Today.Month;

            var UGSK = db.Database.SqlQuery<ChartUrunGrubuSatisAnalizi>(string.Format("[FINSAT6{0}].[wms].[DB_UrunGrubu_SatisAnalizi_Kriter] @Ay = {1}, @Kriter={2}", "01", tarih, kriter)).ToList();
            ViewBag.Tarih = tarih;
            ViewBag.Kriter = kriter;
            return PartialView("_PartialUrunGrubuSatisKriter", UGSK);
        }

        public PartialViewResult PartialLokasyonSatis(short? tarih)
        {
            if (tarih == null)
                tarih = (short)DateTime.Today.Month;
            var UGS = db.Database.SqlQuery<ChartUrunGrubuSatisAnalizi>(string.Format("[FINSAT6{0}].[wms].[DB_LokasyonBazli_SatisAnalizi] @Ay = {1}", "01", tarih)).ToList();
            ViewBag.Tarih = tarih;
            return PartialView("_PartialLokasyonSatis", UGS);
        }

        public PartialViewResult PartialLokasyonSatisKriter(short? tarih, string kriter)
        {
            if (tarih == null)
                tarih = (short)DateTime.Today.Month;

            var UGSK = db.Database.SqlQuery<ChartUrunGrubuSatisAnalizi>(string.Format("[FINSAT6{0}].[wms].[DB_LokasyonBazli_SatisAnalizi_Kriter] @Ay = {1}, @Kriter={2}", "01", tarih, kriter)).ToList();
            ViewBag.Tarih = tarih;
            ViewBag.Kriter = kriter;
            return PartialView("_PartialLokasyonSatisKriter", UGSK);
        }

        public PartialViewResult PartialBakiyeRiskAnalizi()
        {
            var BRA = db.Database.SqlQuery<ChartBakiyeRiskAnalizi>(string.Format("[FINSAT6{0}].[wms].[DB_BakiyeRiskAnalizi]", "01")).ToList();
            return PartialView("_PartialBakiyeRiskAnalizi", BRA);
        }

        public PartialViewResult PartialBekleyenSiparisUrunGrubu(string chk, int? bastarih, int? bittarih)
        {
            var tarih1 = DateTime.Today.Date;
            var tarih2 = DateTime.Today.Date;
            if (bastarih == null)
            {
                bastarih = DateTime.Today.ToOADate().ToInt32();
            }
            else
            {
                tarih1 = bastarih.Value.FromOaDate().Date;
            }

            if (bittarih == null)
            {
                bittarih = DateTime.Today.ToOADate().ToInt32();
            }
            else
            {
                tarih2 = bittarih.Value.FromOaDate().Date;
            }

            ViewBag.BasTarih = tarih1;
            ViewBag.BitTarih = tarih2;

            var BSUG = db.Database.SqlQuery<ChartBekleyenSiparisUrunGrubu>(string.Format("[FINSAT6{0}].[wms].[DB_BekleyenSiparis_UrunGrubu] @BasTarih = {1}, @BitTarih = {2}", chk, bastarih, bittarih)).ToList();
            return PartialView("_PartialBekleyenSiparisUrunGrubu", BSUG);
        }

        public PartialViewResult PartialBekleyenSiparisUrunGrubuMiktar(bool miktarTutar)
        {

            if (miktarTutar == true)
            {
                var BSUG = db.Database.SqlQuery<ChartBekleyenSiparisUrunGrubu>(string.Format("[FINSAT6{0}].[wms].[DB_BekleyenSiparis_UrunGrubu_Miktar]", "01")).ToList();
                ViewBag.MiktarTutar = "Miktar";
                return PartialView("_PartialBekleyenSiparisUrunGrubuMiktar", BSUG);
            }
            else
            {
                var BSUG = db.Database.SqlQuery<ChartBekleyenSiparisUrunGrubu>(string.Format("[FINSAT6{0}].[wms].[DB_BekleyenSiparis_UrunGrubu_Fiyat]", "01")).ToList();
                ViewBag.MiktarTutar = "Tutar";
                return PartialView("_PartialBekleyenSiparisUrunGrubuMiktar", BSUG);
            }
        }

        public PartialViewResult PartialBekleyenSiparisUrunGrubuMiktarPie(bool miktarTutar)
        {

            if (miktarTutar == true)
            {
                var BSUG = db.Database.SqlQuery<ChartBekleyenSiparisUrunGrubu>(string.Format("[FINSAT6{0}].[wms].[DB_BekleyenSiparis_UrunGrubu_Miktar]", "01")).ToList();
                ViewBag.MiktarTutar = "Miktar";
                return PartialView("_PartialBekleyenSiparisUrunGrubuMiktarPie", BSUG);
            }
            else
            {
                var BSUG = db.Database.SqlQuery<ChartBekleyenSiparisUrunGrubu>(string.Format("[FINSAT6{0}].[wms].[DB_BekleyenSiparis_UrunGrubu_Fiyat]", "01")).ToList();
                ViewBag.MiktarTutar = "Tutar";
                return PartialView("_PartialBekleyenSiparisUrunGrubuMiktarPie", BSUG);
            }
        }

        public PartialViewResult PartialBekleyenSiparisUrunGrubuMiktarKriter(bool miktarTutar, string kriter)
        {

            if (miktarTutar == true)
            {
                var BSUG = db.Database.SqlQuery<ChartBekleyenSiparisUrunGrubu>(string.Format("[FINSAT6{0}].[wms].[DB_BekleyenSiparis_UrunGrubu_Miktar]", "01")).ToList();
                ViewBag.MiktarTutar = "Miktar";
                ViewBag.Kriter = "";
                return PartialView("_PartialBekleyenSiparisUrunGrubuMiktarKriter", BSUG);
            }
            else
            {
                var BSUG = db.Database.SqlQuery<ChartBekleyenSiparisUrunGrubu>(string.Format("[FINSAT6{0}].[wms].[DB_BekleyenSiparis_UrunGrubu_Fiyat_Kriter] @Kriter='{1}'", "01", kriter)).ToList();
                ViewBag.MiktarTutar = "Tutar";
                ViewBag.Kriter = kriter;
                return PartialView("_PartialBekleyenSiparisUrunGrubuMiktarKriter", BSUG);
            }
        }

        public PartialViewResult PartialBekleyenSiparisUrunGrubuMiktarKriterPie(bool miktarTutar, string kriter)
        {

            if (miktarTutar == true)
            {
                var BSUG = db.Database.SqlQuery<ChartBekleyenSiparisUrunGrubu>(string.Format("[FINSAT6{0}].[wms].[DB_BekleyenSiparis_UrunGrubu_Miktar]", "01")).ToList();
                ViewBag.MiktarTutar = "Miktar";
                ViewBag.Kriter = "";
                return PartialView("_PartialBekleyenSiparisUrunGrubuMiktarKriterPie", BSUG);
            }
            else
            {
                var BSUG = db.Database.SqlQuery<ChartBekleyenSiparisUrunGrubu>(string.Format("[FINSAT6{0}].[wms].[DB_BekleyenSiparis_UrunGrubu_Fiyat_Kriter] @Kriter='{1}'", "01", kriter)).ToList();
                ViewBag.MiktarTutar = "Tutar";
                ViewBag.Kriter = kriter;
                return PartialView("_PartialBekleyenSiparisUrunGrubuMiktarKriterPie", BSUG);
            }
        }

        public PartialViewResult PartialBekleyenSiparisMusteriAnalizi(string kod, string doviz)
        {

            var BSMA = db.Database.SqlQuery<ChartBekleyenSiparisUrunGrubu>(string.Format("[FINSAT6{0}].[wms].[DB_BekleyenSiparis_Musteri_Analizi] @Kod = '{1}', @Kriter = '{2}'", "01", kod, doviz)).ToList();


            ViewBag.Doviz = doviz;
            ViewBag.Kriter = kod;
            return PartialView("_PartialBekleyenSiparisMusteriAnalizi", BSMA);
        }

        public PartialViewResult PartialSatisTemsilcisiAylikSatisAnalizi(string kod, short? tarih)
        {
            if (tarih == null)
                tarih = (short)DateTime.Today.Month;

            var STASA = db.Database.SqlQuery<ChartBekleyenSiparisUrunGrubu>(string.Format("[FINSAT6{0}].[wms].[SatisTemsilcisi_AylikSatisAnalizi] @Ay = '{1}', @Kriter = '{2}'", "01", tarih, kod)).ToList();


            ViewBag.Tarih = tarih;
            ViewBag.Kriter = kod;
            return PartialView("_PartialSatisTemsilcisi_AylikSatisAnalizi", STASA);
        }

        public PartialViewResult PartialBaglantiUrunGrubu()
        {
            List<ChartBaglantiUrunGrup> BUGS;
            try
            {
                BUGS = db.Database.SqlQuery<ChartBaglantiUrunGrup>(string.Format("[FINSAT6{0}].[wms].[DB_SatisBaglanti_UrunGrubu]", "01")).ToList();
            }
            catch (Exception)
            {
                BUGS = new List<ChartBaglantiUrunGrup>();
            }
            return PartialView("_PartialBaglantiUrunGrubu", BUGS);
        }

        public PartialViewResult PartialBaglantiUrunGrubuPie()
        {
            List<ChartBaglantiUrunGrup> BUGS;
            try
            {
                BUGS = db.Database.SqlQuery<ChartBaglantiUrunGrup>(string.Format("[FINSAT6{0}].[wms].[DB_SatisBaglanti_UrunGrubu]", "01")).ToList();
            }
            catch (Exception)
            {
                BUGS = new List<ChartBaglantiUrunGrup>();
            }
            return PartialView("_PartialBaglantiUrunGrubuPie", BUGS);
        }

        public PartialViewResult PartialGunlukMDFUretimi(int? tarih)
        {
            var tarih2 = DateTime.Today.Date;
            if (tarih == null)
            {
                tarih = DateTime.Today.ToOADate().ToInt32();
            }
            else
            {
                tarih2 = tarih.Value.FromOaDate().Date;
            }
            var GSA = db.Database.SqlQuery<ChartGunlukMDFUretimi>(string.Format("[FINSAT6{0}].[wms].[MDF_UretimRapor_Chart] @BasTarih = {1}, @BitTarih = {2}, @Tip={3}", "01", tarih, tarih, 1)).ToList();


            ViewBag.Tarih = tarih2;
            return PartialView("_PartialGunlukMDFUretim", GSA);
        }

        public PartialViewResult PartialGunlukMDFUretimiPie(int? tarih)
        {
            var tarih2 = DateTime.Today.Date;
            if (tarih == null)
            {
                tarih = DateTime.Today.ToOADate().ToInt32();
            }
            else
            {
                tarih2 = tarih.Value.FromOaDate().Date;
            }
            string sql = string.Format("[FINSAT6{0}].[wms].[MDF_UretimRapor_Chart] @BasTarih = {1}, @BitTarih = {2}, @Tip={3}", "01", tarih, tarih, 1);
            var GSA = db.Database.SqlQuery<ChartGunlukMDFUretimi>(sql).ToList();


            ViewBag.Tarih = tarih2;
            return PartialView("_PartialGunlukMDFUretimPie", GSA);
        }

        public PartialViewResult PartialBaglantiZamanCizelgesi()
        {
            try
            {
                var BUGS = db.Database.SqlQuery<ChartBaglantiZaman>(string.Format("[FINSAT6{0}].[wms].[DB_BaglantiLogGetir]", "33")).ToList();
                return PartialView("_PartialBaglantiZamanCizelgesi", BUGS);

            }
            catch (Exception)
            {
                return null;
            }
        }

        public PartialViewResult PartialBolgeBazliSatisAnalizi(int ay, string kriter)
        {
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

            var BSUG = db.Database.SqlQuery<ChartBolgeBazliSatisAnalizi>(string.Format("[FINSAT6{0}].[wms].[DB_Bolge_Bazinda_SatisAnalizi] @Ay = {1}, @Kriter = '{2}'", "01", ay, kriter)).ToList();
            return PartialView("_PartialBolgeBazliSatisAnalizi", BSUG);
        }

        public PartialViewResult PartialBolgeBazliSatisAnaliziPie(int ay, string kriter)
        {
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

            var BSUG = db.Database.SqlQuery<ChartBolgeBazliSatisAnalizi>(string.Format("[FINSAT6{0}].[wms].[DB_Bolge_Bazinda_SatisAnalizi] @Ay = {1}, @Kriter = '{2}'", "01", ay, kriter)).ToList();
            return PartialView("_PartialBolgeBazliSatisAnaliziPie", BSUG);
        }

        public string CHKSelect()
        {
            var CHK = db.Database.SqlQuery<RaporCHKSelect>(string.Format("[FINSAT6{0}].[wms].[CHKSelectKartTip]", "01")).ToList();
            var json = new JavaScriptSerializer().Serialize(CHK);
            return json;
        }

        public string BolgeBazliSatisAnaliziKriter()
        {
            var Kriter = db.Database.SqlQuery<ChartBolgeBazliSatisAnaliziKriter>(string.Format("[FINSAT6{0}].[wms].[BolgeBazliSatisAnaliziKriterSelect]", "01")).ToList();
            var json = new JavaScriptSerializer().Serialize(Kriter);
            return json;
        }
        public string GunlukSatisAnaliziDoubleKriter()
        {
            var Kriter = db.Database.SqlQuery<ChartBolgeBazliSatisAnaliziKriter>(string.Format("[FINSAT6{0}].[wms].[GunlukSatisAnaliziKriterSelect]", "01")).ToList();
            var json = new JavaScriptSerializer().Serialize(Kriter);
            return json;
        }

        public string AylikSatisAnaliziKodTipDovizKriter()
        {
            var Kriter = db.Database.SqlQuery<ChartBolgeBazliSatisAnaliziKriter>(string.Format("[FINSAT6{0}].[wms].[GunlukSatisAnaliziKriterSelect]", "01")).ToList();
            var json = new JavaScriptSerializer().Serialize(Kriter);
            return json;
        }

        public string BekleyenSiparisMusteriKriter()
        {
            var Kriter = db.Database.SqlQuery<ChartBolgeBazliSatisAnaliziKriter>(string.Format("[FINSAT6{0}].[wms].[BekleyenSiparisMusteriKriterSelect]", "01")).ToList();
            var json = new JavaScriptSerializer().Serialize(Kriter);
            return json;
        }
    }
}