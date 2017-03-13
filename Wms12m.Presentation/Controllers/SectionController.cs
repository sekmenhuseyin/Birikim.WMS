using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Wms12m.Business;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m.Presentation.Controllers
{
    public class SectionController : RootController
    {
        abstractTables<Bolum> SectionOperation;
        /// <summary>
        /// anasayfası
        /// </summary>
        public ActionResult Index()
        {
            ViewBag.DepoID = new SelectList(Store.GetList(), "ID", "DepoAd");
            ViewBag.KoridorID = new SelectList(Corridor.GetList(0), "ID", "KoridorAd");
            ViewBag.RafID = new SelectList(Shelf.GetList(0), "ID", "RafAd");
            return View("Index", new Bolum());
        }
        /// <summary>
        /// listesi
        /// </summary>
        public ActionResult SectionGridPartial(string Id)
        {
            int CorridorId = 0;
            int StoreId = 0;
            int ShelfId = 0;
            string Locked = "";
            SectionOperation = new Section();
            List<Bolum> _List = new List<Bolum>();
            try
            {
                if (Id.IndexOf("#") > -1)
                {
                    Locked = Id.Split('#')[0];
                    StoreId = Convert.ToInt16(Id.Split('#')[1]);
                    CorridorId = Convert.ToInt16(Id.Split('#')[2]);
                    ShelfId = Convert.ToInt16(Id.Split('#')[3]);
                    _List = Locked == "Locked" ? SectionOperation.GetList(ShelfId).Where(a => a.Aktif == true).ToList() : Locked == "noLocked" ? SectionOperation.GetList(ShelfId).Where(a => a.Aktif ==false).ToList() : SectionOperation.GetList(ShelfId).ToList();
                    return PartialView("_SectionGridPartial", _List);
                }
                else
                {
                    _List = SectionOperation.GetList(Convert.ToInt16(Id));
                    return PartialView("_SectionGridPartial", _List);
                }
            }
            catch (Exception)
            {
                return PartialView("_SectionGridPartial", _List);
            }
        }
        /// <summary>
        /// düzenle
        /// </summary>
        public ActionResult SectionDetailPartial(string Id)
        {
            int tmp = Convert.ToInt32(Id);
            if (tmp == 0)
            {
                ViewBag.DepoID = new SelectList(Store.GetList(), "ID", "DepoAd");
                ViewBag.KoridorID = new SelectList(Corridor.GetList(0), "ID", "KoridorAd");
                ViewBag.RafID = new SelectList(Shelf.GetList(0), "ID", "RafAd");
                return PartialView("_SectionDetailPartial", new Bolum());
            }
            else
            {
                var tablo = Section.Detail(tmp);
                ViewBag.DepoID = new SelectList(Store.GetList(), "ID", "DepoAd", tablo.Raf.Koridor.DepoID);
                ViewBag.KoridorID = new SelectList(Corridor.GetList(tablo.Raf.Koridor.DepoID), "ID", "KoridorAd", tablo.Raf.KoridorID);
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
            catch (Exception)
            {
                return Json(_List, JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// sil
        /// </summary>
        public JsonResult Delete(string Id)
        {
            SectionOperation = new Section();
            Result _Result = SectionOperation.Delete(string.IsNullOrEmpty(Id) ? 0 : Convert.ToInt32(Id));
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// kayıt işlemleri
        /// </summary>
        public ActionResult SectioniOperation(Bolum P)
        {
            SectionOperation = new Section();
            Result _Result = SectionOperation.Operation(P);
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
    }
}