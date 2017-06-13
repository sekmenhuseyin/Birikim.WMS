using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m.Presentation.Areas.ToDo.Controllers
{
    public class DutyWorkController : RootController
    {


        public ActionResult Index()
        {
            ViewBag.GorevID = new SelectList(db.Gorevlers.Where(a => a.Sorumlu == vUser.UserName || a.Sorumlu2 == vUser.UserName || a.Sorumlu3 == vUser.UserName).ToList(), "ID", "Gorev");
            return View(new GorevCalisma());
        }

        public PartialViewResult NewWork()
        {
            var id = Url.RequestContext.RouteData.Values["id"];
            var ID = id.ToInt32();
            ViewBag.id = ID;
            ViewBag.GorevID = new SelectList(db.Gorevlers.Where(a => (a.Sorumlu == vUser.UserName || a.Sorumlu2 == vUser.UserName || a.Sorumlu3 == vUser.UserName) && a.ID == ID).ToList(), "ID", "Gorev");
            return PartialView(new GorevCalisma());
        }

        public PartialViewResult List()
        {
            var gorevCalismas = db.GorevCalismas.Include(g => g.Gorevler);
            return PartialView(gorevCalismas.Where(a => a.Gorevler.Sorumlu == vUser.UserName || a.Gorevler.Sorumlu2 == vUser.UserName || a.Gorevler.Sorumlu3 == vUser.UserName).ToList());
        }

        public PartialViewResult Edit(int? id)
        {
            GorevCalisma gorevCalisma = db.GorevCalismas.Find(id);

            ViewBag.GorevID = new SelectList(db.Gorevlers.Where(a => a.Sorumlu == vUser.UserName || a.Sorumlu2 == vUser.UserName || a.Sorumlu3 == vUser.UserName).ToList(), "ID", "Gorev", gorevCalisma.GorevID);
            return PartialView(gorevCalisma);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Save([Bind(Include = "ID,GorevID,Tarih,CalismaSure,Calisma,Kaydeden,KayitTarih,Degistiren,DegisTarih")] GorevCalisma gorevCalisma)
        {
            if (ModelState.IsValid)
            {
                if (gorevCalisma.ID == 0)
                {
                    gorevCalisma.Degistiren = vUser.UserName;
                    gorevCalisma.Kaydeden = vUser.UserName;
                    gorevCalisma.DegisTarih = DateTime.Now;
                    gorevCalisma.KayitTarih = gorevCalisma.DegisTarih;

                    db.GorevCalismas.Add(gorevCalisma);
                }
                else
                {
                    var tbl = db.GorevCalismas.Where(m => m.ID == gorevCalisma.ID).FirstOrDefault();
                    tbl.Tarih = gorevCalisma.Tarih;//
                    tbl.CalismaSure = gorevCalisma.CalismaSure;//
                    tbl.Calisma = gorevCalisma.Calisma;//



                }
                try
                {
                    db.SaveChanges();
                    return Json(new Result(true, gorevCalisma.ID), JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                }
            }
            ViewBag.GorevID = new SelectList(db.Gorevlers, "ID", "Gorev", gorevCalisma.GorevID);
            return Json(new Result(false, "Hata oldu"), JsonRequestBehavior.AllowGet);


        }
        public JsonResult Delete(string Id)
        {
            if (CheckPerm("Görev", PermTypes.Deleting) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            GorevCalisma gorevcalisma = db.GorevCalismas.Find(Id.ToInt32());
            db.GorevCalismas.Remove(gorevcalisma);
            db.SaveChanges();

            Result _Result = new Result(true, Id.ToInt32());
            return Json(_Result, JsonRequestBehavior.AllowGet);

        }
    }
}