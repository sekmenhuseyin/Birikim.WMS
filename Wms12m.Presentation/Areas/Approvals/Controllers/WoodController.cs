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

            string sorgu1 = string.Format(@"SELECT DISTINCT CONVERT(varchar, Yil) + '-' + CONVERT(varchar, Hafta) AS value,  CONVERT(varchar, Hafta) + '. Hafta (' +  CONVERT(varchar, Yil) + ')' AS name, Yil, Hafta
                                        FROM            FINSAT6{0}.FINSAT6{0}.IHLTAH WITH (NOLOCK)
                                        WHERE        (Tip = 2)
                                        ORDER BY Yil, Hafta", "17");
            var slctHafta = db.Database.SqlQuery<IHLTAH>(sorgu1).ToList();


            ViewBag.Hafta = new SelectList(slctHafta, "value", "name");
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
            if (Hafta != "")
            {
                string[] tmp = Hafta.Split('-');
                s = string.Format("[FINSAT6{0}].[dbo].[IHLTAHKayit] @Tip = 2, @Yil={1}, @Hafta={2}, @HesapKodu=NULL", "17", tmp[0], tmp[1]);
            }
            else
                s = string.Format("[FINSAT6{0}].[dbo].[IHLTAHKayit] @Tip = 2, @Yil=0, @Hafta=0, @HesapKodu='{1}'", "17", Isletme);


            var RT = db.Database.SqlQuery<IHLTAHKayitResult>(s).ToList();
            var json = new JavaScriptSerializer().Serialize(RT);

            return json;
        }

        public ActionResult TahsisliIsletmeKasasi()
        {
            // if (CheckPerm(Perms.FiyatOnaylamaGM, PermTypes.Reading) == false) return Redirect("/");
            return View();
        }
        public PartialViewResult TahsisliIsletmeKasasi_List(string EvrakNo,string HesapKodu)
        {
            var RT = db.Database.SqlQuery<MyChi>(string.Format(@"SELECT DISTINCT CHI.HesapKodu, CHK.Unvan1 as Unvan, CHI.EvrakNo, CHI.EvrakNo2
                                                                , CAST(CHI.Tarih-2 as smalldatetime) as Tarih, CHI.Kod13, CHI.Kod14 

                                                                FROM FINSAT6{0}.FINSAT6{0}.CHI (nolock)
                                                                LEFT JOIN FINSAT6{0}.FINSAT6{0}.CHK (nolock) ON CHK.HesapKodu=CHI.HesapKodu
                                                                WHERE CHI.KynkEvrakTip=4 AND CHI.Kod13>0 AND CHI.Kod14>0 
                                                                AND CHI.EvrakNo2='{1}' AND CHI.HesapKodu='{2}' ", "17", EvrakNo, HesapKodu)).ToList();

            var RT1 = db.Database.SqlQuery<MySti>(string.Format(@"SELECT STI.EvrakNo, STI.Tarih, STI.Chk, STI.MalKodu, STK.MalAdi, STI.BirimMiktar, STI.Birim 
                                                                  FROM FINSAT6{0}.FINSAT6{0}.STI (nolock)
                                                                INNER JOIN FINSAT6{0}.FINSAT6{0}.STK (nolock) ON STK.MalKodu=STI.MalKodu
                                                                WHERE STI.KynkEvrakTip=4 AND STI.Chk='{2}' AND STI.EvrakNo IN  
                                                                (
                                                                    SELECT EvrakNo FROM FINSAT6{0}.FINSAT6{0}.CHI (nolock) 
                                                                    WHERE KynkEvrakTip=4 AND Kod13>0 AND Kod14>0 AND EvrakNo2='{1}' 
                                                                    AND HesapKodu='{2}' AND BirimMiktar>0
                                                                 )", "17", EvrakNo, HesapKodu)).ToList();


            foreach (MyChi chi in RT)
            {
                chi.FaturaDetay = RT1.FindAll(t => t.EvrakNo == chi.EvrakNo);
            }
            return PartialView(RT);
        }
        public string KasaEvrakCek()
        {
            var RT = db.Database.SqlQuery<TahsisliIsletmeKasa>(string.Format(@"SELECT IHL.EvrakNo, IHL.OrmIslt, CHK.Unvan1 as OrmIsltUnvan, IHL.Yil, IHL.Hafta 
                                                                        , IHL.TahTopMektupTutar, IHL.TahPesinat
                                                                        , IHL.IbreliMiktarSter, IHL.IbreliMiktarM3
                                                                        , IHL.YaprakliMiktarSter, IHL.YaprakliMiktarM3

                                                                        FROM FINSAT6{0}.FINSAT6{0}.IHLTAH (nolock) AS IHL
                                                                        LEFT JOIN FINSAT6{0}.FINSAT6{0}.CHK (nolock) ON CHK.HesapKodu=IHL.OrmIslt
                                                                        WHERE IHL.Tip=2
                                                                        ORDER BY IHL.Yil DESC, IHL.Hafta DESC", "17")).ToList();
            var json = new JavaScriptSerializer().Serialize(RT);
            return json;
        }
        public string KasaEvrakCekEvrakNoIle(string EvrakNo,string HesapKodu)
        {
            var RT = db.Database.SqlQuery<MyChi>(string.Format(@"SELECT DISTINCT CHI.HesapKodu, CHK.Unvan1 as Unvan, CHI.EvrakNo, CHI.EvrakNo2
                                                                , CAST(CHI.Tarih-2 as smalldatetime) as Tarih, CHI.Kod13, CHI.Kod14 

                                                                FROM FINSAT6{0}.FINSAT6{0}.CHI (nolock)
                                                                LEFT JOIN FINSAT6{0}.FINSAT6{0}.CHK (nolock) ON CHK.HesapKodu=CHI.HesapKodu
                                                                WHERE CHI.KynkEvrakTip=4 AND CHI.Kod13>0 AND CHI.Kod14>0 
                                                                AND CHI.EvrakNo2='{1}' AND CHI.HesapKodu='{2}' ", "17",EvrakNo,HesapKodu)).ToList();
           
            var RT1 = db.Database.SqlQuery<MySti>(string.Format(@"SELECT STI.EvrakNo, STI.Tarih, STI.Chk, STI.MalKodu, STK.MalAdi, STI.BirimMiktar, STI.Birim 
                                                                  FROM FINSAT6{0}.FINSAT6{0}.STI (nolock)
                                                                INNER JOIN FINSAT6{0}.FINSAT6{0}.STK (nolock) ON STK.MalKodu=STI.MalKodu
                                                                WHERE STI.KynkEvrakTip=4 AND STI.Chk='{2}' AND STI.EvrakNo IN  
                                                                (
                                                                    SELECT EvrakNo FROM FINSAT6{0}.FINSAT6{0}.CHI (nolock) 
                                                                    WHERE KynkEvrakTip=4 AND Kod13>0 AND Kod14>0 AND EvrakNo2='{1}' 
                                                                    AND HesapKodu='{2}' AND BirimMiktar>0
                                                                 )", "17", EvrakNo, HesapKodu)).ToList();


            foreach (MyChi chi in RT)
            {
                chi.FaturaDetay = RT1.FindAll(t => t.EvrakNo == chi.EvrakNo);
            }

            

            var json = new JavaScriptSerializer().Serialize(RT);
            return json;
        }


        public ActionResult IhaleliOnay()
        {
            // if (CheckPerm(Perms.FiyatOnaylamaGM, PermTypes.Reading) == false) return Redirect("/");
            return View();
        }
        public PartialViewResult IhaleliAlim_List()
        {
            // if (CheckPerm(Perms.FiyatOnaylamaGM, PermTypes.Reading) == false) return null;
            return PartialView();
        }
        public string IhaleliAlimOnayCek()
        {
            if (CheckPerm(Perms.FiyatOnaylamaGM, PermTypes.Reading) == false) return null;
            var RT = db.Database.SqlQuery<TahsisOnayOdun>(string.Format("[FINSAT6{0}].[dbo].[IHLTAHOnaydaBekleyen] @Tip = 0", "17")).ToList();
            var json = new JavaScriptSerializer().Serialize(RT);
            return json;
        }
        public JsonResult IhaleliOnay(string Data)
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
                            sql += ", TavanFiyat=" + insertObj["TavanFiyat"].ToString().Replace(',', '.').ToDecimal();
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
        public JsonResult IhaleliRed(string Data)
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

    }
}