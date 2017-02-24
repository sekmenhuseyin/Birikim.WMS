using System;
using System.Linq;
using Wms12m.Entity;
using System.Web.Mvc;
using Wms12m.Business;

namespace Wms12m.Presentation.Controllers
{
    public class GorevController : RootController
    {

        /// <summary>
        /// görev anasayfa
        /// </summary>
        public ActionResult Index()
        {
            var list = db.GorevListesis.ToList();
            return View("Index", list);
        }
        /// <summary>
        /// sadece listeyi gösterir
        /// </summary>
        public PartialViewResult GorevGridPartial()
        {
            var list = db.GorevListesis.ToList();
            return PartialView("_GorevGridPartial", list);
        }
        /// <summary>
        /// görev düzenle
        /// </summary>
        public PartialViewResult GorevDetailPartial(int id)
        {
            var list = db.GorevListesis.Where(m => m.ID == id).FirstOrDefault();
            ViewBag.GorevTipiID = new SelectList(db.ComboItemNames.Where(m => m.ComboID == 1).ToList(), "ID", "ItemName", list.GorevTipiID);
            ViewBag.DurumID = new SelectList(db.ComboItemNames.Where(m => m.ComboID == 2).ToList(), "ID", "ItemName", list.DurumID);
            ViewBag.GorevliID = new SelectList(db.USR01.ToList(), "Id", "UserName", list.GorevliID);
            return PartialView("_GorevDetailPartial", list);
        }
        /// <summary>
        /// görev güncelle
        /// </summary>
        public PartialViewResult Update(frmGorev tbl)
        {
            //update
            Gorev tmpTable = new Gorev();
            Result _Result = tmpTable.Update(tbl);
            //get list
            var list = db.GorevListesis.ToList();
            return PartialView("_GorevGridPartial", list);
        }
        /// <summary>
        /// görev sil
        /// </summary>
        public JsonResult Delete(int ID)
        {
            Gorev tmpTable = new Gorev();
            Result _Result = tmpTable.Delete(ID);
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// görevli ata
        /// </summary>
        [HttpPost]
        public PartialViewResult GorevliAta()
        {
            var id = Url.RequestContext.RouteData.Values["id"];
            if (id == null) return null;
            Int32 ID = Convert.ToInt32(id);
            var list = db.GorevListesis.Where(m=>m.ID==ID).FirstOrDefault();
            ViewBag.GorevliID = new SelectList(db.USR01.OrderBy(m=>m.Kod).ToList(), "ID", "Kod", list.GorevliID);
            return PartialView("GorevliAta", list);
        }
        /// <summary>
        /// görevliyi kaydeder
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult GorevliKaydet(frmGorevli tbl)
        {
            Gorev tmpTable = new Gorev();
            Result _Result = tmpTable.UpdateGorevli(tbl);
            return RedirectToAction("Index");
        }
    }
}