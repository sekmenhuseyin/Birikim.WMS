using System.Web.Mvc;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m.Presentation.Controllers
{
    public class ComboItemsController : RootController
    {
        /// <summary>
        /// tüm liste
        /// </summary>
        public ActionResult Index()
        {
            var id = Url.RequestContext.RouteData.Values["id"];
            if (id == null) return null;
            if (id.ToString() == "0") return null;
            ViewBag.ComboID = new SelectList(Combo.GetList(), "ID", "ComboName");
            return View("Index", ComboSub.GetList(id.ToInt32()));
        }
        /// <summary>
        /// yeni sayfası
        /// </summary>
        public PartialViewResult New()
        {
            return PartialView("New", new ComboItem_Name());
        }
        /// <summary>
        /// düzenle
        /// </summary>
        public PartialViewResult Edit(int id)
        {
            var tablo = ComboSub.Detail(id);
            ViewBag.ComboID = new SelectList(Combo.GetList(), "ID", "ComboName", tablo.ComboID);
            return PartialView("Edit", tablo);
        }
        /// <summary>
        /// kaydet
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public JsonResult Save(ComboItem_Name tbl)
        {
            Result _Result = new Result();
            if (ModelState.IsValid)
                _Result = ComboSub.Operation(tbl);
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// sil
        /// </summary>
        [HttpPost]
        public JsonResult Delete(int id)
        {
            Result _Result = new Result();
            _Result = ComboSub.Delete(id);
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
    }
}