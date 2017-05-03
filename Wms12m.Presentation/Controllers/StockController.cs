using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Wms12m.Business;
using Wms12m.Entity;
using Wms12m.Entity.Models;
using Wms12m.Entity.Mysql;

namespace Wms12m.Presentation.Controllers
{
    public class StockController : RootController
    {
        /// <summary>
        /// stok ana sayfası
        /// </summary>
        public ActionResult Index()
        {
            if (CheckPerm("Stock", PermTypes.Reading) == false) return Redirect("/");
            ViewBag.DepoID = new SelectList(Store.GetList(vUser.DepoId), "ID", "DepoAd");
            return View("Index");
        }
        /// <summary>
        /// listeyi yeniler
        /// </summary>
        [HttpPost]
        public PartialViewResult List(string Id)
        {
            if (CheckPerm("Stock", PermTypes.Reading) == false) return null;
            //dbler tempe aktarılıyor
            var list = db.GetSirketDBs();
            List<string> liste = new List<string>();
            foreach (var item in list) { liste.Add(item); }
            ViewBag.Sirket = liste;
            //id'ye göre liste döner
            string[] ids = Id.Split('#');
            ViewBag.Manual = false;
            try
            {
                if (ids[2] != "0" && ids[2].ToString2() != "") //bir kattaki ait malzemeler
                {
                    ViewBag.Manual = true;
                    return PartialView("List", Yerlestirme.GetList(ids[2].ToInt32()));
                }
                else if (ids[1] != "0" && ids[1].ToString2() != "") //bir raftaki ait malzemeler
                    return PartialView("List", Yerlestirme.GetListFromRaf(ids[1].ToInt32()));
                else// if (ids[0] != "0") //tüm depoya ait malzemeler: burada timeout verebilir
                    return PartialView("List", Yerlestirme.GetListFromDepo(ids[0].ToInt32()));
            }
            catch (System.Exception ex)
            {
                Logger(ex, "Stock/List");
                return PartialView("List", new List<Yer>());
            }
        }
        /// <summary>
        /// kablo stok ana sayfası
        /// </summary>
        public ActionResult Cable()
        {
            if (CheckPerm("Stock", PermTypes.Reading) == false) return Redirect("/");
            ViewBag.DepoID = new SelectList(Store.GetListCable(vUser.DepoId), "ID", "DepoAd");
            return View("Cable");
        }
        /// <summary>
        /// kablo stoğunu getirir
        /// </summary>
        [HttpPost]
        public PartialViewResult CableList(int Id)
        {
            if (CheckPerm("Stock", PermTypes.Reading) == false) return null;
            using (KabloEntities dbx = new KabloEntities())
            {
                var kblDepoID = Store.Detail(Id).KabloDepoID;
                var depo = dbx.depoes.Where(m => m.id == kblDepoID).Select(m => m.depo1).FirstOrDefault();
                var list = dbx.kblstoks.Where(m => m.depo == depo).ToList();
                return PartialView("CableList", list);
            }
        }
        /// <summary>
        /// evrak noya ait mallar
        /// </summary>
        [HttpPost]
        public JsonResult CableMovements(int ID)
        {
            if (CheckPerm("Stock", PermTypes.Reading) == false) return null;
            using (KabloEntities dbx = new KabloEntities())
            {
                var list = dbx.harekets.Where(m => m.id == ID).OrderBy(m => m.tarih).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// mal hareketleri
        /// </summary>
        public ActionResult History()
        {
            if (CheckPerm("Stock", PermTypes.Reading) == false) return Redirect("/");
            ViewBag.DepoID = new SelectList(Store.GetList(vUser.DepoId), "ID", "DepoAd");
            ViewBag.SirketID = db.GetSirketDBs().FirstOrDefault();
            return View("History");
        }
        /// <summary>
        /// hareketler alt sayfa
        /// </summary>
        [HttpPost]
        public PartialViewResult Movements(string Id)
        {
            if (CheckPerm("Stock", PermTypes.Reading) == false) return null;
            if (Id.Contains("#") == false) return null;
            var ids = Id.Split('#');
            var depoID = ids[1].ToInt32();
            var depoKodu = Store.Detail(depoID).DepoKodu;
            string kod = ids[0];
            string sql = "";
            //get stok from finsat
            var tmps = db.GetSirketDBs();
            foreach (var item in tmps)
            {
                if (sql != "") sql += " UNION ";
                sql += string.Format("SELECT FINSAT6{0}.FINSAT6{0}.DST.DvrMiktar + FINSAT6{0}.FINSAT6{0}.DST.GirMiktar - FINSAT6{0}.FINSAT6{0}.DST.CikMiktar AS stok " +
                    "FROM FINSAT6{0}.FINSAT6{0}.DST INNER JOIN FINSAT6{0}.FINSAT6{0}.STK ON FINSAT6{0}.FINSAT6{0}.DST.MalKodu = FINSAT6{0}.FINSAT6{0}.STK.MalKodu " +
                    "WHERE (FINSAT6{0}.FINSAT6{0}.DST.Depo = '{1}') AND (FINSAT6{0}.FINSAT6{0}.DST.MalKodu = '{2}') AND (FINSAT6{0}.FINSAT6{0}.DST.DvrMiktar + FINSAT6{0}.FINSAT6{0}.DST.GirMiktar - FINSAT6{0}.FINSAT6{0}.DST.CikMiktar > 0)", item, depoKodu, kod);
            }
            sql = "SELECT ISNULL(SUM(Stok),0) as Stok from (" + sql + ")t";
            //return
            var list = db.Yer_Log.Where(m => m.MalKodu == kod && m.Kat.Bolum.Raf.Koridor.DepoID == depoID).OrderBy(m => m.KayitTarihi).ToList();
            ViewBag.Stok = db.Database.SqlQuery<decimal>(sql).FirstOrDefault();
            return PartialView("_Movements", list);
        }
        /// <summary>
        /// manual yerleştir
        /// </summary>
        public ActionResult ManualPlacement()
        {
            if (CheckPerm("Stock", PermTypes.Reading) == false) return Redirect("/");
            ViewBag.DepoID = new SelectList(Store.GetList(vUser.DepoId), "ID", "DepoAd");
            ViewBag.KoridorID = new SelectList(Corridor.GetList(0), "ID", "KoridorAd");
            ViewBag.RafID = new SelectList(Shelf.GetList(0), "ID", "RafAd");
            ViewBag.BolumID = new SelectList(Section.GetList(0), "ID", "BolumAd");
            ViewBag.KatID = new SelectList(Floor.GetList(0), "ID", "KatAd");
            ViewBag.SirketID = db.GetSirketDBs().FirstOrDefault();
            return View("ManualPlacement", new Yer());
        }
        /// <summary>
        /// manual yerleştir
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public JsonResult ManualPlacement(Yer tbl)
        {
            if (CheckPerm("Stock", PermTypes.Writing) == false) return Json(false, JsonRequestBehavior.AllowGet);
            //yerleştirme kaydı yapılır
            var stok = new Yerlestirme();
            var tmp2 = stok.Detail(tbl.KatID, tbl.MalKodu, tbl.Birim);
            if (tmp2 == null)
            {
                tmp2 = new Yer()
                {
                    KatID = tbl.KatID,
                    MalKodu = tbl.MalKodu,
                    Birim = tbl.Birim,
                    Miktar = tbl.Miktar
                };
                stok.Insert(tmp2, 0, vUser.Id);
            }
            else
            {
                tmp2.Miktar += tbl.Miktar;
                stok.Update(tmp2, 0, vUser.Id, false, tbl.Miktar);
            }
            //return
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// manual yer değiştir
        /// </summary>
        public ActionResult ManualMovement()
        {
            if (CheckPerm("Stock", PermTypes.Reading) == false) return Redirect("/");
            ViewBag.DepoID = new SelectList(Store.GetList(vUser.DepoId), "ID", "DepoAd");
            ViewBag.KoridorID = new SelectList(Corridor.GetList(0), "ID", "KoridorAd");
            ViewBag.RafID = new SelectList(Shelf.GetList(0), "ID", "RafAd");
            ViewBag.BolumID = new SelectList(Section.GetList(0), "ID", "BolumAd");
            ViewBag.KatID = new SelectList(Floor.GetList(0), "ID", "KatAd");
            return View("ManualMovement", new Yer());
        }
        /// <summary>
        /// 
        /// </summary>
        [HttpPost]
        public PartialViewResult ManualNewPlace(int Id)
        {
            if (CheckPerm("Stock", PermTypes.Reading) == false) return null;
            return PartialView("ManualNewPlace");
        }
    }
}