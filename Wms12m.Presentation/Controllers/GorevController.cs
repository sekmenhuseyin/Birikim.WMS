using System;
using System.Linq;
using System.Web.Mvc;
using Wms12m.Business;
using Wms12m.Entity;

namespace Wms12m.Presentation.Controllers
{
    public class GorevController : RootController
    {

        /// <summary>
        /// görev anasayfa
        /// </summary>
        public ActionResult Index()
        {
            var list = db.Gorevs.OrderBy(m=>m.DurumID).OrderByDescending(m=>m.ID).ToList();
            return View("Index", list);
        }
        /// <summary>
        /// sadece listeyi gösterir
        /// </summary>
        public PartialViewResult GorevGridPartial()
        {
            var list = db.Gorevs.OrderBy(m => m.DurumID).OrderByDescending(m => m.ID).ToList();
            return PartialView("_GorevGridPartial", list);
        }
        /// <summary>
        /// görev düzenle
        /// </summary>
        public PartialViewResult GorevDetailPartial(int id)
        {
            var list = db.Gorevs.Where(m => m.ID == id).FirstOrDefault();
            ViewBag.GorevTipiID = new SelectList(db.ComboItem_Name.Where(m => m.ComboID == 1).OrderBy(m => m.Name).ToList(), "ID", "Name", list.GorevTipiID);
            ViewBag.DurumID = new SelectList(db.ComboItem_Name.Where(m => m.ComboID == 2).OrderBy(m=>m.Name).ToList(), "ID", "Name", list.DurumID);
            return PartialView("_GorevDetailPartial", list);
        }
        /// <summary>
        /// görev güncelle
        /// </summary>
        public PartialViewResult Update(frmGorev tbl)
        {
            //update
            Task tmpTable = new Task();
            Result _Result = tmpTable.Update(tbl);
            //get list
            var list = db.Gorevs.ToList();
            return PartialView("_GorevGridPartial", list);
        }
        /// <summary>
        /// görev sil
        /// </summary>
        public JsonResult Delete(int ID)
        {
            Task tmpTable = new Task();
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
            var list = db.Gorevs.Where(m=>m.ID==ID).FirstOrDefault();
            ViewBag.GorevliID = new SelectList(db.Users.OrderBy(m=>m.Kod).ToList(), "ID", "AdSoyad", list.GorevliID);
            return PartialView("GorevliAta", list);
        }
        /// <summary>
        /// görevliyi kaydeder
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult GorevliKaydet(frmGorevli tbl)
        {
            Task tmpTable = new Task();
            Result _Result = tmpTable.UpdateGorevli(tbl);
            return RedirectToAction("Index");
        }
    }
}