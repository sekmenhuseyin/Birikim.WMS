﻿using System;
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
    public class ProjectFormController : RootController
    {
        private WMSEntities db = new WMSEntities();

     
        // GET: MainProjectForm/Create
        public ActionResult Index()
        {
            ViewBag.MusteriID = new SelectList(db.Musteris, "ID", "Firma");
            ViewBag.PID = new SelectList(db.ProjeForms.Where(x=>x.PID!=null).ToList(), "ID", "Proje");
            return View(new ProjeForm());
        }

        public ActionResult List()
        {
            var projeForms = db.ProjeForms.Include(p => p.Musteri).Include(p => p.ProjeForm2);
            return View(projeForms.ToList());
        }

        // GET: MainProjectForm/Edit/5
        public PartialViewResult Edit(int? id)
        {

            ProjeForm projeForm = db.ProjeForms.Find(id);

            ViewBag.MusteriID = new SelectList(db.Musteris, "ID", "Firma", projeForm.MusteriID);
            ViewBag.PID = new SelectList(db.ProjeForms.Where(x => x.PID != null).ToList(), "ID", "Proje", projeForm.PID);
            return PartialView(projeForm);
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

            //if (ModelState.IsValid)
            //{
            //    projeForm.Degistiren = vUser.UserName;
            //    projeForm.Kaydeden = vUser.UserName;
            //    DateTime date = DateTime.Now;
            //    projeForm.DegisTarih = date;
            //    projeForm.KayitTarih = date;
            //    projeForm.PID = null;

            //    db.ProjeForms.Add(projeForm);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}

            //ViewBag.MusteriID = new SelectList(db.Musteris, "ID", "Firma", projeForm.MusteriID);
            //ViewBag.PID = new SelectList(db.ProjeForms, "ID", "Proje", projeForm.PID);
            //return View(projeForm);
        }

 
        public JsonResult Delete(string Id)
        {
            if (CheckPerm("ProjeForm", PermTypes.Deleting) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            Musteri musteri = db.Musteris.Find(Id.ToInt32());
            db.Musteris.Remove(musteri);
            db.SaveChanges();

            Result _Result = new Result(true, Id.ToInt32());
            return Json(_Result, JsonRequestBehavior.AllowGet);

        }

        //// GET: MainProjectForm/Delete/5
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


        //// POST: MainProjectForm/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    ProjeForm projeForm = db.ProjeForms.Find(id);
        //    db.ProjeForms.Remove(projeForm);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

    }
}
