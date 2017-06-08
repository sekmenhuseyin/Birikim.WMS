using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m.Presentation.Controllers
{
    public class SubProjectFormController : RootController
    {

        public ActionResult Index()
        {
            ViewBag.MusteriID = new SelectList(db.Musteris.ToList(), "ID", "Firma");
            ViewBag.PID = new SelectList(db.ProjeForms.Where(x => x.PID == null).ToList(), "ID", "Proje");
            ViewBag.Sorumlu = new SelectList(db.Users.ToList(), "Kod", "AdSoyad");
            return View(new ProjeForm());
        }

        public PartialViewResult List()
        {
            var projeForms = db.ProjeForms.Include(p => p.Musteri).Include(p => p.ProjeForm2);
            return PartialView(projeForms.Where(a=>a.PID!=null).ToList());
        }

        // GET: MainProjectForm/Edit/5
        public PartialViewResult Edit(int? id)
        {

            ProjeForm projeForm = db.ProjeForms.Find(id);

            ViewBag.MusteriID = new SelectList(db.Musteris.ToList(), "ID", "Firma", projeForm.MusteriID);
            ViewBag.PID = new SelectList(db.ProjeForms.Where(x => x.PID == null).ToList(), "ID", "Proje", projeForm.PID);
            ViewBag.Sorumlu = new SelectList(db.Users.ToList(), "Kod", "AdSoyad");
            return PartialView(projeForm);
        }

       
        public PartialViewResult EditAlt(int? id)
        {

            ProjeForm projeForm = db.ProjeForms.Find(id);

            ViewBag.MusteriID = new SelectList(db.Musteris.ToList(), "ID", "Firma", projeForm.MusteriID);
            ViewBag.PID = new SelectList(db.ProjeForms.Where(x => x.PID == null).ToList(), "ID", "Proje", projeForm.PID);
            ViewBag.Sorumlu = new SelectList(db.Users.ToList(), "Kod", "AdSoyad");
            return PartialView(projeForm);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SaveAlt([Bind(Include = "ID,MusteriID,Proje,Form,Sorumlu,KarsiSorumlu,Aciklama,MesaiKontrol,MesaiKota,PID,Durum,Kaydeden,KayitTarih,Degistiren,DegisTarih")] ProjeForm projeForm)
        {

            if (ModelState.IsValid)
            {
                if (projeForm.ID == 0)
                {
                    projeForm.Degistiren = vUser.UserName;
                    projeForm.Kaydeden = vUser.UserName;
                    projeForm.DegisTarih = DateTime.Now;
                    projeForm.KayitTarih = projeForm.DegisTarih;
                    projeForm.Durum = null;

                    db.ProjeForms.Add(projeForm);
                }
                else
                {
                    var tbl = db.ProjeForms.Where(m => m.ID == projeForm.ID).FirstOrDefault();
                    tbl.Aciklama = projeForm.Aciklama;//
                    tbl.Durum = projeForm.Durum;//
                    tbl.Form = projeForm.Form;//
                    tbl.KarsiSorumlu = projeForm.KarsiSorumlu;//
                    tbl.MesaiKontrol = projeForm.MesaiKontrol;//
                    tbl.MesaiKota = projeForm.MesaiKota;//

                    tbl.Proje = projeForm.Proje;//
                    tbl.Sorumlu = projeForm.Sorumlu;//
                    tbl.Durum = null;



                }
                try
                {
                    db.SaveChanges();
                    return Json(new Result(true, projeForm.ID), JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                }
            }
            ViewBag.MusteriID = new SelectList(db.Musteris, "ID", "Firma", projeForm.MusteriID);
            ViewBag.PID = new SelectList(db.ProjeForms, "ID", "Proje", projeForm.PID);
            return Json(new Result(false, "Hata oldu"), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Save([Bind(Include = "ID,MusteriID,Proje,Form,Sorumlu,KarsiSorumlu,Aciklama,MesaiKontrol,MesaiKota,PID,Durum,Kaydeden,KayitTarih,Degistiren,DegisTarih")] ProjeForm projeForm)
        {
            if (ModelState.IsValid)
            {
                if (projeForm.ID == 0)
                {
                    projeForm.Degistiren = vUser.UserName;
                    projeForm.Kaydeden = vUser.UserName;
                    projeForm.DegisTarih = DateTime.Now;
                    projeForm.KayitTarih = projeForm.DegisTarih;
                    projeForm.Durum = null;
                   
                    db.ProjeForms.Add(projeForm);
                }
                else
                {
                    var tbl = db.ProjeForms.Where(m => m.ID == projeForm.ID).FirstOrDefault();
                    tbl.Aciklama = projeForm.Aciklama;//
                    tbl.Durum = projeForm.Durum;//
                    tbl.Form = projeForm.Form;//
                    tbl.KarsiSorumlu = projeForm.KarsiSorumlu;//
                    tbl.MesaiKontrol = projeForm.MesaiKontrol;//
                    tbl.MesaiKota = projeForm.MesaiKota;//

                    tbl.Proje = projeForm.Proje;//
                    tbl.Sorumlu = projeForm.Sorumlu;//
                    tbl.Durum = null;
                    


                }
                try
                {
                    db.SaveChanges();
                    return Json(new Result(true, projeForm.ID), JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                }
            }
            ViewBag.MusteriID = new SelectList(db.Musteris, "ID", "Firma", projeForm.MusteriID);
            ViewBag.PID = new SelectList(db.ProjeForms, "ID", "Proje", projeForm.PID);
            return Json(new Result(false, "Hata oldu"), JsonRequestBehavior.AllowGet);


        }


        public JsonResult Delete(string Id)
        {
            if (CheckPerm("ProjeForm", PermTypes.Deleting) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            ProjeForm projeform = db.ProjeForms.Find(Id.ToInt32());
            db.ProjeForms.Remove(projeform);
            db.SaveChanges();

            Result _Result = new Result(true, Id.ToInt32());
            return Json(_Result, JsonRequestBehavior.AllowGet);

        }
        //private WMSEntities db = new WMSEntities();

        //// GET: ProjectForm
        //public ActionResult Index()
        //{
        //    var projeForms = db.ProjeForms.Include(p => p.Musteri).Include(p => p.ProjeForm2);
        //    return View(projeForms.ToList());
        //}

        //// GET: ProjectForm/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ProjeForm projeForm = db.ProjeForms.Find(id);
        //    if (projeForm == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(projeForm);
        //}

        //// GET: ProjectForm/Create
        //public ActionResult Create()
        //{
        //    ViewBag.MusteriID = new SelectList(db.Musteris.ToList(), "ID", "Firma");

        //    ViewBag.PID = new SelectList(db.ProjeForms.Where(x=>x.PID==null).ToList(), "ID", "Proje");
        //    return View();
        //}

        //// POST: ProjectForm/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "ID,MusteriID,Proje,Form,Sorumlu,KarsiSorumlu,Aciklama,MesaiKontrol,MesaiKota,PID,Durum,Kaydeden,KayitTarih")] ProjeForm projeForm)
        //{
        //    if (ModelState.IsValid)
        //    {

        //        projeForm.Degistiren = vUser.UserName;
        //        projeForm.Kaydeden = vUser.UserName;
        //        DateTime date = DateTime.Now;
        //        projeForm.DegisTarih = date;
        //        projeForm.KayitTarih = date;
        //       // projeForm.Musteri = "deneme musterisi";
        //        db.ProjeForms.Add(projeForm);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.MusteriID = new SelectList(db.Musteris, "ID", "Firma", projeForm.MusteriID);
        //    ViewBag.PID = new SelectList(db.ProjeForms, "ID", "Proje", projeForm.PID);
        //    return View(projeForm);
        //}

        //// GET: ProjectForm/Edit/5
        //public ActionResult Edit(int? id)
        //{


        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ProjeForm projeForm = db.ProjeForms.Find(id);
        //    if (projeForm == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.MusteriID = new SelectList(db.Musteris.ToList(), "ID", "Firma", projeForm.MusteriID);
        //    ViewBag.PID = new SelectList(db.ProjeForms.Where(x => x.PID == null).ToList(), "ID", "Proje", projeForm.PID);
        //    return View(projeForm);
        //}

        //// POST: ProjectForm/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ID,MusteriID,Proje,Form,Sorumlu,KarsiSorumlu,Aciklama,MesaiKontrol,MesaiKota,PID,Durum,Kaydeden,KayitTarih")] ProjeForm projeForm)
        //{   
        //    if (ModelState.IsValid)
        //    {
        //        projeForm.Degistiren = vUser.UserName;
        //       // projeForm.Kaydeden = vUser.UserName;
        //        DateTime date = DateTime.Now;
        //        projeForm.DegisTarih = date;

        //      //  projeForm.KayitTarih = date;
        //        db.Entry(projeForm).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.MusteriID = new SelectList(db.Musteris, "ID", "Firma", projeForm.MusteriID);
        //    ViewBag.PID = new SelectList(db.ProjeForms, "ID", "Proje", projeForm.PID);
        //    return View(projeForm);
        //}

        //// GET: ProjectForm/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ProjeForm projeForm = db.ProjeForms.Find(id);
        //    if (projeForm == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(projeForm);
        //}

        //// POST: ProjectForm/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    ProjeForm projeForm = db.ProjeForms.Find(id);
        //    db.ProjeForms.Remove(projeForm);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
