using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Wms12m.Entity;

namespace Wms12m.Presentation.Controllers
{
    public class StokOnayController : RootController
    {
        // GET: StokOnay
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult PartialStokOnay(string Durum)
        {
            int param = 1;
            if (Durum == "Tumu") { param = 0; }
            else
            if (Durum == "Onay") { param = 1; }
            else
            if (Durum == "Pasif") { param = 2; }
            else
            if (Durum == "Aktif") { param = 3; }
            else
            if (Durum == "Red") { param = 4; }

            List<StokOnaySelect> KOD = db.Database.SqlQuery<StokOnaySelect>(string.Format("[FINSAT6{0}].[dbo].[StokOnaySelect] {1}", "17", param)).ToList();
            return PartialView("_PartialStokOnay", KOD);
        }
    }
}