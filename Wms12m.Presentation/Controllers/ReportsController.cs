using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Wms12m.Entity;

namespace Wms12m.Presentation.Controllers
{
    public class ReportsController : RootController
    {
        public ActionResult Stok()
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return Redirect("/");
            var KOD = db.Database.SqlQuery<RaporGetKod>(string.Format("[FINSAT6{0}].[wms].[DB_GetKod]", "17")).ToList();
            return View(KOD);
        }

        public PartialViewResult PartialStokRaporu(int Tarih, string BasGrupKod, string BitGrupKod, string BasTipKod, string BitTipKod, string BasOzelKod, string BitOzelKod, string BasKod1, string BitKod1, string BasKod2, string BitKod2, string BasKod3, string BitKod3)
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return null;
            var SR = db.Database.SqlQuery<RaporStokKodCase>(string.Format("[FINSAT6{0}].[wms].[StokRaporuKodCase] @Tarih = {1}, @BasGrupKod = '{2}',@BitGrupKod = '{3}', @BasTipKod = '{4}',@BitTipKod= '{5}', @BasOzelKod = '{6}', @BitOzelKod = '{7}',@BasKod1 = '{8}', @BitKod1 = '{9}',@BasKod2= '{10}', @BitKod2 = '{11}',@BasKod3= '{12}', @BitKod3 = '{13}'", "17", Tarih, BasGrupKod, BitGrupKod, BasTipKod, BitTipKod, BasOzelKod, BitOzelKod, BasKod1, BitKod1, BasKod2, BitKod2, BasKod3, BitKod3)).ToList();
            return PartialView("_PartialStokRaporu", SR);
        }

        public ActionResult BekleyenSiparis()
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return Redirect("/");
            return View();
        }

        public PartialViewResult PartialBekleyenSiparis(int bastarih, int bittarih, int basteslimtarih, int bitteslimtarih)
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return null;
            var BSR = db.Database.SqlQuery<RaporBekleyenSiparis>(string.Format("[FINSAT6{0}].[dbo].[BekleyenSiparisRaporu] @BasTarih = {1}, @BitTarih = {2},@BasTeslimTarih = {3}, @BitTeslimTarih = {4}", "17", bastarih, bittarih, basteslimtarih, bitteslimtarih)).ToList();
            return PartialView("_PartialBekleyenSiparis", BSR);
        }

        public ActionResult SatilmayanUrunler()
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return Redirect("/");
            return View();
        }

        public PartialViewResult PartialSatilmayanUrunler(int gunsayisi)
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return null;
            var SU = db.Database.SqlQuery<RaporSatilmayanUrunler>(string.Format("[FINSAT6{0}].[dbo].[SatilmayanUrunler] @Number = {1}", "17", gunsayisi)).ToList();
            return PartialView("_PartialSatilmayanUrunler", SU);
        }

        public ActionResult GunlukSatis()
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return Redirect("/");
            return View();
        }

        public PartialViewResult PartialGunlukSatis(int bastarih, int bittarih)
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return null;
            var GS = db.Database.SqlQuery<RaporGunlukSatis>(string.Format("[FINSAT6{0}].[dbo].[GunlukSatisRaporu] @BasTarih = {1}, @BitTarih = {2}", "17", bastarih, bittarih)).ToList();
            return PartialView("_PartialGunlukSatisRaporu", GS);
        }

        public ActionResult CariEkstre()
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return Redirect("/");
            var CHK = db.Database.SqlQuery<RaporCHKSelect>(string.Format("[FINSAT6{0}].[wms].[CHKSelectKartTip]", "17")).ToList();
            return View(CHK);
        }

        public PartialViewResult PartialCariEkstre(string chk)
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return null;
            var CE = db.Database.SqlQuery<RaporCariEkstre>(string.Format("[FINSAT6{0}].[dbo].[DB_CariEkstre] @HesapKodu = '{1}'", "17", chk)).ToList();
            //var CE = db.Database.SqlQuery<RaporCariEkstre>(string.Format("[FINSAT6{0}].[wms].[DB_CariEkstre] @HesapKodu = '{1}'", "17", chk)).ToList();
            return PartialView("_PartialCariEkstre", CE);
        }

        public ActionResult VadesiGelmemisCekler()
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return Redirect("/");
            return View();
        }

        public PartialViewResult PartialVadesiGelmemisCekler(int bastarih, int bittarih)
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return null;
            var VGC = db.Database.SqlQuery<RaporVadesiGelmemisCekler>(string.Format("[FINSAT6{0}].[wms].[VadesiGelmemisCekler] @BasTarih = {1}, @BitTarih = {2}", "17", bastarih, bittarih)).ToList();
            return PartialView("_PartialVadesiGelmemisCekler", VGC);
        }

        public ActionResult CekListesi()
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return Redirect("/");
            var PZ = db.Database.SqlQuery<RaporPozisyonCekSenet>(string.Format("[FINSAT6{0}].[wms].[PozisyonCekSenet]", "17")).ToList();
            return View(PZ);
        }

        public PartialViewResult PartialCekListesi(int pozisyon, int ay, int yil)
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return null;
            var CLR = db.Database.SqlQuery<RaporCekListesi>(string.Format("[FINSAT6{0}].[dbo].[CekListesiRaporu] @Ay = {1}, @IslemTip = {2}, @Yil = {3}", "17", ay, pozisyon, yil)).ToList();
            return PartialView("_PartialCekListesi", CLR);
        }

        public ActionResult ToplamRiskBakiyesi()
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return Redirect("/");
            var CHK = db.Database.SqlQuery<RaporCHKSelect>(string.Format("[FINSAT6{0}].[wms].[CHKSelectKartTip]", "17")).ToList();
            return View(CHK);
        }

        public PartialViewResult PartialToplamRiskBakiyesi(int bastarih, int bittarih, int basvadetarih, int bitvadetarih, string chk_bas, string chk_bit)
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return null;
            var TRB = db.Database.SqlQuery<RaporToplamRiskBakiyesi>(string.Format("[FINSAT6{0}].[dbo].[ToplamRiskBakiyesi] @BasTarih = {1}, @BitTarih = {2},@VadeBaslangic = {3}, @VadeBitis = {4},@BasHesapKodu= '{5}', @BitHesapKodu = '{6}'", "17", bastarih, bittarih, basvadetarih, bitvadetarih, chk_bas, chk_bit)).ToList();
            return PartialView("_PartialToplamRiskBakiyesi", TRB);
        }

        public ActionResult CekPortfoyListesi()
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return Redirect("/");
            return View();
        }

        public PartialViewResult PartialCekPortfoyListesi(int bastarih, int bittarih, int basvadetarih, int bitvadetarih)
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return null;
            var GS = db.Database.SqlQuery<RaporCekPortfoyListesi>(string.Format("[FINSAT6{0}].[wms].[CekPortfoyListesi] @BasTarih = {1}, @BitTarih = {2},@BasVadeTarih = {3}, @BitVadeTarih = {4}", "17", bastarih, bittarih, basvadetarih, bitvadetarih)).ToList();
            return PartialView("_PartialCekPortfoyListesi", GS);
        }

        public ActionResult OdemeYapmayanMusteriler()
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return Redirect("/");
            return View();
        }

        public PartialViewResult PartialOdemeYapmayanMusteriler(int gunsayisi)
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return null;
            var OYM = db.Database.SqlQuery<RaporOdemeYapmayanMusteriler>(string.Format("[FINSAT6{0}].[wms].[OdemeYapmayanMusteriler] @Number = {1}", "17", gunsayisi)).ToList();
            return PartialView("_PartialOdemeYapmayanMusteriler", OYM);
        }

        public ActionResult Bakiye()
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return Redirect("/");
            var CHK = db.Database.SqlQuery<RaporCHKSelect>(string.Format("[FINSAT6{0}].[wms].[CHKSelectKartTip]", "17")).ToList();
            return View(CHK);
        }

        public PartialViewResult PartialBakiye(int bastarih, int bittarih, int basvadetarih, int bitvadetarih, string chk_bas, string chk_bit, decimal bakiye)
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return null;
            var BR = db.Database.SqlQuery<RaporBakiye>(string.Format("[FINSAT6{0}].[wms].[BakiyeRaporu] @BasTarih = {1}, @BitTarih = {2},@VadeBaslangic = {3}, @VadeBitis = {4},@BasHesapKodu = '{5}', @BitHesapKodu = '{6}', @Bakiye = {7}", "17", bastarih, bittarih, basvadetarih, bitvadetarih, chk_bas, chk_bit, bakiye)).ToList();
            return PartialView("_PartialBakiye", BR);
        }
        public string BakiyeDetay(string CHK)
        {
            JavaScriptSerializer json = new JavaScriptSerializer()
            {
                MaxJsonLength = int.MaxValue
            };
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return null;
            var CE = db.Database.SqlQuery<RaporCariEkstre>(string.Format("[FINSAT6{0}].[dbo].[DB_CariEkstre] @HesapKodu = '{1}'", "17", CHK)).ToList();
            return json.Serialize(CE);
        }

        public string CariDetayCek(string CHK, string EvrakNo)
        {
            JavaScriptSerializer json = new JavaScriptSerializer()
            {
                MaxJsonLength = int.MaxValue
            };
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return null;
            var CE = db.Database.SqlQuery<RaporCariEkstreCek>(string.Format("[FINSAT6{0}].[dbo].[BTB_RP_CariEkstreDetay_Cek] @Tip='', @HesapKodu = '{1}', @EvrakNo='{2}'", "17", CHK, EvrakNo)).ToList();
            return json.Serialize(CE);
        }

        public string CariDetayFatura(string CHK, string EvrakNo)
        {
            JavaScriptSerializer json = new JavaScriptSerializer()
            {
                MaxJsonLength = int.MaxValue
            };
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return null;
            var CE = db.Database.SqlQuery<RaporCariEkstreFatura>(string.Format("[FINSAT6{0}].[dbo].[BTB_RP_CariEkstreDetay_Fatura]  @Tip='', @HesapKodu = '{1}', @EvrakNo='{2}'", "17", CHK, EvrakNo)).ToList();
            return json.Serialize(CE);
        }

        public string CariDetayDiger(string CHK, string EvrakNo)
        {
            JavaScriptSerializer json = new JavaScriptSerializer()
            {
                MaxJsonLength = int.MaxValue
            };
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return null;
            var CE = db.Database.SqlQuery<RaporCariEkstreDiger>(string.Format("[FINSAT6{0}].[dbo].[BTB_RP_CariEkstreDetay_Diger]  @HesapKodu = '{1}', @EvrakNo='{2}'", "17", CHK, EvrakNo)).ToList();
            return json.Serialize(CE);
        }
    }
}