using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Wms12m.Entity;

namespace Wms12m.Presentation.Areas.Reports.Controllers
{
    public class FinancialController : RootController
    {
        /// <summary>
        /// cari ciro
        /// </summary>
        public ActionResult CariCiro()
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return Redirect("/");
            var CHK = db.Database.SqlQuery<CariCiroRaporuResult>(string.Format("[FINSAT6{0}].[wms].[CariCiroRaporu]", db.GetSirketDBs().FirstOrDefault())).ToList();
            return View(CHK);
        }
        /// <summary>
        /// cari ekstre
        /// </summary>
        public ActionResult CariEkstre()
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return Redirect("/");
            var CHK = db.Database.SqlQuery<RaporCHKSelect>(string.Format("[FINSAT6{0}].[wms].[CHKSelectKartTip]", db.GetSirketDBs().FirstOrDefault())).ToList();
            return View(CHK);
        }
        public PartialViewResult CariEkstreList(string chk)
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return null;
            var CE = db.Database.SqlQuery<RaporCariEkstre>(string.Format("[FINSAT6{0}].[wms].[DB_CariEkstre] @HesapKodu = '{1}'", db.GetSirketDBs().FirstOrDefault(), chk)).ToList();
            return PartialView("CariEkstreList", CE);
        }
        /// <summary>
        /// Vadesi Gelmemis Cekler
        /// </summary>
        public ActionResult VadesiGelmemisCek()
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return Redirect("/");
            return View();
        }
        public PartialViewResult VadesiGelmemisCekList(int bastarih, int bittarih)
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return null;
            var VGC = db.Database.SqlQuery<RaporVadesiGelmemisCekler>(string.Format("[FINSAT6{0}].[wms].[VadesiGelmemisCekler] @BasTarih = {1}, @BitTarih = {2}", db.GetSirketDBs().FirstOrDefault(), bastarih, bittarih)).ToList();
            return PartialView("VadesiGelmemisCekList", VGC);
        }
        /// <summary>
        /// çek listesi
        /// </summary>
        public ActionResult Cek()
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return Redirect("/");
            var PZ = db.Database.SqlQuery<RaporPozisyonCekSenet>(string.Format("[FINSAT6{0}].[wms].[PozisyonCekSenet]", db.GetSirketDBs().FirstOrDefault())).ToList();
            return View(PZ);
        }
        public PartialViewResult CekList(int pozisyon, int ay, int yil)
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return null;
            var CLR = db.Database.SqlQuery<RaporCekListesi>(string.Format("[FINSAT6{0}].[wms].[CekListesiRaporu] @Ay = {1}, @IslemTip = {2}, @Yil = {3}", db.GetSirketDBs().FirstOrDefault(), ay, pozisyon, yil)).ToList();
            return PartialView("CekList", CLR);
        }
        /// <summary>
        /// toplam risk bakiyesi
        /// </summary>
        public ActionResult RiskBakiye()
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return Redirect("/");
            return View();
        }
        public PartialViewResult RiskBakiyeList(int bastarih, int bittarih, int basvadetarih, int bitvadetarih, string chk_bas, string chk_bit)
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return null;
            var TRB = db.Database.SqlQuery<RaporToplamRiskBakiyesi>(string.Format("[FINSAT6{0}].[wms].[ToplamRiskBakiyesi] @BasTarih = {1}, @BitTarih = {2},@VadeBaslangic = {3}, @VadeBitis = {4},@BasHesapKodu= '{5}', @BitHesapKodu = '{6}'", db.GetSirketDBs().FirstOrDefault(), bastarih, bittarih, basvadetarih, bitvadetarih, chk_bas, chk_bit)).ToList();
            return PartialView("RiskBakiyeList", TRB);
        }
        /// <summary>
        /// çek portfoy listesi
        /// </summary>
        public ActionResult CekPortfoy()
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return Redirect("/");
            return View();
        }
        public PartialViewResult CekPortfoyList(int bastarih, int bittarih, int basvadetarih, int bitvadetarih)
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return null;
            var GS = db.Database.SqlQuery<RaporCekPortfoyListesi>(string.Format("[FINSAT6{0}].[wms].[CekPortfoyListesi] @BasTarih = {1}, @BitTarih = {2},@BasVadeTarih = {3}, @BitVadeTarih = {4}", db.GetSirketDBs().FirstOrDefault(), bastarih, bittarih, basvadetarih, bitvadetarih)).ToList();
            return PartialView("CekPortfoyList", GS);
        }
        /// <summary>
        /// ödeme yapmayan müşteriler
        /// </summary>
        /// <returns></returns>
        public ActionResult OdemeYapmayanMusteriler()
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return Redirect("/");
            return View();
        }
        public PartialViewResult OdemeYapmayanMusterilerList(int gunsayisi)
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return null;
            var OYM = db.Database.SqlQuery<RaporOdemeYapmayanMusteriler>(string.Format("[FINSAT6{0}].[wms].[OdemeYapmayanMusteriler] @Number = {1}", db.GetSirketDBs().FirstOrDefault(), gunsayisi)).ToList();
            return PartialView("OdemeYapmayanMusterilerList", OYM);
        }
        /// <summary>
        /// bakiye
        /// </summary>
        public ActionResult Bakiye()
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return Redirect("/");
            var CHK = db.Database.SqlQuery<RaporCHKSelect>(string.Format("[FINSAT6{0}].[wms].[CHKSelectKartTip]", db.GetSirketDBs().FirstOrDefault())).ToList();
            return View(CHK);
        }
        public PartialViewResult BakiyeList(int bastarih, int bittarih, int basvadetarih, int bitvadetarih, string chk_bas, string chk_bit, decimal bakiye)
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return null;
            var BR = db.Database.SqlQuery<RaporBakiye>(string.Format("[FINSAT6{0}].[wms].[BakiyeRaporu] @BasTarih = {1}, @BitTarih = {2},@VadeBaslangic = {3}, @VadeBitis = {4},@BasHesapKodu = '{5}', @BitHesapKodu = '{6}', @Bakiye = {7}", db.GetSirketDBs().FirstOrDefault(), bastarih, bittarih, basvadetarih, bitvadetarih, chk_bas, chk_bit, bakiye)).ToList();
            return PartialView("BakiyeList", BR);
        }
        public string BakiyeDetay(string CHK)
        {
            JavaScriptSerializer json = new JavaScriptSerializer()
            {
                MaxJsonLength = int.MaxValue
            };
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return null;
            var CE = db.Database.SqlQuery<RaporCariEkstre>(string.Format("[FINSAT6{0}].[wms].[DB_CariEkstre] @HesapKodu = '{1}'", db.GetSirketDBs().FirstOrDefault(), CHK)).ToList();
            return json.Serialize(CE);
        }
        /// <summary>
        /// satış bağlantı
        /// </summary>
        public ActionResult SatisBaglanti()
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return Redirect("/");
            return View();
        }
        public PartialViewResult SatisBaglantiList(int tip)
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return null;
            var SBR = db.Database.SqlQuery<SatisBaglatiRapru>(string.Format("[FINSAT6{0}].[wms].[SatisBaglantiRaporu] @Tip={1}", db.GetSirketDBs().FirstOrDefault(), tip)).ToList();
            return PartialView("SatisBaglantiList", SBR);
        }
        public string SatBagSozlesmeDetayListesiSelect(string listeNo)
        {

            var STL = db.Database.SqlQuery<BaglantiDetaySelect>(string.Format("[FINSAT6{0}].[wms].[BaglantiDetaySelect] @ListeNo='{1}'", db.GetSirketDBs().FirstOrDefault(), listeNo)).ToList();
            var json = new JavaScriptSerializer().Serialize(STL);
            return json;
        }
        public string SatBagHareketListesiSelect(string listeNo)
        {

            var STL = db.Database.SqlQuery<SatisBaglantiHareketleri>(string.Format("[FINSAT6{0}].[wms].[SatisBaglantiHareketleri] @SozlesmeNo='{1}'", db.GetSirketDBs().FirstOrDefault(), listeNo)).ToList();
            var json = new JavaScriptSerializer().Serialize(STL);
            return json;
        }
        /// <summary>
        /// toplam risk analiz raporu
        /// </summary>
        public ActionResult RiskAnaliz()
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return Redirect("/");
            return View();
        }
        public PartialViewResult RiskAnalizList(string baschk, string bitchk)
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return null;

            return PartialView("RiskAnalizList");
        }
        public string RiskAnalizListData(string baschk, string bitchk)
        {
            JavaScriptSerializer json = new JavaScriptSerializer()
            {
                MaxJsonLength = int.MaxValue
            };
            var TRAR = db.Database.SqlQuery<ToplamRiskAnaliziRaporu>(string.Format("[FINSAT6{0}].[wms].[ToplamRiskAnaliziRaporu] @BasHesapKodu='{1}', @BitHesapKodu='{2}'", "17", baschk, bitchk)).ToList();
            return json.Serialize(TRAR);
        }
        /// <summary>
        /// cari detay
        /// </summary>
        public string CariDetayCek(string CHK, string EvrakNo)
        {
            JavaScriptSerializer json = new JavaScriptSerializer()
            {
                MaxJsonLength = int.MaxValue
            };
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return null;
            var CE = db.Database.SqlQuery<RaporCariEkstreCek>(string.Format("[FINSAT6{0}].[wms].[BTB_RP_CariEkstreDetay_Cek] @Tip='', @HesapKodu = '{1}', @EvrakNo='{2}'", db.GetSirketDBs().FirstOrDefault(), CHK, EvrakNo)).ToList();
            return json.Serialize(CE);
        }
        public string CariDetayFatura(string CHK, string EvrakNo)
        {
            JavaScriptSerializer json = new JavaScriptSerializer()
            {
                MaxJsonLength = int.MaxValue
            };
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return null;
            var CE = db.Database.SqlQuery<RaporCariEkstreFatura>(string.Format("[FINSAT6{0}].[wms].[BTB_RP_CariEkstreDetay_Fatura]  @Tip='', @HesapKodu = '{1}', @EvrakNo='{2}'", db.GetSirketDBs().FirstOrDefault(), CHK, EvrakNo)).ToList();
            return json.Serialize(CE);
        }
        public string CariDetayDiger(string CHK, string EvrakNo)
        {
            JavaScriptSerializer json = new JavaScriptSerializer()
            {
                MaxJsonLength = int.MaxValue
            };
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return null;
            var CE = db.Database.SqlQuery<RaporCariEkstreDiger>(string.Format("[FINSAT6{0}].[wms].[BTB_RP_CariEkstreDetay_Diger]  @HesapKodu = '{1}', @EvrakNo='{2}'", db.GetSirketDBs().FirstOrDefault(), CHK, EvrakNo)).ToList();
            return json.Serialize(CE);
        }
        /// <summary>
        /// sipariş kampanya
        /// </summary>
        public string SiparisKampanyaDetay(string CHK, string EvrakNo, int bastarih, int bittarih)
        {
            JavaScriptSerializer json = new JavaScriptSerializer()
            {
                MaxJsonLength = int.MaxValue
            };
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return null;
            var list = db.Database.SqlQuery<KampanyaSiparisDetay>(string.Format("[FINSAT6{0}].[wms].[SiparisKampanyaDetay] @CHK='{1}', @EvrakNo='{2}', @BasTarih={3}, @BitTarih={4}", db.GetSirketDBs().FirstOrDefault(), CHK, EvrakNo, bastarih, bittarih)).ToList();
            return json.Serialize(list);
        }
        public JsonResult GetChKCode(string term)
        {
            var id = Url.RequestContext.RouteData.Values["id"];
            if (id == null) return null;
            string sql = "";
            //generate sql
            if (id.ToString() == "0")
                id = db.GetSirketDBs().FirstOrDefault();
            sql = String.Format("FINSAT6{0}.[wms].[CHKSearch] @HesapKodu = N'{1}', @Unvan = N'', @top = 200", id.ToString(), term);
            //return
            try
            {
                var list = db.Database.SqlQuery<frmJson>(sql).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger(ex, "Reports/Financial/GetChKCode");
                return Json(new List<frmJson>(), JsonRequestBehavior.AllowGet);
            }
        }
    }
}