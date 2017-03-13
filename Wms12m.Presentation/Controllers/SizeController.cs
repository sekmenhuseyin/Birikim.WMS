using System;
using System.Linq;
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
            var list = Dimension.GetList();
            return View("Index", list);
        }
        /// <summary>
        /// silme sonrası listeyi yenile
        /// </summary>
        public PartialViewResult List(string Id)
        {
            var list = Dimension.GetList();
            return PartialView("_List", list);
        }
        /// <summary>
        /// yeni boyut kartı
        /// </summary>
        public PartialViewResult Create()
        {
            ViewBag.SirketKod = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
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
            var list = Dimension.GetList();
            return PartialView("_List", list);
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