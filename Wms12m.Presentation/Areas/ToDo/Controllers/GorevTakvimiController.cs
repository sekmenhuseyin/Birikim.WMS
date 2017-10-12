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
            ViewBag.Yetki = CheckPerm(Perms.TodoTakvim, PermTypes.Writing);
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
            ViewBag.UserName = UserName;
            ViewBag.RoleName = vUser.RoleName;
            if (vUser.RoleName == "Admin" || vUser.RoleName == " ")
                ViewBag.Tatil = db.Etkinliks.ToList();
            else
                ViewBag.Tatil = db.Etkinliks.Where(m => m.Username == UserName || m.Username == null).ToList();
            var list = db.GorevlerCalismas.Where(m => m.Kaydeden == UserName).GroupBy(m => m.Tarih).Select(m => new frmGorevlerCalismalar { Tarih = m.Key, Sure = m.Sum(n => n.Sure) }).ToList();
            return PartialView("List", list);
        }
        /// <summary>
        /// tatil liste
        /// </summary>
        public PartialViewResult ListTatil()
        {
            var list = db.Etkinliks.OrderByDescending(m => m.Tarih).ToList();
            return PartialView("ListTatil", list);
        }
        /// <summary>
        /// ayrıntılar
        /// </summary>
        [HttpPost]
        public PartialViewResult Details(int ID, string User)
        {
            var Tarih = ID.FromOaDate();
            var list = db.GorevlerCalismas.Where(m => m.Tarih == Tarih && m.Kaydeden == User).OrderByDescending(m => m.ID).ToList();
            return PartialView("Details", list);
        }
        /// <summary>
        /// yeni izin sayfası
        /// </summary>
        public PartialViewResult New()
        {
            if (CheckPerm(Perms.TodoTakvim, PermTypes.Writing) == false) return null;
            ViewBag.TatilTipi = new SelectList(ComboSub.GetList(Combos.TatilTipi.ToInt32()), "ID", "Name");
            ViewBag.Username = new SelectList(Persons.GetList(), "Kod", "AdSoyad");
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
        public JsonResult Save(Etkinlik satir)
        {
            if (CheckPerm(Perms.TodoTakvim, PermTypes.Writing) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            if (ModelState.IsValid)
            {
                if (satir.ID == 0)
                {
                    db.Etkinliks.Add(satir);
                }
                else
                {
                    var tbl = db.Etkinliks.Where(m => m.ID == satir.ID).FirstOrDefault();
                    tbl.TatilTipi = satir.TatilTipi;
                    tbl.Username = satir.Username;
                    tbl.Aciklama = satir.Aciklama;
                    tbl.Tarih = satir.Tarih;
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
            db.Etkinliks.Remove(tbl);
            try
            {
                db.SaveChanges();
                LogActions("ToDo", "GorevTakvimi", "Delete", ComboItems.alSil, Id, "Tatil/İzin");
                return Json(new Result(true, Id), JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new Result(false, "Hata oldu."), JsonRequestBehavior.AllowGet);
            }
        }
    }
}