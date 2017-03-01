using System;
using System.Linq;
using Wms12m.Entity;
using System.Web.Mvc;
using Wms12m.Business;
using Wms12m.Entity.Models;
using System.Collections.Generic;

namespace Wms12m.Presentation.Controllers
{
    public class ShelfController : RootController
    {
        abstractTables<TK_RAF> ShelfOperation;
        /// <summary>
        /// anasayfası
        /// </summary>
        public ActionResult Index()
        {
            ViewBag.DepoID = new SelectList(db.TK_DEP.ToList(), "ID", "Depo");
            return View("Index", new TK_RAF());
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
            List<TK_RAF> _List = new List<TK_RAF>();
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
                ViewBag.DepoID = new SelectList(db.TK_DEP.ToList(), "ID", "Depo");
                ViewBag.KoridorID = new SelectList(db.TK_KOR.Where(m => m.ID == 0).ToList(), "ID", "Koridor");
                return PartialView("_ShelfDetailPartial", new TK_RAF());
            }
            else
            {
                var tablo = db.TK_RAF.Where(m => m.ID == tmp).FirstOrDefault();
                ViewBag.DepoID = new SelectList(db.TK_DEP.ToList(), "ID", "Depo", tablo.TK_KOR.DepoID);
                ViewBag.KoridorID = new SelectList(db.TK_KOR.ToList(), "ID", "Koridor", tablo.KoridorID);
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
            List<TK_RAF> _List = new List<TK_RAF>();
            ShelfOperation = new Shelf();
            try
            {
                _List = ShelfOperation.GetList().Where(a => a.KoridorID == Convert.ToInt16(id)).ToList();
                List<SelectListItem> List = new List<SelectListItem>();
                foreach (TK_RAF item in _List)
                {
                    List.Add(new SelectListItem
                    {
                        Selected = false,
                        Text = item.Raf,
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
        public ActionResult ShelfiOperation(TK_RAF P)
        {
            ShelfOperation = new Shelf();
            Result _Result = ShelfOperation.Operation(P);
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
    }
}