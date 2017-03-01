using System;
using System.Linq;
using Wms12m.Entity;
using System.Web.Mvc;
using Wms12m.Business;
using Wms12m.Entity.Models;
using System.Collections.Generic;

namespace Wms12m.Presentation.Controllers
{
    public class SectionController : RootController
    {
        abstractTables<TK_BOL> SectionOperation;
        /// <summary>
        /// anasayfası
        /// </summary>
        public ActionResult Index()
        {
            ViewBag.DepoID = new SelectList(db.TK_DEP.ToList(), "ID", "Depo");
            ViewBag.KoridorID = new SelectList(db.TK_KOR.Where(m => m.ID == 0).ToList(), "ID", "Koridor");
            ViewBag.RafID = new SelectList(db.TK_RAF.Where(m => m.ID == 0).ToList(), "ID", "Raf");
            return View("Index", new TK_BOL());
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
            List<TK_BOL> _List = new List<TK_BOL>();
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
                ViewBag.DepoID = new SelectList(db.TK_DEP.ToList(), "ID", "Depo");
                ViewBag.KoridorID = new SelectList(db.TK_KOR.Where(m => m.ID == 0).ToList(), "ID", "Koridor");
                ViewBag.RafID = new SelectList(db.TK_RAF.Where(m => m.ID == 0).ToList(), "ID", "Raf");
                return PartialView("_SectionDetailPartial", new TK_BOL());
            }
            else
            {
                var tablo = db.TK_BOL.Where(m => m.ID == tmp).FirstOrDefault();
                ViewBag.DepoID = new SelectList(db.TK_DEP.ToList(), "ID", "Depo", tablo.TK_RAF.TK_KOR.DepoID);
                ViewBag.KoridorID = new SelectList(db.TK_KOR.ToList(), "ID", "Koridor", tablo.TK_RAF.KoridorID);
                ViewBag.RafID = new SelectList(db.TK_RAF.ToList(), "ID", "Raf", tablo.RafID);
                return PartialView("_SectionDetailPartial", new Section().Detail(tmp));
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
            List<TK_BOL> _List = new List<TK_BOL>();
            SectionOperation = new Section();
            try
            {
                _List = SectionOperation.GetList().Where(a => a.RafID == Convert.ToInt16(id)).ToList();
                List<SelectListItem> List = new List<SelectListItem>();
                foreach (TK_BOL item in _List)
                {
                    List.Add(new SelectListItem
                    {
                        Selected = false,
                        Text = item.Bolum,
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
        public ActionResult SectioniOperation(TK_BOL P)
        {
            SectionOperation = new Section();
            Result _Result = SectionOperation.Operation(P);
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
    }
}