﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Wms12m.Entity.Models;

namespace Wms12m.Presentation.Controllers
{
    public class ProjectFormController : RootController
    {
        private WMSEntities db = new WMSEntities();

        // GET: MainProjectForm
        public ActionResult Index()
        {
            var projeForms = db.ProjeForms.Include(p => p.Musteri).Include(p => p.ProjeForm2);
            return View(projeForms.ToList());
        }

        // GET: MainProjectForm/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjeForm projeForm = db.ProjeForms.Find(id);
            if (projeForm == null)
            {
                return HttpNotFound();
            }
            return View(projeForm);
        }

        // GET: MainProjectForm/Create
        public ActionResult Create()
        {
            ViewBag.MusteriID = new SelectList(db.Musteris, "ID", "Firma");
            ViewBag.PID = new SelectList(db.ProjeForms.Where(x=>x.PID!=null).ToList(), "ID", "Proje");
            return View();
        }

        // POST: MainProjectForm/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,MusteriID,Proje,Form,Sorumlu,KarsiSorumlu,Aciklama,MesaiKontrol,MesaiKota,PID,Durum,Kaydeden,KayitTarih,Degistiren,DegisTarih")] ProjeForm projeForm)
        {
            if (ModelState.IsValid)
            {
                projeForm.Degistiren = vUser.UserName;
                projeForm.Kaydeden = vUser.UserName;
                DateTime date = DateTime.Now;
                projeForm.DegisTarih = date;
                projeForm.KayitTarih = date;
                projeForm.PID = null;

                db.ProjeForms.Add(projeForm);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MusteriID = new SelectList(db.Musteris, "ID", "Firma", projeForm.MusteriID);
            ViewBag.PID = new SelectList(db.ProjeForms, "ID", "Proje", projeForm.PID);
            return View(projeForm);
        }

        // GET: MainProjectForm/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjeForm projeForm = db.ProjeForms.Find(id);
            if (projeForm == null)
            {
                return HttpNotFound();
            }
            ViewBag.MusteriID = new SelectList(db.Musteris, "ID", "Firma", projeForm.MusteriID);
            ViewBag.PID = new SelectList(db.ProjeForms.Where(x => x.PID != null).ToList(), "ID", "Proje", projeForm.PID);
            return View(projeForm);
        }

        // POST: MainProjectForm/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,MusteriID,Proje,Form,Sorumlu,KarsiSorumlu,Aciklama,MesaiKontrol,MesaiKota,PID,Durum,Kaydeden,KayitTarih,Degistiren,DegisTarih")] ProjeForm projeForm)
        {
            if (ModelState.IsValid)
            {
                projeForm.Degistiren = vUser.UserName;
               // projeForm.Kaydeden = vUser.UserName;
                DateTime date = DateTime.Now;
                projeForm.DegisTarih = date;
                //projeForm.KayitTarih = date;
                projeForm.PID = null;

                db.Entry(projeForm).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MusteriID = new SelectList(db.Musteris, "ID", "Firma", projeForm.MusteriID);
            ViewBag.PID = new SelectList(db.ProjeForms, "ID", "Proje", projeForm.PID);
            return View(projeForm);
        }

        // GET: MainProjectForm/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjeForm projeForm = db.ProjeForms.Find(id);
            if (projeForm == null)
            {
                return HttpNotFound();
            }
            return View(projeForm);
        }

        // POST: MainProjectForm/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProjeForm projeForm = db.ProjeForms.Find(id);
            db.ProjeForms.Remove(projeForm);
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