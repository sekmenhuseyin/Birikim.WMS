using System;
using System.Linq;
using System.Web.Mvc;
using Wms12m.Business;
using Wms12m.Entity;

namespace Wms12m.Presentation.Controllers
{
    public class GorevController : RootController
    {

        // GET: Gorev
        public ActionResult Index()
        {
            var list = db.GorevListesis.ToList();
            return View("Index", list);
        }
        public ActionResult GorevDetailPartial(int id)
        {
            var list = db.GorevListesis.Where(m => m.ID == id).FirstOrDefault();
            ViewBag.GorevTipiID = new SelectList(db.ComboItemNames.Where(m => m.ComboID == 1).ToList(), "ID", "ItemName", list.GorevTipiID);
            ViewBag.DurumID = new SelectList(db.ComboItemNames.Where(m => m.ComboID == 2).ToList(), "ID", "ItemName", list.DurumID);
            ViewBag.GorevliID = new SelectList(db.USR01.ToList(), "Id", "UserName", list.GorevliID);
            return PartialView("_GorevDetailPartial", list);
        }

        public PartialViewResult Update(frmGorev tbl)
        {
            //update
            Gorev tmpTable = new Gorev();
            Result _Result = tmpTable.Update(tbl);
            //get list
            ViewBag.GorevID = tbl.ID;
            var list = db.GorevListesis.ToList();
            return PartialView("_GorevGridPartial", list);
        }

        public string MalKabulOnay(string EvrakNo, string CHK, int IrsaliyeNo, string GorevNo, int SirketKodu)
        {
            return "";
        }
    }
}