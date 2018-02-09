using System;
using System.Linq;
using System.Web.Mvc;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m.Presentation.Areas.ToDo.Controllers
{
    public class GorevTakvimiController : RootController
    {
        /// <summary>
        /// görevtakvimi sayfası
        /// </summary>
        public ActionResult Index()
        {
            if (CheckPerm(Perms.TodoTakvim, PermTypes.Reading) == false) return Redirect("/");
            ViewBag.UserName = vUser.UserName;
            ViewBag.RoleName = vUser.RoleName;
            if (vUser.RoleName == "Admin" || vUser.RoleName == " ")
                ViewBag.UserKod = new SelectList(Persons.GetList(), "Kod", "AdSoyad", vUser.UserName);
            return View("Index");
        }

        /// <summary>
        /// liste
        /// </summary>
        public PartialViewResult List(string UserName)
        {
            if (vUser.RoleName != "Admin" && vUser.RoleName != " " && UserName != vUser.UserName)
                UserName = vUser.UserName;
            var tblEtki = db.Etkinliks.Where(m => m.Tekrarlayan == false && m.Onay == true);
            if (vUser.RoleName != "Admin" && vUser.RoleName != " ")
                tblEtki.Where(m => m.Username == UserName || m.Username == null);
            var lstEtkinlik = tblEtki.ToList();
            var tekrarlayan = db.Etkinliks.Where(m => m.Tekrarlayan == true).ToList();
            foreach (var item in tekrarlayan)
            {
                var fark = DateTime.Today.Year - item.Tarih.Year + 1;
                for (int i = 0; i <= fark; i++)
                {
                    var item2 = new Etkinlik()
                    {
                        ID = item.ID,
                        Username = item.Username,
                        TatilTipi = item.TatilTipi,
                        Tarih = item.Tarih.AddYears(i),
                        Aciklama = item.Aciklama + " (" + i + ". Yıldönümü)",
                        Tekrarlayan = item.Tekrarlayan,
                        ComboItem_Name = item.ComboItem_Name,
                        Sure = item.Sure
                    };
                    lstEtkinlik.Add(item2);
                }
            }

            // return
            ViewBag.UserName = UserName;
            ViewBag.RoleName = vUser.RoleName;
            ViewBag.Tatil = lstEtkinlik;
            var list = db.GorevlerCalismas.Where(m => m.Kaydeden == UserName).GroupBy(m => m.Tarih).Select(m => new frmGorevlerCalismalar { Tarih = m.Key, Sure = m.Sum(n => n.Sure) }).ToList();
            return PartialView("List", list);
        }

        /// <summary>
        /// tatil liste
        /// </summary>
        public PartialViewResult ListTatil()
        {
            ViewBag.Yetki = CheckPerm(Perms.TodoTakvim, PermTypes.Writing);
            var list = db.Etkinliks.Where(m => m.ID > 0);
            if (ViewBag.Yetki == false)
                list = list.Where(m => m.Username == vUser.UserName);
            return PartialView("ListTatil", list.OrderByDescending(m => m.Tarih).ToList());
        }

        /// <summary>
        /// ayrıntılar
        /// </summary>
        [HttpPost]
        public PartialViewResult Details(int ID, string User)
        {
            var Tarih = ID.FromOaDate();
            var list = db.GorevlerCalismas.Where(m => m.Tarih == Tarih && m.Kaydeden == User).OrderByDescending(m => m.ID).ToList();
            ViewBag.Yetki2 = CheckPerm(Perms.TodoÇalışma, PermTypes.Deleting);
            return PartialView("Details", list);
        }

        /// <summary>
        /// yeni izin sayfası
        /// </summary>
        public PartialViewResult New()
        {
            if (vUser.RoleName == "Admin" || vUser.RoleName == " ")
            {
                ViewBag.Username = new SelectList(Persons.GetList(), "Kod", "AdSoyad");
                ViewBag.TatilTipi = new SelectList(ComboSub.GetList(Combos.TatilTipi.ToInt32()), "ID", "Name");
                ViewBag.Yetki = true;
            }
            else
            {
                ViewBag.Username = new SelectList(Persons.GetList(vUser.Id), "Kod", "AdSoyad", vUser.UserName);
                ViewBag.TatilTipi = new SelectList(ComboSub.GetList(new int[] { ComboItems.Mazaretİzni.ToInt32(), ComboItems.Yıllıkİzin.ToInt32() }), "ID", "Name");
                ViewBag.Yetki = false;
            }

            ViewBag.New = 0;
            return PartialView("New", new Etkinlik());
        }

        /// <summary>
        /// izin düzenleme sayfası
        /// </summary>
        public PartialViewResult Edit(int Id)
        {
            if (CheckPerm(Perms.TodoTakvim, PermTypes.Writing) == false) return null;
            var tbl = db.Etkinliks.Where(m => m.ID == Id).FirstOrDefault();
            ViewBag.TatilTipi = new SelectList(ComboSub.GetList(Combos.TatilTipi.ToInt32()), "ID", "Name", tbl.TatilTipi);
            ViewBag.Username = new SelectList(Persons.GetList(), "Kod", "AdSoyad", tbl.Username);
            ViewBag.New = 1;
            return PartialView("New", tbl);
        }

        /// <summary>
        /// izin çoğaltma sayfası
        /// </summary>
        public PartialViewResult Dublicate(int Id)
        {
            if (CheckPerm(Perms.TodoTakvim, PermTypes.Writing) == false) return null;
            var tbl = db.Etkinliks.Where(m => m.ID == Id).FirstOrDefault();
            tbl.ID = 0;
            ViewBag.TatilTipi = new SelectList(ComboSub.GetList(Combos.TatilTipi.ToInt32()), "ID", "Name", tbl.TatilTipi);
            ViewBag.Username = new SelectList(Persons.GetList(), "Kod", "AdSoyad", tbl.Username);
            ViewBag.New = 2;
            return PartialView("New", tbl);
        }

        /// <summary>
        /// kaydet
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public JsonResult Save(Etkinlik satir, DateTime? Tarih2)
        {
            if (ModelState.IsValid)
            {
                if (satir.ID == 0)
                {
                    if (satir.TatilTipi == ComboItems.Yıllıkİzin.ToInt32() && vUser.RoleName != "Admin" && vUser.RoleName != " ")
                        satir.Onay = false;
                    else
                        satir.Onay = true;
                    if (Tarih2 != null)
                    {
                        if (Tarih2 < satir.Tarih)
                            return Json(new Result(false, "Bitiş tarihi başlangıç tarihinden önce olamaz"), JsonRequestBehavior.AllowGet);
                        for (var i = satir.Tarih; i <= Tarih2; i = i.AddDays(1))
                        {
                            db.Etkinliks.Add(new Etkinlik()
                            {
                                TatilTipi = satir.TatilTipi,
                                Tarih = i,
                                Username = satir.Username,
                                Aciklama = satir.Aciklama,
                                Sure = satir.Sure,
                                Tekrarlayan = satir.Tekrarlayan,
                                Onay = satir.Onay
                            });
                        }
                    }
                    else
                        db.Etkinliks.Add(satir);
                }
                else
                {
                    var tbl = db.Etkinliks.Where(m => m.ID == satir.ID).FirstOrDefault();
                    tbl.TatilTipi = satir.TatilTipi;
                    tbl.Username = satir.Username;
                    tbl.Aciklama = satir.Aciklama;
                    tbl.Tarih = satir.Tarih;
                    tbl.Tekrarlayan = satir.Tekrarlayan;
                    tbl.Sure = satir.Sure;
                }

                try
                {
                    db.SaveChanges();
                    LogActions("ToDo", "GorevTakvimi", "Save", ComboItems.alEkle, satir.ID, "Kime: " + satir.Username + ", Açıklama: " + satir.Aciklama + ", Tarih: " + satir.Tarih);
                    return Json(new Result(true, satir.ID), JsonRequestBehavior.AllowGet);
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
        public JsonResult Delete(int Id)
        {
            if (CheckPerm(Perms.TodoTakvim, PermTypes.Deleting) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            var tbl = db.Etkinliks.Find(Id);
            try
            {
                db.Etkinliks.Remove(tbl);
                db.SaveChanges();
                return Json(new Result(true, Id), JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new Result(false, "Hata oldu."), JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// yıllık izin onay
        /// </summary>
        public JsonResult Approve(int Id)
        {
            if (CheckPerm(Perms.TodoTakvim, PermTypes.Deleting) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            var tbl = db.Etkinliks.Find(Id);
            try
            {
                tbl.Onay = true;
                db.SaveChanges();
                return Json(new Result(true, Id), JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new Result(false, "Hata oldu."), JsonRequestBehavior.AllowGet);
            }
        }
    }
}