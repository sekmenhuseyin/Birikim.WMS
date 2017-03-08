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
        abstractTables<Raf> ShelfOperation;
        /// <summary>
        /// anasayfası
        /// </summary>
        public ActionResult Index()
        {
            ViewBag.DepoID = new SelectList(db.Depoes.ToList(), "ID", "DepoAd");
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
            ShelfOperation = new Shelf();
            List<Raf> _List = new List<Raf>();
            try
            {
                if (Id.IndexOf("#") > -1)
                {
                    CorridorId = Convert.ToInt16(Id.Split('#')[2]);
                    StoreId = Convert.ToInt16(Id.Split('#')[1]);
                    Locked = Id.Split('#')[0];
                     _List = Locked == "Locked" ? ShelfOperation.GetList(CorridorId).Where(a => a.Aktif == true).ToList() : Locked == "noLocked" ? ShelfOperation.GetList(CorridorId).Where(a => a.Aktif == false).ToList() : ShelfOperation.GetList(CorridorId).ToList();
                    return PartialView("_ShelfGridPartial", _List);
                }
                else
                {
                    _List = ShelfOperation.GetList(Convert.ToInt16(Id));
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
                ViewBag.DepoID = new SelectList(db.Depoes.ToList(), "ID", "DepoAd");
                ViewBag.KoridorID = new SelectList(db.Koridors.Where(m => m.ID == 0).ToList(), "ID", "KoridorAd");
                return PartialView("_ShelfDetailPartial", new Raf());
            }
            else
            {
                var tablo = db.Rafs.Where(m => m.ID == tmp).FirstOrDefault();
                ViewBag.DepoID = new SelectList(db.Depoes.ToList(), "ID", "DepoAd", tablo.Koridor.DepoID);
                ViewBag.KoridorID = new SelectList(db.Koridors.ToList(), "ID", "KoridorAd", tablo.KoridorID);
                return PartialView("_ShelfDetailPartial", new Shelf().Detail(tmp));
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
            ShelfOperation = new Shelf();
            try
            {
                _List = ShelfOperation.GetList().Where(a => a.KoridorID == Convert.ToInt16(id)).ToList();
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
            ShelfOperation = new Shelf();
            Result _Result = ShelfOperation.Delete(string.IsNullOrEmpty(Id) ? 0 : Convert.ToInt32(Id));
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// kayıt işlemleri
        /// </summary>
        public ActionResult ShelfiOperation(Raf P)
        {
            ShelfOperation = new Shelf();
            Result _Result = ShelfOperation.Operation(P);
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
    }
}