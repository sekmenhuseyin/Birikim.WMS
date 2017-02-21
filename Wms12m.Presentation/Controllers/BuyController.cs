using System;
using System.Linq;
using System.Web.Mvc;
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
                DateTime dateValue=DateTime.Now;
                if (DateTime.TryParse(tbl.Tarih, out dateValue) == true)
                {
                    try
                    {
                        tbl.Id = db.UpdateIRS(0, tbl.SirketID, tbl.DepoID, false, tbl.EvrakNo, tbl.HesapKodu, "", Convert.ToInt32(dateValue.ToOADate()), User.LogonUserName, Convert.ToInt32(DateTime.Today.ToOADate()), User.Id).FirstOrDefault().Value;
                    }
                    catch (Exception) { }
                }
            }
            else
                tbl.Id = tmp.ID;
            //get list
            var list = db.GetIrsaliyeSTI(tbl.Id,tbl.SirketID).ToList();
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
            var list = db.GetIrsaliyeSTI(tbl.IrsaliyeId, "33");
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
            var list = db.GetMalzeme(term, "", id.ToString()).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getMalzemebyName(string term)
        {
            var id = Url.RequestContext.RouteData.Values["id"];
            if (id == null) return null;
            var list = db.GetMalzeme("", term, id.ToString()).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// malzeme koduna göre birim getirir
        /// </summary>
        [HttpPost]
        public JsonResult getBirim(string kod,string s)
        {
            var list = db.GetMalBirim(kod, s);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// anasayfadaki malzeme listesi
        /// </summary>
        public PartialViewResult GetHesapCodes()
        {
            var id = Url.RequestContext.RouteData.Values["id"];
            if (id == null) return null;
            using (DinamikModelContext Dinamik = new DinamikModelContext(id.ToString()))
            {
                var list = Dinamik.Context.CHKs.Where(x => x.HesapKodu.StartsWith("320")).Select(m => new frmHesapUnvan { HesapKodu = m.HesapKodu, Unvan = m.Unvan1 + " " + m.Unvan2 }).ToList();
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