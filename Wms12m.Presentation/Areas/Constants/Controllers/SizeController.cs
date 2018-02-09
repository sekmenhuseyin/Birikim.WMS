using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
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
            if (CheckPerm(Perms.BoyutKartı, PermTypes.Reading) == false) return Redirect("/");
            ViewBag.Yetki = CheckPerm(Perms.BoyutKartı, PermTypes.Writing);
            ViewBag.YetkiSil = CheckPerm(Perms.BoyutKartı, PermTypes.Deleting);
            return View("Index", new Olcu());
        }

        /// <summary>
        /// listeyi yenile
        /// </summary>
        public JsonResult List([DataSourceRequest]DataSourceRequest request)
        {
            var sql = string.Format("EXEC FINSAT6{0}.wms.getOlcuList", vUser.SirketKodu);
            var list = db.Database.SqlQuery<frmOlcu>(sql);
            return Json(list.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// düzenleme
        /// </summary>
        public PartialViewResult Edit(int id)
        {
            if (id == 0) return null;
            var tbl = Dimension.Detail(id);
            if (tbl == null) return null;
            return PartialView("_Edit", tbl);
        }

        /// <summary>
        /// yeni boyut kartı
        /// </summary>
        [HttpPost]
        public JsonResult Save(Olcu tbl)
        {
            if (CheckPerm(Perms.BoyutKartı, PermTypes.Writing) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            var _Result = Dimension.Operation(tbl);
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// silme
        /// </summary>
        public JsonResult Delete(string Id)
        {
            if (CheckPerm(Perms.BoyutKartı, PermTypes.Deleting) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            var _Result = Dimension.Delete(string.IsNullOrEmpty(Id) ? 0 : Convert.ToInt32(Id));
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
    }
}