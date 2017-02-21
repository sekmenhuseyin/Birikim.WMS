using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m.Presentation.Controllers
{
    public class MalKabulController : RootController
    {
        // GET: MalKabul
        public ActionResult Index(int IrsNo)
        {
            ViewBag.IrsNo = IrsNo;
            return View();
        }

        public PartialViewResult MalKabulGridPartial(int IrsNo)
        {
            var list = db.GetIrsaliyeSTI(IrsNo,"33").ToList();
            return PartialView("_MalKabulGridPartial", list);
        }
        public PartialViewResult MalKabulDetailPartial(int id)
        {
            ViewBag.ID = id;
            ViewBag.GorevTipiID = new SelectList(db.ComboItemNames.Where(m => m.ComboID == 1).ToList(), "ID", "ItemName");
            ViewBag.DurumID = new SelectList(db.ComboItemNames.Where(m => m.ComboID == 2).ToList(), "ID", "ItemName");
            ViewBag.GorevliID = new SelectList(db.USR01.ToList(), "Id", "UserName");

            var list = db.WMS_STI.Where(m => m.ID == id).FirstOrDefault();
            return PartialView("_MalKabulDetailPartial", list);
        }

        public PartialViewResult MalKabulIcmalPartial(int IrsNo)
        {

            var list = db.WMS_IRS.Where(m => m.ID == IrsNo).FirstOrDefault();
            using (DinamikModelContext Dinamik = new DinamikModelContext(list.SirketKod))
            {
                ViewBag.Unvan = Dinamik.Context.CHKs.Where(x => x.HesapKodu== list.HesapKodu).Select(m => new frmUnvan { Unvan = m.Unvan1 + " " + m.Unvan2 }).FirstOrDefault();
            }
            return PartialView("_MalKabulIcmalPartial", list);
        }

        public PartialViewResult New(frmMalzeme tbl)
        {
            //check if exists
            var tmp = db.WMS_STI.Where(m => m.ID == tbl.Id).FirstOrDefault();
            if (tmp != null)
            {
                //add new
                tmp.MalKodu = tbl.MalKodu;
                tmp.Birim = tbl.Birim;
                tmp.Miktar = tbl.Miktar;
                db.SaveChanges();
            }

            var list = db.WMS_STI.Where(m => m.IrsaliyeID == tbl.IrsaliyeId).ToList();
            return PartialView("_MalKabulGridPartial", list);
        }
    }
}