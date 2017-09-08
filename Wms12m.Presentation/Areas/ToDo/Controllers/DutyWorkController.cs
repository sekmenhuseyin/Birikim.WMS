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
            Gorevler gorevler = db.Gorevlers.Where(a => (a.Sorumlu == vUser.UserName || a.Sorumlu2 == vUser.UserName || a.Sorumlu3 == vUser.UserName) && a.ID == ID).FirstOrDefault();
            ViewBag.id = ID;
            ViewBag.GorevID = new SelectList(db.Gorevlers.Where(a => (a.Sorumlu == vUser.UserName || a.Sorumlu2 == vUser.UserName || a.Sorumlu3 == vUser.UserName) && a.ID == ID).ToList(), "ID", "Gorev");
            ViewBag.Aciklama = gorevler.Aciklama;
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
            ViewBag.Aciklama = gorevCalisma.Calisma;
            ViewBag.GorevID = new SelectList(db.Gorevlers.Where(a => a.Sorumlu == vUser.UserName || a.Sorumlu2 == vUser.UserName || a.Sorumlu3 == vUser.UserName).ToList(), "ID", "Gorev", gorevCalisma.GorevID);
            return PartialView(gorevCalisma);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Save([Bind(Include = "ID,GorevID,Tarih,CalismaSure,Calisma,Kaydeden,KayitTarih,Degistiren,DegisTarih,work,checkitem,eskiyeni")] GorevCalisma gorevCalisma)
        {
            if (ModelState.IsValid)
            {
                var grv = db.Gorevlers.Where(z => z.ID == gorevCalisma.GorevID).FirstOrDefault();
                string a = gorevCalisma.work[0];
                string b = gorevCalisma.checkitem[0];
                if (gorevCalisma.ID == 0)
                {
                    gorevCalisma.Calisma = "";
                    grv.Aciklama = "";
                    for (int i = 0; i < gorevCalisma.work.Length; i++)
                    {
                        if (gorevCalisma.checkitem[i] == "true")
                        {
                            if (gorevCalisma.eskiyeni[i] == "false")
                            {
                                gorevCalisma.Calisma += gorevCalisma.work[i] + "12MConsulting12MDA";
                            }
                            grv.Aciklama += "TTTTT" + gorevCalisma.work[i] + "12MConsulting12MDA";
                        }
                        else
                        {
                            grv.Aciklama += "FFFFF" + gorevCalisma.work[i] + "12MConsulting12MDA";
                        }

                    }
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
            GorevCalisma gorevcalisma = db.GorevCalismas.Find(Id.ToInt32());
            db.GorevCalismas.Remove(gorevcalisma);
            db.SaveChanges();

            Result _Result = new Result(true, Id.ToInt32());
            return Json(_Result, JsonRequestBehavior.AllowGet);

        }

    }
}