﻿using Newtonsoft.Json;
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
    public class StockController : RootController
    {
        /// <summary>
        /// stok anasayfa
        /// </summary>
        public ActionResult Index()
        {
            if (CheckPerm("Stok Onaylama", PermTypes.Reading) == false) return Redirect("/");
            return View();
        }
        /// <summary>
        /// liste
        /// </summary>
        public PartialViewResult List()
        {
            if (CheckPerm("Stok Onaylama", PermTypes.Reading) == false) return null;
            return PartialView();
        }
        /// <summary>
        /// 
        /// </summary>
        public string OnayCek(string Durum)
        {
            if (CheckPerm("Stok Onaylama", PermTypes.Reading) == false) return null;
            int param = 1;
            if (Durum == "Tumu") { param = 0; }
            else if (Durum == "Onay") { param = 1; }
            else if (Durum == "Pasif") { param = 2; }
            else if (Durum == "Aktif") { param = 3; }
            else if (Durum == "Red") { param = 4; }
            var KOD = db.Database.SqlQuery<StokOnaySelect>(string.Format("[FINSAT6{0}].[wms].[StokOnaySelect] {1}", "17", param)).ToList();
            var json = new JavaScriptSerializer().Serialize(KOD);
            return json;
        }
        /// <summary>
        /// onayla
        /// </summary>
        public JsonResult Onay(string Data)
        {
            Result _Result = new Result(true);
            if (CheckPerm("Stok Onaylama", PermTypes.Writing) == false) return null;
            JArray parameters = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JArray>(Request["Data"]);
            SqlExper sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, "17");
            try
            {
                Dictionary<string, int> FiyatMaxSiraNo = new Dictionary<string, int>();
                foreach (JObject insertObj in parameters)
                {
                    DateTime date = DateTime.Now;
                    var shortDate = date.ToString("yyyy-MM-dd");
                    var sonuc = sqlexper.AcceptChanges();
                    db.Database.ExecuteSqlCommand(string.Format("UPDATE [FINSAT6{0}].[FINSAT6{0}].[STK] SET AktifPasif = 0, CheckSum =1  where MalKodu = '{1}'", "17", insertObj["MalKodu"].ToString()));
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
        /// <summary>
        /// reddet
        /// </summary>
        public JsonResult Red(string Data)
        {
            Result _Result = new Result(true);
            if (CheckPerm("Stok Onaylama", PermTypes.Writing) == false) return null;
            JArray parameters = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JArray>(Request["Data"]);
            SqlExper sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, "17");

            try
            {
                Dictionary<string, int> FiyatMaxSiraNo = new Dictionary<string, int>();
                foreach (JObject insertObj in parameters)
                {
                    DateTime date = DateTime.Now;
                    var shortDate = date.ToString("yyyy-MM-dd");
                    var sonuc = sqlexper.AcceptChanges();
                    db.Database.ExecuteSqlCommand(string.Format("UPDATE [FINSAT6{0}].[FINSAT6{0}].[STK] SET AktifPasif = 0, CheckSum =-1  where MalKodu = '{1}'", "17", insertObj["MalKodu"].ToString()));
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
    }
}