using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Wms12m.Business;
using Wms12m.Entity;
using Wms12m.Entity.Models;
using Wms12m.Entity.Mysql;

namespace Wms12m.Presentation.Areas.Constants.Controllers
{
    public class StorageController : RootController
    {
        /// <summary>
        /// anasayfası
        /// </summary>
        public ActionResult Index()
        {
            if (CheckPerm(Perms.DepoKartı, PermTypes.Reading) == false) return Redirect("/");
            var mysql = db.Settings.Select(m => m.KabloSiparisMySql).FirstOrDefault();
            if (mysql)
            {
                using (KabloEntities dbx = new KabloEntities())
                {
                    ViewBag.KabloDepoID = new SelectList(dbx.depoes.OrderBy(m => m.depo1).ToList(), "id", "depo1");
                }
            }

            ViewBag.mysql = mysql;
            ViewBag.Yetki = CheckPerm(Perms.DepoKartı, PermTypes.Writing);
            return View("Index", new Depo());
        }
        /// <summary>
        /// listesi
        /// </summary>
        public PartialViewResult StoreGridPartial(string Id)
        {
            if (CheckPerm(Perms.DepoKartı, PermTypes.Reading) == false) return null;
            List<Depo> _List = new List<Depo>();
            _List = Id == "Locked" ? Store.GetList().Where(a => a.Aktif == true).ToList() : Id == "noLocked" ? Store.GetList().Where(a => a.Aktif == false).ToList() : Store.GetList();
            return PartialView("_StoreGridPartial", _List);
        }
        /// <summary>
        /// düzenle
        /// </summary>
        public PartialViewResult StoreDetailPartial(string Id)
        {
            if (CheckPerm(Perms.DepoKartı, PermTypes.Reading) == false) return null;
            var item = Convert.ToInt16(Id == "" ? "0" : Id) > 0 ? Store.Detail(Convert.ToInt16(Id)) : new Depo() { Aktif = false };
            var mysql = db.Settings.Select(m => m.KabloSiparisMySql).FirstOrDefault();
            if (mysql)
            {
                using (KabloEntities dbx = new KabloEntities())
                {
                    ViewBag.KabloDepoID = new SelectList(dbx.depoes.OrderBy(m => m.depo1).ToList(), "id", "depo1", item.KabloDepoID);
                }
            }

            ViewBag.mysql = mysql;
            ViewBag.Yetki = CheckPerm(Perms.DepoKartı, PermTypes.Writing);
            return PartialView("_StoreDetailPartial", item);
        }
        /// <summary>
        /// sil
        /// </summary>
        public JsonResult Delete(string Id)
        {
            if (CheckPerm(Perms.DepoKartı, PermTypes.Deleting) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            var _Result = Store.Delete(string.IsNullOrEmpty(Id) ? 0 : Convert.ToInt32(Id));
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// kayıt işlemleri
        /// </summary>
        public JsonResult StoreOperation(Depo P)
        {
            if (CheckPerm(Perms.DepoKartı, PermTypes.Writing) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            Result _Result;
            if (P.ID == 0)//yeni depo ise mutlaka rezerv kat=koridor oluştur...
            {
                _Result = Store.Operation(P);
                _Result = Corridor.Operation(new Koridor() { DepoID = _Result.Id, Aktif = true, KoridorAd = "R" });
                _Result = Shelf.Operation(new Raf() { KoridorID = _Result.Id, Aktif = true, RafAd = "Z" });
                _Result = Section.Operation(new Bolum() { RafID = _Result.Id, Aktif = true, BolumAd = "R" });
                _Result = Floor.Operation(new Kat() { BolumID = _Result.Id, Aktif = true, KatAd = "V", OzellikID = ComboItems.MuhtelifKoli.ToInt32(), Boy = 1, En = 1, Derinlik = 1, AgirlikKapasite = 1 });
            }
            else
                _Result = Store.Operation(P);
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
    }
}