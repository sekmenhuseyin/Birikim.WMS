﻿using Humanizer;
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
            if (CheckPerm(Perms.GrupYetkileri, PermTypes.Reading) == false) return Redirect("/");
            ViewBag.RoleName = new SelectList(db.Roles.Where(m => m.RoleName != "").ToList(), "RoleName", "RoleName");
            return View("Index");
        }

        /// <summary>
        /// yetki oluşturma sayfası
        /// </summary>
        public PartialViewResult List(string id)
        {
            if (CheckPerm(Perms.GrupYetkileri, PermTypes.Reading) == false) return null;
            var list = db.GetRolePermsFor(id, "WMS").ToList();
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
                if (CheckPerm(Perms.GrupYetkileri, PermTypes.Writing))
                {
                    var tbl = db.RolePerms.FirstOrDefault(m => m.ID == rolePerm.ID);
                    if (tbl != null)
                    {
                        if (rolePerm.Reading != "on" && rolePerm.Writing != "on" && rolePerm.Updating != "on" && rolePerm.Updating != "on")
                        {
                            db.RolePerms.Remove(tbl);
                            // log
                            LogActions("", "Perms", "Save", ComboItems.alSil, tbl.ID, tbl.PermName);
                        }
                        else
                        {
                            tbl.Reading = rolePerm.Reading == "on";
                            tbl.Writing = rolePerm.Writing == "on";
                            tbl.Updating = rolePerm.Updating == "on";
                            tbl.Deleting = rolePerm.Deleting == "on";
                            tbl.ModifiedDate = DateTime.Now;
                            tbl.ModifiedUser = vUser.UserName;
                            // log
                            LogActions("", "Perms", "Save", ComboItems.alDüzenle, tbl.ID, tbl.PermName + ": R:" + tbl.Reading + ", W:" + tbl.Writing + ", U:" + tbl.Updating + ", D:" + tbl.Deleting);
                        }
                    }
                    else
                    {
                        tbl = new RolePerm()
                        {
                            PermName = rolePerm.PermName.Dehumanize(),
                            RoleName = rolePerm.RoleName,
                            Reading = rolePerm.Reading == "on",
                            Writing = rolePerm.Writing == "on",
                            Updating = rolePerm.Updating == "on",
                            Deleting = rolePerm.Deleting == "on",
                            RecordDate = DateTime.Now,
                            RecordUser = vUser.UserName,
                            ModifiedDate = DateTime.Now,
                            ModifiedUser = vUser.UserName
                        };
                        db.RolePerms.Add(tbl);
                        // log
                        LogActions("", "Perms", "Save", ComboItems.alEkle, tbl.ID, tbl.PermName + ": R:" + tbl.Reading + ", W:" + tbl.Writing + ", U:" + tbl.Updating + ", D:" + tbl.Deleting);
                    }

                    if (tbl.Reading || tbl.Writing || tbl.Updating || tbl.Deleting || tbl.ID > 0)
                        try { db.SaveChanges(); }
                        catch (Exception ex) { Logger(ex, "Perms/Save"); }
                }
            }
        }
    }
}