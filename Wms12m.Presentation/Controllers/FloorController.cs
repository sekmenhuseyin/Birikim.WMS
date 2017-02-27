using System;
using System.Linq;
using Wms12m.Entity;
using System.Web.Mvc;
using Wms12m.Business;
using Wms12m.Entity.Models;
using System.Collections.Generic;

namespace Wms12m.Presentation.Controllers
{
    public class FloorController : RootController
    {
        abstractTables<TK_KAT> FloorOperation;
        /// <summary>
        /// anasayfası
        /// </summary>
        public ActionResult Index()
        {
            ViewBag.DepoID = new SelectList(db.TK_DEP.ToList(), "ID", "Depo");
            ViewBag.KoridorID = new SelectList(db.TK_KOR.Where(m => m.ID == 0).ToList(), "ID", "Koridor");
            ViewBag.RafID = new SelectList(db.TK_RAF.Where(m => m.ID == 0).ToList(), "ID", "Raf");
            return View("Index", new TK_KAT());
        }
        /// <summary>
        /// listesi
        /// </summary>
        public ActionResult FloorGridPartial(string Id)
        {
            int CorridorId = 0;
            int StoreId = 0;
            int ShelfId = 0;
            string Locked = "";
            FloorOperation = new Floor();
            List<TK_KAT> _List = new List<TK_KAT>();
            try
            {
                if (Id.IndexOf("#") > -1)
                {
                    CorridorId = Convert.ToInt16(Id.Split('#')[2]);
                    StoreId = Convert.ToInt16(Id.Split('#')[1]);
                    ShelfId = Convert.ToInt16(Id.Split('#')[3]);
                    Locked = Id.Split('#')[0];
                    _List = Locked == "Locked" ? FloorOperation.GetList(ShelfId).Where(a => a.Aktif == true).ToList() : Locked == "noLocked" ? FloorOperation.GetList(ShelfId).Where(a => a.Aktif ==false).ToList() : FloorOperation.GetList(ShelfId).ToList();
                    return PartialView("_FloorGridPartial", _List);
                }
                else
                {
                    _List = FloorOperation.GetList(Convert.ToInt16(Id));
                    return PartialView("_FloorGridPartial", _List);
                }
            }
            catch (Exception)
            {
                return PartialView("_FloorGridPartial", _List);
            }
        }
        /// <summary>
        /// düzenle
        /// </summary>
        public ActionResult FloorDetailPartial(string Id)
        {
            int tmp = Convert.ToInt32(Id);
            if (tmp == 0)
            {
                ViewBag.DepoID = new SelectList(db.TK_DEP.ToList(), "ID", "Depo");
                ViewBag.KoridorID = new SelectList(db.TK_KOR.Where(m => m.ID == 0).ToList(), "ID", "Koridor");
                ViewBag.RafID = new SelectList(db.TK_RAF.Where(m => m.ID == 0).ToList(), "ID", "Raf");
                return PartialView("_FloorDetailPartial", new TK_KAT());
            }
            else
            {
                var tablo = db.TK_KAT.Where(m => m.ID == tmp).FirstOrDefault();
                ViewBag.DepoID = new SelectList(db.TK_DEP.ToList(), "ID", "Depo", tablo.TK_BOL.TK_RAF.TK_KOR.DepoID);
                ViewBag.KoridorID = new SelectList(db.TK_KOR.ToList(), "ID", "Koridor", tablo.TK_BOL.TK_RAF.KoridorID);
                ViewBag.RafID = new SelectList(db.TK_RAF.ToList(), "ID", "Raf", tablo.TK_BOL.RafID);
                ViewBag.BolumID = new SelectList(db.TK_BOL.ToList(), "ID", "Bolum", tablo.BolumID);
                return PartialView("_FloorDetailPartial", new Floor().Detail(tmp));
            }
        }
        /// <summary>
        /// sil
        /// </summary>
        public ActionResult Delete(string Id)
        {
            FloorOperation = new Floor();
            Result _Result = FloorOperation.Delete(string.IsNullOrEmpty(Id) ? 0 : Convert.ToInt32(Id));
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// kayıt işlemleri
        /// </summary>
        public ActionResult FlooriOperation(TK_KAT P)
        {
            FloorOperation = new Floor();
            Result _Result = FloorOperation.Operation(P);
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
    }
}