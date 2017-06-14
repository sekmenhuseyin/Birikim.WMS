﻿using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m.Presentation.Areas.ToDo.Controllers
{
    public class DutiesController : RootController
    {
        // GET: MainProjectForm/Create
        public ActionResult Index()
        {
            ViewBag.DurumID = new SelectList(ComboSub.GetList(Combos.GörevYönetimDurumları.ToInt32()), "ID", "Name");
            ViewBag.OncelikID = new SelectList(ComboSub.GetList(Combos.Öncelik.ToInt32()), "ID", "Name");
            ViewBag.GorevTipiID = new SelectList(ComboSub.GetList(Combos.GörevYönetimTipleri.ToInt32()), "ID", "Name", "");
            ViewBag.DepartmanID = new SelectList(ComboSub.GetList(Combos.Departman.ToInt32()), "ID", "Name", "");
            ViewBag.ProjeFormID = new SelectList(db.ProjeForms, "ID", "Proje");
            ViewBag.Sorumlu = new SelectList(db.Users.ToList(), "Kod", "AdSoyad");
            ViewBag.Sorumlu2 = ViewBag.Sorumlu;
            ViewBag.Sorumlu3 = ViewBag.Sorumlu;
            return View(new Gorevler());
        }

        public PartialViewResult New()
        {
            ViewBag.DurumID = new SelectList(ComboSub.GetList(Combos.GörevYönetimDurumları.ToInt32()), "ID", "Name");
            ViewBag.OncelikID = new SelectList(ComboSub.GetList(Combos.Öncelik.ToInt32()), "ID", "Name");
            ViewBag.GorevTipiID = new SelectList(ComboSub.GetList(Combos.GörevYönetimTipleri.ToInt32()), "ID", "Name", "");
            ViewBag.DepartmanID = new SelectList(ComboSub.GetList(Combos.Departman.ToInt32()), "ID", "Name", "");
            ViewBag.ProjeFormID = new SelectList(db.ProjeForms, "ID", "Proje");
            ViewBag.Sorumlu = new SelectList(db.Users.ToList(), "Kod", "AdSoyad");
            ViewBag.Sorumlu2 = ViewBag.Sorumlu;
            ViewBag.Sorumlu3 = ViewBag.Sorumlu;
            return PartialView(new Gorevler());
        }

        [HttpPost]
        public PartialViewResult Duty_Details(int ID)
        {
            var gorevCalismas = db.GorevCalismas.Include(g => g.Gorevler);
            var list = gorevCalismas.Where(a => a.GorevID == ID).ToList();
            return PartialView("Duty_Details", list);

        }

        public PartialViewResult List()
        {
            var list = db.Gorevlers.ToList();
            return PartialView(list);
        }

        // GET: MainProjectForm/Edit/5
        public PartialViewResult Edit(int? id)
        {
            var tbl = db.Gorevlers.Find(id);
            Gorevler gorevler = db.Gorevlers.Find(id);

            ViewBag.DurumID = new SelectList(ComboSub.GetList(Combos.GörevYönetimDurumları.ToInt32()), "ID", "Name");
            ViewBag.OncelikID = new SelectList(ComboSub.GetList(Combos.Öncelik.ToInt32()), "ID", "Name");
            ViewBag.GorevTipiID = new SelectList(ComboSub.GetList(Combos.GörevYönetimTipleri.ToInt32()), "ID", "Name", "");
            ViewBag.ProjeFormID = new SelectList(db.ProjeForms, "ID", "Proje", gorevler.ProjeFormID);
            ViewBag.DepartmanID = new SelectList(ComboSub.GetList(Combos.Departman.ToInt32()), "ID", "Name", "");
            ViewBag.Sorumlu = new SelectList(db.Users.ToList(), "Kod", "AdSoyad");
            ViewBag.Sorumlu2 = new SelectList(db.Users.ToList(), "Kod", "AdSoyad", "");
            ViewBag.Sorumlu3 = new SelectList(db.Users.ToList(), "Kod", "AdSoyad", "");
            return PartialView("Edit", tbl);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Save([Bind(Include = "ID,ProjeFormID,Sorumlu,Sorumlu2,Sorumlu3,Gorev,Aciklama,OncelikID,DurumID,GorevTipiID,DepartmanID,TahminiBitis,BitisTarih,IslemTip,IslemSira,Kaydeden,KayitTarih,Degistiren,DegisTarih")] Gorevler gorevler)
        {
            if (ModelState.IsValid)
            {
                if (gorevler.ID == 0)
                {
                    gorevler.Degistiren = vUser.UserName;
                    gorevler.Kaydeden = vUser.UserName;
                    gorevler.DegisTarih = DateTime.Now;
                    gorevler.KayitTarih = gorevler.DegisTarih;
                    gorevler.IslemTip = 0;
                    gorevler.BitisTarih = null;
                    gorevler.IslemSira = null;

                    db.Gorevlers.Add(gorevler);
                }
                else
                {
                    var tbl = db.Gorevlers.Where(m => m.ID == gorevler.ID).FirstOrDefault();
                    tbl.Sorumlu = gorevler.Sorumlu;
                    tbl.Sorumlu2 = gorevler.Sorumlu2;
                    tbl.Sorumlu3 = gorevler.Sorumlu3;
                    tbl.Gorev = gorevler.Gorev;
                    tbl.Aciklama = gorevler.Aciklama;
                    tbl.OncelikID = gorevler.OncelikID;

                    tbl.DurumID = gorevler.DurumID;
                    tbl.GorevTipiID = gorevler.GorevTipiID;
                    tbl.DepartmanID = gorevler.DepartmanID;
                    tbl.TahminiBitis = gorevler.TahminiBitis;

                    tbl.DurumID = gorevler.DurumID;
                    tbl.GorevTipiID = gorevler.GorevTipiID;
                    tbl.DepartmanID = gorevler.DepartmanID;
                    tbl.TahminiBitis = gorevler.TahminiBitis;
                    tbl.BitisTarih = null;
                    tbl.IslemTip = 0;
                    tbl.IslemSira = null;


                }
                try
                {
                    db.SaveChanges();
                    return Json(new Result(true, gorevler.ID), JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                }
            }
            ViewBag.ProjeFormID = new SelectList(db.ProjeForms, "ID", "Proje", gorevler.ProjeFormID);
            return Json(new Result(false, "Hata oldu"), JsonRequestBehavior.AllowGet);


        }


        public JsonResult Delete(string Id)
        {
            if (CheckPerm("Görev", PermTypes.Deleting) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            Gorevler gorev = db.Gorevlers.Find(Id.ToInt32());
            db.Gorevlers.Remove(gorev);
            db.SaveChanges();

            Result _Result = new Result(true, Id.ToInt32());
            return Json(_Result, JsonRequestBehavior.AllowGet);

        }


    }
}