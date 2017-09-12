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
            catch (Exception)
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
            catch (Exception)
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
            string s = HttpContext.Request.Url.ToString();
            Uri uri = new Uri(s);
            string link = string.Format("{0}:///{1}:{2}", "file", uri.Host, uri.Port);
            ViewBag.Path = link;
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
        public PartialViewResult TahsisliIsletmeKasasi_List(string EvrakNo, string HesapKodu)
        {
            string sql = string.Format(@"SELECT DISTINCT FINSAT6{0}.FINSAT6{0}.CHI.HesapKodu, FINSAT6{0}.FINSAT6{0}.CHK.Unvan1 AS Unvan, FINSAT6{0}.FINSAT6{0}.CHI.EvrakNo, FINSAT6{0}.FINSAT6{0}.CHI.EvrakNo2, CAST(FINSAT6{0}.FINSAT6{0}.CHI.Tarih - 2 AS smalldatetime) 
															 AS Tarih, FINSAT6{0}.FINSAT6{0}.CHI.Kod13, FINSAT6{0}.FINSAT6{0}.CHI.Kod14,
																 (SELECT        ISNULL(SUM(FINSAT6{0}.FINSAT6{0}.STI.BirimMiktar), 0) AS Expr1
																   FROM            FINSAT6{0}.FINSAT6{0}.STI INNER JOIN
																							 FINSAT6{0}.FINSAT6{0}.STK ON FINSAT6{0}.FINSAT6{0}.STI.MalKodu = FINSAT6{0}.FINSAT6{0}.STK.MalKodu
																   WHERE        (FINSAT6{0}.FINSAT6{0}.STK.MalAdi LIKE 'İBRE%') AND (FINSAT6{0}.FINSAT6{0}.STI.Birim = 'Ster') AND (FINSAT6{0}.FINSAT6{0}.STI.EvrakNo = CHI.EvrakNo)) AS topSterIbre,
																 (SELECT        ISNULL(SUM(FINSAT6{0}.FINSAT6{0}.STI.BirimMiktar), 0) AS Expr1
																   FROM            FINSAT6{0}.FINSAT6{0}.STI INNER JOIN
																							 FINSAT6{0}.FINSAT6{0}.STK ON FINSAT6{0}.FINSAT6{0}.STI.MalKodu = FINSAT6{0}.FINSAT6{0}.STK.MalKodu
																   WHERE        (FINSAT6{0}.FINSAT6{0}.STK.MalAdi LIKE 'Yaprak%') AND (FINSAT6{0}.FINSAT6{0}.STI.Birim = 'Ster') AND (FINSAT6{0}.FINSAT6{0}.STI.EvrakNo = CHI.EvrakNo)) AS topSterYaprak,
																 (SELECT        ISNULL(SUM(FINSAT6{0}.FINSAT6{0}.STI.BirimMiktar), 0) AS Expr1
																   FROM            FINSAT6{0}.FINSAT6{0}.STI INNER JOIN
																							 FINSAT6{0}.FINSAT6{0}.STK ON FINSAT6{0}.FINSAT6{0}.STI.MalKodu = FINSAT6{0}.FINSAT6{0}.STK.MalKodu
																   WHERE        (FINSAT6{0}.FINSAT6{0}.STK.MalAdi LIKE 'İBRE%') AND (FINSAT6{0}.FINSAT6{0}.STI.Birim = 'M3') AND (FINSAT6{0}.FINSAT6{0}.STI.EvrakNo = CHI.EvrakNo)) AS topM3Ibre,
																 (SELECT        ISNULL(SUM(FINSAT6{0}.FINSAT6{0}.STI.BirimMiktar), 0) AS Expr1
																   FROM            FINSAT6{0}.FINSAT6{0}.STI INNER JOIN
																							 FINSAT6{0}.FINSAT6{0}.STK ON FINSAT6{0}.FINSAT6{0}.STI.MalKodu = FINSAT6{0}.FINSAT6{0}.STK.MalKodu
																   WHERE        (FINSAT6{0}.FINSAT6{0}.STK.MalAdi LIKE 'Yaprak%') AND (FINSAT6{0}.FINSAT6{0}.STI.Birim = 'M3') AND (FINSAT6{0}.FINSAT6{0}.STI.EvrakNo = CHI.EvrakNo)) AS topM3Yaprak

									FROM            FINSAT6{0}.FINSAT6{0}.CHI WITH (nolock) LEFT OUTER JOIN
															 FINSAT6{0}.FINSAT6{0}.CHK WITH (nolock) ON FINSAT6{0}.FINSAT6{0}.CHK.HesapKodu = FINSAT6{0}.FINSAT6{0}.CHI.HesapKodu
									WHERE        (FINSAT6{0}.FINSAT6{0}.CHI.KynkEvrakTip = 4) AND (FINSAT6{0}.FINSAT6{0}.CHI.Kod13 > 0) AND (FINSAT6{0}.FINSAT6{0}.CHI.Kod14 > 0) AND 
									(FINSAT6{0}.FINSAT6{0}.CHI.EvrakNo2 = '{1}') AND  (FINSAT6{0}.FINSAT6{0}.CHI.HesapKodu = '{2}')", "17", EvrakNo, HesapKodu);
            var RT = db.Database.SqlQuery<MyChi>(sql).ToList();

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
        public string KasaEvrakCekEvrakNoIle(string EvrakNo, string HesapKodu)
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
        public JsonResult IhaleliOnayla(string Data)
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
            catch (Exception)
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
            catch (Exception)
            {

                _Result.Status = false;
                _Result.Message = "Hata Oluştu. ";

            }
            return Json(_Result, JsonRequestBehavior.AllowGet);

        }


        public ActionResult IhaleAlim()
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
        public PartialViewResult IhaleAlim_List(string Hafta, string Isletme)
        {
            ViewBag.Hafta = Isletme;
            ViewBag.Isletme = Hafta;
            return PartialView();
        }
        public string IhaleAlimCek(string Hafta, string Isletme)
        {
            string s;
            if (Hafta != "")
            {
                string[] tmp = Hafta.Split('-');
                s = string.Format("[FINSAT6{0}].[dbo].[IHLTAHKayit] @Tip = 0, @Yil={1}, @Hafta={2}, @HesapKodu=NULL", "17", tmp[0], tmp[1]);
            }
            else
                s = string.Format("[FINSAT6{0}].[dbo].[IHLTAHKayit] @Tip = 0, @Yil=0, @Hafta=0, @HesapKodu='{1}'", "17", Isletme);


            var RT = db.Database.SqlQuery<IHLTAHKayitResult>(s).ToList();
            var json = new JavaScriptSerializer().Serialize(RT);

            return json;
        }

        public ActionResult IhaleliIsletmeKasasi()
        {
            // if (CheckPerm(Perms.FiyatOnaylamaGM, PermTypes.Reading) == false) return Redirect("/");
            return View();
        }
        public PartialViewResult IhaleliIsletmeKasasi_List(string EvrakNo, string HesapKodu)
        {
            string sql = string.Format(@"SELECT DISTINCT FINSAT6{0}.FINSAT6{0}.CHI.HesapKodu, FINSAT6{0}.FINSAT6{0}.CHK.Unvan1 AS Unvan, FINSAT6{0}.FINSAT6{0}.CHI.EvrakNo, FINSAT6{0}.FINSAT6{0}.CHI.EvrakNo2, CAST(FINSAT6{0}.FINSAT6{0}.CHI.Tarih - 2 AS smalldatetime) 
															 AS Tarih, FINSAT6{0}.FINSAT6{0}.CHI.Kod13, FINSAT6{0}.FINSAT6{0}.CHI.Kod14,
																 (SELECT        ISNULL(SUM(FINSAT6{0}.FINSAT6{0}.STI.BirimMiktar), 0) AS Expr1
																   FROM            FINSAT6{0}.FINSAT6{0}.STI INNER JOIN
																							 FINSAT6{0}.FINSAT6{0}.STK ON FINSAT6{0}.FINSAT6{0}.STI.MalKodu = FINSAT6{0}.FINSAT6{0}.STK.MalKodu
																   WHERE        (FINSAT6{0}.FINSAT6{0}.STK.MalAdi LIKE 'İBRE%') AND (FINSAT6{0}.FINSAT6{0}.STI.Birim = 'Ster') AND (FINSAT6{0}.FINSAT6{0}.STI.EvrakNo = CHI.EvrakNo)) AS topSterIbre,
																 (SELECT        ISNULL(SUM(FINSAT6{0}.FINSAT6{0}.STI.BirimMiktar), 0) AS Expr1
																   FROM            FINSAT6{0}.FINSAT6{0}.STI INNER JOIN
																							 FINSAT6{0}.FINSAT6{0}.STK ON FINSAT6{0}.FINSAT6{0}.STI.MalKodu = FINSAT6{0}.FINSAT6{0}.STK.MalKodu
																   WHERE        (FINSAT6{0}.FINSAT6{0}.STK.MalAdi LIKE 'Yaprak%') AND (FINSAT6{0}.FINSAT6{0}.STI.Birim = 'Ster') AND (FINSAT6{0}.FINSAT6{0}.STI.EvrakNo = CHI.EvrakNo)) AS topSterYaprak,
																 (SELECT        ISNULL(SUM(FINSAT6{0}.FINSAT6{0}.STI.BirimMiktar), 0) AS Expr1
																   FROM            FINSAT6{0}.FINSAT6{0}.STI INNER JOIN
																							 FINSAT6{0}.FINSAT6{0}.STK ON FINSAT6{0}.FINSAT6{0}.STI.MalKodu = FINSAT6{0}.FINSAT6{0}.STK.MalKodu
																   WHERE        (FINSAT6{0}.FINSAT6{0}.STK.MalAdi LIKE 'İBRE%') AND (FINSAT6{0}.FINSAT6{0}.STI.Birim = 'M3') AND (FINSAT6{0}.FINSAT6{0}.STI.EvrakNo = CHI.EvrakNo)) AS topM3Ibre,
																 (SELECT        ISNULL(SUM(FINSAT6{0}.FINSAT6{0}.STI.BirimMiktar), 0) AS Expr1
																   FROM            FINSAT6{0}.FINSAT6{0}.STI INNER JOIN
																							 FINSAT6{0}.FINSAT6{0}.STK ON FINSAT6{0}.FINSAT6{0}.STI.MalKodu = FINSAT6{0}.FINSAT6{0}.STK.MalKodu
																   WHERE        (FINSAT6{0}.FINSAT6{0}.STK.MalAdi LIKE 'Yaprak%') AND (FINSAT6{0}.FINSAT6{0}.STI.Birim = 'M3') AND (FINSAT6{0}.FINSAT6{0}.STI.EvrakNo = CHI.EvrakNo)) AS topM3Yaprak

									FROM            FINSAT6{0}.FINSAT6{0}.CHI WITH (nolock) LEFT OUTER JOIN
															 FINSAT6{0}.FINSAT6{0}.CHK WITH (nolock) ON FINSAT6{0}.FINSAT6{0}.CHK.HesapKodu = FINSAT6{0}.FINSAT6{0}.CHI.HesapKodu
									WHERE        (FINSAT6{0}.FINSAT6{0}.CHI.KynkEvrakTip = 4) AND (FINSAT6{0}.FINSAT6{0}.CHI.Kod13 > 0) AND (FINSAT6{0}.FINSAT6{0}.CHI.Kod14 > 0) AND 
									(FINSAT6{0}.FINSAT6{0}.CHI.EvrakNo2 = '{1}') AND  (FINSAT6{0}.FINSAT6{0}.CHI.HesapKodu = '{2}')", "17", EvrakNo, HesapKodu);
            var RT = db.Database.SqlQuery<MyChi>(sql).ToList();

            return PartialView(RT);
        }
        public string IhaleEvrakCek()
        {
            var RT = db.Database.SqlQuery<TahsisliIsletmeKasa>(string.Format(@"SELECT DISTINCT IHL.EvrakNo, IHL.OrmIslt, CHK.Unvan1 as OrmIsltUnvan, IHL.Yil, IHL.Hafta 
																				, IHL.TebVadeliTeminat, IHL.TebVadeliPesinat
																				, IHL.TebKampTeminat, IHL.TebKampPesinat, IHL.TebPesinTutar
																				, IHL.TebKrediKartOdeme, IHL.TebHavaleOdeme

																				FROM FINSAT6{0}.FINSAT6{0}.IHLTAH (nolock) AS IHL
																				LEFT JOIN FINSAT6{0}.FINSAT6{0}.CHK (nolock) ON CHK.HesapKodu=IHL.OrmIslt
																				WHERE IHL.Tip=0
																				ORDER BY IHL.Yil DESC, IHL.Hafta DESC", "17")).ToList();
            var json = new JavaScriptSerializer().Serialize(RT);
            return json;
        }

        public ActionResult NakliyeFiyatOnay()
        {
            // if (CheckPerm(Perms.FiyatOnaylamaGM, PermTypes.Reading) == false) return Redirect("/");
            return View();
        }
        public PartialViewResult NakliyeFiyat_List()
        {
            // if (CheckPerm(Perms.FiyatOnaylamaGM, PermTypes.Reading) == false) return null;
            return PartialView();
        }
        public string NakliyeFiyatOnayCek()
        {
            if (CheckPerm(Perms.FiyatOnaylamaGM, PermTypes.Reading) == false) return null;
            var RT = db.Database.SqlQuery<MyDep>(string.Format("SELECT *  FROM[FINSAT6{0}].[FINSAT6{0}].[DEP]  WHERE Depo LIKE 'H%' AND Kod2<>''", "17")).ToList();
            var json = new JavaScriptSerializer().Serialize(RT);
            return json;
        }
        public JsonResult NakliyeFiyatOnayla(string Data)
        {
            Result _Result = new Result(true);
            //  if (CheckPerm(Perms.TeminatOnay, PermTypes.Writing) == false) return null;
            JArray parameters = JsonConvert.DeserializeObject<JArray>(Request["Data"]);
            SqlExper sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, "17");

            try
            {

                foreach (JObject insertObj in parameters)
                {
                    string s = string.Format("UPDATE [FINSAT6{0}].[FINSAT6{0}].[DEP] SET Kod1 = '{1}', Kod2='', Degistiren='" + vUser.UserName + "',DegisTarih='{2}'  where Depo = '{3}'AND Kod2<>''", "17", insertObj["Kod2"].ToString(), (int)DateTime.Now.ToOADate(), insertObj["Depo"].ToString());
                    db.Database.ExecuteSqlCommand(s);
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
        public JsonResult NakliyeFiyatRed(string Data)
        {
            Result _Result = new Result(true);
            //  if (CheckPerm(Perms.TeminatOnay, PermTypes.Writing) == false) return null;
            JArray parameters = JsonConvert.DeserializeObject<JArray>(Request["Data"]);
            SqlExper sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, "17");

            try
            {

                foreach (JObject insertObj in parameters)
                {
                    string s = string.Format("UPDATE [FINSAT6{0}].[FINSAT6{0}].[DEP] SET Kod2 = '', Degistiren='" + vUser.UserName + "',DegisTarih='{1}'  where Depo = '{2}'AND Kod2<>''", "17", (int)DateTime.Now.ToOADate(), insertObj["Depo"].ToString());

                    db.Database.ExecuteSqlCommand(s);
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

        public ActionResult NakliyeFiyatlar()
        {
            // if (CheckPerm(Perms.FiyatOnaylamaGM, PermTypes.Reading) == false) return Redirect("/");
            return View();
        }
        public PartialViewResult NakliyeFiyatlar_List()
        {
            // if (CheckPerm(Perms.FiyatOnaylamaGM, PermTypes.Reading) == false) return null;
            return PartialView();
        }
        public string NakliyeFiyatlarCek()
        {
            if (CheckPerm(Perms.FiyatOnaylamaGM, PermTypes.Reading) == false) return null;
            var RT = db.Database.SqlQuery<MyDep>(string.Format("SELECT *  FROM[FINSAT6{0}].[FINSAT6{0}].[DEP]  WHERE Depo LIKE 'H%'", "17")).ToList();
            var json = new JavaScriptSerializer().Serialize(RT);
            return json;
        }


        public ActionResult DisDepoStokRapor()
        {

            return View();
        }
        public PartialViewResult DisDepoStokRapor_List()
        {
            return PartialView();
        }
        public string DisDepoStokCek(string MalKoduBas, string MalKoduBit)
        {
            if (CheckPerm(Perms.FiyatOnaylamaGM, PermTypes.Reading) == false) return null;
            string sql = string.Format(@"SELECT DST.Depo, DEP.DepoAdi, DST.MalKodu, STK.MalAdi, STK.Birim1 as Birim
																, DST.DvrMiktar, DST.GirMiktar, DST.CikMiktar 

																, ISNULL(A.DevirMiktar,0) as STIDvrMiktar, ISNULL(A.GirisMiktar,0) as STIGirMiktar, ISNULL(A.CikisMiktar,0) as STICikMiktar

																FROM FINSAT6{0}.FINSAT6{0}.DST (nolock)
																INNER JOIN FINSAT6{0}.FINSAT6{0}.DEP (nolock) ON DEP.Depo=DST.Depo
																INNER JOIN FINSAT6{0}.FINSAT6{0}.STK (nolock) ON STK.MalKodu=DST.MalKodu

																LEFT JOIN
																(
																	SELECT Depo, MalKodu
																	, SUM(CASE WHEN STI.IslemTur=0 AND STI.IslemTip=0 THEN STI.Miktar WHEN STI.IslemTur=1 AND STI.IslemTip=0 THEN -STI.Miktar ELSE 0 END) 
																	as DevirMiktar
																	, SUM(CASE WHEN STI.IslemTur=0 and STI.IslemTip  not in (0,18) AND STI.Irsfat not in (2,3) THEN STI.Miktar ELSE 0 END)
																	as GirisMiktar
																	, SUM(CASE WHEN STI.Islemtur=1 AND STI.Islemtip not in (0,18) AND STI.Irsfat not in (2,3)  THEN STI.Miktar ELSE 0 END)
																	as CikisMiktar

 
																	FROM FINSAT6{0}.FINSAT6{0}.STI (nolock)
																	WHERE Depo LIKE 'H%' 	
																	GROUP BY Depo, MalKodu
																) AS A ON A.Depo=DST.Depo AND A.MalKodu=DST.MalKodu


																WHERE DST.Depo LIKE 'H%'", "17");
            if (!string.IsNullOrEmpty(MalKoduBas) || !string.IsNullOrEmpty(MalKoduBit))
            {
                if (string.IsNullOrEmpty(MalKoduBas))
                    MalKoduBas = "0000000000000000000";
                if (string.IsNullOrEmpty(MalKoduBit))
                    MalKoduBit = "9999999999999999999";

                sql += string.Format(" AND DST.MalKodu BETWEEN '{0}' AND '{1}'", MalKoduBas, MalKoduBit);
            }
            sql += " ORDER BY DST.Depo";
            var RT = db.Database.SqlQuery<MyDst>(sql).ToList();
            var json = new JavaScriptSerializer().Serialize(RT);
            return json;
        }
        public string MalKoduCek()
        {
            var RT = db.Database.SqlQuery<MyStk>(string.Format(@"SELECT MalKodu, MalAdi, Birim1, Birim2, Birim3 FROM FINSAT6{0}.FINSAT6{0}.STK (nolock)
																			WHERE MalKodu LIKE '095%'", "17")).ToList();
            var json = new JavaScriptSerializer().Serialize(RT);
            return json;
        }

        public ActionResult DisDepoStokMaliyetRapor()
        {

            return View();
        }
        public PartialViewResult DisDepoStokMaliyetRapor_List()
        {
            // if (CheckPerm(Perms.FiyatOnaylamaGM, PermTypes.Reading) == false) return null;
            return PartialView();
        }
        public string DisDepoStokMaliyetCek(string MalKoduBas, string MalKoduBit, bool isletmeBazli, bool depoBazli, Nullable<int> datebas, Nullable<int> datebit)
        {

            List<MySti> list = new List<MySti>();
            List<MySti> RT = new List<MySti>();
            string malkoduaraliksql = "";
            if (!string.IsNullOrEmpty(MalKoduBas) || !string.IsNullOrEmpty(MalKoduBit))
            {
                if (string.IsNullOrEmpty(MalKoduBas))
                    MalKoduBas = "09500000000000000000";
                if (string.IsNullOrEmpty(MalKoduBit))
                    MalKoduBit = "09599999999999999999";

                malkoduaraliksql = string.Format("AND MalKodu BETWEEN '{0}' AND '{1}' ", MalKoduBas, MalKoduBit);
            }


            string tarihsql = "";
            if (datebas != null && datebit != null)
                tarihsql = string.Format("AND STI.Tarih BETWEEN {0} AND {1}", datebas, datebit);
            else if (datebas == null && datebit != null)
                tarihsql = string.Format("AND STI.Tarih <= {0}", datebit);
            else if (datebas != null && datebit == null)
                tarihsql = string.Format("AND STI.Tarih >= {0}", datebas);


            string sorgu = "";
            if (depoBazli == false && isletmeBazli == false)
            {
                sorgu = string.Format(
                    @"SELECT A.MalKodu, STK.MalAdi, STK.Birim1 as Birim, ISNULL(A.Fiyat,0) as BirimFiyat 
										FROM FINSAT6{0}.FINSAT6{0}.STK (nolock)
										INNER JOIN
										(
											SELECT MalKodu, SUM(Tutar)/NULLIF(SUM(Miktar),0) as Fiyat
											FROM FINSAT6{0}.FINSAT6{0}.STI (nolock)
											WHERE (STI.KynkEvrakTip=4 OR (STI.KynkEvrakTip=58 AND STI.IslemTip=0))
											AND Depo LIKE 'H%' {1} {2}
											GROUP BY MalKodu
										) as A ON A.MalKodu=STK.MalKodu"
                , "17"
                , malkoduaraliksql
                , tarihsql);

                RT = db.Database.SqlQuery<MySti>(sorgu).ToList();

                foreach (MySti item in RT)
                {
                    MySti sti = new MySti
                    {
                        MalKodu = item.MalKodu,
                        MalAdi = item.MalAdi,
                        Birim = item.Birim,
                        BirimFiyat = item.BirimFiyat
                    };

                    list.Add(sti);
                }
            }


            else
            {

                string where = "";
                if (malkoduaraliksql != "")
                    where = "WHERE " + malkoduaraliksql.Substring(4);

                sorgu = string.Format(

@"SELECT A.Chk, CHK.Unvan1, A.Depo, A.MalKodu, A.Tutar, A.BirimMiktar
				, STK.MalAdi, STK.Birim1 as Birim
				, ISNULL(A.Tutar / NULLIF(A.BirimMiktar,0),0) as BirimFiyat
				, DEP.DepoAdi
				FROM
				(
					SELECT Chk, Depo, MalKodu, SUM(Tutar) as Tutar, SUM(BirimMiktar) as BirimMiktar
					FROM
					(
						SELECT Chk, Depo, MalKodu, Tutar, BirimMiktar
						FROM FINSAT6{0}.FINSAT6{0}.STI (nolock)
						WHERE STI.KynkEvrakTip=4 {2}
						AND Depo LIKE 'H%' 

						union all

						SELECT DEP.Yetkili2, STI.Depo, MalKodu, Tutar, BirimMiktar
						FROM FINSAT6{0}.FINSAT6{0}.STI (nolock)
						INNER JOIN FINSAT6{0}.FINSAT6{0}.DEP (nolock) on DEP.Depo=STI.Depo
						WHERE STI.KynkEvrakTip=58 AND STI.IslemTip=0
						AND STI.Depo LIKE 'H%' {2} 
					) as B
					{1}
					GROUP BY Chk, Depo, MalKodu
				) as A
				INNER JOIN FINSAT6{0}.FINSAT6{0}.STK (nolock) ON STK.MalKodu=A.MalKodu
				INNER JOIN FINSAT6{0}.FINSAT6{0}.CHK (nolock) ON CHK.HesapKodu=A.Chk
				INNER JOIN FINSAT6{0}.FINSAT6{0}.DEP (nolock) ON DEP.Depo=A.Depo"
                , "17"
                , where
                , tarihsql);



                List<MySti> list2 = new List<MySti>();
                RT = db.Database.SqlQuery<MySti>(sorgu).ToList();
                if (RT.Count() > 0)
                {
                    int Sayac = 0;
                    try
                    {
                        foreach (MySti item in RT)
                        {


                            MySti sti = new MySti
                            {
                                Chk = item.Chk,
                                Unvan = item.Unvan1,
                                Depo = item.Depo,
                                MalKodu = item.MalKodu,
                                Tutar = item.Tutar,
                                BirimMiktar = item.BirimMiktar,
                                MalAdi = item.MalAdi,
                                Birim = item.Birim,
                                BirimFiyat = item.BirimFiyat,
                                DepoAdi = item.DepoAdi
                            };

                            list2.Add(sti);


                            Sayac++;
                        }
                    }
                    catch (Exception)
                    {


                    }


                }

                if (depoBazli && isletmeBazli)
                {
                    list.AddRange(list2);
                }
                else if (depoBazli)
                {
                    foreach (var item in list2.GroupBy(t => new { t.Depo, t.MalKodu }))
                    {
                        MySti sti = new MySti
                        {
                            Depo = item.Key.Depo,
                            DepoAdi = item.First().DepoAdi,
                            MalKodu = item.Key.MalKodu,
                            MalAdi = item.First().MalAdi,
                            Birim = item.First().Birim
                        };


                        decimal tutar = item.Sum(t => t.Tutar);
                        decimal birimMiktar = item.Sum(t => t.BirimMiktar);
                        if (birimMiktar > 0)
                            sti.BirimFiyat = tutar / birimMiktar;

                        list.Add(sti);
                    }
                }
                else if (isletmeBazli)
                {
                    foreach (var item in list2.GroupBy(t => new { t.Chk, t.MalKodu }))
                    {
                        MySti sti = new MySti
                        {
                            Chk = item.Key.Chk,
                            Unvan = item.First().Unvan,
                            MalKodu = item.Key.MalKodu,
                            MalAdi = item.First().MalAdi,
                            Birim = item.First().Birim
                        };

                        decimal tutar = item.Sum(t => t.Tutar);
                        decimal birimMiktar = item.Sum(t => t.BirimMiktar);
                        if (birimMiktar > 0)
                            sti.BirimFiyat = tutar / birimMiktar;

                        list.Add(sti);
                    }
                }
            }





            var json = new JavaScriptSerializer().Serialize(list);
            return json;
        }

        public ActionResult StokYaslandirmaRapor()
        {

            return View();
        }
        public PartialViewResult StokYaslandirmaRapor_List()
        {
            return PartialView();
        }
        public string StokYaslandirmaRaporCek(string MalKoduBas, string MalKoduBit, string DepoBas, string DepoBit)
        {
            List<MySti> list = new List<MySti>();
            List<MySti> RT = new List<MySti>();
            try
            {
                string deposql = "";
                if (!string.IsNullOrEmpty(DepoBas) && !string.IsNullOrEmpty(DepoBit))
                    deposql = string.Format("AND DST.Depo BETWEEN '{0}' AND '{1}'", DepoBas, DepoBit);
                else if (string.IsNullOrEmpty(DepoBas) && !string.IsNullOrEmpty(DepoBit))
                    deposql = string.Format("AND DST.Depo <= '{0}'", DepoBit);
                else if (!string.IsNullOrEmpty(DepoBas) && string.IsNullOrEmpty(DepoBit))
                    deposql = string.Format("AND DST.Depo >= '{0}'", DepoBas);

                string malsql = "";
                if (!string.IsNullOrEmpty(MalKoduBas) && !string.IsNullOrEmpty(MalKoduBit))
                    malsql = string.Format("AND STK.MalKodu BETWEEN '{0}' AND '{1}'", MalKoduBas, MalKoduBit);
                else if (string.IsNullOrEmpty(MalKoduBas) && !string.IsNullOrEmpty(MalKoduBit))
                    malsql = string.Format("AND STK.MalKodu <= '{0}'", MalKoduBit);
                else if (!string.IsNullOrEmpty(MalKoduBas) && string.IsNullOrEmpty(MalKoduBit))
                    malsql = string.Format("AND STK.MalKodu >= '{0}'", MalKoduBas);

                string sorgu = string.Format(
                                            @"SELECT STI.EvrakNo, (CASE STI.KynkEvrakTip WHEN 58 THEN STI.VadeTarih ELSE STI.Tarih END) as Tarih
											, ISNULL(STI.CHK,'') AS CHK, ISNULL(CHK.Unvan1,'') as Unvan, STI.Birim, STI.BirimMiktar, STI.KynkEvrakTip 
											, (DST.DvrMiktar+DST.GirMiktar-DST.CikMiktar) as DepoStokMiktar, DST.Depo, DEP.DepoAdi
											, STI.MalKodu, STK.MalAdi
											FROM FINSAT6{0}.FINSAT6{0}.STI (nolock)
											INNER JOIN FINSAT6{0}.FINSAT6{0}.DST (nolock) ON DST.Depo=STI.Depo AND DST.MalKodu=STI.MalKodu
											LEFT JOIN FINSAT6{0}.FINSAT6{0}.CHK (nolock) ON CHK.HesapKodu=STI.Chk
											INNER JOIN FINSAT6{0}.FINSAT6{0}.STK (nolock) ON STK.MalKodu=STI.MalKodu
											INNER JOIN FINSAT6{0}.FINSAT6{0}.DEP (nolock) ON DST.Depo=DEP.Depo
											WHERE (STI.KynkEvrakTip=4 OR (STI.KynkEvrakTip=58 AND STI.IslemTip=0)) 
											{1} {2} AND STI.BirimMiktar>0 AND DST.Depo LIKE 'H%'
											AND (DST.DvrMiktar+DST.GirMiktar-DST.CikMiktar)>0
											ORDER BY STI.MalKodu, STI.Tarih DESC", "17", deposql, malsql);


                RT = db.Database.SqlQuery<MySti>(sorgu).ToList();
                Dictionary<string, decimal> dic = new Dictionary<string, decimal>();
                foreach (MySti item in RT)
                {
                    string depo = item.Depo.ToString();
                    string malKodu = item.MalKodu.ToString();
                    string key = string.Format("{0}-{1}", depo, malKodu);

                    decimal depoStokMik = (decimal)item.DepoStokMiktar;

                    if (dic.ContainsKey(key))
                    {
                        if (dic[key] >= depoStokMik) //depoStokMik
                            continue;
                    }
                    else
                        dic.Add(key, 0);



                    MySti sti = new MySti
                    {
                        EvrakNo = item.EvrakNo.ToString(),
                        Tarih = (int)item.Tarih,
                        Chk = item.Chk.ToString(),
                        Unvan = item.Unvan.ToString(),
                        Birim = item.Birim.ToString(),
                        BirimMiktar = (decimal)item.BirimMiktar,
                        Depo = depo,

                        MalKodu = malKodu,
                        MalAdi = item.MalAdi.ToString(),
                        KynkEvrakTip = (short)item.KynkEvrakTip,

                        Kod13 = depoStokMik// depoStokMik;
                    };

                    sti.Kod1 = String.Format("{0}       Depo Stok Miktarı:  {1:N2}", sti.MalKodu, sti.Kod13);

                    sti.Kod2 = string.Format("{0}    {1}", depo, item.DepoAdi);

                    dic[key] += sti.BirimMiktar;

                    list.Add(sti);

                    if (dic[key] > depoStokMik) //depoStokMik
                    {
                        decimal fark = dic[key] - depoStokMik;//depoStokMik;
                        sti.BirimMiktar -= fark;
                    }
                }





            }
            catch (Exception ex)
            {
                throw ex;
            }


            var json = new JavaScriptSerializer().Serialize(list);
            return json;

        }
    }
}