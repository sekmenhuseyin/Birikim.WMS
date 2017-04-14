using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Wms12m.Business;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m.Presentation.Controllers
{
    public class SizeController : RootController
    {
        /// <summary>
        /// boyutlar anasayfası
        /// </summary>
        public ActionResult Index()
        {
            //dbler tempe aktarılıyor
            var list = db.GetSirketDBs();
            List<string> liste = new List<string>();
            foreach (var item in list) { liste.Add(item); }
            ViewBag.Sirket = liste;
            return View("Index", Dimension.GetList());
        }
        /// <summary>
        /// silme sonrası listeyi yenile
        /// </summary>
        public PartialViewResult List()
        {
            //dbler tempe aktarılıyor
            var list = db.GetSirketDBs();
            List<string> liste = new List<string>();
            foreach (var item in list) { liste.Add(item); }
            ViewBag.Sirket = liste;
            return PartialView("_List", Dimension.GetList());
        }
        /// <summary>
        /// yeni boyut kartı
        /// </summary>
        public PartialViewResult Create()
        {
            return PartialView("_Create", new Olcu());
        }
        /// <summary>
        /// düzenleme
        /// </summary>
        public PartialViewResult Edit()
        {
            var id = Url.RequestContext.RouteData.Values["id"];
            if (id == null) return null;
            if (id.ToString() == "0") return null;
            var tbl = Dimension.Detail(id.ToInt32());
            if (tbl == null) return null;
            return PartialView("_Edit", tbl);
        }
        /// <summary>
        /// yeni boyut kartı
        /// </summary>
        public PartialViewResult Save(Olcu tbl)
        {
            Result _Result = Dimension.Operation(tbl);
            //dbler tempe aktarılıyor
            var list = db.GetSirketDBs();
            List<string> liste = new List<string>();
            foreach (var item in list) { liste.Add(item); }
            ViewBag.Sirket = liste;
            return PartialView("_List", Dimension.GetList());
        }
        /// <summary>
        /// silme
        /// </summary>
        public JsonResult Delete(string Id)
        {
            Result _Result = Dimension.Delete(string.IsNullOrEmpty(Id) ? 0 : Convert.ToInt32(Id));
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
    }
}