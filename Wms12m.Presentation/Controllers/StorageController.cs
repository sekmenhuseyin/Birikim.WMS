using System;
using System.Linq;
using Wms12m.Entity;
using System.Web.Mvc;
using Wms12m.Business;
using Wms12m.Entity.Models;
using System.Collections.Generic;

namespace Wms12m.Presentation.Controllers
{
    public class StorageController : RootController
    {
        abstractTables<TK_DEP> Operation;
        /// <summary>
        /// anasayfası
        /// </summary>
        public ActionResult Index()
        {
            return View("Index", new TK_DEP());
        }
        /// <summary>
        /// listesi
        /// </summary>
        public ActionResult StoreGridPartial(string Id)
        {
            Operation = new Store();
            List<TK_DEP> _List = new List<TK_DEP>();
            _List = Id == "Locked" ? Operation.GetList().Where(a => a.Aktif == true).ToList() : Id == "noLocked" ? Operation.GetList().Where(a => a.Aktif == false).ToList() : Operation.GetList();
            return PartialView("_StoreGridPartial", _List);
        }
        /// <summary>
        /// düzenle
        /// </summary>
        public ActionResult StoreDetailPartial(string Id)
        {
            Operation = new Store();
            return PartialView("_StoreDetailPartial", Convert.ToInt16(Id == "" ? "0" : Id) > 0 ? Operation.Detail(Convert.ToInt16(Id)) : new TK_DEP() { Aktif = false });
        }
        /// <summary>
        /// sil
        /// </summary>
        public ActionResult Delete(string Id)
        {
            Operation = new Store();
            Result _Result = Operation.Delete(string.IsNullOrEmpty(Id) ? 0 : Convert.ToInt32(Id));
            return Json(_Result, JsonRequestBehavior.AllowGet);
            
        }
        /// <summary>
        /// kayıt işlemleri
        /// </summary>
        public ActionResult StoreOperation(TK_DEP P)
        {
            abstractTables<TK_DEP> Operation = new Store();
            Result _Result = Operation.Operation(P);
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
    }
}