using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Wms12m.Entity;

namespace Wms12m.Presentation.Areas.Approvals.Controllers
{
    public class GuaranteeController : RootController
    {
        public ActionResult Index()
        {
            if (CheckPerm(Perms.TeminatOnay, PermTypes.Reading) == false) return Redirect("/");
            return View();
        }
        public string List()
        {
            if (CheckPerm(Perms.TeminatOnay, PermTypes.Reading) == false) return null;
            var RT = db.Database.SqlQuery<TeminatOnaySelect>(string.Format("[FINSAT6{0}].[wms].[TeminatOnayList]", "17")).ToList();
            var json = new JavaScriptSerializer().Serialize(RT);
            return json;
        }

        public JsonResult Onay(string Data)
        {
            Result _Result = new Result(true);
            if (CheckPerm(Perms.TeminatOnay, PermTypes.Writing) == false) return null;
            JArray parameters = JsonConvert.DeserializeObject<JArray>(Request["Data"]);
            SqlExper sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, "17");

            try
            {
                Dictionary<string, int> FiyatMaxSiraNo = new Dictionary<string, int>();
                foreach (JObject insertObj in parameters)
                {
                    DateTime date = DateTime.Now;
                    var shortDate = date.ToString("yyyy-MM-dd");
                    var sonuc = sqlexper.AcceptChanges();
                    db.Database.ExecuteSqlCommand(string.Format("[FINSAT6{0}].[wms].[TeminatOnayUpdate] @ID = {1}, @Kullanici = '{2}'", "17", insertObj["ID"].ToString(), vUser.UserName));

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
        public JsonResult Red(string Data)
        {
            Result _Result = new Result(true);
            if (CheckPerm(Perms.TeminatOnay, PermTypes.Writing) == false) return null;
            JArray parameters = JsonConvert.DeserializeObject<JArray>(Request["Data"]);
            SqlExper sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, "17");

            try
            {
                Dictionary<string, int> FiyatMaxSiraNo = new Dictionary<string, int>();
                foreach (JObject insertObj in parameters)
                {
                    DateTime date = DateTime.Now;
                    var shortDate = date.ToString("yyyy-MM-dd");
                    var sonuc = sqlexper.AcceptChanges();
                    db.Database.ExecuteSqlCommand(string.Format("DELETE FROM [FINSAT6{0}].[FINSAT6{0}].[Teminat] WHERE  ID = {1}", "17", insertObj["ID"].ToString()));
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
            if (CheckPerm(Perms.TeminatTanim, PermTypes.Reading) == false) return Redirect("/");
            var CHK = db.Database.SqlQuery<RaporCHKSelect>(string.Format("[FINSAT6{0}].[wms].[CHKSelect1]", "17")).ToList();
            return View(CHK);
        }
        public string Durbun()
        {
            var DRBN = db.Database.SqlQuery<RaporCHKSelect>(string.Format("[FINSAT6{0}].[wms].[TeminatDurbunSelect]", "17")).ToList();
            var json = new JavaScriptSerializer().Serialize(DRBN);
            return json;
        }

        public string Bekleyen()
        {
            var BKLYN = db.Database.SqlQuery<Teminat>(string.Format("[FINSAT6{0}].[wms].[TeminatOnayList]", "17")).ToList();
            var json = new JavaScriptSerializer().Serialize(BKLYN);
            return json;
        }


        public PartialViewResult TanimList(string chk)
        {
            if (CheckPerm(Perms.TeminatTanim, PermTypes.Reading) == false) return null;
            ViewBag.CHK = chk;
            return PartialView("TanimList");
        }

        public string Select(string chk)
        {
            var TMNT = db.Database.SqlQuery<Teminat>(string.Format("[FINSAT6{0}].[wms].[TeminatOnaySelect] @HesapKodu='{1}'", "17", chk)).ToList();
            var json = new JavaScriptSerializer().Serialize(TMNT);
            return json;
        }

        public string Sil(int ID)
        {
            if (CheckPerm(Perms.TeminatTanim, PermTypes.Deleting) == false) return null;
            var sonuc = db.Database.SqlQuery<int>(string.Format("[FINSAT6{0}].[wms].[TeminatSil] @ID={1}", "17", ID)).FirstOrDefault();
            if (sonuc == 1)
            {
                return "OK";
            }
            else
            {
                return "NO";
            }

        }

        public string TeminatTanimInsert(string Data)
        {
            if (CheckPerm(Perms.TeminatTanim, PermTypes.Writing) == false) return null;
            JObject parameters = JsonConvert.DeserializeObject<JObject>(Request["Data"]);
            SqlExper sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, "17");
            try
            {
                Teminat tmnt = new Teminat()
                {
                    HesapKodu = parameters["HesapKodu"].ToString(),
                    Cins = parameters["TeminatCinsi"].ToString(),
                    Tutar = Convert.ToDecimal(parameters["TeminatTutari"].ToString()),
                    Tarih = Convert.ToDateTime(parameters["Tarih"]),
                    AltBayi = parameters["AltBayi"].ToString(),
                    Unvan = parameters["Unvan"].ToString(),
                    SureliSuresiz = parameters["SureliSuresiz"].ToBool()
                };
                if (parameters["VadeTarihi"] != null && parameters["VadeTarihi"].ToString() != "")
                {
                    tmnt.VadeTarih = Convert.ToDateTime(parameters["VadeTarihi"]);
                }

                sqlexper.Insert(tmnt);
                var sonuc = sqlexper.AcceptChanges();
                if (sonuc.Status == true)
                {
                    return "OK";
                }
                else
                {
                    return "NO";
                }

            }

            catch (Exception ex)
            {
                return "NO";
            }
        }
    }
}