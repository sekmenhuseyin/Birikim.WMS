﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wms12m.Entity;

namespace Wms12m.Presentation.Controllers
{
    public class FaturaOnayController : RootController
    {
        // GET: FaturaOnay
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult GridPartialRender(string Refresh, string ListType)
        {
            var FO = new List<FaturaOnay>();
            if (Refresh != "")
            {
                FO = db.Database.SqlQuery<FaturaOnay>(string.Format("[FINSAT6{0}].[dbo].[FaturaOnay] @onayTip='{1}'", "17",ListType)).ToList();
               
            }
            return PartialView("_PartialFaturaOnayGrid", FO);
        }

        public ActionResult FaturaDetay(string EvrakNo)
        {
            FaturaDetayData _FDD = new FaturaDetayData();
            var FDD = db.MultipleResults(string.Format("[FINSAT6{0}].[dbo].[FaturaOnayDetay] @EvrakNo='{1}'", 17, "VAS000000791")).With<FaturaDetayGenel>().With<FaturaDetaySTI>().With<FaturaDetayFTD>().Execute();
            foreach (var item in FDD[0])
            {
                _FDD.GENEL.Add((FaturaDetayGenel)item);
            }
            foreach (var item in FDD[1])
            {
                _FDD.STI.Add((FaturaDetaySTI)item);
            }
            foreach (var item in FDD[2])
            {
                _FDD.FTD.Add((FaturaDetayFTD)item);
            }
            ViewBag.EvrakNo = EvrakNo;
            return View(_FDD);
        }
    }
}