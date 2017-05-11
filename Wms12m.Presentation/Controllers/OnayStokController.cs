using System.Linq;
using System.Web.Mvc;
using Wms12m.Entity;

namespace Wms12m.Presentation.Controllers
{
    public class OnayStokController : RootController
    {
        // GET: StokOnay
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult List(string Durum)
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

            var KOD = db.Database.SqlQuery<StokOnaySelect>(string.Format("[FINSAT6{0}].[wms].[StokOnaySelect] {1}", "01", param)).ToList();
            return PartialView("List", KOD);
        }
    }
}