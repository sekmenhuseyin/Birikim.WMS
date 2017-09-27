using System;
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
        /// <summary>
        /// çalışmalar
        /// </summary>
        public ActionResult Index()
        {
            ViewBag.GorevID = new SelectList(db.Gorevlers.Where(a => a.Sorumlu == vUser.UserName || a.Sorumlu2 == vUser.UserName || a.Sorumlu3 == vUser.UserName || a.KaliteKontrol == vUser.UserName).ToList(), "ID", "Gorev");
            return View(new GorevCalisma());
        }
        /// <summary>
        /// yeni çalışma
        /// </summary>
        public PartialViewResult New()
        {
            var id = Url.RequestContext.RouteData.Values["id"];
            var ID = id.ToInt32();
            Gorevler gorev = db.Gorevlers.Find(ID);
            ViewBag.RoleName = vUser.RoleName;
            ViewBag.GorevID = new SelectList(db.Gorevlers.Where(m => m.ID == ID).ToList(), "ID", "Gorev");
            GorevCalisma grv = new GorevCalisma
            {
                Gorevler = gorev
            };
            return PartialView(grv);
        }
        /// <summary>
        /// yeni çalışma
        /// </summary>
        public PartialViewResult NewAll()
        {
            ViewBag.RoleName = vUser.RoleName;
            ViewBag.GorevID = new SelectList(db.Gorevlers.Where(m => m.Sorumlu == vUser.UserName || m.Sorumlu2 == vUser.UserName || m.Sorumlu3 == vUser.UserName || m.KaliteKontrol == vUser.UserName).ToList(), "ID", "Gorev");
            return PartialView(new GorevCalisma());
        }
        /// <summary>
        /// liste
        /// </summary>
        public PartialViewResult List()
        {
            var gorevCalismas = db.GorevCalismas.Where(a => a.Kaydeden == vUser.UserName || a.Gorevler.Sorumlu == vUser.UserName || a.Gorevler.Sorumlu2 == vUser.UserName || a.Gorevler.Sorumlu3 == vUser.UserName || a.Gorevler.KaliteKontrol == vUser.UserName).OrderByDescending(m => m.Tarih).ToList();
            ViewBag.Yetki = vUser.RoleName;
            return PartialView("List", gorevCalismas);
        }
        /// <summary>
        /// düzenle
        /// </summary>
        public PartialViewResult Edit(int Id)
        {
            GorevCalisma gorevCalisma = db.GorevCalismas.Find(Id);
            var ids = gorevCalisma.ToDoListID.Split(',');
            ViewBag.GorevID = new SelectList(db.Gorevlers.Where(a => a.Sorumlu == vUser.UserName || a.Sorumlu2 == vUser.UserName || a.Sorumlu3 == vUser.UserName).ToList(), "ID", "Gorev", gorevCalisma.GorevID);
            ViewBag.TodoList = db.GorevToDoLists.Where(x => ((x.OnayDurum == false) && (x.AktifPasif == true) && (x.GorevID == gorevCalisma.GorevID)) || ids.Contains(x.ID.ToString())).ToList();
            return PartialView(gorevCalisma);
        }
        /// <summary>
        /// kaydet
        /// </summary>
        /// <param name="gorevCalisma"></param>
        /// <returns></returns>
        [HttpPost, ValidateAntiForgeryToken]
        public JsonResult Save([Bind(Include = "ID,GorevID,Tarih,CalismaSure,Calisma,Kaydeden,KayitTarih,Degistiren,DegisTarih")] GorevCalisma gorevCalisma, string[] work, string[] checkitem, string[] todo)
        {
            var sayac = 0;
            if (ModelState.IsValid)
            {
                //yeni çalışma girerken
                if (gorevCalisma.ID == 0)
                {
                    var kontOnay = false;
                    if (vUser.RoleName == "Destek")
                        kontOnay = true;
                    gorevCalisma.ToDoListID = "";
                    var grv = db.Gorevlers.Where(a => a.ID == gorevCalisma.GorevID).FirstOrDefault();
                    for (int i = 0; i < work.Length; i++)
                    {
                        if (checkitem[i] == "true")
                        {
                            gorevCalisma.ToDoListID += todo[i] + ",";
                            var idx = todo[i].ToInt32();
                            var grvtodo = db.GorevToDoLists.Where(m => m.ID == idx).FirstOrDefault();
                            grvtodo.OnayDurum = true;
                            grvtodo.Kontrol = true;
                            grvtodo.KontrolOnay = kontOnay;
                            grv.Kontrol = true;
                        }
                    }
                    for (int i = 0; i < checkitem.Length; i++)
                    {
                        if (checkitem[i] == "false")
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
                            grv.GorevTipiID = ComboItems.gytKaliteKontrol.ToInt32();
                        }
                    }
                    gorevCalisma.Degistiren = vUser.UserName;
                    gorevCalisma.Kaydeden = vUser.UserName;
                    gorevCalisma.DegisTarih = DateTime.Now;
                    gorevCalisma.KayitTarih = gorevCalisma.DegisTarih;
                    db.GorevCalismas.Add(gorevCalisma);
                }
                //çalışma güncelleme
                else
                {
                    var tbl = db.GorevCalismas.Where(m => m.ID == gorevCalisma.ID).FirstOrDefault();
                    tbl.Tarih = gorevCalisma.Tarih;//
                    tbl.CalismaSure = gorevCalisma.CalismaSure;//
                    tbl.Calisma = gorevCalisma.Calisma;//
                    tbl.ToDoListID = "";
                    for (int i = 0; i < work.Length; i++)
                    {
                        if (Convert.ToBoolean(checkitem[i]) == true)
                        {
                            tbl.ToDoListID += todo[i] + ",";
                        }
                        var id2 = Convert.ToInt32(todo[i]);
                        var grv = db.GorevToDoLists.Where(m => m.ID == id2).FirstOrDefault();
                        if (grv.OnayDurum != Convert.ToBoolean(checkitem[i]) && grv.AktifPasif != false)
                        {
                            var gr = db.Gorevlers.Where(a => a.ID == grv.GorevID).FirstOrDefault();
                            grv.DegisTarih = DateTime.Now;
                            grv.Degistiren = vUser.UserName;
                            grv.OnayDurum = Convert.ToBoolean(checkitem[i]);

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
            return Json(new Result(false, "Hata oldu"), JsonRequestBehavior.AllowGet);


        }
        /// <summary>
        /// sil
        /// </summary>
        public JsonResult Delete(string Id)
        {
            GorevCalisma gorevcalisma = db.GorevCalismas.Find(Id.ToInt32());
            db.GorevCalismas.Remove(gorevcalisma);
            try
            {
                db.SaveChanges();
                return Json(new Result(true, Id.ToInt32()), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger(ex, "/ToDo/DutyWork/Delete");
                return Json(new Result(false, "Hata oldu"), JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// iş listesi
        /// </summary>
        public string GorevToDoList()
        {
            var id = Url.RequestContext.RouteData.Values["id"];
            var ID = id.ToInt32();
            List<GorevToDoList> grvToDO = db.GorevToDoLists.Where(m => m.GorevID == ID).ToList();
            List<frmGorevTodos> aa = new List<frmGorevTodos>();
            foreach (GorevToDoList item in grvToDO)
            {
                frmGorevTodos a = new frmGorevTodos
                {
                    ID = item.ID,
                    AktifPasif = item.AktifPasif,
                    Aciklama = item.Aciklama,
                    OnayDurum = item.OnayDurum
                };

                aa.Add(a);

            }
            var json = new JavaScriptSerializer().Serialize(aa);
            return json;

        }
    }
}