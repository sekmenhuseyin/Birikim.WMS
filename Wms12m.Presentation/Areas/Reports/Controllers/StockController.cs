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
            List<RaporStokKodCase> SR;
            try
            {
                SR = db.Database.SqlQuery<RaporStokKodCase>(string.Format("[FINSAT6{0}].[wms].[StokRaporuKodCase] @Tarih = {1}, @BasGrupKod = '{2}',@BitGrupKod = '{3}', @BasTipKod = '{4}',@BitTipKod= '{5}', @BasOzelKod = '{6}', @BitOzelKod = '{7}',@BasKod1 = '{8}', @BitKod1 = '{9}',@BasKod2= '{10}', @BitKod2 = '{11}',@BasKod3= '{12}', @BitKod3 = '{13}'", vUser.SirketKodu, Tarih, BasGrupKod, BitGrupKod, BasTipKod, BitTipKod, BasOzelKod, BitOzelKod, BasKod1, BitKod1, BasKod2, BitKod2, BasKod3, BitKod3)).ToList();
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
            List<RaporBekleyenSiparis> BSR;
            try
            {
                BSR = db.Database.SqlQuery<RaporBekleyenSiparis>(string.Format("[FINSAT6{0}].[wms].[BekleyenSiparisRaporu] @BasTarih = {1}, @BitTarih = {2},@BasTeslimTarih = {3}, @BitTeslimTarih = {4}", vUser.SirketKodu, bastarih, bittarih, basteslimtarih, bitteslimtarih)).ToList();
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
            List<RaporSatilmayanUrunler> SU;
            try
            {
                SU = db.Database.SqlQuery<RaporSatilmayanUrunler>(string.Format("[FINSAT6{0}].[wms].[SatilmayanUrunler] @Number = {1}", vUser.SirketKodu, gunsayisi)).ToList();
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
            return PartialView("GunlukSatisList");
        }

        public string List(int bastarih, int bittarih)
        {
            var json = new JavaScriptSerializer()
            {
                MaxJsonLength = int.MaxValue
            };
            var GS = db.Database.SqlQuery<RaporGunlukSatis>(string.Format("[FINSAT6{0}].[wms].[GunlukSatisRaporu] @BasTarih = {1}, @BitTarih = {2}", vUser.SirketKodu, bastarih, bittarih)).ToList();
            return json.Serialize(GS);
        }

        public string GerceklesenSevkiyatList(int bastarih, int bittarih)
        {
            var json = new JavaScriptSerializer()
            {
                MaxJsonLength = int.MaxValue
            };
            var SU = db.Database.SqlQuery<GerceklesenSevkiyatPlani>(string.Format("[FINSAT6{0}].[wms].[GerceklesenSevkiyatRaporu] @BasTarih={1}, @BitTarih={2}", vUser.SirketKodu, bastarih, bittarih)).ToList();
            return json.Serialize(SU);
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
            List<KampanyaliSatisRaporu> SU;
            try
            {
                SU = db.Database.SqlQuery<KampanyaliSatisRaporu>(string.Format("[FINSAT6{0}].[wms].[KampanyaliSatisRaporu] @BasTarih={1}, @BitTarih={2}", vUser.SirketKodu, bastarih, bittarih)).ToList();
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
            var list = db.Database.SqlQuery<KampanyaliSatisRaporu>(string.Format("[FINSAT6{0}].[wms].[ChkKampanyaDetay] @CHK='{1}', @BasTarih={2}, @BitTarih={3}", vUser.SirketKodu, CHK, bastarih, bittarih)).ToList();
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
            return PartialView("GerceklesenSevkiyatPlaniList");
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

        public ActionResult GidenBarkod()
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return Redirect("/");
            var CHK = db.Database.SqlQuery<GidenBarkodSelect>(string.Format(@"SELECT DISTINCT
                 [SevkEvrakNo]
                 FROM[solar6].[dbo].[Onikim_Terminal](NOLOCK) T
                 where T.Sirketkodu = '{0}' AND T.[AktarimDurum] = 1 AND T.Islemtip = 1 ", vUser.SirketKodu)).ToList();

            var siparis = db.Database.SqlQuery<GidenBarkodSelect>(string.Format(@"SELECT DISTINCT
                 [SiparisNo]
                 FROM[solar6].[dbo].[Onikim_Terminal](NOLOCK) T
                 where T.Sirketkodu = '{0}' AND T.[AktarimDurum] = 1 AND T.Islemtip = 1 ", vUser.SirketKodu)).ToList();

            ViewBag.siparis = siparis;
            return View(CHK);
        }

        public PartialViewResult GidenBarkodList(string sip, string sevk)
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return null;
            if (sip != null)
            {
                var CE = db.Database.SqlQuery<GidenBarkodListe>(string.Format(@"
SELECT  OT.ID, OT.SirketKodu, OT.SevkEvrakNo, STI.EvrakNo as IrsaliyeNo, OT.SiparisNo, OT.Chk, CHK.Unvan1 + CHK.Unvan2 AS Unvan, OT.SipSiraNo, OT.MalKodu,STK.MalAdi, OT.BarkodNo, OT.BarkodMiktar, STI.Birim, STI.Depo, OT.IslemTip, OT.AktarimDurum, OT.Kaydeden, OT.KayitTarih, OT.KayitTarih2
FROM            SOLAR6.dbo.Onikim_Terminal OT (NOLOCK)
INNER JOIN FINSAT617.FINSAT617.STI STI (NOLOCK) on OT.Malkodu=STI.Malkodu and OT.SiparisNo=STI.KaynakSiparisNo and OT.SipSiraNo=STI.KaynakSiraNo
INNER JOIN FINSAT617.FINSAT617.STK STK (NOLOCK) ON OT.Malkodu=STK.Malkodu
INNER JOIN FINSAT617.FINSAT617.CHK CHK (NOLOCK) ON OT.CHK=CHK.HesapKodu
WHERE OT.Sirketkodu='{0}' AND OT.[AktarimDurum]=1 AND OT.Islemtip=1 AND [SiparisNo] = '{1}'", vUser.SirketKodu, sip)).ToList();
                return PartialView("GidenBarkodList", CE);
            }
            else
            {
                var CE = db.Database.SqlQuery<GidenBarkodListe>(string.Format(@"
SELECT  OT.ID, OT.SirketKodu, OT.SevkEvrakNo, STI.EvrakNo as IrsaliyeNo, OT.SiparisNo, OT.Chk, CHK.Unvan1 + CHK.Unvan2 AS Unvan, OT.SipSiraNo, OT.MalKodu,STK.MalAdi, OT.BarkodNo, OT.BarkodMiktar, STI.Birim, STI.Depo, OT.IslemTip, OT.AktarimDurum, OT.Kaydeden, OT.KayitTarih, OT.KayitTarih2
FROM            SOLAR6.dbo.Onikim_Terminal OT (NOLOCK)
INNER JOIN FINSAT617.FINSAT617.STI STI (NOLOCK) on OT.Malkodu=STI.Malkodu and OT.SiparisNo=STI.KaynakSiparisNo and OT.SipSiraNo=STI.KaynakSiraNo
INNER JOIN FINSAT617.FINSAT617.STK STK (NOLOCK) ON OT.Malkodu=STK.Malkodu
INNER JOIN FINSAT617.FINSAT617.CHK CHK (NOLOCK) ON OT.CHK=CHK.HesapKodu
WHERE OT.Sirketkodu='{0}' AND OT.[AktarimDurum]=1 AND OT.Islemtip=1 AND [SevkEvrakNo] = '{1}'", vUser.SirketKodu, sevk)).ToList();
                return PartialView("GidenBarkodList", CE);
            }
        }



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