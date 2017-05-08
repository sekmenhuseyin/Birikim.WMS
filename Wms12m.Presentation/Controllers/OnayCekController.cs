using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Wms12m.Entity;

namespace Wms12m.Presentation.Controllers
{
    public class OnayCekController : RootController
    {
        public ActionResult CekOnaySPGMY()
        {

            return View();
        }
        public PartialViewResult PartialCekOnaySPGMY()
        {
            List<CekOnaySelect> KOD = db.Database.SqlQuery<CekOnaySelect>(string.Format("[FINSAT6{0}].[dbo].[CekOnaySPGMY]", "17")).ToList();
            return PartialView("_PartialCekOnaySPGMY", KOD);
        }

        public ActionResult CekOnayMIGMY()
        {

            return View();
        }
        public PartialViewResult PartialCekOnayMIGMY()
        {
            List<CekOnaySelect> KOD = db.Database.SqlQuery<CekOnaySelect>(string.Format("[FINSAT6{0}].[dbo].[CekOnayMIGMY]", "17")).ToList();
            return PartialView("_PartialCekOnayMIGMY", KOD);
        }

        public ActionResult CekOnayGM()
        {

            return View();
        }
        public PartialViewResult PartialCekOnayGM()
        {
            List<CekOnaySelect> KOD = db.Database.SqlQuery<CekOnaySelect>(string.Format("[FINSAT6{0}].[dbo].[CekOnayGM]", "17")).ToList();
            return PartialView("_PartialCekOnayGM", KOD);
        }
    }
}