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
            if (CheckPerm(Perms.TodoÇalışma, PermTypes.Reading) == false) return Redirect("/");
            ViewBag.GorevID = new SelectList(db.Gorevlers.Where(a => a.Sorumlu == vUser.UserName || a.Sorumlu2 == vUser.UserName || a.Sorumlu3 == vUser.UserName || a.KontrolSorumlusu == vUser.UserName).ToList(), "ID", "Gorev");
            return View();
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
            GorevlerCalisma grv = new GorevlerCalisma
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
            ViewBag.GorevID = new SelectList(db.Gorevlers.Where(m => m.Sorumlu == vUser.UserName || m.Sorumlu2 == vUser.UserName || m.Sorumlu3 == vUser.UserName || m.KontrolSorumlusu == vUser.UserName).ToList(), "ID", "Gorev");
            return PartialView(new GorevlerCalisma());
        }
        /// <summary>
        /// liste
        /// </summary>
        public PartialViewResult List()
        {
            var gorevCalismas = db.GorevlerCalismas.Where(m => m.ID > 0);
            if (CheckPerm(Perms.TodoÇalışma, PermTypes.Deleting) == false)
                gorevCalismas = gorevCalismas.Where(a => a.Kaydeden == vUser.UserName);
            ViewBag.Yetki2 = CheckPerm(Perms.TodoÇalışma, PermTypes.Deleting);
            return PartialView("List", gorevCalismas.OrderByDescending(m => m.Tarih).ToList());
        }
        /// <summary>
        /// düzenle
        /// </summary>
        public PartialViewResult Edit(int Id)
        {
            //TODO:
            var gorevCalisma = db.GorevlerCalismas.Find(Id);
            ViewBag.GorevID = new SelectList(db.Gorevlers.Where(a => a.Sorumlu == vUser.UserName || a.Sorumlu2 == vUser.UserName || a.Sorumlu3 == vUser.UserName).ToList(), "ID", "Gorev", gorevCalisma.GorevID);
            return PartialView(gorevCalisma);
        }
        /// <summary>
        /// kaydet
        /// </summary>
        /// <param name="gorevCalisma"></param>
        /// <returns></returns>
        [HttpPost, ValidateAntiForgeryToken]
        public JsonResult Save(GorevlerCalisma gorevCalisma, string[] work, string[] checkitem, string[] todo)
        {
            if (CheckPerm(Perms.TodoÇalışma, PermTypes.Writing) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            if (ModelState.IsValid)
            {
                //yeni çalışma girerken
                if (gorevCalisma.ID == 0)
                {
                    var kontrol = db.GorevlerCalismas.Where(m => m.GorevID == gorevCalisma.GorevID && m.Tarih == gorevCalisma.Tarih && m.Kaydeden == vUser.UserName).FirstOrDefault();
                    if (kontrol != null)
                        return Json(new Result(false, "Bu gün için daha önce çalışma kaydetmişsiniz"), JsonRequestBehavior.AllowGet);
                    var grv = db.Gorevlers.Where(a => a.ID == gorevCalisma.GorevID).FirstOrDefault();
                    //todo list için bir döngü yap
                    for (int i = 0; i < work.Length; i++)
                    {
                        if (checkitem[i] == "true")
                        {
                            var idx = todo[i].ToInt32();
                            var tbl = new GorevlerCalismaToDoList()
                            {
                                GorevlerCalisma = gorevCalisma,
                                Aciklama = work[i]
                            };
                            db.GorevlerCalismaToDoLists.Add(tbl);
                            var c = grv.GorevlerToDoLists.Where(m => m.ID == idx).FirstOrDefault();
                            if (vUser.RoleName == "Destek") c.KontrolOnay = true;
                            else c.Onay = true;
                        }
                    }
                    //eğer 
                    if (vUser.RoleName == "Developer")
                    {
                        var c = grv.GorevlerToDoLists.Where(m => m.Onay == false).FirstOrDefault();
                        if (c == null)
                            grv.GorevTipiID = ComboItems.gytKaliteKontrol.ToInt32();
                    }
                    gorevCalisma.Kaydeden = vUser.UserName;
                    gorevCalisma.KayitTarih = DateTime.Now;
                    gorevCalisma.DegisTarih = gorevCalisma.KayitTarih;
                    gorevCalisma.Degistiren = vUser.UserName;
                    db.GorevlerCalismas.Add(gorevCalisma);
                }
                //çalışma güncelleme
                else
                {
                    var tbl = db.GorevlerCalismas.Where(m => m.ID == gorevCalisma.ID).FirstOrDefault();
                    tbl.Tarih = gorevCalisma.Tarih;
                    tbl.Sure = gorevCalisma.Sure;
                    tbl.Calisma = gorevCalisma.Calisma;
                    tbl.DegisTarih = DateTime.Now;
                    tbl.Degistiren = vUser.UserName;
                    for (int i = 0; i < work.Length; i++)
                    {
                        if (Convert.ToBoolean(checkitem[i]) == true)
                        {
                        }
                        var id2 = Convert.ToInt32(todo[i]);
                        var grv = db.GorevlerToDoLists.Where(m => m.ID == id2).FirstOrDefault();
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
            return Json(new Result(false, "Hata oldu"), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// sil
        /// </summary>
        public JsonResult Delete(string Id)
        {
            if (CheckPerm(Perms.TodoÇalışma, PermTypes.Deleting) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            GorevlerCalisma gorevcalisma = db.GorevlerCalismas.Find(Id.ToInt32());
            db.GorevlerCalismas.Remove(gorevcalisma);
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
            var grvToDO = db.GorevlerToDoLists.Where(m => m.GorevID == ID);
            var list = new List<frmGorevTodos>();
            foreach (GorevlerToDoList item in grvToDO.ToList())
            {
                list.Add(new frmGorevTodos { ID = item.ID, Aciklama = item.Aciklama });
            }
            var json = new JavaScriptSerializer().Serialize(list);
            return json;

        }
        /// <summary>
        /// todo lists page
        /// </summary>
        public ActionResult ToDos()
        {
            if (CheckPerm(Perms.TodoÇalışma, PermTypes.Reading) == false) return Redirect("/");
            return View();
        }
        /// <summary>
        /// liste
        /// </summary>
        public PartialViewResult ToDosList(bool Tip)
        {
            var list = db.GorevlerToDoLists.Where(m => m.ID > 0);
            if (Tip == false)
            {
                if (vUser.RoleName == "Admin" || vUser.RoleName == " ")
                {
                    list = list.Where(m => m.Onay == true && m.KontrolOnay == true && m.AdminOnay == false);
                }
                else if (vUser.RoleName == "Destek")
                {
                    list = list.Where(m => ((m.Gorevler.Sorumlu == vUser.UserName || m.Gorevler.Sorumlu2 == vUser.UserName || m.Gorevler.Sorumlu3 == vUser.UserName) && m.Onay == false) || (m.Onay == true && m.KontrolOnay == false && (m.Gorevler.KontrolSorumlusu == vUser.UserName || m.Gorevler.KontrolSorumlusu == null)));
                }
                else
                {
                    list = list.Where(m => (m.Gorevler.Sorumlu == vUser.UserName || m.Gorevler.Sorumlu2 == vUser.UserName || m.Gorevler.Sorumlu3 == vUser.UserName) && m.Onay == false);
                }
            }
            else
            {
                if (vUser.RoleName == "Destek")
                    list = list.Where(m => m.Kaydeden == vUser.UserName || m.Degistiren == vUser.UserName || m.Gorevler.Sorumlu == vUser.UserName || m.Gorevler.Sorumlu2 == vUser.UserName || m.Gorevler.Sorumlu3 == vUser.UserName || m.Gorevler.KontrolSorumlusu == vUser.UserName || m.Gorevler.KontrolSorumlusu == null);
                else if (vUser.RoleName != "Admin" && vUser.RoleName != " ")
                {
                    list = list.Where(m => m.Kaydeden == vUser.UserName || m.Degistiren == vUser.UserName || m.Gorevler.Sorumlu == vUser.UserName || m.Gorevler.Sorumlu2 == vUser.UserName || m.Gorevler.Sorumlu3 == vUser.UserName);
                }
            }
            ViewBag.Yetki = CheckPerm(Perms.TodoGörevler, PermTypes.Writing);
            ViewBag.Tip = Tip;
            return PartialView("ToDosList", list.OrderBy(m => m.Gorevler.OncelikID).ThenBy(m => m.ID).ToList());
        }
        /// <summary>
        /// ToDos onay / ret
        /// </summary>
        public JsonResult ToDosOnay(int Id, bool Onay)
        {
            try
            {
                string sql = "UPDATE [BIRIKIM].[ong].[GorevlerToDoList] SET ";
                if (vUser.RoleName == "Developer" && Onay == true)
                {
                    sql += "Onay = 1";
                }
                else if (vUser.RoleName == "Destek")
                    if (Onay == true)
                        sql += "KontrolOnay = 1";
                    else
                        sql += "Onay = 0";
                else if (Onay == true)
                    sql += "AdminOnay = 1";
                else
                    sql += "Onay = 0 AND KontrolOnay = 0";

                sql += " WHERE  ID = {0}";
                db.Database.ExecuteSqlCommand(sql, Id);
                if (vUser.RoleName == "Developer" && Onay == true) db.SaveChanges();
                return Json(new Result(true, Id), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger(ex, "ToDo/Duties/ToDosOnay");
                return Json(new Result(false, "Hata oldu"), JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// ayrıntı
        /// </summary>
        public PartialViewResult ToDosDetails(int ID)
        {
            return PartialView("ToDosDetails", db.Gorevlers.Where(m => m.ID == ID).FirstOrDefault());
        }
    }
}