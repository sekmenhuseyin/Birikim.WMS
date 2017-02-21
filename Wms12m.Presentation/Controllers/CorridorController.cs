using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Wms12m.Business;
using Wms12m.Entity;

namespace Wms12m.Presentation.Controllers
{
    public class CorridorController : RootController
    {
        // GET: Corridor
        Result _Result;
        abstractStore<Store02> Operation;
        abstractStore<Store03> delKontrolOpertion;
        //koridor anasayfa
        public ActionResult Index()
        {
            ViewBag.DepoID = new SelectList(db.TK_DEP.ToList(), "ID", "DepoAdi");
            return View("Index", new Store02());
        }
        //koridor listesi
        public ActionResult CorridorGridPartial(string Id)
        {
            Operation = new Corridor();
            List<Store02> _List = new List<Store02>();
            int StoreId = 0;
            string Locked = "";
            try
            {
                if (Id.IndexOf("#") > -1)
                {
                    StoreId = Convert.ToInt32(Id.Split('#')[1]);
                    Locked = Id.Split('#')[0];
                    _List = Locked == "Locked" ? Operation.SubList(StoreId).Where(a => a.Aktif == true).ToList() : Locked == "noLocked" ? Operation.SubList(StoreId).Where(a => a.Aktif == false).ToList() : Operation.SubList(StoreId).ToList();
                }
                else
                {
                    StoreId = Convert.ToInt16(Id);
                    _List = Operation.SubList(StoreId).ToList();
                }
                return PartialView("_CorridorGridPartial", _List);
            }
            catch (Exception)
            {
                return PartialView("_CorridorGridPartial", new List<Store02>());
            }

        }
        //koridor düzenle
        public ActionResult CorridorDetailPartial(string Id)
        {
            int tmp = Convert.ToInt32(Id);
            if (tmp == 0)
            {
                ViewBag.DepoID = new SelectList(db.TK_DEP.ToList(), "ID", "DepoAdi");
                return PartialView("_CorridorDetailPartial", new Store02());
            }
            else
            {
                var tablo = db.TK_KOR.Where(m => m.ID == tmp).FirstOrDefault();
                ViewBag.DepoID = new SelectList(db.TK_DEP.ToList(), "ID", "DepoAdi", tablo.DepoID);
                return PartialView("_CorridorDetailPartial", new Corridor().Detail(tmp));
            }
        }
        //koridor listesi
        [HttpPost]
        public JsonResult CorridorList()
        {
            var id = Url.RequestContext.RouteData.Values["id"];
            if (id == null) return null;
            List<Store02> _List = new List<Store02>();
            Operation = new Corridor();
            try
            {
                _List = Operation.GetList().Where(a => a.DepoID == Convert.ToInt16(id)).ToList();
                List<SelectListItem> List = new List<SelectListItem>();
                foreach (Store02 item in _List)
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
        //koridor sil
        public ActionResult Delete(string Id)
        {
            _Result = new Result();
            
            delKontrolOpertion = new Shelf();
            int altBirim = delKontrolOpertion.GetList().Where(a => a.KoridorID == Convert.ToInt32(Id)).Count();
            if (altBirim < 1)
            {
                Operation = new Corridor();
                _Result = Operation.Delete(string.IsNullOrEmpty(Id) ? 0 : Convert.ToInt32(Id));
                return Json(_Result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                _Result.Message = "Koridor";
                return Json(_Result, JsonRequestBehavior.AllowGet);
            }
            
        }
        //işlemler
        public ActionResult CorridorOperation(Store02 P)
        {
            _Result = new Result();
            try
            {
                Operation = new Corridor();
                _Result = Operation.Operation(P);
                return Json(_Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { data = (_Result.Status), Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}