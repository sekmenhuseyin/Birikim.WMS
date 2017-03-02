using System;
using System.Linq;
using Wms12m.Entity;
using System.Web.Mvc;
using Wms12m.Business;
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
            var list = db.TK_OLCU.OrderBy(m => m.MalKodu).ToList();
            return View("Index", list);
        }
        /// <summary>
        /// silme sonrası listeyi yenile
        /// </summary>
        public PartialViewResult List(string Id)
        {
            var list = db.TK_OLCU.OrderBy(m => m.MalKodu).ToList();
            return PartialView("_List", list);
        }
        /// <summary>
        /// yeni boyut kartı
        /// </summary>
        public PartialViewResult Create()
        {
            ViewBag.SirketKod = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            return PartialView("_Create", new TK_OLCU());
        }
        /// <summary>
        /// düzenleme
        /// </summary>
        public PartialViewResult Edit()
        {
            var id = Url.RequestContext.RouteData.Values["id"];
            if (id == null) return null;
            if (id.ToString() == "0") return null;
            int tmp = id.ToInt32();
            var tbl = db.TK_OLCU.Where(m => m.ID == tmp).FirstOrDefault();
            if (tbl == null) return null;
            return PartialView("_Edit", tbl);
        }
        /// <summary>
        /// yeni boyut kartı
        /// </summary>
        public PartialViewResult Save(TK_OLCU tbl)
        {
            Dimension tmpTable = new Dimension();
            Result _Result = tmpTable.Operation(tbl);
            var list = db.TK_OLCU.OrderBy(m => m.MalKodu).ToList();
            return PartialView("_List", list);
        }
        /// <summary>
        /// silme
        /// </summary>
        public JsonResult Delete(string Id)
        {
             var operation = new Dimension();
            Result _Result = operation.Delete(string.IsNullOrEmpty(Id) ? 0 : Convert.ToInt32(Id));
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
    }
}