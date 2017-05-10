using System;
using System.Linq;
using System.Web.Mvc;
using Wms12m.Entity;
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
            ViewBag.RoleName = new SelectList(db.Roles.ToList(), "RoleName", "RoleName");
            return View("Index");
        }
        /// <summary>
        /// yetki oluşturma sayfası
        /// </summary>
        public PartialViewResult List(string id)
        {
            if (CheckPerm("Grup Yetkileri", PermTypes.Reading) == false) return null;
            var list = db.GetRolePermsFor(id).ToList();
            ViewBag.RoleName = id;
            return PartialView("List", list);
        }
        /// <summary>
        /// yetki oluştur
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public void Save([Bind(Include = "ID,RoleName,PermName,Reading,Writing,Updating,Deleting")] frmRolePerms rolePerm)
        {
            if (ModelState.IsValid && rolePerm.RoleName != "" && rolePerm.PermName != "")
            {
                if (CheckPerm("Grup Yetkileri", PermTypes.Writing) == true)
                {
                    var tbl = db.RolePerms.Where(m => m.ID == rolePerm.ID).FirstOrDefault();
                    if (tbl != null)
                    {
                        tbl.Reading = rolePerm.Reading == "on" ? true : false;
                        tbl.Writing = rolePerm.Writing == "on" ? true : false;
                        tbl.Updating = rolePerm.Updating == "on" ? true : false;
                        tbl.Deleting = rolePerm.Deleting == "on" ? true : false;
                        tbl.ModifiedDate = DateTime.Now;
                        tbl.ModifiedUser = vUser.UserName;
                    }
                    else
                    {
                        tbl = new RolePerm()
                        {
                            PermName = rolePerm.PermName,
                            RoleName = rolePerm.RoleName,
                            Reading = rolePerm.Reading == "on" ? true : false,
                            Writing = rolePerm.Writing == "on" ? true : false,
                            Updating = rolePerm.Updating == "on" ? true : false,
                            Deleting = rolePerm.Deleting == "on" ? true : false,
                            RecordDate = DateTime.Now,
                            RecordUser = vUser.UserName,
                            ModifiedDate = DateTime.Now,
                            ModifiedUser = vUser.UserName
                        };
                        db.RolePerms.Add(tbl);
                    }
                    if (tbl.Reading != false || tbl.Writing != false || tbl.Updating != false || tbl.Deleting != false || tbl.ID > 0)
                        try { db.SaveChanges(); }
                        catch (Exception ex) { Logger(ex, "Perms/Save"); }
                }
            }
        }
    }
}
