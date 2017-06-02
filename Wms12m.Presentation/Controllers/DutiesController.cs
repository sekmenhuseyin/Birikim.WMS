using System;
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
    public class DutiesController : RootController
    {
        private WMSEntities db = new WMSEntities();

        // GET: Duties
        public ActionResult Index()
        {
            var gorevlers = db.Gorevlers.Include(g => g.ProjeForm);
            return View(gorevlers.ToList());
        }

        // GET: Duties/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gorevler gorevler = db.Gorevlers.Find(id);
            if (gorevler == null)
            {
                return HttpNotFound();
            }
            return View(gorevler);
        }

        // GET: Duties/Create
        public ActionResult Create()
        {
            ViewBag.DurumID = new SelectList(ComboSub.GetList(Combos.GörevYönetimDurumları.ToInt32()), "ID", "Name");
            ViewBag.OncelikID = new SelectList(ComboSub.GetList(Combos.Öncelik.ToInt32()), "ID", "Name");
            ViewBag.GorevTipiID = new SelectList(ComboSub.GetList(Combos.GörevYönetimTipleri.ToInt32()), "ID", "Name");
            ViewBag.DepartmanID = new SelectList(ComboSub.GetList(Combos.Departman.ToInt32()), "ID", "Name");
            ViewBag.ProjeFormID = new SelectList(db.ProjeForms, "ID", "Proje");
            return View();
        }

        // POST: Duties/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ProjeFormID,Sorumlu,Sorumlu2,Sorumlu3,Gorev,Aciklama,OncelikID,DurumID,GorevTipiID,DepartmanID,TahminiBitis,BitisTarih,IslemTip,IslemSira,Kaydeden,KayitTarih,Degistiren,DegisTarih")] Gorevler gorevler)
        {
            if (ModelState.IsValid)
            {
                db.Gorevlers.Add(gorevler);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProjeFormID = new SelectList(db.ProjeForms, "ID", "Proje", gorevler.ProjeFormID);
            return View(gorevler);
        }

        // GET: Duties/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gorevler gorevler = db.Gorevlers.Find(id);
            if (gorevler == null)
            {
                return HttpNotFound();
            }
            ViewBag.DurumID = new SelectList(ComboSub.GetList(Combos.GörevYönetimDurumları.ToInt32()), "ID", "Name");
            ViewBag.OncelikID = new SelectList(ComboSub.GetList(Combos.Öncelik.ToInt32()), "ID", "Name");
            ViewBag.GorevTipiID = new SelectList(ComboSub.GetList(Combos.GörevYönetimTipleri.ToInt32()), "ID", "Name");
            ViewBag.ProjeFormID = new SelectList(db.ProjeForms, "ID", "Proje", gorevler.ProjeFormID);
            ViewBag.DepartmanID = new SelectList(ComboSub.GetList(Combos.Departman.ToInt32()), "ID", "Name");
            return View(gorevler);
        }

        // POST: Duties/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ProjeFormID,Sorumlu,Sorumlu2,Sorumlu3,Gorev,Aciklama,OncelikID,DurumID,GorevTipiID,DepartmanID,TahminiBitis,BitisTarih,IslemTip,IslemSira,Kaydeden,KayitTarih,Degistiren,DegisTarih")] Gorevler gorevler)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gorevler).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProjeFormID = new SelectList(db.ProjeForms, "ID", "Proje", gorevler.ProjeFormID);
            return View(gorevler);
        }

        // GET: Duties/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gorevler gorevler = db.Gorevlers.Find(id);
            if (gorevler == null)
            {
                return HttpNotFound();
            }
            return View(gorevler);
        }

        // POST: Duties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Gorevler gorevler = db.Gorevlers.Find(id);
            db.Gorevlers.Remove(gorevler);
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
