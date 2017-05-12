using System.Linq;
using System.Web.Mvc;
using Wms12m.Entity;

namespace Wms12m.Presentation.Controllers
{
    public class ReportsController : RootController
    {
        public ActionResult Stok()
        {
            var KOD = db.Database.SqlQuery<RaporGetKod>(string.Format("[FINSAT6{0}].[wms].[DB_GetKod]", "01")).ToList();
            return View(KOD);
        }

        public PartialViewResult PartialStokRaporu(int Tarih, string BasGrupKod, string BitGrupKod, string BasTipKod, string BitTipKod, string BasOzelKod, string BitOzelKod, string BasKod1, string BitKod1, string BasKod2, string BitKod2, string BasKod3, string BitKod3)
        {
            var SR = db.Database.SqlQuery<RaporStokKodCase>(string.Format("[FINSAT6{0}].[wms].[StokRaporuKodCase] @Tarih = {1}, @BasGrupKod = '{2}',@BitGrupKod = '{3}', @BasTipKod = '{4}',@BitTipKod= '{5}', @BasOzelKod = '{6}', @BitOzelKod = '{7}',@BasKod1 = '{8}', @BitKod1 = '{9}',@BasKod2= '{10}', @BitKod2 = '{11}',@BasKod3= '{12}', @BitKod3 = '{13}'", "01", Tarih, BasGrupKod, BitGrupKod, BasTipKod, BitTipKod, BasOzelKod, BitOzelKod, BasKod1, BitKod1, BasKod2, BitKod2, BasKod3, BitKod3)).ToList();
            return PartialView("_PartialStokRaporu", SR);
        }

        public ActionResult BekleyenSiparis()
        {
            return View();
        }

        public PartialViewResult PartialBekleyenSiparis(int bastarih, int bittarih, int basteslimtarih, int bitteslimtarih)
        {

            var BSR = db.Database.SqlQuery<RaporBekleyenSiparis>(string.Format("[FINSAT6{0}].[wms].[BekleyenSiparisRaporu] @BasTarih = {1}, @BitTarih = {2},@BasTeslimTarih = {3}, @BitTeslimTarih = {4}", "01", bastarih, bittarih, basteslimtarih, bitteslimtarih)).ToList();
            return PartialView("_PartialBekleyenSiparis", BSR);
        }

        public ActionResult SatilmayanUrunler()
        {
            return View();
        }

        public PartialViewResult PartialSatilmayanUrunler(int gunsayisi)
        {

            var SU = db.Database.SqlQuery<RaporSatilmayanUrunler>(string.Format("[FINSAT6{0}].[wms].[SatilmayanUrunler] @Number = {1}", "01", gunsayisi)).ToList();
            return PartialView("_PartialSatilmayanUrunler", SU);
        }

        public ActionResult GunlukSatis()
        {
            return View();
        }

        public PartialViewResult PartialGunlukSatis(int bastarih, int bittarih)
        {

            var GS = db.Database.SqlQuery<RaporGunlukSatis>(string.Format("[FINSAT6{0}].[wms].[GunlukSatisRaporu] @BasTarih = {1}, @BitTarih = {2}", "01", bastarih, bittarih)).ToList();
            return PartialView("_PartialGunlukSatisRaporu", GS);
        }

        public ActionResult CariEkstre()
        {
            var CHK = db.Database.SqlQuery<RaporCHKSelect>(string.Format("[FINSAT6{0}].[wms].[CHKSelectKartTip]", "01")).ToList();
            return View(CHK);
        }

        public PartialViewResult PartialCariEkstre(string chk)
        {

            var CE = db.Database.SqlQuery<RaporCariEkstre>(string.Format("[FINSAT6{0}].[wms].[DB_CariEkstre] @HesapKodu = '{1}'", "01", chk)).ToList();
            return PartialView("_PartialCariEkstre", CE);
        }

        public ActionResult VadesiGelmemisCekler()
        {
            return View();
        }

