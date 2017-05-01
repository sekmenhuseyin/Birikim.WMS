using System;
using System.Linq;
using System.Web.Mvc;
using Wms12m.Business;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m.Presentation.Controllers
{
    public class TasksController : RootController
    {

        /// <summary>
        /// görev anasayfa
        /// </summary>
        public ActionResult Index()
        {
            if (CheckPerm("Tasks", PermTypes.Reading) == false) return Redirect("/");
            ViewBag.DurumID = new SelectList(ComboSub.GetList(Combos.GorevDurum.ToInt32()), "ID", "Name");
            return View("Index");
        }
        /// <summary>
        /// listeyi gösterir
        /// </summary>
        public PartialViewResult List(int Id)
        {
            if (CheckPerm("Tasks", PermTypes.Reading) == false) return null;
            var list = Task.GetList(Id);
            return PartialView("List", list);
        }
        /// <summary>
        /// görev ayrıntıları
        /// </summary>
        [HttpPost]
        public PartialViewResult Details(int ID)
        {
            if (CheckPerm("Tasks", PermTypes.Reading) == false) return null;
            var list = db.GetIrsDetayfromGorev(ID);
            return PartialView("Details", list);
        }
        /// <summary>
        /// görev düzenle
        /// </summary>
        public PartialViewResult GorevDetailPartial(int id)
        {
            if (CheckPerm("Tasks", PermTypes.Reading) == false) return null;
            var list = Task.Detail(id);
            ViewBag.GorevTipiID = new SelectList(ComboSub.GetList(Combos.GorevTipleri.ToInt32()), "ID", "Name", list.GorevTipiID);
            ViewBag.DurumID = new SelectList(ComboSub.GetList(Combos.GorevDurum.ToInt32()), "ID", "Name", list.DurumID);
            return PartialView("_GorevDetailPartial", list);
        }
        /// <summary>
        /// yeni kontrollü sayım görevi ekle
        /// </summary>
        [HttpPost]
        public PartialViewResult New()
        {
            if (CheckPerm("Tasks", PermTypes.Reading) == false) return null;
            ViewBag.DepoID = new SelectList(Store.GetList(), "ID", "DepoAd");
            return PartialView("New");
        }
        /// <summary>
        /// yeni görevi kaydeder
        /// </summary>
        [HttpPost]
        public JsonResult SaveNew(int DepoID)
        {
            if (CheckPerm("Tasks", PermTypes.Writing) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            Result _Result;
            //kontrol
            int açık = ComboItems.Açık.ToInt32();
            int sayim = ComboItems.KontrolSayım.ToInt32();
            var grv = db.Gorevs.Where(m => m.DepoID == DepoID && m.GorevTipiID == sayim && m.DurumID == açık).FirstOrDefault();
            if (grv == null)
            {
                var grvNo = db.SettingsGorevNo(fn.ToOADate()).FirstOrDefault();
                var tbl = new Gorev() { DepoID = DepoID, GorevNo = grvNo, GorevTipiID = sayim, DurumID = açık };
                _Result = Task.Operation(tbl);
            }
            else
                _Result = new Result(false, "Bu görev zaten var");
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// görev güncelle
        /// </summary>
        public JsonResult Update(frmGorev tbl)
        {
            if (CheckPerm("Tasks", PermTypes.Writing) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            //update
            Task tmpTable = new Task();
            Result _Result = tmpTable.Update(tbl);
            //get list
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// görev sil
        /// </summary>
        public JsonResult Delete(int ID)
        {
            if (CheckPerm("Tasks", PermTypes.Deleting) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            Result _Result = new Result();
            try
            {
                db.DeleteFromGorev(ID);
                _Result.Status = true; _Result.Id = ID;
            }
            catch (Exception ex)
            {
                Logger(ex, "Tasks/Delete");
                _Result.Status = false;
                _Result.Message = ex.Message;
            }
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// görevli ata
        /// </summary>
        [HttpPost]
        public PartialViewResult GorevliAta()
        {
            if (CheckPerm("Tasks", PermTypes.Reading) == false) return null;
            var id = Url.RequestContext.RouteData.Values["id"];
            if (id == null) return null;
            Int32 ID = Convert.ToInt32(id);
            var list = Task.Detail(ID);
            ViewBag.Gorevli = new SelectList(Persons.GetList(), "Kod", "AdSoyad", list.Gorevli);
            return PartialView("GorevliAta", list);
        }
        /// <summary>
        /// görevliyi kaydeder
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult GorevliKaydet(frmGorevli tbl)
        {
            if (CheckPerm("Tasks", PermTypes.Writing) == false) return Redirect("/");
            Task tmpTable = new Task();
            Result _Result = tmpTable.UpdateGorevli(tbl);
            return RedirectToAction("Index");
        }
    }
}