using System;
using System.Linq;
using System.Web.Mvc;
using Wms12m.Business;
using Wms12m.Entity;

namespace Wms12m.Presentation.Controllers
{
    public class CableController : RootController
    {
        /// <summary>
        /// kablo sipariş ana sayfası
        /// </summary>
        public ActionResult Index()
        {
            ViewBag.DepoID = new SelectList(Store.GetList(), "DepoKodu", "DepoAd");
            return View("Index");
        }
        /// <summary>
        /// depo ve şirket seçince açık siparişler gelecek
        /// </summary>
        [HttpPost]
        public PartialViewResult GetSiparis(string DepoID)
        {
            if (DepoID == "0") return null;
            string sql = "";
            var tmp = db.GetSirketDBs().ToList();
            foreach (var item in tmp)
            {
                if (sql != "") sql += " union ";
                sql += String.Format("SELECT '{0}' as SirketID, FINSAT6{0}.FINSAT6{0}.SPI.EvrakNo, FINSAT6{0}.FINSAT6{0}.SPI.Tarih, FINSAT6{0}.FINSAT6{0}.SPI.Chk, FINSAT6{0}.FINSAT6{0}.CHK.Unvan1 AS Unvan, FINSAT6{0}.FINSAT6{0}.CHK.GrupKod, FINSAT6{0}.FINSAT6{0}.CHK.FaturaAdres3 AS FaturaAdres, FINSAT6{0}.FINSAT6{0}.MFK.Aciklama, COUNT(FINSAT6{0}.FINSAT6{0}.SPI.MalKodu) AS Çeşit, SUM(FINSAT6{0}.FINSAT6{0}.SPI.BirimMiktar - FINSAT6{0}.FINSAT6{0}.SPI.TeslimMiktar - FINSAT6{0}.FINSAT6{0}.SPI.KapatilanMiktar) AS Miktar, MIN(FINSAT6{0}.FINSAT6{0}.SPI.KayitSaat) as Saat " +
                                    "FROM FINSAT6{0}.FINSAT6{0}.SPI WITH(NOLOCK) INNER JOIN FINSAT6{0}.FINSAT6{0}.MFK WITH(NOLOCK) ON FINSAT6{0}.FINSAT6{0}.SPI.EvrakNo = FINSAT6{0}.FINSAT6{0}.MFK.EvrakNo AND FINSAT6{0}.FINSAT6{0}.SPI.Tarih = FINSAT6{0}.FINSAT6{0}.MFK.EvrakTarih AND FINSAT6{0}.FINSAT6{0}.SPI.Chk = FINSAT6{0}.FINSAT6{0}.MFK.HesapKod AND FINSAT6{0}.FINSAT6{0}.SPI.KynkEvrakTip = FINSAT6{0}.FINSAT6{0}.MFK.KynkEvrakTip INNER JOIN FINSAT6{0}.FINSAT6{0}.CHK WITH(NOLOCK) ON FINSAT6{0}.FINSAT6{0}.SPI.Chk = FINSAT6{0}.FINSAT6{0}.CHK.HesapKodu " +
                                    "WHERE (FINSAT6{0}.FINSAT6{0}.SPI.Depo = '{1}') AND (FINSAT6{0}.FINSAT6{0}.SPI.SiparisDurumu = 0) AND (FINSAT6{0}.FINSAT6{0}.SPI.KynkEvrakTip = 62) AND (FINSAT6{0}.FINSAT6{0}.SPI.Kod10 IN ('Terminal', 'Onaylandı')) AND ((FINSAT6{0}.FINSAT6{0}.SPI.EvrakNo LIKE 'KA%')) " +
                                    "GROUP BY FINSAT6{0}.FINSAT6{0}.SPI.EvrakNo, FINSAT6{0}.FINSAT6{0}.SPI.Tarih, FINSAT6{0}.FINSAT6{0}.SPI.Chk, FINSAT6{0}.FINSAT6{0}.CHK.Unvan1, FINSAT6{0}.FINSAT6{0}.CHK.GrupKod, FINSAT6{0}.FINSAT6{0}.CHK.FaturaAdres3, FINSAT6{0}.FINSAT6{0}.MFK.Aciklama", item, DepoID.ToString());
            }
            ViewBag.Depo = DepoID;
            try
            {
                var list = db.Database.SqlQuery<frmSiparisler>(sql).ToList();
                return PartialView("_Siparis", list);
            }
            catch (Exception ex)
            {
                db.Logger(vUser.UserName, "", fn.GetIPAddress(), ex.Message, ex.InnerException != null ? ex.InnerException.Message : "", "Sell/GetSiparis");
                return null;
            }
        }
        /// <summary>
        /// evrak noya ait mallar
        /// </summary>
        [HttpPost]
        public JsonResult Details(string ID)
        {
            string[] tmp = ID.Split('-');
            string sql = String.Format("SELECT FINSAT6{0}.FINSAT6{0}.SPI.MalKodu, FINSAT6{0}.FINSAT6{0}.STK.MalAdi, FINSAT6{0}.FINSAT6{0}.SPI.BirimMiktar - FINSAT6{0}.FINSAT6{0}.SPI.KapatilanMiktar - FINSAT6{0}.FINSAT6{0}.SPI.TeslimMiktar AS Miktar, FINSAT6{0}.FINSAT6{0}.SPI.Birim " +
                        "FROM FINSAT6{0}.FINSAT6{0}.SPI WITH(NOLOCK) INNER JOIN FINSAT6{0}.FINSAT6{0}.STK WITH(NOLOCK) ON FINSAT6{0}.FINSAT6{0}.SPI.MalKodu = FINSAT6{0}.FINSAT6{0}.STK.MalKodu " +
                        "WHERE (FINSAT6{0}.FINSAT6{0}.SPI.Depo = '{1}') AND (FINSAT6{0}.FINSAT6{0}.SPI.EvrakNo = '{2}') AND (FINSAT6{0}.FINSAT6{0}.SPI.KynkEvrakTip = 62) AND (FINSAT6{0}.FINSAT6{0}.SPI.SiparisDurumu = 0) AND (FINSAT6{0}.FINSAT6{0}.SPI.Kod10 IN ('Terminal', 'Onaylandı'))", tmp[0], tmp[1], tmp[2]);
            var list = db.Database.SqlQuery<frmSiparisMalzeme>(sql).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}