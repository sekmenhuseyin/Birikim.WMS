using System.Web.Mvc;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m.Presentation.Controllers
{
    public class CombosController : RootController
    {
        /// <summary>
        /// tüm liste
        /// </summary>
        public ActionResult Index()
        {
            return View("Index", Combo.GetList());
        }
        /// <summary>
        /// yeni sayfası
        /// </summary>
        public PartialViewResult New()
        {
            return PartialView("New", new Combo_Name());
        }
        /// <summary>
        /// düzenle
        /// </summary>
        public PartialViewResult Edit(int id)
        {
            return PartialView("Edit", Combo.Detail(id));
        }
        /// <summary>
        /// kaydet
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public JsonResult Save([Bind(Include = "ID,ComboName")] Combo_Name tbl)
        {
            Result _Result = new Result();
            if (ModelState.IsValid)
                _Result = Combo.Operation(tbl);
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// sil
        /// </summary>
        [HttpPost]
        public JsonResult Delete(int id)
        {
            Result _Result = new Result();
            _Result = Combo.Delete(id);
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
    }
}
