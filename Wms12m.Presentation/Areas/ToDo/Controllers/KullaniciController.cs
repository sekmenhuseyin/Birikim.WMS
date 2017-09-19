using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Wms12m.Entity.Models;

namespace Wms12m.Presentation.Areas.ToDo.Controllers
{
    public class KullaniciController : Controller
    {
        private WMSEntities db = new WMSEntities();

        // GET: ToDo/Kullanici
        public ActionResult Index()
        {
            //var users = db.Users.Include(u => u.Users1).Include(u => u.User1).Include(u => u.Role).Include(u => u.UserDetail);
            return View(db.Users.ToList());
        }

        // GET: ToDo/Kullanici/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: ToDo/Kullanici/Create
        public ActionResult Create()
        {
            ViewBag.ID = new SelectList(db.Users, "ID", "Sirket");
            ViewBag.ID = new SelectList(db.Users, "ID", "Sirket");
            ViewBag.RoleName = new SelectList(db.Roles, "RoleName", "Aciklama");
            ViewBag.ID = new SelectList(db.UserDetails, "UserID", "GosterilecekSirket");
            return View();
        }

        // POST: ToDo/Kullanici/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Sirket,Tip,Kod,Sifre,AdSoyad,Email,RoleName,Tema,Admin,Aktif,Kaydeden,KayitTarih,KayitSaat,KayitKaynak,KayitSurum,Degistiren,DegisTarih,DegisSaat,DegisKaynak,DegisSurum,Guid")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID = new SelectList(db.Users, "ID", "Sirket", user.ID);
            ViewBag.ID = new SelectList(db.Users, "ID", "Sirket", user.ID);
            ViewBag.RoleName = new SelectList(db.Roles, "RoleName", "Aciklama", user.RoleName);
            ViewBag.ID = new SelectList(db.UserDetails, "UserID", "GosterilecekSirket", user.ID);
            return View(user);
        }

        // GET: ToDo/Kullanici/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID = new SelectList(db.Users, "ID", "Sirket", user.ID);
            ViewBag.ID = new SelectList(db.Users, "ID", "Sirket", user.ID);
            ViewBag.RoleName = new SelectList(db.Roles, "RoleName", "Aciklama", user.RoleName);
            ViewBag.ID = new SelectList(db.UserDetails, "UserID", "GosterilecekSirket", user.ID);
            return View(user);
        }

        // POST: ToDo/Kullanici/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Sirket,Tip,Kod,Sifre,AdSoyad,Email,RoleName,Tema,Admin,Aktif,Kaydeden,KayitTarih,KayitSaat,KayitKaynak,KayitSurum,Degistiren,DegisTarih,DegisSaat,DegisKaynak,DegisSurum,Guid")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID = new SelectList(db.Users, "ID", "Sirket", user.ID);
            ViewBag.ID = new SelectList(db.Users, "ID", "Sirket", user.ID);
            ViewBag.RoleName = new SelectList(db.Roles, "RoleName", "Aciklama", user.RoleName);
            ViewBag.ID = new SelectList(db.UserDetails, "UserID", "GosterilecekSirket", user.ID);
            return View(user);
        }

        // GET: ToDo/Kullanici/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: ToDo/Kullanici/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
