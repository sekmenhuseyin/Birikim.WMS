using Humanizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
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
        /// kullanıcı listesi
        /// </summary>
        public PartialViewResult List()
        {
            if (CheckPerm(Perms.Kullanıcılar, PermTypes.Reading) == false) return null;
            List<User> list;
            if (vUser.Id == 1)
                list = db.Users.Where(m => m.Sirket == "" && m.Tip == 0).ToList();
            else
                list = db.Users.Where(m => m.Sirket == "" && m.Tip == 0 && m.ID > 1).ToList();
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
        /// <summary>
        /// b2b kullanıcı sayfası
        /// </summary>
        public ActionResult B2B()
        {
            if (CheckPerm(Perms.Kullanıcılar, PermTypes.Reading) == false) return Redirect("/");
            ViewBag.Yetki = CheckPerm(Perms.Kullanıcılar, PermTypes.Writing);
            return View("B2B");
        }
        /// <summary>
        /// b2b kullanıcı listesi
        /// </summary>
        public PartialViewResult B2BList()
        {
            if (CheckPerm(Perms.Kullanıcılar, PermTypes.Reading) == false) return null;
            string sql = string.Format("SELECT solar6.dbo.B2B_User.ID, solar6.dbo.B2B_User.HesapKodu, FINSAT6{0}.FINSAT6{0}.CHK.Unvan1 as Unvan, solar6.dbo.B2B_User.YetkiliEMail, solar6.dbo.B2B_User.Parola " +
                                "FROM solar6.dbo.B2B_User WITH(NOLOCK) INNER JOIN FINSAT6{0}.FINSAT6{0}.CHK WITH(NOLOCK) ON solar6.dbo.B2B_User.HesapKodu = FINSAT6{0}.FINSAT6{0}.CHK.HesapKodu " +
                                "ORDER BY Unvan1", db.GetSirketDBs().FirstOrDefault());
            List<mdlB2BUsers> list;
            try
            {
                list = db.Database.SqlQuery<mdlB2BUsers>(sql).ToList();
            }
            catch (Exception ex)
            {
                Logger(ex, "Users/B2BList");
                list = new List<mdlB2BUsers>();
            }
            ViewBag.Yetki = CheckPerm(Perms.Kullanıcılar, PermTypes.Writing);
            ViewBag.Yetki2 = CheckPerm(Perms.Kullanıcılar, PermTypes.Deleting);
            return PartialView("B2BList", list);
        }
        /// <summary>
        /// şifreyi değiştir
        /// </summary>
        [HttpPost]
        public JsonResult B2BChangePass(int ID, string Pass)
        {
            if (CheckPerm(Perms.Kullanıcılar, PermTypes.Writing) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            try
            {
                db.Database.ExecuteSqlCommand("UPDATE solar6.dbo.B2B_User SET Parola = '" + Pass + "' WHERE ID = " + ID);
                LogActions("", "Users", "B2BChangePass", ComboItems.alDüzenle, ID);
                return Json(new Result(true, ID), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger(ex, "Users/B2BChangePass");
                return Json(new Result(false, "Hata oldu"), JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// sil
        /// </summary>
        [HttpPost]
        public JsonResult B2BDelete(int ID)
        {
            if (CheckPerm(Perms.Kullanıcılar, PermTypes.Deleting) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            try
            {
                db.Database.ExecuteSqlCommand("DELETE FROM solar6.dbo.B2B_User WHERE ID = " + ID);
                LogActions("", "Users", "B2BDelete", ComboItems.alSil, ID);
                return Json(new Result(true, ID), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger(ex, "Users/B2BDelete");
                return Json(new Result(false, "Hata oldu"), JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// yeni form
        /// </summary>
        public PartialViewResult B2BNew()
        {
            if (CheckPerm(Perms.Kullanıcılar, PermTypes.Reading) == false) return null;
            return PartialView("B2BNew", new mdlB2BUsers());
        }
        /// <summary>
        /// kaydet
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public JsonResult B2BSave(mdlB2BUsers tbl)
        {
            if (CheckPerm(Perms.Kullanıcılar, PermTypes.Writing) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            try
            {
                db.Database.ExecuteSqlCommand(string.Format("INSERT INTO solar6.dbo.B2B_User (HesapKodu, YetkiliEMail, Parola) VALUES ('{0}', '{1}', '{2}')", tbl.HesapKodu, tbl.YetkiliEMail, tbl.Parola));
                LogActions("", "Users", "B2BSave", ComboItems.alEkle, 1);
                return Json(new Result(true, 1), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger(ex, "Users/B2BSave");
                return Json(new Result(false, "Hata oldu"), JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// malzeme autocomplete
        /// </summary>
        public JsonResult GetChKCode(string term)
        {
            var id = Url.RequestContext.RouteData.Values["id"];
            if (id == null) return null;
            string sql = "";
            //generate sql
            if (id.ToString() == "0")
                id = db.GetSirketDBs().FirstOrDefault();
            sql = String.Format("FINSAT6{0}.[wms].[CHKSearch] @HesapKodu = N'{1}', @Unvan = N'', @top = 20", id.ToString(), term);
            //return
            try
            {
                var list = db.Database.SqlQuery<frmJson>(sql).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger(ex, "Users/GetChKCode");
                return Json(new List<frmJson>(), JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetChKUnvan(string term)
        {
            var id = Url.RequestContext.RouteData.Values["id"];
            if (id == null) return null;
            string sql = "";
            //generate sql
            if (id.ToString() == "0")
                id = db.GetSirketDBs().FirstOrDefault();
            sql = String.Format("FINSAT6{0}.[wms].[CHKSearch] @HesapKodu = N'', @Unvan = N'{1}', @top = 20", id.ToString(), term);
            //return
            try
            {
                var list = db.Database.SqlQuery<frmJson>(sql).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger(ex, "Users/GetChKCode");
                return Json(new List<frmJson>(), JsonRequestBehavior.AllowGet);
            }
        }

        public PartialViewResult YetkiDuzenle(int ID)
        {
            var tbl = db.UserDetails.Where(m => m.UserID == ID).FirstOrDefault();
            SipOnayYetkiler yetki = new SipOnayYetkiler();
            if (tbl != null)
            {

                yetki.GostCHKKodAlani = tbl.GostCHKKodAlani;
                yetki.GosterilecekSirket = tbl.GosterilecekSirket;
                yetki.GostKod3OrtBakiye = tbl.GostKod3OrtBakiye;
                yetki.GostRiskDeger = tbl.GostRiskDeger;
                yetki.GostSTKDeger = tbl.GostSTKDeger;
                yetki.AdSoyad = tbl.User.AdSoyad;

            }
            else
            {
                yetki.GostCHKKodAlani = "";
                yetki.GosterilecekSirket = "";
                yetki.GostKod3OrtBakiye = "";
                yetki.GostRiskDeger = "";
                yetki.GostSTKDeger = "";
                yetki.AdSoyad = vUser.FirstName;
            }
            ViewBag.ID = ID;
            return PartialView("YetkiDuzenle", yetki);
        }

        public string TipKodSelect()
        {
            var KOD = db.Database.SqlQuery<RaporGetKod>(string.Format("[FINSAT6{0}].[wms].[DB_GetTipKod]", "71")).ToList();
            var json = new JavaScriptSerializer().Serialize(KOD);
            return json;
        }

        public JsonResult ParametreUpdate(string CHKAraligi, string Sirketler, string Tipler, string Kod3, string Risk, int ID)
        {
            Result _Result = new Result(true);
            var tbl = db.UserDetails.Where(m => m.UserID == ID).FirstOrDefault();
            if (tbl != null)
                db.Database.ExecuteSqlCommand(string.Format("[BIRIKIM].[wms].[TumpaSiparisParametreOnayla] @CHKAraligi = '{0}',@Sirketler = '{1}', @Tipler='{2}',@Kod3 = '{3}', @Risk='{4}', @UserID={5}", CHKAraligi, Sirketler, Tipler, Kod3, Risk, ID));
            else
            {
                UserDetail udt = new UserDetail();
                udt.UserID = ID;
                udt.GostCHKKodAlani = CHKAraligi;
                udt.GosterilecekSirket = Sirketler;
                udt.GostSTKDeger = Tipler;
                udt.GostKod3OrtBakiye = Kod3;
                udt.GostRiskDeger = Risk;

                db.UserDetails.Add(udt);
            }
            try
            {
                db.SaveChanges();
                _Result.Status = true;
                _Result.Message = "İşlem Başarılı ";

            }
            catch (Exception ex)
            {

                _Result.Status = false;
                _Result.Message = "Hata Oluştu. ";

            }
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
    }
}
