﻿using System;
using System.Linq;
using Wms12m.Entity;
using System.Web.Mvc;
using Wms12m.Business;
using Wms12m.Entity.Models;

namespace Wms12m.Presentation.Controllers
{
    public class SizeController : RootController
    {
        /// <summary>
        /// boyutlar anasayfası
        /// </summary>
        public ActionResult Index()
        {
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            return View("Index");
        }
        /// <summary>
        /// şirket ID'ye göre stok listesini getirir
        /// </summary>
        public PartialViewResult GetList()
        {
            var id = Url.RequestContext.RouteData.Values["id"];
            if (id == null) return null;
            if (id.ToString() == "0") return null;
            var list = db.Olcus.Where(m => m.SirketKodu == id.ToString()).OrderBy(m => m.MalKodu).ToList();
            return PartialView("_GetList", list);
        }
        /// <summary>
        /// yeni boyut kartı
        /// </summary>
        public PartialViewResult Create()
        {
            var id = Url.RequestContext.RouteData.Values["id"];
            if (id == null) return null;
            if (id.ToString() == "0") return null;
            Olcu tbl = new Olcu();
            tbl.SirketKodu = id.ToString();
            return PartialView("_Create", tbl);
        }
        /// <summary>
        /// düzenleme
        /// </summary>
        public ActionResult Edit()
        {
            var id = Url.RequestContext.RouteData.Values["id"];
            if (id == null) return null;
            if (id.ToString() == "0") return null;
            int tmp = id.ToInt32();
            var tbl = db.Olcus.Where(m => m.ID == tmp).FirstOrDefault();
            if (tbl == null) return null;
            return View("_Edit", tbl);
        }
        /// <summary>
        /// yeni boyut kartı
        /// </summary>
        public PartialViewResult Save(Olcu tbl)
        {
            Dimension tmpTable = new Dimension();
            Result _Result = tmpTable.Operation(tbl);
            var list = db.Olcus.Where(m => m.SirketKodu == tbl.SirketKodu).OrderBy(m => m.MalKodu).ToList();
            return PartialView("_GetList", list);
        }
        /// <summary>
        /// silme
        /// </summary>
        public ActionResult Delete()
        {
            return View("Index");
        }
    }
}