using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
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

        public string GGGG()
        {
            var RT = db.Database.SqlQuery<RiskTanimToplu>(string.Format("[FINSAT6{0}].[dbo].[CHKSelect2]", "17")).ToList();
            var json = new JavaScriptSerializer().Serialize(RT);
            return json;

        }

        public string OnayRiskInsert(string Data)
        {
            //string sql = string.Format(@"INSERT INTO FINSAT";
            JArray parameters = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JArray>(Request["Data"]);
            //JValue parameters = JsonConvert.<Newtonsoft.Json.Linq.JValue>(JsonConvert.SerializeObject(Data));
            SqlExper sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, "17");
            try
            {
                foreach (JObject insertObj in parameters)
                {
                    if (insertObj["YeniSahsiCekLimiti"].ToDecimal() <= 0)
                        continue;

                    RiskTanim rsk = new RiskTanim();

                    rsk.HesapKodu = insertObj["HesapKodu"].ToString();
                    rsk.Unvan = insertObj["Unvan"].ToString();
                    rsk.SahsiCekLimiti = insertObj["SahsiCekLimiti"].ToDecimal();
                    rsk.MusteriCekLimiti = insertObj["MusteriCekLimiti"].ToDecimal();
                    rsk.SMOnay = false;
                    rsk.SMOnaylayan = "";
                    rsk.SPGMYOnay = false;
                    rsk.SPGMYOnaylayan = "";
                    rsk.MIGMYOnay = false;
                    rsk.MIGMYOnaylayan = "";
                    rsk.GMOnay = false;
                    rsk.GMOnaylayan = "";
                    rsk.Durum = false;

                    if (Convert.ToDecimal(insertObj["YeniSahsiCekLimiti"]) < 20000)
                    {
                        rsk.OnayTip = 0;
                    }
                    else if (Convert.ToDecimal(insertObj["YeniSahsiCekLimiti"]) < 100000)
                    {
                        rsk.OnayTip = 1;
                    }
                    else if (Convert.ToDecimal(insertObj["YeniSahsiCekLimiti"]) < 200000)
                    {
                        rsk.OnayTip = 2;
                    }
                    else if (Convert.ToDecimal(insertObj["YeniSahsiCekLimiti"]) < 500000)
                    {
                        rsk.OnayTip = 3;
                    }
                    else if (Convert.ToDecimal(insertObj["YeniSahsiCekLimiti"]) >= 500000)
                    {
                        rsk.OnayTip = 4;
                    }
                    else
                    {
                        rsk.OnayTip = -1;
                    }

                    sqlexper.Insert(rsk);
                    var sonuc = sqlexper.AcceptChanges();

                }
                return "OK";
            }
            catch (Exception ex)
            {

                return "NO";
            }
            
        }
    }
}