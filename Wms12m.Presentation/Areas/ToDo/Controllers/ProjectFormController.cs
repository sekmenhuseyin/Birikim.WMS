using System;
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
        /// <summary>
        /// proje sayfası
        /// </summary>
        public ActionResult Index()
        {
            ViewBag.MusteriID = new SelectList(db.Musteris.OrderBy(m => m.Unvan).ToList(), "ID", "Unvan ");
            ViewBag.Sorumlu = new SelectList(Persons.GetList(), "Kod", "AdSoyad");
            return View(new ProjeForm());
        }
        /// <summary>
        /// liste
        /// </summary>
        public PartialViewResult List()
        {
            var projeForms = db.ProjeForms.Include(p => p.Musteri).Include(p => p.ProjeForm2);
            // return PartialView(projeForms.ToList());
            return PartialView(projeForms.Where(a => a.PID == null).ToList());
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
            ProjeForm projeform = db.ProjeForms.Find(Id.ToInt32());
            db.ProjeForms.Remove(projeform);
            try
            {
                db.SaveChanges();
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
        public ActionResult FormIndex()
        {
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
            return PartialView(projeForm);
        }
        /// <summary>
        /// form listesi
        /// </summary>
        public PartialViewResult FormList()
        {
            var id = Url.RequestContext.RouteData.Values["id"];
            var ID = id.ToInt32();
            ViewBag.id = ID;
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
                    return Json(new Result(true, projeForm.PID.Value), JsonRequestBehavior.AllowGet);
                }
                catch (Exception)
                {
                }
            }
            return Json(new Result(false, "Hata oldu"), JsonRequestBehavior.AllowGet);
        }
    }
}
