using System;
using System.Linq;
using System.Web.Mvc;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m.Presentation.Areas.ToDo.Controllers
{
    public class CustomerController : RootController
    {
      





        // GET: Customer/Create
        public ActionResult Index()
        {
            return View(new Musteri());
        }

        // GET: Customer
        public PartialViewResult List()
        {
            return PartialView(db.Musteris.ToList());
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
        public JsonResult Save([Bind(Include = "ID,Firma,Unvan,Aciklama,Email,Tel1,Tel2,MesaiKontrol,MesaiKota")] Musteri musteri)
        {
            if (ModelState.IsValid)
            {
                if (musteri.ID == 0)
                {
                    musteri.Degistiren = vUser.UserName;
                    musteri.Kaydeden = vUser.UserName;
                    musteri.DegisTarih = DateTime.Now;
                    musteri.KayitTarih = musteri.DegisTarih;
                    db.Musteris.Add(musteri);
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
                    return Json(new Result(true, musteri.ID), JsonRequestBehavior.AllowGet);
                }
                catch (Exception)
                {
                }
            }
            return Json(new Result(false, "Hata oldu"), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete(string Id)
        {
            Musteri musteri = db.Musteris.Find(Id.ToInt32());
            db.Musteris.Remove(musteri);
            db.SaveChanges();

            Result _Result = new Result(true, Id.ToInt32());
            return Json(_Result, JsonRequestBehavior.AllowGet);

        }

        public JsonResult DeleteKontrol(string Id)
        {
            Result _Result = new Result();
            var musteri = db.Database.SqlQuery<ProjeForm>(string.Format("SELECT * FROM BIRIKIM.ong.ProjeForm where MusteriID='{0}'", Id)).ToList();
            if (musteri.Count() > 0)
            {
                _Result.Status = false;
                _Result.Id = Id.ToInt32();
                _Result.Message = "Firmaya ait proje bulunduğu için silme işlemi gerçekleştirilememiştir.";
            }
            else
            {
                _Result.Status = true;
                _Result.Id = Id.ToInt32();
            }
            return Json(_Result, JsonRequestBehavior.AllowGet);

        }



    }
}
