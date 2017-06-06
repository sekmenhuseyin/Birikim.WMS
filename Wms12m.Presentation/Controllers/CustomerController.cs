using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Wms12m.Entity.Models;

namespace Wms12m.Presentation.Controllers
{
    public class CustomerController : RootController
    {

        // GET: Customer
        public PartialViewResult List()
        {
            return PartialView(db.Musteris.ToList());
        }

        // GET: Customer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Musteri musteri = db.Musteris.Find(id);
            if (musteri == null)
            {
                return HttpNotFound();
            }
            return View(musteri);
        }

        // GET: Customer/Create
        public ActionResult Index()
        {
            return View();
        }

        // POST: Customer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "ID,Firma,Unvan,Aciklama,Email,Tel1,Tel2,MesaiKontrol,MesaiKota,Kaydeden,KayitTarih,Degistiren,DegisTarih")] Musteri musteri)
        {
            if (ModelState.IsValid)
            {
                musteri.Degistiren = vUser.UserName;
                musteri.Kaydeden = vUser.UserName;
                DateTime date = DateTime.Now;
                musteri.DegisTarih = date;
                musteri.KayitTarih = date;
                db.Musteris.Add(musteri);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(musteri);
        }

        // GET: Customer/Edit/5
        public PartialViewResult Edit(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            Musteri musteri = db.Musteris.Find(id);
            //if (musteri == null)
            //{
            //    return HttpNotFound();
            //}
            return PartialView(musteri);
        }

        // POST: Customer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public PartialViewResult Edit([Bind(Include = "ID,Firma,Unvan,Aciklama,Email,Tel1,Tel2,MesaiKontrol,MesaiKota,Kaydeden,KayitTarih,Degistiren,DegisTarih")] Musteri musteri)
        {
            if (ModelState.IsValid)
            {
                musteri.Degistiren = vUser.UserName;
                // projeForm.Kaydeden = vUser.UserName;
                DateTime date = DateTime.Now;
                musteri.DegisTarih = date;

                db.Entry(musteri).State = EntityState.Modified;
                db.SaveChanges();
               // return RedirectToAction("Index");
            }
            return PartialView(musteri);
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Musteri musteri = db.Musteris.Find(id);
            if (musteri == null)
            {
                return HttpNotFound();
            }
            return View(musteri);
        }

        // POST: Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Musteri musteri = db.Musteris.Find(id);
            db.Musteris.Remove(musteri);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
