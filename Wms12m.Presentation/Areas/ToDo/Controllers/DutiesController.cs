using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
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
        /// yeni görev sayfası
        /// </summary>
        public PartialViewResult New()
        {
            if (CheckPerm(Perms.TodoGörevler, PermTypes.Writing) == false) return null;
            ViewBag.GorevTipiID = new SelectList(ComboSub.GetList(Combos.GörevYönetimTipleri.ToInt32()), "ID", "Name", "");
            ViewBag.DepartmanID = new SelectList(ComboSub.GetList(Combos.Departman.ToInt32()), "ID", "Name", "");
            ViewBag.MusteriID = new SelectList(db.Musteris.OrderBy(m => m.Unvan).ToList(), "ID", "Unvan");
            ViewBag.Sorumlu = new SelectList(Persons.GetList(), "Kod", "AdSoyad");
            ViewBag.KontrolSorumlusu = new SelectList(Persons.GetList(new string[] { "Destek", "Admin" }), "Kod", "AdSoyad");
            ViewBag.Sorumlu2 = ViewBag.Sorumlu;
            ViewBag.Sorumlu3 = ViewBag.Sorumlu;
            return PartialView(new Gorevler());
        }
        /// <summary>
        /// ayrıntılar
        /// </summary>
        [HttpPost]
        public PartialViewResult Details(int ID)
        {
            var list = db.GorevlerCalismas.Where(a => a.GorevID == ID).OrderByDescending(m => m.Tarih).ToList();
            ViewBag.ID = ID;
            return PartialView("Details", list);
        }
        /// <summary>
        /// liste
        /// </summary>
        public PartialViewResult List(int Tip)
        {
            var list = db.Gorevlers.Where(m => m.ID > 0);
            if (Tip == 0)
            {
                var tip1 = ComboItems.gydReddedildi.ToInt32();
                var tip2 = ComboItems.gydOnaylandı.ToInt32();
                var tip3 = ComboItems.gydBeklemede.ToInt32();
                var tip4 = ComboItems.gydOnayVer.ToInt32();
                var tip5 = ComboItems.gydBitti.ToInt32();
                var tip6 = ComboItems.gydDurduruldu.ToInt32();
                list = list.Where(m => m.DurumID != tip1 && m.DurumID != tip2 && m.DurumID != tip3 && m.DurumID != tip4 && m.DurumID != tip5 && m.DurumID != tip6);
            }
            else
                list = list.Where(m => m.DurumID == Tip);
            ViewBag.Yetki = CheckPerm(Perms.TodoGörevler, PermTypes.Deleting);
            ViewBag.Yetki1 = CheckPerm(Perms.TodoGörevler, PermTypes.Writing);
            return PartialView(list.OrderBy(m => m.OncelikID).ToList());
        }
        /// <summary>
        /// sadece onay listesi
        /// </summary>
        public PartialViewResult ListOnay()
        {
            if (CheckPerm(Perms.TodoGörevler, PermTypes.Deleting) == false) return null;
            var Tip = ComboItems.gydOnayVer.ToInt32();
            var list = db.Gorevlers.Where(m => m.DurumID == Tip);
            ViewBag.Yetki = CheckPerm(Perms.TodoGörevler, PermTypes.Deleting);
            ViewBag.Yetki1 = CheckPerm(Perms.TodoGörevler, PermTypes.Writing);
            ViewBag.Tip = Tip;
            return PartialView(list.OrderBy(m => m.OncelikID).ToList());
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
            ViewBag.KontrolSorumlusu = new SelectList(Persons.GetList(new string[] { "Destek", "Admin" }), "Kod", "AdSoyad", tbl.KontrolSorumlusu);
            return PartialView("Edit", tbl);
        }
        /// <summary>
        /// projeler
        /// </summary>
        public JsonResult ProjeListesi()
        {
            var id = Url.RequestContext.RouteData.Values["id"];
            var ID = id.ToInt32();
            var list = db.ProjeForms.Where(m => m.MusteriID == ID && m.PID == null).Select(m => new SelectListItem { Selected = false, Text = m.Proje, Value = m.ID.ToString() }).OrderBy(m => m.Text).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);

        }
        /// <summary>
        /// formlar
        /// </summary>
        public JsonResult FormListesi()
        {
            var id = Url.RequestContext.RouteData.Values["id"];
            var ID = id.ToInt32();
            var list = db.ProjeForms.Where(m => m.PID == ID).Select(m => new SelectListItem { Selected = false, Text = m.Form, Value = m.ID.ToString() }).OrderBy(m => m.Text).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);

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
                var grv = db.Gorevlers.ToList();
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
                db.SaveChanges();
                return Json(new Result(true, 1), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger(ex, "ToDo/Duties/OncelikGuncelle");
                return Json(new Result(false, "Hata oldu"), JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// görev onay / ret / durdur
        /// </summary>
        public JsonResult GorevOnay(int Id, int Tip)
        {
            if (CheckPerm(Perms.TodoGörevler, PermTypes.Deleting) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);//sadece admin onaylayabilsin diye silme yetkisine bakıyorum
            try
            {
                var satir = db.Gorevlers.Where(m => m.ID == Id).FirstOrDefault();
                satir.DurumID = Tip == 0 ? ComboItems.gydAtandı.ToInt32() : (Tip == 1 ? ComboItems.gydReddedildi.ToInt32() : (Tip == 2 ? ComboItems.gydDurduruldu.ToInt32() : ComboItems.gydBaşlandı.ToInt32()));
                satir.Degistiren = vUser.UserName;
                satir.DegisTarih = DateTime.Now;
                db.SaveChanges();
                return Json(new Result(true, Id), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger(ex, "ToDo/Duties/GorevOnay");
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
            if (!ModelState.IsValid)
                return Json(new Result(false, "Form hatalı. Sayfayı yenileyin"), JsonRequestBehavior.AllowGet);
            //yeni görev ekle
            if (gorevler.ID == 0)
            {
                if (work == null)
                    return Json(new Result(false, "Lütfen bir madde yazınız!"), JsonRequestBehavior.AllowGet);
                if (work[0] == null || work[0] == "")
                    return Json(new Result(false, "Lütfen bir madde yazınız!"), JsonRequestBehavior.AllowGet);
                //set
                var maxSira = db.Database.SqlQuery<int>("SELECT ISNULL(MAX(OncelikID), 0) AS Expr1 FROM ong.Gorevler").FirstOrDefault();
                gorevler.Aciklama = gorevler.Aciklama ?? "";
                gorevler.Degistiren = vUser.UserName;
                gorevler.DegisTarih = DateTime.Now;
                gorevler.Kaydeden = vUser.UserName;
                gorevler.KayitTarih = gorevler.DegisTarih;
                gorevler.OncelikID = maxSira + 1;
                if (CheckPerm(Perms.TodoGörevler, PermTypes.Deleting) == false && gorevler.GorevTipiID == ComboItems.gytGeliştirme.ToInt32())
                    gorevler.DurumID = ComboItems.gydOnayVer.ToInt32();
                else
                    gorevler.DurumID = ComboItems.gydAtandı.ToInt32();
                db.Gorevlers.Add(gorevler);
                //todo lists
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
            //görev güncelle
            else
            {
                //sil
                string[] sl = new string[0];
                if (silinenler != null)
                {
                    sl = silinenler.Split(',');
                }
                for (int j = 0; j < sl.Length - 1; j++)
                {
                    var idx = Convert.ToInt32(sl[j]);
                    var silGrv = db.GorevlerToDoLists.Where(m => m.ID == idx).FirstOrDefault();
                    db.GorevlerToDoLists.Remove(silGrv);
                }
                //görevi bul ve değiştir
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
                if (work != null)
                    for (int i = 0; i < work.Length; i++)
                    {
                        //yeni madde ekle
                        if (todo[i] == "0")
                        {
                            GorevlerToDoList grvTDL = new GorevlerToDoList
                            {
                                Aciklama = work[i],
                                Kaydeden = vUser.UserName,
                                KayitTarih = DateTime.Now,
                                Degistiren = vUser.UserName,
                                DegisTarih = DateTime.Now,
                                Gorevler = tbl
                            };
                            if (grvTDL.Aciklama.Trim() != "") db.GorevlerToDoLists.Add(grvTDL);
                        }
                        //maddeyi güncelle
                        else
                        {
                            var id2 = Convert.ToInt32(todo[i]);
                            var grv = db.GorevlerToDoLists.Where(m => m.ID == id2).FirstOrDefault();
                            if (grv.Onay == true && grv.Aciklama != work[i].ToString2())
                                return Json(new Result(false, "Tamamlanan maddeleri değiştiremezsiniz"), JsonRequestBehavior.AllowGet);
                            grv.Aciklama = work[i].ToString2();
                            grv.DegisTarih = DateTime.Now;
                            grv.Degistiren = vUser.UserName;
                        }
                    }
            }
            try
            {
                db.SaveChanges();
                LogActions("ToDo", "Duties", "Save", ComboItems.alEkle, gorevler.ID, "Gorev: " + gorevler.Gorev);
                return Json(new Result(true, gorevler.ID), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger(ex, "ToDo/Duties/Save");
                return Json(new Result(false, "Kayıt hatası. Sayfayı yenileyin"), JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// sil
        /// </summary>
        public JsonResult Delete(string Id)
        {
            if (CheckPerm(Perms.TodoGörevler, PermTypes.Deleting) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            Gorevler gorev = db.Gorevlers.Find(Id.ToInt32());
            if (gorev.GorevlerCalismas.FirstOrDefault() != null)
                return Json(new Result(false, "Bu göreve çalışma kaydedildiği için silinemez"), JsonRequestBehavior.AllowGet);
            try
            {
                LogActions("ToDo", "Duties", "Delete", ComboItems.alSil, Id.ToInt32());
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
