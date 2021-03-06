﻿using Birikim.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
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
            if (CheckPerm(Perms.StokOnaylama, PermTypes.Reading) == false) return Redirect("/");
            return View();
        }

        /// <summary>
        /// liste
        /// </summary>
        public PartialViewResult List()
        {
            if (CheckPerm(Perms.StokOnaylama, PermTypes.Reading) == false) return null;
            return PartialView();
        }

        /// <summary>
        ///
        /// </summary>
        public string OnayCek(string Durum)
        {
            if (CheckPerm(Perms.StokOnaylama, PermTypes.Reading) == false) return null;
            var json = new JavaScriptSerializer()
            {
                MaxJsonLength = int.MaxValue
            };
            var param = 1;
            if (Durum == "Tumu") param = 0;
            else if (Durum == "Onay") param = 1;
            else if (Durum == "Pasif") param = 2;
            else if (Durum == "Aktif") param = 3;
            else if (Durum == "Red") param = 4;
            var KOD = db.Database.SqlQuery<StokOnaySelect>(string.Format("[FINSAT6{0}].[wms].[StokOnaySelect] {1}", vUser.SirketKodu, param)).ToList();
            return json.Serialize(KOD);
        }

        /// <summary>
        /// onayla
        /// </summary>
        public JsonResult Onay(string Data)
        {
            var _Result = new Result(true);
            if (CheckPerm(Perms.StokOnaylama, PermTypes.Writing) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            var parameters = JsonConvert.DeserializeObject<JArray>(Request["Data"]);
            try
            {
                Dictionary<string, int> FiyatMaxSiraNo = new Dictionary<string, int>();
                foreach (JObject insertObj in parameters)
                {
                    var date = DateTime.Now;
                    var shortDate = date.ToString("yyyy-MM-dd");
                    db.Database.ExecuteSqlCommand(string.Format("UPDATE [FINSAT6{0}].[FINSAT6{0}].[STK] SET AktifPasif = 0, CheckSum =1  where MalKodu = '{1}'", vUser.SirketKodu, insertObj["MalKodu"].ToString()));
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

        /// <summary>
        /// reddet
        /// </summary>
        public JsonResult Red(string Data)
        {
            var _Result = new Result(true);
            if (CheckPerm(Perms.StokOnaylama, PermTypes.Writing) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            var parameters = JsonConvert.DeserializeObject<JArray>(Request["Data"]);
            try
            {
                Dictionary<string, int> FiyatMaxSiraNo = new Dictionary<string, int>();
                foreach (JObject insertObj in parameters)
                {
                    var date = DateTime.Now;
                    var shortDate = date.ToString("yyyy-MM-dd");
                    db.Database.ExecuteSqlCommand(string.Format("UPDATE [FINSAT6{0}].[FINSAT6{0}].[STK] SET AktifPasif = 0, CheckSum =-1  where MalKodu = '{1}'", vUser.SirketKodu, insertObj["MalKodu"].ToString()));
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