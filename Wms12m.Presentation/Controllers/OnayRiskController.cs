using System.Linq;
using System.Web.Mvc;
using Wms12m.Entity;

namespace Wms12m.Presentation.Controllers
{
    public class OnayRiskController : RootController
    {

        public ActionResult SM()
        {

            return View();
        }
        public PartialViewResult PartialSM()
        {
            var KOD = db.Database.SqlQuery<RiskTanim>(string.Format("SELECT *   FROM [FINSAT6{0}].[FINSAT6{0}].[RiskTanim]   where OnayTip = 0", "33")).ToList();//-- and SMOnay = 1 and Durum = 0 eklenecek.
            return PartialView("_PartialSM", KOD);
        }

        public ActionResult GM()
        {

            return View();
        }
        public PartialViewResult PartialGM()
        {
            var KOD = db.Database.SqlQuery<RiskTanim>(string.Format("SELECT *   FROM [FINSAT6{0}].[FINSAT6{0}].[RiskTanim]", "33")).ToList();//--where (OnayTip = 3 and SPGMYOnay = 1 and MIGMYOnay = 1 and GMOnay=0) or (OnayTip = 4 and GMOnay = 0 ) and Durum =0
            return PartialView("_PartialGM", KOD);
        }

        public ActionResult SPGMY()
        {

            return View();
        }
        public PartialViewResult PartialSPGMY()
        {
            var KOD = db.Database.SqlQuery<RiskTanim>(string.Format("SELECT *   FROM [FINSAT6{0}].[FINSAT6{0}].[RiskTanim] where SPGMYOnay =0", "33")).ToList();//--where (OnayTip = 1 and SPGMYOnay =0) OR (OnayTip = 2 and SPGMYOnay =0) OR (OnayTip = 3 and SPGMYOnay = 0) and Durum = 0
            return PartialView("_PartialSPGMY", KOD);
        }

        public ActionResult MIGMY()
        {

            return View();
        }
        public PartialViewResult PartialMIGMY()
        {
            var KOD = db.Database.SqlQuery<RiskTanim>(string.Format("SELECT *   FROM [FINSAT6{0}].[FINSAT6{0}].[RiskTanim] where SPGMYOnay = 1", "33")).ToList();//--where (OnayTip = 2 and SPGMYOnay = 1 and MIGMYOnay = 0) OR (OnayTip = 3 and SPGMYOnay = 1 and MIGMYOnay = 0) and Durum = 0
            return PartialView("_PartialMIGMY", KOD);
        }

        public ActionResult RiskTanim()
        {
            return View();
        }

        public PartialViewResult PartialRiskTanim()
        {
            var RT = db.Database.SqlQuery<RiskTanimToplu>(string.Format("[FINSAT6{0}].[dbo].[CHKSelect2]", "17")).ToList();
            return PartialView("_PartialRiskTanim", RT);
        }
    }
}