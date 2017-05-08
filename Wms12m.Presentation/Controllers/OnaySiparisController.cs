using System.Linq;
using System.Web.Mvc;
using Wms12m.Entity;
namespace Wms12m.Presentation.Controllers
{
    public class OnaySiparisController : RootController
    {
        public ActionResult SM()
        {

            return View();
        }
        public PartialViewResult PartialSM()
        {
            var KOD = db.Database.SqlQuery<SMSiparisOnaySelect>(string.Format("[FINSAT6{0}].[dbo].[SiparisOnayListSM]", "33")).ToList();
            return PartialView("_PartialSM", KOD);
        }

        public ActionResult SPGMY()
        {

            return View();
        }
        public PartialViewResult PartialSPGMY()
        {
            var KOD = db.Database.SqlQuery<SMSiparisOnaySelect>(string.Format("[FINSAT6{0}].[dbo].[SiparisOnayListSPGMY]", "33")).ToList();
            return PartialView("_PartialSPGMY", KOD);
        }

        public ActionResult GMOnay()
        {

            return View();
        }
        public PartialViewResult PartialGM()
        {
            var KOD = db.Database.SqlQuery<SMSiparisOnaySelect>(string.Format("[FINSAT6{0}].[dbo].[SiparisOnayListGM]", "33")).ToList();
            return PartialView("_PartialGM", KOD);
        }

    }
}