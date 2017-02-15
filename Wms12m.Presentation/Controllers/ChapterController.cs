using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Wms12m.Business;
using Wms12m.Entity;

namespace Wms12m.Presentation.Controllers
{
    public class ChapterController : RootController
    {
        // GET: Chapter
        Result _Result;
        abstractStore<Store01> StoreOperation;
        abstractStore<Store02> CorrridorOperation;
        abstractStore<Store03> ShelfOperation;
        abstractStore<Store04> ChapterOperation;
        abstractStore<Store05> delKontrolOpertion;
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ChapterGridPartial(string Id)
        {
            int CorridorId = 0;
            int StoreId = 0;
            int ShelfId = 0;
            string Locked = "";
            ChapterOperation = new Chapter();
            List<Store04> _List = new List<Store04>();
            try
            {
                if (Id.IndexOf("#") > -1)
                {
                    CorridorId = Convert.ToInt16(Id.Split('#')[2]);
                    StoreId = Convert.ToInt16(Id.Split('#')[1]);
                    ShelfId = Convert.ToInt16(Id.Split('#')[3]);
                    Locked = Id.Split('#')[0];
                    _List = Locked == "Locked" ? ChapterOperation.SubList(ShelfId).Where(a => a.Aktif == true).ToList() : Locked == "noLocked" ? ChapterOperation.SubList(ShelfId).Where(a => a.Aktif ==false).ToList() : ChapterOperation.SubList(ShelfId).ToList();
                    return PartialView("_ChapterGridPartial", _List);
                }
                else
                {
                    _List = ChapterOperation.SubList(Convert.ToInt16(Id));
                    return PartialView("_ChapterGridPartial", _List);
                }
            }
            catch (Exception)
            {
                return PartialView("_ChapterGridPartial", _List);
            }
        }
        public ActionResult ChapterDetailPartial(string Id)
        {
            StoreOperation = new Store();
            ShelfOperation = new Shelf();
            CorrridorOperation = new Corridor();
            ViewBag.Store = StoreOperation.GetList();
            ViewBag.Shelf = ShelfOperation.GetList();
            ViewBag.Corrridor = CorrridorOperation.GetList();
            ChapterOperation = new Chapter();
            return PartialView("_ChapterDetailPartial", Convert.ToInt16(Id == "" ? "0" : Id) > 0 ? ChapterOperation.Detail(Convert.ToInt16(Id)) : new Store04());
        }
        public ActionResult CorrridorList(string val)
        {
            List<Store02> _List = new List<Store02>();
            CorrridorOperation = new Corridor();
            try
            {
                _List = CorrridorOperation.GetList().Where(a => a.DepoID == Convert.ToInt16(val)).ToList();

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
        public ActionResult ShelfList(string Id)
        {
            List<Store03> _List = new List<Store03>();
            ShelfOperation = new Shelf();
            try
            {
                _List = ShelfOperation.GetList().Where(a => a.KoridorID == Convert.ToInt16(Id)).ToList();                
                return Json(_List, JsonRequestBehavior.AllowGet);
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

            delKontrolOpertion = new Floor();
            int altBirim = delKontrolOpertion.GetList().Where(a => a.RafID == Convert.ToInt32(Id)).Count();
            if (altBirim < 1)
            {
                ChapterOperation = new Chapter();
                _Result = ChapterOperation.Delete(string.IsNullOrEmpty(Id) ? 0 : Convert.ToInt32(Id));
                return Json(_Result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                _Result.Message = "Bölüm";
                return Json(_Result, JsonRequestBehavior.AllowGet);
            } 
        }
        public ActionResult ChapteriOperation(Store04 P)
        {
            _Result = new Result();
            try
            {
                ChapterOperation = new Chapter();
                _Result = ChapterOperation.Operation(P);
                return Json(_Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { data = (_Result.Status), Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}