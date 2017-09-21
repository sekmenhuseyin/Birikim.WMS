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
            return View(new Musteri());
        }
        /// <summary>
        /// liste
        /// </summary>
        public PartialViewResult List()
        {
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
        /// <summary>
        /// sil
        /// </summary>
        public JsonResult Delete(string Id)
        {
            Musteri musteri = db.Musteris.Find(Id.ToInt32());
            db.Musteris.Remove(musteri);
            try
            {
                db.SaveChanges();
                return Json(new Result(true, Id.ToInt32()), JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new Result(false, "Firmaya ait proje bulunduğu için silme işlemi gerçekleştirilememiştir."), JsonRequestBehavior.AllowGet);
            }
        }
    }
}
