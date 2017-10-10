﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m.Presentation.Areas.ToDo.Controllers
{
    public class ProjectFormController : RootController
    {
        /// <summary>
        /// proje sayfası
        /// </summary>
        public ActionResult Index()
        {
            if (CheckPerm(Perms.TodoProje, PermTypes.Reading) == false) return Redirect("/");
            ViewBag.MusteriID = new SelectList(db.Musteris.OrderBy(m => m.Unvan).ToList(), "ID", "Unvan ");
            ViewBag.Sorumlu = new SelectList(Persons.GetList(), "Kod", "AdSoyad");
            ViewBag.Yetki = CheckPerm(Perms.TodoProje, PermTypes.Writing);
            return View("Index", new ProjeForm());
        }
        /// <summary>
        /// liste
        /// </summary>
        public PartialViewResult List()
        {
            if (CheckPerm(Perms.TodoProje, PermTypes.Reading) == false) return null;
            ViewBag.Yetki = CheckPerm(Perms.TodoProje, PermTypes.Writing);
            ViewBag.Yetki2 = CheckPerm(Perms.TodoProje, PermTypes.Deleting);
            return PartialView("List", db.ProjeForms.Where(a => a.PID == null).ToList());
        }
        /// <summary>
        /// düzeneleme sayfası
        /// </summary>
        public PartialViewResult Edit(int? id)
        {

            ProjeForm projeForm = db.ProjeForms.Find(id);

            ViewBag.MusteriID = new SelectList(db.Musteris.OrderBy(m => m.Unvan).ToList(), "ID", "Firma", projeForm.MusteriID);
            ViewBag.PID = new SelectList(db.ProjeForms.Where(x => x.PID != null).ToList(), "ID", "Proje", projeForm.PID);
            ViewBag.Sorumlu = new SelectList(db.Users.ToList(), "Kod", "AdSoyad");
            return PartialView(projeForm);
        }
        /// <summary>
        /// kaydet
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public JsonResult Save(ProjeForm projeForm)
        {
            if (CheckPerm(Perms.TodoProje, PermTypes.Writing) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            if (ModelState.IsValid)
            {
                if (projeForm.ID == 0)
                {
                    projeForm.Degistiren = vUser.UserName;
                    projeForm.Kaydeden = vUser.UserName;
                    projeForm.DegisTarih = DateTime.Now;
                    projeForm.KayitTarih = projeForm.DegisTarih;
                    projeForm.Form = "";
                    db.ProjeForms.Add(projeForm);
                }
                else
                {
                    var tbl = db.ProjeForms.Where(m => m.ID == projeForm.ID).FirstOrDefault();
                    tbl.Aciklama = projeForm.Aciklama;
                    tbl.Form = projeForm.Form;
                    tbl.KarsiSorumlu = projeForm.KarsiSorumlu;
                    tbl.MesaiKontrol = projeForm.MesaiKontrol;
                    tbl.MesaiKota = projeForm.MesaiKota;
                    tbl.Proje = projeForm.Proje;
                    tbl.Sorumlu = projeForm.Sorumlu;
                    tbl.Form = "";
                }
                try
                {
                    db.SaveChanges();
                    LogActions("ToDo", "ProjectForm", "Save", ComboItems.alEkle, projeForm.ID, "Proje: " + projeForm.Proje);
                    return Json(new Result(true, projeForm.ID), JsonRequestBehavior.AllowGet);
                }
                catch (Exception)
                {
                }
            }
            return Json(new Result(false, "Hata oldu"), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// sil
        /// </summary>
        public JsonResult Delete(string Id)
        {
            if (CheckPerm(Perms.TodoProje, PermTypes.Deleting) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            ProjeForm projeform = db.ProjeForms.Find(Id.ToInt32());
            db.ProjeForms.Remove(projeform);
            try
            {
                db.SaveChanges();
                LogActions("ToDo", "ProjectForm", "Delete", ComboItems.alSil, Id.ToInt32());
                return Json(new Result(true, Id.ToInt32()), JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new Result(false, "Projeye ait form bulunduğu için silme işlemi gerçekleştirilememiştir."), JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// formlar
        /// </summary>
        public PartialViewResult FormIndex()
        {
            if (CheckPerm(Perms.TodoProje, PermTypes.Reading) == false) return null;
            var id = Url.RequestContext.RouteData.Values["id"];
            var ID = id.ToInt32();
            ProjeForm projeForm = db.ProjeForms.Find(ID);
            ViewBag.id = ID;
            ViewBag.MusteriID = new SelectList(db.Musteris.Where(m => m.ID == projeForm.MusteriID).ToList(), "ID", "Unvan", projeForm.MusteriID);
            ViewBag.PID = new SelectList(db.ProjeForms.Where(x => x.PID == null).ToList(), "ID", "Proje", projeForm.ID);
            projeForm = new ProjeForm
            {
                Proje = projeForm.Proje
            };
            ViewBag.Yetki = CheckPerm(Perms.TodoProje, PermTypes.Writing);
            return PartialView(projeForm);
        }
        /// <summary>
        /// form listesi
        /// </summary>
        public PartialViewResult FormList()
        {
            if (CheckPerm(Perms.TodoProje, PermTypes.Reading) == false) return null;
            var id = Url.RequestContext.RouteData.Values["id"];
            var ID = id.ToInt32();
            ViewBag.id = ID;
            ViewBag.Yetki = CheckPerm(Perms.TodoProje, PermTypes.Writing);
            ViewBag.Yetki2 = CheckPerm(Perms.TodoProje, PermTypes.Deleting);
            return PartialView("FormList", db.ProjeForms.Where(a => a.PID == ID).ToList());
        }
        /// <summary>
        /// form edit
        /// </summary>
        public PartialViewResult FormEdit(int? id)
        {
            ProjeForm projeForm = db.ProjeForms.Find(id);
            ViewBag.MusteriID = new SelectList(db.Musteris.Where(m => m.ID == projeForm.MusteriID).ToList(), "ID", "Unvan", projeForm.MusteriID);
            ViewBag.PID = new SelectList(db.ProjeForms.Where(x => x.PID == null).ToList(), "ID", "Proje", projeForm.PID);
            return PartialView(projeForm);
        }
        /// <summary>
        /// form kaydeder
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public JsonResult FormSave(ProjeForm projeForm)
        {
            if (CheckPerm(Perms.TodoProje, PermTypes.Writing) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
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
                    tbl.Form = projeForm.Form;
                }
                try
                {
                    db.SaveChanges();
                    LogActions("ToDo", "ProjectForm", "FormSave", ComboItems.alEkle, projeForm.ID, "Form: " + projeForm.Form);
                    return Json(new Result(true, projeForm.PID.Value), JsonRequestBehavior.AllowGet);
                }
                catch (Exception)
                {
                }
            }
            return Json(new Result(false, "Hata oldu"), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// müşteriye ait olanları getir
        /// </summary>
        public List<Durum> GetList(int MusteriID)
        {
            return db.ProjeForms.Where(m => m.MusteriID == MusteriID).Select(m => new Durum { ID = m.ID, Name = m.Proje }).ToList();
        }
    }
}
