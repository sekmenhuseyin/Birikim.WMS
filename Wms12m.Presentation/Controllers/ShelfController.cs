using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Wms12m.Business;
using Wms12m.Entity;

namespace Wms12m.Presentation.Controllers
{
    public class ShelfController : RootController
    {
        // GET: Shelf
        Result _Result;
        abstractStore<Store02> CorrridorOperation;
        abstractStore<Store03> ShelfOperation;
        abstractStore<Store04> delKontrolOpertion;
        //raf anasayfası
        public ActionResult Index()
        {
            ViewBag.DepoID = new SelectList(db.TK_DEP.ToList(), "ID", "DepoAdi");
            return View("Index", new Store03());
        }
        //raf listesi
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
        //raf düzenleme
        public PartialViewResult ShelfDetailPartial(string Id)
        {
            int tmp = Convert.ToInt32(Id);
            if (tmp==0)
            {
                ViewBag.DepoID = new SelectList(db.TK_DEP.ToList(), "ID", "DepoAdi");
                ViewBag.KoridorID = new SelectList(db.TK_KOR.Where(m => m.ID == 0).ToList(), "ID", "Koridor");
                return PartialView("_ShelfDetailPartial", new Store03());
            }
            else
            {
                var tablo = db.TK_RAF.Where(m => m.ID == tmp).FirstOrDefault();
                ViewBag.DepoID = new SelectList(db.TK_DEP.ToList(), "ID", "DepoAdi", tablo.TK_KOR.DepoID);
                ViewBag.KoridorID = new SelectList(db.TK_KOR.ToList(), "ID", "Koridor", tablo.KoridorID);
                return PartialView("_ShelfDetailPartial", new Shelf().Detail(tmp));
            }
        }
        //raf listesi
        [HttpPost]
        public ActionResult CorrridorList()
        {
            var id = Url.RequestContext.RouteData.Values["id"];
            if (id == null) return null;
            List<Store02> _List = new List<Store02>();
            CorrridorOperation = new Corridor();
            try
            {
                _List = CorrridorOperation.GetList().Where(a => a.DepoID == Convert.ToInt16(id)).ToList();

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
        //koridor arama?
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
        //raf sil
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
        //raf işlemleri
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