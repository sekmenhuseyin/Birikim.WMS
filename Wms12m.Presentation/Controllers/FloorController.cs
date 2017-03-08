using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Wms12m.Business;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m.Presentation.Controllers
{
    public class FloorController : RootController
    {
        abstractTables<Kat> FloorOperation;
        /// <summary>
        /// anasayfası
        /// </summary>
        public ActionResult Index()
        {
            ViewBag.DepoID = new SelectList(db.Depoes.ToList(), "ID", "DepoAd");
            ViewBag.KoridorID = new SelectList(db.Koridors.Where(m => m.ID == 0).ToList(), "ID", "KoridorAd");
            ViewBag.RafID = new SelectList(db.Rafs.Where(m => m.ID == 0).ToList(), "ID", "RafAd");
            ViewBag.BolumID = new SelectList(db.Bolums.Where(m => m.ID == 0).ToList(), "ID", "BolumAd");
            ViewBag.Ozellik = new SelectList(db.ComboItem_Name.Where(m => m.ComboID == 3).OrderBy(m=>m.Name).ToList(), "ID", "Name");
            return View("Index", new Kat());
        }
        /// <summary>
        /// listesi
        /// </summary>
        public ActionResult FloorGridPartial(string Id)
        {
            int CorridorId = 0;
            int StoreId = 0;
            int ShelfId = 0;
            int SectionId = 0;
            string Locked = "";
            FloorOperation = new Floor();
            List<Kat> _List = new List<Kat>();
            try
            {
                if (Id.IndexOf("#") > -1)
                {
                    Locked = Id.Split('#')[0];
                    StoreId = Convert.ToInt16(Id.Split('#')[1]);
                    CorridorId = Convert.ToInt16(Id.Split('#')[2]);
                    ShelfId = Convert.ToInt16(Id.Split('#')[3]);
                    SectionId = Convert.ToInt16(Id.Split('#')[4]);
                    _List = Locked == "Locked" ? FloorOperation.GetList(SectionId).Where(a => a.Aktif == true).ToList() : Locked == "noLocked" ? FloorOperation.GetList(SectionId).Where(a => a.Aktif ==false).ToList() : FloorOperation.GetList(SectionId).ToList();
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
                ViewBag.DepoID = new SelectList(db.Depoes.ToList(), "ID", "DepoAd");
                ViewBag.KoridorID = new SelectList(db.Koridors.Where(m => m.ID == 0).ToList(), "ID", "KoridorAd");
                ViewBag.RafID = new SelectList(db.Rafs.Where(m => m.ID == 0).ToList(), "ID", "RafAd");
                ViewBag.BolumID = new SelectList(db.Bolums.Where(m => m.ID == 0).ToList(), "ID", "BolumAd");
                ViewBag.Ozellik = new SelectList(db.ComboItem_Name.Where(m => m.ComboID == 3).OrderBy(m=>m.Name).ToList(), "ID", "Name");
                return PartialView("_FloorDetailPartial", new Kat());
            }
            else
            {
                var tablo = db.Kats.Where(m => m.ID == tmp).FirstOrDefault();
                ViewBag.DepoID = new SelectList(db.Depoes.ToList(), "ID", "DepoAd", tablo.Bolum.Raf.Koridor.DepoID);
                ViewBag.KoridorID = new SelectList(db.Koridors.ToList(), "ID", "KoridorAd", tablo.Bolum.Raf.KoridorID);
                ViewBag.RafID = new SelectList(db.Rafs.ToList(), "ID", "RafAd", tablo.Bolum.RafID);
                ViewBag.BolumID = new SelectList(db.Bolums.ToList(), "ID", "BolumAd", tablo.BolumID);
                ViewBag.Ozellik = new SelectList(db.ComboItem_Name.Where(m => m.ComboID == 3).OrderBy(m=>m.Name).ToList(), "ID", "Name", tablo.Ozellik);
                return PartialView("_FloorDetailPartial", new Floor().Detail(tmp));
            }
        }
        /// <summary>
        /// sil
        /// </summary>
        public JsonResult Delete(string Id)
        {
            FloorOperation = new Floor();
            Result _Result = FloorOperation.Delete(string.IsNullOrEmpty(Id) ? 0 : Convert.ToInt32(Id));
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// kayıt işlemleri
        /// </summary>
        public ActionResult FlooriOperation(Kat P)
        {
            FloorOperation = new Floor();
            Result _Result = FloorOperation.Operation(P);
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
    }
}