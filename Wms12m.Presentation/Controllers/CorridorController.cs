using System;
using System.Linq;
using Wms12m.Entity;
using System.Web.Mvc;
using Wms12m.Business;
using Wms12m.Entity.Models;
using System.Collections.Generic;

namespace Wms12m.Presentation.Controllers
{
    public class CorridorController : RootController
    {
        abstractTables<TK_KOR> Operation;
        /// <summary>
        /// anasayfası
        /// </summary>
        public ActionResult Index()
        {
            ViewBag.DepoID = new SelectList(db.TK_DEP.ToList(), "ID", "Depo");
            return View("Index", new TK_KOR());
        }
        /// <summary>
        /// listesi
        /// </summary>
        public ActionResult CorridorGridPartial(string Id)
        {
            Operation = new Corridor();
            List<TK_KOR> _List = new List<TK_KOR>();
            int StoreId = 0;
            string Locked = "";
            try
            {
                if (Id.IndexOf("#") > -1)
                {
                    StoreId = Convert.ToInt32(Id.Split('#')[1]);
                    Locked = Id.Split('#')[0];
                    _List = Locked == "Locked" ? Operation.GetList(StoreId).Where(a => a.Aktif == true).ToList() : Locked == "noLocked" ? Operation.GetList(StoreId).Where(a => a.Aktif == false).ToList() : Operation.GetList(StoreId).ToList();
                }
                else
                {
                    StoreId = Convert.ToInt16(Id);
                    _List = Operation.GetList(StoreId).ToList();
                }
                return PartialView("_CorridorGridPartial", _List);
            }
            catch (Exception)
            {
                return PartialView("_CorridorGridPartial", new List<TK_KOR>());
            }

        }
        /// <summary>
        /// düzenle
        /// </summary>
        public ActionResult CorridorDetailPartial(string Id)
        {
            int tmp = Convert.ToInt32(Id);
            if (tmp == 0)
            {
                ViewBag.DepoID = new SelectList(db.TK_DEP.ToList(), "ID", "Depo");
                return PartialView("_CorridorDetailPartial", new TK_KOR());
            }
            else
            {
                var tablo = db.TK_KOR.Where(m => m.ID == tmp).FirstOrDefault();
                ViewBag.DepoID = new SelectList(db.TK_DEP.ToList(), "ID", "Depo", tablo.DepoID);
                return PartialView("_CorridorDetailPartial", new Corridor().Detail(tmp));
            }
        }
        /// <summary>
        /// listesi
        /// </summary>
        [HttpPost]
        public JsonResult CorridorList()
        {
            var id = Url.RequestContext.RouteData.Values["id"];
            if (id == null) return null;
            List<TK_KOR> _List = new List<TK_KOR>();
            Operation = new Corridor();
            try
            {
                _List = Operation.GetList().Where(a => a.DepoID == Convert.ToInt16(id)).ToList();
                List<SelectListItem> List = new List<SelectListItem>();
                foreach (TK_KOR item in _List)
                {
                    List.Add(new SelectListItem
                    {
                        Selected = false,
                        Text = item.Koridor,
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
            Operation = new Corridor();
            Result _Result = Operation.Delete(string.IsNullOrEmpty(Id) ? 0 : Convert.ToInt32(Id));
            return Json(_Result, JsonRequestBehavior.AllowGet);
            
        }
        /// <summary>
        /// kayıt işlemleri
        /// </summary>
        public ActionResult CorridorOperation(TK_KOR P)
        {
            Operation = new Corridor();
            Result _Result = Operation.Operation(P);
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
    }
}