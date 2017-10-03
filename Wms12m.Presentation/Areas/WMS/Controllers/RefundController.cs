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
            ViewBag.DepoID = new SelectList(Store.GetList(vUser.DepoId), "DepoKodu", "DepoAd");
            //ViewBag.DepoID = new SelectList(Store.GetList(vUser.DepoId), "ID", "DepoAd");
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

        ///<summary>
        ///depo ve şirket seçince açık siparişler gelecek
        ///</summary>
        [HttpPost]
        public PartialViewResult GetSiparis(string Sirket, string DepoID, string CHK, string Starts, string Ends)
        {
            //ilk kontrol
            if (DepoID == "0" || Sirket == "0" || CHK == "") return null;
            bool tarihler = DateTime.TryParse(Starts, out DateTime StartDate); if (tarihler == false) return null;
            tarihler = DateTime.TryParse(Ends, out DateTime EndDate); if (tarihler == false) return null;
            if (StartDate > EndDate) return null;
            //perm kontrol
            if (CheckPerm(Perms.GenelSipariş, PermTypes.Reading) == false) return null;
            string sql = String.Format(@"SELECT 
'{0}' as SirketID, 
STI.EvrakNo, 
STI.Tarih, 
STI.Chk, 
CHK.Unvan1 AS Unvan, 
CHK.GrupKod, 
CHK.FaturaAdres3 AS FaturaAdres, 
MFK.Aciklama, 
COUNT(STI.MalKodu) AS Çeşit, 
SUM(STI.miktar) AS Miktar, 
MIN(STI.KayitSaat) as Saat 
FROM FINSAT6{0}.FINSAT6{0}.STI WITH(NOLOCK) 
INNER JOIN FINSAT6{0}.FINSAT6{0}.STK WITH(NOLOCK) ON STI.MalKodu = STK.MalKodu 
INNER JOIN FINSAT6{0}.FINSAT6{0}.MFK WITH(NOLOCK) ON STI.EvrakNo = MFK.EvrakNo AND STI.Tarih = MFK.EvrakTarih AND STI.Chk = MFK.HesapKod AND STI.KynkEvrakTip = MFK.KynkEvrakTip 
INNER JOIN FINSAT6{0}.FINSAT6{0}.CHK WITH(NOLOCK) ON STI.Chk = CHK.HesapKodu 
WHERE (STI.Tarih >= '{2}') AND (STI.Tarih <= '{3}') AND (STI.Depo = '{1}') AND (STI.KynkEvrakTip = 4) AND (STI.Chk='{4}')
GROUP BY STI.EvrakNo, STI.Tarih, STI.Chk, CHK.Unvan1, CHK.GrupKod, CHK.FaturaAdres3, MFK.Aciklama", Sirket, DepoID.ToString(), StartDate.ToOADateInt(), EndDate.ToOADateInt(), CHK);

            //return list
            ViewBag.Depo = DepoID;
            ViewBag.Sirket = Sirket;
            ViewBag.CHK = CHK;
            try
            {
                var list = db.Database.SqlQuery<frmSiparisler>(sql).ToList();
                return PartialView("SiparisList", list);
            }
            catch (Exception ex)
            {
                Logger(ex, "Refund/GetSiparis");
                return PartialView("SiparisList", new List<frmSiparisler>());
            }
        }

        /// <summary>
        /// evrak noya ait mallar
        /// </summary>
        [HttpPost]
        public JsonResult Details(string ID)
        {
            if (CheckPerm(Perms.GenelSipariş, PermTypes.Reading) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            string[] tmp = ID.Split('-');
            string sql = String.Format("FINSAT6{0}.wms.AlimIptalDetail @DepoKodu = '{1}', @EvrakNo = '{2}' @CHK='{3}'", tmp[0], tmp[1], tmp[2], tmp[3]);
            var list = db.Database.SqlQuery<frmSiparisMalzeme>(sql).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }


}