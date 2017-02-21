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
        abstractStore<Store05> FloorOperation;
        //kat anasayfa
        public ActionResult Index()
        {
            ViewBag.DepoID = new SelectList(db.TK_DEP.ToList(), "ID", "DepoAdi");
            ViewBag.KoridorID = new SelectList(db.TK_KOR.Where(m => m.ID == 0).ToList(), "ID", "Koridor");
            ViewBag.RafID = new SelectList(db.TK_RAF.Where(m => m.ID == 0).ToList(), "ID", "Raf");
            return View("Index", new Store05());
        }
        //kat listesi
        public ActionResult FloorGridPartial(string Id)
        {
            int CorridorId = 0;
            int StoreId = 0;
            int ShelfId = 0;
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
                    Locked = Id.Split('#')[0];
                    _List = Locked == "Locked" ? FloorOperation.SubList(ShelfId).Where(a => a.Aktif == true).ToList() : Locked == "noLocked" ? FloorOperation.SubList(ShelfId).Where(a => a.Aktif ==false).ToList() : FloorOperation.SubList(ShelfId).ToList();
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
        //kat düzenle
        public ActionResult FloorDetailPartial(string Id)
        {
            int tmp = Convert.ToInt32(Id);
            if (tmp == 0)
            {
                ViewBag.DepoID = new SelectList(db.TK_DEP.ToList(), "ID", "DepoAdi");
                ViewBag.KoridorID = new SelectList(db.TK_KOR.Where(m => m.ID == 0).ToList(), "ID", "Koridor");
                ViewBag.RafID = new SelectList(db.TK_RAF.Where(m => m.ID == 0).ToList(), "ID", "Raf");
                return PartialView("_FloorDetailPartial", new Store05());
            }
            else
            {
                var tablo = db.TK_BOL.Where(m => m.ID == tmp).FirstOrDefault();
                ViewBag.DepoID = new SelectList(db.TK_DEP.ToList(), "ID", "DepoAdi", tablo.TK_RAF.TK_KOR.DepoID);
                ViewBag.KoridorID = new SelectList(db.TK_KOR.ToList(), "ID", "Koridor", tablo.TK_RAF.KoridorID);
                ViewBag.RafID = new SelectList(db.TK_RAF.ToList(), "ID", "Raf", tablo.RafID);
                return PartialView("_FloorDetailPartial", new Floor().Detail(tmp));
            }
        }
        //kat sil
        public ActionResult Delete(string Id)
        {
            _Result = new Result();

                FloorOperation = new Floor();
                _Result = FloorOperation.Delete(string.IsNullOrEmpty(Id) ? 0 : Convert.ToInt32(Id));
                return Json(_Result, JsonRequestBehavior.AllowGet);
        }
        //kat kaydet
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