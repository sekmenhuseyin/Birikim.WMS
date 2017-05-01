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
        /// lists menus
        /// </summary>
        public ActionResult Index()
        {
            if (CheckPerm("Menu", PermTypes.Reading) == false) return Redirect("/");
            var webMenus = db.WebMenus.Where(m => m.UstMenuID == null).OrderBy(m => m.MenuYeriID).ThenBy(m => m.Sira);
            ViewBag.Sub = false;
            return View("Index", webMenus.ToList());
        }
        /// <summary>
        /// list sub menus
        /// </summary>
        public ActionResult SubMenu(short? id)
        {
            if (CheckPerm("Menu", PermTypes.Reading) == false) return Redirect("/");
            var webMenus = db.WebMenus.Where(m => m.UstMenuID == id).OrderBy(m => m.MenuYeriID).ThenBy(m => m.Sira);
            ViewBag.Sub = true;
            return View("Index", webMenus.ToList());
        }
        /// <summary>
        /// seçili menüye ait yetkileri listeler
        /// </summary>
        public ActionResult Permission(short id)
        {
            if (CheckPerm("Menu", PermTypes.Reading) == false) return Redirect("/");
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
                if (CheckPerm("Menu", PermTypes.Writing) == false) return Redirect("/");
                try { db.MenuRolEkle(tablo.MenuNo, tablo.RolNo); }
                catch (Exception ex)
                {
                    Logger(ex, "Menu/SavePermission");
                }
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
        [HttpPost]
        public PartialViewResult New()
        {
            if (CheckPerm("Menu", PermTypes.Reading) == false) return null;
            ViewBag.SiteTipiID = new SelectList(db.ComboItem_Name.Where(m => m.ComboID == 5), "ID", "Name");
            ViewBag.MenuYeriID = new SelectList(db.ComboItem_Name.Where(m => m.ComboID == 6), "ID", "Name");
            ViewBag.UstMenuID = new SelectList(db.WebMenus.Select(m => new { m.ID, Ad = m.ComboItem_Name1.Name + ", " + m.ComboItem_Name.Name + ", " + m.Ad }).OrderBy(m => m.Ad), "ID", "Ad");
            ViewBag.SimgeID = new SelectList(db.Simges.Select(m => new { m.ID, m.Icon }).OrderBy(m => m.Icon), "ID", "Icon");
            return PartialView("New");
        }
        /// <summary>
        /// creates menu
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SiteTipiID,MenuYeriID,Ad,Url,SimgeID,UstMenuID,Aktif")] WebMenu webMenu)
        {
            if (ModelState.IsValid)
            {
                if (CheckPerm("Menu", PermTypes.Writing) == false) return Redirect("/");
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
        [HttpPost]
        public PartialViewResult Editor()
        {
            var id = Url.RequestContext.RouteData.Values["id"];
            if (id == null || id.ToString2() == "") return null;
            //find menu
            WebMenu webMenu = db.WebMenus.Find(id.ToShort());
            if (webMenu == null)
                return null;
            if (CheckPerm("Menu", PermTypes.Reading) == false) return null;
            ViewBag.SiteTipiID = new SelectList(db.ComboItem_Name.Where(m => m.ComboID == 5), "ID", "Name", webMenu.SiteTipiID);
            ViewBag.MenuYeriID = new SelectList(db.ComboItem_Name.Where(m => m.ComboID == 6), "ID", "Name", webMenu.MenuYeriID);
            ViewBag.UstMenuID = new SelectList(db.WebMenus.Select(m => new { m.ID, Ad = m.ComboItem_Name1.Name + ", " + m.ComboItem_Name.Name + ", " + m.Ad }).OrderBy(m => m.Ad), "ID", "Ad", webMenu.UstMenuID);
            ViewBag.SimgeID = new SelectList(db.Simges.Select(m => new { m.ID, m.Icon }).OrderBy(m => m.Icon), "ID", "Icon", webMenu.SimgeID);
            return PartialView("New", webMenu);
        }
        /// <summary>
        /// edits menu
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,SiteTipiID,MenuYeriID,Ad,Sira,Url,SimgeID,UstMenuID,Aktif")] WebMenu webMenu)
        {
            if (ModelState.IsValid)
            {
                if (CheckPerm("Menu", PermTypes.Writing) == false) return Redirect("/");
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
                if (CheckPerm("Menu", PermTypes.Writing) == false) return Redirect("/");
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