        public PartialViewResult PartialVadesiGelmemisCekler(int bastarih, int bittarih)
        {

            var VGC = db.Database.SqlQuery<RaporVadesiGelmemisCekler>(string.Format("[FINSAT6{0}].[wms].[VadesiGelmemisCekler] @BasTarih = {1}, @BitTarih = {2}", "01", bastarih, bittarih)).ToList();
            return PartialView("_PartialVadesiGelmemisCekler", VGC);
        }

        public ActionResult CekListesi()
        {

            var PZ = db.Database.SqlQuery<RaporPozisyonCekSenet>(string.Format("[FINSAT6{0}].[wms].[PozisyonCekSenet]", "01")).ToList();
            return View(PZ);
        }

        public PartialViewResult PartialCekListesi(int pozisyon, int ay, int yil)
        {

            var CLR = db.Database.SqlQuery<RaporCekListesi>(string.Format("[FINSAT6{0}].[wms].[CekListesiRaporu] @Ay = {1}, @IslemTip = {2}, @Yil = {3}", "01", pozisyon, ay, yil)).ToList();
            return PartialView("_PartialCekListesi", CLR);
        }

        public ActionResult ToplamRiskBakiyesi()
        {
            var CHK = db.Database.SqlQuery<RaporCHKSelect>(string.Format("[FINSAT6{0}].[wms].[CHKSelectKartTip]", "01")).ToList();
            return View(CHK);
        }

        public PartialViewResult PartialToplamRiskBakiyesi(int bastarih, int bittarih, int basvadetarih, int bitvadetarih, string chk_bas, string chk_bit)
        {

            var TRB = db.Database.SqlQuery<RaporToplamRiskBakiyesi>(string.Format("[FINSAT6{0}].[wms].[ToplamRiskBakiyesi] @BasTarih = {1}, @BitTarih = {2},@VadeBaslangic = {3}, @VadeBitis = {4},@BasHesapKodu= '{5}', @BitHesapKodu = '{6}'", "01", bastarih, bittarih, basvadetarih, bitvadetarih, chk_bas, chk_bit)).ToList();
            return PartialView("_PartialToplamRiskBakiyesi", TRB);
        }

        public ActionResult CekPortfoyListesi()
        {
            return View();
        }

        public PartialViewResult PartialCekPortfoyListesi(int bastarih, int bittarih, int basvadetarih, int bitvadetarih)
        {

            var GS = db.Database.SqlQuery<RaporCekPortfoyListesi>(string.Format("[FINSAT6{0}].[wms].[CekPortfoyListesi] @BasTarih = {1}, @BitTarih = {2},@BasVadeTarih = {3}, @BitVadeTarih = {4}", "01", bastarih, bittarih, basvadetarih, bitvadetarih)).ToList();
            return PartialView("_PartialCekPortfoyListesi", GS);
        }

        public ActionResult OdemeYapmayanMusteriler()
        {
            return View();
        }

        public PartialViewResult PartialOdemeYapmayanMusteriler(int gunsayisi)
        {

            var OYM = db.Database.SqlQuery<RaporOdemeYapmayanMusteriler>(string.Format("[FINSAT6{0}].[wms].[OdemeYapmayanMusteriler] @Number = {1}", "01", gunsayisi)).ToList();
            return PartialView("_PartialOdemeYapmayanMusteriler", OYM);
        }

        public ActionResult Bakiye()
        {
            var CHK = db.Database.SqlQuery<RaporCHKSelect>(string.Format("[FINSAT6{0}].[wms].[CHKSelectKartTip]", "01")).ToList();
            return View(CHK);
        }

        public PartialViewResult PartialBakiye(int bastarih, int bittarih, int basvadetarih, int bitvadetarih, string chk_bas, string chk_bit, decimal bakiye)
        {

            var BR = db.Database.SqlQuery<RaporBakiye>(string.Format("[FINSAT6{0}].[wms].[BakiyeRaporu] @BasTarih = {1}, @BitTarih = {2},@VadeBaslangic = {3}, @VadeBitis = {4},@BasHesapKodu = '{5}', @BitHesapKodu = '{6}', @Bakiye = {7}", "01", bastarih, bittarih, basvadetarih, bitvadetarih, chk_bas, chk_bit, bakiye)).ToList();
            return PartialView("_PartialBakiye", BR);
        }


    }
}