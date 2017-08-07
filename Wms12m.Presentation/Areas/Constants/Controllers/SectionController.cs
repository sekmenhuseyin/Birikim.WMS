using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Wms12m.Business;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m.Presentation.Areas.Constants.Controllers
{
    public class SectionController : RootController
    {
        abstractTables<Bolum> SectionOperation;
        /// <summary>
        /// anasayfası
        /// </summary>
        public ActionResult Index()
        {
            if (CheckPerm(Perms.BölümKartı, PermTypes.Reading) == false) return Redirect("/");
            ViewBag.DepoID = new SelectList(Store.GetList(), "ID", "DepoAd");
            ViewBag.RafID = new SelectList(Shelf.GetList(0), "ID", "RafAd");
            return View("Index", new Bolum());
        }
        /// <summary>
        /// listesi
        /// </summary>
        public PartialViewResult SectionGridPartial(string Id)
        {
            if (CheckPerm(Perms.BölümKartı, PermTypes.Reading) == false) return null;
            int StoreId = 0;
            int ShelfId = 0;
            string Locked = "";
            SectionOperation = new Section();
            List<Bolum> _List = new List<Bolum>();
            try
            {
                if (Id.Right(1) == "#") { }
                else if (Id.IndexOf("#") > -1)
                {
                    Locked = Id.Split('#')[0];
                    StoreId = Convert.ToInt16(Id.Split('#')[1]);
                    ShelfId = Convert.ToInt16(Id.Split('#')[2]);
                    _List = Locked == "Locked" ? SectionOperation.GetList(ShelfId).Where(a => a.Aktif == true).ToList() : Locked == "noLocked" ? SectionOperation.GetList(ShelfId).Where(a => a.Aktif == false).ToList() : SectionOperation.GetList(ShelfId).ToList();
                }
                else
                {
                    _List = SectionOperation.GetList(Convert.ToInt16(Id));
                }
                return PartialView("_SectionGridPartial", _List);
            }
            catch (Exception ex)
            {
                Logger(ex, "Floor/FloorList");
                return PartialView("_SectionGridPartial", _List);
            }
        }
        /// <summary>
        /// düzenle
        /// </summary>
        public PartialViewResult SectionDetailPartial(string Id)
        {
            if (CheckPerm(Perms.BölümKartı, PermTypes.Reading) == false) return null;
            int tmp = Convert.ToInt32(Id);
            if (tmp == 0)
            {
                ViewBag.DepoID = new SelectList(Store.GetList(), "ID", "DepoAd");
                ViewBag.RafID = new SelectList(Shelf.GetList(0), "ID", "RafAd");
                return PartialView("_SectionDetailPartial", new Bolum());
            }
            else
            {
                var tablo = Section.Detail(tmp);
                ViewBag.DepoID = new SelectList(Store.GetList(), "ID", "DepoAd", tablo.Raf.Koridor.DepoID);
                ViewBag.RafID = new SelectList(Shelf.GetList(tablo.Raf.KoridorID), "ID", "RafAd", tablo.RafID);
                return PartialView("_SectionDetailPartial", tablo);
            }
        }
        /// <summary>
        /// listesi
        /// </summary>
        [HttpPost]
        public JsonResult SectionList()
        {
            var id = Url.RequestContext.RouteData.Values["id"];
            if (id == null) return null;
            if (CheckPerm(Perms.BölümKartı, PermTypes.Reading) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            List<Bolum> _List = new List<Bolum>();
            SectionOperation = new Section();
            try
            {
                _List = SectionOperation.GetList().Where(a => a.RafID == Convert.ToInt16(id)).ToList();
                List<SelectListItem> List = new List<SelectListItem>();
                foreach (Bolum item in _List)
                {
                    List.Add(new SelectListItem
                    {
                        Selected = false,
                        Text = item.BolumAd,
                        Value = item.ID.ToString()
                    });
                }
                return Json(List.Select(x => new { Value = x.Value, Text = x.Text, Selected = x.Selected }), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger(ex, "Floor/SectionList");
                return Json(_List, JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// sil
        /// </summary>
        public JsonResult Delete(string Id)
        {
            if (CheckPerm(Perms.BölümKartı, PermTypes.Deleting) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            SectionOperation = new Section();
            Result _Result = SectionOperation.Delete(string.IsNullOrEmpty(Id) ? 0 : Convert.ToInt32(Id));
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// kayıt işlemleri
        /// </summary>
        public JsonResult SectioniOperation(Bolum P)
        {
            if (CheckPerm(Perms.BölümKartı, PermTypes.Writing) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            SectionOperation = new Section();
            Result _Result = SectionOperation.Operation(P);
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
    }
}