﻿using System.Collections.Generic;
using System.Web.Mvc;

namespace Wms12m.Presentation.Controllers
{
    public class StockController : RootController
    {
        /// <summary>
        /// stok ana sayfası
        /// </summary>
        public ActionResult Index()
        {
            ViewBag.DepoID = new SelectList(Store.GetList(), "ID", "DepoAd");
            return View("Index");
        }
        /// <summary>
        /// listeyi yeniler
        /// </summary>
        [HttpPost]
        public PartialViewResult List(string Id)
        {
            var list = db.GetSirketDBs();
            List<string> liste = new List<string>();
            foreach (var item in list)
            {
                liste.Add(item);
            }
            ViewBag.Sirket = liste;
            string[] ids = Id.Split('#');
            if (ids[2] != "0")
                return PartialView("List", Yerlestirme.GetListFromRaf(ids[2].ToInt32()));
            else if (ids[1] != "0")
                return PartialView("List", Yerlestirme.GetListFromKoridor(ids[1].ToInt32()));
            else if (ids[0] != "0")
                return PartialView("List", Yerlestirme.GetListFromDepo(ids[0].ToInt32()));
            return null;
        }
    }
}