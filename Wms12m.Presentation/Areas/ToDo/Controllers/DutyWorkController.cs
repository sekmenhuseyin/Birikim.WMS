using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
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
            Gorevler gorev = db.Gorevlers.Find(ID);
            ViewBag.id = ID;
            ViewBag.GorevID = new SelectList(db.Gorevlers.Where(a => (a.Sorumlu == vUser.UserName || a.Sorumlu2 == vUser.UserName || a.Sorumlu3 == vUser.UserName) && a.ID == ID).ToList(), "ID", "Gorev");
            GorevCalisma aa = new GorevCalisma();
            aa.Gorevler = gorev;
            return PartialView(aa);
        }

        public PartialViewResult List()
        {
            var gorevCalismas = db.GorevCalismas.Include(g => g.Gorevler);
            return PartialView(gorevCalismas.Where(a => a.Gorevler.Sorumlu == vUser.UserName || a.Gorevler.Sorumlu2 == vUser.UserName || a.Gorevler.Sorumlu3 == vUser.UserName).ToList());
        }

        public PartialViewResult Edit(int? id)
        {
            GorevCalisma gorevCalisma = db.GorevCalismas.Find(id);
            var ids = gorevCalisma.ToDoListID.Split(',');
            List<GorevToDoList> grv = db.GorevToDoLists.Where(x => ((x.OnayDurum == false) && (x.AktifPasif == true) && (x.GorevID == gorevCalisma.GorevID)) || ids.Contains(x.ID.ToString())).ToList();
            ViewBag.Aciklama = gorevCalisma.Calisma;
            ViewBag.GorevID = new SelectList(db.Gorevlers.Where(a => a.Sorumlu == vUser.UserName || a.Sorumlu2 == vUser.UserName || a.Sorumlu3 == vUser.UserName).ToList(), "ID", "Gorev", gorevCalisma.GorevID);
            ViewBag.TodoList = db.GorevToDoLists.Where(x => ((x.OnayDurum == false) && (x.AktifPasif == true) && (x.GorevID == gorevCalisma.GorevID)) || ids.Contains(x.ID.ToString())).ToList();
            return PartialView(gorevCalisma);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Save([Bind(Include = "ID,GorevID,Tarih,CalismaSure,Calisma,Kaydeden,KayitTarih,Degistiren,DegisTarih,work,checkitem,todo")] GorevCalisma gorevCalisma)
        {
            if (ModelState.IsValid)
            {
                if (gorevCalisma.ID == 0)
                {
                    gorevCalisma.ToDoListID = "";
                    for (int i = 0; i < gorevCalisma.work.Length; i++)
                    {
                        if (gorevCalisma.checkitem[i] == "true")
                        {
                            gorevCalisma.ToDoListID += gorevCalisma.todo[i] + ",";
                            var idd = gorevCalisma.todo[i];
                            var grvtodo = db.GorevToDoLists.Where(m => m.ID == idd).FirstOrDefault();
                            grvtodo.OnayDurum = true;
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

                    for (int i = 0; i < gorevCalisma.work.Length; i++)
                    {

                        var id2 = Convert.ToInt32(gorevCalisma.todo[i]);
                        var grv = db.GorevToDoLists.Where(m => m.ID == id2).FirstOrDefault();
                        if (grv.OnayDurum != Convert.ToBoolean(gorevCalisma.checkitem[i]) && grv.AktifPasif != false)
                        {
                            grv.DegisTarih = DateTime.Now;
                            grv.Degistiren = vUser.UserName;
                            grv.OnayDurum = Convert.ToBoolean(gorevCalisma.checkitem[i]);


                        }
                    }


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

        public string GorevToDoList(int ID)
        {
            var grvToDO = db.GorevToDoLists.Where(m => m.GorevID == ID).ToList();
            var json = new JavaScriptSerializer().Serialize(grvToDO);
            return json;

        }

    }
}