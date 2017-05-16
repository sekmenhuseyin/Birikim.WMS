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
            var KOD = db.Database.SqlQuery<SMSiparisOnaySelect>(string.Format("[FINSAT6{0}].[wms].[SiparisOnayListSM]", "33")).ToList();
            return PartialView("_PartialSM", KOD);
        }

        public ActionResult SPGMY()
        {

            return View();
        }
        public PartialViewResult PartialSPGMY()
        {
            var KOD = db.Database.SqlQuery<SMSiparisOnaySelect>(string.Format("[FINSAT6{0}].[wms].[SiparisOnayListSPGMY]", "33")).ToList();
            return PartialView("_PartialSPGMY", KOD);
        }

        public ActionResult GM()
        {

            return View();
        }
        public PartialViewResult PartialGM()
        {
            var KOD = db.Database.SqlQuery<SMSiparisOnaySelect>(string.Format("[FINSAT6{0}].[wms].[SiparisOnayListGM]", "33")).ToList();
            return PartialView("_PartialGM", KOD);
        }

        public bool SiparisOnay(string EvrakNo, string Kaydeden, int OnayTip, bool OnaylandiMi)
        {

            bool Result = true;


            if (OnayTip == 3 && OnaylandiMi == true)//GMOnay
            { db.Database.ExecuteSqlCommand(string.Format("[FINSAT6{0}].[wms].[SP_SiparisOnay]", "33", EvrakNo, vUser.UserName, 3, 1)); }
            if (OnayTip == 2 && OnaylandiMi == true)//SPGMYOnay
            { db.Database.ExecuteSqlCommand(string.Format("[FINSAT6{0}].[wms].[SP_SiparisOnay]", "33", EvrakNo, vUser.UserName, 2, 1)); }
            if (OnayTip == 1 && OnaylandiMi == true)//SMOnay
            { db.Database.ExecuteSqlCommand(string.Format("[FINSAT6{0}].[wms].[SP_SiparisOnay]", "33", EvrakNo, vUser.UserName, 1, 1)); }

            return Result;
        }

    }
}