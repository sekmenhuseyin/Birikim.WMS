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
        abstractStore<Store01> StoreOperation;
        abstractStore<Store02> CorrridorOperation;
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
            CorrridorOperation = new Corridor();
            List<Store02> _List = new List<Store02>();
            int StoreId = 0;
            string Locked = "";
            try
            {
                if (Id.IndexOf("#") > -1)
                {
                    StoreId = Convert.ToInt32(Id.Split('#')[1]);
                    Locked = Id.Split('#')[0];
                    _List = Locked == "Locked" ? CorrridorOperation.SubList(StoreId).Where(a => a.Aktif == true).ToList() : Locked == "noLocked" ? CorrridorOperation.SubList(StoreId).Where(a => a.Aktif == false).ToList() : CorrridorOperation.SubList(StoreId).ToList();
                }
                else
                {
                    StoreId = Convert.ToInt16(Id);
                    _List = CorrridorOperation.SubList(StoreId).ToList();
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
            CorrridorOperation = new Corridor();
            ViewBag.DepoID = new SelectList(db.TK_DEP.ToList(), "ID", "DepoAdi");
            return PartialView("_CorridorDetailPartial", Convert.ToInt16(Id == "" ? "0" : Id) > 0 ? CorrridorOperation.Detail(Convert.ToInt16(Id)) : new Store02());
        }
        //koridor listesi
        public ActionResult StoreList()
        {
            StoreOperation = new Store();
            return Json(StoreOperation.GetList(), JsonRequestBehavior.AllowGet);
        }
        //koridor sil
        public ActionResult Delete(string Id)
        {
            _Result = new Result();
            
            delKontrolOpertion = new Shelf();
            int altBirim = delKontrolOpertion.GetList().Where(a => a.KoridorID == Convert.ToInt32(Id)).Count();
            if (altBirim < 1)
            {
                CorrridorOperation = new Corridor();
                _Result = CorrridorOperation.Delete(string.IsNullOrEmpty(Id) ? 0 : Convert.ToInt32(Id));
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
                CorrridorOperation = new Corridor();
                _Result = CorrridorOperation.Operation(P);
                return Json(_Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { data = (_Result.Status), Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}