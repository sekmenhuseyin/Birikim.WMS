﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Wms12m.Entity;

namespace Wms12m.Presentation.Controllers
{
    public class RiskOnayController : RootController
    {

        public ActionResult SMOnay()
        {

            return View();
        }
        public PartialViewResult PartialSMOnay()
        {
            List<RiskTanim> KOD = db.Database.SqlQuery<RiskTanim>(string.Format("SELECT *   FROM [FINSAT6{0}].[FINSAT6{0}].[RiskTanim]   where OnayTip = 0", "17")).ToList();//-- and SMOnay = 1 and Durum = 0 eklenecek.
            return PartialView("_PartialSMRiskOnay", KOD);
        }

        public ActionResult GMOnay()
        {

            return View();
        }
        public PartialViewResult PartialGMOnay()
        {
            List<RiskTanim> KOD = db.Database.SqlQuery<RiskTanim>(string.Format("SELECT *   FROM [FINSAT6{0}].[FINSAT6{0}].[RiskTanim]", "17")).ToList();//--where (OnayTip = 3 and SPGMYOnay = 1 and MIGMYOnay = 1 and GMOnay=0) or (OnayTip = 4 and GMOnay = 0 ) and Durum =0
            return PartialView("_PartialGMRiskOnay", KOD);
        }

        public ActionResult SPGMYOnay()
        {

            return View();
        }
        public PartialViewResult PartialSPGMYOnay()
        {
            List<RiskTanim> KOD = db.Database.SqlQuery<RiskTanim>(string.Format("SELECT *   FROM [FINSAT6{0}].[FINSAT6{0}].[RiskTanim] where SPGMYOnay =0", "17")).ToList();//--where (OnayTip = 1 and SPGMYOnay =0) OR (OnayTip = 2 and SPGMYOnay =0) OR (OnayTip = 3 and SPGMYOnay = 0) and Durum = 0
            return PartialView("_PartialSPGMYRiskOnay", KOD);
        }

        public ActionResult MIGMYOnay()
        {

            return View();
        }
        public PartialViewResult PartialMIGMYOnay()
        {
            List<RiskTanim> KOD = db.Database.SqlQuery<RiskTanim>(string.Format("SELECT *   FROM [FINSAT6{0}].[FINSAT6{0}].[RiskTanim] where SPGMYOnay = 1", "17")).ToList();//--where (OnayTip = 2 and SPGMYOnay = 1 and MIGMYOnay = 0) OR (OnayTip = 3 and SPGMYOnay = 1 and MIGMYOnay = 0) and Durum = 0
            return PartialView("_PartialMIGMYRiskOnay", KOD);
        }
    }
}