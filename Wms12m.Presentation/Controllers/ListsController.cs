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
        public ActionResult Gorev()
        {
            ViewBag.Gorev = new SelectList(Task.GetList(ComboItems.SiparişTopla.ToInt32()), "ID", "GorevNo");
            return View("Gorev");
        }
        /// <summary>
        /// siparişi seçince gelen liste
        /// </summary>
        public PartialViewResult GetGorevDetails()
        {
            var id = Url.RequestContext.RouteData.Values["id"];
            if (id == null || id.ToString2() == "0") return null;
            int ID = id.ToInt32();
            //get gorev yer
            var tablo = TaskYer.GetList(ID);
            if (tablo.Count == 0)
            {
                //get gorev details
                var gList = db.GetIrsDetayfromGorev(ID).ToList();
                foreach (var item in gList)
                {
                    var tmp = db.Yers.Where(m => m.MalKodu == item.MalKodu && m.Birim == item.Birim).OrderBy(m=>m.Miktar).ToList();
                    decimal toplam = 0, miktar = 0;
                    if (tmp != null)
                    {
                        foreach (var itemyer in tmp)
                        {
                            if (itemyer.Miktar >= (item.Miktar - toplam))
                                miktar = item.Miktar.Value - toplam;
                            else
                                miktar = itemyer.Miktar;
                            toplam += miktar;
                            //miktarı tabloya ekle
                            GorevYer tbl = new GorevYer();
                            tbl.GorevID = item.ID;
                            tbl.YerID = itemyer.ID;
                            tbl.MalKodu = item.MalKodu;
                            tbl.Birim = item.Birim;
                            tbl.Miktar = miktar;
                            tbl.GC = true;
                            TaskYer.Operation(tbl);
                            //toplam yeterli miktardaysa
                            if (toplam == item.Miktar) break;
                        }
                    }
                }
                tablo = TaskYer.GetList(ID);
            }
            return PartialView("_GorevDetails", tablo);
        }
        /// <summary>
        /// raf kaldırmalar
        /// </summary>
        public ActionResult Raf()
        {
            ViewBag.Irsaliye = new SelectList(db.IRS.Where(m => m.IslemTur == false && m.Onay == false).ToList(), "ID", "EvrakNo");
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
            yerlog.Kaydeden = vUser.Id;
            yerlog.KayitTarihi = DateTime.Today.ToOADateInt();
            yerlog.KayitSaati = DateTime.Now.SaatiAl();
            db.Yer_Log.Add(yerlog);
            db.SaveChanges();
        }
    }
}