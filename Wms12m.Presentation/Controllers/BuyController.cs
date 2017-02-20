using System;
using System.Linq;
using System.Web.Mvc;
using System.Globalization;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m.Presentation.Controllers
{
    public class BuyController : RootController
    {

        // GET: Buy
        public ActionResult Index()
        {
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
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
        //yeni malzeme
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
        //malzeme autocomplete
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
        //malzeme koduna göre birim getirir
        [HttpPost]
        public JsonResult getBirim(string kod,string s)
        {
            var list = db.GetMalBirim(kod, s);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        //anasayfadaki malzeme listesi
        public PartialViewResult GetHesapCodes()
        {
            var id = Url.RequestContext.RouteData.Values["id"];
            if (id == null) return null;
            var list = db.GetHesapCodes(id.ToString()).ToList();
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