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
            if (CheckPerm("Combos", PermTypes.Reading) == false) return Redirect("/");
            return View("Index", Combo.GetList());
        }
        /// <summary>
        /// yeni sayfası
        /// </summary>
        public PartialViewResult New()
        {
            if (CheckPerm("Combos", PermTypes.Reading) == false) return null;
            return PartialView("New", new Combo_Name());
        }
        /// <summary>
        /// düzenle
        /// </summary>
        public PartialViewResult Edit(int id)
        {
            if (CheckPerm("Combos", PermTypes.Reading) == false) return null;
            return PartialView("Edit", Combo.Detail(id));
        }
        /// <summary>
        /// kaydet
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Save(Combo_Name tbl)
        {
            if (CheckPerm("Combos", PermTypes.Writing) == false) return Redirect("/");
            Result _Result = new Result();
            if (ModelState.IsValid)
                _Result = Combo.Operation(tbl);
            return RedirectToAction("Index");
        }
        /// <summary>
        /// sil
        /// </summary>
        [HttpPost]
        public JsonResult Delete(int id)
        {
            if (CheckPerm("Combos", PermTypes.Deleting) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            Result _Result = Combo.Delete(id);
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
    }
}
