using System.Linq;
using System.Web.Mvc;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m.Presentation.Controllers
{
    public class UsersController : RootController
    {
        /// <summary>
        /// kullanıcılar
        /// </summary>
        public ActionResult Index()
        {
            var list = db.Users.ToList();
            return View("Index", list);
        }
        /// <summary>
        /// ayrıntılar
        /// </summary>
        public PartialViewResult Details(int id)
        {
            var tbl = Persons.Detail(id);
            return PartialView("Details", tbl);
        }
        /// <summary>
        /// yeni form
        /// </summary>
        public PartialViewResult New()
        {
            return PartialView("New", new User());
        }
        /// <summary>
        /// düzenle form
        /// </summary>
        public PartialViewResult Edit(int id)
        {
            return PartialView("Edit", Persons.Detail(id));
        }
        /// <summary>
        /// kaydet
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Save(User tbl)
        {
            Result _Result = Persons.Operation(tbl);
            return RedirectToAction("Index");
        }
        /// <summary>
        /// sil
        /// </summary>
        [HttpPost]
        public JsonResult Delete(int Id)
        {
            Result _Result = Persons.Delete(Id);
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
    }
}
