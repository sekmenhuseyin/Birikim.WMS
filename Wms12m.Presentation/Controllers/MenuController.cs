﻿using Birikim.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m.Presentation.Controllers
{
    public class MenuController : RootController
    {
        /// <summary>
        /// creates menu
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SiteTipiID,MenuYeriID,Ad,Url,SimgeID,UstMenuID,Aktif")] WebMenu webMenu)
        {
            if (ModelState.IsValid)
            {
                if (CheckPerm(Perms.Menü, PermTypes.Writing) == false) return Redirect("/");
                var sira = db.WebMenus.Where(m => m.MenuYeriID == webMenu.MenuYeriID && m.UstMenuID == webMenu.UstMenuID).OrderByDescending(m => m.Sira).Select(m => m.Sira).FirstOrDefault();
                webMenu.Sira = Convert.ToByte(sira + 1);
                db.WebMenus.Add(webMenu);
                db.SaveChanges();
                // log
                LogActions("", "Menu", "Create", ComboItems.alEkle, webMenu.ID, webMenu.Ad + ", " + webMenu.Url);
                return Redirect("/Menu/Permission/" + webMenu.ID);
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// edits menu
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,SiteTipiID,MenuYeriID,Ad,Sira,Url,SimgeID,UstMenuID,Aktif")] WebMenu webMenu)
        {
            if (ModelState.IsValid)
            {
                if (CheckPerm(Perms.Menü, PermTypes.Writing) == false) return Redirect("/");
                db.Entry(webMenu).State = EntityState.Modified;
                db.SaveChanges();
                db.MenuSiralayici(webMenu.SiteTipiID, webMenu.MenuYeriID, webMenu.UstMenuID);
                // log
                LogActions("", "Menu", "Edit", ComboItems.alDüzenle, webMenu.ID, webMenu.Ad + ", " + webMenu.Url);
            }

            if (webMenu.UstMenuID == null)
                return RedirectToAction("Index");
            else
                return Redirect("/Menu/SubMenu/" + webMenu.UstMenuID);
        }

        /// <summary>
        /// edit menu page
        /// </summary>
        [HttpPost]
        public PartialViewResult Editor()
        {
            var id = Url.RequestContext.RouteData.Values["id"];
            if (id == null || id.ToString2() == "") return null;
            // find menu
            var webMenu = db.WebMenus.Find(id.ToShort());
            if (webMenu == null) return null;
            if (CheckPerm(Perms.Menü, PermTypes.Reading) == false) return null;
            ViewBag.SiteTipiID = new SelectList(db.ComboItem_Name.Where(m => m.ComboID == 5), "ID", "Name", webMenu.SiteTipiID);
            ViewBag.MenuYeriID = new SelectList(db.ComboItem_Name.Where(m => m.ComboID == 6), "ID", "Name", webMenu.MenuYeriID);
            ViewBag.UstMenuID = new SelectList(db.WebMenus.Select(m => new { m.ID, Ad = m.ComboItem_Name1.Name + ", " + m.ComboItem_Name.Name + ", " + (m.UstMenuID > 0 ? (m.WebMenu2.UstMenuID > 0 ? m.WebMenu2.WebMenu2.Ad + ", " : "") + m.WebMenu2.Ad + ", " : "") + m.Ad }).OrderBy(m => m.Ad), "ID", "Ad", webMenu.UstMenuID);
            ViewBag.SimgeID = new SelectList(db.Simges.Select(m => new { m.ID, Icon = m.Icon.Replace("icon-", "") }).OrderBy(m => m.Icon), "ID", "Icon", webMenu.SimgeID);
            ViewBag.Simge = webMenu.Simge != null ? webMenu.Simge.Ad : "";
            return PartialView("New", webMenu);
        }

        /// <summary>
        /// lists menus
        /// </summary>
        public ActionResult Index()
        {
            if (CheckPerm(Perms.Menü, PermTypes.Reading) == false) return Redirect("/");
            var webMenus = db.WebMenus.Where(m => m.UstMenuID == null).OrderBy(m => m.MenuYeriID).ThenBy(m => m.Sira);
            ViewBag.Sub = false;
            return View("Index", webMenus.ToList());
        }

        /// <summary>
        /// menü sırasını değiştirir
        /// </summary>
        public ActionResult Move(short id, bool moveUp)
        {
            if (ModelState.IsValid)
            {
                if (CheckPerm(Perms.Menü, PermTypes.Writing) == false) return Redirect("/");
                // find needed two rows
                var current = db.WebMenus.FirstOrDefault(m => m.ID == id);
                byte newSira; var oldSira = current.Sira;
                if (moveUp) newSira = Convert.ToByte(current.Sira - 1); else newSira = Convert.ToByte(current.Sira + 1);
                var other = db.WebMenus.FirstOrDefault(m => m.SiteTipiID == current.SiteTipiID && m.MenuYeriID == current.MenuYeriID && m.UstMenuID == current.UstMenuID && m.Sira == newSira);
                if (other != null)
                {
                    // change siras
                    current.Sira = newSira;
                    other.Sira = oldSira;
                    db.SaveChanges();
                    short tmp = 0; if (current.UstMenuID != null) tmp = Convert.ToInt16(current.UstMenuID);
                    db.MenuSiralayici(current.SiteTipiID, current.MenuYeriID, tmp);
                }
            }

            return Redirect(Request.UrlReferrer.ToString());
        }

        /// <summary>
        /// create menu form page
        /// </summary>
        [HttpPost]
        public PartialViewResult New()
        {
            if (CheckPerm(Perms.Menü, PermTypes.Reading) == false) return null;
            ViewBag.SiteTipiID = new SelectList(db.ComboItem_Name.Where(m => m.ComboID == 5), "ID", "Name");
            ViewBag.MenuYeriID = new SelectList(db.ComboItem_Name.Where(m => m.ComboID == 6), "ID", "Name");
            ViewBag.UstMenuID = new SelectList(db.WebMenus.Select(m => new { m.ID, Ad = m.ComboItem_Name1.Name + ", " + m.ComboItem_Name.Name + ", " + (m.UstMenuID > 0 ? (m.WebMenu2.UstMenuID > 0 ? m.WebMenu2.WebMenu2.Ad + ", " : "") + m.WebMenu2.Ad + ", " : "") + m.Ad }).OrderBy(m => m.Ad), "ID", "Ad");
            ViewBag.SimgeID = new SelectList(db.Simges.Select(m => new { m.ID, Icon = m.Icon.Replace("icon-", "") }).OrderBy(m => m.Icon), "ID", "Icon");
            ViewBag.Simge = "";
            return PartialView("New", new WebMenu());
        }

        /// <summary>
        /// seçili menüye ait yetkileri listeler
        /// </summary>
        public ActionResult Permission(short id)
        {
            if (CheckPerm(Perms.Menü, PermTypes.Reading) == false) return Redirect("/");
            ViewBag.MenuID = id;
            var yetki = db.MenuRolGetir(id);
            return View("Permission", yetki);
        }

        /// <summary>
        /// seçili menüye yeni yetki ekle
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Permission(mdlCreateMenuPermission tablo)
        {
            if (ModelState.IsValid)
            {
                if (CheckPerm(Perms.Menü, PermTypes.Writing) == false) return Redirect("/");
                try
                {
                    db.MenuRolEkle(tablo.MenuNo, tablo.RolNo);
                    // log
                    LogActions("", "Menu", "Permission", ComboItems.alEkle, tablo.MenuNo.ToInt32(), "RolNo: " + tablo.RolNo);
                }
                catch (Exception ex)
                {
                    Logger(ex, "Menu/SavePermission");
                }
            }

            // return
            var mn = db.WebMenus.FirstOrDefault(m => m.ID == tablo.MenuNo);
            return mn.UstMenuID == null ? Redirect("/Menu") : Redirect("/Menu/SubMenu/" + mn.UstMenuID);
        }

        /// <summary>
        /// Yetkiler
        /// </summary>
        public ActionResult Permissions()
        {
            if (CheckPerm(Perms.Menu, PermTypes.Reading) == false) return Redirect("/");
            ViewBag.RoleName = new SelectList(db.Roles.Where(m => m.RoleName != "").ToList(), "RoleName", "RoleName");
            return View("Permissions");
        }

        /// <summary>
        /// yetki oluşturma sayfası
        /// </summary>
        public PartialViewResult PermissionsList(string id)
        {
            if (CheckPerm(Perms.GrupYetkileri, PermTypes.Reading) == false) return null;
            var list = db.GetMenuRoleFor(id).ToList();
            ViewBag.RoleName = id;
            return PartialView("PermissionsList", list);
        }

        /// <summary>
        /// yetkileri kaydet
        /// </summary>
        [HttpPost]
        public JsonResult Save(GetMenuRoleFor_Result tbl)
        {
            if (ModelState.IsValid)
                if (CheckPerm(Perms.Menu, PermTypes.Writing))
                    try
                    {
                        db.RolMenuEkle(tbl.RoleName, tbl.Ad);
                        // log
                        LogActions("", "Menu", "Save", ComboItems.alEkle, 0, "RoleName: " + tbl.RoleName + ", Web IDs: " + tbl.Ad);
                        return Json(new Result(true, 1, ""), JsonRequestBehavior.AllowGet);
                    }
                    catch (Exception ex)
                    {
                        Logger(ex, "Menu/Save");
                        return Json(new Result(false, 0, ex.Message), JsonRequestBehavior.AllowGet);
                    }

            return Json(new Result(false, 0, "yetki hatası"), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// list sub menus
        /// </summary>
        public ActionResult SubMenu(short? id)
        {
            if (CheckPerm(Perms.Menü, PermTypes.Reading) == false) return Redirect("/");
            var webMenus = db.WebMenus.Where(m => m.UstMenuID == id).OrderBy(m => m.MenuYeriID).ThenBy(m => m.Sira).ToList();
            var menu = db.WebMenus.FirstOrDefault(m => m.ID == id);
            if (menu != null) ViewBag.id = menu.UstMenuID;
            ViewBag.Sub = true;
            return View("Index", webMenus);
        }
    }
}