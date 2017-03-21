using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Wms12m.Business;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m.Presentation.Controllers
{
    public class ShelfController : RootController
    {
        /// <summary>
        /// anasayfası
        /// </summary>
        public ActionResult Index()
        {
            ViewBag.DepoID = new SelectList(Store.GetList(), "ID", "DepoAd");
            ViewBag.KoridorID = new SelectList(Corridor.GetList(0), "ID", "KoridorAd");
            return View("Index", new Raf());
        }
        /// <summary>
        /// listesi
        /// </summary>
        public ActionResult ShelfGridPartial(string Id)
        {
            int CorridorId = 0;
            int StoreId = 0;
            string Locked = "";
            List<Raf> _List = new List<Raf>();
            try
            {
                if (Id.IndexOf("#") > -1)
                {
                    CorridorId = Convert.ToInt16(Id.Split('#')[2]);
                    StoreId = Convert.ToInt16(Id.Split('#')[1]);
                    Locked = Id.Split('#')[0];
                     _List = Locked == "Locked" ? Shelf.GetList(CorridorId).Where(a => a.Aktif == true).ToList() : Locked == "noLocked" ? Shelf.GetList(CorridorId).Where(a => a.Aktif == false).ToList() : Shelf.GetList(CorridorId).ToList();
                    return PartialView("_ShelfGridPartial", _List);
                }
                else
                {
                    _List = Shelf.GetList(Convert.ToInt16(Id));
                    return PartialView("_ShelfGridPartial", _List);
                }
            }
            catch (Exception)
            {
                return PartialView("_ShelfGridPartial", _List);
            }
        }
        /// <summary>
        /// düzenle
        /// </summary>
        public PartialViewResult ShelfDetailPartial(string Id)
        {
            int tmp = Convert.ToInt32(Id);
            if (tmp==0)
            {
                ViewBag.DepoID = new SelectList(Store.GetList(), "ID", "DepoAd");
                ViewBag.KoridorID = new SelectList(Corridor.GetList(0), "ID", "KoridorAd");
                return PartialView("_ShelfDetailPartial", new Raf());
            }
            else
            {
                var tablo = Shelf.Detail(tmp);
                ViewBag.DepoID = new SelectList(Store.GetList(), "ID", "DepoAd", tablo.Koridor.DepoID);
                ViewBag.KoridorID = new SelectList(Corridor.GetList(tablo.Koridor.DepoID), "ID", "KoridorAd", tablo.KoridorID);
                return PartialView("_ShelfDetailPartial", tablo);
            }
        }
        /// <summary>
        /// listesi
        /// </summary>
        [HttpPost]
        public JsonResult ShelfList()
        {
            var id = Url.RequestContext.RouteData.Values["id"];
            if (id == null) return null;
            List<Raf> _List = new List<Raf>();
            try
            {
                _List = Shelf.GetList().Where(a => a.KoridorID == Convert.ToInt16(id)).ToList();
                List<SelectListItem> List = new List<SelectListItem>();
                foreach (Raf item in _List)
                {
                    List.Add(new SelectListItem
                    {
                        Selected = false,
                        Text = item.RafAd,
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
            Result _Result = Shelf.Delete(string.IsNullOrEmpty(Id) ? 0 : Convert.ToInt32(Id));
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// kayıt işlemleri
        /// </summary>
        public ActionResult ShelfiOperation(Raf P)
        {
            Result _Result = Shelf.Operation(P);
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
    }
}