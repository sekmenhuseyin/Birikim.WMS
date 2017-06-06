using System;
using System.Linq;
using System.Web.Mvc;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m.Presentation.Controllers
{
    public class CustomerController : RootController
    {

        // GET: Customer/Create
        public ActionResult Index()
        {
            return View();
        }

        // GET: Customer
        public PartialViewResult List()
        {
            return PartialView(db.Musteris.ToList());
        }

        // POST: Customer/Create
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
            Musteri musteri = db.Musteris.Find(id);
            return PartialView(musteri);
        }

        // POST: Customer/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Save([Bind(Include = "ID,Firma,Unvan,Aciklama,Email,Tel1,Tel2,MesaiKontrol,MesaiKota,Kaydeden,KayitTarih,Degistiren,DegisTarih")] Musteri musteri)
        {
            if (ModelState.IsValid)
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
                db.SaveChanges();
            }
            return Json(new Result(true, musteri.ID), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete(string Id)
        {
            if (CheckPerm("Müşteri", PermTypes.Deleting) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            Musteri musteri = db.Musteris.Find(Id.ToInt32());
            db.Musteris.Remove(musteri);
            db.SaveChanges();

            Result _Result = new Result(true, Id.ToInt32());
            return Json(_Result, JsonRequestBehavior.AllowGet);

        }



    }
}
