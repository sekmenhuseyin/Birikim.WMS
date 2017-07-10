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
    public class CheckController : RootController
    {
        #region SPGMY
        public ActionResult SPGMY()
        {
            if (CheckPerm(Perms.ÇekOnaylama, PermTypes.Reading) == false) return Redirect("/");
            return View();
        }
        public string SPGMY_List()
        {
            if (CheckPerm(Perms.ÇekOnaylama, PermTypes.Reading) == false) return null;
            var RT = db.Database.SqlQuery<CekOnaySelect>(string.Format("[FINSAT6{0}].[wms].[CekOnaySPGMY]", "17")).ToList();
            var json = new JavaScriptSerializer().Serialize(RT);
            return json;
        }
        public JsonResult Onay_MIGMY(string Data)
        {
            Result _Result = new Result(true);
            if (CheckPerm(Perms.ÇekOnaylama, PermTypes.Writing) == false) return null;

            JArray parameters = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JArray>(Request["Data"]);

            SqlExper sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, "17");

            try
            {
                Dictionary<string, int> FiyatMaxSiraNo = new Dictionary<string, int>();
                foreach (JObject insertObj in parameters)
                {

                    DateTime date = DateTime.Now;
                    var shortDate = date.ToString("yyyy-MM-dd HH:mm:ss");
                    var sonuc = sqlexper.AcceptChanges();
                    db.Database.ExecuteSqlCommand(string.Format("UPDATE [FINSAT6{0}].[FINSAT6{0}].[CEK] SET MIGMYOnay = 1, MIGMYOnaylayan='" + vUser.UserName + "', MIGMYOnayTarih='{2}'  where ID = '{1}'", "17", insertObj["ID"].ToString(), shortDate));




                }


                _Result.Status = true;
                _Result.Message = "İşlem Başarılı ";

            }
            catch (Exception ex)
            {

                _Result.Status = false;
                _Result.Message = "Hata Oluştu. ";

            }
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Red_MIGMY(string Data)
        {
            Result _Result = new Result(true);
            if (CheckPerm(Perms.ÇekOnaylama, PermTypes.Writing) == false) return null;

            JArray parameters = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JArray>(Request["Data"]);

            SqlExper sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, "17");

            try
            {
                Dictionary<string, int> FiyatMaxSiraNo = new Dictionary<string, int>();
                foreach (JObject insertObj in parameters)
                {

                    DateTime date = DateTime.Now;
                    var shortDate = date.ToString("yyyy-MM-dd HH:mm:ss");
                    var sonuc = sqlexper.AcceptChanges();
                    db.Database.ExecuteSqlCommand(string.Format("UPDATE [FINSAT6{0}].[FINSAT6{0}].[CEK] SET MIGMYOnay = 0, MIGMYOnaylayan='" + vUser.UserName + "', MIGMYOnayTarih='{2}', Durum = 1 where ID = '{1}'", "17", insertObj["ID"].ToString(), shortDate));




                }


                _Result.Status = true;
                _Result.Message = "İşlem Başarılı ";

            }
            catch (Exception ex)
            {

                _Result.Status = false;
                _Result.Message = "Hata Oluştu. ";

            }
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region MIGMY
        public ActionResult MIGMY()
        {
            if (CheckPerm(Perms.ÇekOnaylama, PermTypes.Reading) == false) return Redirect("/");
            return View();
        }
        public string MIGMY_List()
        {
            if (CheckPerm(Perms.ÇekOnaylama, PermTypes.Reading) == false) return null;
            var RT = db.Database.SqlQuery<CekOnaySelect>(string.Format("[FINSAT6{0}].[wms].[CekOnayMIGMY]", "17")).ToList();
            var json = new JavaScriptSerializer().Serialize(RT);
            return json;
        }
        public JsonResult Onay_SPGMY(string Data)
        {
            Result _Result = new Result(true);
            if (CheckPerm(Perms.ÇekOnaylama, PermTypes.Writing) == false) return null;

            JArray parameters = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JArray>(Request["Data"]);

            SqlExper sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, "17");

            try
            {
                Dictionary<string, int> FiyatMaxSiraNo = new Dictionary<string, int>();
                foreach (JObject insertObj in parameters)
                {

                    DateTime date = DateTime.Now;
                    var shortDate = date.ToString("yyyy-MM-dd HH:mm:ss");
                    var sonuc = sqlexper.AcceptChanges();
                    db.Database.ExecuteSqlCommand(string.Format("UPDATE [FINSAT6{0}].[FINSAT6{0}].[CEK] SET SPGMYOnay = 1, SPGMYOnaylayan='" + vUser.UserName + "', SPGMYOnayTarih='{2}'  where ID = '{1}'", "17", insertObj["ID"].ToString(), shortDate));




                }


                _Result.Status = true;
                _Result.Message = "İşlem Başarılı ";

            }
            catch (Exception ex)
            {

                _Result.Status = false;
                _Result.Message = "Hata Oluştu. ";

            }
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Red_SPGMY(string Data)
        {
            Result _Result = new Result(true);
            if (CheckPerm(Perms.ÇekOnaylama, PermTypes.Writing) == false) return null;

            JArray parameters = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JArray>(Request["Data"]);

            SqlExper sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, "17");

            try
            {
                Dictionary<string, int> FiyatMaxSiraNo = new Dictionary<string, int>();
                foreach (JObject insertObj in parameters)
                {

                    DateTime date = DateTime.Now;
                    var shortDate = date.ToString("yyyy-MM-dd HH:mm:ss");
                    var sonuc = sqlexper.AcceptChanges();
                    db.Database.ExecuteSqlCommand(string.Format("UPDATE [FINSAT6{0}].[FINSAT6{0}].[CEK] SET SPGMYOnay = 0, SPGMYOnaylayan='" + vUser.UserName + "', SPGMYOnayTarih='{2}', Durum = 1 where ID = '{1}'", "17", insertObj["ID"].ToString(), shortDate));




                }


                _Result.Status = true;
                _Result.Message = "İşlem Başarılı ";

            }
            catch (Exception ex)
            {

                _Result.Status = false;
                _Result.Message = "Hata Oluştu. ";

            }
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region GM
        public ActionResult GM()
        {
            if (CheckPerm(Perms.ÇekOnaylama, PermTypes.Reading) == false) return Redirect("/");
            return View();
        }
        public string GM_List()
        {
            if (CheckPerm(Perms.ÇekOnaylama, PermTypes.Reading) == false) return null;
            var RT = db.Database.SqlQuery<CekOnaySelect>(string.Format("[FINSAT6{0}].[wms].[CekOnayGM]", "17")).ToList();
            var json = new JavaScriptSerializer().Serialize(RT);
            return json;
        }
        public JsonResult Onay_GM(string Data)
        {
            Result _Result = new Result(true);
            if (CheckPerm(Perms.ÇekOnaylama, PermTypes.Writing) == false) return null;

            JArray parameters = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JArray>(Request["Data"]);

            SqlExper sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, "17");

            try
            {
                Dictionary<string, int> FiyatMaxSiraNo = new Dictionary<string, int>();
                foreach (JObject insertObj in parameters)
                {

                    DateTime date = DateTime.Now;
                    var shortDate = date.ToString("yyyy-MM-dd HH:mm:ss");
                    var sonuc = sqlexper.AcceptChanges();
                    db.Database.ExecuteSqlCommand(string.Format("UPDATE [FINSAT6{0}].[FINSAT6{0}].[CEK] SET GMOnay = 1, GMOnaylayan='" + vUser.UserName + "', GMOnayTarih='{2}', Durum=1  where ID = '{1}'", "17", insertObj["ID"].ToString(), shortDate));




                }


                _Result.Status = true;
                _Result.Message = "İşlem Başarılı ";

            }
            catch (Exception ex)
            {

                _Result.Status = false;
                _Result.Message = "Hata Oluştu. ";

            }
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Red_GM(string Data)
        {
            Result _Result = new Result(true);
            if (CheckPerm(Perms.ÇekOnaylama, PermTypes.Writing) == false) return null;

            JArray parameters = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JArray>(Request["Data"]);

            SqlExper sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, "17");

            try
            {
                Dictionary<string, int> FiyatMaxSiraNo = new Dictionary<string, int>();
                foreach (JObject insertObj in parameters)
                {

                    DateTime date = DateTime.Now;
                    var shortDate = date.ToString("yyyy-MM-dd HH:mm:ss");
                    var sonuc = sqlexper.AcceptChanges();
                    db.Database.ExecuteSqlCommand(string.Format("UPDATE [FINSAT6{0}].[FINSAT6{0}].[CEK] SET GMOnay = 0, GMOnaylayan='" + vUser.UserName + "', GMOnayTarih='{2}', Durum = 1 where ID = '{1}'", "17", insertObj["ID"].ToString(), shortDate));




                }


                _Result.Status = true;
                _Result.Message = "İşlem Başarılı ";

            }
            catch (Exception ex)
            {

                _Result.Status = false;
                _Result.Message = "Hata Oluştu. ";

            }
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}