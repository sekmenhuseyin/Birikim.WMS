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
            try
            {
                if (ids[2] != "0" && ids[2].ToString2() != "") //bir raftaki ait malzemeler
                    return PartialView("List", Yerlestirme.GetList(ids[2].ToInt32()));
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
            var list = db.Yer_Log.GroupBy(m => m.MalKodu).Select(m => new frmMalKoduMiktar { MalKodu = m.Key, Birim = "", Miktar = 0 }).ToList();
            ViewBag.MalKodu = new SelectList(list, "MalKodu", "MalKodu");
            ViewBag.DepoID = new SelectList(Store.GetList(vUser.DepoId), "ID", "DepoAd");
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
            string kod = ids[0];
            var list = db.Yer_Log.Where(m => m.MalKodu == kod && m.Kat.Bolum.Raf.Koridor.DepoID == depoID).OrderBy(m => m.KayitTarihi).ToList();
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
    }
}