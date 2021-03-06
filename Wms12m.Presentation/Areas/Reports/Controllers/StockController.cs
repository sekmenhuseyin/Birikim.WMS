﻿using System;
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
        /// <summary>
        /// stok
        /// </summary>
        public ActionResult Stok()
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return Redirect("/");
            var kOD = db.Database.SqlQuery<RaporGetKod>(string.Format("[FINSAT6{0}].[wms].[DB_GetKod]", vUser.SirketKodu)).ToList();
            return View(kOD);
        }

        public PartialViewResult StokList(int Tarih, string BasGrupKod, string BitGrupKod, string BasTipKod, string BitTipKod, string BasOzelKod, string BitOzelKod, string BasKod1, string BitKod1, string BasKod2, string BitKod2, string BasKod3, string BitKod3)
        {
            List<RaporStokKodCase> liste;
            try
            {
                liste = db.Database.SqlQuery<RaporStokKodCase>(string.Format("[FINSAT6{0}].[wms].[StokRaporuKodCase] @Tarih = {1}, @BasGrupKod = '{2}',@BitGrupKod = '{3}', @BasTipKod = '{4}',@BitTipKod= '{5}', @BasOzelKod = '{6}', @BitOzelKod = '{7}',@BasKod1 = '{8}', @BitKod1 = '{9}',@BasKod2= '{10}', @BitKod2 = '{11}',@BasKod3= '{12}', @BitKod3 = '{13}'", vUser.SirketKodu, Tarih, BasGrupKod, BitGrupKod, BasTipKod, BitTipKod, BasOzelKod, BitOzelKod, BasKod1, BitKod1, BasKod2, BitKod2, BasKod3, BitKod3)).ToList();
            }
            catch (Exception ex)
            {
                Logger(ex, "/Reports/Stock/StokList");
                liste = new List<RaporStokKodCase>();
            }

            return PartialView("StokList", liste);
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
            string sql = string.Format("[FINSAT6{0}].[wms].[BekleyenSiparisRaporu] @BasTarih = {1}, @BitTarih = {2},@BasTeslimTarih = {3}, @BitTeslimTarih = {4}", vUser.SirketKodu, bastarih, bittarih, basteslimtarih, bitteslimtarih);
            if (ViewBag.settings.BolgeKoduParametre == true)
                sql += string.Format(",@UserName='{0}'", vUser.UserName);
            var liste = db.Database.SqlQuery<RaporBekleyenSiparis>(sql).ToList();
            return PartialView("BekleyenSiparisList", liste);
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
            List<RaporSatilmayanUrunler> liste;
            try
            {
                liste = db.Database.SqlQuery<RaporSatilmayanUrunler>(string.Format("[FINSAT6{0}].[wms].[SatilmayanUrunler] @Number = {1}", vUser.SirketKodu, gunsayisi)).ToList();
            }
            catch (Exception ex)
            {
                Logger(ex, "/Reports/Stock/SatilmayanUrunlerList");
                liste = new List<RaporSatilmayanUrunler>();
            }

            return PartialView("SatilmayanUrunlerList", liste);
        }

        /// <summary>
        /// günlük satış
        /// </summary>
        public ActionResult GunlukSatis()
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return Redirect("/");
            return View();
        }

        public string GunlukSatisList(int bastarih, int bittarih)
        {
            var json = new JavaScriptSerializer()
            {
                MaxJsonLength = int.MaxValue
            };
            string sql = string.Format("[FINSAT6{0}].[wms].[GunlukSatisRaporu] @BasTarih = {1}, @BitTarih = {2}", vUser.SirketKodu, bastarih, bittarih);
            if (ViewBag.settings.BolgeKoduParametre == true)
                sql += string.Format(",@UserName='{0}'", vUser.UserName);
            var GS = db.Database.SqlQuery<RaporGunlukSatis>(sql).ToList();
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

            string sql = string.Format("[FINSAT6{0}].[wms].[KampanyaliSatisRaporu] @BasTarih={1}, @BitTarih={2}", vUser.SirketKodu, bastarih, bittarih);
            if (ViewBag.settings.BolgeKoduParametre == true)
                sql += string.Format(",@UserName='{0}'", vUser.UserName);
            var liste = db.Database.SqlQuery<KampanyaliSatisRaporu>(sql).ToList();
            return PartialView("KampanyaliSatisList", liste);
        }

        public PartialViewResult KampanyaChkDetay(string CHK, int bastarih, int bittarih)
        {
            var list = db.Database.SqlQuery<KampanyaliSatisRaporu>(string.Format("[FINSAT6{0}].[wms].[ChkKampanyaDetay] @CHK='{1}', @BasTarih={2}, @BitTarih={3}", vUser.SirketKodu, CHK, bastarih, bittarih)).ToList();
            return PartialView(list);
        }

        /// <summary>
        /// gerçekleşen sevkiyat
        /// </summary>
        public ActionResult GerceklesenSevkiyatPlani()
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return Redirect("/");
            return View("GerceklesenSevkiyatPlani");
        }

        public string GerceklesenSevkiyatPlaniList(int bastarih, int bittarih)
        {
            var json = new JavaScriptSerializer()
            {
                MaxJsonLength = int.MaxValue
            };

            string sql = string.Format("[FINSAT6{0}].[wms].[GerceklesenSevkiyatRaporu] @BasTarih={1}, @BitTarih={2}", vUser.SirketKodu, bastarih, bittarih);
            if (ViewBag.settings.BolgeKoduParametre == true)
                sql += string.Format(",@UserName='{0}'", vUser.UserName);
            var list = db.Database.SqlQuery<GerceklesenSevkiyatPlani>(sql).ToList();
            return json.Serialize(list);

        }

        /// <summary>
        /// sipariş onay
        /// </summary>
        public ActionResult SiparisOnay()
        {
            return View("SiparisOnay");
        }

        public string SiparisOnayData(string tip, int bastarih, int bittarih)
        {
            tip = tip == "0" ? "Beklemede" : tip == "1" ? "Onaylandı" : "Reddedildi";
            var json = new JavaScriptSerializer()
            {
                MaxJsonLength = int.MaxValue
            };
            try
            {
                var sipBilgi = dbl.TumSiparisOnayLogs.Where(m => m.OnayDurumu == tip && m.DegisTarih >= bastarih && m.DegisTarih <= bittarih).ToList();
                return json.Serialize(sipBilgi);
            }
            catch (Exception)
            {
            }
            return json.Serialize(new List<TumSiparisOnayLog>());
        }

        /// <summary>
        /// Giden Barkod
        /// </summary>
        public ActionResult GidenBarkod()
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return Redirect("/");
            var CHK = db.Database.SqlQuery<GidenBarkodSelect>(string.Format(@"SELECT DISTINCT [SevkEvrakNo]
                 FROM[solar6].[dbo].[Onikim_Terminal](NOLOCK) T
                 where T.Sirketkodu = '{0}' AND T.[AktarimDurum] = 1 AND T.Islemtip = 1 ", vUser.SirketKodu)).ToList();

            var siparis = db.Database.SqlQuery<GidenBarkodSelect>(string.Format(@"SELECT DISTINCT [SiparisNo]
                 FROM[solar6].[dbo].[Onikim_Terminal](NOLOCK) T
                 where T.Sirketkodu = '{0}' AND T.[AktarimDurum] = 1 AND T.Islemtip = 1 ", vUser.SirketKodu)).ToList();

            ViewBag.siparis = siparis;
            return View("GidenBarkod", CHK);
        }

        public PartialViewResult GidenBarkodList(string sip, string sevk)
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return null;
            //create sql
            List<GidenBarkodListe> liste;
            var sql = string.Format(@"
                SELECT      OT.ID, OT.SirketKodu, OT.SevkEvrakNo, STI.EvrakNo as IrsaliyeNo, OT.SiparisNo, OT.Chk, CHK.Unvan1 + CHK.Unvan2 AS Unvan, OT.SipSiraNo, OT.MalKodu,STK.MalAdi, OT.BarkodNo, OT.BarkodMiktar, STI.Birim, STI.Depo, OT.IslemTip, OT.AktarimDurum, OT.Kaydeden, OT.KayitTarih, OT.KayitTarih2
                FROM            SOLAR6.dbo.Onikim_Terminal OT (NOLOCK) INNER JOIN 
                                    FINSAT617.FINSAT617.STI STI (NOLOCK) on OT.Malkodu=STI.Malkodu and OT.SiparisNo=STI.KaynakSiparisNo and OT.SipSiraNo=STI.KaynakSiraNo INNER JOIN 
                                    FINSAT617.FINSAT617.STK STK (NOLOCK) ON OT.Malkodu=STK.Malkodu INNER JOIN 
                                    FINSAT617.FINSAT617.CHK CHK (NOLOCK) ON OT.CHK=CHK.HesapKodu
                WHERE       OT.Sirketkodu='{0}' AND OT.[AktarimDurum]=1 AND OT.Islemtip=1 AND ", vUser.SirketKodu);
            if (sip != null)
                sql += string.Format("[SiparisNo] = '{0}'", sip);
            else
                sql += string.Format("[SevkEvrakNo] = '{0}'", sevk);
            //get data
            liste = db.Database.SqlQuery<GidenBarkodListe>(sql).ToList();

            //return
            return PartialView("GidenBarkodList", liste);
        }

        /// <summary>
        /// sevkiyattan kalan stok
        /// </summary>
        public ActionResult SevkiyatKalanStok()
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return Redirect("/");
            return View();
        }

        public string SevkiyatKalanStokList()
        {
            var json = new JavaScriptSerializer()
            {
                MaxJsonLength = int.MaxValue
            };
            var list = db.Database.SqlQuery<SevkiyatKalanRapor>(string.Format("[FINSAT6{0}].[wms].[RP_SevkiyatKalanStok] ", vUser.SirketKodu)).ToList();
            return json.Serialize(list);



        }

        public PartialViewResult SevkiyatKalanDetay(string MalKodu)
        {
            var list = db.Database.SqlQuery<SevkiyatKalanDetay>(string.Format("[FINSAT6{0}].[wms].[RP_SevkiyatKalanDetay] @MalKodu='{1}' ", vUser.SirketKodu, MalKodu)).ToList();
            ViewBag.MalKodu = MalKodu;
            return PartialView("SevkiyatKalanDetay", list);
        }
    }
}