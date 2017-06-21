using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Wms12m.Entity;

namespace Wms12m.Presentation.Areas.Approvals.Controllers
{
    public class RiskController : RootController
    {
        public ActionResult SM()
        {
            if (CheckPerm(Perms.RiskOnaylama, PermTypes.Reading) == false) return Redirect("/");
            return View();
        }
        public PartialViewResult SM_List()
        {
            if (CheckPerm(Perms.RiskOnaylama, PermTypes.Reading) == false) return null;
            return PartialView();
        }
        public string OnayCekSM()
        {
            if (CheckPerm(Perms.RiskOnaylama, PermTypes.Reading) == false) return null;
            var RT = db.Database.SqlQuery<RiskTanim>(string.Format("SELECT *   FROM [FINSAT6{0}].[FINSAT6{0}].[RiskTanim]   where OnayTip = 0 and SMOnay = 0 and Durum = 0", "17")).ToList();
            var json = new JavaScriptSerializer().Serialize(RT);
            return json;
        }
        public JsonResult Onay_SM(string Data)
        {
            Result _Result = new Result(true);
            if (CheckPerm(Perms.RiskOnaylama, PermTypes.Writing) == false) return null;
            JArray parameters = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JArray>(Request["Data"]);
            SqlExper sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, "17");

            try
            {

                foreach (JObject insertObj in parameters)
                {
                    DateTime date = DateTime.Now;
                    var shortDate = date.ToString("yyyy-MM-dd");
                    var sonuc = sqlexper.AcceptChanges();
                    db.Database.ExecuteSqlCommand(string.Format("UPDATE [FINSAT6{0}].[FINSAT6{0}].[RiskTanim] SET SMOnay = 1, SMOnaylayan='" + vUser.UserName + "', SMOnayTarih='{2}'  where ID = '{1}'", "17", insertObj["ID"].ToString(), shortDate));
                }

                _Result.Status = true;
                _Result.Message = "İşlem Başarılı ";

            }
            catch (Exception)
            {

                _Result.Status = false;
                _Result.Message = "Hata Oluştu. ";

            }
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Red_SM(string Data)
        {
            Result _Result = new Result(true);
            if (CheckPerm(Perms.RiskOnaylama, PermTypes.Writing) == false) return null;
            JArray parameters = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JArray>(Request["Data"]);
            SqlExper sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, "17");

            try
            {

                foreach (JObject insertObj in parameters)
                {
                    DateTime date = DateTime.Now;
                    var shortDate = date.ToString("yyyy-MM-dd");
                    var sonuc = sqlexper.AcceptChanges();
                    db.Database.ExecuteSqlCommand(string.Format("DELETE FROM [FINSAT6{0}].[FINSAT6{0}].[RiskTanim] where ID = '{1}'", "17", insertObj["ID"].ToString()));

                }

                _Result.Status = true;
                _Result.Message = "İşlem Başarılı ";

            }
            catch (Exception)
            {

                _Result.Status = false;
                _Result.Message = "Hata Oluştu. ";

            }
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GM()
        {
            if (CheckPerm(Perms.RiskOnaylama, PermTypes.Reading) == false) return Redirect("/");
            return View();
        }
        public PartialViewResult GM_List()
        {
            if (CheckPerm(Perms.RiskOnaylama, PermTypes.Reading) == false) return null;
            return PartialView();
        }
        public string OnayCekGM()
        {
            if (CheckPerm(Perms.RiskOnaylama, PermTypes.Reading) == false) return null;
            var RT = db.Database.SqlQuery<RiskTanim>(string.Format("SELECT *   FROM [FINSAT6{0}].[FINSAT6{0}].[RiskTanim]  where (OnayTip = 3 and SPGMYOnay = 1 and MIGMYOnay = 1 and GMOnay=0) or (OnayTip = 4 and GMOnay = 0 ) and Durum =0", "17")).ToList();
            var json = new JavaScriptSerializer().Serialize(RT);
            return json;
        }
        public JsonResult Onay_GM(string Data)
        {
            Result _Result = new Result(true);
            if (CheckPerm(Perms.RiskOnaylama, PermTypes.Writing) == false) return null;
            JArray parameters = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JArray>(Request["Data"]);
            SqlExper sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, "17");

