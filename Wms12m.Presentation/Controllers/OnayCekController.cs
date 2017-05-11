using System.Linq;
using System.Web.Mvc;
using Wms12m.Entity;

namespace Wms12m.Presentation.Controllers
{
    public class OnayCekController : RootController
    {
        public ActionResult SPGMY()
        {
            return View();
        }
        public PartialViewResult PartialSPGMY()
        {
            var KOD = db.Database.SqlQuery<CekOnaySelect>(string.Format("[FINSAT6{0}].[dbo].[CekOnaySPGMY]", "01")).ToList();
            return PartialView("_PartialSPGMY", KOD);
        }

        public ActionResult MIGMY()
        {

            return View();
        }
        public PartialViewResult PartialMIGMY()
        {
            var KOD = db.Database.SqlQuery<CekOnaySelect>(string.Format("[FINSAT6{0}].[dbo].[CekOnayMIGMY]", "01")).ToList();
            return PartialView("_PartialMIGMY", KOD);
        }

        public ActionResult GM()
        {

            return View();
        }
        public PartialViewResult PartialGM()
        {
            var KOD = db.Database.SqlQuery<CekOnaySelect>(string.Format("[FINSAT6{0}].[dbo].[CekOnayGM]", "01")).ToList();
            return PartialView("_PartialGM", KOD);
        }
    }
}