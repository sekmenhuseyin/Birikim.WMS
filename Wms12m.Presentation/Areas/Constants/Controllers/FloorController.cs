using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Wms12m.Business;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m.Presentation.Areas.Constants.Controllers
{
    public class FloorController : RootController
    {
        /// <summary>
        /// anasayfası
        /// </summary>
        public ActionResult Index()
        {
            if (CheckPerm(Perms.KatKartı, PermTypes.Reading) == false) return Redirect("/");
            ViewBag.DepoID = new SelectList(Store.GetList(), "ID", "DepoAd");
            return View("Index");
        }
        /// <summary>
        /// listesi
        /// </summary>
        public PartialViewResult FloorGridPartial(string Id)
        {
            if (CheckPerm(Perms.KatKartı, PermTypes.Reading) == false) return null;
            var StoreId = 0;
            var ShelfId = 0;
            var SectionId = 0;
            var Locked = "";
            List<Kat> _List = new List<Kat>();
            try
            {
                if (Id.Right(1) == "#") { }
                else if (Id.IndexOf("#") > -1)
                {
                    Locked = Id.Split('#')[0];
                    StoreId = Convert.ToInt16(Id.Split('#')[1]);
                    ShelfId = Convert.ToInt16(Id.Split('#')[2]);
                    SectionId = Convert.ToInt16(Id.Split('#')[3]);
                    _List = Locked == "Locked" ? Floor.GetList(SectionId).Where(a => a.Aktif == true).ToList() : Locked == "noLocked" ? Floor.GetList(SectionId).Where(a => a.Aktif == false).ToList() : Floor.GetList(SectionId).ToList();
                }
                else
                {
                    _List = Floor.GetList(Convert.ToInt16(Id));
                }

                return PartialView("_FloorGridPartial", _List);
            }
            catch (Exception ex)
            {
                Logger(ex, "Floor/FloorGridPartial");
                return PartialView("_FloorGridPartial", _List);
            }
        }
        /// <summary>
        /// düzenle
        /// </summary>
        public PartialViewResult FloorDetailPartial(string Id)
        {
            if (CheckPerm(Perms.KatKartı, PermTypes.Reading) == false) return null;
            var tmp = Convert.ToInt32(Id);
            if (tmp == 0)
            {
                ViewBag.DepoID = new SelectList(Store.GetList(), "ID", "DepoAd");
                ViewBag.RafID = new SelectList(Shelf.GetList(0), "ID", "RafAd");
                ViewBag.BolumID = new SelectList(Section.GetList(0), "ID", "BolumAd");
                ViewBag.OzellikID = new SelectList(ComboSub.GetList(Combos.Özellik.ToInt32()), "ID", "Name");
                return PartialView("_FloorDetailPartial", new Kat());
            }
            else
            {
                var tablo = Floor.Detail(tmp);
                ViewBag.DepoID = new SelectList(Store.GetList(), "ID", "DepoAd", tablo.Bolum.Raf.Koridor.DepoID);
                ViewBag.RafID = new SelectList(Shelf.GetList(tablo.Bolum.Raf.KoridorID), "ID", "RafAd", tablo.Bolum.RafID);
                ViewBag.BolumID = new SelectList(Section.GetList(tablo.Bolum.RafID), "ID", "BolumAd", tablo.BolumID);
                ViewBag.OzellikID = new SelectList(ComboSub.GetList(Combos.Özellik.ToInt32()), "ID", "Name", tablo.OzellikID);
                return PartialView("_FloorDetailPartial", tablo);
            }
        }
        /// <summary>
        /// bölüme ait kat listesi
        /// </summary>
        [HttpPost]
        public JsonResult FloorList()
        {
            var id = Url.RequestContext.RouteData.Values["id"];
            if (id == null) return null;
            if (CheckPerm(Perms.KatKartı, PermTypes.Reading) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            List<Kat> _List = new List<Kat>();
            try
            {
                _List = Floor.GetList().Where(a => a.BolumID == Convert.ToInt16(id)).ToList();
                List<SelectListItem> List = new List<SelectListItem>();
                foreach (Kat item in _List)
                {
                    List.Add(new SelectListItem
                    {
                        Selected = false,
                        Text = item.KatAd,
                        Value = item.ID.ToString()
                    });
                }

                return Json(List.Select(x => new { Value = x.Value, Text = x.Text, Selected = x.Selected }), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger(ex, "Floor/FloorList");
                return Json(_List, JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// sil
        /// </summary>
        public JsonResult Delete(string Id)
        {
            if (CheckPerm(Perms.KatKartı, PermTypes.Deleting) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            var _Result = Floor.Delete(string.IsNullOrEmpty(Id) ? 0 : Convert.ToInt32(Id));
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// kayıt işlemleri
        /// </summary>
        public JsonResult FlooriOperation(Kat P)
        {
            if (CheckPerm(Perms.KatKartı, PermTypes.Writing) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            var _Result = Floor.Operation(P);
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
    }
}