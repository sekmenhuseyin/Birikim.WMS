using System;
using System.Linq;
using System.Web.Mvc;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m.Presentation.Controllers
{
    public class ListsController : RootController
    {
        /// <summary>
        /// listeler
        /// </summary>
        public ActionResult Index()
        {
            return View("Index");
        }
        /// <summary>
        /// siparişler
        /// </summary>
        public ActionResult Siparis()
        {
            ViewBag.Siparis = new SelectList(db.IRS.Where(m => m.IslemTur == true && m.Onay == false).ToList(), "ID", "EvrakNo");
            return View("Siparis");
        }
        /// <summary>
        /// siparişi seçince gelen liste
        /// </summary>
        public PartialViewResult GetSiparisDetails()
        {
            var id = Url.RequestContext.RouteData.Values["id"];
            if (id == null || id.ToString2() == "0") return null;
            int ID = id.ToInt32();
            var list = db.IRS_Detay.Where(m => m.IrsaliyeID == ID).ToList();
            return PartialView("_SiparisDetails", list);
        }
        /// <summary>
        /// raf kaldırmalar
        /// </summary>
        public ActionResult Raf()
        {
            ViewBag.Siparis = new SelectList(db.IRS.Where(m => m.IslemTur == false && m.Onay == false).ToList(), "ID", "EvrakNo");
            return View("Raf");
        }
        /// <summary>
        /// siparişi seçince gelen liste
        /// </summary>
        public PartialViewResult GetRafDetails()
        {
            var id = Url.RequestContext.RouteData.Values["id"];
            if (id == null || id.ToString2() == "0") return null;
            int ID = id.ToInt32();
            var list = db.IRS_Detay.Where(m => m.IrsaliyeID == ID).ToList();
            int DepoID = db.IRS.Where(m => m.ID == ID).Select(m => m.DepoID).FirstOrDefault();
            ViewBag.KatID = new SelectList(db.GetHucreAd(DepoID).ToList(), "ID", "Ad");
            return PartialView("_RafDetails", list);
        }
        /// <summary>
        /// rafa ekleme
        /// </summary>
        [HttpPost]
        public void AddRaf(frmSiparisMalzeme tbl)
        {
            //yerleştirme tablosu
            Yer yer = new Yer();
            yer.KatID = tbl.KatID;
            yer.MalKodu = tbl.MalKodu;
            yer.Miktar = tbl.Miktar;
            yer.Birim = tbl.Birim;
            db.Yers.Add(yer);
            //yerleştirme harekletler
            Yer_Log yerlog = new Yer_Log();
            yerlog.HucreAd = tbl.MalAdi;
            yerlog.MalKodu = tbl.MalKodu;
            yerlog.Miktar = tbl.Miktar;
            yerlog.Birim = tbl.Birim;
            yerlog.GC = false;
            yerlog.Kaydeden = User.LogonUserName;
            yerlog.KayitTarihi = DateTime.Today.ToOADateInt();
            yerlog.KayitSaati = DateTime.Now.SaatiAl();
            db.Yer_Log.Add(yerlog);
            db.SaveChanges();
        }
    }
}