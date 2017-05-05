﻿using System.Linq;
using System.Web.Mvc;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m.Presentation.Controllers
{
    public class TerminalController : RootController
    {
        /// <summary>
        /// kullanıcı sayfası
        /// </summary>
        public ActionResult Index()
        {
            if (CheckPerm("Users", PermTypes.Reading) == false) return Redirect("/");
            return View("Index");
        }
        /// <summary>
        /// kullanıcılar
        /// </summary>
        public PartialViewResult List()
        {
            if (CheckPerm("Users", PermTypes.Reading) == false) return null;
            var list = PersonPerms.GetList();
            return PartialView("List", list);
        }
        /// <summary>
        /// yeni form
        /// </summary>
        public PartialViewResult New()
        {
            if (CheckPerm("Users", PermTypes.Reading) == false) return null;
            ViewBag.DepoID = new SelectList(Store.GetList(), "ID", "DepoAd");
            ViewBag.UserID = new SelectList(Persons.GetListWithoutTerminal(), "ID", "AdSoyad");
            return PartialView("Editor", new UserDetail());
        }
        /// <summary>
        /// düzenler
        /// </summary>
        public PartialViewResult Edit()
        {
            var id = Url.RequestContext.RouteData.Values["id"];
            if (id == null || id.ToString2() == "") return null;
            if (CheckPerm("Users", PermTypes.Reading) == false) return null;
            //return
            var tbl = PersonPerms.Detail(id.ToInt32());
            ViewBag.DepoID = new SelectList(Store.GetList(), "ID", "DepoAd", tbl.DepoID);
            ViewBag.UserID = new SelectList(db.Users.Where(m => m.ID == tbl.UserID).ToList(), "ID", "AdSoyad");
            return PartialView("Editor", tbl);
        }
        /// <summary>
        /// kaydet
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Save(UserDetail tbl)
        {
            if (CheckPerm("Users", PermTypes.Writing) == false) return Redirect("/");
            Result _Result = PersonPerms.Operation(tbl);
            return RedirectToAction("Index");
        }
        /// <summary>
        /// sil
        /// </summary>
        [HttpPost]
        public JsonResult Delete(int Id)
        {
            if (CheckPerm("Users", PermTypes.Deleting) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            Result _Result = PersonPerms.Delete(Id);
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
    }
}