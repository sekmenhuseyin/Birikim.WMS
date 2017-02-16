using System;
using System.Linq;
using System.Web.Mvc;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m.Presentation.Controllers
{
    public class BuyController : RootController
    {

        // GET: Buy
        public ActionResult Index()
        {
            return View("Index", new frmIrsaliye());
        }
        //yeni irsaliye fatura
        [HttpPost, ValidateAntiForgeryToken]
        public PartialViewResult New(frmIrsaliye tbl)
        {
            //check if exists
            var tmp = db.IRS.Where(m => m.EvrakNo == tbl.EvrakNo).FirstOrDefault();
            if (tmp == null)
            {
                //add new
                IR tablo = new IR();
                tablo.EvrakNo = tbl.EvrakNo;
                tablo.HesapKodu = tbl.HesapKodu;
                tablo.Tarih = tbl.Tarih;
                db.IRS.Add(tablo);
                db.SaveChanges();
                tbl.Id = tablo.ID;
            }
            else
            {
                tbl.Id = tmp.ID;
            }
            //get list
            var list = db.STIs.Where(m => m.IrsaliyeID == tbl.Id).ToList();
            ViewBag.IrsaliyeId = tbl.Id;
            return PartialView("_GridPartial", list);
        }
        //yeni malzeme
        [HttpPost, ValidateAntiForgeryToken]
        public PartialViewResult InsertMalzeme(frmMalzeme tbl)
        {
            //add new
            STI tablo = new STI();
            tablo.IrsaliyeID = tbl.IrsaliyeId;
            tablo.MalKodu = tbl.MalKodu;
            tablo.Miktar = tbl.Miktar;
            tablo.Birim = tbl.Birim;
            db.STIs.Add(tablo);
            db.SaveChanges();
            //get list
            var list = db.STIs.Where(m => m.IrsaliyeID == tablo.IrsaliyeID).ToList();
            ViewBag.IrsaliyeId = tablo.ID;
            return PartialView("_GridPartial", list);
        }
        //malzeme autocomplete and search
        //[HttpPost]
        public JsonResult getMalzeme(frmMalzemeSearch tbl)
        {
            var list = db.GetMalzeme(tbl.kod, tbl.ad).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        //anasayfadaki malzeme listesi
        public PartialViewResult GridPartial()
        {
            var list = db.STIs.ToList();
            return PartialView("_GridPartial", list);
        }
        //anasayfadaki malzeme listesi
        public PartialViewResult GetHesapCodes()
        {
            var list = db.GetHesapCodes().ToList();
            return PartialView("_HesapGridPartial", list);
        }
        // GET: Buy
        public PartialViewResult NewMalzemeForm(int id)
        {
            ViewBag.IrsaliyeId = id;
            return PartialView("_GridNewPartial", new frmMalzeme());
        }
    }
}