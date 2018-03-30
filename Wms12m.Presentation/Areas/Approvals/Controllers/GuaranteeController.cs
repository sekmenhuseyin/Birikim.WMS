using Birikim.Models;
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
    public class GuaranteeController : RootController
    {
        /// <summary>
        /// Teminat onay
        /// </summary>
        public ActionResult Index()
        {
            if (CheckPerm(Perms.TeminatOnay, PermTypes.Reading) == false) return Redirect("/");
            return View();
        }

        /// <summary>
        /// liste
        /// </summary>
        public string List()
        {
            if (CheckPerm(Perms.TeminatOnay, PermTypes.Reading) == false) return null;
            var RT = db.Database.SqlQuery<TeminatOnaySelect>(string.Format("[FINSAT6{0}].[wms].[TeminatOnayList]", vUser.SirketKodu)).ToList();
            var json = new JavaScriptSerializer().Serialize(RT);
            return json;
        }

        /// <summary>
        /// onayla
        /// </summary>
        public JsonResult Onay(string Data)
        {
            var _Result = new Result(true);
            if (CheckPerm(Perms.TeminatOnay, PermTypes.Writing) == false) return null;
            var parameters = JsonConvert.DeserializeObject<JArray>(Request["Data"]);

            try
            {
                Dictionary<string, int> FiyatMaxSiraNo = new Dictionary<string, int>();
                foreach (JObject insertObj in parameters)
                {
                    var date = DateTime.Now;
                    var shortDate = date.ToString("yyyy-MM-dd");
                    var sonuc = SqlExper.AcceptChanges();
                    db.Database.ExecuteSqlCommand(string.Format("[FINSAT6{0}].[wms].[TeminatOnayUpdate] @ID = {1}, @Kullanici = '{2}'", vUser.SirketKodu, insertObj["ID"].ToString(), vUser.UserName));
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
            if (CheckPerm(Perms.TeminatOnay, PermTypes.Writing) == false) return null;
            var parameters = JsonConvert.DeserializeObject<JArray>(Request["Data"]);
            try
            {
                Dictionary<string, int> FiyatMaxSiraNo = new Dictionary<string, int>();
                foreach (JObject insertObj in parameters)
                {
                    var date = DateTime.Now;
                    var shortDate = date.ToString("yyyy-MM-dd");
                    var sonuc = SqlExper.AcceptChanges();
                    db.Database.ExecuteSqlCommand(string.Format("DELETE FROM [FINSAT6{0}].[FINSAT6{0}].[Teminat] WHERE  ID = {1}", vUser.SirketKodu, insertObj["ID"].ToString()));
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
        /// tanım
        /// </summary>
        public ActionResult Tanim()
        {
            if (CheckPerm(Perms.TeminatTanim, PermTypes.Reading) == false) return Redirect("/");
            var CHK = db.Database.SqlQuery<RaporCHKSelect>(string.Format("[FINSAT6{0}].[wms].[CHKSelect1]", vUser.SirketKodu)).ToList();
            return View(CHK);
        }

        public string Durbun()
        {
            var DRBN = db.Database.SqlQuery<RaporCHKSelect>(string.Format("[FINSAT6{0}].[wms].[TeminatDurbunSelect]", vUser.SirketKodu)).ToList();
            var json = new JavaScriptSerializer().Serialize(DRBN);
            return json;
        }

        public string Bekleyen()
        {
            var BKLYN = db.Database.SqlQuery<Teminat>(string.Format("[FINSAT6{0}].[wms].[TeminatOnayList]", vUser.SirketKodu)).ToList();
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
            var TMNT = db.Database.SqlQuery<Teminat>(string.Format("[FINSAT6{0}].[wms].[TeminatOnaySelect] @HesapKodu='{1}'", vUser.SirketKodu, chk)).ToList();
            var json = new JavaScriptSerializer().Serialize(TMNT);
            return json;
        }

        public string Sil(int ID)
        {
            if (CheckPerm(Perms.TeminatTanim, PermTypes.Deleting) == false) return null;
            var sonuc = db.Database.SqlQuery<int>(string.Format("[FINSAT6{0}].[wms].[TeminatSil] @ID={1}", vUser.SirketKodu, ID)).FirstOrDefault();
            if (sonuc == 1) return "OK";
            else return "NO";
        }

        public string TeminatTanimInsert(string Data)
        {
            if (CheckPerm(Perms.TeminatTanim, PermTypes.Writing) == false) return null;
            var parameters = JsonConvert.DeserializeObject<JObject>(Request["Data"]);
            try
            {
                var tmnt = new Teminat()
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

                SqlExper.Insert(tmnt);
                var sonuc = SqlExper.AcceptChanges();
                if (sonuc.Status == true) return "OK";
                else return "NO";
            }
            catch (Exception)
            {
                return "NO";
            }
        }
    }
}