using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Wms12m.Business;
using Wms12m.Entity;

namespace Wms12m.Presentation.Controllers
{
    public class StorageController : RootController
    {
        // GET: Storage
        Result _Result;
        abstractStore<Store01> Operation;
        abstractStore<Store02> delKontrolOpertion;
        //depo anasayfası
        public ActionResult Index()
        {
            return View("Index", new Store01());
        }
        //depo listesi
        public ActionResult StoreGridPartial(string Id)
        {
            Operation = new Store();
            List<Store01> _List = new List<Store01>();
            _List = Id == "Locked" ? Operation.GetList().Where(a => a.Aktif == true).ToList() : Id == "noLocked" ? Operation.GetList().Where(a => a.Aktif == false).ToList() : Operation.GetList();
            return PartialView("_StoreGridPartial", _List);
        }
        //depo düzenle
        public ActionResult StoreDetailPartial(string Id)
        {
            Operation = new Store();
            return PartialView("_StoreDetailPartial", Convert.ToInt16(Id == "" ? "0" : Id) > 0 ? Operation.Detail(Convert.ToInt16(Id)) : new Store01() { Aktif = false });
        }
        //depo sil
        public ActionResult Delete(string Id)
        {
            _Result = new Result();
            delKontrolOpertion = new Corridor();
            int altBirim = delKontrolOpertion.GetList().Where(a => a.DepoID == Convert.ToInt32(Id)).Count();
            if (altBirim < 1)
            {
                Operation = new Store();
                _Result = Operation.Delete(string.IsNullOrEmpty(Id) ? 0 : Convert.ToInt32(Id));
                return Json(_Result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                _Result.Message = "Depo";
                return Json(_Result, JsonRequestBehavior.AllowGet);
            }
            
        }
        //depo işlemleri
        public ActionResult StoreOperation(Store01 P)
        {
            _Result = new Result();
            try
            {
                abstractStore<Store01> Operation = new Store();
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