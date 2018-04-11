using Birikim.Models;
using Humanizer;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
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
            return PartialView("List", Persons.GetListAll());
        }

        public JsonResult List2([DataSourceRequest]DataSourceRequest request)
        {
            return Json(Persons.GetListAll2().ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
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
            ViewBag.RoleName = new SelectList(Roles.GetList(), "RoleName", "RoleName", tbl.RoleName);
            return PartialView("Edit", tbl);
        }

        /// <summary>
        /// kaydet
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public JsonResult Save(User tbl)
        {
            if (CheckPerm(Perms.Kullanıcılar, PermTypes.Writing) == false || tbl.ID == 1) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            var result = Persons.Operation(tbl);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// sil
        /// </summary>
        [HttpPost]
        public JsonResult Delete(int Id)
        {
            if (vUser.Id > 1 || Id == 1) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            var result = Persons.Delete(Id);
            return Json(result, JsonRequestBehavior.AllowGet);
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

        #region Password

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
        /// şifreyi değiştir
        /// </summary>
        [HttpPost]
        public JsonResult ChangePass(frmUserChangePass tmp)
        {
            if (CheckPerm(Perms.Kullanıcılar, PermTypes.Writing) == false || tmp.ID == 1) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            var result = new Result();
            if (tmp.Password == tmp.Password2)
            {
                var tbl = new User()
                {
                    ID = tmp.ID,
                    Sifre = tmp.Password
                };
                result = Persons.ChangePass(tbl);
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// şifreyi çözümler ve gösterir
        /// </summary>
        [HttpPost]
        public JsonResult GetPass(int ID)
        {
            if (vUser.Id != 1) return Json("Yetkiniz yok", JsonRequestBehavior.AllowGet);
            return Json(Persons.GetPass(ID), JsonRequestBehavior.AllowGet);
        }

        #endregion Password

        #region Permissions

        /// <summary>
        /// kişiye yetki atama sayfası
        /// </summary>
        public PartialViewResult Perm()
        {
            // kontrol
            if (CheckPerm(Perms.Kullanıcılar, PermTypes.Reading) == false) return null;
            var id = Url.RequestContext.RouteData.Values["id"];
            if (id == null || id.ToString2() == "") return null;
            // return
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
                if (CheckPerm(Perms.Kullanıcılar, PermTypes.Writing))
                {
                    rolePerm.PermName = rolePerm.PermName.Dehumanize();
                    var tbl = db.UserPerms.FirstOrDefault(m => m.PermName == rolePerm.PermName && m.UserName == rolePerm.UserName);
                    if (tbl == null)//ilk defa kayıt olacak
                    {
                        tbl = new UserPerm()
                        {
                            PermName = rolePerm.PermName,
                            UserName = rolePerm.UserName,
                            Reading = rolePerm.Reading == "on",
                            Writing = rolePerm.Writing == "on",
                            Updating = rolePerm.Updating == "on",
                            Deleting = rolePerm.Deleting == "on",
                            RecordDate = DateTime.Now,
                            RecordUser = vUser.UserName,
                            ModifiedDate = DateTime.Now,
                            ModifiedUser = vUser.UserName
                        };
                        if (tbl.Reading || tbl.Writing || tbl.Updating || tbl.Deleting)
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
                            tbl.Reading = rolePerm.Reading == "on";
                            tbl.Writing = rolePerm.Writing == "on";
                            tbl.Updating = rolePerm.Updating == "on";
                            tbl.Deleting = rolePerm.Deleting == "on";
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
            string[] ids = Id.Split('#');
            string pname = ids[0], uname = ids[1];
            try
            {
                var tbl = db.UserPerms.FirstOrDefault(m => m.PermName == pname && m.UserName == uname);
                if (tbl != null)
                {
                    db.UserPerms.Remove(tbl);
                    db.SaveChanges();
                    // log
                    LogActions("", "Users", "DeletePerm", ComboItems.alSil, 0, "PermName: " + pname + ", UserName: " + uname);
                }
            }
            catch (Exception ex)
            {
                Logger(ex, "Users/DeletePerm");
            }

            // return
            var result = new Result()
            {
                Id = 1,
                Message = "İşlem Başarılı !!!",
                Status = true
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion Permissions

        #region B2B

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
            var sql = string.Format("SELECT solar6.dbo.B2B_User.ID, solar6.dbo.B2B_User.HesapKodu, FINSAT6{0}.FINSAT6{0}.CHK.Unvan1 as Unvan, solar6.dbo.B2B_User.YetkiliEMail, solar6.dbo.B2B_User.Parola " +
                                "FROM solar6.dbo.B2B_User WITH(NOLOCK) INNER JOIN FINSAT6{0}.FINSAT6{0}.CHK WITH(NOLOCK) ON solar6.dbo.B2B_User.HesapKodu = FINSAT6{0}.FINSAT6{0}.CHK.HesapKodu " +
                                "ORDER BY Unvan1", vUser.SirketKodu);
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
        /// b2b chk
        /// </summary>
        public JsonResult GetChKCode(string term)
        {
            var sql = string.Format("FINSAT6{0}.[wms].[BTBCHKSearch] @HesapKodu = N'{1}', @Unvan = N'', @top = 20", vUser.SirketKodu, term);
            // return
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
            var sql = string.Format("FINSAT6{0}.[wms].[BTBCHKSearch] @HesapKodu = N'', @Unvan = N'{1}', @top = 20", vUser.SirketKodu, term);
            // return
            try
            {
                var list = db.Database.SqlQuery<frmJson>(sql).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger(ex, "Users/GetChKUnvan");
                return Json(new List<frmJson>(), JsonRequestBehavior.AllowGet);
            }
        }

        #endregion B2B

        /// <summary>
        /// sipriş yetki düzenleme sayfası
        /// </summary>
        public PartialViewResult Parametre(int ID)
        {
            if (CheckPerm(Perms.Kullanıcılar, PermTypes.Reading) == false || ID == 1) return null;
            ViewBag.ID = ID;
            var tbl = db.UserDetails.Where(m => m.UserID == ID).FirstOrDefault();
            if (tbl == null) tbl = new UserDetail()
            {
                GostCHKKodAlani = "0;ZZZ",
                GostKod3OrtBakiye = "0, 999999999999, 0, 0",
                GostRiskDeger = "0, 999999999999, 0, 0"
            };
            return PartialView("Parametre", tbl);
        }

        public PartialViewResult YetkiDuzenle(int ID)
        {
            if (CheckPerm(Perms.Kullanıcılar, PermTypes.Reading) == false || ID == 1) return null;
            var tbl = db.UserDetails.FirstOrDefault(m => m.UserID == ID);
            var yetki = new SipOnayYetkiler();
            if (tbl != null)
            {
                yetki.GostCHKKodAlani = tbl.GostCHKKodAlani;
                yetki.GostKod3OrtBakiye = tbl.GostKod3OrtBakiye;
                yetki.GostRiskDeger = tbl.GostRiskDeger;
                yetki.GostSTKDeger = tbl.GostSTKDeger;
                yetki.AdSoyad = tbl.User.AdSoyad;
            }
            else
            {
                var grv = db.Users.FirstOrDefault(m => m.ID == ID);
                yetki.GostCHKKodAlani = "";
                yetki.GostKod3OrtBakiye = "";
                yetki.GostRiskDeger = "";
                yetki.GostSTKDeger = "";
                if (grv != null) yetki.AdSoyad = grv.AdSoyad;
            }

            ViewBag.ID = ID;
            return PartialView("YetkiDuzenle", yetki);
        }

        public PartialViewResult BolgeKoduDuzenle(int ID)
        {
            if (CheckPerm(Perms.Kullanıcılar, PermTypes.Reading) == false || ID == 1) return null;
            var tbl = db.UserDetails.FirstOrDefault(m => m.UserID == ID);
            var yetki = new SipOnayYetkiler();
            if (tbl != null)
            {
                yetki.GostCHKKodAlani = tbl.GostCHKKodAlani;
                yetki.GostKod3OrtBakiye = tbl.GostKod3OrtBakiye;
                yetki.GostRiskDeger = tbl.GostRiskDeger;
                yetki.GostSTKDeger = tbl.GostSTKDeger;
                yetki.AdSoyad = tbl.User.AdSoyad;
            }
            else
            {
                var grv = db.Users.FirstOrDefault(m => m.ID == ID);
                yetki.GostCHKKodAlani = "";
                yetki.GostKod3OrtBakiye = "";
                yetki.GostRiskDeger = "";
                yetki.GostSTKDeger = "";
                if (grv != null) yetki.AdSoyad = grv.AdSoyad;
            }

            ViewBag.ID = ID;
            return PartialView("BolgeKoduDuzenle", yetki);
        }

        /// <summary>
        /// select box doldurur
        /// </summary>
        public string TipKodSelect()
        {
            var kod = db.Database.SqlQuery<RaporGetKod>(string.Format("[FINSAT6{0}].[wms].[DB_GetTipKod]", vUser.SirketKodu)).ToList();
            var json = new JavaScriptSerializer().Serialize(kod);
            return json;
        }

        /// <summary>
        /// sipriş yetki update
        /// </summary>
        public JsonResult ParametreUpdate(string CHKAraligi, string Tipler, string Kod3, string Risk, int ID)
        {
            if (CheckPerm(Perms.Kullanıcılar, PermTypes.Writing) == false || ID == 1) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            var result = new Result(false, "Hata Oluştu. ");
            var tbl = db.UserDetails.FirstOrDefault(m => m.UserID == ID);
            if (tbl != null)
                db.Database.ExecuteSqlCommand(string.Format("UPDATE BIRIKIM.usr.UserDetails SET GostKod3OrtBakiye = '{0}', GostRiskDeger = '{1}', GostSTKDeger = '{2}', GostCHKKodAlani = '{3}' WHERE UserID = {4}", Kod3, Risk, Tipler, CHKAraligi, ID));
            else
            {
                db.UserDetails.Add(new UserDetail
                {
                    UserID = ID,
                    GostCHKKodAlani = CHKAraligi,
                    GostSTKDeger = Tipler,
                    GostKod3OrtBakiye = Kod3,
                    GostRiskDeger = Risk
                });
            }
            try
            {
                db.SaveChanges();
                result.Status = true;
                result.Id = ID;
                result.Message = "İşlem Başarılı ";
            }
            catch (Exception)
            {
                // ignored
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}