using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m.Presentation.Controllers
{
    public class StockController : RootController
    {
        /// <summary>
        /// stok ana sayfası
        /// </summary>
        public ActionResult Index()
        {
            ViewBag.DepoID = new SelectList(Store.GetList(), "ID", "DepoAd");
            return View("Index");
        }
        /// <summary>
        /// listeyi yeniler
        /// </summary>
        [HttpPost]
        public PartialViewResult List(string Id)
        {
            //dbler tempe aktarılıyor
            var list = db.GetSirketDBs();
            List<string> liste = new List<string>();
            foreach (var item in list) { liste.Add(item); }
            ViewBag.Sirket = liste;
            //id'ye göre liste döner
            string[] ids = Id.Split('#');
            try
            {
                if (ids[2] != "0") //bir raftaki malzemeler
                    return PartialView("List", Yerlestirme.GetListFromRaf(ids[2].ToInt32()));
                else if (ids[1] != "0") //bir koridora ait malzemeler
                    return PartialView("List", Yerlestirme.GetListFromKoridor(ids[1].ToInt32()));
                else// if (ids[0] != "0") //tüm depoya ait malzemeler: burada timeout verebilir
                    return PartialView("List", Yerlestirme.GetListFromDepo(ids[0].ToInt32()));
            }
            catch (System.Exception)
            {
                return PartialView("_List", new List<Yer>());
            }
        }
        /// <summary>
        /// mal hareketleri
        /// </summary>
        public ActionResult History()
        {
            var list = db.Yer_Log.GroupBy(m => m.MalKodu).Select(m => new frmMalKoduMiktar { MalKodu = m.Key, Birim = "", Miktar = 0 }).ToList();
            ViewBag.MalKodu = new SelectList(list, "MalKodu", "MalKodu");
            return View("History");
        }
        /// <summary>
        /// hareketler alt sayfa
        /// </summary>
        [HttpPost]
        public PartialViewResult Movements(string Id)
        {
            var list = db.Yer_Log.Where(m => m.MalKodu == Id).OrderBy(m => m.KayitTarihi).ToList();
            return PartialView("_Movements", list);
        }
    }
}