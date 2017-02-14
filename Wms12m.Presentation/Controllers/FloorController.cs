using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Wms12m.Business;
using Wms12m.Entity;

namespace Wms12m.Presentation.Controllers
{
    public class FloorController : RootController
    {
        // GET: Floor
        Result _Result;
        abstractStore<Store01> StoreOperation;
        abstractStore<Store02> CorrridorOperation;
        abstractStore<Store03> ShelfOperation;
        abstractStore<Store04> ChapterOperation;
        abstractStore<Store05> FloorOperation;
        abstractStore<Store06> delKontrolOpertion;
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FloorGridPartial(string Id)
        {
            int CorridorId = 0;
            int StoreId = 0;
            int ShelfId = 0;
            int ChapterId = 0;
            string Locked = "";
            FloorOperation = new Floor();
            List<Store05> _List = new List<Store05>();
            try
            {
                if (Id.IndexOf("#") > -1)
                {
                    CorridorId = Convert.ToInt16(Id.Split('#')[2]);
                    StoreId = Convert.ToInt16(Id.Split('#')[1]);
                    ShelfId = Convert.ToInt16(Id.Split('#')[3]);
                    ChapterId = Convert.ToInt16(Id.Split('#')[4]);
                    Locked = Id.Split('#')[0];
                    _List = Locked == "Locked" ? FloorOperation.SubList(ChapterId).Where(a => a.Aktif == true).ToList() : Locked == "noLocked" ? FloorOperation.SubList(ChapterId).Where(a => a.Aktif ==false).ToList() : FloorOperation.SubList(ChapterId).ToList();
                    return PartialView("_FloorGridPartial", _List);
                }
                else
                {
                    _List = FloorOperation.SubList(Convert.ToInt16(Id));
                    return PartialView("_FloorGridPartial", _List);
                }
            }
            catch (Exception)
            {
                return PartialView("_FloorGridPartial", _List);
            }
        }
        public ActionResult FloorDetailPartial(string Id)
        {
            StoreOperation = new Store();
            ShelfOperation = new Shelf();
            CorrridorOperation = new Corridor();
            ChapterOperation = new Chapter();
            ViewBag.Store = StoreOperation.GetList();
            ViewBag.Shelf = ShelfOperation.GetList();
            ViewBag.Corrridor = CorrridorOperation.GetList();
            ViewBag.Chapter = ChapterOperation.GetList();
            FloorOperation = new Floor();
            return PartialView("_FloorDetailPartial", Convert.ToInt16(Id == "" ? "0" : Id) > 0 ? FloorOperation.Detail(Convert.ToInt16(Id)) : new Store05());
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
        public ActionResult ShelfList(string val)
        {
            List<Store03> _List = new List<Store03>();
            ShelfOperation = new Shelf();
            try
            {
                _List = ShelfOperation.GetList().Where(a => a.KoridorID == Convert.ToInt16(val)).ToList();

                List<SelectListItem> List = new List<SelectListItem>();
                foreach (Store03 item in _List)
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
        public ActionResult SearchShelf(string Id)
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
        public ActionResult ChapterList(string Id)
        {
            List<Store04> _List = new List<Store04>();
            ChapterOperation = new Chapter();
            try
            {
                _List = ChapterOperation.GetList().Where(a => a.RafID == Convert.ToInt16(Id)).ToList();

                List<SelectListItem> List = new List<SelectListItem>();
                foreach (Store04 item in _List)
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
        public ActionResult SearchChapter(string Id)
        {
            List<Store04> _List = new List<Store04>();
            ChapterOperation = new Chapter();
            try
            {
                _List = ChapterOperation.GetList().Where(a => a.RafID == Convert.ToInt16(Id)).ToList();
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

            delKontrolOpertion = new Size();
            int altBirim = delKontrolOpertion.GetList().Where(a => a.KatID == Convert.ToInt32(Id)).Count();
            if (altBirim < 1)
            {
                FloorOperation = new Floor();
                _Result = FloorOperation.Delete(string.IsNullOrEmpty(Id) ? 0 : Convert.ToInt32(Id));
                return Json(_Result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                _Result.Message = "Bölüm";
                return Json(_Result, JsonRequestBehavior.AllowGet);
            } 
        }
        public ActionResult FlooriOperation(Store05 P)
        {
            _Result = new Result();
            try
            {
                FloorOperation = new Floor();
                _Result = FloorOperation.Operation(P);
                return Json(_Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { data = (_Result.Status), Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}