            try
            {

                foreach (JObject insertObj in parameters)
                {
                    DateTime date = DateTime.Now;
                    var shortDate = date.ToString("yyyy-MM-dd");
                    var sonuc = sqlexper.AcceptChanges();
                    db.Database.ExecuteSqlCommand(string.Format("UPDATE [FINSAT6{0}].[FINSAT6{0}].[RiskTanim] SET GMOnay = 1, GMOnaylayan='" + vUser.UserName + "', GMOnayTarih='{2}'  where ID = '{1}'", "17", insertObj["ID"].ToString(), shortDate));
                }

                _Result.Status = true;
                _Result.Message = "İşlem Başarılı ";

            }
            catch (Exception)
            {

                _Result.Status = false;
                _Result.Message = "Hata Oluştu. ";

            }
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Red_GM(string Data)
        {
            Result _Result = new Result(true);
            if (CheckPerm(Perms.RiskOnaylama, PermTypes.Writing) == false) return null;
            JArray parameters = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JArray>(Request["Data"]);
            SqlExper sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, "17");

            try
            {

                foreach (JObject insertObj in parameters)
                {
                    DateTime date = DateTime.Now;
                    var shortDate = date.ToString("yyyy-MM-dd");
                    var sonuc = sqlexper.AcceptChanges();
                    db.Database.ExecuteSqlCommand(string.Format("DELETE FROM [FINSAT6{0}].[FINSAT6{0}].[RiskTanim] where ID = '{1}'", "17", insertObj["ID"].ToString()));

                }
                _Result.Status = true;
                _Result.Message = "İşlem Başarılı ";

            }
            catch (Exception)
            {

                _Result.Status = false;
                _Result.Message = "Hata Oluştu. ";

            }
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SPGMY()
        {
            if (CheckPerm(Perms.RiskOnaylama, PermTypes.Reading) == false) return Redirect("/");
            return View();
        }
        public PartialViewResult SPGMY_List()
        {
            if (CheckPerm(Perms.RiskOnaylama, PermTypes.Reading) == false) return null;
            return PartialView();
        }
        public string OnayCekSPGMY()
        {
            if (CheckPerm(Perms.RiskOnaylama, PermTypes.Reading) == false) return null;
            var RT = db.Database.SqlQuery<RiskTanim>(string.Format("SELECT *   FROM [FINSAT6{0}].[FINSAT6{0}].[RiskTanim]   where (OnayTip = 1 and SPGMYOnay =0) OR (OnayTip = 2 and SPGMYOnay =0) OR (OnayTip = 3 and SPGMYOnay = 0) and Durum = 0", "17")).ToList();
            var json = new JavaScriptSerializer().Serialize(RT);
            return json;
        }
        public JsonResult Onay_SPGMY(string Data)
        {
            Result _Result = new Result(true);
            if (CheckPerm(Perms.RiskOnaylama, PermTypes.Writing) == false) return null;
            JArray parameters = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JArray>(Request["Data"]);
            SqlExper sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, "17");

            try
            {

                foreach (JObject insertObj in parameters)
                {
                    DateTime date = DateTime.Now;
                    var shortDate = date.ToString("yyyy-MM-dd");
                    var sonuc = sqlexper.AcceptChanges();
                    db.Database.ExecuteSqlCommand(string.Format("UPDATE [FINSAT6{0}].[FINSAT6{0}].[RiskTanim] SET SPGMYOnay = 1, SPGMYOnaylayan='" + vUser.UserName + "', SPGMYOnayTarih='{2}'  where ID = '{1}'", "17", insertObj["ID"].ToString(), shortDate));

                }

                _Result.Status = true;
                _Result.Message = "İşlem Başarılı ";

            }
            catch (Exception)
            {

                _Result.Status = false;
                _Result.Message = "Hata Oluştu. ";

            }
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Red_SPGMY(string Data)
        {
            Result _Result = new Result(true);
            if (CheckPerm(Perms.RiskOnaylama, PermTypes.Writing) == false) return null;
            JArray parameters = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JArray>(Request["Data"]);
            SqlExper sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, "17");

            try
            {

                foreach (JObject insertObj in parameters)
                {
                    DateTime date = DateTime.Now;
                    var shortDate = date.ToString("yyyy-MM-dd");
                    var sonuc = sqlexper.AcceptChanges();
                    db.Database.ExecuteSqlCommand(string.Format("DELETE FROM [FINSAT6{0}].[FINSAT6{0}].[RiskTanim] where ID = '{1}'", "17", insertObj["ID"].ToString()));

                }

                _Result.Status = true;
                _Result.Message = "İşlem Başarılı ";

            }
            catch (Exception)
            {

                _Result.Status = false;
                _Result.Message = "Hata Oluştu. ";

            }
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }


