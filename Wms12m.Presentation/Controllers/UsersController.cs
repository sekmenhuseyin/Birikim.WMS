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
            ViewBag.Yetki = CheckPerm(Perms.Kullanıcılar, PermTypes.Writing);
            return PartialView("List", Persons.GetList());
        }
        /// <summary>
        /// chat için kullanıcı listesi
        /// </summary>
        public PartialViewResult SharedList()
        {
            var list = db.Database.SqlQuery<frmChatUser>(string.Format(@"
                            SELECT        CONVERT(varchar(50), usr.Users.Guid) as Guid, usr.Users.Kod, usr.Users.AdSoyad, usr.Users.RoleName, ISNULL(Connections.IsOnline, CAST(0 AS Bit)) AS Aktif,
                                                         (SELECT        COUNT(ID) AS Expr1
                                                           FROM            [Messages]
                                                           WHERE        (Kimden = usr.Users.Kod) AND (MesajTipi = 84) AND (Okundu = 0) AND (Kime = '{1}')) AS ID
                            FROM            Connections RIGHT OUTER JOIN
                                                     usr.Users ON Connections.UserName = usr.Users.Kod
                            WHERE        (usr.Users.ID > 1) AND (usr.Users.Aktif = 1) AND (usr.Users.ID <> {0})
                            ORDER BY Aktif DESC, usr.Users.AdSoyad", vUser.Id, vUser.UserName)).ToList();
            return PartialView("../Shared/Users", list);
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
        /// resim değiştirme ekranı
        /// </summary>
        public PartialViewResult Image()
        {
            if (CheckPerm(Perms.Kullanıcılar, PermTypes.Reading) == false) return null;
            var id = Url.RequestContext.RouteData.Values["id"];
            if (id == null || id.ToString2() == "") return null;
            return PartialView("Image", Persons.Detail(id.ToInt32()));
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
            if (ID == 1) return null;
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
        /// şifreyi çözümler ve gösterir
        /// </summary>
        [HttpPost]
        public JsonResult GetPass(int ID)
        {
            if (CheckPerm(Perms.Kullanıcılar, PermTypes.Reading) == false || ID == 1) return Json("Yetkiniz yok", JsonRequestBehavior.AllowGet);
            return Json(Persons.GetPass(ID), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// sil
        /// </summary>
        [HttpPost]
        public JsonResult Delete(int Id)
        {
            if (vUser.Id > 1 || Id == 1) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
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
            sql = String.Format("FINSAT6{0}.[wms].[BTBCHKSearch] @HesapKodu = N'{1}', @Unvan = N'', @top = 20", id.ToString(), term);
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
            sql = String.Format("FINSAT6{0}.[wms].[BTBCHKSearch] @HesapKodu = N'', @Unvan = N'{1}', @top = 20", id.ToString(), term);
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
        /// <summary>
        /// sipriş yetki düzenleme sayfası
        /// </summary>
        public PartialViewResult YetkiDuzenle(int ID)
        {
            if (CheckPerm(Perms.Kullanıcılar, PermTypes.Reading) == false || ID == 1) return null;
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
                User grv = db.Users.Where(m => m.ID == ID).FirstOrDefault();
                yetki.GostCHKKodAlani = "";
                yetki.GosterilecekSirket = "";
                yetki.GostKod3OrtBakiye = "";
                yetki.GostRiskDeger = "";
                yetki.GostSTKDeger = "";
                yetki.AdSoyad = grv.AdSoyad;
            }
            ViewBag.ID = ID;
            return PartialView("YetkiDuzenle", yetki);
        }
        /// <summary>
        /// select box doldurur
        /// </summary>
        public string TipKodSelect()
        {
            var KOD = db.Database.SqlQuery<RaporGetKod>(string.Format("[FINSAT6{0}].[wms].[DB_GetTipKod]", "71")).ToList();
            var json = new JavaScriptSerializer().Serialize(KOD);
            return json;
        }
        /// <summary>
        /// sipriş yetki update
        /// </summary>
        public JsonResult ParametreUpdate(string CHKAraligi, string Sirketler, string Tipler, string Kod3, string Risk, int ID)
        {
            if (CheckPerm(Perms.Kullanıcılar, PermTypes.Writing) == false || ID == 1) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            Result _Result = new Result(true);
            var tbl = db.UserDetails.Where(m => m.UserID == ID).FirstOrDefault();
            if (tbl != null)
                db.Database.ExecuteSqlCommand(string.Format("[BIRIKIM].[wms].[TumpaSiparisParametreOnayla] @CHKAraligi = '{0}',@Sirketler = '{1}', @Tipler='{2}',@Kod3 = '{3}', @Risk='{4}', @UserID={5}", CHKAraligi, Sirketler, Tipler, Kod3, Risk, ID));
            else
            {
                db.UserDetails.Add(new UserDetail
                {
                    UserID = ID,
                    GostCHKKodAlani = CHKAraligi,
                    GosterilecekSirket = Sirketler,
                    GostSTKDeger = Tipler,
                    GostKod3OrtBakiye = Kod3,
                    GostRiskDeger = Risk
                });
            }
            try
            {
                db.SaveChanges();
                _Result.Status = true;
                _Result.Message = "İşlem Başarılı ";

            }
            catch (Exception)
            {
                _Result.Status = false;
                _Result.Message = "Hata Oluştu. ";

            }
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
    }
}
