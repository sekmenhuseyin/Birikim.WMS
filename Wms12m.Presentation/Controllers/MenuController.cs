﻿using System;
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
        /// lists menus
        /// </summary>
        public ActionResult Index()
        {
            var webMenus = db.WebMenus.Where(m => m.UstMenuID == null).OrderBy(m => m.MenuYeriID).ThenBy(m => m.Sira);
            return View("Index", webMenus.ToList());
        }
        /// <summary>
        /// list sub menus
        /// </summary>
        public ActionResult SubMenu(short? id)
        {
            var webMenus = db.WebMenus.Where(m => m.UstMenuID == id).OrderBy(m => m.MenuYeriID).ThenBy(m => m.Sira);
            return View("Index", webMenus.ToList());
        }
        /// <summary>
        /// seçili menüye ait yetkileri listeler
        /// </summary>
        public ActionResult Permission(short id)
        {
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
                try { db.MenuRolEkle(tablo.MenuNo, tablo.RolNo); }
                catch (Exception) { }
            }
            var mn = db.WebMenus.Where(m => m.ID == tablo.MenuNo).FirstOrDefault();
            if (mn.UstMenuID == null)
                return Redirect("/Menu");
            else
                return Redirect("/Menu/SubMenu/" + mn.UstMenuID);
        }
        /// <summary>
        /// create menu form page
        /// </summary>
        public ActionResult Create()
        {
            ViewBag.WebSiteTipiID = new SelectList(db.ComboItem_Name.Where(m => m.ComboID == 5), "ID", "Name");
            ViewBag.MenuYeriID = new SelectList(db.ComboItem_Name.Where(m => m.ComboID == 6), "ID", "Name");
            ViewBag.UstMenuID = new SelectList(db.WebMenus.Select(m => new { m.ID, Ad = m.ComboItem_Name.Name + ", " + m.Ad }).OrderBy(m => m.Ad), "ID", "Ad");
            ViewBag.SimgeID = new SelectList(db.Simges.Select(m => new { m.ID, m.Icon }).OrderBy(m => m.Icon), "ID", "Icon");
            return View();
        }
        /// <summary>
        /// creates menu
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,WebSiteTipiID,MenuYeriID,DilAnaID,Url,SimgeID,UstMenuID,Aktif")] WebMenu webMenu)
        {
            if (ModelState.IsValid)
            {
                var sira = db.WebMenus.Where(m => m.MenuYeriID == webMenu.MenuYeriID && m.UstMenuID == webMenu.UstMenuID).OrderByDescending(m => m.Sira).Select(m => m.Sira).FirstOrDefault();
                webMenu.Sira = Convert.ToByte(sira + 1);
                db.WebMenus.Add(webMenu);
                db.SaveChanges();
                return Redirect("/Menu/Permission/" + webMenu.ID);
            }
            return RedirectToAction("Index");
        }
        /// <summary>
        /// edit menu page
        /// </summary>
        public ActionResult Edit(short id)
        {
            WebMenu webMenu = db.WebMenus.Find(id);
            if (webMenu == null)
                return RedirectToAction("Index");
            ViewBag.WebSiteTipiID = new SelectList(db.ComboItem_Name.Where(m => m.ComboID == 5), "ID", "Name", webMenu.ComboItem_Name);
            ViewBag.MenuYeriID = new SelectList(db.ComboItem_Name.Where(m => m.ComboID == 6), "ID", "Name", webMenu.ComboItem_Name1);
            ViewBag.UstMenuID = new SelectList(db.WebMenus.Select(m => new { m.ID, Ad = m.ComboItem_Name.Name + ", " + m.Ad }).OrderBy(m => m.Ad), "ID", "Ad", webMenu.UstMenuID);
            ViewBag.SimgeID = new SelectList(db.Simges.Select(m => new { m.ID, m.Icon }).OrderBy(m => m.Icon), "ID", "Icon", webMenu.SimgeID);
            return View(webMenu);
        }
        /// <summary>
        /// edits menu
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,WebSiteTipiID,MenuYeriID,DilAnaID,Sira,Url,SimgeID,UstMenuID,Aktif")] WebMenu webMenu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(webMenu).State = EntityState.Modified;
                db.SaveChanges();
                db.MenuSiralayici(webMenu.SiteTipiID, webMenu.MenuYeriID, webMenu.UstMenuID);
            }
            if (webMenu.UstMenuID == null)
                return RedirectToAction("Index");
            else
                return Redirect("/Menu/SubMenu/" + webMenu.UstMenuID);
        }
        /// <summary>
        /// menü sırasını değiştirir
        /// </summary>
        public ActionResult Move(short id, bool moveUp)
        {
            if (ModelState.IsValid)
            {
                //find needed two rows
                var current = db.WebMenus.Where(m => m.ID == id).FirstOrDefault();
                byte newSira; byte oldSira = current.Sira;
                if (moveUp == true) newSira = Convert.ToByte(current.Sira - 1); else newSira = Convert.ToByte(current.Sira + 1);
                var other = db.WebMenus.Where(m => m.SiteTipiID == current.SiteTipiID && m.MenuYeriID == current.MenuYeriID && m.UstMenuID == current.UstMenuID && m.Sira == newSira).FirstOrDefault();
                if (other != null)
                {
                    //change siras
                    current.Sira = newSira;
                    other.Sira = oldSira;
                    db.SaveChanges();
                    short tmp = 0; if (current.UstMenuID != null) tmp = Convert.ToInt16(current.UstMenuID);
                    db.MenuSiralayici(current.SiteTipiID, current.MenuYeriID, tmp);
                }
            }
            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}