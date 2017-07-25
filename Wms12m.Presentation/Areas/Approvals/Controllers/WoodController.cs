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
            var RT = db.Database.SqlQuery<TahsisOnayOdun>(string.Format("[FINSAT6{0}].[dbo].[IHLTAHOnaydaBekleyen] @Tip = 2", "17")).ToList();
            var json = new JavaScriptSerializer().Serialize(RT);
            return json;
        }
        public JsonResult Onay(string Data)
        {
            Result _Result = new Result(true);
          //  if (CheckPerm(Perms.TeminatOnay, PermTypes.Writing) == false) return null;
            JArray parameters = JsonConvert.DeserializeObject<JArray>(Request["Data"]);
            SqlExper sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, "17");
           
            try
            {
                
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
                        if (insertObj["TavanMiktar"].ToDecimal() > 0)
                            sql = ",TavanMiktar=" + insertObj["TavanMiktar"].ToString().Replace(',', '.').ToDecimal(); 
                        if (insertObj["TavanMiktar"].ToDecimal() > 0)
                            sql+= ", TavanFiyat=" + insertObj["TavanFiyat"].ToString().Replace(',', '.').ToDecimal();
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
                    string s = string.Format("UPDATE [FINSAT6{0}].[FINSAT6{0}].[IHLTAH] SET Onay = 1, Onaylayan='" + vUser.UserName + "', OnayTarih='{2}',DegisTarih='{2}'{3}  where ID = {1} AND Onay=0", "17", insertObj["ID"].ToString(), shortDate, sql);
                    db.Database.ExecuteSqlCommand(s);
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
        public JsonResult Red(string Data)
        {
            Result _Result = new Result(true);
            //  if (CheckPerm(Perms.TeminatOnay, PermTypes.Writing) == false) return null;
            JArray parameters = JsonConvert.DeserializeObject<JArray>(Request["Data"]);
            SqlExper sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, "17");

            try
            {
               
                foreach (JObject insertObj in parameters)
                {
                    string s = string.Format("DELETE FROM [FINSAT6{0}].[FINSAT6{0}].[IHLTAH] where ID = {1} AND Onay=0", "17", insertObj["ID"].ToString());
                    db.Database.ExecuteSqlCommand(s);
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


        public ActionResult TahsisliAlim()
        {

            string sorgu = string.Format(@"SELECT HesapKodu, (Unvan1+' '+Unvan2) AS Unvan 
	                                        FROM FINSAT6{0}.FINSAT6{0}.CHK(NOLOCK) 
	                                        WHERE HesapKodu Like '320%'", "17");
            var slctIsletme = db.Database.SqlQuery<CHKSelect1Result>(sorgu).ToList();

            string sorgu1 = string.Format(@"SELECT DISTINCT Yil, Hafta 
	                                        FROM FINSAT6{0}.FINSAT6{0}.IHLTAH(NOLOCK)", "17");
            var slctHafta = db.Database.SqlQuery<IHLTAH>(sorgu1).ToList();


            ViewBag.Hafta = new SelectList(slctHafta, "Yil", "Hafta");
            ViewBag.Isletme = new SelectList(slctIsletme, "HesapKodu", "Unvan");

            // if (CheckPerm(Perms.FiyatOnaylamaGM, PermTypes.Reading) == false) return Redirect("/");
            return View();
        }
        public PartialViewResult TahsisliAlim_List(string Hafta, string Isletme)
        {
            ViewBag.Hafta = Isletme;
            ViewBag.Isletme = Hafta;
            return PartialView();
        }
        public string TahsisliAlimCek(string Hafta, string Isletme)
        {
            string s;
            if (Hafta == "")
                s = string.Format("[FINSAT6{0}].[dbo].[IHLTAHKayit] @Tip = 2, @Yil=0, @Hafta=0, @HesapKodu='{1}'", "17", Isletme);
            else
                s = string.Format("[FINSAT6{0}].[dbo].[IHLTAHKayit] @Tip = 2, @Yil={1}, @Hafta={2}, @HesapKodu=NULL", "17", DateTime.Today.Year, Hafta);


            var RT = db.Database.SqlQuery<IHLTAHKayitResult>(s).ToList();
            var json = new JavaScriptSerializer().Serialize(RT);

            return json;
        }
    }
}