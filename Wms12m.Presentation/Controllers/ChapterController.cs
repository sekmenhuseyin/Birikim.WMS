using System;
using System.Linq;
using Wms12m.Entity;
using System.Web.Mvc;
using Wms12m.Business;
using Wms12m.Entity.Models;
using System.Collections.Generic;

namespace Wms12m.Presentation.Controllers
{
    public class ChapterController : RootController
    {
        abstractTables<TK_BOL> ChapterOperation;
        /// <summary>
        /// anasayfası
        /// </summary>
        public ActionResult Index()
        {
            ViewBag.DepoID = new SelectList(db.TK_DEP.ToList(), "ID", "Depo");
            ViewBag.KoridorID = new SelectList(db.TK_KOR.Where(m => m.ID == 0).ToList(), "ID", "Koridor");
            ViewBag.RafID = new SelectList(db.TK_RAF.Where(m => m.ID == 0).ToList(), "ID", "Raf");
            return View("Index", new TK_BOL());
        }
        /// <summary>
        /// listesi
        /// </summary>
        public ActionResult ChapterGridPartial(string Id)
        {
            int CorridorId = 0;
            int StoreId = 0;
            int ShelfId = 0;
            string Locked = "";
            List<TK_BOL> _List = new List<TK_BOL>();
            try
            {
                if (Id.IndexOf("#") > -1)
                {
                    CorridorId = Convert.ToInt16(Id.Split('#')[2]);
                    StoreId = Convert.ToInt16(Id.Split('#')[1]);
                    ShelfId = Convert.ToInt16(Id.Split('#')[3]);
                    Locked = Id.Split('#')[0];
                    _List = Locked == "Locked" ? ChapterOperation.GetList(ShelfId).Where(a => a.Aktif == true).ToList() : Locked == "noLocked" ? ChapterOperation.GetList(ShelfId).Where(a => a.Aktif ==false).ToList() : ChapterOperation.GetList(ShelfId).ToList();
                    return PartialView("_ChapterGridPartial", _List);
                }
                else
                {
                    _List = ChapterOperation.GetList(Convert.ToInt16(Id));
                    return PartialView("_ChapterGridPartial", _List);
                }
            }
            catch (Exception)
            {
                return PartialView("_ChapterGridPartial", _List);
            }
        }
        /// <summary>
        /// düzenle
        /// </summary>
        public ActionResult ChapterDetailPartial(string Id)
        {
            int tmp = Convert.ToInt32(Id);
            if (tmp == 0)
            {
                ViewBag.DepoID = new SelectList(db.TK_DEP.ToList(), "ID", "Depo");
                ViewBag.KoridorID = new SelectList(db.TK_KOR.Where(m => m.ID == 0).ToList(), "ID", "Koridor");
                ViewBag.RafID = new SelectList(db.TK_RAF.Where(m => m.ID == 0).ToList(), "ID", "Raf");
                return PartialView("_ChapterDetailPartial", new TK_BOL());
            }
            else
            {
                var tablo = db.TK_BOL.Where(m => m.ID == tmp).FirstOrDefault();
                ViewBag.DepoID = new SelectList(db.TK_DEP.ToList(), "ID", "Depo", tablo.TK_RAF.TK_KOR.DepoID);
                ViewBag.KoridorID = new SelectList(db.TK_KOR.ToList(), "ID", "Koridor", tablo.TK_RAF.KoridorID);
                ViewBag.RafID = new SelectList(db.TK_RAF.ToList(), "ID", "Raf", tablo.RafID);
                return PartialView("_ChapterDetailPartial", new Section().Detail(tmp));
            }
        }
        /// <summary>
        /// sil
        /// </summary>
        public ActionResult Delete(string Id)
        {
            ChapterOperation = new Section();
            Result _Result = ChapterOperation.Delete(string.IsNullOrEmpty(Id) ? 0 : Convert.ToInt32(Id));
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// kayıt işlemleri
        /// </summary>
        public ActionResult ChapteriOperation(TK_BOL P)
        {
            ChapterOperation = new Section();
            Result _Result = ChapterOperation.Operation(P);
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
    }
}