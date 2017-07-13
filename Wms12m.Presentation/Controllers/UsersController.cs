using Humanizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m.Presentation.Controllers
{
    public class UsersController : RootController
    {
        /// <summary>
        /// kullanıcı sayfası
        /// </summary>
        public ActionResult Index()
        {
            if (CheckPerm(Perms.Kullanıcılar, PermTypes.Reading) == false) return Redirect("/");
            ViewBag.Yetki = CheckPerm(Perms.Kullanıcılar, PermTypes.Writing);
            return View("Index");
        }
        /// <summary>
        /// kullanıcılar
        /// </summary>
        public PartialViewResult List()
        {
            if (CheckPerm(Perms.Kullanıcılar, PermTypes.Reading) == false) return null;
            List<User> list;
            if (vUser.Id == 1)
                list = db.Users.ToList();
            else
                list = db.Users.Where(m => m.ID > 1).ToList();
            ViewBag.Yetki = CheckPerm(Perms.Kullanıcılar, PermTypes.Writing);
            return PartialView("List", list);
        }
        /// <summary>
        /// ayrıntılar
        /// </summary>
        public PartialViewResult Details(int id)
        {
            if (CheckPerm(Perms.Kullanıcılar, PermTypes.Reading) == false) return null;
            var tbl = Persons.Detail(id);
            return PartialView("Details", tbl);
        }
        /// <summary>
        /// yeni form
        /// </summary>
        public PartialViewResult New()
        {
            if (CheckPerm(Perms.Kullanıcılar, PermTypes.Reading) == false) return null;
            ViewBag.Sirket = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            ViewBag.RoleName = new SelectList(Roles.GetList(), "RoleName", "RoleName");
            return PartialView("New", new User());
        }
        /// <summary>
        /// düzenle form
        /// </summary>
        public PartialViewResult Edit(int id)
        {
            if (CheckPerm(Perms.Kullanıcılar, PermTypes.Reading) == false) return null;
            var tbl = Persons.Detail(id);
            ViewBag.Sirket = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad", tbl.Sirket);
            ViewBag.RoleName = new SelectList(Roles.GetList(), "RoleName", "RoleName", tbl.RoleName);
            return PartialView("Edit", tbl);
        }
        /// <summary>
        /// şifre değiştirme ekranı
        /// </summary>
        public PartialViewResult Pass()
        {
            if (CheckPerm(Perms.Kullanıcılar, PermTypes.Reading) == false) return null;
            var id = Url.RequestContext.RouteData.Values["id"];
            if (id == null || id.ToString2() == "") return null;
            ViewBag.ID = id;
            ViewBag.UserName = Persons.Detail(id.ToInt32()).Kod;
            return PartialView("Pass", new frmUserChangePass());
        }
        /// <summary>
        /// kişiye yetki atama sayfası
        /// </summary>
        public PartialViewResult Perm()
        {
            //kontrol
            if (CheckPerm(Perms.Kullanıcılar, PermTypes.Reading) == false) return null;
            var id = Url.RequestContext.RouteData.Values["id"];
            if (id == null || id.ToString2() == "") return null;
            //return
            var ID = id.ToInt32();
            var list = db.GetUserPermsFor(ID).ToList();
            ViewBag.PermName = new SelectList(db.Perms.ToList(), "PermName", "PermName");
            ViewBag.UserName = Persons.Detail(ID).Kod;
            return PartialView("Perm", list);
        }
        /// <summary>
        /// yetkileri kaydet
        /// </summary>
        [HttpPost]
        public void SavePerm(frmRolePerms rolePerm)
        {
            if (ModelState.IsValid && rolePerm.PermName != "")
            {
                if (CheckPerm(Perms.Kullanıcılar, PermTypes.Writing) == true)
                {
                    rolePerm.PermName = rolePerm.PermName.Dehumanize();
                    var tbl = db.UserPerms.Where(m => m.PermName == rolePerm.PermName && m.UserName == rolePerm.UserName).FirstOrDefault();
                    if (tbl == null)//ilk defa kayıt olacak
                    {
                        tbl = new UserPerm()
                        {
                            PermName = rolePerm.PermName,
                            UserName = rolePerm.UserName,
                            Reading = rolePerm.Reading == "on" ? true : false,
                            Writing = rolePerm.Writing == "on" ? true : false,
                            Updating = rolePerm.Updating == "on" ? true : false,
                            Deleting = rolePerm.Deleting == "on" ? true : false,
                            RecordDate = DateTime.Now,
                            RecordUser = vUser.UserName,
                            ModifiedDate = DateTime.Now,
                            ModifiedUser = vUser.UserName
                        };
                        if (tbl.Reading == true || tbl.Writing == true || tbl.Updating == true || tbl.Deleting == true)
                        {
                            db.UserPerms.Add(tbl);
                            LogActions("", "Users", "SavePerm", ComboItems.alEkle, 0, "PermName: " + tbl.PermName + ", UserName: " + tbl.UserName + ", R: " + tbl.Reading + ", W: " + tbl.Writing + ", U: " + tbl.Updating + ", D: " + tbl.Deleting);
                        }
                    }
                    else
                    {
                        if (rolePerm.Reading != "on" && rolePerm.Writing != "on" && rolePerm.Updating != "on" && rolePerm.Deleting != "on")
                        {
                            db.UserPerms.Remove(tbl);
                            LogActions("", "Users", "SavePerm", ComboItems.alSil, 0, "PermName: " + tbl.PermName + ", UserName: " + tbl.UserName);
                        }
                        else
                        {
                            tbl.Reading = rolePerm.Reading == "on" ? true : false;
                            tbl.Writing = rolePerm.Writing == "on" ? true : false;
                            tbl.Updating = rolePerm.Updating == "on" ? true : false;
                            tbl.Deleting = rolePerm.Deleting == "on" ? true : false;
                            tbl.ModifiedDate = DateTime.Now;
                            tbl.ModifiedUser = vUser.UserName;
                            LogActions("", "Users", "SavePerm", ComboItems.alDüzenle, 0, "PermName: " + tbl.PermName + ", UserName: " + tbl.UserName + ", R: " + tbl.Reading + ", W: " + tbl.Writing + ", U: " + tbl.Updating + ", D: " + tbl.Deleting);
                        }
                    }
                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        Logger(ex, "Users/SavePerm");
                    }
                }
            }
        }
        /// <summary>
        /// yetkileri kaydet
        /// </summary>
        [HttpPost]
        public JsonResult DeletePerm(string Id)
        {
            if (CheckPerm(Perms.Kullanıcılar, PermTypes.Deleting) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            string[] ids = Id.ToString().Split('#');
            string pname = ids[0], uname = ids[1];
            try
            {
                var tbl = db.UserPerms.Where(m => m.PermName == pname && m.UserName == uname).FirstOrDefault();
                if (tbl != null)
                {
                    db.UserPerms.Remove(tbl);
                    db.SaveChanges();
                    //log
                    LogActions("", "Users", "DeletePerm", ComboItems.alSil, 0, "PermName: " + pname + ", UserName: " + uname);
                }
            }
            catch (Exception ex)
            {
                Logger(ex, "Users/DeletePerm");
            }
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
        /// kaydet
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public JsonResult Save(User tbl)
        {
            if (CheckPerm(Perms.Kullanıcılar, PermTypes.Writing) == false || tbl.ID == 1) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            Result _Result = Persons.Operation(tbl);
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// şifreyi değiştir
        /// </summary>
        [HttpPost]
        public JsonResult ChangePass(frmUserChangePass tmp)
        {
            if (CheckPerm(Perms.Kullanıcılar, PermTypes.Writing) == false || tmp.ID == 1) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            Result _Result = new Result();
            if (tmp.Password == tmp.Password2)
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
            if (CheckPerm(Perms.Kullanıcılar, PermTypes.Deleting) == false || Id == 1) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            Result _Result = Persons.Delete(Id);
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
    }
}
