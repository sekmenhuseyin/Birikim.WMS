using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Wms12m.Presentation.Controllers
{
    public class MalKabulController : RootController
    {
        // GET: MalKabul
        public ActionResult Index(int IrsNo)
        {
            return View();
        }

        public PartialViewResult MalKabulGridPartial(int id)
        {
            var list = db.GorevListesis.ToList();
            return PartialView("_GorevGridPartial", list);
        }
        public PartialViewResult MalKabulDetailPartial(int id)
        {
            ViewBag.ID = id;
            ViewBag.GorevTipiID = new SelectList(db.ComboItemNames.Where(m => m.ComboID == 1).ToList(), "ID", "ItemName");
            ViewBag.DurumID = new SelectList(db.ComboItemNames.Where(m => m.ComboID == 2).ToList(), "ID", "ItemName");
            ViewBag.GorevliID = new SelectList(db.USR01.ToList(), "Id", "UserName");

            var list = db.GorevListesis.Where(m => m.ID == id).FirstOrDefault();
            return PartialView("_GorevDetailPartial", list);
        }

        public PartialViewResult MalKabulIcmalPartial(int id)
        {
            ViewBag.ID = id;
            ViewBag.GorevTipiID = new SelectList(db.ComboItemNames.Where(m => m.ComboID == 1).ToList(), "ID", "ItemName");
            ViewBag.DurumID = new SelectList(db.ComboItemNames.Where(m => m.ComboID == 2).ToList(), "ID", "ItemName");
            ViewBag.GorevliID = new SelectList(db.USR01.ToList(), "Id", "UserName");

            var list = db.GorevListesis.Where(m => m.ID == id).FirstOrDefault();
            return PartialView("_GorevDetailPartial", list);
        }

        //public PartialViewResult New(frmGorev tbl)
        //{
        //    //check if exists
        //    var tmp = db.GorevListesis.Where(m => m.ID == tbl.ID).FirstOrDefault();
        //    if (tmp != null)
        //    {
        //        //add new
        //        tmp.GorevliID = tbl.GorevliID;
        //        tmp.Aciklama = tbl.Aciklama;
        //        tmp.Bilgi = tbl.Bilgi;
        //        tmp.DurumID = tbl.DurumID;
        //        db.SaveChanges();
        //    }
        //    //get list
        //    //var list = db.GorevListesis.Where(m => m.ID == tbl.ID).ToList();
        //    ViewBag.GorevID = tbl.ID;
        //    var list = db.GorevListesis.ToList();
        //    return PartialView("_GorevGridPartial", list);
        //}
    }
}