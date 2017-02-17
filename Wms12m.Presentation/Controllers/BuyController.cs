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
            ViewBag.DepoID = new SelectList(db.TK_DEP.ToList(), "ID", "DepoAdi");
            return View("Index", new frmIrsaliye());
        }
        //yeni irsaliye fatura
        [HttpPost, ValidateAntiForgeryToken]
        public PartialViewResult New(frmIrsaliye tbl)
        {
            //check if exists
            var tmp = db.IRS.Where(m => m.EvrakNo == tbl.EvrakNo).FirstOrDefault();
            if (tmp == null)
                tbl.Id = db.UpdateIrsaliye(0, tbl.DepoID, false, tbl.EvrakNo, tbl.HesapKodu, "", Convert.ToInt32(tbl.Tarih.ToOADate()), User.LogonUserName, Convert.ToInt32(tbl.Tarih.ToOADate()), User.Id).FirstOrDefault().Value;
            else
                tbl.Id = tmp.ID;
            //get list
            var list = db.GetIrsaliyeSTI(tbl.Id).ToList();
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
            var list = db.GetIrsaliyeSTI(tablo.IrsaliyeID).ToList();
            ViewBag.IrsaliyeId = tablo.ID;
            return PartialView("_GridPartial", list);
        }
        //malzeme autocomplete and search
        [HttpPost]
        public JsonResult getMalzeme(frmMalzemeSearch tbl)
        {
            var list = db.GetMalzeme(tbl.kod, tbl.ad).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        //malzeme koduna göre birim getirir
        [HttpPost]
        public JsonResult getBirim(string kod)
        {
            var list = db.GetMalBirim(kod).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
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