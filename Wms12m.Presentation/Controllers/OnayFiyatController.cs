using System.Linq;
using System.Web.Mvc;
using Wms12m.Entity;

namespace Wms12m.Presentation.Controllers
{
    public class OnayFiyatController : RootController
    {
        // GET: FiyatOnay
        public ActionResult GM()
        {
            return View();
        }

        public PartialViewResult PartialGM()
        {


            var KOD = db.Database.SqlQuery<FiyatOnayGMSelect>(string.Format("[FINSAT6{0}].[dbo].[FiyatOnayListGM]", "01")).ToList();
            return PartialView("_PartialGM", KOD);
        }

        public ActionResult SPGMY()
        {
            return View();
        }

        public PartialViewResult PartialSPGMY()
        {


            var KOD = db.Database.SqlQuery<FiyatOnayGMSelect>(string.Format("[FINSAT6{0}].[dbo].[FiyatOnayListSPGMY]", "01")).ToList();
            return PartialView("_PartialSPGMY", KOD);
        }

        public ActionResult SM()
        {
            return View();
        }

        public PartialViewResult PartialSM()
        {


            var KOD = db.Database.SqlQuery<FiyatOnayGMSelect>(string.Format("[FINSAT6{0}].[dbo].[FiyatOnayList]", "01")).ToList();
            return PartialView("_PartialSM", KOD);
        }
    }
}