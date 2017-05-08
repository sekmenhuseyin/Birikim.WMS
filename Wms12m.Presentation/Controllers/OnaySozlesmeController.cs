using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Wms12m.Entity;

namespace Wms12m.Presentation.Controllers
{
    public class OnaySozlesmeController : RootController
    {
        public ActionResult SozlesmeOnayGM()
        {

            return View();
        }
        public PartialViewResult ListGM()
        {
            List<SozlesmeOnaySelect> list = db.Database.SqlQuery<SozlesmeOnaySelect>(string.Format("[FINSAT6{0}].[dbo].[SP_SozlesmeOnay]", "17")).ToList();
            return PartialView("ListGM", list);
        }

        [HttpPost]
        public PartialViewResult Details(string ListeNo)
        {
            // var list = db.GetIrsDetayfromGorev(ID);
            var list = db.Database.SqlQuery<BaglantiDetaySelect1>(string.Format("[FINSAT6{0}].[dbo].[BaglantiDetaySelect1] '{1}'", "17", ListeNo)).ToList();
            return PartialView("Details", list);
        }
        //--------------------------------------------------------------------------------------------------------------
        public ActionResult SozlesmeOnaySM()
        {

            return View();
        }
        public PartialViewResult ListSM()
        {
            List<SozlesmeOnaySelect> list = db.Database.SqlQuery<SozlesmeOnaySelect>(string.Format("[FINSAT6{0}].[dbo].[SP_SozlesmeOnaySM]", "17")).ToList();
            return PartialView("ListSM", list);
        }

        [HttpPost]
        public PartialViewResult DetailsSM(string ListeNo)
        {
            // var list = db.GetIrsDetayfromGorev(ID);
            var list = db.Database.SqlQuery<BaglantiDetaySelect1>(string.Format("[FINSAT6{0}].[dbo].[BaglantiDetaySelect1] '{1}'", "17", ListeNo)).ToList();
            return PartialView("DetailsSM", list);
        }
        //---------------------------------------------------------------------------------------------------------------------
        public ActionResult SozlesmeOnaySPGMY()
        {

            return View();
        }
        public PartialViewResult ListSPGMY()
        {
            List<SozlesmeOnaySelect> list = db.Database.SqlQuery<SozlesmeOnaySelect>(string.Format("[FINSAT6{0}].[dbo].[SP_SozlesmeOnaySPGMY]", "17")).ToList();
            return PartialView("ListSM", list);
        }

        [HttpPost]
        public PartialViewResult DetailsSPMY(string ListeNo)
        {
            // var list = db.GetIrsDetayfromGorev(ID);
            var list = db.Database.SqlQuery<BaglantiDetaySelect1>(string.Format("[FINSAT6{0}].[dbo].[BaglantiDetaySelect1] '{1}'", "17", ListeNo)).ToList();
            return PartialView("DetailsSPGMY", list);
        }
    }
}