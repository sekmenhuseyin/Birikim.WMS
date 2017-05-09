using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Wms12m.Business;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m.Presentation.Controllers
{
    public class CorridorController : RootController
    {
        /// <summary>
        /// anasayfası
        /// </summary>
        public ActionResult Index()
        {
            if (CheckPerm("Koridor Kartı", PermTypes.Reading) == false) return Redirect("/");
            ViewBag.DepoID = new SelectList(Store.GetList(), "ID", "DepoAd");
            return View("Index", new Koridor());
        }
        /// <summary>
        /// listesi
        /// </summary>
        public PartialViewResult CorridorGridPartial(string Id)
        {
            if (CheckPerm("Koridor Kartı", PermTypes.Reading) == false) return null;
            List<Koridor> _List = new List<Koridor>();
            int StoreId = 0;
            string Locked = "";
            try
            {
                if (Id.IndexOf("#") > -1)
                {
                    StoreId = Convert.ToInt32(Id.Split('#')[1]);
                    Locked = Id.Split('#')[0];
                    _List = Locked == "Locked" ? Corridor.GetList(StoreId).Where(a => a.Aktif == true).ToList() : Locked == "noLocked" ? Corridor.GetList(StoreId).Where(a => a.Aktif == false).ToList() : Corridor.GetList(StoreId).ToList();
                }
                else
                {
                    StoreId = Convert.ToInt16(Id);
                    _List = Corridor.GetList(StoreId).ToList();
                }
                return PartialView("_CorridorGridPartial", _List);
            }
            catch (Exception)
            {
                return PartialView("_CorridorGridPartial", new List<Koridor>());
            }

        }
        /// <summary>
        /// düzenle
        /// </summary>
        public PartialViewResult CorridorDetailPartial(string Id)
        {
            if (CheckPerm("Koridor Kartı", PermTypes.Reading) == false) return null;
            int tmp = Convert.ToInt32(Id);
            if (tmp == 0)
            {
                ViewBag.DepoID = new SelectList(Store.GetList(), "ID", "DepoAd");
                return PartialView("_CorridorDetailPartial", new Koridor());
            }
            else
            {
                var tablo = Corridor.Detail(tmp);
                ViewBag.DepoID = new SelectList(Store.GetList(), "ID", "DepoAd", tablo.DepoID);
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
            if (CheckPerm("Koridor Kartı", PermTypes.Reading) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            List<Koridor> _List = new List<Koridor>();
            try
            {
                _List = Corridor.GetList().Where(a => a.DepoID == Convert.ToInt16(id)).ToList();
                List<SelectListItem> List = new List<SelectListItem>();
                foreach (Koridor item in _List)
                {
                    List.Add(new SelectListItem
                    {
                        Selected = false,
                        Text = item.KoridorAd,
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
            if (CheckPerm("Koridor Kartı", PermTypes.Deleting) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            Result _Result = Corridor.Delete(string.IsNullOrEmpty(Id) ? 0 : Convert.ToInt32(Id));
            return Json(_Result, JsonRequestBehavior.AllowGet);

        }
        /// <summary>
        /// kayıt işlemleri
        /// </summary>
        public JsonResult CorridorOperation(Koridor P)
        {
            if (CheckPerm("Koridor Kartı", PermTypes.Writing) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            Result _Result = Corridor.Operation(P);
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
    }
}