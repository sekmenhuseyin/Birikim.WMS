using System;
using System.Linq;
using System.Web.Mvc;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m.Presentation.Areas.ToDo.Controllers
{
    public class CustomerController : RootController
    {
        /// <summary>
        /// müşteriler
        /// </summary>
        public ActionResult Index()
        {
            if (CheckPerm(Perms.TodoMüşteri, PermTypes.Reading) == false) return Redirect("/");
            ViewBag.Yetki = CheckPerm(Perms.TodoMüşteri, PermTypes.Writing);
            return View(new Musteri());
        }
        /// <summary>
        /// liste
        /// </summary>
        public PartialViewResult List()
        {
            if (CheckPerm(Perms.TodoMüşteri, PermTypes.Reading) == false) return null;
            ViewBag.Yetki = CheckPerm(Perms.TodoMüşteri, PermTypes.Writing);
            ViewBag.Yetki2 = CheckPerm(Perms.TodoMüşteri, PermTypes.Deleting);
            return PartialView(db.Musteris.ToList());
        }
        /// <summary>
        /// düzenleme sayfası
        /// </summary>
        public PartialViewResult Edit(int? id)
        {
            Musteri musteri = db.Musteris.Find(id);
            return PartialView(musteri);
        }
        /// <summary>
        /// kaydet
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public JsonResult Save([Bind(Include = "ID,Firma,Unvan,Aciklama,Email,Tel1,Tel2,MesaiKontrol,MesaiKota")] Musteri musteri)
        {
            if (CheckPerm(Perms.TodoMüşteri, PermTypes.Writing) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            if (ModelState.IsValid)
            {
                if (musteri.ID == 0)
                {
                    musteri.Degistiren = vUser.UserName;
                    musteri.Kaydeden = vUser.UserName;
                    musteri.DegisTarih = DateTime.Now;
                    musteri.KayitTarih = musteri.DegisTarih;
                     db.Musteris.Add(musteri);
                    //destek projesi
                    var proje = new ProjeForm()
                    {
                        Musteri = musteri,
                        Proje = "Destek",
                        Form = "",
                        MesaiKontrol = false,
                        Kaydeden = vUser.UserName,
                        KayitTarih = DateTime.Now,
                        Degistiren = vUser.UserName,
                        DegisTarih = DateTime.Now
                    };
                    db.ProjeForms.Add(proje);
                }
                else
                {
                    var tbl = db.Musteris.Where(m => m.ID == musteri.ID).FirstOrDefault();
                    tbl.Firma = musteri.Firma;
                    tbl.Unvan = musteri.Unvan;
                    tbl.Aciklama = musteri.Aciklama;
                    tbl.Tel1 = musteri.Tel1;
                    tbl.Tel2 = musteri.Tel2;
                    tbl.MesaiKontrol = musteri.MesaiKontrol;
                    tbl.MesaiKota = musteri.MesaiKota;
                    tbl.Degistiren = vUser.UserName;
                    tbl.DegisTarih = DateTime.Now;
                }
                try
                {
                    db.SaveChanges();
                    LogActions("ToDo", "Customer", "Save", ComboItems.alEkle, musteri.ID, "Firma: " + musteri.Firma + ", Unvan: " + musteri.Unvan + ", MesaiKontrol: " + musteri.MesaiKontrol + ", MesaiKota: " + musteri.MesaiKota);
                    return Json(new Result(true, musteri.ID), JsonRequestBehavior.AllowGet);
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
        public JsonResult Delete(int Id)
        {
            if (CheckPerm(Perms.TodoMüşteri, PermTypes.Deleting) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            Musteri musteri = db.Musteris.Find(Id);
            db.Musteris.Remove(musteri);
            try
            {
                db.SaveChanges();
                LogActions("ToDo", "Customer", "Delete", ComboItems.alSil, musteri.ID);
                return Json(new Result(true, Id), JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new Result(false, "Firmaya ait proje bulunduğu için silme işlemi gerçekleştirilememiştir."), JsonRequestBehavior.AllowGet);
            }
        }
    }
}
