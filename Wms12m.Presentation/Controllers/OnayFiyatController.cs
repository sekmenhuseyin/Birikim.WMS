using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Wms12m.Entity;

namespace Wms12m.Presentation.Controllers
{
    public class OnayFiyatController : RootController
    {
        // GET: FiyatOnay
        public ActionResult FiyatOnayListGM()
        {
            return View();
        }

        public PartialViewResult PartialFiyatOnayListGM()
        {


            List<FiyatOnayGMSelect> KOD = db.Database.SqlQuery<FiyatOnayGMSelect>(string.Format("[FINSAT6{0}].[dbo].[FiyatOnayListGM]", "17")).ToList();
            return PartialView("_PartialFiyatOnayListGM", KOD);
        }

        public ActionResult FiyatOnayListSPGMY()
        {
            return View();
        }

        public PartialViewResult PartialFiyatOnayListSPGMY()
        {


            List<FiyatOnayGMSelect> KOD = db.Database.SqlQuery<FiyatOnayGMSelect>(string.Format("[FINSAT6{0}].[dbo].[FiyatOnayListSPGMY]", "17")).ToList();
            return PartialView("_PartialFiyatOnayListSPGMY", KOD);
        }

        public ActionResult FiyatOnayListSM()
        {
            return View();
        }

        public PartialViewResult PartialFiyatOnayListSM()
        {


            List<FiyatOnayGMSelect> KOD = db.Database.SqlQuery<FiyatOnayGMSelect>(string.Format("[FINSAT6{0}].[dbo].[FiyatOnayList]", "17")).ToList();
            return PartialView("_PartialFiyatOnayListSM", KOD);
        }
    }
}