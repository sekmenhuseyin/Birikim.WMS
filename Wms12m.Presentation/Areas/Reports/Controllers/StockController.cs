using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m.Presentation.Areas.Reports.Controllers
{
    public class StockController : RootController
    {
        public LOGEntities logdb = new LOGEntities();
        /// <summary>
        /// stok
        /// </summary>
        public ActionResult Stok()
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return Redirect("/");
            var KOD = db.Database.SqlQuery<RaporGetKod>(string.Format("[FINSAT6{0}].[wms].[DB_GetKod]", db.GetSirketDBs().FirstOrDefault())).ToList();
            return View(KOD);
        }
        public PartialViewResult StokList(int Tarih, string BasGrupKod, string BitGrupKod, string BasTipKod, string BitTipKod, string BasOzelKod, string BitOzelKod, string BasKod1, string BitKod1, string BasKod2, string BitKod2, string BasKod3, string BitKod3)
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return null;
            List<RaporStokKodCase> SR;
            try
            {
                db.Database.CommandTimeout = 2000;
                SR = db.Database.SqlQuery<RaporStokKodCase>(string.Format("[FINSAT6{0}].[wms].[StokRaporuKodCase] @Tarih = {1}, @BasGrupKod = '{2}',@BitGrupKod = '{3}', @BasTipKod = '{4}',@BitTipKod= '{5}', @BasOzelKod = '{6}', @BitOzelKod = '{7}',@BasKod1 = '{8}', @BitKod1 = '{9}',@BasKod2= '{10}', @BitKod2 = '{11}',@BasKod3= '{12}', @BitKod3 = '{13}'", db.GetSirketDBs().FirstOrDefault(), Tarih, BasGrupKod, BitGrupKod, BasTipKod, BitTipKod, BasOzelKod, BitOzelKod, BasKod1, BitKod1, BasKod2, BitKod2, BasKod3, BitKod3)).ToList();
            }
            catch (Exception ex)
            {
                Logger(ex, "/Reports/Stock/StokList");
                SR = new List<RaporStokKodCase>();
            }
            return PartialView("StokList", SR);
        }
        /// <summary>
        /// bekleyen sipariş
        /// </summary>
        public ActionResult BekleyenSiparis()
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return Redirect("/");
            return View();
        }
        public PartialViewResult BekleyenSiparisList(int bastarih, int bittarih, int basteslimtarih, int bitteslimtarih)
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return null;
            List<RaporBekleyenSiparis> BSR;
            try
            {
                BSR = db.Database.SqlQuery<RaporBekleyenSiparis>(string.Format("[FINSAT6{0}].[wms].[BekleyenSiparisRaporu] @BasTarih = {1}, @BitTarih = {2},@BasTeslimTarih = {3}, @BitTeslimTarih = {4}", db.GetSirketDBs().FirstOrDefault(), bastarih, bittarih, basteslimtarih, bitteslimtarih)).ToList();
            }
            catch (Exception ex)
            {
                Logger(ex, "/Reports/Stock/BekleyenSiparisList");
                BSR = new List<RaporBekleyenSiparis>();
            }
            return PartialView("BekleyenSiparisList", BSR);
        }
        /// <summary>
        /// satılmayan ürünler
        /// </summary>
        public ActionResult SatilmayanUrunler()
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return Redirect("/");
            return View();
        }
        public PartialViewResult SatilmayanUrunlerList(int gunsayisi)
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return null;
            List<RaporSatilmayanUrunler> SU;
            try
            {
                db.Database.CommandTimeout = 2000;
                SU = db.Database.SqlQuery<RaporSatilmayanUrunler>(string.Format("[FINSAT6{0}].[wms].[SatilmayanUrunler] @Number = {1}", db.GetSirketDBs().FirstOrDefault(), gunsayisi)).ToList();
            }
            catch (Exception ex)
            {
                Logger(ex, "/Reports/Stock/SatilmayanUrunlerList");
                SU = new List<RaporSatilmayanUrunler>();
            }
            return PartialView("SatilmayanUrunlerList", SU);
        }
        /// <summary>
        /// günlük satış
        /// </summary>
        public ActionResult GunlukSatis()
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return Redirect("/");
            return View();
        }
        public PartialViewResult GunlukSatisList(int bastarih, int bittarih)
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return null;
            //List<RaporGunlukSatis> SU;
            //try
            //{
            //    SU = db.Database.SqlQuery<RaporGunlukSatis>(string.Format("[FINSAT6{0}].[wms].[GunlukSatisRaporu] @BasTarih = {1}, @BitTarih = {2}",db.GetSirketDBs().FirstOrDefault(), bastarih, bittarih)).ToList();
            //}
            //catch (Exception ex)
            //{
            //    Logger(ex, "/Reports/Stock/GunlukSatisList");
            //    SU = new List<RaporGunlukSatis>();
            //}
            return PartialView("GunlukSatisList");
        }
        public string List(int bastarih, int bittarih)
        {
            JavaScriptSerializer json = new JavaScriptSerializer()
            {
                MaxJsonLength = int.MaxValue
            };
            var GS = db.Database.SqlQuery<RaporGunlukSatis>(string.Format("[FINSAT6{0}].[wms].[GunlukSatisRaporu] @BasTarih = {1}, @BitTarih = {2}", db.GetSirketDBs().FirstOrDefault(), bastarih, bittarih)).ToList();
            return json.Serialize(GS);
        }
        /// <summary>
        /// kampanyalı satış
        /// </summary>
        public ActionResult KampanyaliSatis()
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return Redirect("/");
            return View();
        }
        public PartialViewResult KampanyaliSatisList(int bastarih, int bittarih)
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return null;
            List<KampanyaliSatisRaporu> SU;
            try
            {
                SU = db.Database.SqlQuery<KampanyaliSatisRaporu>(string.Format("[FINSAT6{0}].[wms].[KampanyaliSatisRaporu] @BasTarih={1}, @BitTarih={2}", db.GetSirketDBs().FirstOrDefault(), bastarih, bittarih)).ToList();
            }
            catch (Exception ex)
            {
                Logger(ex, "/Reports/Stock/KampanyaliSatisList");
                SU = new List<KampanyaliSatisRaporu>();
            }
            return PartialView("KampanyaliSatisList", SU);
        }
        public PartialViewResult KampanyaChkDetay(string CHK, int bastarih, int bittarih)
        {
            if (CheckPerm(Perms.SözleşmeOnaylama, PermTypes.Reading) == false) return null;
            var list = db.Database.SqlQuery<KampanyaliSatisRaporu>(string.Format("[FINSAT6{0}].[wms].[ChkKampanyaDetay] @CHK='{1}', @BasTarih={2}, @BitTarih={3}", db.GetSirketDBs().FirstOrDefault(), CHK, bastarih, bittarih)).ToList();
            return PartialView(list);
        }
        /// <summary>
        /// gerçekleşen sevkiyat
        /// </summary>
        public ActionResult GerceklesenSevkiyatPlani()
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return Redirect("/");
            return View();
        }
        public PartialViewResult GerceklesenSevkiyatPlaniList(int bastarih, int bittarih)
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return null;
            List<GerceklesenSevkiyatPlani> SU;
            try
            {
                SU = db.Database.SqlQuery<GerceklesenSevkiyatPlani>(string.Format("[FINSAT6{0}].[wms].[GerceklesenSevkiyatRaporu] @BasTarih={1}, @BitTarih={2}", db.GetSirketDBs().FirstOrDefault(), bastarih, bittarih)).ToList();
            }
            catch (Exception ex)
            {
                Logger(ex, "/Reports/Stock/GerceklesenSevkiyatPlaniList");
                SU = new List<GerceklesenSevkiyatPlani>();
            }
            return PartialView("GerceklesenSevkiyatPlaniList", SU);
        }

        public ActionResult SiparisOnayRapor(string onayRed)
        {
            if (onayRed == null)
            {
                ViewBag.OnayDurum = "Onaylandı";
            }
            else
            {
                ViewBag.OnayDurum = onayRed;
            }
            return View();
        }

        public PartialViewResult SiparisOnayRaporList(string Tip, int bastarih, int bittarih)
        {
            ViewBag.Tip = Tip;
            ViewBag.bastarih = bastarih;
            ViewBag.bittarih = bittarih;
            return PartialView("SiparisOnayRaporList");
        }

        public string SiparisOnayRaporData(string tip, int bastarih, int bittarih)
        {

            JavaScriptSerializer json = new JavaScriptSerializer()
            {
                MaxJsonLength = int.MaxValue
            };
            List<TumSiparisOnayLog> sipBilgi = new List<TumSiparisOnayLog>();

            try
            {
                sipBilgi = logdb.TumSiparisOnayLogs.Where(m => m.OnayDurumu == tip && m.DegisTarih >= bastarih && m.DegisTarih <= bittarih).ToList();
            }
            catch (Exception ex)
            {

            }
            return json.Serialize(sipBilgi);
        }

    }
}