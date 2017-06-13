using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Wms12m.Business;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m.Presentation.Areas.Constants.Controllers
{
    public class SizeController : RootController
    {
        /// <summary>
        /// boyutlar anasayfası
        /// </summary>
        public ActionResult Index()
        {
            if (CheckPerm("Boyut Kartı", PermTypes.Reading) == false) return Redirect("/");
            ViewBag.SirketID = db.GetSirketDBs().FirstOrDefault();
            ViewBag.Yetki = CheckPerm("Boyut Kartı", PermTypes.Writing);
            return View("Index", new Olcu());
        }
        /// <summary>
        /// silme sonrası listeyi yenile
        /// </summary>
        public PartialViewResult List()
        {
            if (CheckPerm("Boyut Kartı", PermTypes.Reading) == false) return null;
            //dbler tempe aktarılıyor
            var list = db.GetSirketDBs();
            List<string> liste = new List<string>();
            foreach (var item in list) { liste.Add(item); }
            ViewBag.Sirket = liste;
            ViewBag.Yetki = CheckPerm("Boyut Kartı", PermTypes.Writing);
            ViewBag.YetkiSil = CheckPerm("Boyut Kartı", PermTypes.Deleting);
            return PartialView("_List", Dimension.GetList());
        }
        /// <summary>
        /// yeni boyut kartı
        /// </summary>
        public PartialViewResult Create()
        {
            if (CheckPerm("Boyut Kartı", PermTypes.Reading) == false) return null;
            ViewBag.SirketID = db.GetSirketDBs().FirstOrDefault();
            return PartialView("_Create", new Olcu());
        }
        /// <summary>
        /// düzenleme
        /// </summary>
        public PartialViewResult Edit(int id)
        {
            if (id == 0) return null;
            if (CheckPerm("Boyut Kartı", PermTypes.Reading) == false) return null;
            var tbl = Dimension.Detail(id);
            if (tbl == null) return null;
            ViewBag.SirketID = db.GetSirketDBs().FirstOrDefault();
            return PartialView("_Edit", tbl);
        }
        /// <summary>
        /// yeni boyut kartı
        /// </summary>
        [HttpPost]
        public JsonResult Save(Olcu tbl)
        {
            if (CheckPerm("Boyut Kartı", PermTypes.Writing) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            Result _Result = Dimension.Operation(tbl);
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// silme
        /// </summary>
        public JsonResult Delete(string Id)
        {
            if (CheckPerm("Boyut Kartı", PermTypes.Deleting) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            Result _Result = Dimension.Delete(string.IsNullOrEmpty(Id) ? 0 : Convert.ToInt32(Id));
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
    }
}