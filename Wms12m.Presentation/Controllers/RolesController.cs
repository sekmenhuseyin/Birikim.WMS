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
            if (CheckPerm(Perms.Gruplar, PermTypes.Reading) == false) return Redirect("/");
            var list = db.Roles.Where(m => m.RoleName != "").ToList();
            return View("Index", list);
        }
        /// <summary>
        /// rol listesini güncelle
        /// </summary>
        public PartialViewResult List()
        {
            if (CheckPerm(Perms.Gruplar, PermTypes.Reading) == false) return null;
            var list = db.Roles.Where(m => m.RoleName != "").ToList();
            return PartialView("List", list);
        }
        /// <summary>
        /// yeni rol sayfası
        /// </summary>
        public PartialViewResult New()
        {
            if (CheckPerm(Perms.Gruplar, PermTypes.Reading) == false) return null;
            return PartialView("Editor", new Role());
        }
        /// <summary>
        /// rol düzenleme sayfası
        /// </summary>
        public PartialViewResult Edit()
        {
            if (CheckPerm(Perms.Gruplar, PermTypes.Reading) == false) return null;
            var id = Url.RequestContext.RouteData.Values["id"];
            if (id == null || id.ToString2() == "") return null;
            //return
            var ID = id.ToInt32();
            var tbl = db.Roles.Where(m => m.ID == ID).FirstOrDefault();
            return PartialView("Editor", tbl);
        }
        /// <summary>
        /// yeni rolü kaydet
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Save(Role role)
        {
            if (CheckPerm(Perms.Gruplar, PermTypes.Writing) == false) return Redirect("/");
            if (ModelState.IsValid)
            {
                if (role.ID == 0)
                {
                    db.Roles.Add(role);
                }
                else
                {
                    var tbl = db.Roles.Where(m => m.ID == role.ID).FirstOrDefault();
                    tbl.Aciklama = role.Aciklama;
                }
                db.SaveChanges();
                //log
                LogActions("", "Roles", "Save", ComboItems.alEkle, role.ID, role.RoleName);
            }
            return RedirectToAction("Index");
        }
        /// <summary>
        /// rol sil
        /// </summary>
        public JsonResult Delete(string id)
        {
            if (CheckPerm(Perms.Gruplar, PermTypes.Deleting) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            try
            {
                Role role = db.Roles.Find(id);
                db.Roles.Remove(role);
                db.SaveChanges();
                //log
                LogActions("", "Roles", "Delete", ComboItems.alSil, id.ToInt32(), role.RoleName);
                return Json(new Result(true, 1, ""), JsonRequestBehavior.AllowGet);
            }
            catch (System.Exception ex)
            {
                Logger(ex, "Roles/Delete");
                return Json(new Result(false, "Bu yetki kullanılıyor"), JsonRequestBehavior.AllowGet);
            }
        }
    }
}