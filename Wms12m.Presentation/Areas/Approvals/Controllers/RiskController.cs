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
            if (CheckPerm(Perms.RiskOnaylamaSM, PermTypes.Reading) == false) return Redirect("/");
            return View();
        }
        public PartialViewResult SM_List()
        {
            if (CheckPerm(Perms.RiskOnaylamaSM, PermTypes.Reading) == false) return null;
            return PartialView();
        }
        public string OnayCekSM()
        {
            if (CheckPerm(Perms.RiskOnaylamaSM, PermTypes.Reading) == false) return null;
            var RT = db.Database.SqlQuery<RiskTanim>(string.Format("SELECT *   FROM [FINSAT6{0}].[FINSAT6{0}].[RiskTanim]   where OnayTip = 0 and SMOnay = 0 and Durum = 0", vUser.SirketKodu)).ToList();
            var json = new JavaScriptSerializer().Serialize(RT);
            return json;
        }
        public JsonResult Red_SM(string Data)
        {
            var _Result = new Result(true);
            if (CheckPerm(Perms.RiskOnaylamaSM, PermTypes.Writing) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            var parameters = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JArray>(Request["Data"]);
            var sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, vUser.SirketKodu);

            try
            {
                foreach (JObject insertObj in parameters)
                {
                    var date = DateTime.Now;
                    var shortDate = date.ToString("yyyy-MM-dd HH:mm:ss");
                    var sonuc = sqlexper.AcceptChanges();
                    db.Database.ExecuteSqlCommand(string.Format("DELETE FROM [FINSAT6{0}].[FINSAT6{0}].[RiskTanim] where ID = '{1}'", vUser.SirketKodu, insertObj["ID"].ToString()));
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
            if (CheckPerm(Perms.RiskOnaylamaGM, PermTypes.Reading) == false) return Redirect("/");
            return View();
        }
        public PartialViewResult GM_List()
        {
            if (CheckPerm(Perms.RiskOnaylamaGM, PermTypes.Reading) == false) return null;
            return PartialView();
        }
        public string OnayCekGM()
        {
            if (CheckPerm(Perms.RiskOnaylamaGM, PermTypes.Reading) == false) return null;
            var RT = db.Database.SqlQuery<RiskTanim>(string.Format("SELECT *   FROM [FINSAT6{0}].[FINSAT6{0}].[RiskTanim]  where (OnayTip = 3 and SPGMYOnay = 1 and MIGMYOnay = 1 and GMOnay=0) or (OnayTip = 4 and GMOnay = 0 ) and Durum =0", vUser.SirketKodu)).ToList();
            var json = new JavaScriptSerializer().Serialize(RT);
            return json;
        }
        public JsonResult Red_GM(string Data)
        {
            var _Result = new Result(true);
            if (CheckPerm(Perms.RiskOnaylamaGM, PermTypes.Writing) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            var parameters = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JArray>(Request["Data"]);
            var sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, vUser.SirketKodu);

            try
            {
                foreach (JObject insertObj in parameters)
                {
                    var date = DateTime.Now;
                    var shortDate = date.ToString("yyyy-MM-dd HH:mm:ss");
                    var sonuc = sqlexper.AcceptChanges();
                    db.Database.ExecuteSqlCommand(string.Format("DELETE FROM [FINSAT6{0}].[FINSAT6{0}].[RiskTanim] where ID = '{1}'", vUser.SirketKodu, insertObj["ID"].ToString()));
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
            if (CheckPerm(Perms.RiskOnaylamaSPGMY, PermTypes.Reading) == false) return Redirect("/");
            return View();
        }
        public PartialViewResult SPGMY_List()
        {
            if (CheckPerm(Perms.RiskOnaylamaSPGMY, PermTypes.Reading) == false) return null;
            return PartialView();
        }
        public string OnayCekSPGMY()
        {
            if (CheckPerm(Perms.RiskOnaylamaSPGMY, PermTypes.Reading) == false) return null;
            var RT = db.Database.SqlQuery<RiskTanim>(string.Format("SELECT *   FROM [FINSAT6{0}].[FINSAT6{0}].[RiskTanim]   where (OnayTip = 1 and SPGMYOnay =0) OR (OnayTip = 2 and SPGMYOnay =0) OR (OnayTip = 3 and SPGMYOnay = 0) and Durum = 0", vUser.SirketKodu)).ToList();
            var json = new JavaScriptSerializer().Serialize(RT);
            return json;
        }
        public JsonResult Red_SPGMY(string Data)
        {
            var _Result = new Result(true);
            if (CheckPerm(Perms.RiskOnaylamaSPGMY, PermTypes.Writing) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            var parameters = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JArray>(Request["Data"]);
            var sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, vUser.SirketKodu);

            try
            {
                foreach (JObject insertObj in parameters)
                {
                    var date = DateTime.Now;
                    var shortDate = date.ToString("yyyy-MM-dd HH:mm:ss");
                    var sonuc = sqlexper.AcceptChanges();
                    db.Database.ExecuteSqlCommand(string.Format("DELETE FROM [FINSAT6{0}].[FINSAT6{0}].[RiskTanim] where ID = '{1}'", vUser.SirketKodu, insertObj["ID"].ToString()));
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
            if (CheckPerm(Perms.RiskOnaylamaMIGMY, PermTypes.Reading) == false) return Redirect("/");
            return View();
        }
        public PartialViewResult MIGMY_List()
        {
            if (CheckPerm(Perms.RiskOnaylamaMIGMY, PermTypes.Reading) == false) return null;
            return PartialView();
        }
        public string OnayCekMIGMY()
        {
            if (CheckPerm(Perms.RiskOnaylamaMIGMY, PermTypes.Reading) == false) return null;
            var RT = db.Database.SqlQuery<RiskTanim>(string.Format("SELECT *   FROM [FINSAT6{0}].[FINSAT6{0}].[RiskTanim]  where ((OnayTip = 2 and SPGMYOnay = 1 and MIGMYOnay = 0) OR (OnayTip = 3 and SPGMYOnay = 1 and MIGMYOnay = 0)) and Durum = 0", vUser.SirketKodu)).ToList();
            var json = new JavaScriptSerializer().Serialize(RT);
            return json;
        }
        public JsonResult Red_MIGMY(string Data)
        {
            var _Result = new Result(true);
            if (CheckPerm(Perms.RiskOnaylamaMIGMY, PermTypes.Writing) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            var parameters = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JArray>(Request["Data"]);
            var sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, vUser.SirketKodu);

            try
            {
                foreach (JObject insertObj in parameters)
                {
                    var date = DateTime.Now;
                    var shortDate = date.ToString("yyyy-MM-dd HH:mm:ss");
                    var sonuc = sqlexper.AcceptChanges();
                    db.Database.ExecuteSqlCommand(string.Format("DELETE FROM [FINSAT6{0}].[FINSAT6{0}].[RiskTanim] where ID = '{1}'", vUser.SirketKodu, insertObj["ID"].ToString()));
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
            if (CheckPerm(Perms.RiskTanim, PermTypes.Reading) == false) return Redirect("/");
            return View();
        }
        public PartialViewResult TanimPartial()
        {
            if (CheckPerm(Perms.RiskTanim, PermTypes.Reading) == false) return null;
            return PartialView();
        }
        public string CHKSelect()
        {
            if (CheckPerm(Perms.RiskTanim, PermTypes.Reading) == false) return null;
            var RT = db.Database.SqlQuery<RiskTanimToplu>(string.Format("[FINSAT6{0}].[wms].[CHKSelect2]", vUser.SirketKodu)).ToList();
            var json = new JavaScriptSerializer().Serialize(RT);
            return json;
        }
        public string OnayRiskInsert(string Data)
        {
            if (CheckPerm(Perms.RiskTanim, PermTypes.Writing) == false) return "NO";
            var parameters = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JArray>(Request["Data"]);
            var sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, vUser.SirketKodu);
            try
            {
                var sonucMessage = "OK";
                foreach (JObject insertObj in parameters)
                {
                    if (Convert.ToDecimal(insertObj["YeniSahsiCekLimiti"].ToString()) <= 0)
                    {
                        sonucMessage = "EKSIK";
                        continue;
                    }

                    var rsk = new RiskTanim
                    {
                        HesapKodu = insertObj["HesapKodu"].ToString(),
                        Unvan = insertObj["Unvan"].ToString(),
                        SahsiCekLimiti = insertObj["YeniSahsiCekLimiti"].ToDecimal(),
                        MusteriCekLimiti = insertObj["YeniMusteriCekLimiti"].ToDecimal(),
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
                    if (insertObj["YeniSahsiCekLimiti"].ToDecimal() < 20000)
                    {
                        rsk.OnayTip = 0;
                    }
                    else if (insertObj["YeniSahsiCekLimiti"].ToDecimal() < 100000)
                    {
                        rsk.OnayTip = 1;
                    }
                    else if (insertObj["YeniSahsiCekLimiti"].ToDecimal() < 200000)
                    {
                        rsk.OnayTip = 2;
                    }
                    else if (insertObj["YeniSahsiCekLimiti"].ToDecimal() < 500000)
                    {
                        rsk.OnayTip = 3;
                    }
                    else if (insertObj["YeniSahsiCekLimiti"].ToDecimal() >= 500000)
                    {
                        rsk.OnayTip = 4;
                    }
                    else
                    {
                        rsk.OnayTip = -1;
                    }

                    sqlexper.Insert(rsk);
                    var sonuc = sqlexper.AcceptChanges();
                    if (sonuc.Status == false) sonucMessage = "NO";
                }

                return sonucMessage;
            }
            catch (Exception)
            {
                return "NO";
            }
        }

        public JsonResult RiskOnay(string Data, int Tip)
        {
            var _Result = new Result(true);
            if (CheckPerm(Perms.RiskOnaylama, PermTypes.Writing) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            var parameters = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JArray>(Request["Data"]);
            var sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, vUser.SirketKodu);
            var CHK = "";
            var ID = 0;
            try
            {
                foreach (JObject insertObj in parameters)
                {
                    CHK = insertObj["HesapKodu"].ToString();
                    ID = insertObj["ID"].ToInt32();
                    if (Tip == 3)//GMOnay
                    { db.Database.ExecuteSqlCommand(string.Format("[FINSAT6{0}].[wms].[SP_RiskOnay] @CHK = '{1}',@Tip={2},@Kullanici = '{3}',@ID={4}", vUser.SirketKodu, CHK, Tip, vUser.UserName, ID)); }
                    else if (Tip == 2)//MIGMYOnay
                    { db.Database.ExecuteSqlCommand(string.Format("[FINSAT6{0}].[wms].[SP_RiskOnay] @CHK = '{1}',@Tip={2},@Kullanici = '{3}',@ID={4}", vUser.SirketKodu, CHK, Tip, vUser.UserName, ID)); }
                    else if (Tip == 1)//SPGMYOnay
                    { db.Database.ExecuteSqlCommand(string.Format("[FINSAT6{0}].[wms].[SP_RiskOnay] @CHK = '{1}',@Tip={2},@Kullanici = '{3}',@ID={4}", vUser.SirketKodu, CHK, Tip, vUser.UserName, ID)); }
                    else if (Tip == 0)//SMOnay
                    { db.Database.ExecuteSqlCommand(string.Format("[FINSAT6{0}].[wms].[SP_RiskOnay] @CHK = '{1}',@Tip={2},@Kullanici = '{3}',@ID={4}", vUser.SirketKodu, CHK, Tip, vUser.UserName, ID)); }
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
    }
}