using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Wms12m.Entity;

namespace Wms12m.Presentation.Controllers
{
    public class OnaySozlesmeController : RootController
    {
        public ActionResult GM()
        {

            return View();
        }
        [HttpPost]
        public PartialViewResult PartialGM()
        {
            List<SozlesmeOnaySelect> list = db.Database.SqlQuery<SozlesmeOnaySelect>(string.Format("[FINSAT6{0}].[wms].[SP_SozlesmeOnay]", "33")).ToList();
            return PartialView("_PartialGM", list);
        }

        [HttpPost]
        public PartialViewResult DetailsGM(string ListeNo)
        {
            var list = db.Database.SqlQuery<BaglantiDetaySelect1>(string.Format("[FINSAT6{0}].[wms].[BaglantiDetaySelect1] '{1}'", "33", ListeNo)).ToList();
            return PartialView("_DetailsGM", list);
        }
        //--------------------------------------------------------------------------------------------------------------
        public ActionResult SM()
        {

            return View();
        }
        public PartialViewResult PartialSM()
        {
            List<SozlesmeOnaySelect> list = db.Database.SqlQuery<SozlesmeOnaySelect>(string.Format("[FINSAT6{0}].[wms].[SP_SozlesmeOnaySM]", "33")).ToList();
            return PartialView("_PartialSM", list);
        }

        [HttpPost]
        public PartialViewResult DetailsSM(string ListeNo)
        {
            var list = db.Database.SqlQuery<BaglantiDetaySelect1>(string.Format("[FINSAT6{0}].[wms].[BaglantiDetaySelect1] '{1}'", "33", ListeNo)).ToList();
            return PartialView("_DetailsSM", list);
        }
        //---------------------------------------------------------------------------------------------------------------------
        public ActionResult SPGMY()
        {

            return View();
        }
        public PartialViewResult PartialSPGMY()
        {
            List<SozlesmeOnaySelect> list = db.Database.SqlQuery<SozlesmeOnaySelect>(string.Format("[FINSAT6{0}].[wms].[SP_SozlesmeOnaySPGMY]", "33")).ToList();
            return PartialView("_PartialSM", list);
        }

        [HttpPost]
        public PartialViewResult DetailsSPMY(string ListeNo)
        {
            var list = db.Database.SqlQuery<BaglantiDetaySelect1>(string.Format("[FINSAT6{0}].[wms].[BaglantiDetaySelect1] '{1}'", "33", ListeNo)).ToList();
            return PartialView("_DetailsSPGMY", list);
        }
    }
}