﻿using System;
using System.Collections.Generic;
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
            ViewBag.GorevID = new SelectList(db.Gorevlers.Where(a => a.Sorumlu == vUser.UserName || a.Sorumlu2 == vUser.UserName || a.Sorumlu3 == vUser.UserName || a.KaliteKontrol == vUser.UserName).ToList(), "ID", "Gorev");
            return View(new GorevCalisma());
        }

        public PartialViewResult NewWork()
        {
            var id = Url.RequestContext.RouteData.Values["id"];
            var ID = id.ToInt32();
            Gorevler gorev = db.Gorevlers.Find(ID);
            ViewBag.id = ID;
            ViewBag.RoleName = vUser.RoleName;
            ViewBag.GorevID = new SelectList(db.Gorevlers.Where(a => (a.Sorumlu == vUser.UserName || a.Sorumlu2 == vUser.UserName || a.Sorumlu3 == vUser.UserName || (a.Kontrol == true && a.KaliteKontrol == vUser.UserName)) && a.ID == ID).ToList(), "ID", "Gorev");
            GorevCalisma aa = new GorevCalisma();

            aa.Gorevler = gorev;
            return PartialView(aa);
        }

        public PartialViewResult List()
        {
            List<GorevCalisma> gorevCalismas = db.GorevCalismas.Where(a => a.Gorevler.Sorumlu == vUser.UserName || a.Gorevler.Sorumlu2 == vUser.UserName || a.Gorevler.Sorumlu3 == vUser.UserName || a.Gorevler.KaliteKontrol == vUser.UserName).ToList();
            return PartialView(gorevCalismas);
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
            var sayac = 0;
            if (ModelState.IsValid)
            {
                if (gorevCalisma.ID == 0)
                {
                    var kontOnay = false;
                    if (vUser.RoleName == "Destek")
                    {
                        kontOnay = true;
                    }
                    gorevCalisma.ToDoListID = "";
                    var grv = db.Gorevlers.Where(a => a.ID == gorevCalisma.GorevID).FirstOrDefault();
                    for (int i = 0; i < gorevCalisma.work.Length; i++)
                    {
                        if (gorevCalisma.checkitem[i] == "true")
                        {
                            gorevCalisma.ToDoListID += gorevCalisma.todo[i] + ",";
                            var idd = gorevCalisma.todo[i];
                            var grvtodo = db.GorevToDoLists.Where(m => m.ID == idd).FirstOrDefault();
                            grvtodo.OnayDurum = true;
                            grvtodo.Kontrol = true;
                            grvtodo.KontrolOnay = kontOnay;

                            grv.Kontrol = true;
                        }
                    }
                    for (int i = 0; i < gorevCalisma.checkitem.Length; i++)
                    {
                        if (gorevCalisma.checkitem[i] == "false")
                        {
                            sayac++;
                        }
                    }
                    if (sayac == 0)
                    {
                        if (kontOnay == true)
                        {
                            grv.AktifPasif = false;
                        }
                        else
                        {
                            var tipId = Combos.GörevYönetimTipleri.ToInt32();
                            var gorevTipleri = db.ComboItem_Name.Where(a => a.ComboID == tipId && a.Name == "Kalite Kontrol").FirstOrDefault();
                            grv.GorevTipiID = gorevTipleri.ID;
                        }
                        //grv.Sorumlu = grv.KaliteKontrol;
                        //grv.Sorumlu2 = grv.KaliteKontrol;
                        //grv.Sorumlu3 = grv.KaliteKontrol;
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
                    tbl.ToDoListID = "";
                    for (int i = 0; i < gorevCalisma.work.Length; i++)
                    {
                        if (Convert.ToBoolean(gorevCalisma.checkitem[i]) == true)
                        {
                            tbl.ToDoListID += gorevCalisma.todo[i] + ",";
                        }
                        var id2 = Convert.ToInt32(gorevCalisma.todo[i]);
                        var grv = db.GorevToDoLists.Where(m => m.ID == id2).FirstOrDefault();
                        if (grv.OnayDurum != Convert.ToBoolean(gorevCalisma.checkitem[i]) && grv.AktifPasif != false)
                        {
                            var gr = db.Gorevlers.Where(a => a.ID == grv.GorevID).FirstOrDefault();
                            grv.DegisTarih = DateTime.Now;
                            grv.Degistiren = vUser.UserName;
                            grv.OnayDurum = Convert.ToBoolean(gorevCalisma.checkitem[i]);

                            if (grv.OnayDurum == true)
                            {
                                sayac++;
                                grv.Kontrol = true;
                                grv.KontrolOnay = false;

                                gr.Kontrol = true;

                            }
                            else
                            {
                                grv.Kontrol = false;
                                grv.KontrolOnay = false;
                                if (sayac == 0)
                                {
                                    var z = db.GorevToDoLists.Where(m => m.GorevID == grv.GorevID && m.OnayDurum == true && m.Kontrol == true && m.KontrolOnay == false && m.AktifPasif != false).ToList();
                                    if (z.Count <= 1)
                                    {
                                        gr.Kontrol = false;
                                    }
                                }

                            }

                        }
                    }


                }

                try
                {
                    db.SaveChanges();
                    return Json(new Result(true, gorevCalisma.ID), JsonRequestBehavior.AllowGet);
                }
                catch (Exception)
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

        public string GorevToDoList()
        {
            var id = Url.RequestContext.RouteData.Values["id"];
            var ID = id.ToInt32();
            List<GorevToDoList> grvToDO = db.GorevToDoLists.Where(m => m.GorevID == ID).ToList();
            List<frmGorevTodos> aa = new List<frmGorevTodos>();
            foreach (GorevToDoList item in grvToDO)
            {
                frmGorevTodos a = new frmGorevTodos();
                a.ID = item.ID;
                a.AktifPasif = item.AktifPasif;
                a.Aciklama = item.Aciklama;
                a.OnayDurum = item.OnayDurum;

                aa.Add(a);

            }
            var json = new JavaScriptSerializer().Serialize(aa);
            return json;

        }

    }
}