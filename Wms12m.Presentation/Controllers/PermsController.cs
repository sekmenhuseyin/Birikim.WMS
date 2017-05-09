using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Wms12m.Entity.Models;

namespace Wms12m.Presentation.Controllers
{
    public class PermsController : RootController
    {
        /// <summary>
        /// Yetkiler
        /// </summary>
        public ActionResult Index()
        {
            if (CheckPerm("Grup Yetkileri", PermTypes.Reading) == false) return Redirect("/");
            var rolePerms = db.RolePerms.Include(r => r.Perm).Include(r => r.Role);
            return View("Index", rolePerms.ToList());
        }
        /// <summary>
        /// yetki oluşturma sayfası
        /// </summary>
        public ActionResult Create()
        {
            if (CheckPerm("Grup Yetkileri", PermTypes.Reading) == false) return Redirect("/");
            ViewBag.PermName = new SelectList(db.Perms, "PermName", "PermName");
            ViewBag.RoleName = new SelectList(db.Roles, "RoleName", "RoleName");
            return View("Create");
        }
        /// <summary>
        /// yetki oluştur
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,RoleName,PermName,Reading,Writing,Updating,Deleting")] RolePerm rolePerm)
        {
            if (ModelState.IsValid)
            {
                if (CheckPerm("Grup Yetkileri", PermTypes.Writing) == false) return Redirect("/");
                try
                {
                    rolePerm.RecordDate = DateTime.Now;
                    rolePerm.RecordUser = vUser.UserName;
                    rolePerm.ModifiedDate = DateTime.Now;
                    rolePerm.ModifiedUser = vUser.UserName;
                    db.RolePerms.Add(rolePerm);
                    db.SaveChanges();
                }
                catch (Exception) { }
            }
            return RedirectToAction("Index");
        }
        /// <summary>
        /// yetki düzenleme sayfası
        /// </summary>
        public ActionResult Edit(int id)
        {
            RolePerm rolePerm = db.RolePerms.Where(m => m.ID == id).FirstOrDefault();
            if (rolePerm == null)
                return RedirectToAction("Index");
            if (CheckPerm("Grup Yetkileri", PermTypes.Reading) == false) return Redirect("/");
            ViewBag.PermName = new SelectList(db.Perms, "PermName", "PermName", rolePerm.PermName);
            ViewBag.RoleName = new SelectList(db.Roles, "RoleName", "RoleName", rolePerm.RoleName);
            return View("Edit", rolePerm);
        }
        /// <summary>
        /// yetki düzenleme kaydet
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,RoleName,PermName,Reading,Writing,Updating,Deleting")] RolePerm rolePerm)
        {
            if (ModelState.IsValid)
            {
                if (CheckPerm("Grup Yetkileri", PermTypes.Writing) == false) return Redirect("/");
                try
                {
                    var tbl = db.RolePerms.Where(m => m.ID == rolePerm.ID).FirstOrDefault();
                    tbl.RoleName = rolePerm.RoleName;
                    tbl.PermName = rolePerm.PermName;
                    tbl.Reading = rolePerm.Reading;
                    tbl.Writing = rolePerm.Writing;
                    tbl.Updating = rolePerm.Updating;
                    tbl.Deleting = rolePerm.Deleting;
                    tbl.ModifiedDate = DateTime.Now;
                    tbl.ModifiedUser = vUser.UserName;
                    db.SaveChanges();
                }
                catch (Exception) { }
            }
            return RedirectToAction("Index");
        }
        /// <summary>
        /// Yetki sil
        /// </summary>
        public ActionResult Delete(int id)
        {
            if (CheckPerm("Grup Yetkileri", PermTypes.Deleting) == false) return Redirect("/");
            var satir = db.RolePerms.Where(m => m.ID == id).FirstOrDefault();
            try
            {
                db.RolePerms.Remove(satir);
                db.SaveChanges();
            }
            catch (Exception)
            {
            }
            return RedirectToAction("Index");
        }
    }
}
