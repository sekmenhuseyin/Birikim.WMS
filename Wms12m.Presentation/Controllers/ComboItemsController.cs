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
        public ActionResult List()
        {
            if (CheckPerm("ComboItems", PermTypes.Reading) == false) return Redirect("/");
            var id = Url.RequestContext.RouteData.Values["id"];
            if (id == null) return null;
            if (id.ToString() == "0") return null;
            ViewBag.ComboID = new SelectList(Combo.GetList(), "ID", "ComboName");
            return View("List", ComboSub.GetList(id.ToInt32()));
        }
        /// <summary>
        /// yeni sayfası
        /// </summary>
        public PartialViewResult New()
        {
            if (CheckPerm("ComboItems", PermTypes.Reading) == false) return null;
            return PartialView("New", new ComboItem_Name());
        }
        /// <summary>
        /// düzenle
        /// </summary>
        public PartialViewResult Edit(int id)
        {
            if (CheckPerm("ComboItems", PermTypes.Reading) == false) return null;
            var tablo = ComboSub.Detail(id);
            ViewBag.ComboID = new SelectList(Combo.GetList(), "ID", "ComboName", tablo.ComboID);
            return PartialView("Edit", tablo);
        }
        /// <summary>
        /// kaydet
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Save(ComboItem_Name tbl)
        {
            if (CheckPerm("ComboItems", PermTypes.Writing) == false) return Redirect("/");
            Result _Result = new Result();
            if (ModelState.IsValid)
                _Result = ComboSub.Operation(tbl);
            return Redirect(Request.UrlReferrer.ToString());
        }
        /// <summary>
        /// sil
        /// </summary>
        [HttpPost]
        public JsonResult Delete(int id)
        {
            if (CheckPerm("ComboItems", PermTypes.Deleting) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            Result _Result = ComboSub.Delete(id);
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
    }
}