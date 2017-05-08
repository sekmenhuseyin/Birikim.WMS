using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Wms12m.Entity;
namespace Wms12m.Presentation.Controllers
{
    public class OnaySiparisController : RootController
    {
        public ActionResult SMOnay()
        {

            return View();
        }
        public PartialViewResult PartialSMOnay()
        {
            List<SMSiparisOnaySelect> KOD = db.Database.SqlQuery<SMSiparisOnaySelect>(string.Format("[FINSAT6{0}].[dbo].[SiparisOnayListSM]", "33")).ToList();
            return PartialView("_PartialSMSiparisOnay", KOD);
        }

        public ActionResult GMYOnay()
        {

            return View();
        }
        public PartialViewResult PartialGMYOnay()
        {
            List<SMSiparisOnaySelect> KOD = db.Database.SqlQuery<SMSiparisOnaySelect>(string.Format("[FINSAT6{0}].[dbo].[SiparisOnayListSPGMY]", "33")).ToList();
            return PartialView("_PartialGMYSiparisOnay", KOD);
        }

        public ActionResult GMOnay()
        {

            return View();
        }
        public PartialViewResult PartialGMOnay()
        {
            List<SMSiparisOnaySelect> KOD = db.Database.SqlQuery<SMSiparisOnaySelect>(string.Format("[FINSAT6{0}].[dbo].[SiparisOnayListGM]", "33")).ToList();
            return PartialView("_PartialGMSiparisOnay", KOD);
        }

    }
}