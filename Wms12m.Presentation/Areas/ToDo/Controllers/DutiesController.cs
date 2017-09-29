using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m.Presentation.Areas.ToDo.Controllers
{
    public class DutiesController : RootController
    {
        /// <summary>
        /// görevler sayfası
        /// </summary>
        public ActionResult Index()
        {
            if (CheckPerm(Perms.TodoGörevler, PermTypes.Reading) == false) return Redirect("/");
            ViewBag.Yetki = CheckPerm(Perms.TodoGörevler, PermTypes.Writing);
            ViewBag.DurumID = new SelectList(ComboSub.GetList(Combos.GörevYönetimDurumları.ToInt32()), "ID", "Name");
            return View();
        }
        /// <summary>
        /// yenbi görev sayfası
        /// </summary>
        public PartialViewResult New()
        {
            if (CheckPerm(Perms.TodoGörevler, PermTypes.Writing) == false) return null;
            ViewBag.GorevTipiID = new SelectList(ComboSub.GetList(Combos.GörevYönetimTipleri.ToInt32()), "ID", "Name", "");
            ViewBag.DepartmanID = new SelectList(ComboSub.GetList(Combos.Departman.ToInt32()), "ID", "Name", "");
            ViewBag.MusteriID = new SelectList(db.Musteris.OrderBy(m => m.Unvan).ToList(), "ID", "Unvan");
            ViewBag.Sorumlu = new SelectList(Persons.GetList(), "Kod", "AdSoyad");
            ViewBag.KontrolSorumlusu = new SelectList(Persons.GetList("Destek"), "Kod", "AdSoyad");
            ViewBag.Sorumlu2 = ViewBag.Sorumlu;
            ViewBag.Sorumlu3 = ViewBag.Sorumlu;
            return PartialView(new Gorevler());
        }
        /// <summary>
        /// ayrıntılar
        /// </summary>
        [HttpPost]
        public PartialViewResult Duty_Details(int ID)
        {
            var list = db.GorevlerCalismas.Where(a => a.GorevID == ID).ToList();
            return PartialView("Duty_Details", list);
        }
        /// <summary>
        /// liste
        /// </summary>
        public PartialViewResult List(int Tip)
        {
            var list = new List<Gorevler>();
            if (CheckPerm(Perms.TodoGörevler, PermTypes.Writing) == true)
            {
                list = db.Gorevlers.Where(a => a.DurumID == Tip).OrderBy(a => a.OncelikID).ToList();
            }
            else if (vUser.RoleName == "Destek")
            {
                list = db.Gorevlers.Where(a => a.DurumID == Tip && (a.Sorumlu == vUser.UserName || a.Sorumlu2 == vUser.UserName || a.Sorumlu3 == vUser.UserName || a.KontrolSorumlusu == vUser.UserName || a.Kaydeden == vUser.UserName)).OrderBy(a => a.OncelikID).ToList();
            }
            else if (Tip != ComboItems.gydOnayVer.ToInt32() && Tip != ComboItems.gydReddedildi.ToInt32() && Tip != ComboItems.gydDurduruldu.ToInt32() && Tip != ComboItems.gydBeklemede.ToInt32())
            {
                list = db.Gorevlers.Where(a => a.DurumID == Tip && (a.Sorumlu == vUser.UserName || a.Sorumlu2 == vUser.UserName || a.Sorumlu3 == vUser.UserName || a.KontrolSorumlusu == vUser.UserName || a.Kaydeden == vUser.UserName)).OrderBy(a => a.OncelikID).ToList();
            }
            ViewBag.Yetki = CheckPerm(Perms.TodoGörevler, PermTypes.Writing);
            ViewBag.Yetki2 = CheckPerm(Perms.TodoGörevler, PermTypes.Deleting);
            return PartialView(list);
        }
        /// <summary>
        /// düzenleme sayfası
        /// </summary>
        public PartialViewResult Edit(int? id)
        {
            if (CheckPerm(Perms.TodoGörevler, PermTypes.Writing) == false) return null;
            var tbl = db.Gorevlers.Find(id);
            ViewBag.MusteriID = new SelectList(db.Musteris.OrderBy(m => m.Unvan).ToList(), "ID", "Unvan", tbl.ProjeForm.MusteriID);
            ViewBag.Proje = new SelectList(db.ProjeForms.Where(m => m.MusteriID == tbl.ProjeForm.MusteriID && m.PID == null).ToList(), "ID", "Proje", tbl.ProjeForm.PID);
            ViewBag.ProjeFormID = new SelectList(db.ProjeForms.Where(m => m.PID == tbl.ProjeForm.PID).ToList(), "ID", "Form", tbl.ProjeForm.ID);
            ViewBag.GorevTipiID = new SelectList(ComboSub.GetList(Combos.GörevYönetimTipleri.ToInt32()), "ID", "Name", tbl.GorevTipiID);
            ViewBag.DepartmanID = new SelectList(ComboSub.GetList(Combos.Departman.ToInt32()), "ID", "Name", tbl.DepartmanID);
            ViewBag.Sorumlu = new SelectList(Persons.GetList(), "Kod", "AdSoyad", tbl.Sorumlu);
            ViewBag.Sorumlu2 = new SelectList(Persons.GetList(), "Kod", "AdSoyad", tbl.Sorumlu2);
            ViewBag.Sorumlu3 = new SelectList(Persons.GetList(), "Kod", "AdSoyad", tbl.Sorumlu3);
            ViewBag.KontrolSorumlusu = new SelectList(Persons.GetList("Destek"), "Kod", "AdSoyad", tbl.KontrolSorumlusu);
            return PartialView("Edit", tbl);
        }
        /// <summary>
        /// projeler
        /// </summary>
        public JsonResult ProjeListesi()
        {
            var id = Url.RequestContext.RouteData.Values["id"];
            var ID = id.ToInt32();
            List<ProjeForm> Prj = db.ProjeForms.Where(m => m.MusteriID == ID && m.PID == null).ToList();
            List<SelectListItem> List = new List<SelectListItem>();
            foreach (ProjeForm item in Prj)
            {
                List.Add(new SelectListItem
                {
                    Selected = false,
                    Text = item.Proje,
                    Value = item.ID.ToString()
                });
            }
            return Json(List.Select(x => new { Value = x.Value, Text = x.Text, Selected = x.Selected }), JsonRequestBehavior.AllowGet);

        }
        /// <summary>
        /// formlar
        /// </summary>
        public JsonResult FormListesi()
        {
            var id = Url.RequestContext.RouteData.Values["id"];
            var ID = id.ToInt32();
            List<ProjeForm> Prj = db.ProjeForms.Where(m => m.PID == ID).ToList();
            List<SelectListItem> List = new List<SelectListItem>();
            foreach (ProjeForm item in Prj)
            {
                List.Add(new SelectListItem
                {
                    Selected = false,
                    Text = item.Form,
                    Value = item.ID.ToString()
                });
            }
            return Json(List.Select(x => new { Value = x.Value, Text = x.Text, Selected = x.Selected }), JsonRequestBehavior.AllowGet);

        }
        /// <summary>
        /// öncelik update
        /// </summary>
        public JsonResult OncelikGuncelle(string Data)
        {
            if (CheckPerm(Perms.SözleşmeTanim, PermTypes.Writing) == false) return null;
            JArray parameters = JsonConvert.DeserializeObject<JArray>(Request["Data"]);
            foreach (JObject bds in parameters)
            {
                var id = bds["ID"].ToInt32();
                List<Gorevler> grv = db.Gorevlers.ToList();
                foreach (Gorevler item in grv)
                {
                    if (item.ID == id)
                    {
                        item.OncelikID = bds["OncelikID"].ToInt32();
                    }
                }
            }
            try
            {
                var x = db.SaveChanges();
                return Json(new Result(true, 1), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger(ex, "ToDo/Duties/OncelikGuncelle");
                return Json(new Result(false, "Hata oldu"), JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// görev ret
        /// </summary>
        public JsonResult GorevReddet(int Id)
        {
            if (CheckPerm(Perms.TodoGörevler, PermTypes.Writing) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            try
            {
                db.Database.ExecuteSqlCommand(string.Format("UPDATE [BIRIKIM].[ong].[Gorevler] SET DurumID={0} WHERE ID = {1}", ComboItems.gydReddedildi.ToInt32(), Id));
                return Json(new Result(true, Id), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger(ex, "ToDo/Duties/GorevReddet");
                return Json(new Result(false, "Hata oldu"), JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// görev onay
        /// </summary>
        public JsonResult GorevOnayla(int Id)
        {
            if (CheckPerm(Perms.TodoGörevler, PermTypes.Writing) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            try
            {
                db.Database.ExecuteSqlCommand(string.Format("UPDATE [BIRIKIM].[ong].[Gorevler] SET DurumID = {0} WHERE  ID = {1}", ComboItems.gydAtandı.ToInt32(), Id));
                return Json(new Result(true, Id), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger(ex, "ToDo/Duties/GorevOnayla");
                return Json(new Result(false, "Hata oldu"), JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// kaydetme
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public JsonResult Save(Gorevler gorevler, string[] work, string silinenler, string[] todo)
        {
            if (CheckPerm(Perms.TodoGörevler, PermTypes.Writing) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            if (ModelState.IsValid)
            {
                if (gorevler.ID == 0)
                {
                    var maxSira = db.Database.SqlQuery<int>("SELECT ISNULL(MAX(OncelikID), 0) AS Expr1 FROM ong.Gorevler").FirstOrDefault();
                    gorevler.Aciklama = gorevler.Aciklama ?? "";
                    gorevler.Degistiren = vUser.UserName;
                    gorevler.Kaydeden = vUser.UserName;
                    gorevler.DegisTarih = DateTime.Now;
                    gorevler.KayitTarih = gorevler.DegisTarih;
                    gorevler.BitisTarih = null;
                    gorevler.OncelikID = maxSira + 1;
                    if (vUser.RoleName == "Admin" || vUser.RoleName == " ")
                        gorevler.DurumID = ComboItems.gydAtandı.ToInt32();
                    else if (gorevler.GorevTipiID == ComboItems.gytGeliştirme.ToInt32())
                        gorevler.DurumID = ComboItems.gydOnayVer.ToInt32();
                    else
                        gorevler.DurumID = ComboItems.gydAtandı.ToInt32();
                    db.Gorevlers.Add(gorevler);
                    //todo lists
                    if (work != null)
                        foreach (var item in work)
                        {
                            GorevlerToDoList grvTDL = new GorevlerToDoList
                            {
                                Aciklama = item,
                                DegisTarih = DateTime.Now,
                                Degistiren = vUser.UserName,
                                KayitTarih = DateTime.Now,
                                Kaydeden = vUser.UserName,
                                Gorevler = gorevler
                            };
                            if (grvTDL.Aciklama.Trim() != "") db.GorevlerToDoLists.Add(grvTDL);
                        }
                }
                else
                {
                    string[] sl = new string[0];
                    if (silinenler != null)
                    {
                        sl = silinenler.Split(',');
                    }
                    var tbl = db.Gorevlers.Where(m => m.ID == gorevler.ID).FirstOrDefault();
                    tbl.Sorumlu = gorevler.Sorumlu;
                    tbl.Sorumlu2 = gorevler.Sorumlu2;
                    tbl.Sorumlu3 = gorevler.Sorumlu3;
                    tbl.KontrolSorumlusu = gorevler.KontrolSorumlusu;
                    tbl.Gorev = gorevler.Gorev;
                    tbl.Aciklama = gorevler.Aciklama ?? "";
                    tbl.GorevTipiID = gorevler.GorevTipiID;
                    tbl.DepartmanID = gorevler.DepartmanID;
                    tbl.TahminiBitis = gorevler.TahminiBitis;
                    tbl.Degistiren = vUser.UserName;
                    tbl.DegisTarih = DateTime.Now;
                    tbl.BitisTarih = null;
                    for (int j = 0; j < sl.Length - 1; j++)
                    {
                        var idx = Convert.ToInt32(sl[j]);
                        var silGrv = db.GorevlerToDoLists.Where(m => m.ID == idx).FirstOrDefault();
                        silGrv.DegisTarih = DateTime.Now;
                        silGrv.Degistiren = vUser.UserName;
                    }
                    if (work != null)
                        for (int i = 0; i < work.Length; i++)
                        {
                            if (todo[i] == "0")
                            {
                                GorevlerToDoList grvTDL = new GorevlerToDoList
                                {
                                    Aciklama = work[i],
                                    DegisTarih = DateTime.Now,
                                    Degistiren = vUser.UserName,
                                    KayitTarih = DateTime.Now,
                                    Kaydeden = vUser.UserName,
                                    Gorevler = tbl
                                };
                                if (grvTDL.Aciklama.Trim() != "") db.GorevlerToDoLists.Add(grvTDL);
                            }
                            else
                            {
                                var id2 = Convert.ToInt32(todo[i]);
                                var grv = db.GorevlerToDoLists.Where(m => m.ID == id2).FirstOrDefault();
                                if (grv.Aciklama.Trim() != work[i])
                                {
                                    grv.DegisTarih = DateTime.Now;
                                    grv.Degistiren = vUser.UserName;
                                    GorevlerToDoList grvTDL = new GorevlerToDoList
                                    {
                                        Aciklama = work[i],
                                        DegisTarih = DateTime.Now,
                                        Degistiren = vUser.UserName,
                                        KayitTarih = DateTime.Now,
                                        Kaydeden = vUser.UserName,
                                        Gorevler = tbl
                                    };
                                    if (grvTDL.Aciklama.Trim() != "") db.GorevlerToDoLists.Add(grvTDL);
                                }
                            }
                        }
                }
                try
                {
                    db.SaveChanges();
                    return Json(new Result(true, gorevler.ID), JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    Logger(ex, "ToDo/Duties/Save");
                    return Json(new Result(false, "Kayıt hatası. Sayfayı yenileyin"), JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new Result(false, "Form hatalı. Sayfayı yenileyin"), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// sil
        /// </summary>
        public JsonResult Delete(string Id)
        {
            if (CheckPerm(Perms.TodoGörevler, PermTypes.Deleting) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            try
            {
                //TODO:
                Gorevler gorev = db.Gorevlers.Find(Id.ToInt32());
                db.SaveChanges();
                return Json(new Result(true, Id.ToInt32()), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger(ex, "ToDo/Duties/Delete");
                return Json(new Result(false, "Hata oldu"), JsonRequestBehavior.AllowGet);
            }
        }
    }
}
