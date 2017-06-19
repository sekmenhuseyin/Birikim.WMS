using System.Linq;
using System.Web.Mvc;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m.Presentation.Areas.WMS.Controllers
{
    public class TerminalController : RootController
    {
        /// <summary>
        /// kullanıcı sayfası
        /// </summary>
        public ActionResult Index()
        {
            if (CheckPerm("Terminal için Yetkilendirme", PermTypes.Reading) == false) return Redirect("/");
            return View("Index");
        }
        /// <summary>
        /// kullanıcılar
        /// </summary>
        public PartialViewResult List()
        {
            if (CheckPerm("Terminal için Yetkilendirme", PermTypes.Reading) == false) return null;
            var list = PersonDetails.GetList();
            return PartialView("List", list);
        }
        /// <summary>
        /// yeni form
        /// </summary>
        public PartialViewResult New()
        {
            if (CheckPerm("Terminal için Yetkilendirme", PermTypes.Reading) == false) return null;
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
            if (CheckPerm("Terminal için Yetkilendirme", PermTypes.Reading) == false) return null;
            //return
            var tbl = PersonDetails.Detail(id.ToInt32());
            ViewBag.DepoID = new SelectList(Store.GetList(), "ID", "DepoAd", tbl.DepoID);
            ViewBag.UserID = new SelectList(db.Users.Where(m => m.ID == tbl.UserID).ToList(), "ID", "AdSoyad");
            return PartialView("Editor", tbl);
        }
        /// <summary>
        /// düzenler
        /// </summary>
        public PartialViewResult Barcode()
        {
            var id = Url.RequestContext.RouteData.Values["id"];
            if (id == null || id.ToString2() == "") return null;
            if (CheckPerm("Terminal için Yetkilendirme", PermTypes.Reading) == false) return null;
            //return
            var tbl = Persons.Detail(id.ToInt32());
            return PartialView("Barcode", tbl);
        }
        /// <summary>
        /// kaydet
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Save(UserDetail tbl)
        {
            if (CheckPerm("Terminal için Yetkilendirme", PermTypes.Writing) == false) return Redirect("/");
            Result _Result = PersonDetails.Operation(tbl);
            return RedirectToAction("Index");
        }
        /// <summary>
        /// sil
        /// </summary>
        [HttpPost]
        public JsonResult Delete(int Id)
        {
            if (CheckPerm("Terminal için Yetkilendirme", PermTypes.Deleting) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            Result _Result = PersonDetails.Delete(Id);
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
    }
}