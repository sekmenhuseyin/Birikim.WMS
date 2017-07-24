﻿using Newtonsoft.Json;
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
                    //var sql = string.Format("SELECT * FROM [FINSAT6{0}].[FINSAT6{0}].[IHLTAH]  WHERE ID={1} AND ONAY = 0", "17", insertObj["ID"].ToString());

                    //var sonuc = sqlexper.AcceptChanges();
                    // IHLTAH ihltah = VKContext.IHLTAHs.Where(t => t.ID == item.ID && t.Onay == false).FirstOrDefault();
                    //var ihltah1 = db.Database.SqlQuery<IHLTAH>(sql).ToList();
                    string sql = "";
                    if (insertObj["Tip"].ToShort() == (short)0)
                    {
                        sql = ",TavanMiktar=" + insertObj["TavanMiktar"].ToDecimal() + ", TavanFiyat=" + insertObj["TavanFiyat"].ToDecimal();
                        //ihltah1[0].TavanMiktar = item.TavanMiktar;
                        //ihltah1[0].TavanFiyat = item.TavanFiyat;

                    }


                    //foreach (IHLTAH item in ihltah1)
                    //{
                    //    if (item.Tip == 0) 
                    //    {
                    //        ihltah1[0].TavanMiktar = item.TavanMiktar;
                    //        ihltah1[0].TavanFiyat = item.TavanFiyat;

                    //    }
                    //}

                    db.Database.ExecuteSqlCommand(string.Format("UPDATE [FINSAT6{0}].[FINSAT6{0}].[IHLTAH] SET Onay = 1, Onaylayan='" + vUser.UserName + "', OnayTarih='{2}',DegisTarih='{2}''{3}'  where ID = '{1}' AND Onay=0", "17", insertObj["ID"].ToString(), shortDate, sql));
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