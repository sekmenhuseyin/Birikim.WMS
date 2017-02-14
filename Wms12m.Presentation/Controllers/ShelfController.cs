using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Wms12m.Business;
using Wms12m.Entity;
using Wms12m.Security;

namespace Wms12m.Presentation.Controllers
{
    public class ShelfController : RootController
    {
        // GET: Shelf
        Result _Result;
        abstractStore<Store01> StoreOperation;
        abstractStore<Store02> CorrridorOperation;
        abstractStore<Store03> ShelfOperation;
        abstractStore<Store04> delKontrolOpertion;
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ShelfGridPartial(string Id)
        {
            int CorridorId = 0;
            int StoreId = 0;
            string Locked = "";
            ShelfOperation = new Shelf();
            List<Store03> _List = new List<Store03>();
            try
            {
                if (Id.IndexOf("#") > -1)
                {
                    CorridorId = Convert.ToInt16(Id.Split('#')[2]);
                    StoreId = Convert.ToInt16(Id.Split('#')[1]);
                    Locked = Id.Split('#')[0];
                     _List = Locked == "Locked" ? ShelfOperation.SubList(CorridorId).Where(a => a.Aktif == true).ToList() : Locked == "noLocked" ? ShelfOperation.SubList(CorridorId).Where(a => a.Aktif == false).ToList() : ShelfOperation.SubList(CorridorId).ToList();
                    return PartialView("_ShelfGridPartial", _List);
                }
                else
                {
                    _List = ShelfOperation.SubList(Convert.ToInt16(Id));
                    return PartialView("_ShelfGridPartial", _List);
                }
            }
            catch (Exception)
            {
                return PartialView("_ShelfGridPartial", _List);
            }
        }
        public ActionResult ShelfDetailPartial(string Id)
        {
            StoreOperation = new Store();
            ViewBag.Store = StoreOperation.GetList();
            ShelfOperation = new Shelf();
            return PartialView("_ShelfDetailPartial", Convert.ToInt16(Id == "" ? "0" : Id) > 0 ? ShelfOperation.Detail(Convert.ToInt16(Id)) : new Store03());
        }
        public ActionResult CorrridorList(string val)
        {
            List<Store02> _List = new List<Store02>();
            CorrridorOperation = new Corridor();
            try
            {
                _List = CorrridorOperation.GetList().Where(a => a.DepoID == Convert.ToInt16(val)).ToList();

                List<SelectListItem>  List = new List<SelectListItem>();
                foreach (Store02 item in _List)
                {
                    
                        List.Add(new SelectListItem
                        {
                            Selected = false,
                            Text = item.Koridor,
                            Value = item.ID.ToString()
                        });
                                     
                }
                return Json(List.Select(x => new { Value = x.Value, Text = x.Text, Selected=x.Selected}), JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(_List, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult SearchCorrridor(string Id)
        {
            List<Store02> _List = new List<Store02>();
            CorrridorOperation = new Corridor();
            try
            {
                _List = CorrridorOperation.GetList().Where(a => a.DepoID == Convert.ToInt16(Id)).ToList();
                return Json(_List, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(_List, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Delete(string Id)
        {
            _Result = new Result();

            delKontrolOpertion = new Chapter();
            int altBirim = delKontrolOpertion.GetList().Where(a => a.RafID == Convert.ToInt32(Id)).Count();
            if (altBirim < 1)
            {
                ShelfOperation = new Shelf();
                _Result = ShelfOperation.Delete(string.IsNullOrEmpty(Id) ? 0 : Convert.ToInt32(Id));
                return Json(_Result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                _Result.Message = "Raf";
                return Json(_Result, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult ShelfiOperation(Store03 P)
        {
            _Result = new Result();
            try
            {
                ShelfOperation = new Shelf();
                _Result = ShelfOperation.Operation(P);
                return Json(_Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { data = (_Result.Status), Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}