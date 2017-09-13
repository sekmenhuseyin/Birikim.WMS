﻿using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m.Presentation.Areas.ToDo.Controllers
{
    public class ProjectFormController : RootController
    {


        // GET: MainProjectForm/Create
        public ActionResult Index()
        {
            ViewBag.MusteriID = new SelectList(db.Musteris.ToList(), "ID", "Firma");
            ViewBag.PID = new SelectList(db.ProjeForms.Where(x => x.PID != null).ToList(), "ID", "Proje");
            ViewBag.Sorumlu = new SelectList(db.Users.ToList(), "Kod", "AdSoyad");
            return View(new ProjeForm());
        }

        public PartialViewResult List()
        {
            var projeForms = db.ProjeForms.Include(p => p.Musteri).Include(p => p.ProjeForm2);
            // return PartialView(projeForms.ToList());
            return PartialView(projeForms.Where(a => a.PID == null).ToList());
        }

        // GET: MainProjectForm/Edit/5
        public PartialViewResult Edit(int? id)
        {

            ProjeForm projeForm = db.ProjeForms.Find(id);

            ViewBag.MusteriID = new SelectList(db.Musteris.ToList(), "ID", "Firma", projeForm.MusteriID);
            ViewBag.PID = new SelectList(db.ProjeForms.Where(x => x.PID != null).ToList(), "ID", "Proje", projeForm.PID);
            ViewBag.Sorumlu = new SelectList(db.Users.ToList(), "Kod", "AdSoyad");
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
                    projeForm.Form = "";
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
                    tbl.Form = "";
                    tbl.Durum = null;


                }
                try
                {
                    db.SaveChanges();
                    return Json(new Result(true, projeForm.ID), JsonRequestBehavior.AllowGet);
                }
                catch (Exception)
                {
                }
            }
            ViewBag.MusteriID = new SelectList(db.Musteris, "ID", "Firma", projeForm.MusteriID);
            ViewBag.PID = new SelectList(db.ProjeForms, "ID", "Proje", projeForm.PID);
            return Json(new Result(false, "Hata oldu"), JsonRequestBehavior.AllowGet);


        }


        public JsonResult Delete(string Id)
        {
            ProjeForm projeform = db.ProjeForms.Find(Id.ToInt32());
            db.ProjeForms.Remove(projeform);
            db.SaveChanges();

            Result _Result = new Result(true, Id.ToInt32());
            return Json(_Result, JsonRequestBehavior.AllowGet);

        }

        public JsonResult DeleteKontrol(string Id)
        {
            Result _Result = new Result();
            var musteri = db.Database.SqlQuery<ProjeForm>(string.Format("SELECT * FROM BIRIKIM.ong.ProjeForm where PID='{0}'", Id)).ToList();
            if (musteri.Count() > 0)
            {
                _Result.Status = false;
                _Result.Id = Id.ToInt32();
                _Result.Message = "Projeye ait form bulunduğu için silme işlemi gerçekleştirilememiştir.";
            }
            else
            {
                _Result.Status = true;
                _Result.Id = Id.ToInt32();
            }
            return Json(_Result, JsonRequestBehavior.AllowGet);

        }


        public ActionResult FormIndex()
        {
            var id = Url.RequestContext.RouteData.Values["id"];
            var ID = id.ToInt32();

            ProjeForm projeForm = db.ProjeForms.Find(ID);
            ViewBag.id = ID;
            ViewBag.MusteriID = new SelectList(db.Musteris.ToList(), "ID", "Firma", projeForm.MusteriID);
            ViewBag.PID = new SelectList(db.ProjeForms.Where(x => x.PID == null).ToList(), "ID", "Proje", projeForm.ID);
            var proje = projeForm.Proje;
            projeForm = new ProjeForm
            {
                Proje = proje
            };
            return PartialView(projeForm);
            //ViewBag.MusteriID = new SelectList(db.Musteris.ToList(), "ID", "Firma");
            //ViewBag.PID = new SelectList(db.ProjeForms.Where(x => x.PID == null).ToList(), "ID", "Proje");
            //return View(new ProjeForm());
        }

        public PartialViewResult FormList()
        {

            var projeForms = db.ProjeForms.Include(p => p.Musteri).Include(p => p.ProjeForm2);
            //ViewBag.id = ID;
            return PartialView(projeForms.Where(a => a.PID != null).ToList());
        }

        public PartialViewResult ListAlt()
        {
            var id = Url.RequestContext.RouteData.Values["id"];
            var ID = id.ToInt32();
            var projeForms = db.ProjeForms.Include(p => p.Musteri).Include(p => p.ProjeForm2);
            ViewBag.id = ID;
            return PartialView("FormList", projeForms.Where(a => a.PID == ID).ToList());
        }

        // GET: MainProjectForm/Edit/5
        public PartialViewResult FormEdit(int? id)
        {

            ProjeForm projeForm = db.ProjeForms.Find(id);

            ViewBag.MusteriID = new SelectList(db.Musteris.ToList(), "ID", "Firma", projeForm.MusteriID);
            ViewBag.PID = new SelectList(db.ProjeForms.Where(x => x.PID == null).ToList(), "ID", "Proje", projeForm.PID);
            return PartialView(projeForm);
        }


        public PartialViewResult EditAlt()//ProjectForm'dan gelen veriyi alır. Form ekler.
        {
            var id = Url.RequestContext.RouteData.Values["id"];
            var ID = id.ToInt32();
            ProjeForm projeForm = db.ProjeForms.Find(ID);
            ViewBag.id = ID;
            ViewBag.MusteriID = new SelectList(db.Musteris.ToList(), "ID", "Firma", projeForm.MusteriID);
            ViewBag.PID = new SelectList(db.ProjeForms.Where(x => x.PID == null).ToList(), "ID", "Proje", projeForm.ID);
            return PartialView(projeForm);
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult FormSave([Bind(Include = "ID,MusteriID,Proje,Form,PID,Durum,Kaydeden,KayitTarih,Degistiren,DegisTarih")] ProjeForm projeForm)
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
                    tbl.Form = projeForm.Form;//
                }
                try
                {
                    db.SaveChanges();
                    return Json(new Result(true, projeForm.ID), JsonRequestBehavior.AllowGet);
                }
                catch (Exception)
                {
                }
            }
            ViewBag.MusteriID = new SelectList(db.Musteris, "ID", "Firma", projeForm.MusteriID);
            ViewBag.PID = new SelectList(db.ProjeForms, "ID", "Proje", projeForm.PID);
            return Json(new Result(false, "Hata oldu"), JsonRequestBehavior.AllowGet);


        }
    }
}
