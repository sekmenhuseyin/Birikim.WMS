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
            ViewBag.AraDepo = ViewBag.GirisDepo;
            return View("Index");
        }
        /// <summary>
        /// transfere ait mallar
        /// </summary>
        [HttpPost]
        public PartialViewResult Details(int ID)
        {
            var list = db.Transfer_Detay.Where(m => m.TransferID == ID).Select(m => new frmMalKoduMiktar { MalKodu = m.MalKodu, Miktar = m.Miktar, Birim = m.Birim }).ToList();
            return PartialView("_Details", list);
        }
        /// <summary>
        /// ilk sayfada seçtiklerini gösterip onaylatan bir sayfa
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult PlanOnay(frmTransferMalzemeApprove tbl)
        {
            if (tbl.SirketID == "" || tbl.GirisDepo == "" || tbl.CikisDepo == "" || tbl.checkboxes.ToString2() == "")
                return RedirectToAction("Index");
            //add to list
            int aDepoID = Store.Detail(tbl.AraDepo).ID;
            var cDepoID = Store.Detail(tbl.CikisDepo);
            var gDepoID = Store.Detail(tbl.GirisDepo);
            //tüm diğer başlamamış görevler silinir
            Task.DeleteSome();
            //yeni bir görev eklenir
            string GorevNo = db.SettingsGorevNo(fn.ToOADate()).FirstOrDefault();
            var gorev = new Gorev() { GorevTipiID = ComboItems.Transfer.ToInt32(), DepoID = gDepoID.ID, GorevNo = GorevNo, DurumID = ComboItems.Başlamamış.ToInt32(), Bilgi = "Giriş: " + gDepoID.DepoAd + ", Çıkış: " + cDepoID.DepoAd };
            var sonuc = Task.Operation(gorev);
            if (sonuc.Status == false)
                return RedirectToAction("Index");
            //yeni transfer eklenir
            sonuc = Transfers.Operation(new Transfer() { SirketKod = tbl.SirketID, GirisDepoID = gDepoID.ID, CikisDepoID = cDepoID.ID, AraDepoID = aDepoID, GorevID = sonuc.Id });
            if (sonuc.Status == false)
                return RedirectToAction("Index");
            //find detays
            tbl.checkboxes = tbl.checkboxes.Left(tbl.checkboxes.Length - 1);
            string[] tmp = tbl.checkboxes.Split('#');
            string malkodlari = ""; int sira = 0;
            var mallar = new Transfer_Detay();
            foreach (var item in tmp)
            {
                if (sira == 0)
                {
                    mallar.TransferID = sonuc.Id;
                    mallar.MalKodu = item;
                    if (malkodlari != "") malkodlari += ",";
                    malkodlari += "'" + item + "'";
                    sira++;
                }
                else if (sira == 1)
                {
                    mallar.Birim = item;
                    sira++;
                }
                else
                {
                    mallar.Miktar = item.ToDecimal();
                    Transfers.AddDetay(mallar);
                    mallar = new Transfer_Detay();
                    sira = 0;
                }
            }
            //return
            return RedirectToAction("List");
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
        /// onay bekleyen transfer lsitesi
        /// </summary>
        public ActionResult List()
        {
            var list = Transfers.GetList(false);
            return View("List", list);
        }
        /// <summary>
        /// bekleyen transferi onayla
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Approve(int ID)
        {
            //get and set transfer details
            var tbl = Transfers.Detail(ID);
            tbl.Onay = true;
            Transfers.Operation(tbl);
            var tbl2 = db.Gorevs.Where(m => m.ID == tbl.GorevID).FirstOrDefault();
            tbl2.DurumID = ComboItems.Açık.ToInt32();
            db.SaveChanges();
            //return
            return RedirectToAction("List");
        }
    }
}