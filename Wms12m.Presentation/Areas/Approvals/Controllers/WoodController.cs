using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Wms12m.Entity;

namespace Wms12m.Presentation.Areas.Approvals.Controllers
{
    public class WoodController : RootController
    {
      
        public ActionResult TahsisliOnay()
        {
           // if (CheckPerm(Perms.FiyatOnaylamaGM, PermTypes.Reading) == false) return Redirect("/");
            return View();
        }
        public PartialViewResult TahsisliOnay_List()
        {
           // if (CheckPerm(Perms.FiyatOnaylamaGM, PermTypes.Reading) == false) return null;
            return PartialView();
        }
        public string TahsisliOnayCek()
        {
            if (CheckPerm(Perms.FiyatOnaylamaGM, PermTypes.Reading) == false) return null;
            var RT = db.Database.SqlQuery<TahsisOnayOdun>(string.Format("[FINSAT6{0}].[dbo].[IHLTAHOnaydaBekleyen] @Tip = 0", "17")).ToList();
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
    }
}