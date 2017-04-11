using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m.Presentation.Controllers
{
    public class TransferController : RootController
    {
        /// <summary>
        /// transfer planlama
        /// </summary>
        public ActionResult Index()
        {
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            ViewBag.GirisDepo = new SelectList(Store.GetList(), "DepoKodu", "DepoAd");
            ViewBag.CikisDepo = ViewBag.GirisDepo;
            return View("Index");
        }
        /// <summary>
        /// planlamadaki 1. adımdaki malzeme listesi
        /// </summary>
        public PartialViewResult Stock(string Id)
        {
            string[] ids = Id.Split('#');
            string sql = string.Format("SELECT STK.MalKodu, STK.MalAdi, STK.Birim1 as Birim, " +
                                        "(ISNULL(DST.DvrMiktar, 0) + ISNULL(DST.GirMiktar, 0) - ISNULL(DST.CikMiktar, 0)) as Depo1StokMiktar, ISNULL(DST.KritikStok, 0) as Depo1KritikMiktar, ((ISNULL(DST.DvrMiktar, 0) + ISNULL(DST.GirMiktar, 0) - ISNULL(DST.CikMiktar, 0)) - ISNULL(DST.KritikStok, 0)) as Depo1GerekenMiktar, ISNULL(DST.AlSiparis, 0) as AlSiparis, ISNULL(DST.SatSiparis, 0) as SatSiparis, " +
                                        "(ISNULL(DST2.DvrMiktar, 0) + ISNULL(DST2.GirMiktar, 0) - ISNULL(DST2.CikMiktar, 0)) - (SELECT isnull(SUM(Miktar - TeslimMiktar), 0) Miktar FROM  FINSAT6{0}.FINSAT6{0}.DTF(NOLOCK) WHERE CikDepo = '{2}' AND Durum = 0 and MalKodu = DST2.MalKodu) as Depo2StokMiktar, isnull(DST2.KritikStok, 0) as Depo2KritikMiktar, ((ISNULL(DST2.DvrMiktar, 0) + ISNULL(DST2.GirMiktar, 0) - ISNULL(DST2.CikMiktar, 0)) - isnull(DST2.KritikStok, 0)) as Depo2GerekenMiktar, CAST(0 AS DECIMAL) Depo2Miktar " +
                                        "FROM FINSAT6{0}.FINSAT6{0}.STK(NOLOCK) STK LEFT join FINSAT6{0}.FINSAT6{0}.DST(NOLOCK) DST ON STK.MalKodu = DST.MalKodu and DST.Depo = '{1}' LEFT JOIN FINSAT6{0}.FINSAT6{0}.DST(NOLOCK) DST2 ON STK.MalKodu = DST2.MalKodu AND DST2.Depo = '{2}' LEFT JOIN(SELECT MalKodu, SUM(Miktar-TeslimMiktar) Miktar FROM  FINSAT6{0}.FINSAT6{0}.DTF(NOLOCK) WHERE GirDepo = '{1}' AND Durum = 0 GROUP BY MalKodu) DTF ON DTF.MalKodu = STK.MalKodu " +
                                        "WHERE((ISNULL(DST.DvrMiktar, 0) + ISNULL(DST.GirMiktar, 0) - ISNULL(DST.CikMiktar, 0)) - ISNULL(DST.KritikStok, 0)) < 0  order by DST.MalKodu asc", ids[0], ids[1], ids[2]);
            var list = db.Database.SqlQuery<frmTransferMalzemeler>(sql).ToList();
            ViewBag.SirketID = ids[0];
            ViewBag.GirisDepo = ids[1];
            ViewBag.CikisDepo = ids[2];
            return PartialView("_Stock", list);
        }
        /// <summary>
        /// ilk sayfada seçtiklerini gösterip onaylatan bir sayfa
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Step2(frmTransferMalzemeApprove tbl)
        {
            if (tbl.SirketID == "" || tbl.GirisDepo == "" || tbl.CikisDepo == "" || tbl.checkboxes.ToString2() == "")
                return RedirectToAction("Index");
            tbl.checkboxes = tbl.checkboxes.Left(tbl.checkboxes.Length - 1);
            string[] tmp = tbl.checkboxes.Split('#');
            string malkodlari = "";
            var mallar = new List<frmMalKoduMiktar>();
            bool ilki = true; int sayi = 0;
            foreach (var item in tmp)
            {
                if (ilki == true)
                {
                    mallar.Add(new frmMalKoduMiktar() { MalKodu = item, Miktar = 0 });
                    if (malkodlari != "") malkodlari += ",";
                    malkodlari += "'" + item + "'";
                    ilki = false;
                }
                else
                {
                    mallar[sayi].Miktar = item.ToDecimal();
                    sayi++;
                    ilki = true;
                }
            }
            string sql = string.Format("SELECT STK.MalKodu, STK.MalAdi, STK.Birim1 as Birim, " +
                                        "(ISNULL(DST.DvrMiktar, 0) + ISNULL(DST.GirMiktar, 0) - ISNULL(DST.CikMiktar, 0)) as Depo1StokMiktar, ISNULL(DST.KritikStok, 0) as Depo1KritikMiktar, ((ISNULL(DST.DvrMiktar, 0) + ISNULL(DST.GirMiktar, 0) - ISNULL(DST.CikMiktar, 0)) - ISNULL(DST.KritikStok, 0)) as Depo1GerekenMiktar, ISNULL(DST.AlSiparis, 0) as AlSiparis, ISNULL(DST.SatSiparis, 0) as SatSiparis, " +
                                        "(ISNULL(DST2.DvrMiktar, 0) + ISNULL(DST2.GirMiktar, 0) - ISNULL(DST2.CikMiktar, 0)) - (SELECT isnull(SUM(Miktar - TeslimMiktar), 0) Miktar FROM  FINSAT6{0}.FINSAT6{0}.DTF(NOLOCK) WHERE CikDepo = '{2}' AND Durum = 0 and MalKodu = DST2.MalKodu) as Depo2StokMiktar, isnull(DST2.KritikStok, 0) as Depo2KritikMiktar, ((ISNULL(DST2.DvrMiktar, 0) + ISNULL(DST2.GirMiktar, 0) - ISNULL(DST2.CikMiktar, 0)) - isnull(DST2.KritikStok, 0)) as Depo2GerekenMiktar, CAST(0 AS DECIMAL) Depo2Miktar " +
                                        "FROM FINSAT6{0}.FINSAT6{0}.STK(NOLOCK) STK LEFT join FINSAT6{0}.FINSAT6{0}.DST(NOLOCK) DST ON STK.MalKodu = DST.MalKodu and DST.Depo = '{1}' LEFT JOIN FINSAT6{0}.FINSAT6{0}.DST(NOLOCK) DST2 ON STK.MalKodu = DST2.MalKodu AND DST2.Depo = '{2}' LEFT JOIN(SELECT MalKodu, SUM(Miktar-TeslimMiktar) Miktar FROM  FINSAT6{0}.FINSAT6{0}.DTF(NOLOCK) WHERE GirDepo = '{1}' AND Durum = 0 GROUP BY MalKodu) DTF ON DTF.MalKodu = STK.MalKodu " +
                                        "WHERE((ISNULL(DST.DvrMiktar, 0) + ISNULL(DST.GirMiktar, 0) - ISNULL(DST.CikMiktar, 0)) - ISNULL(DST.KritikStok, 0)) < 0 AND (STK.MalKodu IN ({3}))  order by DST.MalKodu asc", tbl.SirketID, tbl.GirisDepo, tbl.CikisDepo, malkodlari);
            var list = db.Database.SqlQuery<frmTransferMalzemeler>(sql).ToList();
            ViewBag.AraDepo = new SelectList(Store.GetList(), "DepoKodu", "DepoAd");
            ViewBag.SirketID = tbl.SirketID;
            ViewBag.GirisDepo = tbl.GirisDepo;
            ViewBag.CikisDepo = tbl.CikisDepo;
            ViewBag.checkboxes = tbl.checkboxes;
            ViewBag.mallar = mallar;
            return View("Step2", list);
        }
        /// <summary>
        /// transfer onaylama
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Approve(frmTransferMalzemeApprove tbl)
        {
            if (tbl.SirketID == "" || tbl.GirisDepo == "" || tbl.CikisDepo == "" || tbl.checkboxes.ToString2() == "")
                return RedirectToAction("Index");
            tbl.checkboxes = tbl.checkboxes.Left(tbl.checkboxes.Length - 1);
            string[] tmp = tbl.checkboxes.Split('#');
            string malkodlari = "";
            var mallar = new List<frmMalKoduMiktar>();
            bool ilki = true; int sayi = 0;
            foreach (var item in tmp)
            {
                if (ilki == true)
                {
                    mallar.Add(new frmMalKoduMiktar() { MalKodu = item, Miktar = 0 });
                    if (malkodlari != "") malkodlari += ",";
                    malkodlari += "'" + item + "'";
                    ilki = false;
                }
                else
                {
                    mallar[sayi].Miktar = item.ToDecimal();
                    sayi++;
                    ilki = true;
                }
            }
            int aDepoID = Store.Detail(tbl.AraDepo).ID;
            int cDepoID = Store.Detail(tbl.CikisDepo).ID;
            int gDepoID = Store.Detail(tbl.GirisDepo).ID;
            Transfers.Operation(new Transfer() { SirketKod = tbl.SirketID, GirisDepoID = gDepoID, CikisDepoID = cDepoID, AraDepoID = aDepoID, Malzemeler = tbl.checkboxes });
            return RedirectToAction("List");
        }
        /// <summary>
        /// onay bekleyen transfer lsitesi
        /// </summary>
        public ActionResult List()
        {
            return View("List", Transfers.GetList(false));
        }
    }
}