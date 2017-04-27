﻿using System;
using System.Linq;
using System.Web.Mvc;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m.Presentation.Controllers
{
    public class UsersController : RootController
    {
        /// <summary>
        /// kullanıcılar
        /// </summary>
        public ActionResult Index()
        {
            if (CheckPerm("Users", PermTypes.Reading) == false) return Redirect("/");
            var list = db.Users.ToList();
            return View("Index", list);
        }
        /// <summary>
        /// ayrıntılar
        /// </summary>
        public PartialViewResult Details(int id)
        {
            if (CheckPerm("Users", PermTypes.Reading) == false) return null;
            var tbl = Persons.Detail(id);
            return PartialView("Details", tbl);
        }
        /// <summary>
        /// yeni form
        /// </summary>
        public PartialViewResult New()
        {
            if (CheckPerm("Users", PermTypes.Reading) == false) return null;
            ViewBag.Sirket = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            ViewBag.RoleName = new SelectList(Roles.GetList(), "RoleName", "RoleName");
            return PartialView("New", new User());
        }
        /// <summary>
        /// düzenle form
        /// </summary>
        public PartialViewResult Edit(int id)
        {
            if (CheckPerm("Users", PermTypes.Reading) == false) return null;
            var tbl = Persons.Detail(id);
            ViewBag.Sirket = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad", tbl.Sirket);
            ViewBag.RoleName = new SelectList(Roles.GetList(), "RoleName", "RoleName", tbl.RoleName);
            return PartialView("Edit", tbl);
        }
        /// <summary>
        /// depo yetkilerini ayarlar
        /// </summary>
        public PartialViewResult Depo()
        {
            //kontrol
            if (CheckPerm("Users", PermTypes.Reading) == false) return null;
            var id = Url.RequestContext.RouteData.Values["id"];
            if (id == null || id.ToString2() == "") return null;
            //return
            var tbl = db.YetkiDepo(id.ToInt32()).ToList();
            ViewBag.DepoID = new SelectList(Store.GetList(), "ID", "DepoAd");
            ViewBag.ID = id;
            return PartialView("Depo", tbl);
        }
        /// <summary>
        /// şifre değiştirme ekranı
        /// </summary>
        public PartialViewResult Pass()
        {
            if (CheckPerm("Users", PermTypes.Reading) == false) return null;
            var id = Url.RequestContext.RouteData.Values["id"];
            if (id == null || id.ToString2() == "") return null;
            ViewBag.ID = id;
            return PartialView("Pass", new frmUserChangePass());
        }
        /// <summary>
        /// kişiye yetki atama sayfası
        /// </summary>
        public PartialViewResult Perm()
        {
            //kontrol
            if (CheckPerm("Users", PermTypes.Reading) == false) return null;
            var id = Url.RequestContext.RouteData.Values["id"];
            if (id == null || id.ToString2() == "") return null;
            //return
            int ID = id.ToInt32();
            var username = db.Users.Where(m => m.ID == ID).Select(m => m.Kod).FirstOrDefault();
            var list = db.UserPerms.Where(m => m.UserName == username).ToList();
            ViewBag.PermName = new SelectList(db.Perms.ToList(), "PermName", "PermName");
            ViewBag.ID = ID;
            ViewBag.username = username;
            return PartialView("Perm", list);
        }
        /// <summary>
        /// yetkileri kaydet
        /// </summary>
        [HttpPost]
        public PartialViewResult SavePerm(UserPerm tbl)
        {
            if (CheckPerm("Users", PermTypes.Writing) == false) return null;
            if (ModelState.IsValid)
            {
                try
                {
                    tbl.ModifiedDate = DateTime.Now;
                    tbl.ModifiedUser = vUser.UserName;
                    tbl.RecordDate = DateTime.Now;
                    tbl.RecordUser = vUser.UserName;
                    db.UserPerms.Add(tbl);
                    db.SaveChanges();
                }catch (System.Exception){}
            }
            //return
            var list = db.UserPerms.Where(m => m.UserName == tbl.UserName).ToList();
            ViewBag.PermName = new SelectList(db.Perms.ToList(), "PermName", "PermName");
            ViewBag.ID = db.Users.Where(m => m.Kod == tbl.UserName).Select(m => m.ID).FirstOrDefault();
            ViewBag.username = tbl.UserName;
            return PartialView("Perm", list);
        }
        /// <summary>
        /// yetkileri kaydet
        /// </summary>
        [HttpPost]
        public JsonResult DeletePerm(string Id)
        {
            if (CheckPerm("Users", PermTypes.Deleting) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            string[] ids = Id.ToString().Split('#');
            string pname = ids[0], uname = ids[1];
            try
            {
                var tbl = db.UserPerms.Where(m => m.PermName == pname && m.UserName == uname).FirstOrDefault();
                if (tbl != null)
                {
                    db.UserPerms.Remove(tbl);
                    db.SaveChanges();
                }
            }
            catch (System.Exception) { }
            //return
            Result _Result = new Result()
            {
                Id = 1,
                Message = "İşlem Başarılı !!!",
                Status = true
            };
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// depo yetkisini kaydet
        /// </summary>
        [HttpPost]
        public PartialViewResult DepoSet(int UserID, int DepoID)
        {
            if (CheckPerm("Users", PermTypes.Writing) == false) return null;
            try
            {
                db.YetkiDepoSet(DepoID, UserID, true);
            } catch (System.Exception) { }
            var tbl = db.YetkiDepo(UserID).ToList();
            ViewBag.DepoID = new SelectList(Store.GetList(), "ID", "DepoAd");
            ViewBag.ID = UserID;
            return PartialView("Depo", tbl);
        }
        /// <summary>
        /// depo yetkisi sil
        /// </summary>
        public JsonResult DepoDelete(string Id)
        {
            if (CheckPerm("Users", PermTypes.Deleting) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            string[] ids = Id.ToString().Split('-');
            db.YetkiDepoSet(ids[1].ToInt32(), ids[0].ToInt32(), false);
            Result _Result = new Result()
            {
                Id = ids[1].ToInt32(),
                Message = "İşlem Başarılı !!!",
                Status = true
            };
            return Json(_Result, JsonRequestBehavior.AllowGet);

        }
        /// <summary>
        /// kaydet
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Save(User tbl)
        {
            if (CheckPerm("Users", PermTypes.Writing) == false) return Redirect("/");
            Result _Result = Persons.Operation(tbl);
            return RedirectToAction("Index");
        }
        /// <summary>
        /// şifreyi değiştir
        /// </summary>
        [HttpPost]
        public JsonResult ChangePass(frmUserChangePass tmp)
        {
            if (CheckPerm("Users", PermTypes.Writing) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            Result _Result = new Result();
            if (tmp.Password==tmp.Password2)
            {
                User tbl = new User()
                {
                    ID = tmp.ID,
                    Sifre = tmp.Password
                };
                _Result = Persons.ChangePass(tbl);
            }
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// sil
        /// </summary>
        [HttpPost]
        public JsonResult Delete(int Id)
        {
            if (CheckPerm("Users", PermTypes.Deleting) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            Result _Result = Persons.Delete(Id);
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
    }
}
