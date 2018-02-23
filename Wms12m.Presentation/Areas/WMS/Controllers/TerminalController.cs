using System.Linq;
using System.Web.Mvc;
using Birikim.Models;
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
            if (CheckPerm(Perms.TerminalİçinYetkilendirme, PermTypes.Reading) == false) return Redirect("/");
            return View("Index");
        }

        /// <summary>
        /// kullanıcılar
        /// </summary>
        public PartialViewResult List()
        {
            if (CheckPerm(Perms.TerminalİçinYetkilendirme, PermTypes.Reading) == false) return null;
            var list = PersonDetails.GetList();
            return PartialView("List", list);
        }

        /// <summary>
        /// yeni form
        /// </summary>
        public PartialViewResult New()
        {
            if (CheckPerm(Perms.TerminalİçinYetkilendirme, PermTypes.Reading) == false) return null;
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
            if (CheckPerm(Perms.TerminalİçinYetkilendirme, PermTypes.Reading) == false) return null;
            // return
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
            if (CheckPerm(Perms.TerminalİçinYetkilendirme, PermTypes.Reading) == false) return null;
            // return
            var tbl = Persons.Detail(id.ToInt32());
            return PartialView("Barcode", tbl);
        }

        /// <summary>
        /// kaydet
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Save(UserDetail tbl)
        {
            if (CheckPerm(Perms.TerminalİçinYetkilendirme, PermTypes.Writing) == false) return Redirect("/");
            var _Result = PersonDetails.Operation(tbl);
            LogActions("WMS", "Terminal", "Save", ComboItems.alEkle, tbl.UserID);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// sil
        /// </summary>
        [HttpPost]
        public JsonResult Delete(int Id)
        {
            if (CheckPerm(Perms.TerminalİçinYetkilendirme, PermTypes.Deleting) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            var _Result = PersonDetails.Delete(Id);
            LogActions("WMS", "Terminal", "Delete", ComboItems.alSil, Id);
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
    }
}