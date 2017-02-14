using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Wms12m.Business;
using Wms12m.Entity;

namespace Wms12m.Presentation.Controllers
{
    public class SizeController : RootController
    {
        // GET: Floor
        Result _Result;
        abstractStore<Store01> StoreOperation;
        abstractStore<Store02> CorrridorOperation;
        abstractStore<Store03> ShelfOperation;
        abstractStore<Store04> ChapterOperation;
        abstractStore<Store05> FloorOperation;
        abstractStore<Store06> SizeOperation;

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SizeGridPartial(string Id)
        {
            int CorridorId = 0;
            int StoreId = 0;
            int ShelfId = 0;
            int ChapterId = 0;
            int FloorId = 0;
            string Locked = "";
            SizeOperation = new Size();
            List<Store06> _List = new List<Store06>();
            try
            {
                if (Id.IndexOf("#") > -1)
                {
                    CorridorId = Convert.ToInt16(Id.Split('#')[2]);
                    StoreId = Convert.ToInt16(Id.Split('#')[1]);
                    ShelfId = Convert.ToInt16(Id.Split('#')[3]);
                    ChapterId = Convert.ToInt16(Id.Split('#')[4]);
                    FloorId = Convert.ToInt16(Id.Split('#')[5]);
                    Locked = Id.Split('#')[0];
                    _List = Locked == "Locked" ? SizeOperation.SubList(FloorId).Where(a => a.Aktif == true).ToList() : Locked == "noLocked" ? SizeOperation.SubList(FloorId).Where(a => a.Aktif == false).ToList() : SizeOperation.SubList(FloorId).ToList();
                    return PartialView("_SizeGridPartial", _List);
                }
                else
                {
                    _List = SizeOperation.SubList(Convert.ToInt16(Id));
                    return PartialView("_SizeGridPartial", _List);
                }
            }
            catch (Exception)
            {
                return PartialView("_SizeGridPartial", _List);
            }
        }
        public ActionResult SizeDetailPartial(string Id)
        {
            StoreOperation = new Store();
            ShelfOperation = new Shelf();
            CorrridorOperation = new Corridor();
            ChapterOperation = new Chapter();
            FloorOperation = new Floor();
            ViewBag.Store = StoreOperation.GetList();
            ViewBag.Shelf = ShelfOperation.GetList();
            ViewBag.Corrridor = CorrridorOperation.GetList();
            ViewBag.Chapter = ChapterOperation.GetList();
            ViewBag.Floor = FloorOperation.GetList();
            SizeOperation = new Size();
            return PartialView("_SizeDetailPartial", Convert.ToInt16(Id == "" ? "0" : Id) > 0 ? SizeOperation.Detail(Convert.ToInt16(Id)) : new Store06());
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

        public ActionResult FloorList(string Id)
        {
            List<Store05> _List = new List<Store05>();
            FloorOperation = new Floor();
            try
            {
                _List = FloorOperation.GetList().Where(a => a.BolumID == Convert.ToInt16(Id)).ToList();

                List<SelectListItem> List = new List<SelectListItem>();
                foreach (Store05 item in _List)
                {

                    List.Add(new SelectListItem
                    {
                        Selected = false,
                        Text = item.Kat,
                        Value = item.ID.ToString()
                    });

                }
                return Json(List.Select(x => new { Value = x.Value, Text = x.Text, Selected = x.Selected }), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(_List, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult SearchFloor(string Id)
        {
            List<Store05> _List = new List<Store05>();
            FloorOperation = new Floor();
            try
            {
                _List = FloorOperation.GetList().Where(a => a.BolumID == Convert.ToInt16(Id)).ToList();
                return Json(_List, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(_List, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Delete(string Id)
        {
            SizeOperation = new Size();
            _Result = new Result();
            _Result = SizeOperation.Delete(string.IsNullOrEmpty(Id) ? 0 : Convert.ToInt32(Id));
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SizeiOperation(Store06 P)
        {
            _Result = new Result();
            try
            {
                

                SizeOperation = new Size();
                _Result = SizeOperation.Operation(P);
                return Json(_Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { data = (_Result.Status), Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}