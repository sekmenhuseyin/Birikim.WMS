using System.Linq;
using System.Web.Mvc;
using Wms12m.Business;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m.Presentation.Controllers
{
    public class MalKabulController : RootController
    {
        Result _Result = new Result();
        // GET: MalKabul
        public ActionResult Index(int IrsNo)
        {
            dene dn = new dene();
            _Result = dn.MalKabulOnay("2", "3201100KB", 1, "220217-1", 33,"ADA");
            ViewBag.IrsNo = IrsNo;
            return View();
        }

        public PartialViewResult MalKabulGridPartial(int IrsNo)
        {
            var list = db.IRS_Detay.Where(m=>m.IrsaliyeID== IrsNo).ToList();
            return PartialView("_MalKabulGridPartial", list);
        }
        public PartialViewResult MalKabulDetailPartial(int id)
        {
            ViewBag.ID = id;
            ViewBag.GorevTipiID = new SelectList(db.ComboItem_Name.Where(m => m.ComboID == 1).ToList(), "ID", "ItemName");
            ViewBag.DurumID = new SelectList(db.ComboItem_Name.Where(m => m.ComboID == 2).ToList(), "ID", "ItemName");
            ViewBag.GorevliID = new SelectList(db.Users.ToList(), "Id", "UserName");

            var list = db.IRS_Detay.Where(m => m.ID == id).FirstOrDefault();
            return PartialView("_MalKabulDetailPartial", list);
        }

        public PartialViewResult MalKabulIcmalPartial(int IrsNo)
        {

            var list = db.IRS.Where(m => m.ID == IrsNo).FirstOrDefault();
            using (DinamikModelContext Dinamik = new DinamikModelContext(list.SirketKod))
            {
                ViewBag.Unvan = Dinamik.Context.CHKs.Where(x => x.HesapKodu== list.HesapKodu).Select(m => new frmUnvan { Unvan = m.Unvan1 + " " + m.Unvan2 }).FirstOrDefault();
            }
            return PartialView("_MalKabulIcmalPartial", list);
        }

        public PartialViewResult New(frmMalzeme tbl)
        {
            //check if exists
            var tmp = db.IRS_Detay.Where(m => m.ID == tbl.Id).FirstOrDefault();
            if (tmp != null)
            {
                //add new
                tmp.MalKodu = tbl.MalKodu;
                tmp.Birim = tbl.Birim;
                tmp.Miktar = tbl.Miktar;
                db.SaveChanges();
            }

            var list = db.IRS_Detay.Where(m => m.IrsaliyeID == tbl.IrsaliyeId).ToList();
            return PartialView("_MalKabulGridPartial", list);
        }
    }
}