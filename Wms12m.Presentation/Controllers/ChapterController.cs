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
        //bölüm anasayfa
        public ActionResult Index()
        {
            ViewBag.DepoID = new SelectList(db.TK_DEP.ToList(), "ID", "DepoAdi");
            ViewBag.KoridorID = new SelectList(db.TK_KOR.Where(m => m.ID == 0).ToList(), "ID", "Koridor");
            return View("Index", new Store04());
        }
        //bölüm listesi
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
        //bölüm düzenle
        public ActionResult ChapterDetailPartial(string Id)
        {
            int tmp = Convert.ToInt32(Id);
            if (tmp == 0)
            {
                ViewBag.DepoID = new SelectList(db.TK_DEP.ToList(), "ID", "DepoAdi");
                ViewBag.KoridorID = new SelectList(db.TK_KOR.Where(m => m.ID == 0).ToList(), "ID", "Koridor");
                ViewBag.RafID = new SelectList(db.TK_RAF.Where(m => m.ID == 0).ToList(), "ID", "Raf");
                return PartialView("_ChapterDetailPartial", new Store04());
            }
            else
            {
                var tablo = db.TK_BOL.Where(m => m.ID == tmp).FirstOrDefault();
                ViewBag.DepoID = new SelectList(db.TK_DEP.ToList(), "ID", "DepoAdi", tablo.TK_RAF.TK_KOR.DepoID);
                ViewBag.KoridorID = new SelectList(db.TK_KOR.ToList(), "ID", "Koridor", tablo.TK_RAF.KoridorID);
                ViewBag.RafID = new SelectList(db.TK_RAF.ToList(), "ID", "Raf", tablo.RafID);
                return PartialView("_ChapterDetailPartial", new Chapter().Detail(tmp));
            }
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
        //bölüm sil
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
        //bölüm operasyon
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