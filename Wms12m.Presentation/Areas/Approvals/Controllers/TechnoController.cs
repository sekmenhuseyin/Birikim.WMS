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
    public class TechnoController : RootController
    {
        // GET: Approvals/Techno
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult Ucret_List(string Tip)
        {
            if (CheckPerm("Fiyat Onaylama", PermTypes.Reading) == false) return null;
            ViewBag.Tip = Tip;
            return PartialView();
        }

        public PartialViewResult Prim_List(string Tip)
        {
            if (CheckPerm("Fiyat Onaylama", PermTypes.Reading) == false) return null;
            ViewBag.Tip = Tip;
            return PartialView();
        }

        public PartialViewResult EskiUcretData(string PERSONELID)
        {
            if (CheckPerm("Sözleşme Onaylama", PermTypes.Reading) == false) return null;
            var list = db.Database.SqlQuery<TechnoList>(string.Format("[HR0312M].[dbo].[TCH_EskiUcretSelect] {0}", PERSONELID)).ToList();
            return PartialView(list);
        }

        public string UcretListData(string tip)
        {
            JavaScriptSerializer json = new JavaScriptSerializer()
            {
                MaxJsonLength = int.MaxValue
            };
            List<TechnoList> ucretBilgi;
            try
            {
                ucretBilgi = db.Database.SqlQuery<TechnoList>(string.Format("[HR0312M].[dbo].[TCH_UcretOnaySelect] @Birim='GM', @Tip='{0}'", tip)).ToList();
            }
            catch (Exception ex)
            {
                ucretBilgi = new List<Entity.TechnoList>();
            }
            return json.Serialize(ucretBilgi);
        }
        public string PrimListData(string tip)
        {
            JavaScriptSerializer json = new JavaScriptSerializer()
            {
                MaxJsonLength = int.MaxValue
            };
            List<TechnoList> primBilgi;
            try
            {
                primBilgi = db.Database.SqlQuery<TechnoList>(string.Format("[HR0312M].[dbo].[TCH_PrimOnaySelect] @Birim='IK', @Tip='{0}'", tip)).ToList();
            }
            catch (Exception ex)
            {
                primBilgi = new List<Entity.TechnoList>();
            }
            return json.Serialize(primBilgi);
        }



        public JsonResult Ucret_Onayla(string Data)
        {
            Result _Result = new Result(true);
            if (CheckPerm("Sözleşme Onaylama", PermTypes.Writing) == false) return null;
            JArray parameters = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JArray>(Request["Data"]);
            SqlExper sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, "17");
            try
            {
                foreach (JObject insertObj in parameters)
                {
                    var ID = insertObj["ID"];
                    var OnayDerece = 1;
                    string s = string.Format("[HR0312M].[dbo].[TCH_UcretOnayUpdate] @OnayDerece={0}, @ID={1},@Birim='GM'", OnayDerece, ID);
                    var x = db.Database.SqlQuery<int>(s).ToList();
                    string ss = string.Format("[HR0312M].[dbo].[TCH_BUTUCRETINSERT] @ID={0}", ID);
                    var xx = db.Database.SqlQuery<int>(ss).ToList();
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
        public JsonResult Prim_Onayla(string Data)
        {
            Result _Result = new Result(true);
            if (CheckPerm("Sözleşme Onaylama", PermTypes.Writing) == false) return null;
            JArray parameters = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JArray>(Request["Data"]);
            SqlExper sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, "17");
            try
            {
                foreach (JObject insertObj in parameters)
                {
                    var ID = insertObj["ID"];
                    var OnayDerece = 1;
                    string s = string.Format("[HR0312M].[dbo].[TCH_PrimOnayUpdate] @OnayDerece={0}, @ID={1},@Birim='IK'", OnayDerece, ID);
                    var x = db.Database.SqlQuery<int>(s).ToList();
                    string ss = string.Format("[HR0312M].[dbo].[TCH_BRDSKALAINSERT] @ID={0}", ID);
                    var xx = db.Database.SqlQuery<int>(ss).ToList();
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