        public ActionResult MIGMY()
        {
            if (CheckPerm(Perms.RiskOnaylama, PermTypes.Reading) == false) return Redirect("/");
            return View();
        }
        public PartialViewResult MIGMY_List()
        {
            if (CheckPerm(Perms.RiskOnaylama, PermTypes.Reading) == false) return null;
            return PartialView();
        }
        public string OnayCekMIGMY()
        {
            if (CheckPerm(Perms.RiskOnaylama, PermTypes.Reading) == false) return null;
            var RT = db.Database.SqlQuery<RiskTanim>(string.Format("SELECT *   FROM [FINSAT6{0}].[FINSAT6{0}].[RiskTanim]  where (OnayTip = 2 and SPGMYOnay = 1 and MIGMYOnay = 0) OR (OnayTip = 3 and SPGMYOnay = 1 and MIGMYOnay = 0) and Durum = 0", "17")).ToList();
            var json = new JavaScriptSerializer().Serialize(RT);
            return json;
        }

        public JsonResult Onay_MIGMY(string Data)
        {
            Result _Result = new Result(true);
            if (CheckPerm(Perms.RiskOnaylama, PermTypes.Writing) == false) return null;
            JArray parameters = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JArray>(Request["Data"]);
            SqlExper sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, "17");

            try
            {

                foreach (JObject insertObj in parameters)
                {
                    DateTime date = DateTime.Now;
                    var shortDate = date.ToString("yyyy-MM-dd");
                    var sonuc = sqlexper.AcceptChanges();
                    db.Database.ExecuteSqlCommand(string.Format("UPDATE [FINSAT6{0}].[FINSAT6{0}].[RiskTanim] SET MIGMYOnay = 1, MIGMYOnaylayan='" + vUser.UserName + "', MIGMYOnayTarih='{2}'  where ID = '{1}'", "17", insertObj["ID"].ToString(), shortDate));

                }

                _Result.Status = true;
                _Result.Message = "İşlem Başarılı ";

            }
            catch (Exception)
            {

                _Result.Status = false;
                _Result.Message = "Hata Oluştu. ";

            }
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Red_MIGMY(string Data)
        {
            Result _Result = new Result(true);
            if (CheckPerm(Perms.RiskOnaylama, PermTypes.Writing) == false) return null;
            JArray parameters = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JArray>(Request["Data"]);
            SqlExper sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, "17");

            try
            {

                foreach (JObject insertObj in parameters)
                {
                    DateTime date = DateTime.Now;
                    var shortDate = date.ToString("yyyy-MM-dd");
                    var sonuc = sqlexper.AcceptChanges();
                    db.Database.ExecuteSqlCommand(string.Format("DELETE FROM [FINSAT6{0}].[FINSAT6{0}].[RiskTanim] where ID = '{1}'", "17", insertObj["ID"].ToString()));

                }

                _Result.Status = true;
                _Result.Message = "İşlem Başarılı ";

            }
            catch (Exception)
            {

                _Result.Status = false;
                _Result.Message = "Hata Oluştu. ";

            }
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Tanim()
        {
            if (CheckPerm(Perms.RiskOnaylama, PermTypes.Reading) == false) return Redirect("/");
            return View();
        }
        public PartialViewResult TanimPartial()
        {
            if (CheckPerm(Perms.RiskOnaylama, PermTypes.Reading) == false) return null;
            return PartialView();
        }
        public string CHKSelect()
        {
            if (CheckPerm(Perms.RiskOnaylama, PermTypes.Reading) == false) return null;
            var RT = db.Database.SqlQuery<RiskTanimToplu>(string.Format("[FINSAT6{0}].[wms].[CHKSelect2]", "17")).ToList();
            var json = new JavaScriptSerializer().Serialize(RT);
            return json;
        }
        public string OnayRiskInsert(string Data)
        {
            if (CheckPerm(Perms.RiskOnaylama, PermTypes.Writing) == false) return null;
            JArray parameters = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JArray>(Request["Data"]);
            SqlExper sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, "17");
            try
            {
                string sonucMessage = "OK";
                foreach (JObject insertObj in parameters)
                {
                    if (insertObj["YeniSahsiCekLimiti"].ToDecimal() <= 0)
                    {
                        sonucMessage = "EKSIK";
                        continue;
                    }

                    RiskTanim rsk = new RiskTanim()
                    {
                        HesapKodu = insertObj["HesapKodu"].ToString(),
                        Unvan = insertObj["Unvan"].ToString(),
                        SahsiCekLimiti = insertObj["SahsiCekLimiti"].ToDecimal(),
                        MusteriCekLimiti = insertObj["MusteriCekLimiti"].ToDecimal(),
                        SMOnay = false,
                        SMOnaylayan = "",
                        SPGMYOnay = false,
                        SPGMYOnaylayan = "",
                        MIGMYOnay = false,
                        MIGMYOnaylayan = "",
                        GMOnay = false,
                        GMOnaylayan = "",
                        Durum = false
                    };
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
                    if (sonuc.Status == false)
                    {
                        sonucMessage = "NO";
                    }
                }
                return sonucMessage;
            }
            catch (Exception)
            {
                return "NO";
            }
        }
    }
}