using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
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
        public PartialViewResult Save([Bind(Include = "ID,Firma,Unvan,Aciklama,Email,Tel1,Tel2,MesaiKontrol,MesaiKota,Kaydeden,KayitTarih,Degistiren,DegisTarih")] Musteri musteri)
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
