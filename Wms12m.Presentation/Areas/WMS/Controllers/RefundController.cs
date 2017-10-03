using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Wms12m.Entity;

namespace Wms12m.Presentation.Areas.WMS.Controllers
{
    public class RefundController : RootController
    {
        // GET: WMS/Refund
        public ActionResult Index()
        {
            if (CheckPerm(Perms.MalKabul, PermTypes.Reading) == false) return Redirect("/");
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            ViewBag.DepoID = new SelectList(Store.GetList(vUser.DepoId), "ID", "DepoAd");
            return View("Index");
        }

        public JsonResult GetChKCode(string term)
        {
            var id = Url.RequestContext.RouteData.Values["id"];

            if (id == null) return null;
            string sql = "";
            //generate sql

            sql = String.Format("FINSAT6{0}.[wms].[CHKSearch4] @HesapKodu = N'', @Unvan = N'{1}', @top = 200", id.ToString(), term);
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

        /// <summary>
        /// depo ve şirket seçince açık siparişler gelecek
        /// </summary>
        [HttpPost]
        //public PartialViewResult GetSiparis(string Sirket,string DepoID,string CHK, string Starts, string Ends)
        //{
        //    //ilk kontrol
        //    if (DepoID == "0" || Sirket=="0" || CHK=="") return null;
        //    bool tarihler = DateTime.TryParse(Starts, out DateTime StartDate); if (tarihler == false) return null;
        //    tarihler = DateTime.TryParse(Ends, out DateTime EndDate); if (tarihler == false) return null;
        //    if (StartDate > EndDate) return null;
        //    //perm kontrol
        //    if (CheckPerm(Perms.GenelSipariş, PermTypes.Reading) == false) return null;
        //    string sql = String.Format("SELECT '{0}' as SirketID, FINSAT6{0}.FINSAT6{0}.SPI.EvrakNo, FINSAT6{0}.FINSAT6{0}.SPI.Tarih, FINSAT6{0}.FINSAT6{0}.SPI.Chk, FINSAT6{0}.FINSAT6{0}.CHK.Unvan1 AS Unvan, FINSAT6{0}.FINSAT6{0}.CHK.GrupKod, FINSAT6{0}.FINSAT6{0}.CHK.FaturaAdres3 AS FaturaAdres, FINSAT6{0}.FINSAT6{0}.MFK.Aciklama, COUNT(FINSAT6{0}.FINSAT6{0}.SPI.MalKodu) AS Çeşit, SUM(FINSAT6{0}.FINSAT6{0}.SPI.BirimMiktar - FINSAT6{0}.FINSAT6{0}.SPI.TeslimMiktar - FINSAT6{0}.FINSAT6{0}.SPI.KapatilanMiktar) AS Miktar, MIN(FINSAT6{0}.FINSAT6{0}.SPI.KayitSaat) as Saat " +
        //                            "FROM FINSAT6{0}.FINSAT6{0}.SPI WITH(NOLOCK) INNER JOIN FINSAT6{0}.FINSAT6{0}.STK WITH(NOLOCK) ON FINSAT6{0}.FINSAT6{0}.SPI.MalKodu = FINSAT6{0}.FINSAT6{0}.STK.MalKodu INNER JOIN FINSAT6{0}.FINSAT6{0}.MFK WITH(NOLOCK) ON FINSAT6{0}.FINSAT6{0}.SPI.EvrakNo = FINSAT6{0}.FINSAT6{0}.MFK.EvrakNo AND FINSAT6{0}.FINSAT6{0}.SPI.Tarih = FINSAT6{0}.FINSAT6{0}.MFK.EvrakTarih AND FINSAT6{0}.FINSAT6{0}.SPI.Chk = FINSAT6{0}.FINSAT6{0}.MFK.HesapKod AND FINSAT6{0}.FINSAT6{0}.SPI.KynkEvrakTip = FINSAT6{0}.FINSAT6{0}.MFK.KynkEvrakTip INNER JOIN FINSAT6{0}.FINSAT6{0}.CHK WITH(NOLOCK) ON FINSAT6{0}.FINSAT6{0}.SPI.Chk = FINSAT6{0}.FINSAT6{0}.CHK.HesapKodu " +
        //                            "WHERE (FINSAT6{0}.FINSAT6{0}.SPI.Tarih >= '{2}') AND (FINSAT6{0}.FINSAT6{0}.SPI.Tarih <= '{3}') AND (FINSAT6{0}.FINSAT6{0}.SPI.Depo = '{1}') AND (FINSAT6{0}.FINSAT6{0}.SPI.SiparisDurumu = 0) AND (FINSAT6{0}.FINSAT6{0}.SPI.KynkEvrakTip = 62) AND (FINSAT6{0}.FINSAT6{0}.SPI.Kod10 IN ('Terminal', 'Onaylandı')) AND (FINSAT6{0}.FINSAT6{0}.STK.Kod1 <> 'KKABLO') AND " +
        //                            "(FINSAT6{0}.FINSAT6{0}.SPI.BirimMiktar - FINSAT6{0}.FINSAT6{0}.SPI.TeslimMiktar - FINSAT6{0}.FINSAT6{0}.SPI.KapatilanMiktar) > 0 AND " +
        //                            "FINSAT6{0}.FINSAT6{0}.SPI.ROW_ID NOT IN (SELECT BIRIKIM.wms.IRS_Detay.KynkSiparisID FROM BIRIKIM.wms.IRS_Detay INNER JOIN BIRIKIM.wms.GorevIRS ON BIRIKIM.wms.IRS_Detay.IrsaliyeID = BIRIKIM.wms.GorevIRS.IrsaliyeID INNER JOIN BIRIKIM.wms.Gorev ON BIRIKIM.wms.GorevIRS.GorevID = BIRIKIM.wms.Gorev.ID WHERE (BIRIKIM.wms.Gorev.DurumID = 9 OR BIRIKIM.wms.Gorev.DurumID = 11) AND (NOT(BIRIKIM.wms.IRS_Detay.KynkSiparisID IS NULL)) GROUP BY BIRIKIM.wms.IRS_Detay.KynkSiparisID) " +
        //                            "GROUP BY FINSAT6{0}.FINSAT6{0}.SPI.EvrakNo, FINSAT6{0}.FINSAT6{0}.SPI.Tarih, FINSAT6{0}.FINSAT6{0}.SPI.Chk, FINSAT6{0}.FINSAT6{0}.CHK.Unvan1, FINSAT6{0}.FINSAT6{0}.CHK.GrupKod, FINSAT6{0}.FINSAT6{0}.CHK.FaturaAdres3, FINSAT6{0}.FINSAT6{0}.MFK.Aciklama", item, DepoID.ToString(), StartDate.ToOADateInt(), EndDate.ToOADateInt());

        //    //return list
        //    ViewBag.Depo = DepoID;
        //    ViewBag.Sirket = Sirket;
        //    ViewBag.CHK = CHK;
        //    try
        //    {
        //        var list = db.Database.SqlQuery<frmSiparisler>(sql).ToList();
        //        return PartialView("_Siparis", list);
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger(ex, "Sales/GetSiparis");
        //        return PartialView("_Siparis", new List<frmSiparisler>());
        //    }
        //}
    }


}