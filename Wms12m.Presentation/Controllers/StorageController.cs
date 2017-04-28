using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Wms12m.Business;
using Wms12m.Entity;
using Wms12m.Entity.Models;
using Wms12m.Entity.Mysql;

namespace Wms12m.Presentation.Controllers
{
    public class StorageController : RootController
    {
        /// <summary>
        /// anasayfası
        /// </summary>
        public ActionResult Index()
        {
            if (CheckPerm("Storage", PermTypes.Reading) == false) return Redirect("/");
            using (KabloEntities dbx = new KabloEntities())
            {
                ViewBag.KabloDepoID = new SelectList(dbx.depoes.OrderBy(m => m.depo1).ToList(), "id", "depo1");
            }
            return View("Index", new Depo());
        }
        /// <summary>
        /// listesi
        /// </summary>
        public PartialViewResult StoreGridPartial(string Id)
        {
            if (CheckPerm("Storage", PermTypes.Reading) == false) return null;
            List<Depo> _List = new List<Depo>();
            _List = Id == "Locked" ? Store.GetList().Where(a => a.Aktif == true).ToList() : Id == "noLocked" ? Store.GetList().Where(a => a.Aktif == false).ToList() : Store.GetList();
            return PartialView("_StoreGridPartial", _List);
        }
        /// <summary>
        /// düzenle
        /// </summary>
        public PartialViewResult StoreDetailPartial(string Id)
        {
            if (CheckPerm("Storage", PermTypes.Reading) == false) return null;
            var item = Convert.ToInt16(Id == "" ? "0" : Id) > 0 ? Store.Detail(Convert.ToInt16(Id)) : new Depo() { Aktif = false };
            using (KabloEntities dbx = new KabloEntities())
            {
                ViewBag.KabloDepoID = new SelectList(dbx.depoes.OrderBy(m => m.depo1).ToList(), "id", "depo1", item.KabloDepoID);
            }
            return PartialView("_StoreDetailPartial", item);
        }
        /// <summary>
        /// sil
        /// </summary>
        public JsonResult Delete(string Id)
        {
            if (CheckPerm("Storage", PermTypes.Deleting) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            Result _Result = Store.Delete(string.IsNullOrEmpty(Id) ? 0 : Convert.ToInt32(Id));
            return Json(_Result, JsonRequestBehavior.AllowGet);
            
        }
        /// <summary>
        /// kayıt işlemleri
        /// </summary>
        public JsonResult StoreOperation(Depo P)
        {
            if (CheckPerm("Storage", PermTypes.Writing) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            Result _Result = Store.Operation(P);
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
    }
}