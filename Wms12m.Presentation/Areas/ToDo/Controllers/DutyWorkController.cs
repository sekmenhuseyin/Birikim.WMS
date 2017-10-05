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
            ViewBag.GorevID = new SelectList(db.Gorevlers.Where(m => m.Sorumlu == vUser.UserName || m.Sorumlu2 == vUser.UserName || m.Sorumlu3 == vUser.UserName || m.KontrolSorumlusu == vUser.UserName).ToList(), "ID", "Gorev");
            return View();
        }
        /// <summary>
        /// yeni çalışma
        /// </summary>
        public PartialViewResult New()
        {
            var id = Url.RequestContext.RouteData.Values["id"];
            var ID = id.ToInt32();
            var TodoItem = db.GorevlerToDoLists.Find(ID);
            ViewBag.GorevID = new SelectList(db.Gorevlers.Where(m => m.ID == TodoItem.GorevID).ToList(), "ID", "Gorev", TodoItem.GorevID);
            var tbl = new GorevlerCalisma
            {
                Calisma = TodoItem.Aciklama,
                GorevID = TodoItem.GorevID
            };
            return PartialView("New", tbl);
        }
        /// <summary>
        /// yeni çalışma
        /// </summary>
        public PartialViewResult NewAll()
        {
            ViewBag.RoleName = vUser.RoleName;
            var durum = ComboItems.gydAtandı.ToInt32();
            var list = db.Gorevlers.Where(m => m.DurumID == durum);
            if (vUser.RoleName == "Destek")
                list = list.Where(m => m.Sorumlu == vUser.UserName || m.Sorumlu2 == vUser.UserName || m.Sorumlu3 == vUser.UserName || m.KontrolSorumlusu == vUser.UserName || m.KontrolSorumlusu == null);
            else
                list = list.Where(m => m.Sorumlu == vUser.UserName || m.Sorumlu2 == vUser.UserName || m.Sorumlu3 == vUser.UserName);
            ViewBag.GorevID = new SelectList(list.ToList(), "ID", "Gorev");
            return PartialView(new GorevlerCalisma());
        }
        /// <summary>
        /// liste
        /// </summary>
        public PartialViewResult List(bool Tip)
        {
            var gorevCalismas = db.GorevlerCalismas.Where(m => m.ID > 0);
            var yetki = CheckPerm(Perms.TodoÇalışma, PermTypes.Deleting);
            if (yetki == true)//admin ise filtre yapma hepsini gör
            {

            }
            else if (Tip == false)//bana ait
            {
                gorevCalismas = gorevCalismas.Where(m => m.Kaydeden == vUser.UserName);
            }
            else
            {
                gorevCalismas = gorevCalismas.Where(m => m.Kaydeden == vUser.UserName || m.Gorevler.Sorumlu == vUser.UserName || m.Gorevler.Sorumlu2 == vUser.UserName || m.Gorevler.Sorumlu3 == vUser.UserName || m.Gorevler.KontrolSorumlusu == vUser.UserName || m.Gorevler.Kaydeden == vUser.UserName);
            }
            ViewBag.Yetki2 = yetki;
            return PartialView("List", gorevCalismas.OrderByDescending(m => m.Tarih).ToList());
        }
        /// <summary>
        /// düzenle
        /// </summary>
        public PartialViewResult Edit(int Id)
        {
            //TODO:
            var gorevCalisma = db.GorevlerCalismas.Find(Id);
            ViewBag.GorevID = new SelectList(db.Gorevlers.Where(m => m.ID == gorevCalisma.GorevID).ToList(), "ID", "Gorev", gorevCalisma.GorevID);
            return PartialView(gorevCalisma);
        }
        /// <summary>
        /// kaydet
        /// </summary>
        /// <param name="gorevCalisma"></param>
        /// <returns></returns>
        [HttpPost, ValidateAntiForgeryToken]
        public JsonResult Save(GorevlerCalisma gorevCalisma)
        {
            if (CheckPerm(Perms.TodoÇalışma, PermTypes.Writing) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            if (!ModelState.IsValid)
                return Json(new Result(false, "Hata oldu. Sayfayı yenileyin"), JsonRequestBehavior.AllowGet);
            if (gorevCalisma.Sure < 0 || gorevCalisma.Sure > 540)
                return Json(new Result(false, "Çalışma süresini doğru yazınız"), JsonRequestBehavior.AllowGet);
            //yeni çalışma girerken
            if (gorevCalisma.ID == 0)
            {
                //kontrol
                string sql = string.Format("SELECT ISNULL(SUM(Sure), 0) AS Expr1 FROM ong.GorevlerCalisma WHERE (GorevID = {0}) AND (Tarih = '{1}') AND (Kaydeden = '{2}')", gorevCalisma.GorevID, DateTime.Now.ToString("yyyy-MM-dd"), vUser.UserName);
                var kontrol = db.Database.SqlQuery<int>(sql).FirstOrDefault();
                if ((kontrol + gorevCalisma.Sure) > 540)
                    return Json(new Result(false, "Seçili görev ve tarih için 540 dakikadan daha fazla çalışma girilemez."), JsonRequestBehavior.AllowGet);
                //update
                var grv = db.Gorevlers.Where(m => m.ID == gorevCalisma.GorevID).FirstOrDefault();
                gorevCalisma.Kaydeden = vUser.UserName;
                gorevCalisma.KayitTarih = DateTime.Now;
                gorevCalisma.Degistiren = vUser.UserName;
                gorevCalisma.DegisTarih = gorevCalisma.KayitTarih;
                //eğer 
                if (vUser.RoleName == "Developer")
                {
                    var c = grv.GorevlerToDoLists.Where(m => m.Onay == false).FirstOrDefault();
                    if (c == null)
                        grv.GorevTipiID = ComboItems.gydKaliteKontrol.ToInt32();
                }
                else if (vUser.RoleName == "Destek")
                {
                    var c = grv.GorevlerToDoLists.Where(m => m.KontrolOnay == false).FirstOrDefault();
                    if (c == null)
                        grv.GorevTipiID = ComboItems.gydBeklemede.ToInt32();
                }
                db.GorevlerCalismas.Add(gorevCalisma);
            }
            //çalışma güncelleme
            else
            {
                //kontrol
                string sql = string.Format("SELECT ISNULL(SUM(Sure), 0) AS Expr1 FROM ong.GorevlerCalisma WHERE (GorevID = {0}) AND (Tarih = '{1}') AND (Kaydeden = '{2}') AND (ID <> {3})", gorevCalisma.GorevID, DateTime.Now.ToString("yyyy-MM-dd"), vUser.UserName, gorevCalisma.ID);
                var kontrol = db.Database.SqlQuery<int>(sql).FirstOrDefault();
                if ((kontrol + gorevCalisma.Sure) > 540)
                    return Json(new Result(false, "Seçili görev ve tarih için 540 dakikadan daha fazla çalışma girilemez."), JsonRequestBehavior.AllowGet);
                //update
                var tbl = db.GorevlerCalismas.Where(m => m.ID == gorevCalisma.ID).FirstOrDefault();
                tbl.Tarih = gorevCalisma.Tarih;
                tbl.Sure = gorevCalisma.Sure;
                tbl.Calisma = gorevCalisma.Calisma.ToString2();
                tbl.DegisTarih = DateTime.Now;
                tbl.Degistiren = vUser.UserName;
            }
            try
            {
                db.SaveChanges();
                return Json(new Result(true, gorevCalisma.ID), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger(ex, "ToDo/DutyWork/Save");
                return Json(new Result(false, "Kayıt hatası"), JsonRequestBehavior.AllowGet);
            }
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
            ViewBag.RoleName = vUser.RoleName;
            return PartialView("ToDosList", list.OrderBy(m => m.Gorevler.OncelikID).ThenBy(m => m.ID).ToList());
        }
        /// <summary>
        /// ToDos onay / ret
        /// </summary>
        public JsonResult ToDosOnay(int Id, bool Onay, int Sure)
        {
            var tbl = db.GorevlerToDoLists.Where(m => m.ID == Id).FirstOrDefault();
            //kontrol
            string sql = string.Format("SELECT ISNULL(SUM(Sure), 0) AS Expr1 FROM ong.GorevlerCalisma WHERE (GorevID = {0}) AND (Tarih = '{1}') AND (Kaydeden = '{2}')", tbl.GorevID, DateTime.Now.ToString("yyyy-MM-dd"), vUser.UserName);
            var kontrol = db.Database.SqlQuery<int>(sql).FirstOrDefault();
            if ((kontrol + Sure) > 540)
                return Json(new Result(false, "Seçili görev ve tarih için 540 dakikadan daha fazla çalışma girilemez."), JsonRequestBehavior.AllowGet);
            //getir
            tbl.DegisTarih = DateTime.Now;
            tbl.Degistiren = vUser.UserName;
            if (Onay == true)
            {
                //onaylamalar
                if (tbl.Onay == false && (tbl.Gorevler.Sorumlu == vUser.UserName || tbl.Gorevler.Sorumlu2 == vUser.UserName || tbl.Gorevler.Sorumlu3 == vUser.UserName))
                {
                    tbl.Onay = true;
                    tbl.Onaylayan = vUser.UserName;
                }
                else if (tbl.Onay == true && vUser.RoleName == "Destek")
                {
                    tbl.KontrolOnay = true;
                    tbl.KontrolEden = vUser.UserName;
                }
                else if (tbl.Onay == true && tbl.KontrolOnay == true && CheckPerm(Perms.TodoÇalışma, PermTypes.Deleting) == true)
                {
                    tbl.AdminOnay = true;
                }
                //çalışma gir
                if (Sure > 0)
                {
                    var calisma = new GorevlerCalisma()
                    {
                        GorevID = tbl.GorevID,
                        Tarih = DateTime.Now,
                        Sure = Sure,
                        Calisma = tbl.Aciklama,
                        KayitTarih = DateTime.Now,
                        Kaydeden = vUser.UserName,
                        Degistiren = vUser.UserName,
                        DegisTarih = DateTime.Now,
                    };
                    db.GorevlerCalismas.Add(calisma);
                }
                //eğer 
                if (vUser.RoleName == "Developer")
                {
                    var c = db.GorevlerToDoLists.Where(m => m.GorevID == tbl.GorevID && m.Onay == false).FirstOrDefault();
                    if (c == null)
                        tbl.Gorevler.GorevTipiID = ComboItems.gydKaliteKontrol.ToInt32();
                }
                else if (vUser.RoleName == "Destek")
                {
                    var c = db.GorevlerToDoLists.Where(m => m.GorevID == tbl.GorevID && m.KontrolOnay == false).FirstOrDefault();
                    if (c == null)
                        tbl.Gorevler.GorevTipiID = ComboItems.gydBeklemede.ToInt32();
                }
            }
            else
            {
                if ((tbl.Onay == true && tbl.KontrolOnay == true && CheckPerm(Perms.TodoÇalışma, PermTypes.Deleting) == true) || (tbl.Onay == true && vUser.RoleName == "Destek"))
                {
                    tbl.AdminOnay = false;
                    tbl.KontrolOnay = false;
                    tbl.KontrolEden = null;
                    tbl.Onay = false;
                    tbl.Onaylayan = null;
                    tbl.Gorevler.GorevTipiID = ComboItems.gydAtandı.ToInt32();
                }
            }
            //kaydet
            try
            {
                db.SaveChanges();
                return Json(new Result(true, Id), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger(ex, "ToDo/DutyWork/ToDosOnay");
                return Json(new Result(false, "Kayıt hatası"), JsonRequestBehavior.AllowGet);
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