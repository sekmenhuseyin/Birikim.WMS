using System.Linq;
using System.Web.Mvc;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m.Presentation.Controllers
{
    public class RolesController : RootController
    {
        /// <summary>
        /// rol sayfası
        /// </summary>
        public ActionResult Index()
        {
            if (CheckPerm("Gruplar", PermTypes.Reading) == false) return Redirect("/");
            var list = db.Roles.Where(m => m.RoleName != "").ToList();
            return View("Index", list);
        }
        /// <summary>
        /// rol listesini güncelle
        /// </summary>
        public PartialViewResult List()
        {
            if (CheckPerm("Gruplar", PermTypes.Reading) == false) return null;
            var list = db.Roles.Where(m => m.RoleName != "").ToList();
            return PartialView("List", list);
        }
        /// <summary>
        /// yeni rol sayfası
        /// </summary>
        public PartialViewResult New()
        {
            if (CheckPerm("Gruplar", PermTypes.Reading) == false) return null;
            return PartialView("Create", new Role());
        }
        /// <summary>
        /// yeni rolü kaydet
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,RoleName")] Role role)
        {
            if (CheckPerm("Gruplar", PermTypes.Writing) == false) return Redirect("/");
            if (ModelState.IsValid)
            {
                db.Roles.Add(role);
                db.SaveChanges();
                //log
                LogActions("", "Roles", "Create", ComboItems.alEkle, role.ID, role.RoleName);
            }
            return RedirectToAction("Index");
        }
        /// <summary>
        /// rol sil
        /// </summary>
        public JsonResult Delete(string id)
        {
            if (CheckPerm("Gruplar", PermTypes.Deleting) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            try
            {
                Role role = db.Roles.Find(id);
                db.Roles.Remove(role);
                db.SaveChanges();
                //log
                LogActions("", "Roles", "Delete", ComboItems.alSil, id.ToInt32(), role.RoleName);
                return Json(new Result(true, 1, ""), JsonRequestBehavior.AllowGet);
            }
            catch (System.Exception)
            {
                return Json(new Result(false, "Bu yetki kullanılıyor"), JsonRequestBehavior.AllowGet);
            }
        }
    }
}