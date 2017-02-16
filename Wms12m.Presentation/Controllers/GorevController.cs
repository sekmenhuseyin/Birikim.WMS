using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m.Presentation.Controllers
{
    public class GorevController : RootController
    {
        // GET: Gorev
        public ActionResult Index()
        {
            return View("Index", new frmGorev());
        }
        public PartialViewResult GorevGridPartial()
        {
            var list = db.GorevListesis.ToList();
            return PartialView("_GorevGridPartial", list);
        }
        public PartialViewResult GorevDetailPartial(int id)
        {
            ViewBag.ID = id;
            var list = db.GorevListesis.Where(m => m.ID == id).FirstOrDefault();
            return PartialView("_GorevDetailPartial", list);
        }

        public PartialViewResult New(frmGorev tbl)
        {
            //check if exists
            var tmp = db.GorevListesis.Where(m => m.ID == tbl.ID).FirstOrDefault();
            if (tmp != null)
            {
                //add new
                tmp.GorevliID = tbl.GorevliID;
                tmp.AtayanID = tbl.AtayanID;
                tmp.Aciklama = tbl.Aciklama;
                tmp.Bilgi = tbl.Bilgi;
                tmp.DurumID = tbl.Durum;
                db.SaveChanges();
            }
            //get list
            //var list = db.GorevListesis.Where(m => m.ID == tbl.ID).ToList();
            ViewBag.GorevID = tbl.ID;
            var list = db.GorevListesis.ToList();
            return PartialView("_GorevGridPartial", list);
        }
    }
}