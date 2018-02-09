using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Wms12m.Entity;

namespace Wms12m.Presentation.Areas.Approvals.Controllers
{
    public class CheckController : RootController
    {
        #region SPGMY

        public JsonResult Onay_SPGMY(string Data)
        {
            var _Result = new Result(false, "Yetkiniz yok");
            if (CheckPerm(Perms.ÇekOnaylama, PermTypes.Writing) == false) return Json(_Result, JsonRequestBehavior.AllowGet);

            var parameters = JsonConvert.DeserializeObject<JArray>(Request["Data"]);
            var shortDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var sql = "";
            foreach (JObject insertObj in parameters)
            {
                sql += string.Format("UPDATE [FINSAT6{0}].[FINSAT6{0}].[CEK] SET SPGMYOnay = 1, SPGMYOnaylayan='" + vUser.UserName + "', SPGMYOnayTarih='{2}'  where ID = '{1}';", vUser.SirketKodu, insertObj["ID"].ToString(), shortDate);
            }

            try
            {
                db.Database.ExecuteSqlCommand(sql);
                _Result.Status = true;
                _Result.Message = "İşlem Başarılı ";
            }
            catch (Exception)
            {
                _Result.Message = "Hata Oluştu. ";
            }

            return Json(_Result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Red_SPGMY(string Data)
        {
            var _Result = new Result(false, "Yetkiniz yok");
            if (CheckPerm(Perms.ÇekOnaylama, PermTypes.Writing) == false) return Json(_Result, JsonRequestBehavior.AllowGet);

            var parameters = JsonConvert.DeserializeObject<JArray>(Request["Data"]);
            var shortDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var sql = "";
            foreach (JObject insertObj in parameters)
            {
                sql += string.Format("UPDATE [FINSAT6{0}].[FINSAT6{0}].[CEK] SET SPGMYOnay = 0, SPGMYOnaylayan='" + vUser.UserName + "', SPGMYOnayTarih='{2}', Durum = 1 where ID = '{1}';", vUser.SirketKodu, insertObj["ID"].ToString(), shortDate);
            }
            try
            {
                db.Database.ExecuteSqlCommand(sql);
                _Result.Status = true;
                _Result.Message = "İşlem Başarılı ";
            }
            catch (Exception)
            {
                _Result.Message = "Hata Oluştu. ";
            }

            return Json(_Result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SPGMY()
        {
            if (CheckPerm(Perms.ÇekOnaylama, PermTypes.Reading) == false) return Redirect("/");
            return View();
        }

        public string SPGMY_List()
        {
            var RT = db.Database.SqlQuery<CekOnaySelect>(string.Format("[FINSAT6{0}].[wms].[CekOnaySPGMY]", vUser.SirketKodu)).ToList();
            var json = new JavaScriptSerializer().Serialize(RT);
            return json;
        }

        #endregion SPGMY

        #region MIGMY

        public ActionResult MIGMY()
        {
            if (CheckPerm(Perms.ÇekOnaylama, PermTypes.Reading) == false) return Redirect("/");
            return View();
        }

        public string MIGMY_List()
        {
            var RT = db.Database.SqlQuery<CekOnaySelect>(string.Format("[FINSAT6{0}].[wms].[CekOnayMIGMY]", vUser.SirketKodu)).ToList();
            var json = new JavaScriptSerializer().Serialize(RT);
            return json;
        }

        public JsonResult Onay_MIGMY(string Data)
        {
            var _Result = new Result(false, "Yetkiniz yok");
            if (CheckPerm(Perms.ÇekOnaylama, PermTypes.Writing) == false) return Json(_Result, JsonRequestBehavior.AllowGet);
            var parameters = JsonConvert.DeserializeObject<JArray>(Request["Data"]);
            var shortDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var sql = "";
            foreach (JObject insertObj in parameters)
            {
                sql += string.Format("UPDATE [FINSAT6{0}].[FINSAT6{0}].[CEK] SET MIGMYOnay = 1, MIGMYOnaylayan='" + vUser.UserName + "', MIGMYOnayTarih='{2}'  where ID = '{1}'", vUser.SirketKodu, insertObj["ID"].ToString(), shortDate);
            }
            try
            {
                db.Database.ExecuteSqlCommand(sql);
                _Result.Status = true;
                _Result.Message = "İşlem Başarılı ";
            }
            catch (Exception)
            {
                _Result.Message = "Hata Oluştu. ";
            }

            return Json(_Result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Red_MIGMY(string Data)
        {
            var _Result = new Result(false, "Yetkiniz yok");
            if (CheckPerm(Perms.ÇekOnaylama, PermTypes.Writing) == false) return Json(_Result, JsonRequestBehavior.AllowGet);

            var parameters = JsonConvert.DeserializeObject<JArray>(Request["Data"]);
            var shortDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var sql = "";
            foreach (JObject insertObj in parameters)
            {
                sql += string.Format("UPDATE [FINSAT6{0}].[FINSAT6{0}].[CEK] SET MIGMYOnay = 0, MIGMYOnaylayan='" + vUser.UserName + "', MIGMYOnayTarih='{2}', Durum = 1 where ID = '{1}'", vUser.SirketKodu, insertObj["ID"].ToString(), shortDate);
            }
            try
            {
                db.Database.ExecuteSqlCommand(sql);
                _Result.Status = true;
                _Result.Message = "İşlem Başarılı ";
            }
            catch (Exception)
            {
                _Result.Message = "Hata Oluştu. ";
            }

            return Json(_Result, JsonRequestBehavior.AllowGet);
        }

        #endregion MIGMY

        #region GM

        public ActionResult GM()
        {
            if (CheckPerm(Perms.ÇekOnaylama, PermTypes.Reading) == false) return Redirect("/");
            return View();
        }

        public string GM_List()
        {
            var RT = db.Database.SqlQuery<CekOnaySelect>(string.Format("[FINSAT6{0}].[wms].[CekOnayGM]", vUser.SirketKodu)).ToList();
            var json = new JavaScriptSerializer().Serialize(RT);
            return json;
        }

        public JsonResult Onay_GM(string Data)
        {
            var _Result = new Result(false, "Yetkiniz yok");
            if (CheckPerm(Perms.ÇekOnaylama, PermTypes.Writing) == false) return Json(_Result, JsonRequestBehavior.AllowGet);

            var parameters = JsonConvert.DeserializeObject<JArray>(Request["Data"]);
            var shortDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var sql = "";
            foreach (JObject insertObj in parameters)
            {
                sql += string.Format("UPDATE [FINSAT6{0}].[FINSAT6{0}].[CEK] SET GMOnay = 1, GMOnaylayan='" + vUser.UserName + "', GMOnayTarih='{2}', Durum=1  where ID = '{1}'", vUser.SirketKodu, insertObj["ID"].ToString(), shortDate);
            }
            try
            {
                db.Database.ExecuteSqlCommand(sql);
                _Result.Status = true;
                _Result.Message = "İşlem Başarılı ";
            }
            catch (Exception)
            {
                _Result.Message = "Hata Oluştu. ";
            }

            return Json(_Result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Red_GM(string Data)
        {
            var _Result = new Result(false, "Yetkiniz yok");
            if (CheckPerm(Perms.ÇekOnaylama, PermTypes.Writing) == false) return Json(_Result, JsonRequestBehavior.AllowGet);

            var parameters = JsonConvert.DeserializeObject<JArray>(Request["Data"]);
            var shortDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var sql = "";
            foreach (JObject insertObj in parameters)
            {
                sql += string.Format("UPDATE [FINSAT6{0}].[FINSAT6{0}].[CEK] SET GMOnay = 0, GMOnaylayan='" + vUser.UserName + "', GMOnayTarih='{2}', Durum = 1 where ID = '{1}'", vUser.SirketKodu, insertObj["ID"].ToString(), shortDate);
            }
            try
            {
                db.Database.ExecuteSqlCommand(sql);
                _Result.Status = true;
                _Result.Message = "İşlem Başarılı ";
            }
            catch (Exception)
            {
                _Result.Message = "Hata Oluştu. ";
            }

            return Json(_Result, JsonRequestBehavior.AllowGet);
        }

        #endregion GM

        public string Onay_Details(string EvrakNo)
        {
            var json = new JavaScriptSerializer()
            {
                MaxJsonLength = int.MaxValue
            };

            var CE = db.Database.SqlQuery<CekOnayDetay>(string.Format("[FINSAT6{0}].[wms].[CekOnayDetay] @EvrakNo = '{1}'", vUser.SirketKodu, EvrakNo)).ToList();
            return json.Serialize(CE);
        }
    }
}