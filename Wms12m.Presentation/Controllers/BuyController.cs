using System;
using System.Linq;
using System.Web.Mvc;
using Wms12m.Business;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m.Presentation.Controllers
{
    public class BuyController : RootController
    {

        /// <summary>
        /// irsaliye sayfası
        /// </summary>
        public ActionResult Index()
        {
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            ViewBag.DepoID = new SelectList(db.TK_DEP.ToList(), "ID", "DepoAdi");
            return View("Index", new frmIrsaliye());
        }
        /// <summary>
        /// yeni irsaliye fatura kaydeder
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public PartialViewResult New(frmIrsaliye tbl)
        {
            //check if exists
            var tmp = db.WMS_IRS.Where(m => m.EvrakNo == tbl.EvrakNo).FirstOrDefault();
            if (tmp == null)
            {
                Irsaliye irsaliye = new Irsaliye();
                Result _Result = irsaliye.Insert(tbl);
                if (_Result.Id == 0) return null;
                tbl.Id = _Result.Id;
            }
            else
                tbl.Id = tmp.ID;
            //get list
            var list = db.WMS_STI.Where(m=>m.IrsaliyeID==tbl.Id).ToList();
            ViewBag.IrsaliyeId = tbl.Id;
            return PartialView("_GridPartial", list);
        }
        /// <summary>
        /// yeni malzeme
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public PartialViewResult InsertMalzeme(frmMalzeme tbl)
        {
            //add new
            try
            {
                var tmp = db.UpdateSTI(0, tbl.IrsaliyeId, tbl.MalKodu, tbl.Miktar, tbl.Birim).FirstOrDefault();
            }catch (Exception){}
            //get list
            var list = db.WMS_STI.Where(m => m.IrsaliyeID == tbl.IrsaliyeId).ToList();
            ViewBag.IrsaliyeId = tbl.IrsaliyeId;
            return PartialView("_GridPartial", list);
        }
        /// <summary>
        /// malzeme autocomplete
        /// </summary>
        public JsonResult getMalzemebyCode(string term)
        {
            var id = Url.RequestContext.RouteData.Values["id"];
            if (id == null) return null;
            using (DinamikModelContext Dinamik = new DinamikModelContext(id.ToString()))
            {
                var list = Dinamik.Context.STKs.Where(m => m.MalKodu.StartsWith(term)).Select(m => new frmJson { id = m.MalKodu, value = m.MalAdi, label = m.MalAdi }).Take(20).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult getMalzemebyName(string term)
        {
            var id = Url.RequestContext.RouteData.Values["id"];
            if (id == null) return null;
            using (DinamikModelContext Dinamik = new DinamikModelContext(id.ToString()))
            {
                var list = Dinamik.Context.STKs.Where(m => m.MalAdi.Contains(term)).Select(m => new frmJson { id = m.MalKodu, value = m.MalAdi, label = m.MalAdi }).Take(20).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// malzeme koduna göre birim getirir
        /// </summary>
        [HttpPost]
        public JsonResult getBirim(string kod,string s)
        {
            using (DinamikModelContext Dinamik = new DinamikModelContext(s))
            {
                var list = Dinamik.Context.STKs.Where(m => m.MalKodu == kod).Select(m => new { m.Birim1, m.Birim2, m.Birim3, m.Birim4 }).FirstOrDefault();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// anasayfadaki malzeme listesi
        /// </summary>
        public PartialViewResult GetHesapCodes()
        {
            var id = Url.RequestContext.RouteData.Values["id"];
            if (id == null) return null;
            if (id.ToString() == "0") return null;
            using (DinamikModelContext Dinamik = new DinamikModelContext(id.ToString()))
            {
                var list = Dinamik.Context.CHKs.Where(m => m.HesapKodu.StartsWith("320")).Select(m => new frmHesapUnvan { HesapKodu = m.HesapKodu, Unvan = m.Unvan1 + " " + m.Unvan2 }).ToList();
                return PartialView("_HesapGridPartial", list);
            }
        }
        /// <summary>
        /// yeni malzeme satırı formu
        /// </summary>
        public PartialViewResult NewMalzemeForm(int id)
        {
            ViewBag.IrsaliyeId = id;
            return PartialView("_GridNewPartial", new frmMalzeme());
        }
    }
}