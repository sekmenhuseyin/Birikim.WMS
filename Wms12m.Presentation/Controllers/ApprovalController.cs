using KurumsalKaynakPlanlaması12M;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Wms12m.Entity;
using Wms12m.Entity.Viewmodels;

namespace Wms12m.Presentation.Controllers
{
    public static class MyGlobalVariables
    {
        public static KKPEvrakSiparis SipEvrak { get; set; }
        public static List<MMK> TesisList { get; set; }
        public static List<SatTalep> SipTalepList { get; set; }
        public static List<SatTalep> TalepSource { get; set; }
        public static List<KKP_SPI> GridSource { get; set; }
        public static List<KKP_FTD> GridFTD { get; set; }
        public static bool DovizDurum { get; set; }

    }

    public class ApprovalController : RootController
    {
        
        #region Çek
        public ActionResult Cek_SPGMY()
        {
            if (CheckPerm("Çek Onaylama", PermTypes.Reading) == false) return Redirect("/");
            return View();
        }
        public PartialViewResult Cek_SPGMY_List()
        {
            if (CheckPerm("Çek Onaylama", PermTypes.Reading) == false) return null;
            return PartialView();
        }
        public string CekSPGMYOnayCek()
        {
            if (CheckPerm("Çek Onaylama", PermTypes.Reading) == false) return null;
            var RT = db.Database.SqlQuery<CekOnaySelect>(string.Format("[FINSAT6{0}].[wms].[CekOnaySPGMY]", "17")).ToList();
            var json = new JavaScriptSerializer().Serialize(RT);
            return json;
        }

        public ActionResult Cek_MIGMY()
        {
            if (CheckPerm("Çek Onaylama", PermTypes.Reading) == false) return Redirect("/");
            return View();
        }
        public PartialViewResult Cek_MIGMY_List()
        {
            if (CheckPerm("Çek Onaylama", PermTypes.Reading) == false) return null;
            return PartialView();
        }
        public string CekMIGMYOnayCek()
        {
            if (CheckPerm("Çek Onaylama", PermTypes.Reading) == false) return null;
            var RT = db.Database.SqlQuery<CekOnaySelect>(string.Format("[FINSAT6{0}].[wms].[CekOnayMIGMY]", "17")).ToList();
            var json = new JavaScriptSerializer().Serialize(RT);
            return json;
        }

        public ActionResult Cek_GM()
        {
            if (CheckPerm("Çek Onaylama", PermTypes.Reading) == false) return Redirect("/");
            return View();
        }
        public PartialViewResult Cek_GM_List()
        {
            if (CheckPerm("Çek Onaylama", PermTypes.Reading) == false) return null;
            return PartialView();
        }
        public string CekGMOnayCek()
        {
            if (CheckPerm("Çek Onaylama", PermTypes.Reading) == false) return null;
            var RT = db.Database.SqlQuery<CekOnaySelect>(string.Format("[FINSAT6{0}].[wms].[CekOnayGM]", "17")).ToList();
            var json = new JavaScriptSerializer().Serialize(RT);
            return json;
        }
        public JsonResult Cek_Onay_MIGMY(string Data)
        {
            Result _Result = new Result(true);
            if (CheckPerm("Çek Onaylama", PermTypes.Writing) == false) return null;
            //string sql = string.Format(@"INSERT INTO FINSAT";
            JArray parameters = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JArray>(Request["Data"]);
            //JValue parameters = JsonConvert.<Newtonsoft.Json.Linq.JValue>(JsonConvert.SerializeObject(Data));
            SqlExper sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, "17");

            try
            {
                Dictionary<string, int> FiyatMaxSiraNo = new Dictionary<string, int>();
                foreach (JObject insertObj in parameters)
                {
                    // var fytData = db.Database.SqlQuery<FYT>(string.Format("SELECT ISNULL(Max(FiyatListName),'') as FiyatListName,ISNULL(Max(BasTarih),0) as BasTarih,ISNULL(Max(BitTarih),0) as BitTarih FROM[FINSAT6{0}].[FINSAT6{0}].[FYT] where FiyatListNum = '{1}'", "17", insertObj["FiyatListNum"].ToString())).FirstOrDefault();
                    DateTime date = DateTime.Now;
                    var shortDate = date.ToString("yyyy-MM-dd");
                    var sonuc = sqlexper.AcceptChanges();
                    db.Database.ExecuteSqlCommand(string.Format("UPDATE [FINSAT6{0}].[FINSAT6{0}].[CEK] SET MIGMYOnay = 1, MIGMYOnaylayan='" + vUser.UserName + "', MIGMYOnayTarih='{2}'  where ID = '{1}'", "17", insertObj["ID"].ToString(), shortDate));




                }
                //return "OK";

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
        public JsonResult Cek_Onay_SPGMY(string Data)
        {
            Result _Result = new Result(true);
            if (CheckPerm("Çek Onaylama", PermTypes.Writing) == false) return null;
            //string sql = string.Format(@"INSERT INTO FINSAT";
            JArray parameters = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JArray>(Request["Data"]);
            //JValue parameters = JsonConvert.<Newtonsoft.Json.Linq.JValue>(JsonConvert.SerializeObject(Data));
            SqlExper sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, "17");

            try
            {
                Dictionary<string, int> FiyatMaxSiraNo = new Dictionary<string, int>();
                foreach (JObject insertObj in parameters)
                {
                    // var fytData = db.Database.SqlQuery<FYT>(string.Format("SELECT ISNULL(Max(FiyatListName),'') as FiyatListName,ISNULL(Max(BasTarih),0) as BasTarih,ISNULL(Max(BitTarih),0) as BitTarih FROM[FINSAT6{0}].[FINSAT6{0}].[FYT] where FiyatListNum = '{1}'", "17", insertObj["FiyatListNum"].ToString())).FirstOrDefault();
                    DateTime date = DateTime.Now;
                    var shortDate = date.ToString("yyyy-MM-dd");
                    var sonuc = sqlexper.AcceptChanges();
                    db.Database.ExecuteSqlCommand(string.Format("UPDATE [FINSAT6{0}].[FINSAT6{0}].[CEK] SET SPGMYOnay = 1, SPGMYOnaylayan='" + vUser.UserName + "', SPGMYOnayTarih='{2}'  where ID = '{1}'", "17", insertObj["ID"].ToString(), shortDate));




                }
                //return "OK";

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
        public JsonResult Cek_Onay_GM(string Data)
        {
            Result _Result = new Result(true);
            if (CheckPerm("Çek Onaylama", PermTypes.Writing) == false) return null;
            //string sql = string.Format(@"INSERT INTO FINSAT";
            JArray parameters = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JArray>(Request["Data"]);
            //JValue parameters = JsonConvert.<Newtonsoft.Json.Linq.JValue>(JsonConvert.SerializeObject(Data));
            SqlExper sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, "17");

            try
            {
                Dictionary<string, int> FiyatMaxSiraNo = new Dictionary<string, int>();
                foreach (JObject insertObj in parameters)
                {
                    // var fytData = db.Database.SqlQuery<FYT>(string.Format("SELECT ISNULL(Max(FiyatListName),'') as FiyatListName,ISNULL(Max(BasTarih),0) as BasTarih,ISNULL(Max(BitTarih),0) as BitTarih FROM[FINSAT6{0}].[FINSAT6{0}].[FYT] where FiyatListNum = '{1}'", "17", insertObj["FiyatListNum"].ToString())).FirstOrDefault();
                    DateTime date = DateTime.Now;
                    var shortDate = date.ToString("yyyy-MM-dd");
                    var sonuc = sqlexper.AcceptChanges();
                    db.Database.ExecuteSqlCommand(string.Format("UPDATE [FINSAT6{0}].[FINSAT6{0}].[CEK] SET GMOnay = 1, GMOnaylayan='" + vUser.UserName + "', GMOnayTarih='{2}'  where ID = '{1}'", "17", insertObj["ID"].ToString(), shortDate));




                }
                //return "OK";

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

        public JsonResult Cek_Red_MIGMY(string Data)
        {
            Result _Result = new Result(true);
            if (CheckPerm("Çek Onaylama", PermTypes.Writing) == false) return null;
            //string sql = string.Format(@"INSERT INTO FINSAT";
            JArray parameters = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JArray>(Request["Data"]);
            //JValue parameters = JsonConvert.<Newtonsoft.Json.Linq.JValue>(JsonConvert.SerializeObject(Data));
            SqlExper sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, "17");

            try
            {
                Dictionary<string, int> FiyatMaxSiraNo = new Dictionary<string, int>();
                foreach (JObject insertObj in parameters)
                {
                    // var fytData = db.Database.SqlQuery<FYT>(string.Format("SELECT ISNULL(Max(FiyatListName),'') as FiyatListName,ISNULL(Max(BasTarih),0) as BasTarih,ISNULL(Max(BitTarih),0) as BitTarih FROM[FINSAT6{0}].[FINSAT6{0}].[FYT] where FiyatListNum = '{1}'", "17", insertObj["FiyatListNum"].ToString())).FirstOrDefault();
                    DateTime date = DateTime.Now;
                    var shortDate = date.ToString("yyyy-MM-dd");
                    var sonuc = sqlexper.AcceptChanges();
                    db.Database.ExecuteSqlCommand(string.Format("UPDATE [FINSAT6{0}].[FINSAT6{0}].[CEK] SET MIGMYOnay = 0, MIGMYOnaylayan='" + vUser.UserName + "', MIGMYOnayTarih='{2}', Durum = 1 where ID = '{1}'", "17", insertObj["ID"].ToString(), shortDate));




                }
                //return "OK";

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
        public JsonResult Cek_Red_SPGMY(string Data)
        {
            Result _Result = new Result(true);
            if (CheckPerm("Çek Onaylama", PermTypes.Writing) == false) return null;
            //string sql = string.Format(@"INSERT INTO FINSAT";
            JArray parameters = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JArray>(Request["Data"]);
            //JValue parameters = JsonConvert.<Newtonsoft.Json.Linq.JValue>(JsonConvert.SerializeObject(Data));
            SqlExper sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, "17");

            try
            {
                Dictionary<string, int> FiyatMaxSiraNo = new Dictionary<string, int>();
                foreach (JObject insertObj in parameters)
                {
                    // var fytData = db.Database.SqlQuery<FYT>(string.Format("SELECT ISNULL(Max(FiyatListName),'') as FiyatListName,ISNULL(Max(BasTarih),0) as BasTarih,ISNULL(Max(BitTarih),0) as BitTarih FROM[FINSAT6{0}].[FINSAT6{0}].[FYT] where FiyatListNum = '{1}'", "17", insertObj["FiyatListNum"].ToString())).FirstOrDefault();
                    DateTime date = DateTime.Now;
                    var shortDate = date.ToString("yyyy-MM-dd");
                    var sonuc = sqlexper.AcceptChanges();
                    db.Database.ExecuteSqlCommand(string.Format("UPDATE [FINSAT6{0}].[FINSAT6{0}].[CEK] SET SPGMYOnay = 0, SPGMYOnaylayan='" + vUser.UserName + "', SPGMYOnayTarih='{2}', Durum = 1 where ID = '{1}'", "17", insertObj["ID"].ToString(), shortDate));




                }
                //return "OK";

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
        public JsonResult Cek_Red_GM(string Data)
        {
            Result _Result = new Result(true);
            if (CheckPerm("Çek Onaylama", PermTypes.Writing) == false) return null;
            //string sql = string.Format(@"INSERT INTO FINSAT";
            JArray parameters = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JArray>(Request["Data"]);
            //JValue parameters = JsonConvert.<Newtonsoft.Json.Linq.JValue>(JsonConvert.SerializeObject(Data));
            SqlExper sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, "17");

            try
            {
                Dictionary<string, int> FiyatMaxSiraNo = new Dictionary<string, int>();
                foreach (JObject insertObj in parameters)
                {
                    // var fytData = db.Database.SqlQuery<FYT>(string.Format("SELECT ISNULL(Max(FiyatListName),'') as FiyatListName,ISNULL(Max(BasTarih),0) as BasTarih,ISNULL(Max(BitTarih),0) as BitTarih FROM[FINSAT6{0}].[FINSAT6{0}].[FYT] where FiyatListNum = '{1}'", "17", insertObj["FiyatListNum"].ToString())).FirstOrDefault();
                    DateTime date = DateTime.Now;
                    var shortDate = date.ToString("yyyy-MM-dd");
                    var sonuc = sqlexper.AcceptChanges();
                    db.Database.ExecuteSqlCommand(string.Format("UPDATE [FINSAT6{0}].[FINSAT6{0}].[CEK] SET GMOnay = 0, GMOnaylayan='" + vUser.UserName + "', GMOnayTarih='{2}', Durum = 1 where ID = '{1}'", "17", insertObj["ID"].ToString(), shortDate));




                }
                //return "OK";

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
        #endregion
        #region Fiyat
        public ActionResult Fiyat_GM()
        {
            if (CheckPerm("Fiyat Onaylama", PermTypes.Reading) == false) return Redirect("/");
            return View();
        }
        public PartialViewResult Fiyat_GM_List()
        {
            if (CheckPerm("Fiyat Onaylama", PermTypes.Reading) == false) return null;
            return PartialView();
        }
        public string FiyatGMOnayCek()
        {
            if (CheckPerm("Fiyat Onaylama", PermTypes.Reading) == false) return null;
            var RT = db.Database.SqlQuery<FiyatOnayGMSelect>(string.Format("[FINSAT6{0}].[wms].[FiyatOnayListGM]", "17")).ToList();
            var json = new JavaScriptSerializer().Serialize(RT);
            return json;
        }

        public PartialViewResult Fiyat_GM_List_Koleksiyon()
        {
            if (CheckPerm("Fiyat Onaylama", PermTypes.Reading) == false) return null;
            return PartialView();
        }
        public string FiyatGMOnayCekGMKoleksiyon()
        {
            if (CheckPerm("Fiyat Onaylama", PermTypes.Reading) == false) return null;
            var RT = db.Database.SqlQuery<FiyatKoleksiyonSelect>(string.Format("[FINSAT6{0}].[wms].[FiyatOnayListGM_Koleksiyon]", "17")).ToList();
            var json = new JavaScriptSerializer().Serialize(RT);
            return json;
        }

        public PartialViewResult Fiyat_GM_List_Grup()
        {
            if (CheckPerm("Fiyat Onaylama", PermTypes.Reading) == false) return null;
            return PartialView();
        }
        public string FiyatGMOnayCekGMGrup()
        {
            if (CheckPerm("Fiyat Onaylama", PermTypes.Reading) == false) return null;
            var RT = db.Database.SqlQuery<FiyatGrupSelect>(string.Format("[FINSAT6{0}].[wms].[FiyatOnayListGM_GrupEbatYuzey]", "17")).ToList();
            var json = new JavaScriptSerializer().Serialize(RT);
            return json;
        }

        public ActionResult Fiyat_SPGMY()
        {
            if (CheckPerm("Fiyat Onaylama", PermTypes.Reading) == false) return Redirect("/");
            return View();
        }
        public PartialViewResult Fiyat_SPGMY_List()
        {
            if (CheckPerm("Fiyat Onaylama", PermTypes.Reading) == false) return null;
            return PartialView();
        }
        public string FiyatSPGMYOnayCek()
        {
            if (CheckPerm("Fiyat Onaylama", PermTypes.Reading) == false) return null;
            var RT = db.Database.SqlQuery<FiyatOnayGMSelect>(string.Format("[FINSAT6{0}].[wms].[FiyatOnayListSPGMY]", "17")).ToList();
            var json = new JavaScriptSerializer().Serialize(RT);
            return json;
        }

        public PartialViewResult Fiyat_SPGMY_List_Koleksiyon()
        {
            if (CheckPerm("Fiyat Onaylama", PermTypes.Reading) == false) return null;
            return PartialView();
        }
        public string FiyatSPGMYOnayCekSPGMYKoleksiyon()
        {
            if (CheckPerm("Fiyat Onaylama", PermTypes.Reading) == false) return null;
            var RT = db.Database.SqlQuery<FiyatKoleksiyonSelect>(string.Format("[FINSAT6{0}].[wms].[FiyatOnayListSPGMY_Koleksiyon]", "17")).ToList();
            var json = new JavaScriptSerializer().Serialize(RT);
            return json;
        }


        public PartialViewResult Fiyat_SPGMY_List_Grup()
        {
            if (CheckPerm("Fiyat Onaylama", PermTypes.Reading) == false) return null;
            return PartialView();
        }
        public string FiyatSPGMYOnayCekSPGMYGrup()
        {
            if (CheckPerm("Fiyat Onaylama", PermTypes.Reading) == false) return null;
            var RT = db.Database.SqlQuery<FiyatGrupSelect>(string.Format("[FINSAT6{0}].[wms].[FiyatOnayListSPGMY_GrupEbatYuzey]", "17")).ToList();
            var json = new JavaScriptSerializer().Serialize(RT);
            return json;
        }

        public ActionResult Fiyat_SM()
        {
            if (CheckPerm("Fiyat Onaylama", PermTypes.Reading) == false) return Redirect("/");
            return View();
        }
        public PartialViewResult Fiyat_SM_List()
        {
            if (CheckPerm("Fiyat Onaylama", PermTypes.Reading) == false) return null;
            return PartialView();
        }
        public string FiyatSMOnayCek()
        {
            if (CheckPerm("Fiyat Onaylama", PermTypes.Reading) == false) return null;
            var RT = db.Database.SqlQuery<FiyatOnayGMSelect>(string.Format("[FINSAT6{0}].[wms].[FiyatOnayList]", "17")).ToList();
            var json = new JavaScriptSerializer().Serialize(RT);
            return json;
        }

        public PartialViewResult Fiyat_SM_List_Koleksiyon()
        {
            if (CheckPerm("Fiyat Onaylama", PermTypes.Reading) == false) return null;
            return PartialView();
        }
        public string FiyatSMOnayCekSMKoleksiyon()
        {
            if (CheckPerm("Fiyat Onaylama", PermTypes.Reading) == false) return null;
            var RT = db.Database.SqlQuery<FiyatKoleksiyonSelect>(string.Format("[FINSAT6{0}].[wms].[FiyatOnayList_Koleksiyon]", "17")).ToList();
            var json = new JavaScriptSerializer().Serialize(RT);
            return json;
        }

        public PartialViewResult Fiyat_SM_List_Grup()
        {
            if (CheckPerm("Fiyat Onaylama", PermTypes.Reading) == false) return null;
            return PartialView();
        }
        public string FiyatSMOnayCekSMGrup()
        {
            if (CheckPerm("Fiyat Onaylama", PermTypes.Reading) == false) return null;
            var RT = db.Database.SqlQuery<FiyatGrupSelect>(string.Format("[FINSAT6{0}].[wms].[FiyatOnayList_GrupEbatYuzey]", "17")).ToList();
            var json = new JavaScriptSerializer().Serialize(RT);
            return json;
        }


        public ActionResult FiyatListesi()
        {
            if (CheckPerm("Fiyat Onaylama", PermTypes.Reading) == false) return Redirect("/");
            var LNO = db.Database.SqlQuery<ListeNoSelect>(string.Format("[FINSAT6{0}].[dbo].[FYTSelect2]", "17")).ToList();
            return View(LNO);
        }

        public string FiyatUrunGrupSelect()
        {
            var FUGS = db.Database.SqlQuery<FiyatUrunGrupSelect>(string.Format("[FINSAT6{0}].[dbo].[STKSelect1]", "17")).ToList();
            var json = new JavaScriptSerializer().Serialize(FUGS);
            return json;
        }

        public string FiyatHesapKoduSelect()
        {
            var FHKS = db.Database.SqlQuery<RaporCHKSelect>(string.Format("[FINSAT6{0}].[dbo].[CHKSelect1]", "17")).ToList();
            var json = new JavaScriptSerializer().Serialize(FHKS);
            return json;
        }

        public PartialViewResult PartialFiyatList(string listeNo)
        {
            if (CheckPerm("Fiyat Onaylama", PermTypes.Reading) == false) return null;
            //var TMNT = db.Database.SqlQuery<TeminatSelect>(string.Format("[FINSAT6{0}].[dbo].[TeminatOnaySelect]", "17")).ToList();
            ViewBag.ListeNo = listeNo;
            return PartialView("FiyatListesiPartial");
        }

        public string FiyatListesiSelect(string listeNo)
        {
            var FYTS = db.Database.SqlQuery<FiyatListSelect>(string.Format("[FINSAT6{0}].[dbo].[FYTSelect1] @ListeNo='{1}'", "17", listeNo)).ToList();
            var json = new JavaScriptSerializer().Serialize(FYTS);
            return json;
        }

        public string FiyatListesiBekleyen()
        {
            var FLB = db.Database.SqlQuery<BekleyenFiyatListesi>(string.Format("[FINSAT6{0}].[wms].[FiyatOnayListTumBekleyenler]", "17")).ToList();
            var json = new JavaScriptSerializer().Serialize(FLB);
            return json;
        }

        public JsonResult Fiyat_Onay(string Data)//GM
        {
            Result _Result = new Result(true);
            if (CheckPerm("Fiyat Onaylama", PermTypes.Writing) == false) return null;
            //string sql = string.Format(@"INSERT INTO FINSAT";
            JArray parameters = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JArray>(Request["Data"]);
            //JValue parameters = JsonConvert.<Newtonsoft.Json.Linq.JValue>(JsonConvert.SerializeObject(Data));
            SqlExper sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, "17");
           
            int BasTarih = 0;
            int BitTarih = 0;
            string ListeAdi = "";//int FytSiraNo = -1;
            try
            {
                Dictionary<string, int> FiyatMaxSiraNo = new Dictionary<string, int>();
                foreach (JObject insertObj in parameters)
                {

                    if (FiyatMaxSiraNo.ContainsKey(insertObj["FiyatListNum"].ToString()))
                    {
                        continue;
                    }
                    else
                    {
                        int FytSiraNo = -1;
                        var FLB = db.Database.SqlQuery<short>(string.Format("SELECT ISNULL(MAX(SiraNo),0) FROM [FINSAT6{0}].[FINSAT6{0}].[FYT] WHERE FiyatListNum='{1}'", "17", insertObj["FiyatListNum"].ToString())).FirstOrDefault();
                        if (FLB > 0)
                        {
                            FytSiraNo = FLB;
                            FytSiraNo++;
                        }
                        else
                        {
                            FytSiraNo = 0;
                        }
                        FiyatMaxSiraNo.Add(insertObj["FiyatListNum"].ToString(), FytSiraNo);
                    }
                    if (insertObj["Durum"].ToString() == "Yeni Kayıt")
                    {
                        var fytData = db.Database.SqlQuery<FYT>(string.Format("SELECT ISNULL(Max(FiyatListName),'') as FiyatListName,ISNULL(Max(BasTarih),0) as BasTarih,ISNULL(Max(BitTarih),0) as BitTarih FROM[FINSAT6{0}].[FINSAT6{0}].[FYT] where FiyatListNum = '{1}'", "17", insertObj["FiyatListNum"].ToString())).FirstOrDefault();
                        //var bastarih = db.Database.SqlQuery<FiyatListSelect>(string.Format("SELECT Max(BasTarih)as bastarih FROM[FINSAT6{0}].[FINSAT6{0}].[FYT] where FiyatListNum = '{1}'", "33", insertObj["FiyatListNum"].ToString())).ToList();
                        //var bittarih = db.Database.SqlQuery<FiyatListSelect>(string.Format("SELECT Max(BitTarih)as bittarih FROM[FINSAT6{0}].[FINSAT6{0}].[FYT] where FiyatListNum = '{1}'", "33", insertObj["FiyatListNum"].ToString())).ToList();

                        FYT fyt = new FYT();

                        if (fytData.FiyatListNum != null)
                        {
                            ListeAdi = fytData.FiyatListName;
                            BasTarih = fytData.BasTarih;
                            BitTarih = fytData.BitTarih;

                        }
                        else
                        {
                            ListeAdi = insertObj["FiyatListNum"].ToString() + " Fiyat Listesi";
                            if (ListeAdi.Length > 30)
                            {
                                ListeAdi = ListeAdi.Substring(0, 30);
                            }
                            BasTarih = insertObj["BasTarih"].ToInt32();
                            BitTarih = insertObj["BitTarih"].ToInt32();

                        }

                        fyt.MalKodu = insertObj["MalKodu"].ToString();
                        fyt.BasTarih = BasTarih;
                        fyt.BasSaat = 0;
                        fyt.BitTarih = BitTarih;
                        fyt.BitSaat = 0;

                        if (FiyatMaxSiraNo.ContainsKey(insertObj["FiyatListNum"].ToString()))
                        {
                            fyt.SiraNo = Convert.ToInt16(FiyatMaxSiraNo[insertObj["FiyatListNum"].ToString()]);
                            FiyatMaxSiraNo[insertObj["FiyatListNum"].ToString()] = FiyatMaxSiraNo[insertObj["FiyatListNum"].ToString()]++;
                        }
                        else
                        {
                            fyt.SiraNo = 0;
                        }

                        fyt.RenkBedenKod1 = "";
                        fyt.RenkBedenKod2 = "";
                        fyt.RenkBedenKod3 = "";
                        fyt.RenkBedenKod4 = "";
                        fyt.FiyatListNum = insertObj["FiyatListNum"].ToString();
                        fyt.FiyatTuru = 1;
                        fyt.Kod1 = "";
                        fyt.Kod2 = "";
                        fyt.Kod3 = "";
                        fyt.KodTipi = 0;
                        fyt.MusteriKodu = insertObj["HesapKodu"].ToString();
                        fyt.AlisFiyat1 = 0;
                        fyt.AlisFiyat2 = 0;
                        fyt.AlisFiyat3 = 0;
                        fyt.DvzAlisFiyat1 = 0;
                        fyt.DvzAlisFiyat2 = 0;
                        fyt.DvzAlisFiyat3 = 0;
                        fyt.AF1DovizCinsi = "";
                        fyt.AF2DovizCinsi = "";
                        fyt.AF3DovizCinsi = "";
                        fyt.AF1KDV = 0;
                        fyt.AF2KDV = 0;
                        fyt.AF3KDV = 0;
                        fyt.DovizAF1KDV = 0;
                        fyt.DovizAF2KDV = 0;
                        fyt.DovizAF3KDV = 0;
                        fyt.AF1Birim = 0;
                        fyt.AF2Birim = 0;
                        fyt.AF3Birim = 0;
                        fyt.DovizAF1Birim = 0;
                        fyt.DovizAF2Birim = 0;
                        fyt.DovizAF3Birim = 0;
                        fyt.SatisFiyat1 = insertObj["SatisFiyat1"].ToDecimal();
                        fyt.SatisFiyat2 = 0;
                        fyt.SatisFiyat3 = 0;
                        fyt.SatisFiyat4 = 0;
                        fyt.SatisFiyat5 = 0;
                        fyt.SatisFiyat6 = 0;
                        fyt.DvzSatisFiyat1 = insertObj["DovizSatisFiyat1"].ToDecimal();
                        fyt.DvzSatisFiyat2 = 0;
                        fyt.DvzSatisFiyat3 = 0;
                        fyt.SF1DovizCinsi = insertObj["DovizCinsi"].ToString().Trim();
                        fyt.SF2DovizCinsi = "";
                        fyt.SF3DovizCinsi = "";
                        fyt.SF4DovizCinsi = "";
                        fyt.SF5DovizCinsi = "";
                        fyt.SF6DovizCinsi = "";
                        fyt.SF1KDV = 0;
                        fyt.SF2KDV = 0;
                        fyt.SF3KDV = 0;
                        fyt.SF4KDV = 0;
                        fyt.SF5KDV = 0;
                        fyt.SF6KDV = 0;
                        fyt.DovizSF1KDV = 0;
                        fyt.DovizSF2KDV = 0;
                        fyt.DovizSF3KDV = 0;
                        fyt.SF1Birim = insertObj["SF1Birim"].ToShort();
                        fyt.SF2Birim = -1;
                        fyt.SF3Birim = -1;
                        fyt.SF4Birim = -1;
                        fyt.SF5Birim = -1;
                        fyt.SF6Birim = -1;
                        fyt.DovizSF1Birim = insertObj["DovizSF1BirimInt"].ToShort();
                        fyt.DovizSF2Birim = -1;
                        fyt.DovizSF3Birim = -1;
                        fyt.FiyatListName = ListeAdi;
                        fyt.SatisFiyatAltLimit = 0;
                        fyt.SatisFiyatUstLimit = 0;
                        fyt.SF1ValorGun = 0;
                        fyt.SF2ValorGun = 0;
                        fyt.SF3ValorGun = 0;
                        fyt.SF4ValorGun = 0;
                        fyt.SF5ValorGun = 0;
                        fyt.SF6ValorGun = 0;
                        fyt.DvzSF1ValorGun = 0;
                        fyt.DvzSF2ValorGun = 0;
                        fyt.DvzSF3ValorGun = 0;
                        fyt.KayitTuru = -1;
                        fyt.GuvenlikKod = "";
                        fyt.Kaydeden = vUser.UserName.ToString();
                        fyt.KayitTarih = (int)DateTime.Now.ToOADate();
                        fyt.KayitSaat = 0;
                        fyt.KayitKaynak = 117;
                        fyt.KayitSurum = "8.10.100";
                        fyt.Degistiren = vUser.UserName.ToString();
                        fyt.DegisTarih = (int)DateTime.Now.ToOADate();
                        fyt.DegisSaat = 0;
                        fyt.DegisKaynak = 117;
                        fyt.DegisSurum = "8.10.100";
                        fyt.CheckSum = 12;
                        fyt.Aciklama = "";
                        fyt.Kod4 = "";
                        fyt.Kod5 = "";
                        fyt.Kod6 = "";
                        fyt.Kod7 = "";
                        fyt.Kod8 = "";
                        fyt.Kod9 = "";
                        fyt.Kod10 = "";
                        fyt.Kod11 = 0;
                        fyt.Kod12 = 0;


                        DateTime date = DateTime.Now;
                        var shortDate = date.ToString("yyyy-MM-dd");
                        sqlexper.Insert(fyt);
                        var sonuc = sqlexper.AcceptChanges();
                        db.Database.ExecuteSqlCommand(string.Format("UPDATE [FINSAT6{0}].[FINSAT6{0}].[Fiyat] SET GMOnay = 1, GMOnaylayan='" + vUser.UserName + "', GMOnayTarih='{2}'  where ID = '{1}'", "17", insertObj["ID"].ToString(), shortDate));
                    }
                    else if (insertObj["Durum"].ToString() == "Silinecek Kayıt")
                    {
                        string durum2sql = string.Format("SELECT * FROM [FINSAT6{0}].[FINSAT6{0}].[FYT] WHERE ROW_ID = {1}", "17", insertObj["ROW_ID"].ToInt32());
                        var fytSilinecek = db.Database.SqlQuery<FYT>(durum2sql).ToList();
                        if (fytSilinecek.Count > 0)
                        {
                            db.Database.ExecuteSqlCommand(string.Format("DELETE FROM [FINSAT6{0}].[FINSAT6{0}].[FYT] WHERE ROW_ID = {1}", "17", insertObj["ROW_ID"].ToInt32()));
                        }
                        DateTime date = DateTime.Now;
                        var shortDate = date.ToString("yyyy-MM-dd");
                        db.Database.ExecuteSqlCommand(string.Format("UPDATE [FINSAT6{0}].[FINSAT6{0}].[Fiyat] SET GMOnay = 1, GMOnaylayan='" + vUser.UserName + "', GMOnayTarih='{2}'  where ID = '{1}'", "17", insertObj["ID"].ToString(), shortDate));


                    }
                    else if (insertObj["Durum"].ToString() == "Güncellenecek Kayıt")
                    {
                        string durum3sql = string.Format("SELECT * FROM [FINSAT6{0}].[FINSAT6{0}].[FYT] WHERE ROW_ID = {1}", "17", insertObj["ROW_ID"].ToInt32());
                        var fytGuncellenecek = db.Database.SqlQuery<FYT>(durum3sql).FirstOrDefault();
                        if (fytGuncellenecek != null)
                        {
                            fytGuncellenecek.SatisFiyat1 = insertObj["SatisFiyat1"].ToDecimal();
                            fytGuncellenecek.SF1Birim = insertObj["SatisFiyat1BirimInt"].ToShort();
                            fytGuncellenecek.DvzSatisFiyat1 = insertObj["DvzSatisFiyat1"].ToDecimal();
                            fytGuncellenecek.SF1DovizCinsi = insertObj["DovizCinsi"].ToString().Trim();
                            fytGuncellenecek.DovizSF1Birim = insertObj["DovizSF1BirimInt"].ToShort();
                        }
                        DateTime date = DateTime.Now;
                        var shortDate = date.ToString("yyyy-MM-dd");
                        db.Database.ExecuteSqlCommand(string.Format("UPDATE [FINSAT6{0}].[FINSAT6{0}].[Fiyat] SET GMOnay = 1, GMOnaylayan='" + vUser.UserName + "', GMOnayTarih='{2}'  where ID = '{1}'", "17", insertObj["ID"].ToString(), shortDate));


                    }


                }
                //return "OK";

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
        public JsonResult Fiyat_Onay_Koleksiyon(string Data)//GM
        {
            Result _Result = new Result(true);

            if (CheckPerm("Fiyat Onaylama", PermTypes.Writing) == false) return null;
            //string sql = string.Format(@"INSERT INTO FINSAT";
            JArray parameters = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JArray>(Request["Data"]);
            //JValue parameters = JsonConvert.<Newtonsoft.Json.Linq.JValue>(JsonConvert.SerializeObject(Data));
            SqlExper sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, "17");

            int BasTarih = 0;
            int BitTarih = 0;
            string ListeAdi = "";
            try
            {
                Dictionary<string, int> FiyatMaxSiraNo = new Dictionary<string, int>();
                foreach (JObject insertObj in parameters)
                {
                    if (FiyatMaxSiraNo.ContainsKey(insertObj["FiyatListNum"].ToString()))
                    {
                        continue;
                    }
                    else
                    {
                        int FytSiraNo = -1;
                        var FLB = db.Database.SqlQuery<short>(string.Format("SELECT ISNULL(MAX(SiraNo),0) FROM [FINSAT6{0}].[FINSAT6{0}].[FYT] WHERE FiyatListNum='{1}'", "17", insertObj["FiyatListNum"].ToString())).FirstOrDefault();
                        if (FLB>0)
                        {
                            FytSiraNo = FLB;
                            FytSiraNo++;
                        }
                        else
                        {
                            FytSiraNo = 0;
                        }
                        FiyatMaxSiraNo.Add(insertObj["FiyatListNum"].ToString(), FytSiraNo);
                    }
                    string sql = string.Format(@"SELECT ID from [FINSAT6{0}].[FINSAT6{0}].[Fiyat] F
                    JOIN[FINSAT6{0}].[FINSAT6{0}].[STK] S ON S.MalKodu = F.MalKodu
                    WHERE S.GrupKod = 'PARKE' AND F.FiyatListNum = '{1}'
                    AND S.Kod4 = '{2}' AND S.TipKod = '{3}' AND F.SatisFiyat1 = {4}
                    AND F.SatisFiyat1Birim = '{5}' AND F.SatisFiyat1BirimInt = {6}
                    AND F.DovizSatisFiyat1 = {7} AND F.DovizSF1Birim = '{8}' AND F.DovizSF1BirimInt = {9}
                    AND F.DovizCinsi = '{10}' AND F.Onay = 1 AND F.SPGMYOnay = 1 AND F.GMOnay = 0 ", "17", insertObj["FiyatListNum"].ToString(), insertObj["Kod4"].ToString(),
                    insertObj["TipKod"].ToString(), insertObj["SatisFiyat1"].ToString().Replace(",","."), insertObj["SatisFiyat1Birim"].ToString(), insertObj["SatisFiyat1BirimInt"].ToString(),
                    insertObj["DovizSatisFiyat1"].ToString().Replace(",", "."), insertObj["DovizSF1Birim"].ToString(), insertObj["DovizSF1BirimInt"].ToInt32(), insertObj["DovizCinsi"].ToString()
                    );
                    var fiyatID = db.Database.SqlQuery<int>(sql).ToList();
                    var result = String.Join(", ", fiyatID.ToArray());
                    sql = string.Format("SELECT * FROM [FINSAT6{0}].[FINSAT6{0}].[Fiyat] where ID in({1})", "17", result);
                    var OnayFiyatList = db.Database.SqlQuery<Fiyat>(sql).ToList();
                    foreach (Fiyat rts in OnayFiyatList)
                    {
                        if (rts.Durum == 1) //YENİ KAYIT
                        {

                            var fytData = db.Database.SqlQuery<FYT>(string.Format("SELECT ISNULL(Max(FiyatListName),'') as FiyatListName,ISNULL(Max(BasTarih),0) as BasTarih,ISNULL(Max(BitTarih),0) as BitTarih FROM[FINSAT6{0}].[FINSAT6{0}].[FYT] where FiyatListNum = '{1}'", "17", rts.FiyatListNum.ToString())).FirstOrDefault();
                            //var bastarih = db.Database.SqlQuery<FiyatListSelect>(string.Format("SELECT Max(BasTarih)as bastarih FROM[FINSAT6{0}].[FINSAT6{0}].[FYT] where FiyatListNum = '{1}'", "33", insertObj["FiyatListNum"].ToString())).ToList();
                            //var bittarih = db.Database.SqlQuery<FiyatListSelect>(string.Format("SELECT Max(BitTarih)as bittarih FROM[FINSAT6{0}].[FINSAT6{0}].[FYT] where FiyatListNum = '{1}'", "33", insertObj["FiyatListNum"].ToString())).ToList();

                            FYT fyt = new FYT();

                            if (fytData.FiyatListNum != null)
                            {
                                ListeAdi = fytData.FiyatListName;
                                BasTarih = fytData.BasTarih;
                                BitTarih = fytData.BitTarih;

                            }
                            else
                            {
                                ListeAdi = rts.FiyatListNum.ToString() + " Fiyat Listesi";
                                if (ListeAdi.Length > 30)
                                {
                                    ListeAdi = ListeAdi.Substring(0, 30);
                                }
                                BasTarih = rts.BasTarih.ToInt32();
                                BitTarih = rts.BitTarih.ToInt32();

                            }

                            fyt.MalKodu = rts.MalKodu.ToString();
                            fyt.BasTarih = BasTarih;
                            fyt.BasSaat = 0;
                            fyt.BitTarih = BitTarih;
                            fyt.BitSaat = 0;
                            if (FiyatMaxSiraNo.ContainsKey(insertObj["FiyatListNum"].ToString()))
                            {
                                fyt.SiraNo = Convert.ToInt16(FiyatMaxSiraNo[insertObj["FiyatListNum"].ToString()]);
                                FiyatMaxSiraNo[insertObj["FiyatListNum"].ToString()] = FiyatMaxSiraNo[insertObj["FiyatListNum"].ToString()]++;
                            }
                            else
                            {
                                fyt.SiraNo = 0;
                            }
                            fyt.RenkBedenKod1 = "";
                            fyt.RenkBedenKod2 = "";
                            fyt.RenkBedenKod3 = "";
                            fyt.RenkBedenKod4 = "";
                            fyt.FiyatListNum = rts.FiyatListNum.ToString();
                            fyt.FiyatTuru = 1;
                            fyt.Kod1 = "";
                            fyt.Kod2 = "";
                            fyt.Kod3 = "";
                            fyt.KodTipi = 0;
                            fyt.MusteriKodu = rts.HesapKodu.ToString();
                            fyt.AlisFiyat1 = 0;
                            fyt.AlisFiyat2 = 0;
                            fyt.AlisFiyat3 = 0;
                            fyt.DvzAlisFiyat1 = 0;
                            fyt.DvzAlisFiyat2 = 0;
                            fyt.DvzAlisFiyat3 = 0;
                            fyt.AF1DovizCinsi = "";
                            fyt.AF2DovizCinsi = "";
                            fyt.AF3DovizCinsi = "";
                            fyt.AF1KDV = 0;
                            fyt.AF2KDV = 0;
                            fyt.AF3KDV = 0;
                            fyt.DovizAF1KDV = 0;
                            fyt.DovizAF2KDV = 0;
                            fyt.DovizAF3KDV = 0;
                            fyt.AF1Birim = 0;
                            fyt.AF2Birim = 0;
                            fyt.AF3Birim = 0;
                            fyt.DovizAF1Birim = 0;
                            fyt.DovizAF2Birim = 0;
                            fyt.DovizAF3Birim = 0;
                            fyt.SatisFiyat1 = rts.SatisFiyat1.ToDecimal();
                            fyt.SatisFiyat2 = 0;
                            fyt.SatisFiyat3 = 0;
                            fyt.SatisFiyat4 = 0;
                            fyt.SatisFiyat5 = 0;
                            fyt.SatisFiyat6 = 0;
                            fyt.DvzSatisFiyat1 = rts.DovizSatisFiyat1.ToDecimal();
                            fyt.DvzSatisFiyat2 = 0;
                            fyt.DvzSatisFiyat3 = 0;
                            fyt.SF1DovizCinsi = rts.DovizCinsi.ToString().Trim();
                            fyt.SF2DovizCinsi = "";
                            fyt.SF3DovizCinsi = "";
                            fyt.SF4DovizCinsi = "";
                            fyt.SF5DovizCinsi = "";
                            fyt.SF6DovizCinsi = "";
                            fyt.SF1KDV = 0;
                            fyt.SF2KDV = 0;
                            fyt.SF3KDV = 0;
                            fyt.SF4KDV = 0;
                            fyt.SF5KDV = 0;
                            fyt.SF6KDV = 0;
                            fyt.DovizSF1KDV = 0;
                            fyt.DovizSF2KDV = 0;
                            fyt.DovizSF3KDV = 0;
                            fyt.SF1Birim = rts.SatisFiyat1BirimInt.ToShort(); ;
                            fyt.SF2Birim = -1;
                            fyt.SF3Birim = -1;
                            fyt.SF4Birim = -1;
                            fyt.SF5Birim = -1;
                            fyt.SF6Birim = -1;
                            fyt.DovizSF1Birim = rts.DovizSF1BirimInt.ToShort();
                            fyt.DovizSF2Birim = -1;
                            fyt.DovizSF3Birim = -1;
                            fyt.FiyatListName = ListeAdi;
                            fyt.SatisFiyatAltLimit = 0;
                            fyt.SatisFiyatUstLimit = 0;
                            fyt.SF1ValorGun = 0;
                            fyt.SF2ValorGun = 0;
                            fyt.SF3ValorGun = 0;
                            fyt.SF4ValorGun = 0;
                            fyt.SF5ValorGun = 0;
                            fyt.SF6ValorGun = 0;
                            fyt.DvzSF1ValorGun = 0;
                            fyt.DvzSF2ValorGun = 0;
                            fyt.DvzSF3ValorGun = 0;
                            fyt.KayitTuru = -1;
                            fyt.GuvenlikKod = "";
                            fyt.Kaydeden = vUser.UserName.ToString();
                            fyt.KayitTarih = (int)DateTime.Now.ToOADate();
                            fyt.KayitSaat = 0;
                            fyt.KayitKaynak = 117;
                            fyt.KayitSurum = "8.10.100";
                            fyt.Degistiren = vUser.UserName.ToString();
                            fyt.DegisTarih = (int)DateTime.Now.ToOADate();
                            fyt.DegisSaat = 0;
                            fyt.DegisKaynak = 117;
                            fyt.DegisSurum = "8.10.100";
                            fyt.CheckSum = 12;
                            fyt.Aciklama = "";
                            fyt.Kod4 = "";
                            fyt.Kod5 = "";
                            fyt.Kod6 = "";
                            fyt.Kod7 = "";
                            fyt.Kod8 = "";
                            fyt.Kod9 = "";
                            fyt.Kod10 = "";
                            fyt.Kod11 = 0;
                            fyt.Kod12 = 0;


                            DateTime date = DateTime.Now;
                            var shortDate = date.ToString("yyyy-MM-dd");
                            sqlexper.Insert(fyt);
                            var sonuc = sqlexper.AcceptChanges();
                            db.Database.ExecuteSqlCommand(string.Format("UPDATE [FINSAT6{0}].[FINSAT6{0}].[Fiyat] SET GMOnay = 1, GMOnaylayan='" + vUser.UserName + "', GMOnayTarih='{2}'  where ID = '{1}'", "17", rts.ID, shortDate));



                        }
                        else if (rts.Durum == 2)//Silinecek Kayıt
                        {
                            string durum2sql = string.Format("SELECT * FROM [FINSAT6{0}].[FINSAT6{0}].[FYT] WHERE ROW_ID = {1}", "17", rts.ROW_ID);
                            var fytSilinecek = db.Database.SqlQuery<FYT>(durum2sql).ToList();
                            if (fytSilinecek.Count > 0)
                            {
                                db.Database.ExecuteSqlCommand(string.Format("DELETE FROM [FINSAT6{0}].[FINSAT6{0}].[FYT] WHERE ROW_ID = {1}", "17", rts.ROW_ID));
                            }
                            DateTime date = DateTime.Now;
                            var shortDate = date.ToString("yyyy-MM-dd");
                            db.Database.ExecuteSqlCommand(string.Format("UPDATE [FINSAT6{0}].[FINSAT6{0}].[Fiyat] SET GMOnay = 1, GMOnaylayan='" + vUser.UserName + "', GMOnayTarih='{2}'  where ID = '{1}'", "17", rts.ID, shortDate));
                        }
                        else if (rts.Durum == 3)//"Güncellenecek Kayıt"
                        {
                            string durum3sql = string.Format("SELECT * FROM [FINSAT6{0}].[FINSAT6{0}].[FYT] WHERE ROW_ID = {1}", "17", rts.ROW_ID);
                            var fytGuncellenecek = db.Database.SqlQuery<FYT>(durum3sql).FirstOrDefault();
                            if (fytGuncellenecek!=null)
                            {
                                fytGuncellenecek.SatisFiyat1 = rts.SatisFiyat1;
                                fytGuncellenecek.SF1Birim = (short)rts.SatisFiyat1BirimInt;
                                fytGuncellenecek.DvzSatisFiyat1 = rts.DovizSatisFiyat1;
                                fytGuncellenecek.SF1DovizCinsi = rts.DovizCinsi.Trim();
                                fytGuncellenecek.DovizSF1Birim = (short)rts.DovizSF1BirimInt;
                            }
                            DateTime date = DateTime.Now;
                            var shortDate = date.ToString("yyyy-MM-dd");
                            db.Database.ExecuteSqlCommand(string.Format("UPDATE [FINSAT6{0}].[FINSAT6{0}].[Fiyat] SET GMOnay = 1, GMOnaylayan='" + vUser.UserName + "', GMOnayTarih='{2}'  where ID = '{1}'", "17", rts.ID, shortDate));

                        }

                    }

 
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
        public JsonResult Fiyat_Onay_Grup(string Data)//GM
        {
            Result _Result = new Result(true);
            if (CheckPerm("Fiyat Onaylama", PermTypes.Writing) == false) return null;
            //string sql = string.Format(@"INSERT INTO FINSAT";
            JArray parameters = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JArray>(Request["Data"]);
            //JValue parameters = JsonConvert.<Newtonsoft.Json.Linq.JValue>(JsonConvert.SerializeObject(Data));
            SqlExper sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, "17");

            int BasTarih = 0;
            int BitTarih = 0;
            string ListeAdi = "";
            try
            {
                Dictionary<string, int> FiyatMaxSiraNo = new Dictionary<string, int>();
                foreach (JObject insertObj in parameters)
                {
                    if (FiyatMaxSiraNo.ContainsKey(insertObj["FiyatListNum"].ToString()))
                    {
                        continue;
                    }
                    else
                    {
                        int FytSiraNo = -1;
                        var FLB = db.Database.SqlQuery<short>(string.Format("SELECT ISNULL(MAX(SiraNo),0) FROM [FINSAT6{0}].[FINSAT6{0}].[FYT] WHERE FiyatListNum='{1}'", "17", insertObj["FiyatListNum"].ToString())).FirstOrDefault();
                        if (FLB > 0)
                        {
                            FytSiraNo = FLB;
                            FytSiraNo++;
                        }
                        else
                        {
                            FytSiraNo = 0;
                        }
                        FiyatMaxSiraNo.Add(insertObj["FiyatListNum"].ToString(), FytSiraNo);
                    }
                    string sql = string.Format(@"SELECT ID from [FINSAT6{0}].[FINSAT6{0}].[Fiyat] F
                    JOIN[FINSAT6{0}].[FINSAT6{0}].[STK] S ON S.MalKodu = F.MalKodu
                    WHERE S.GrupKod <> 'PARKE' AND S.GrupKod = '{1}' AND F.FiyatListNum = '{2}'
                    AND S.TipKod = '{3}' AND S.Kod2 = '{4}' AND S.Kod3 = '{5}' AND S.Kod1 ='{6}'
                    AND S.Kod8 ='{7}'
                    AND F.SatisFiyat1 = {8} AND F.SatisFiyat1Birim = '{9}' AND F.SatisFiyat1BirimInt = {10}
                    AND F.DovizSatisFiyat1 = {11} AND F.DovizSF1Birim = '{12}' AND F.DovizSF1BirimInt = {13}
                    AND F.DovizCinsi = '{14}' AND F.Onay = 1 AND F.SPGMYOnay = 1 AND F.GMOnay = 0 ", "17", insertObj["GrupKod"].ToString(),
                    insertObj["FiyatListNum"].ToString(),insertObj["Kalite"].ToString(),
                    insertObj["En"].ToString(), insertObj["Boy"].ToString(), insertObj["Kalinlik"].ToString(), insertObj["Yuzey"].ToString(),
                    insertObj["SatisFiyat1"].ToString().Replace(",", "."), insertObj["SatisFiyat1Birim"].ToString(),
                    insertObj["SatisFiyat1BirimInt"].ToInt32(), insertObj["DovizSatisFiyat1"].ToString().Replace(",", "."),
                    insertObj["DovizSF1Birim"].ToString(), insertObj["DovizSF1BirimInt"].ToInt32(), insertObj["DovizCinsi"].ToString()
                    );
                    var fiyatID = db.Database.SqlQuery<int>(sql).ToList();
                    var result = String.Join(", ", fiyatID.ToArray());
                    sql = string.Format("SELECT * FROM [FINSAT6{0}].[FINSAT6{0}].[Fiyat] where ID in({1})", "17", result);
                    var OnayFiyatList = db.Database.SqlQuery<Fiyat>(sql).ToList();
                    foreach (Fiyat rts in OnayFiyatList)
                    {
                        if (rts.Durum == 1) //YENİ KAYIT
                        {

                            var fytData = db.Database.SqlQuery<FYT>(string.Format("SELECT ISNULL(Max(FiyatListName),'') as FiyatListName,ISNULL(Max(BasTarih),0) as BasTarih,ISNULL(Max(BitTarih),0) as BitTarih FROM[FINSAT6{0}].[FINSAT6{0}].[FYT] where FiyatListNum = '{1}'", "17", rts.FiyatListNum.ToString())).FirstOrDefault();
                            //var bastarih = db.Database.SqlQuery<FiyatListSelect>(string.Format("SELECT Max(BasTarih)as bastarih FROM[FINSAT6{0}].[FINSAT6{0}].[FYT] where FiyatListNum = '{1}'", "33", insertObj["FiyatListNum"].ToString())).ToList();
                            //var bittarih = db.Database.SqlQuery<FiyatListSelect>(string.Format("SELECT Max(BitTarih)as bittarih FROM[FINSAT6{0}].[FINSAT6{0}].[FYT] where FiyatListNum = '{1}'", "33", insertObj["FiyatListNum"].ToString())).ToList();

                            FYT fyt = new FYT();

                            if (fytData.FiyatListNum != null)
                            {
                                ListeAdi = fytData.FiyatListName;
                                BasTarih = fytData.BasTarih;
                                BitTarih = fytData.BitTarih;

                            }
                            else
                            {
                                ListeAdi = rts.FiyatListNum.ToString() + " Fiyat Listesi";
                                if (ListeAdi.Length > 30)
                                {
                                    ListeAdi = ListeAdi.Substring(0, 30);
                                }
                                BasTarih = rts.BasTarih.ToInt32();
                                BitTarih = rts.BitTarih.ToInt32();

                            }

                            fyt.MalKodu = rts.MalKodu.ToString();
                            fyt.BasTarih = BasTarih;
                            fyt.BasSaat = 0;
                            fyt.BitTarih = BitTarih;
                            fyt.BitSaat = 0;
                            if (FiyatMaxSiraNo.ContainsKey(insertObj["FiyatListNum"].ToString()))
                            {
                                fyt.SiraNo = Convert.ToInt16(FiyatMaxSiraNo[insertObj["FiyatListNum"].ToString()]);
                                FiyatMaxSiraNo[insertObj["FiyatListNum"].ToString()] = FiyatMaxSiraNo[insertObj["FiyatListNum"].ToString()]++;
                            }
                            else
                            {
                                fyt.SiraNo = 0;
                            }
                            fyt.RenkBedenKod1 = "";
                            fyt.RenkBedenKod2 = "";
                            fyt.RenkBedenKod3 = "";
                            fyt.RenkBedenKod4 = "";
                            fyt.FiyatListNum = rts.FiyatListNum.ToString();
                            fyt.FiyatTuru = 1;
                            fyt.Kod1 = "";
                            fyt.Kod2 = "";
                            fyt.Kod3 = "";
                            fyt.KodTipi = 0;
                            fyt.MusteriKodu = rts.HesapKodu.ToString();
                            fyt.AlisFiyat1 = 0;
                            fyt.AlisFiyat2 = 0;
                            fyt.AlisFiyat3 = 0;
                            fyt.DvzAlisFiyat1 = 0;
                            fyt.DvzAlisFiyat2 = 0;
                            fyt.DvzAlisFiyat3 = 0;
                            fyt.AF1DovizCinsi = "";
                            fyt.AF2DovizCinsi = "";
                            fyt.AF3DovizCinsi = "";
                            fyt.AF1KDV = 0;
                            fyt.AF2KDV = 0;
                            fyt.AF3KDV = 0;
                            fyt.DovizAF1KDV = 0;
                            fyt.DovizAF2KDV = 0;
                            fyt.DovizAF3KDV = 0;
                            fyt.AF1Birim = 0;
                            fyt.AF2Birim = 0;
                            fyt.AF3Birim = 0;
                            fyt.DovizAF1Birim = 0;
                            fyt.DovizAF2Birim = 0;
                            fyt.DovizAF3Birim = 0;
                            fyt.SatisFiyat1 = rts.SatisFiyat1.ToDecimal();
                            fyt.SatisFiyat2 = 0;
                            fyt.SatisFiyat3 = 0;
                            fyt.SatisFiyat4 = 0;
                            fyt.SatisFiyat5 = 0;
                            fyt.SatisFiyat6 = 0;
                            fyt.DvzSatisFiyat1 = rts.DovizSatisFiyat1.ToDecimal();
                            fyt.DvzSatisFiyat2 = 0;
                            fyt.DvzSatisFiyat3 = 0;
                            fyt.SF1DovizCinsi = rts.DovizCinsi.ToString().Trim();
                            fyt.SF2DovizCinsi = "";
                            fyt.SF3DovizCinsi = "";
                            fyt.SF4DovizCinsi = "";
                            fyt.SF5DovizCinsi = "";
                            fyt.SF6DovizCinsi = "";
                            fyt.SF1KDV = 0;
                            fyt.SF2KDV = 0;
                            fyt.SF3KDV = 0;
                            fyt.SF4KDV = 0;
                            fyt.SF5KDV = 0;
                            fyt.SF6KDV = 0;
                            fyt.DovizSF1KDV = 0;
                            fyt.DovizSF2KDV = 0;
                            fyt.DovizSF3KDV = 0;
                            fyt.SF1Birim = rts.SatisFiyat1BirimInt.ToShort(); ;
                            fyt.SF2Birim = -1;
                            fyt.SF3Birim = -1;
                            fyt.SF4Birim = -1;
                            fyt.SF5Birim = -1;
                            fyt.SF6Birim = -1;
                            fyt.DovizSF1Birim = rts.DovizSF1BirimInt.ToShort();
                            fyt.DovizSF2Birim = -1;
                            fyt.DovizSF3Birim = -1;
                            fyt.FiyatListName = ListeAdi;
                            fyt.SatisFiyatAltLimit = 0;
                            fyt.SatisFiyatUstLimit = 0;
                            fyt.SF1ValorGun = 0;
                            fyt.SF2ValorGun = 0;
                            fyt.SF3ValorGun = 0;
                            fyt.SF4ValorGun = 0;
                            fyt.SF5ValorGun = 0;
                            fyt.SF6ValorGun = 0;
                            fyt.DvzSF1ValorGun = 0;
                            fyt.DvzSF2ValorGun = 0;
                            fyt.DvzSF3ValorGun = 0;
                            fyt.KayitTuru = -1;
                            fyt.GuvenlikKod = "";
                            fyt.Kaydeden = vUser.UserName.ToString();
                            fyt.KayitTarih = (int)DateTime.Now.ToOADate();
                            fyt.KayitSaat = 0;
                            fyt.KayitKaynak = 117;
                            fyt.KayitSurum = "8.10.100";
                            fyt.Degistiren = vUser.UserName.ToString();
                            fyt.DegisTarih = (int)DateTime.Now.ToOADate();
                            fyt.DegisSaat = 0;
                            fyt.DegisKaynak = 117;
                            fyt.DegisSurum = "8.10.100";
                            fyt.CheckSum = 12;
                            fyt.Aciklama = "";
                            fyt.Kod4 = "";
                            fyt.Kod5 = "";
                            fyt.Kod6 = "";
                            fyt.Kod7 = "";
                            fyt.Kod8 = "";
                            fyt.Kod9 = "";
                            fyt.Kod10 = "";
                            fyt.Kod11 = 0;
                            fyt.Kod12 = 0;


                            DateTime date = DateTime.Now;
                            var shortDate = date.ToString("yyyy-MM-dd");
                            sqlexper.Insert(fyt);
                            var sonuc = sqlexper.AcceptChanges();
                            db.Database.ExecuteSqlCommand(string.Format("UPDATE [FINSAT6{0}].[FINSAT6{0}].[Fiyat] SET GMOnay = 1, GMOnaylayan='" + vUser.UserName + "', GMOnayTarih='{2}'  where ID = '{1}'", "17", rts.ID, shortDate));



                        }
                        else if (rts.Durum == 2)//Silinecek Kayıt
                        {
                            string durum2sql = string.Format("SELECT * FROM [FINSAT6{0}].[FINSAT6{0}].[FYT] WHERE ROW_ID = {1}", "17", rts.ROW_ID);
                            var fytSilinecek = db.Database.SqlQuery<FYT>(durum2sql).ToList();
                            if (fytSilinecek.Count > 0)
                            {
                                db.Database.ExecuteSqlCommand(string.Format("DELETE FROM [FINSAT6{0}].[FINSAT6{0}].[FYT] WHERE ROW_ID = {1}", "17", rts.ROW_ID));
                            }
                            DateTime date = DateTime.Now;
                            var shortDate = date.ToString("yyyy-MM-dd");
                            db.Database.ExecuteSqlCommand(string.Format("UPDATE [FINSAT6{0}].[FINSAT6{0}].[Fiyat] SET GMOnay = 1, GMOnaylayan='" + vUser.UserName + "', GMOnayTarih='{2}'  where ID = '{1}'", "17", rts.ID, shortDate));
                        }
                        else if (rts.Durum == 3)//"Güncellenecek Kayıt"
                        {
                            string durum3sql = string.Format("SELECT * FROM [FINSAT6{0}].[FINSAT6{0}].[FYT] WHERE ROW_ID = {1}", "17", rts.ROW_ID);
                            var fytGuncellenecek = db.Database.SqlQuery<FYT>(durum3sql).FirstOrDefault();
                            if (fytGuncellenecek != null)
                            {
                                fytGuncellenecek.SatisFiyat1 = rts.SatisFiyat1;
                                fytGuncellenecek.SF1Birim = (short)rts.SatisFiyat1BirimInt;
                                fytGuncellenecek.DvzSatisFiyat1 = rts.DovizSatisFiyat1;
                                fytGuncellenecek.SF1DovizCinsi = rts.DovizCinsi.Trim();
                                fytGuncellenecek.DovizSF1Birim = (short)rts.DovizSF1BirimInt;
                            }
                            DateTime date = DateTime.Now;
                            var shortDate = date.ToString("yyyy-MM-dd");
                            db.Database.ExecuteSqlCommand(string.Format("UPDATE [FINSAT6{0}].[FINSAT6{0}].[Fiyat] SET GMOnay = 1, GMOnaylayan='" + vUser.UserName + "', GMOnayTarih='{2}'  where ID = '{1}'", "17", rts.ID, shortDate));

                        }

                    }


                }
                _Result.Message = "İşlem Başarılı";
                _Result.Status = true;
            }
            catch (Exception ex)
            {
                _Result.Message = "Hata Oluştu";
                _Result.Status = false;
            }
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
       


        public JsonResult Fiyat_Onay_SM(string Data)
        {
            Result _Result = new Result(true);
            if (CheckPerm("Fiyat Onaylama", PermTypes.Writing) == false) return null;
            //string sql = string.Format(@"INSERT INTO FINSAT";
            JArray parameters = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JArray>(Request["Data"]);
            //JValue parameters = JsonConvert.<Newtonsoft.Json.Linq.JValue>(JsonConvert.SerializeObject(Data));
            SqlExper sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, "17");

            try
            {
                Dictionary<string, int> FiyatMaxSiraNo = new Dictionary<string, int>();
                foreach (JObject insertObj in parameters)
                {     
                       // var fytData = db.Database.SqlQuery<FYT>(string.Format("SELECT ISNULL(Max(FiyatListName),'') as FiyatListName,ISNULL(Max(BasTarih),0) as BasTarih,ISNULL(Max(BitTarih),0) as BitTarih FROM[FINSAT6{0}].[FINSAT6{0}].[FYT] where FiyatListNum = '{1}'", "17", insertObj["FiyatListNum"].ToString())).FirstOrDefault();
                        DateTime date = DateTime.Now;
                        var shortDate = date.ToString("yyyy-MM-dd");                       
                        var sonuc = sqlexper.AcceptChanges();
                        db.Database.ExecuteSqlCommand(string.Format("UPDATE [FINSAT6{0}].[FINSAT6{0}].[Fiyat] SET Onay = 1, Onaylayan='" + vUser.UserName + "', SMOnayTarih='{2}'  where ID = '{1}'", "17", insertObj["ID"].ToString(), shortDate));
                    



                }
                //return "OK";

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
        public JsonResult Fiyat_Onay_Koleksiyon_SM(string Data)
        {
            Result _Result = new Result(true);
            if (CheckPerm("Fiyat Onaylama", PermTypes.Writing) == false) return null;
            //string sql = string.Format(@"INSERT INTO FINSAT";
            JArray parameters = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JArray>(Request["Data"]);
            //JValue parameters = JsonConvert.<Newtonsoft.Json.Linq.JValue>(JsonConvert.SerializeObject(Data));
            SqlExper sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, "17");

            try
            {
               
                foreach (JObject insertObj in parameters)
                {

                    string sql = string.Format(@"SELECT ID from [FINSAT6{0}].[FINSAT6{0}].[Fiyat] F
                    JOIN[FINSAT6{0}].[FINSAT6{0}].[STK] S ON S.MalKodu = F.MalKodu
                    WHERE S.GrupKod = 'PARKE' AND F.FiyatListNum = '{1}'
                    AND S.Kod4 = '{2}' AND S.TipKod = '{3}' AND F.SatisFiyat1 = {4}
                    AND F.SatisFiyat1Birim = '{5}' AND F.SatisFiyat1BirimInt = {6}
                    AND F.DovizSatisFiyat1 = {7} AND F.DovizSF1Birim = '{8}' AND F.DovizSF1BirimInt = {9}
                    AND F.DovizCinsi = '{10}' AND F.Onay = 0 AND F.GMOnay = 0 ", "17", insertObj["FiyatListNum"].ToString(), insertObj["Kod4"].ToString(),
                    insertObj["TipKod"].ToString(), insertObj["SatisFiyat1"].ToString().Replace(",", "."), insertObj["SatisFiyat1Birim"].ToString(), insertObj["SatisFiyat1BirimInt"].ToString(),
                    insertObj["DovizSatisFiyat1"].ToString().Replace(",", "."), insertObj["DovizSF1Birim"].ToString(), insertObj["DovizSF1BirimInt"].ToInt32(), insertObj["DovizCinsi"].ToString()
                    );
                    var fiyatID = db.Database.SqlQuery<int>(sql).ToList();
                    var result = String.Join(", ", fiyatID.ToArray());
                    sql = string.Format("SELECT * FROM [FINSAT6{0}].[FINSAT6{0}].[Fiyat] where ID in({1})", "17", result);
                    var OnayFiyatList = db.Database.SqlQuery<Fiyat>(sql).ToList();
                    foreach (Fiyat rts in OnayFiyatList)
                    {
                        
                            DateTime date = DateTime.Now;
                            var shortDate = date.ToString("yyyy-MM-dd");                           
                            var sonuc = sqlexper.AcceptChanges();
                            db.Database.ExecuteSqlCommand(string.Format("UPDATE [FINSAT6{0}].[FINSAT6{0}].[Fiyat] SET Onay = 1, Onaylayan='" + vUser.UserName + "', SMOnayTarih='{2}'  where ID = '{1}'", "17", rts.ID, shortDate));

                    }


                }

                _Result.Message = "İşlem Başarılı";
                _Result.Status = true;
            }
            catch (Exception ex)
            {
                _Result.Message = "Hata Oluştu";
                _Result.Status = false;
            }
            return Json(_Result, JsonRequestBehavior.AllowGet);

        }
        public JsonResult Fiyat_Onay_Grup_SM(string Data)
        {
            Result _Result = new Result(true);
            if (CheckPerm("Fiyat Onaylama", PermTypes.Writing) == false) return null;
            //string sql = string.Format(@"INSERT INTO FINSAT";
            JArray parameters = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JArray>(Request["Data"]);
            //JValue parameters = JsonConvert.<Newtonsoft.Json.Linq.JValue>(JsonConvert.SerializeObject(Data));
            SqlExper sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, "17");

            try
            {
                Dictionary<string, int> FiyatMaxSiraNo = new Dictionary<string, int>();
                foreach (JObject insertObj in parameters)
                {

                    string sql = string.Format(@"SELECT ID from [FINSAT6{0}].[FINSAT6{0}].[Fiyat] F
                    JOIN[FINSAT6{0}].[FINSAT6{0}].[STK] S ON S.MalKodu = F.MalKodu
                    WHERE S.GrupKod <> 'PARKE' AND S.GrupKod = '{1}' AND F.FiyatListNum = '{2}'
                    AND S.TipKod = '{3}' AND S.Kod2 = '{4}' AND S.Kod3 = '{5}' AND S.Kod1 ='{6}'
                    AND S.Kod8 ='{7}'
                    AND F.SatisFiyat1 = {8} AND F.SatisFiyat1Birim = '{9}' AND F.SatisFiyat1BirimInt = {10}
                    AND F.DovizSatisFiyat1 = {11} AND F.DovizSF1Birim = '{12}' AND F.DovizSF1BirimInt = {13}
                    AND F.DovizCinsi = '{14}' AND F.Onay = 0 AND F.GMOnay = 0 ", "17", insertObj["GrupKod"].ToString(),
                    insertObj["FiyatListNum"].ToString(), insertObj["Kalite"].ToString(),
                    insertObj["En"].ToString(), insertObj["Boy"].ToString(), insertObj["Kalinlik"].ToString(), insertObj["Yuzey"].ToString(),
                    insertObj["SatisFiyat1"].ToString().Replace(",", "."), insertObj["SatisFiyat1Birim"].ToString(),
                    insertObj["SatisFiyat1BirimInt"].ToInt32(), insertObj["DovizSatisFiyat1"].ToString().Replace(",", "."),
                    insertObj["DovizSF1Birim"].ToString(), insertObj["DovizSF1BirimInt"].ToInt32(), insertObj["DovizCinsi"].ToString()
                    );
                    var fiyatID = db.Database.SqlQuery<int>(sql).ToList();
                    var result = String.Join(", ", fiyatID.ToArray());
                    sql = string.Format("SELECT * FROM [FINSAT6{0}].[FINSAT6{0}].[Fiyat] where ID in({1})", "17", result);
                    var OnayFiyatList = db.Database.SqlQuery<Fiyat>(sql).ToList();
                    foreach (Fiyat rts in OnayFiyatList)
                    {

                        DateTime date = DateTime.Now;
                        var shortDate = date.ToString("yyyy-MM-dd");

                        var sonuc = sqlexper.AcceptChanges();
                        db.Database.ExecuteSqlCommand(string.Format("UPDATE [FINSAT6{0}].[FINSAT6{0}].[Fiyat] SET Onay = 1, Onaylayan='" + vUser.UserName + "', SMOnayTarih='{2}'  where ID = '{1}'", "17", rts.ID, shortDate));



                    }


                }
                _Result.Message = "İşlem Başarılı";
                _Result.Status = true;
            }
            catch (Exception ex)
            {
                _Result.Message = "Hata Oluştu";
                _Result.Status = false;
            }
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Fiyat_Onay_SPGMY(string Data)
        {
            Result _Result = new Result(true);
            if (CheckPerm("Fiyat Onaylama", PermTypes.Writing) == false) return null;
            //string sql = string.Format(@"INSERT INTO FINSAT";
            JArray parameters = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JArray>(Request["Data"]);
            //JValue parameters = JsonConvert.<Newtonsoft.Json.Linq.JValue>(JsonConvert.SerializeObject(Data));
            SqlExper sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, "17");

            try
            {
                Dictionary<string, int> FiyatMaxSiraNo = new Dictionary<string, int>();
                foreach (JObject insertObj in parameters)
                {
                    // var fytData = db.Database.SqlQuery<FYT>(string.Format("SELECT ISNULL(Max(FiyatListName),'') as FiyatListName,ISNULL(Max(BasTarih),0) as BasTarih,ISNULL(Max(BitTarih),0) as BitTarih FROM[FINSAT6{0}].[FINSAT6{0}].[FYT] where FiyatListNum = '{1}'", "17", insertObj["FiyatListNum"].ToString())).FirstOrDefault();
                    DateTime date = DateTime.Now;
                    var shortDate = date.ToString("yyyy-MM-dd");
                    var sonuc = sqlexper.AcceptChanges();
                    db.Database.ExecuteSqlCommand(string.Format("UPDATE [FINSAT6{0}].[FINSAT6{0}].[Fiyat] SET SPGMYOnay = 1, SPGMYOnaylayan='" + vUser.UserName + "', SPGMYOnayTarih='{2}'  where ID = '{1}'", "17", insertObj["ID"].ToString(), shortDate));




                }
                //return "OK";

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
        public JsonResult Fiyat_Onay_Koleksiyon_SPGMY(string Data)
        {
            Result _Result = new Result(true);
            if (CheckPerm("Fiyat Onaylama", PermTypes.Writing) == false) return null;
            //string sql = string.Format(@"INSERT INTO FINSAT";
            JArray parameters = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JArray>(Request["Data"]);
            //JValue parameters = JsonConvert.<Newtonsoft.Json.Linq.JValue>(JsonConvert.SerializeObject(Data));
            SqlExper sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, "17");

            try
            {

                foreach (JObject insertObj in parameters)
                {

                    string sql = string.Format(@"SELECT ID from [FINSAT6{0}].[FINSAT6{0}].[Fiyat] F
                    JOIN[FINSAT6{0}].[FINSAT6{0}].[STK] S ON S.MalKodu = F.MalKodu
                    WHERE S.GrupKod = 'PARKE' AND F.FiyatListNum = '{1}'
                    AND S.Kod4 = '{2}' AND S.TipKod = '{3}' AND F.SatisFiyat1 = {4}
                    AND F.SatisFiyat1Birim = '{5}' AND F.SatisFiyat1BirimInt = {6}
                    AND F.DovizSatisFiyat1 = {7} AND F.DovizSF1Birim = '{8}' AND F.DovizSF1BirimInt = {9}
                    AND F.DovizCinsi = '{10}' AND F.Onay = 1  AND F.GMOnay = 0 F.SPGMYOnay = 0", "17", insertObj["FiyatListNum"].ToString(), insertObj["Kod4"].ToString(),
                    insertObj["TipKod"].ToString(), insertObj["SatisFiyat1"].ToString().Replace(",", "."), insertObj["SatisFiyat1Birim"].ToString(), insertObj["SatisFiyat1BirimInt"].ToString(),
                    insertObj["DovizSatisFiyat1"].ToString().Replace(",", "."), insertObj["DovizSF1Birim"].ToString(), insertObj["DovizSF1BirimInt"].ToInt32(), insertObj["DovizCinsi"].ToString()
                    );
                    var fiyatID = db.Database.SqlQuery<int>(sql).ToList();
                    var result = String.Join(", ", fiyatID.ToArray());
                    sql = string.Format("SELECT * FROM [FINSAT6{0}].[FINSAT6{0}].[Fiyat] where ID in({1})", "17", result);
                    var OnayFiyatList = db.Database.SqlQuery<Fiyat>(sql).ToList();
                    foreach (Fiyat rts in OnayFiyatList)
                    {

                        DateTime date = DateTime.Now;
                        var shortDate = date.ToString("yyyy-MM-dd");
                        var sonuc = sqlexper.AcceptChanges();
                        db.Database.ExecuteSqlCommand(string.Format("UPDATE [FINSAT6{0}].[FINSAT6{0}].[Fiyat] SET SPGMYOnay = 1, SPGMYOnaylayan='" + vUser.UserName + "', SPGMYOnayTarih='{2}'  where ID = '{1}'", "17", rts.ID, shortDate));

                    }


                }

                _Result.Message = "İşlem Başarılı";
                _Result.Status = true;
            }
            catch (Exception ex)
            {
                _Result.Message = "Hata Oluştu";
                _Result.Status = false;
            }
            return Json(_Result, JsonRequestBehavior.AllowGet);

        }
        public JsonResult Fiyat_Onay_Grup_SPGMY(string Data)
        {
            Result _Result = new Result(true);
            if (CheckPerm("Fiyat Onaylama", PermTypes.Writing) == false) return null;
            //string sql = string.Format(@"INSERT INTO FINSAT";
            JArray parameters = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JArray>(Request["Data"]);
            //JValue parameters = JsonConvert.<Newtonsoft.Json.Linq.JValue>(JsonConvert.SerializeObject(Data));
            SqlExper sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, "17");

            try
            {
                Dictionary<string, int> FiyatMaxSiraNo = new Dictionary<string, int>();
                foreach (JObject insertObj in parameters)
                {

                    string sql = string.Format(@"SELECT ID from [FINSAT6{0}].[FINSAT6{0}].[Fiyat] F
                    JOIN[FINSAT6{0}].[FINSAT6{0}].[STK] S ON S.MalKodu = F.MalKodu
                    WHERE S.GrupKod <> 'PARKE' AND S.GrupKod = '{1}' AND F.FiyatListNum = '{2}'
                    AND S.TipKod = '{3}' AND S.Kod2 = '{4}' AND S.Kod3 = '{5}' AND S.Kod1 ='{6}'
                    AND S.Kod8 ='{7}'
                    AND F.SatisFiyat1 = {8} AND F.SatisFiyat1Birim = '{9}' AND F.SatisFiyat1BirimInt = {10}
                    AND F.DovizSatisFiyat1 = {11} AND F.DovizSF1Birim = '{12}' AND F.DovizSF1BirimInt = {13}
                    AND F.DovizCinsi = '{14}' AND F.Onay = 1 AND F.SPGMYOnay = 0 AND  F.GMOnay = 0 ", "17", insertObj["GrupKod"].ToString(),
                    insertObj["FiyatListNum"].ToString(), insertObj["Kalite"].ToString(),
                    insertObj["En"].ToString(), insertObj["Boy"].ToString(), insertObj["Kalinlik"].ToString(), insertObj["Yuzey"].ToString(),
                    insertObj["SatisFiyat1"].ToString().Replace(",", "."), insertObj["SatisFiyat1Birim"].ToString(),
                    insertObj["SatisFiyat1BirimInt"].ToInt32(), insertObj["DovizSatisFiyat1"].ToString().Replace(",", "."),
                    insertObj["DovizSF1Birim"].ToString(), insertObj["DovizSF1BirimInt"].ToInt32(), insertObj["DovizCinsi"].ToString()
                    );
                    var fiyatID = db.Database.SqlQuery<int>(sql).ToList();
                    var result = String.Join(", ", fiyatID.ToArray());
                    sql = string.Format("SELECT * FROM [FINSAT6{0}].[FINSAT6{0}].[Fiyat] where ID in({1})", "17", result);
                    var OnayFiyatList = db.Database.SqlQuery<Fiyat>(sql).ToList();
                    foreach (Fiyat rts in OnayFiyatList)
                    {

                        DateTime date = DateTime.Now;
                        var shortDate = date.ToString("yyyy-MM-dd");

                        var sonuc = sqlexper.AcceptChanges();
                        db.Database.ExecuteSqlCommand(string.Format("UPDATE [FINSAT6{0}].[FINSAT6{0}].[Fiyat] SET SPGMYOnay = 1, SPGMYOnaylayan='" + vUser.UserName + "', SPGMYOnayTarih='{2}'  where ID = '{1}'", "17", rts.ID, shortDate));



                    }


                }
                _Result.Message = "İşlem Başarılı";
                _Result.Status = true;
            }
            catch (Exception ex)
            {
                _Result.Message = "Hata Oluştu";
                _Result.Status = false;
            }
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Fiyat_Red(string Data)
        {
            Result _Result = new Result(true);
            if (CheckPerm("Fiyat Onaylama", PermTypes.Writing) == false) return null;
            //string sql = string.Format(@"INSERT INTO FINSAT";
            JArray parameters = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JArray>(Request["Data"]);
            //JValue parameters = JsonConvert.<Newtonsoft.Json.Linq.JValue>(JsonConvert.SerializeObject(Data));
            SqlExper sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, "17");


            try
            {
                foreach (JObject insertObj in parameters)
                {

 
                    db.Database.ExecuteSqlCommand(string.Format("DELETE FROM [FINSAT6{0}].[FINSAT6{0}].[Fiyat]  where ID = '{1}'", "17", insertObj["ID"].ToString()));
                }
                _Result.Message = "İşlem Başarılı";
                _Result.Status = true;
            }
            catch (Exception ex)
            {
                _Result.Message = "Hata oluştu";
                _Result.Status = false;
            }
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Fiyat_Red_Koleksiyon(string Data)
        {
            Result _Result = new Result(true);
            if (CheckPerm("Fiyat Onaylama", PermTypes.Writing) == false) return null;
            //string sql = string.Format(@"INSERT INTO FINSAT";
            JArray parameters = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JArray>(Request["Data"]);
            //JValue parameters = JsonConvert.<Newtonsoft.Json.Linq.JValue>(JsonConvert.SerializeObject(Data));
            SqlExper sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, "17");


            try
            {
                foreach (JObject insertObj in parameters)
                {
                    string sql = string.Format(@"SELECT ID from [FINSAT6{0}].[FINSAT6{0}].[Fiyat] F
                    JOIN[FINSAT6{0}].[FINSAT6{0}].[STK] S ON S.MalKodu = F.MalKodu
                    WHERE S.GrupKod = 'PARKE' AND F.FiyatListNum = '{1}'
                    AND S.Kod4 = '{2}' AND S.TipKod = '{3}' AND F.SatisFiyat1 = {4}
                    AND F.SatisFiyat1Birim = '{5}' AND F.SatisFiyat1BirimInt = {6}
                    AND F.DovizSatisFiyat1 = {7} AND F.DovizSF1Birim = '{8}' AND F.DovizSF1BirimInt = {9}
                    AND F.DovizCinsi = '{10}' AND F.Onay = 1 AND F.SPGMYOnay = 1 AND F.GMOnay = 0 ", "17", insertObj["FiyatListNum"].ToString(), insertObj["Kod4"].ToString(),
insertObj["TipKod"].ToString(), insertObj["SatisFiyat1"].ToString().Replace(",", "."), insertObj["SatisFiyat1Birim"].ToString(), insertObj["SatisFiyat1BirimInt"].ToString(),
insertObj["DovizSatisFiyat1"].ToInt32(), insertObj["DovizSF1Birim"].ToString(), insertObj["DovizSF1BirimInt"].ToInt32(), insertObj["DovizCinsi"].ToString()
);
                    var fiyatID = db.Database.SqlQuery<int>(sql).ToList();
                    var result = String.Join(", ", fiyatID.ToArray());
                    //sql = string.Format("SELECT * FROM [FINSAT6{0}].[FINSAT6{0}].[Fiyat] where ID in({1})", "17", result);

                    db.Database.ExecuteSqlCommand(string.Format("DELETE FROM [FINSAT6{0}].[FINSAT6{0}].[Fiyat]  where ID in({1})", "17", result));
                }
                _Result.Message = "İşlem Başarılı";
                _Result.Status = true;
            }
            catch (Exception ex)
            {
                _Result.Message = "Hata Oluştu";
                _Result.Status = false;
            }
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Fiyat_Red_Grup(string Data)
        {
            Result _Result = new Result(true);
            if (CheckPerm("Fiyat Onaylama", PermTypes.Writing) == false) return null;
            //string sql = string.Format(@"INSERT INTO FINSAT";
            JArray parameters = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JArray>(Request["Data"]);
            //JValue parameters = JsonConvert.<Newtonsoft.Json.Linq.JValue>(JsonConvert.SerializeObject(Data));
            SqlExper sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, "17");


            try
            {
                foreach (JObject insertObj in parameters)
                {
                    string sql = string.Format(@"SELECT ID from [FINSAT6{0}].[FINSAT6{0}].[Fiyat] F
                    JOIN[FINSAT6{0}].[FINSAT6{0}].[STK] S ON S.MalKodu = F.MalKodu
                    WHERE S.GrupKod <> 'PARKE' AND S.GrupKod = '{1}' AND F.FiyatListNum = '{2}'
                    AND S.TipKod = '{3}' AND S.Kod2 = '{4}' AND S.Kod3 = '{5}' AND S.Kod1 ='{6}'
                    AND S.Kod8 ='{7}'
                    AND F.SatisFiyat1 = {8} AND F.SatisFiyat1Birim = '{9}' AND F.SatisFiyat1BirimInt = {10}
                    AND F.DovizSatisFiyat1 = {11} AND F.DovizSF1Birim = '{12}' AND F.DovizSF1BirimInt = {13}
                    AND F.DovizCinsi = '{14}' AND F.Onay = 1 AND F.SPGMYOnay = 1 AND F.GMOnay = 0 ", "17", insertObj["GrupKod"].ToString(),
                    insertObj["FiyatListNum"].ToString(), insertObj["Kalite"].ToString(),
                    insertObj["En"].ToString(), insertObj["Boy"].ToString(), insertObj["Kalinlik"].ToString(), insertObj["Yuzey"].ToString(),
                    insertObj["SatisFiyat1"].ToString().Replace(",", "."), insertObj["SatisFiyat1Birim"].ToString(),
                    insertObj["SatisFiyat1BirimInt"].ToInt32(), insertObj["DovizSatisFiyat1"].ToString().Replace(",", "."),
                    insertObj["DovizSF1Birim"].ToString(), insertObj["DovizSF1BirimInt"].ToInt32(), insertObj["DovizCinsi"].ToString()
                    );
                    var fiyatID = db.Database.SqlQuery<int>(sql).ToList();
                    var result = String.Join(", ", fiyatID.ToArray());
                    //sql = string.Format("SELECT * FROM [FINSAT6{0}].[FINSAT6{0}].[Fiyat] where ID in({1})", "17", result);

                    db.Database.ExecuteSqlCommand(string.Format("DELETE FROM [FINSAT6{0}].[FINSAT6{0}].[Fiyat]  where ID in({1})", "17", result));
                }
                _Result.Message = "İşlem Başarılı";
                _Result.Status = true;
            }
            catch (Exception ex)
            {
                _Result.Message = "Hata Oluştu";
                _Result.Status = false;

            }
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Fiyat_Red_SM(string Data)
        {
            Result _Result = new Result(true);
            if (CheckPerm("Fiyat Onaylama", PermTypes.Writing) == false) return null;
            //string sql = string.Format(@"INSERT INTO FINSAT";
            JArray parameters = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JArray>(Request["Data"]);
            //JValue parameters = JsonConvert.<Newtonsoft.Json.Linq.JValue>(JsonConvert.SerializeObject(Data));
            SqlExper sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, "17");


            try
            {
                foreach (JObject insertObj in parameters)
                {


                    db.Database.ExecuteSqlCommand(string.Format("DELETE FROM [FINSAT6{0}].[FINSAT6{0}].[Fiyat]  where ID = '{1}'", "17", insertObj["ID"].ToString()));
                }
                _Result.Message = "İşlem Başarılı";
                _Result.Status = true;
            }
            catch (Exception ex)
            {
                _Result.Message = "Hata Oluştu";
                _Result.Status = false;
            }
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Fiyat_Red_Koleksiyon_SM(string Data)
        {
            Result _Result = new Result(true);
            if (CheckPerm("Fiyat Onaylama", PermTypes.Writing) == false) return null;
            //string sql = string.Format(@"INSERT INTO FINSAT";
            JArray parameters = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JArray>(Request["Data"]);
            //JValue parameters = JsonConvert.<Newtonsoft.Json.Linq.JValue>(JsonConvert.SerializeObject(Data));
            SqlExper sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, "17");


            try
            {
                foreach (JObject insertObj in parameters)
                {
                    string sql = string.Format(@"SELECT ID from [FINSAT6{0}].[FINSAT6{0}].[Fiyat] F
                    JOIN[FINSAT6{0}].[FINSAT6{0}].[STK] S ON S.MalKodu = F.MalKodu
                    WHERE S.GrupKod = 'PARKE' AND F.FiyatListNum = '{1}'
                    AND S.Kod4 = '{2}' AND S.TipKod = '{3}' AND F.SatisFiyat1 = {4}
                    AND F.SatisFiyat1Birim = '{5}' AND F.SatisFiyat1BirimInt = {6}
                    AND F.DovizSatisFiyat1 = {7} AND F.DovizSF1Birim = '{8}' AND F.DovizSF1BirimInt = {9}
                    AND F.DovizCinsi = '{10}' AND F.Onay = 0 AND F.GMOnay = 0 ", "17", insertObj["FiyatListNum"].ToString(), insertObj["Kod4"].ToString(),
insertObj["TipKod"].ToString(), insertObj["SatisFiyat1"].ToString().Replace(",", "."), insertObj["SatisFiyat1Birim"].ToString(), insertObj["SatisFiyat1BirimInt"].ToString(),
insertObj["DovizSatisFiyat1"].ToInt32(), insertObj["DovizSF1Birim"].ToString(), insertObj["DovizSF1BirimInt"].ToInt32(), insertObj["DovizCinsi"].ToString()
);
                    var fiyatID = db.Database.SqlQuery<int>(sql).ToList();
                    var result = String.Join(", ", fiyatID.ToArray());
                    //sql = string.Format("SELECT * FROM [FINSAT6{0}].[FINSAT6{0}].[Fiyat] where ID in({1})", "17", result);

                    db.Database.ExecuteSqlCommand(string.Format("DELETE FROM [FINSAT6{0}].[FINSAT6{0}].[Fiyat]  where ID in({1})", "17", result));
                }
                _Result.Message = "İşlem Başarılı";
                _Result.Status = true;
            }
            catch (Exception ex)
            {
                _Result.Message = "Hata Oluştu";
                _Result.Status = false;
            }
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Fiyat_Red_Grup_SM(string Data)
        {
            Result _Result = new Result(true);
            if (CheckPerm("Fiyat Onaylama", PermTypes.Writing) == false) return null;
            //string sql = string.Format(@"INSERT INTO FINSAT";
            JArray parameters = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JArray>(Request["Data"]);
            //JValue parameters = JsonConvert.<Newtonsoft.Json.Linq.JValue>(JsonConvert.SerializeObject(Data));
            SqlExper sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, "17");


            try
            {
                foreach (JObject insertObj in parameters)
                {
                    string sql = string.Format(@"SELECT ID from [FINSAT6{0}].[FINSAT6{0}].[Fiyat] F
                    JOIN[FINSAT6{0}].[FINSAT6{0}].[STK] S ON S.MalKodu = F.MalKodu
                    WHERE S.GrupKod <> 'PARKE' AND S.GrupKod = '{1}' AND F.FiyatListNum = '{2}'
                    AND S.TipKod = '{3}' AND S.Kod2 = '{4}' AND S.Kod3 = '{5}' AND S.Kod1 ='{6}'
                    AND S.Kod8 ='{7}'
                    AND F.SatisFiyat1 = {8} AND F.SatisFiyat1Birim = '{9}' AND F.SatisFiyat1BirimInt = {10}
                    AND F.DovizSatisFiyat1 = {11} AND F.DovizSF1Birim = '{12}' AND F.DovizSF1BirimInt = {13}
                    AND F.DovizCinsi = '{14}' AND F.Onay = 0 AND F.GMOnay = 0 ", "17", insertObj["GrupKod"].ToString(),
                    insertObj["FiyatListNum"].ToString(), insertObj["Kalite"].ToString(),
                    insertObj["En"].ToString(), insertObj["Boy"].ToString(), insertObj["Kalinlik"].ToString(), insertObj["Yuzey"].ToString(),
                    insertObj["SatisFiyat1"].ToString().Replace(",", "."), insertObj["SatisFiyat1Birim"].ToString(),
                    insertObj["SatisFiyat1BirimInt"].ToInt32(), insertObj["DovizSatisFiyat1"].ToString().Replace(",", "."),
                    insertObj["DovizSF1Birim"].ToString(), insertObj["DovizSF1BirimInt"].ToInt32(), insertObj["DovizCinsi"].ToString()
                    );
                    var fiyatID = db.Database.SqlQuery<int>(sql).ToList();
                    var result = String.Join(", ", fiyatID.ToArray());
                    //sql = string.Format("SELECT * FROM [FINSAT6{0}].[FINSAT6{0}].[Fiyat] where ID in({1})", "17", result);

                    db.Database.ExecuteSqlCommand(string.Format("DELETE FROM [FINSAT6{0}].[FINSAT6{0}].[Fiyat]  where ID in({1})", "17", result));
                }
                _Result.Message = "İşlem Başarılı";
                _Result.Status = true;
            }
            catch (Exception ex)
            {
                _Result.Message = "Hata Oluştu";
                _Result.Status = false;
            }
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Fiyat_Red_SPGMY(string Data)
        {
            Result _Result = new Result(true);
            if (CheckPerm("Fiyat Onaylama", PermTypes.Writing) == false) return null;
            //string sql = string.Format(@"INSERT INTO FINSAT";
            JArray parameters = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JArray>(Request["Data"]);
            //JValue parameters = JsonConvert.<Newtonsoft.Json.Linq.JValue>(JsonConvert.SerializeObject(Data));
            SqlExper sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, "17");


            try
            {
                foreach (JObject insertObj in parameters)
                {


                    db.Database.ExecuteSqlCommand(string.Format("DELETE FROM [FINSAT6{0}].[FINSAT6{0}].[Fiyat]  where ID = '{1}'", "17", insertObj["ID"].ToString()));
                }
                _Result.Message = "İşlem Başarılı";
                _Result.Status = true;
            }
            catch (Exception ex)
            {
                _Result.Message = "Hata Oluştu";
                _Result.Status = false;
            }
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Fiyat_Red_Koleksiyon_SPGMY(string Data)
        {
            Result _Result = new Result(true);
            if (CheckPerm("Fiyat Onaylama", PermTypes.Writing) == false) return null;
            //string sql = string.Format(@"INSERT INTO FINSAT";
            JArray parameters = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JArray>(Request["Data"]);
            //JValue parameters = JsonConvert.<Newtonsoft.Json.Linq.JValue>(JsonConvert.SerializeObject(Data));
            SqlExper sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, "17");


            try
            {
                foreach (JObject insertObj in parameters)
                {
                    string sql = string.Format(@"SELECT ID from [FINSAT6{0}].[FINSAT6{0}].[Fiyat] F
                    JOIN[FINSAT6{0}].[FINSAT6{0}].[STK] S ON S.MalKodu = F.MalKodu
                    WHERE S.GrupKod = 'PARKE' AND F.FiyatListNum = '{1}'
                    AND S.Kod4 = '{2}' AND S.TipKod = '{3}' AND F.SatisFiyat1 = {4}
                    AND F.SatisFiyat1Birim = '{5}' AND F.SatisFiyat1BirimInt = {6}
                    AND F.DovizSatisFiyat1 = {7} AND F.DovizSF1Birim = '{8}' AND F.DovizSF1BirimInt = {9}
                    AND F.DovizCinsi = '{10}' AND F.Onay = 0 AND F.GMOnay = 0 ", "17", insertObj["FiyatListNum"].ToString(), insertObj["Kod4"].ToString(),
insertObj["TipKod"].ToString(), insertObj["SatisFiyat1"].ToString().Replace(",", "."), insertObj["SatisFiyat1Birim"].ToString(), insertObj["SatisFiyat1BirimInt"].ToString(),
insertObj["DovizSatisFiyat1"].ToInt32(), insertObj["DovizSF1Birim"].ToString(), insertObj["DovizSF1BirimInt"].ToInt32(), insertObj["DovizCinsi"].ToString()
);
                    var fiyatID = db.Database.SqlQuery<int>(sql).ToList();
                    var result = String.Join(", ", fiyatID.ToArray());
                    //sql = string.Format("SELECT * FROM [FINSAT6{0}].[FINSAT6{0}].[Fiyat] where ID in({1})", "17", result);

                    db.Database.ExecuteSqlCommand(string.Format("DELETE FROM [FINSAT6{0}].[FINSAT6{0}].[Fiyat]  where ID in({1})", "17", result));
                }
                _Result.Message = "İşlem Başarılı";
                _Result.Status = true;
            }
            catch (Exception ex)
            {
                _Result.Message = "Hata Oluştu";
                _Result.Status = false;
            }
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Fiyat_Red_Grup_SPGMY(string Data)
        {
            Result _Result = new Result(true);
            if (CheckPerm("Fiyat Onaylama", PermTypes.Writing) == false) return null;
            //string sql = string.Format(@"INSERT INTO FINSAT";
            JArray parameters = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JArray>(Request["Data"]);
            //JValue parameters = JsonConvert.<Newtonsoft.Json.Linq.JValue>(JsonConvert.SerializeObject(Data));
            SqlExper sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, "17");


            try
            {
                foreach (JObject insertObj in parameters)
                {
                    string sql = string.Format(@"SELECT ID from [FINSAT6{0}].[FINSAT6{0}].[Fiyat] F
                    JOIN[FINSAT6{0}].[FINSAT6{0}].[STK] S ON S.MalKodu = F.MalKodu
                    WHERE S.GrupKod <> 'PARKE' AND S.GrupKod = '{1}' AND F.FiyatListNum = '{2}'
                    AND S.TipKod = '{3}' AND S.Kod2 = '{4}' AND S.Kod3 = '{5}' AND S.Kod1 ='{6}'
                    AND S.Kod8 ='{7}'
                    AND F.SatisFiyat1 = {8} AND F.SatisFiyat1Birim = '{9}' AND F.SatisFiyat1BirimInt = {10}
                    AND F.DovizSatisFiyat1 = {11} AND F.DovizSF1Birim = '{12}' AND F.DovizSF1BirimInt = {13}
                    AND F.DovizCinsi = '{14}' AND F.Onay = 1 AND F.GMOnay = 0 AND F.SPGMYOnay = 0 ", "17", insertObj["GrupKod"].ToString(),
                    insertObj["FiyatListNum"].ToString(), insertObj["Kalite"].ToString(),
                    insertObj["En"].ToString(), insertObj["Boy"].ToString(), insertObj["Kalinlik"].ToString(), insertObj["Yuzey"].ToString(),
                    insertObj["SatisFiyat1"].ToString().Replace(",", "."), insertObj["SatisFiyat1Birim"].ToString(),
                    insertObj["SatisFiyat1BirimInt"].ToInt32(), insertObj["DovizSatisFiyat1"].ToString().Replace(",", "."),
                    insertObj["DovizSF1Birim"].ToString(), insertObj["DovizSF1BirimInt"].ToInt32(), insertObj["DovizCinsi"].ToString()
                    );
                    var fiyatID = db.Database.SqlQuery<int>(sql).ToList();
                    var result = String.Join(", ", fiyatID.ToArray());
                    //sql = string.Format("SELECT * FROM [FINSAT6{0}].[FINSAT6{0}].[Fiyat] where ID in({1})", "17", result);

                    db.Database.ExecuteSqlCommand(string.Format("DELETE FROM [FINSAT6{0}].[FINSAT6{0}].[Fiyat]  where ID in({1})", "17", result));
                }
                _Result.Message = "İşlem Başarılı";
                _Result.Status = true;
            }
            catch (Exception ex)
            {
                _Result.Message = "Hata Oluştu";
                _Result.Status = false;
            }
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }



        public string FiyatSatirEkle(string Data)
        {
            if (CheckPerm("FiyatSatirEkle", PermTypes.Writing) == false) return null;
            JObject parameters = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JObject>(Request["Data"]);
            //JValue parameters = JsonConvert.<Newtonsoft.Json.Linq.JValue>(JsonConvert.SerializeObject(Data));
            SqlExper sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, "17");
            try
            {
                Fiyat fyt = new Fiyat()
                {
                    FiyatListNum = parameters["ListeNo"].ToString(),

                    HesapKodu = parameters["HesapKodu"].ToString(),
                    MalKodu = parameters["UrunKodu"].ToString(),
                    SatisFiyat1 = parameters["SatisFiyat"].ToDecimal(),
                    SatisFiyat1Birim = parameters["Birim"].ToString(),
                    SatisFiyat1BirimInt = parameters["BirimValue"].ToInt32(),
                    DovizSatisFiyat1 = parameters["DovizSatisFiyat"].ToDecimal(),
                    DovizSF1Birim = parameters["DovizBirim"].ToString(),
                    DovizSF1BirimInt = parameters["DovizBirimValue"].ToInt32(),
                    DovizCinsi = parameters["Birim"].ToString(),
                    Durum = parameters["Durum"].ToShort(),
                    ROW_ID = 0
                };
                if (fyt.MalKodu.Substring(0, 2) == "80" || fyt.MalKodu.Substring(0, 2) == "81"
                       || fyt.MalKodu == "M60101000080" || fyt.MalKodu == "M60101000081")
                {
                    fyt.Onay = true;
                    fyt.Onaylayan = "";
                    fyt.SPGMYOnay = true;
                    fyt.SPGMYOnaylayan = "";
                    fyt.GMOnay = false;
                    fyt.GMOnaylayan = "";
                }
                /// Fiyatlar İçin KağıtFiltre Kontrolü
                else if (fyt.MalKodu.StartsWith("2800") || parameters["GrupKod"].ToString() == "FKAĞIT" || fyt.MalKodu == "M001001000017051" || fyt.MalKodu == "M001001000022051") ///2800% ile başlayan ve Navlun ile Sigorta  
                {
                    ///Filtre kağıt önce SM sonra da GMye düşecek.
                    fyt.Onay = false;
                    fyt.Onaylayan = "OZ";  ///SMden Sadece Özgür Beye düşmesi için yapıldı. 
                    fyt.SPGMYOnay = true;  ///SPGMY düşmemesi için direk true diyorum
                    fyt.SPGMYOnaylayan = "";
                    fyt.GMOnay = false;
                    fyt.GMOnaylayan = "";
                }
                else
                {
                    fyt.Onay = false;
                    fyt.Onaylayan = "";
                    fyt.SPGMYOnay = false;
                    fyt.SPGMYOnaylayan = "";
                    fyt.GMOnay = false;
                    fyt.GMOnaylayan = "";
                }
                sqlexper.Insert(fyt);
                var sonuc = sqlexper.AcceptChanges();
                return "OK";
            }
            catch (Exception ex)
            {
                return "NO";
            }
        }

        #endregion
        #region Sipariş
        public ActionResult Siparis_SM()
        {
            if (CheckPerm("Sipariş Onaylama", PermTypes.Reading) == false) return Redirect("/");
            return View();
        }
        public PartialViewResult Siparis_SM_List()
        {
            if (CheckPerm("Sipariş Onaylama", PermTypes.Reading) == false) return null;
            var KOD = db.Database.SqlQuery<SMSiparisOnaySelect>(string.Format("[FINSAT6{0}].[wms].[SiparisOnayListSM]", "17")).ToList();
            return PartialView(KOD);
        }
        public ActionResult Siparis_SPGMY()
        {
            if (CheckPerm("Sipariş Onaylama", PermTypes.Reading) == false) return Redirect("/");
            return View();
        }
        public PartialViewResult Siparis_SPGMY_List()
        {
            if (CheckPerm("Sipariş Onaylama", PermTypes.Reading) == false) return null;
            var KOD = db.Database.SqlQuery<SMSiparisOnaySelect>(string.Format("[FINSAT6{0}].[wms].[SiparisOnayListSPGMY]", "17")).ToList();
            return PartialView(KOD);
        }
        public ActionResult Siparis_GM()
        {
            if (CheckPerm("Sipariş Onaylama", PermTypes.Reading) == false) return Redirect("/");
            return View();
        }
        public PartialViewResult Siparis_GM_List()
        {
            if (CheckPerm("Sipariş Onaylama", PermTypes.Reading) == false) return null;
            var KOD = db.Database.SqlQuery<SMSiparisOnaySelect>(string.Format("[FINSAT6{0}].[wms].[SiparisOnayListGM]", "17")).ToList();
            return PartialView(KOD);
        }
        public JsonResult Siparis_Onay(string Data, int OnayTip, bool OnaylandiMi)
        {

            if (CheckPerm("Sipariş Onaylama", PermTypes.Writing) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            Result _Result = new Result(true);
            JArray parameters = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JArray>(Request["Data"]);
            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                foreach (string insertObj in parameters)
                {
                    if (OnayTip == 3 && OnaylandiMi == true)//GMOnay
                    { db.Database.ExecuteSqlCommand(string.Format("[FINSAT6{0}].[wms].[SP_SiparisOnay] @EvrakNo = '{1}',@Kullanici = '{2}',@OnayTip={3},@OnaylandiMi={4}", "17", insertObj, vUser.UserName, 3, 1)); }
                    else if (OnayTip == 2 && OnaylandiMi == true)//SPGMYOnay
                    { db.Database.ExecuteSqlCommand(string.Format("[FINSAT6{0}].[wms].[SP_SiparisOnay] @EvrakNo = '{1}',@Kullanici = '{2}',@OnayTip={3},@OnaylandiMi={4}", "17", insertObj, vUser.UserName, 2, 1)); }
                    else if (OnayTip == 1 && OnaylandiMi == true)//SMOnay
                    { db.Database.ExecuteSqlCommand(string.Format("[FINSAT6{0}].[wms].[SP_SiparisOnay] @EvrakNo = '{1}',@Kullanici = '{2}',@OnayTip={3},@OnaylandiMi={4}", "17", insertObj, vUser.UserName, 1, 1)); }
                    else if (OnayTip == 3 && OnaylandiMi == false)//GMRet
                    { db.Database.ExecuteSqlCommand(string.Format("[FINSAT6{0}].[wms].[SP_SiparisOnay] @EvrakNo = '{1}',@Kullanici = '{2}',@OnayTip={3},@OnaylandiMi={4}", "17", insertObj, vUser.UserName, 3, 0)); }
                    else if (OnayTip == 2 && OnaylandiMi == false)//SPGMYRet
                    { db.Database.ExecuteSqlCommand(string.Format("[FINSAT6{0}].[wms].[SP_SiparisOnay] @EvrakNo = '{1}',@Kullanici = '{2}',@OnayTip={3},@OnaylandiMi={4}", "17", insertObj, vUser.UserName, 2, 0)); }
                    else if (OnayTip == 1 && OnaylandiMi == false)//SMRet
                    { db.Database.ExecuteSqlCommand(string.Format("[FINSAT6{0}].[wms].[SP_SiparisOnay] @EvrakNo = '{1}',@Kullanici = '{2}',@OnayTip={3},@OnaylandiMi={4}", "17", insertObj, vUser.UserName, 1, 0)); }
                }

                try
                {
                    dbContextTransaction.Commit();

                    _Result.Status = true;
                    _Result.Message = "İşlem Başarılı ";
                }
                catch (Exception)
                {

                    _Result.Status = false;
                    _Result.Message = "Hata Oluştu.";
                }
            }

            return Json(_Result, JsonRequestBehavior.AllowGet);




        }
        #endregion
        #region Stok
        public ActionResult Stok()
        {
            if (CheckPerm("Stok Onaylama", PermTypes.Reading) == false) return Redirect("/");
            return View();
        }
        public PartialViewResult Stok_List()
        {
            if (CheckPerm("Stok Onaylama", PermTypes.Reading) == false) return null;
            return PartialView();
        }
        public string StokOnayCek(string Durum)
        {
            if (CheckPerm("Stok Onaylama", PermTypes.Reading) == false) return null;
            int param = 1;
            if (Durum == "Tumu") { param = 0; }
            else
            if (Durum == "Onay") { param = 1; }
            else
            if (Durum == "Pasif") { param = 2; }
            else
            if (Durum == "Aktif") { param = 3; }
            else
            if (Durum == "Red") { param = 4; }
            var KOD = db.Database.SqlQuery<StokOnaySelect>(string.Format("[FINSAT6{0}].[wms].[StokOnaySelect] {1}", "17", param)).ToList();
            var json = new JavaScriptSerializer().Serialize(KOD);
            return json;
        }
        public JsonResult Stok_Onay(string Data)
        {
            Result _Result = new Result(true);
            if (CheckPerm("Stok Onaylama", PermTypes.Writing) == false) return null;
            //string sql = string.Format(@"INSERT INTO FINSAT";
            JArray parameters = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JArray>(Request["Data"]);
            //JValue parameters = JsonConvert.<Newtonsoft.Json.Linq.JValue>(JsonConvert.SerializeObject(Data));
            SqlExper sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, "17");

            try
            {
                Dictionary<string, int> FiyatMaxSiraNo = new Dictionary<string, int>();
                foreach (JObject insertObj in parameters)
                {
                    // var fytData = db.Database.SqlQuery<FYT>(string.Format("SELECT ISNULL(Max(FiyatListName),'') as FiyatListName,ISNULL(Max(BasTarih),0) as BasTarih,ISNULL(Max(BitTarih),0) as BitTarih FROM[FINSAT6{0}].[FINSAT6{0}].[FYT] where FiyatListNum = '{1}'", "17", insertObj["FiyatListNum"].ToString())).FirstOrDefault();
                    DateTime date = DateTime.Now;
                    var shortDate = date.ToString("yyyy-MM-dd");
                    var sonuc = sqlexper.AcceptChanges();
                    db.Database.ExecuteSqlCommand(string.Format("UPDATE [FINSAT6{0}].[FINSAT6{0}].[STK] SET AktifPasif = 0, CheckSum =1  where MalKodu = '{1}'", "17", insertObj["MalKodu"].ToString()));




                }
                //return "OK";

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
        public JsonResult Stok_Red(string Data)
        {
            Result _Result = new Result(true);
            if (CheckPerm("Stok Onaylama", PermTypes.Writing) == false) return null;
            //string sql = string.Format(@"INSERT INTO FINSAT";
            JArray parameters = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JArray>(Request["Data"]);
            //JValue parameters = JsonConvert.<Newtonsoft.Json.Linq.JValue>(JsonConvert.SerializeObject(Data));
            SqlExper sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, "17");

            try
            {
                Dictionary<string, int> FiyatMaxSiraNo = new Dictionary<string, int>();
                foreach (JObject insertObj in parameters)
                {
                    // var fytData = db.Database.SqlQuery<FYT>(string.Format("SELECT ISNULL(Max(FiyatListName),'') as FiyatListName,ISNULL(Max(BasTarih),0) as BasTarih,ISNULL(Max(BitTarih),0) as BitTarih FROM[FINSAT6{0}].[FINSAT6{0}].[FYT] where FiyatListNum = '{1}'", "17", insertObj["FiyatListNum"].ToString())).FirstOrDefault();
                    DateTime date = DateTime.Now;
                    var shortDate = date.ToString("yyyy-MM-dd");
                    var sonuc = sqlexper.AcceptChanges();
                    db.Database.ExecuteSqlCommand(string.Format("UPDATE [FINSAT6{0}].[FINSAT6{0}].[STK] SET AktifPasif = 0, CheckSum =-1  where MalKodu = '{1}'", "17", insertObj["MalKodu"].ToString()));




                }
                //return "OK";

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
        #endregion
        #region Teminat
        public ActionResult Teminat()
        {
            if (CheckPerm("Teminat Onaylama", PermTypes.Reading) == false) return Redirect("/");
            return View();
        }
        public PartialViewResult Teminat_List()
        {
            if (CheckPerm("Teminat Onaylama", PermTypes.Reading) == false) return null;
            return PartialView();
        }
        public string TeminatOnayCek()
        {
            if (CheckPerm("Teminat Onaylama", PermTypes.Reading) == false) return null;
            var RT = db.Database.SqlQuery<TeminatOnaySelect>(string.Format("[FINSAT6{0}].[wms].[TeminatOnayList]", "17")).ToList();
            var json = new JavaScriptSerializer().Serialize(RT);
            return json;
        }

        public JsonResult Teminat_Onay(string Data)
        {
            Result _Result = new Result(true);
            if (CheckPerm("Teminat Onaylama", PermTypes.Writing) == false) return null;
            //string sql = string.Format(@"INSERT INTO FINSAT";
            JArray parameters = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JArray>(Request["Data"]);
            //JValue parameters = JsonConvert.<Newtonsoft.Json.Linq.JValue>(JsonConvert.SerializeObject(Data));
            SqlExper sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, "17");

            try
            {
                Dictionary<string, int> FiyatMaxSiraNo = new Dictionary<string, int>();
                foreach (JObject insertObj in parameters)
                {
                    // var fytData = db.Database.SqlQuery<FYT>(string.Format("SELECT ISNULL(Max(FiyatListName),'') as FiyatListName,ISNULL(Max(BasTarih),0) as BasTarih,ISNULL(Max(BitTarih),0) as BitTarih FROM[FINSAT6{0}].[FINSAT6{0}].[FYT] where FiyatListNum = '{1}'", "17", insertObj["FiyatListNum"].ToString())).FirstOrDefault();
                    DateTime date = DateTime.Now;
                    var shortDate = date.ToString("yyyy-MM-dd");
                    var sonuc = sqlexper.AcceptChanges();
                    db.Database.ExecuteSqlCommand(string.Format("[FINSAT6{0}].[wms].[TeminatOnayUpdate] @ID = {1}, @Kullanici = '{2}'", "17", insertObj["ID"].ToString(), vUser.UserName));
                   // { db.Database.ExecuteSqlCommand(string.Format("[FINSAT6{0}].[wms].[SP_SiparisOnay] @EvrakNo = '{1}',@Kullanici = '{2}',@OnayTip={3},@OnaylandiMi={4}", "17", insertObj, vUser.UserName, 3, 1)); }



                }
                //return "OK";

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
        public JsonResult Teminat_Red(string Data)
        {
            Result _Result = new Result(true);
            if (CheckPerm("Teminat Onaylama", PermTypes.Writing) == false) return null;
            //string sql = string.Format(@"INSERT INTO FINSAT";
            JArray parameters = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JArray>(Request["Data"]);
            //JValue parameters = JsonConvert.<Newtonsoft.Json.Linq.JValue>(JsonConvert.SerializeObject(Data));
            SqlExper sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, "17");

            try
            {
                Dictionary<string, int> FiyatMaxSiraNo = new Dictionary<string, int>();
                foreach (JObject insertObj in parameters)
                {
                    // var fytData = db.Database.SqlQuery<FYT>(string.Format("SELECT ISNULL(Max(FiyatListName),'') as FiyatListName,ISNULL(Max(BasTarih),0) as BasTarih,ISNULL(Max(BitTarih),0) as BitTarih FROM[FINSAT6{0}].[FINSAT6{0}].[FYT] where FiyatListNum = '{1}'", "17", insertObj["FiyatListNum"].ToString())).FirstOrDefault();
                    DateTime date = DateTime.Now;
                    var shortDate = date.ToString("yyyy-MM-dd");
                    var sonuc = sqlexper.AcceptChanges();
                    db.Database.ExecuteSqlCommand(string.Format("DELETE FROM [FINSAT6{0}].[FINSAT6{0}].[Teminat] WHERE  ID = {1}", "17", insertObj["ID"].ToString()));
                    // { db.Database.ExecuteSqlCommand(string.Format("[FINSAT6{0}].[wms].[SP_SiparisOnay] @EvrakNo = '{1}',@Kullanici = '{2}',@OnayTip={3},@OnaylandiMi={4}", "17", insertObj, vUser.UserName, 3, 1)); }



                }
                //return "OK";

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
        public ActionResult TeminatTanim()
        {
            if (CheckPerm("Teminat Onaylama", PermTypes.Reading) == false) return Redirect("/");
            var CHK = db.Database.SqlQuery<RaporCHKSelect>(string.Format("[FINSAT6{0}].[dbo].[CHKSelect1]", "17")).ToList();
            return View(CHK);
        }
        public string Teminatdurbun()
        {
            var DRBN = db.Database.SqlQuery<RaporCHKSelect>(string.Format("[FINSAT6{0}].[dbo].[TeminatDurbunSelect]", "17")).ToList();
            var json = new JavaScriptSerializer().Serialize(DRBN);
            return json;
        }

        public string TeminatBekleyen()
        {
            var BKLYN = db.Database.SqlQuery<TeminatSelect>(string.Format("[FINSAT6{0}].[wms].[TeminatOnayList]", "17")).ToList();
            var json = new JavaScriptSerializer().Serialize(BKLYN);
            return json;
        }


        public PartialViewResult TeminatTanimList(string chk)
        {
            if (CheckPerm("Teminat Onaylama", PermTypes.Reading) == false) return null;
            //var TMNT = db.Database.SqlQuery<TeminatSelect>(string.Format("[FINSAT6{0}].[dbo].[TeminatOnaySelect]", "17")).ToList();
            ViewBag.CHK = chk;
            return PartialView("TeminatTanimList");
        }

        public string TeminatSelect(string chk)
        {
            var TMNT = db.Database.SqlQuery<TeminatSelect>(string.Format("[FINSAT6{0}].[dbo].[TeminatOnaySelect] @HesapKodu='{1}'", "17", chk)).ToList();
            var json = new JavaScriptSerializer().Serialize(TMNT);
            return json;
        }

        public string TeminatSil(int ID)
        {
            var sonuc = db.Database.SqlQuery<int>(string.Format("[FINSAT6{0}].[dbo].[TeminatSil] @ID={1}", "17", ID)).FirstOrDefault();
            if (sonuc == 1)
            {
                return "OK";
            }
            else
            {
                return "NO";
            }

        }
        #endregion
        #region Sözleşme
        public ActionResult Sozlesme_GM()
        {
            if (CheckPerm("Sözleşme Onaylama", PermTypes.Reading) == false) return Redirect("/");
            return View();
        }
        [HttpPost]
        public PartialViewResult Sozlesme_GM_List()
        {
            if (CheckPerm("Sözleşme Onaylama", PermTypes.Reading) == false) return null;
           // List<SozlesmeOnaySelect> list = db.Database.SqlQuery<SozlesmeOnaySelect>(string.Format("[FINSAT6{0}].[wms].[SP_SozlesmeOnay]", "17")).ToList();
            return PartialView();
        }
        public string SozlesmeOnayCekGM()
        {
            if (CheckPerm("Fiyat Onaylama", PermTypes.Reading) == false) return null;
            var RT = db.Database.SqlQuery<SozlesmeOnaySelect>(string.Format("[FINSAT6{0}].[wms].[SP_SozlesmeOnay]", "17")).ToList();
            var json = new JavaScriptSerializer().Serialize(RT);
            return json;
        }
        public JsonResult Sozlesme_Onay_GM(string Data)
        {
            Result _Result = new Result(true);
            if (CheckPerm("Sözleşme Onaylama", PermTypes.Writing) == false) return null;
            //string sql = string.Format(@"INSERT INTO FINSAT";
            JArray parameters = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JArray>(Request["Data"]);
            //JValue parameters = JsonConvert.<Newtonsoft.Json.Linq.JValue>(JsonConvert.SerializeObject(Data));
            SqlExper sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, "17");
            try
            {
               
                foreach (JObject insertObj in parameters)
                {


                    db.Database.ExecuteSqlCommand(string.Format("[FINSAT6{0}].[dbo].[SozlesmeGenelMudurOnay] @SozlesmeNo = '{1}',@Kullanici = '{2}'", "17", insertObj["ListeNo"].ToString(), vUser.UserName)); 

                    var list = db.Database.SqlQuery<ISS_Temp>(string.Format("SELECT *  FROM [FINSAT6{0}].[FINSAT6{0}].[ISS_Temp] WHERE ListeNo='{1}'", "17", insertObj["ListeNo"].ToString())).ToList();


                    foreach (ISS_Temp lst in list)
                    {
                        if (lst.OnayTip == 2)
                        {
                            ISS insrt = new ISS();
                            insrt.Aciklama = lst.Aciklama;
                            insrt.BasSaat = lst.BasSaat;
                            insrt.BasTarih = lst.BasTarih;
                            insrt.BitSaat = lst.BitSaat;
                            insrt.BitTarih = lst.BitTarih;
                            insrt.DegisKaynak = lst.DegisKaynak;
                            insrt.DegisSaat = lst.DegisSaat;
                            insrt.DegisSurum = lst.DegisSurum;
                            insrt.DegisTarih = lst.DegisTarih;
                            insrt.Degistiren = lst.Degistiren;
                            insrt.DevirTarih = lst.DevirTarih;
                            insrt.DevirTutar = lst.DevirTutar;
                            insrt.GuvenlikKod = lst.GuvenlikKod;
                            insrt.Kaydeden = lst.Kaydeden;
                            insrt.KayitKaynak = lst.KayitKaynak;
                            insrt.KayitSaat = lst.KayitSaat;
                            insrt.KayitSurum = lst.KayitSurum;
                            insrt.KayitTarih = lst.KayitTarih;
                            insrt.KayitTuru = lst.KayitTuru;
                            insrt.Kod1 = lst.Kod1;
                            insrt.Kod10 = lst.Kod10;
                            insrt.Kod11 = lst.Kod11;
                            insrt.Kod12 = lst.Kod12;
                            insrt.Kod2 = lst.Kod2;
                            insrt.Kod3 = lst.Kod3;
                            insrt.Kod4 = lst.Kod4;
                            insrt.Kod5 = lst.Kod5;
                            insrt.Kod6 = lst.Kod6;
                            insrt.Kod7 = lst.Kod7;
                            insrt.Kod8 = lst.Kod8;
                            insrt.Kod9 = lst.BaglantiParaCinsi; ///lst.Kod9;
                            insrt.ListeAdi = lst.ListeAdi;
                            insrt.ListeNo = lst.ListeNo;
                            insrt.MalKod = lst.MalKod;
                            insrt.MalKodGrup = lst.MalKodGrup;
                            insrt.MalUygSekli = lst.MalUygSekli;
                            insrt.MikAralik1 = lst.MikAralik1;
                            insrt.MikAralik2 = lst.MikAralik2;
                            insrt.MikAralik3 = lst.MikAralik3;
                            insrt.MikAralik4 = lst.MikAralik4;
                            insrt.MikAralik5 = lst.MikAralik5;
                            insrt.MikAralik6 = lst.MikAralik6;
                            insrt.MikAralik7 = lst.MikAralik7;
                            insrt.MikAralik8 = lst.MikAralik8;
                            insrt.MikYuzde1 = lst.MikYuzde1;
                            insrt.MikYuzde2 = lst.MikYuzde2;
                            insrt.MikYuzde3 = lst.MikYuzde3;
                            insrt.MikYuzde4 = lst.MikYuzde4;
                            insrt.MikYuzde5 = lst.MikYuzde5;
                            insrt.MikYuzde6 = lst.MikYuzde6;
                            insrt.MikYuzde7 = lst.MikYuzde7;
                            insrt.MikYuzde8 = lst.MikYuzde8;
                            insrt.MusKodGrup = lst.MusKodGrup;
                            insrt.MusteriKod = lst.MusteriKod;
                            insrt.MusUygSekli = lst.MusUygSekli;
                            insrt.OdemeAralik1 = lst.OdemeAralik1;
                            insrt.OdemeAralik2 = lst.OdemeAralik2;
                            insrt.OdemeAralik3 = lst.OdemeAralik3;
                            insrt.OdemeAralik4 = lst.OdemeAralik4;
                            insrt.OdemeAralik5 = lst.OdemeAralik5;
                            insrt.OdemeAralik6 = lst.OdemeAralik6;
                            insrt.OdemeAralik7 = lst.OdemeAralik7;
                            insrt.OdemeAralik8 = lst.OdemeAralik8;
                            insrt.OdemeYuzde1 = lst.OdemeYuzde1;
                            insrt.OdemeYuzde2 = lst.OdemeYuzde2;
                            insrt.OdemeYuzde3 = lst.OdemeYuzde3;
                            insrt.OdemeYuzde4 = lst.OdemeYuzde4;
                            insrt.OdemeYuzde5 = lst.OdemeYuzde5;
                            insrt.OdemeYuzde6 = lst.OdemeYuzde6;
                            insrt.OdemeYuzde7 = lst.OdemeYuzde7;
                            insrt.OdemeYuzde8 = lst.OdemeYuzde8;
                            insrt.Oran = lst.Oran;
                            insrt.Oran1 = lst.Oran1;
                            insrt.Oran2 = lst.Oran2;
                            insrt.Oran3 = lst.Oran3;
                            insrt.Oran4 = lst.Oran4;
                            insrt.Oran5 = lst.Oran5;
                            insrt.SiraNo = lst.SiraNo;
                            insrt.TutarAralik1 = lst.TutarAralik1;
                            insrt.TutarAralik2 = lst.TutarAralik2;
                            insrt.TutarAralik3 = lst.TutarAralik3;
                            insrt.TutarAralik4 = lst.TutarAralik4;
                            insrt.TutarAralik5 = lst.TutarAralik5;
                            insrt.TutarAralik6 = lst.TutarAralik6;
                            insrt.TutarAralik7 = lst.TutarAralik7;
                            insrt.TutarAralik8 = lst.TutarAralik8;
                            insrt.TutarYuzde1 = lst.TutarYuzde1;
                            insrt.TutarYuzde2 = lst.TutarYuzde2;
                            insrt.TutarYuzde3 = lst.TutarYuzde3;
                            insrt.TutarYuzde4 = lst.TutarYuzde4;
                            insrt.TutarYuzde5 = lst.TutarYuzde5;
                            insrt.TutarYuzde6 = lst.TutarYuzde6;
                            insrt.TutarYuzde7 = lst.TutarYuzde7;
                            insrt.TutarYuzde8 = lst.TutarYuzde8;
                            sqlexper.Insert(insrt);
                         
                        }
                    }
                    var sonuc = sqlexper.AcceptChanges();
                }
                //return "OK";

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

        [HttpPost]
        public PartialViewResult Sozlesme_GM_Details(string ListeNo)
        {
            if (CheckPerm("Sözleşme Onaylama", PermTypes.Reading) == false) return null;
            var list = db.Database.SqlQuery<BaglantiDetaySelect1>(string.Format("[FINSAT6{0}].[wms].[BaglantiDetaySelect1] '{1}'", "17", ListeNo)).ToList();
            return PartialView(list);
        }
        public ActionResult Sozlesme_SM()
        {
            if (CheckPerm("Sözleşme Onaylama", PermTypes.Reading) == false) return Redirect("/");
            return View();
        }
        public PartialViewResult Sozlesme_SM_List()
        {
            if (CheckPerm("Sözleşme Onaylama", PermTypes.Reading) == false) return null;
            List<SozlesmeOnaySelect> list = db.Database.SqlQuery<SozlesmeOnaySelect>(string.Format("[FINSAT6{0}].[wms].[SP_SozlesmeOnaySM]", "17")).ToList();
            return PartialView(list);
        }
        [HttpPost]
        public PartialViewResult Sozlesme_SM_Details(string ListeNo)
        {
            if (CheckPerm("Sözleşme Onaylama", PermTypes.Reading) == false) return null;
            var list = db.Database.SqlQuery<BaglantiDetaySelect1>(string.Format("[FINSAT6{0}].[wms].[BaglantiDetaySelect1] '{1}'", "17", ListeNo)).ToList();
            return PartialView(list);
        }
        public ActionResult Sozlesme_SPGMY()
        {
            if (CheckPerm("Sözleşme Onaylama", PermTypes.Reading) == false) return Redirect("/");
            return View();
        }
        public PartialViewResult Sozlesme_SPGMY_List()
        {
            if (CheckPerm("Sözleşme Onaylama", PermTypes.Reading) == false) return null;
            List<SozlesmeOnaySelect> list = db.Database.SqlQuery<SozlesmeOnaySelect>(string.Format("[FINSAT6{0}].[wms].[SP_SozlesmeOnaySPGMY]", "17")).ToList();
            return PartialView(list);
        }
        [HttpPost]
        public PartialViewResult Sozlesme_SPMY_Details(string ListeNo)
        {
            if (CheckPerm("Sözleşme Onaylama", PermTypes.Reading) == false) return null;
            var list = db.Database.SqlQuery<BaglantiDetaySelect1>(string.Format("[FINSAT6{0}].[wms].[BaglantiDetaySelect1] '{1}'", "17", ListeNo)).ToList();
            return PartialView(list);
        }

        public ActionResult SozlesmeTanim()
        {
            if (CheckPerm("Sözleşme Onaylama", PermTypes.Reading) == false) return Redirect("/");
            ViewBag.SRNO = "SOZ " + db.Database.SqlQuery<int>(string.Format("[FINSAT6{0}].[dbo].[SozlesmeSiraNoSelect]", "17")).FirstOrDefault();
            return View();
        }

        public PartialViewResult PartialSozlesmeTanim(string listeNo, string satir)
        {
            if (CheckPerm("Fiyat Onaylama", PermTypes.Reading) == false) return null;
            //var TMNT = db.Database.SqlQuery<TeminatSelect>(string.Format("[FINSAT6{0}].[dbo].[TeminatOnaySelect]", "17")).ToList();
            ViewBag.ListeNo = listeNo;
            ViewBag.Satir = satir;
            return PartialView("SozlesmeTanim_List");
        }

        public string SozlesmeTanimListesiSelect(string listeNo)
        {
            var STL = new List<BaglantiDetaySelect1>();
            if (listeNo != "#12MConsulting#") { 
            STL = db.Database.SqlQuery<BaglantiDetaySelect1>(string.Format("[FINSAT6{0}].[dbo].[BaglantiDetaySelect1] @ListeNo='{1}'", "17", listeNo)).ToList();
            }
            var json = new JavaScriptSerializer().Serialize(STL);
            return json;
        }

        public string SozlesmeListesi(int tip)
        {
            var sozlesmeler = new List<SozlesmeListesi>();
            if (tip == 0)
            {
                sozlesmeler = db.Database.SqlQuery<SozlesmeListesi>(string.Format("[FINSAT6{0}].[dbo].[SozlesmeOnaylanmisList]", "17")).ToList();
            }
            else
            {
                sozlesmeler = db.Database.SqlQuery<SozlesmeListesi>(string.Format("[FINSAT6{0}].[dbo].[SozlesmeOnaylanmamisList]", "17")).ToList();
            }
            var json = new JavaScriptSerializer().Serialize(sozlesmeler);
            return json;
        }

        public string ISSTempList(string SozlesmeNo)
        {
            JavaScriptSerializer json = new JavaScriptSerializer()
            {
                MaxJsonLength = int.MaxValue
            };
            var ISSTemp = db.Database.SqlQuery<ISS_Temp>(string.Format("SELECT * FROM [FINSAT6{0}].[FINSAT6{0}].[ISS_Temp] WHERE ListeNo='{1}'", "17", SozlesmeNo)).ToList();
            return json.Serialize(ISSTemp);
        }
        public string FiyatListeleriSelect()
        {
            JavaScriptSerializer json = new JavaScriptSerializer()
            {
                MaxJsonLength = int.MaxValue
            };
            var FYTList = db.Database.SqlQuery<ListeNoSelect>(string.Format("[FINSAT6{0}].[dbo].[FYTSelect2]", "17")).ToList();
            return json.Serialize(FYTList);
        }

        public string SozlesmeCariBilgileri(string ListeNo, string HesapKodu)
        {
            JavaScriptSerializer json = new JavaScriptSerializer()
            {
                MaxJsonLength = int.MaxValue
            };
            List<SozlesmeCariBilgileri> chkBilgi;
            try
            {
                chkBilgi = db.Database.SqlQuery<SozlesmeCariBilgileri>(string.Format("[FINSAT6{0}].[dbo].[SozlesmeCariBilgileri] @HesapKodu='{2}', @ListeNo='{1}'", "17", ListeNo, HesapKodu)).ToList();
            }
            catch (Exception)
            {
                chkBilgi = new List<Entity.SozlesmeCariBilgileri>();
            }
            return json.Serialize(chkBilgi);
        }
        public string SozlesmeUrunGrupSelect(int Index)
        {
            var FUGS = db.Database.SqlQuery<UrunGrup>(string.Format("[FINSAT6{0}].[dbo].[STKSelect2] @Index={1}", "17",Index)).ToList();
            var json = new JavaScriptSerializer().Serialize(FUGS);
            return json;
        }

        public int SozlesmeListeNoKontrol( string ListeNo)
        {
            var VarMi = db.Database.SqlQuery<int>(string.Format("SELECT Count(ListeNo) FROM [FINSAT6{0}].[FINSAT6{0}].[ISS_Temp] WHERE ListeNo='{1}'", "17", ListeNo)).FirstOrDefault();
            return VarMi;
        }

        public JsonResult ISS_TempUpdate_AktifPasif(string SozlesmeNo, bool AktifPasif)
        {
            Result _Result = new Result(true);
            if (CheckPerm("Fiyat Onaylama", PermTypes.Writing) == false) return null;
            //JArray parameters = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JArray>(Request["Data"]);
            //JValue parameters = JsonConvert.<Newtonsoft.Json.Linq.JValue>(JsonConvert.SerializeObject(Data));
            SqlExper sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, "17");

            try
            {
                var ISSS = db.Database.SqlQuery<ISS_Temp>(string.Format("SELECT * FROM [FINSAT6{0}].[FINSAT6{0}].[ISS_Temp]", "17")).ToList();
                if (!AktifPasif)   ///Pasif In Aktif olunca
                {
                    db.Database.ExecuteSqlCommand(string.Format("UPDATE [FINSAT6{0}].[FINSAT6{0}].[ISS_Temp] SET AktifPasif = 0  where ListeNo = '{1}'", "17", SozlesmeNo));
                    db.Database.ExecuteSqlCommand(string.Format("DELETE FROM [FINSAT6{0}].[FINSAT6{0}].[ISS] where ListeNo = '{1}'", "17", SozlesmeNo));
                }
                else
                {
                    db.Database.ExecuteSqlCommand(string.Format("UPDATE [FINSAT6{0}].[FINSAT6{0}].[ISS_Temp] SET AktifPasif = 1  where ListeNo = '{1}'", "17", SozlesmeNo));
                    foreach (ISS_Temp lst in ISSS)
                    {
                        ISS insrt = new ISS()
                        {
                            Aciklama = lst.Aciklama,
                            BasSaat = lst.BasSaat,
                            BasTarih = lst.BasTarih,
                            BitSaat = lst.BitSaat,
                            BitTarih = lst.BitTarih,
                            DegisKaynak = lst.DegisKaynak,
                            DegisSaat = lst.DegisSaat,
                            DegisSurum = lst.DegisSurum,
                            DegisTarih = lst.DegisTarih,
                            Degistiren = lst.Degistiren,
                            DevirTarih = lst.DevirTarih,
                            DevirTutar = lst.DevirTutar,
                            GuvenlikKod = lst.GuvenlikKod,
                            Kaydeden = lst.Kaydeden,
                            KayitKaynak = lst.KayitKaynak,
                            KayitSaat = lst.KayitSaat,
                            KayitSurum = lst.KayitSurum,
                            KayitTarih = lst.KayitTarih,
                            KayitTuru = lst.KayitTuru,
                            Kod1 = lst.Kod1,
                            Kod10 = lst.Kod10,
                            Kod11 = lst.Kod11,
                            Kod12 = lst.Kod12,
                            Kod2 = lst.Kod2,
                            Kod3 = lst.Kod3,
                            Kod4 = lst.Kod4,
                            Kod5 = lst.Kod5,
                            Kod6 = lst.Kod6,
                            Kod7 = lst.Kod7,
                            Kod8 = lst.Kod8,
                            Kod9 = lst.BaglantiParaCinsi, ///lst.Kod9;
                            ListeAdi = lst.ListeAdi,
                            ListeNo = lst.ListeNo,
                            MalKod = lst.MalKod,
                            MalKodGrup = lst.MalKodGrup,
                            MalUygSekli = lst.MalUygSekli,
                            MikAralik1 = lst.MikAralik1,
                            MikAralik2 = lst.MikAralik2,
                            MikAralik3 = lst.MikAralik3,
                            MikAralik4 = lst.MikAralik4,
                            MikAralik5 = lst.MikAralik5,
                            MikAralik6 = lst.MikAralik6,
                            MikAralik7 = lst.MikAralik7,
                            MikAralik8 = lst.MikAralik8,
                            MikYuzde1 = lst.MikYuzde1,
                            MikYuzde2 = lst.MikYuzde2,
                            MikYuzde3 = lst.MikYuzde3,
                            MikYuzde4 = lst.MikYuzde4,
                            MikYuzde5 = lst.MikYuzde5,
                            MikYuzde6 = lst.MikYuzde6,
                            MikYuzde7 = lst.MikYuzde7,
                            MikYuzde8 = lst.MikYuzde8,
                            MusKodGrup = lst.MusKodGrup,
                            MusteriKod = lst.MusteriKod,
                            MusUygSekli = lst.MusUygSekli,
                            OdemeAralik1 = lst.OdemeAralik1,
                            OdemeAralik2 = lst.OdemeAralik2,
                            OdemeAralik3 = lst.OdemeAralik3,
                            OdemeAralik4 = lst.OdemeAralik4,
                            OdemeAralik5 = lst.OdemeAralik5,
                            OdemeAralik6 = lst.OdemeAralik6,
                            OdemeAralik7 = lst.OdemeAralik7,
                            OdemeAralik8 = lst.OdemeAralik8,
                            OdemeYuzde1 = lst.OdemeYuzde1,
                            OdemeYuzde2 = lst.OdemeYuzde2,
                            OdemeYuzde3 = lst.OdemeYuzde3,
                            OdemeYuzde4 = lst.OdemeYuzde4,
                            OdemeYuzde5 = lst.OdemeYuzde5,
                            OdemeYuzde6 = lst.OdemeYuzde6,
                            OdemeYuzde7 = lst.OdemeYuzde7,
                            OdemeYuzde8 = lst.OdemeYuzde8,
                            Oran = lst.Oran,
                            Oran1 = lst.Oran1,
                            Oran2 = lst.Oran2,
                            Oran3 = lst.Oran3,
                            Oran4 = lst.Oran4,
                            Oran5 = lst.Oran5,
                            SiraNo = lst.SiraNo,
                            TutarAralik1 = lst.TutarAralik1,
                            TutarAralik2 = lst.TutarAralik2,
                            TutarAralik3 = lst.TutarAralik3,
                            TutarAralik4 = lst.TutarAralik4,
                            TutarAralik5 = lst.TutarAralik5,
                            TutarAralik6 = lst.TutarAralik6,
                            TutarAralik7 = lst.TutarAralik7,
                            TutarAralik8 = lst.TutarAralik8,
                            TutarYuzde1 = lst.TutarYuzde1,
                            TutarYuzde2 = lst.TutarYuzde2,
                            TutarYuzde3 = lst.TutarYuzde3,
                            TutarYuzde4 = lst.TutarYuzde4,
                            TutarYuzde5 = lst.TutarYuzde5,
                            TutarYuzde6 = lst.TutarYuzde6,
                            TutarYuzde7 = lst.TutarYuzde7,
                            TutarYuzde8 = lst.TutarYuzde8
                        };
                        var VarMi = db.Database.SqlQuery<int>(string.Format("SELECT Count(*) FROM [FINSAT6{0}].[FINSAT6{0}].[ISS] WHERE ListeNo='{1}' AND BasTarih={2} AND MusUygSekli='{3}' AND SiraNo={4}", "17", insrt.ListeNo, insrt.BasTarih, insrt.MusUygSekli, insrt.SiraNo)).FirstOrDefault();

                        if (VarMi > 0)
                        {
                            sqlexper.Insert(insrt);
                            var sonuc = sqlexper.AcceptChanges();
                        }
                    }
                   
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

        public string SozlesmeYeniSatirKayit(string Data)
        {
            if (CheckPerm("FiyatSatirEkle", PermTypes.Writing) == false) return null;
            JObject parameters = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JObject>(Request["Data"]);
            SqlExper sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, "17");
            return "";
        }

        public JsonResult SozlesmeSil(string SozlesmeNo)
        {
            if (CheckPerm("FiyatSatirEkle", PermTypes.Writing) == false) return null;
            Result _Result = new Result(true, "İşlem Başarılı.");
            try
            {
                db.Database.ExecuteSqlCommand(string.Format("DELETE FROM [FINSAT6{0}].[FINSAT6{0}].[ISS_Temp]  WHERE ListeNo = '{1}'", "17", SozlesmeNo));

            }
            catch (Exception)
            {

                _Result.Status = false;
                _Result.Message = "Hata Oluştu. ";

            }
            return Json(_Result, JsonRequestBehavior.AllowGet);

        }

        public JsonResult SozlesmeGuncelle(string SozlesmeNo, int BasTarih, short MusUygSekli, decimal YeniBaglantiTutari,int YeniBitisTarihi)
        {
            if (CheckPerm("FiyatSatirEkle", PermTypes.Writing) == false) return null;
            Result _Result = new Result(true, "İşlem Başarılı.");
            SqlExper sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, "17");
            try
            {
                bool filtreKagitVarmi = false;
                var list = db.Database.SqlQuery<ISS_Temp>(string.Format("SELECT * FROM [FINSAT6{0}].[FINSAT6{0}].[ISS_Temp] WHERE ListeNo='{1}' AND BasTarih = {2} AND MusUygSekli={3}", "17", SozlesmeNo,BasTarih,MusUygSekli)).ToList();
                foreach (ISS_Temp item in list)
                {
                    item.pk_ListeNo = SozlesmeNo;
                    item.pk_BasTarih = BasTarih;
                    item.pk_MusUygSekli = MusUygSekli;
                    item.pk_SiraNo = item.SiraNo;
                    item.Kod12 = item.Kod11;
                    item.Kod11 = YeniBaglantiTutari;


                    if (YeniBitisTarihi != item.BitTarih)
                    {
                        item.Kod9 = item.BitTarih.ToString2();
                        item.BitTarih = YeniBitisTarihi;
                    }

                    if ((item.MalKodGrup == 0 && item.MalKod.StartsWith("2800")) ||
                     (item.MalKodGrup == 1 && item.MalKod == "FKAĞIT") ||
                      (item.MalKodGrup == 0 && (item.MalKod == "M001001000017051" || item.MalKod == "M001001000022051")))
                    {

                        filtreKagitVarmi = true;
                    }

                    item.SatisMuduruOnay = false;
                    item.FinansmanMuduruOnay = false;
                    item.GenelMudurOnay = false;
                    item.OnaylayanSatisMuduru = "";
                    item.OnaylayanFinansmanMuduru = "";
                    item.OnaylayanGenelMudur = "";
                    item.OnayTarihSatisMuduru = null;
                    item.OnayTarihGenelMudur = null;
                    item.OnayTarihFinansmanMuduru = null;

                    sqlexper.Update(item, null, null, false, "timestamp");
                }

                if (filtreKagitVarmi)
                {
                    foreach (var item in list)
                    {
                        item.OnayTip = 2;
                        item.SatisMuduruOnay = false;
                        item.FinansmanMuduruOnay = true;
                        item.GenelMudurOnay = false;
                        item.OnaylayanSatisMuduru = "OZ";  /// SM sadece Özgür Beyin onayına düşsün diye
                        item.OnaylayanFinansmanMuduru = "";
                        item.OnaylayanGenelMudur = "";

                        sqlexper.Update(item, null, null, false, "timestamp");
                    }
                }

                var sonuc = sqlexper.AcceptChanges();
                if (sonuc.Status == true) {
                db.Database.ExecuteSqlCommand(string.Format("DELETE FROM [FINSAT6{0}].[FINSAT6{0}].[ISS] where ListeNo='{1}' AND BasTarih = {2} AND MusUygSekli={3}", "17", SozlesmeNo, BasTarih, MusUygSekli));
                }
                else
                {
                    _Result.Status = false;
                    _Result.Message = "Hata Oluştu. ";
                }

                //db.Database.ExecuteSqlCommand(string.Format("DELETE FROM [FINSAT6{0}].[FINSAT6{0}].[ISS_Temp]  WHERE ListeNo = '{1}'", "17", SozlesmeNo));

            }
            catch (Exception ex)
            {

                _Result.Status = false;
                _Result.Message = "Hata Oluştu. ";

            }
            return Json(_Result, JsonRequestBehavior.AllowGet);
            //JValue parameters = JsonConvert.<Newtonsoft.Json.Linq.JValue>(JsonConvert.SerializeObject(Data));

        }
        #endregion
        #region Risk



        public ActionResult Risk_SM()
        {
            if (CheckPerm("Risk Onaylama", PermTypes.Reading) == false) return Redirect("/");
            return View();
        }
        public PartialViewResult Risk_SM_List()
        {
            if (CheckPerm("Risk Onaylama", PermTypes.Reading) == false) return null;
            return PartialView();
        }
        public string RiskOnayCekSM()
        {
            if (CheckPerm("Risk Onaylama", PermTypes.Reading) == false) return null;
            var RT = db.Database.SqlQuery<RiskTanim>(string.Format("SELECT *   FROM [FINSAT6{0}].[FINSAT6{0}].[RiskTanim]   where OnayTip = 0 and SMOnay = 0 and Durum = 0", "17")).ToList();
            var json = new JavaScriptSerializer().Serialize(RT);
            return json;
        }
        public JsonResult Risk_Onay_SM(string Data)
        {
            Result _Result = new Result(true);
            if (CheckPerm("Risk Onaylama", PermTypes.Writing) == false) return null;
            //string sql = string.Format(@"INSERT INTO FINSAT";
            JArray parameters = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JArray>(Request["Data"]);
            //JValue parameters = JsonConvert.<Newtonsoft.Json.Linq.JValue>(JsonConvert.SerializeObject(Data));
            SqlExper sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, "17");

            try
            {
               
                foreach (JObject insertObj in parameters)
                {
                    // var fytData = db.Database.SqlQuery<FYT>(string.Format("SELECT ISNULL(Max(FiyatListName),'') as FiyatListName,ISNULL(Max(BasTarih),0) as BasTarih,ISNULL(Max(BitTarih),0) as BitTarih FROM[FINSAT6{0}].[FINSAT6{0}].[FYT] where FiyatListNum = '{1}'", "17", insertObj["FiyatListNum"].ToString())).FirstOrDefault();
                    DateTime date = DateTime.Now;
                    var shortDate = date.ToString("yyyy-MM-dd");
                    var sonuc = sqlexper.AcceptChanges();
                    db.Database.ExecuteSqlCommand(string.Format("UPDATE [FINSAT6{0}].[FINSAT6{0}].[RiskTanim] SET SMOnay = 1, SMOnaylayan='" + vUser.UserName + "', SMOnayTarih='{2}'  where ID = '{1}'", "17", insertObj["ID"].ToString(), shortDate));
                    // { db.Database.ExecuteSqlCommand(string.Format("[FINSAT6{0}].[wms].[SP_SiparisOnay] @EvrakNo = '{1}',@Kullanici = '{2}',@OnayTip={3},@OnaylandiMi={4}", "17", insertObj, vUser.UserName, 3, 1)); }
                }
                //return "OK";

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
        public JsonResult Risk_Red_SM(string Data)
        {
            Result _Result = new Result(true);
            if (CheckPerm("Risk Onaylama", PermTypes.Writing) == false) return null;
            //string sql = string.Format(@"INSERT INTO FINSAT";
            JArray parameters = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JArray>(Request["Data"]);
            //JValue parameters = JsonConvert.<Newtonsoft.Json.Linq.JValue>(JsonConvert.SerializeObject(Data));
            SqlExper sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, "17");

            try
            {

                foreach (JObject insertObj in parameters)
                {
                    // var fytData = db.Database.SqlQuery<FYT>(string.Format("SELECT ISNULL(Max(FiyatListName),'') as FiyatListName,ISNULL(Max(BasTarih),0) as BasTarih,ISNULL(Max(BitTarih),0) as BitTarih FROM[FINSAT6{0}].[FINSAT6{0}].[FYT] where FiyatListNum = '{1}'", "17", insertObj["FiyatListNum"].ToString())).FirstOrDefault();
                    DateTime date = DateTime.Now;
                    var shortDate = date.ToString("yyyy-MM-dd");
                    var sonuc = sqlexper.AcceptChanges();
                    db.Database.ExecuteSqlCommand(string.Format("DELETE FROM [FINSAT6{0}].[FINSAT6{0}].[RiskTanim] where ID = '{1}'", "17", insertObj["ID"].ToString()));
                    // { db.Database.ExecuteSqlCommand(string.Format("[FINSAT6{0}].[wms].[SP_SiparisOnay] @EvrakNo = '{1}',@Kullanici = '{2}',@OnayTip={3},@OnaylandiMi={4}", "17", insertObj, vUser.UserName, 3, 1)); }



                }
                //return "OK";

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


        public ActionResult Risk_GM()
        {
            if (CheckPerm("Risk Onaylama", PermTypes.Reading) == false) return Redirect("/");
            return View();
        }
        public PartialViewResult Risk_GM_List()
        {
            if (CheckPerm("Risk Onaylama", PermTypes.Reading) == false) return null;
            return PartialView();
        }
        public string RiskOnayCekGM()
        {
            if (CheckPerm("Risk Onaylama", PermTypes.Reading) == false) return null;
            var RT = db.Database.SqlQuery<RiskTanim>(string.Format("SELECT *   FROM [FINSAT6{0}].[FINSAT6{0}].[RiskTanim]  where (OnayTip = 3 and SPGMYOnay = 1 and MIGMYOnay = 1 and GMOnay=0) or (OnayTip = 4 and GMOnay = 0 ) and Durum =0", "17")).ToList();
            var json = new JavaScriptSerializer().Serialize(RT);
            return json;
        }
        public JsonResult Risk_Onay_GM(string Data)
        {
            Result _Result = new Result(true);
            if (CheckPerm("Risk Onaylama", PermTypes.Writing) == false) return null;
            //string sql = string.Format(@"INSERT INTO FINSAT";
            JArray parameters = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JArray>(Request["Data"]);
            //JValue parameters = JsonConvert.<Newtonsoft.Json.Linq.JValue>(JsonConvert.SerializeObject(Data));
            SqlExper sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, "17");

            try
            {

                foreach (JObject insertObj in parameters)
                {
                    // var fytData = db.Database.SqlQuery<FYT>(string.Format("SELECT ISNULL(Max(FiyatListName),'') as FiyatListName,ISNULL(Max(BasTarih),0) as BasTarih,ISNULL(Max(BitTarih),0) as BitTarih FROM[FINSAT6{0}].[FINSAT6{0}].[FYT] where FiyatListNum = '{1}'", "17", insertObj["FiyatListNum"].ToString())).FirstOrDefault();
                    DateTime date = DateTime.Now;
                    var shortDate = date.ToString("yyyy-MM-dd");
                    var sonuc = sqlexper.AcceptChanges();
                    db.Database.ExecuteSqlCommand(string.Format("UPDATE [FINSAT6{0}].[FINSAT6{0}].[RiskTanim] SET GMOnay = 1, GMOnaylayan='" + vUser.UserName + "', GMOnayTarih='{2}'  where ID = '{1}'", "17", insertObj["ID"].ToString(), shortDate));
                    // { db.Database.ExecuteSqlCommand(string.Format("[FINSAT6{0}].[wms].[SP_SiparisOnay] @EvrakNo = '{1}',@Kullanici = '{2}',@OnayTip={3},@OnaylandiMi={4}", "17", insertObj, vUser.UserName, 3, 1)); }



                }
                //return "OK";

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
        public JsonResult Risk_Red_GM(string Data)
        {
            Result _Result = new Result(true);
            if (CheckPerm("Risk Onaylama", PermTypes.Writing) == false) return null;
            //string sql = string.Format(@"INSERT INTO FINSAT";
            JArray parameters = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JArray>(Request["Data"]);
            //JValue parameters = JsonConvert.<Newtonsoft.Json.Linq.JValue>(JsonConvert.SerializeObject(Data));
            SqlExper sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, "17");

            try
            {

                foreach (JObject insertObj in parameters)
                {
                    // var fytData = db.Database.SqlQuery<FYT>(string.Format("SELECT ISNULL(Max(FiyatListName),'') as FiyatListName,ISNULL(Max(BasTarih),0) as BasTarih,ISNULL(Max(BitTarih),0) as BitTarih FROM[FINSAT6{0}].[FINSAT6{0}].[FYT] where FiyatListNum = '{1}'", "17", insertObj["FiyatListNum"].ToString())).FirstOrDefault();
                    DateTime date = DateTime.Now;
                    var shortDate = date.ToString("yyyy-MM-dd");
                    var sonuc = sqlexper.AcceptChanges();
                    db.Database.ExecuteSqlCommand(string.Format("DELETE FROM [FINSAT6{0}].[FINSAT6{0}].[RiskTanim] where ID = '{1}'", "17", insertObj["ID"].ToString()));
                    // { db.Database.ExecuteSqlCommand(string.Format("[FINSAT6{0}].[wms].[SP_SiparisOnay] @EvrakNo = '{1}',@Kullanici = '{2}',@OnayTip={3},@OnaylandiMi={4}", "17", insertObj, vUser.UserName, 3, 1)); }



                }
                //return "OK";

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

        public ActionResult Risk_SPGMY()
        {
            if (CheckPerm("Risk Onaylama", PermTypes.Reading) == false) return Redirect("/");
            return View();
        }
        public PartialViewResult Risk_SPGMY_List()
        {
            if (CheckPerm("Risk Onaylama", PermTypes.Reading) == false) return null;
            return PartialView();
        }
        public string RiskOnayCekSPGMY()
        {
            if (CheckPerm("Risk Onaylama", PermTypes.Reading) == false) return null;
            var RT = db.Database.SqlQuery<RiskTanim>(string.Format("SELECT *   FROM [FINSAT6{0}].[FINSAT6{0}].[RiskTanim]   where (OnayTip = 1 and SPGMYOnay =0) OR (OnayTip = 2 and SPGMYOnay =0) OR (OnayTip = 3 and SPGMYOnay = 0) and Durum = 0", "17")).ToList();
            var json = new JavaScriptSerializer().Serialize(RT);
            return json;
        }
        public JsonResult Risk_Onay_SPGMY(string Data)
        {
            Result _Result = new Result(true);
            if (CheckPerm("Risk Onaylama", PermTypes.Writing) == false) return null;
            //string sql = string.Format(@"INSERT INTO FINSAT";
            JArray parameters = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JArray>(Request["Data"]);
            //JValue parameters = JsonConvert.<Newtonsoft.Json.Linq.JValue>(JsonConvert.SerializeObject(Data));
            SqlExper sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, "17");

            try
            {

                foreach (JObject insertObj in parameters)
                {
                    // var fytData = db.Database.SqlQuery<FYT>(string.Format("SELECT ISNULL(Max(FiyatListName),'') as FiyatListName,ISNULL(Max(BasTarih),0) as BasTarih,ISNULL(Max(BitTarih),0) as BitTarih FROM[FINSAT6{0}].[FINSAT6{0}].[FYT] where FiyatListNum = '{1}'", "17", insertObj["FiyatListNum"].ToString())).FirstOrDefault();
                    DateTime date = DateTime.Now;
                    var shortDate = date.ToString("yyyy-MM-dd");
                    var sonuc = sqlexper.AcceptChanges();
                    db.Database.ExecuteSqlCommand(string.Format("UPDATE [FINSAT6{0}].[FINSAT6{0}].[RiskTanim] SET SPGMYOnay = 1, SPGMYOnaylayan='" + vUser.UserName + "', SPGMYOnayTarih='{2}'  where ID = '{1}'", "17", insertObj["ID"].ToString(), shortDate));
                    // { db.Database.ExecuteSqlCommand(string.Format("[FINSAT6{0}].[wms].[SP_SiparisOnay] @EvrakNo = '{1}',@Kullanici = '{2}',@OnayTip={3},@OnaylandiMi={4}", "17", insertObj, vUser.UserName, 3, 1)); }



                }
                //return "OK";

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
        public JsonResult Risk_Red_SPGMY(string Data)
        {
            Result _Result = new Result(true);
            if (CheckPerm("Risk Onaylama", PermTypes.Writing) == false) return null;
            //string sql = string.Format(@"INSERT INTO FINSAT";
            JArray parameters = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JArray>(Request["Data"]);
            //JValue parameters = JsonConvert.<Newtonsoft.Json.Linq.JValue>(JsonConvert.SerializeObject(Data));
            SqlExper sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, "17");

            try
            {

                foreach (JObject insertObj in parameters)
                {
                    // var fytData = db.Database.SqlQuery<FYT>(string.Format("SELECT ISNULL(Max(FiyatListName),'') as FiyatListName,ISNULL(Max(BasTarih),0) as BasTarih,ISNULL(Max(BitTarih),0) as BitTarih FROM[FINSAT6{0}].[FINSAT6{0}].[FYT] where FiyatListNum = '{1}'", "17", insertObj["FiyatListNum"].ToString())).FirstOrDefault();
                    DateTime date = DateTime.Now;
                    var shortDate = date.ToString("yyyy-MM-dd");
                    var sonuc = sqlexper.AcceptChanges();
                    db.Database.ExecuteSqlCommand(string.Format("DELETE FROM [FINSAT6{0}].[FINSAT6{0}].[RiskTanim] where ID = '{1}'", "17", insertObj["ID"].ToString()));
                    // { db.Database.ExecuteSqlCommand(string.Format("[FINSAT6{0}].[wms].[SP_SiparisOnay] @EvrakNo = '{1}',@Kullanici = '{2}',@OnayTip={3},@OnaylandiMi={4}", "17", insertObj, vUser.UserName, 3, 1)); }



                }
                //return "OK";

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


        public ActionResult Risk_MIGMY()
        {
            if (CheckPerm("Risk Onaylama", PermTypes.Reading) == false) return Redirect("/");
            return View();
        }
        public PartialViewResult Risk_MIGMY_List()
        {
            if (CheckPerm("Risk Onaylama", PermTypes.Reading) == false) return null;
            return PartialView();
        }
        public string RiskOnayCekMIGMY()
        {
            if (CheckPerm("Risk Onaylama", PermTypes.Reading) == false) return null;
            var RT = db.Database.SqlQuery<RiskTanim>(string.Format("SELECT *   FROM [FINSAT6{0}].[FINSAT6{0}].[RiskTanim]  where (OnayTip = 2 and SPGMYOnay = 1 and MIGMYOnay = 0) OR (OnayTip = 3 and SPGMYOnay = 1 and MIGMYOnay = 0) and Durum = 0", "17")).ToList();
            var json = new JavaScriptSerializer().Serialize(RT);
            return json;
        }

        public JsonResult Risk_Onay_MIGMY(string Data)
        {
            Result _Result = new Result(true);
            if (CheckPerm("Risk Onaylama", PermTypes.Writing) == false) return null;
            //string sql = string.Format(@"INSERT INTO FINSAT";
            JArray parameters = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JArray>(Request["Data"]);
            //JValue parameters = JsonConvert.<Newtonsoft.Json.Linq.JValue>(JsonConvert.SerializeObject(Data));
            SqlExper sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, "17");

            try
            {

                foreach (JObject insertObj in parameters)
                {
                    // var fytData = db.Database.SqlQuery<FYT>(string.Format("SELECT ISNULL(Max(FiyatListName),'') as FiyatListName,ISNULL(Max(BasTarih),0) as BasTarih,ISNULL(Max(BitTarih),0) as BitTarih FROM[FINSAT6{0}].[FINSAT6{0}].[FYT] where FiyatListNum = '{1}'", "17", insertObj["FiyatListNum"].ToString())).FirstOrDefault();
                    DateTime date = DateTime.Now;
                    var shortDate = date.ToString("yyyy-MM-dd");
                    var sonuc = sqlexper.AcceptChanges();
                    db.Database.ExecuteSqlCommand(string.Format("UPDATE [FINSAT6{0}].[FINSAT6{0}].[RiskTanim] SET MIGMYOnay = 1, MIGMYOnaylayan='" + vUser.UserName + "', MIGMYOnayTarih='{2}'  where ID = '{1}'", "17", insertObj["ID"].ToString(), shortDate));
                    // { db.Database.ExecuteSqlCommand(string.Format("[FINSAT6{0}].[wms].[SP_SiparisOnay] @EvrakNo = '{1}',@Kullanici = '{2}',@OnayTip={3},@OnaylandiMi={4}", "17", insertObj, vUser.UserName, 3, 1)); }



                }
                //return "OK";

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

        public JsonResult Risk_Red_MIGMY(string Data)
        {
            Result _Result = new Result(true);
            if (CheckPerm("Risk Onaylama", PermTypes.Writing) == false) return null;
            //string sql = string.Format(@"INSERT INTO FINSAT";
            JArray parameters = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JArray>(Request["Data"]);
            //JValue parameters = JsonConvert.<Newtonsoft.Json.Linq.JValue>(JsonConvert.SerializeObject(Data));
            SqlExper sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, "17");

            try
            {

                foreach (JObject insertObj in parameters)
                {
                    // var fytData = db.Database.SqlQuery<FYT>(string.Format("SELECT ISNULL(Max(FiyatListName),'') as FiyatListName,ISNULL(Max(BasTarih),0) as BasTarih,ISNULL(Max(BitTarih),0) as BitTarih FROM[FINSAT6{0}].[FINSAT6{0}].[FYT] where FiyatListNum = '{1}'", "17", insertObj["FiyatListNum"].ToString())).FirstOrDefault();
                    DateTime date = DateTime.Now;
                    var shortDate = date.ToString("yyyy-MM-dd");
                    var sonuc = sqlexper.AcceptChanges();
                    db.Database.ExecuteSqlCommand(string.Format("DELETE FROM [FINSAT6{0}].[FINSAT6{0}].[RiskTanim] where ID = '{1}'", "17", insertObj["ID"].ToString()));
                    // { db.Database.ExecuteSqlCommand(string.Format("[FINSAT6{0}].[wms].[SP_SiparisOnay] @EvrakNo = '{1}',@Kullanici = '{2}',@OnayTip={3},@OnaylandiMi={4}", "17", insertObj, vUser.UserName, 3, 1)); }



                }
                //return "OK";

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

        public ActionResult RiskTanim()
        {
            if (CheckPerm("Risk Onaylama", PermTypes.Reading) == false) return Redirect("/");
            return View();
        }
        public PartialViewResult RiskTanimPartial()
        {
            if (CheckPerm("Risk Onaylama", PermTypes.Reading) == false) return null;
            var RT = db.Database.SqlQuery<RiskTanimToplu>(string.Format("[FINSAT6{0}].[dbo].[CHKSelect2]", "17")).ToList();
            return PartialView(RT);
        }
        public string GGGG()
        {
            if (CheckPerm("Risk Onaylama", PermTypes.Reading) == false) return null;
            var RT = db.Database.SqlQuery<RiskTanimToplu>(string.Format("[FINSAT6{0}].[dbo].[CHKSelect2]", "17")).ToList();
            var json = new JavaScriptSerializer().Serialize(RT);
            return json;
        }
        public string OnayRiskInsert(string Data)
        {
            if (CheckPerm("Risk Onaylama", PermTypes.Writing) == false) return null;
            //string sql = string.Format(@"INSERT INTO FINSAT";
            JArray parameters = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JArray>(Request["Data"]);
            //JValue parameters = JsonConvert.<Newtonsoft.Json.Linq.JValue>(JsonConvert.SerializeObject(Data));
            SqlExper sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, "17");
            try
            {
                foreach (JObject insertObj in parameters)
                {
                    if (insertObj["YeniSahsiCekLimiti"].ToDecimal() <= 0)
                        continue;

                    RiskTanim rsk = new RiskTanim()
                    {
                        HesapKodu = insertObj["HesapKodu"].ToString(),
                        Unvan = insertObj["Unvan"].ToString(),
                        SahsiCekLimiti = insertObj["SahsiCekLimiti"].ToDecimal(),
                        MusteriCekLimiti = insertObj["MusteriCekLimiti"].ToDecimal(),
                        SMOnay = false,
                        SMOnaylayan = "",
                        SPGMYOnay = false,
                        SPGMYOnaylayan = "",
                        MIGMYOnay = false,
                        MIGMYOnaylayan = "",
                        GMOnay = false,
                        GMOnaylayan = "",
                        Durum = false
                    };
                    if (Convert.ToDecimal(insertObj["YeniSahsiCekLimiti"]) < 20000)
                    {
                        rsk.OnayTip = 0;
                    }
                    else if (Convert.ToDecimal(insertObj["YeniSahsiCekLimiti"]) < 100000)
                    {
                        rsk.OnayTip = 1;
                    }
                    else if (Convert.ToDecimal(insertObj["YeniSahsiCekLimiti"]) < 200000)
                    {
                        rsk.OnayTip = 2;
                    }
                    else if (Convert.ToDecimal(insertObj["YeniSahsiCekLimiti"]) < 500000)
                    {
                        rsk.OnayTip = 3;
                    }
                    else if (Convert.ToDecimal(insertObj["YeniSahsiCekLimiti"]) >= 500000)
                    {
                        rsk.OnayTip = 4;
                    }
                    else
                    {
                        rsk.OnayTip = -1;
                    }

                    sqlexper.Insert(rsk);
                    var sonuc = sqlexper.AcceptChanges();
                }
                return "OK";
            }
            catch (Exception ex)
            {
                return "NO";
            }
        }
        #endregion
        #region Satınalma
        public ActionResult Satınalma_GM_Onay()
        {
            if (CheckPerm("Risk Onaylama", PermTypes.Reading) == false) return Redirect("/");
            MyGlobalVariables.DovizDurum = false;
            MyGlobalVariables.SipTalepList = db.Database.SqlQuery<SatTalep>(string.Format("[FINSAT6{0}].[wms].[SatinAlmaGMOnayList]", "17")).ToList();
            return View(MyGlobalVariables.SipTalepList);
        }

        public PartialViewResult SipGMOnayList(string HesapKodu, int SipTalepNo)
        {
            if (CheckPerm("Fiyat Onaylama", PermTypes.Reading) == false) return null;
            //var TMNT = db.Database.SqlQuery<SatTalep>(string.Format("[FINSAT6{0}].[dbo].[SatinAlmaGMOnayListData] @HesapKodu='{1}', @SipTalepNo={2} ", "17", HesapKodu, SipTalepNo)).ToList();
            ViewBag.HesapKodu = HesapKodu;
            ViewBag.SipTalepNo = SipTalepNo;
            return PartialView("SatinalmaSipGMOnay_List");
        }
        public PartialViewResult SipGMOnayListFTD(string HesapKodu, int SipTalepNo)
        {
            if (CheckPerm("Fiyat Onaylama", PermTypes.Reading) == false) return null;
            //var TMNT = db.Database.SqlQuery<SatTalep>(string.Format("[FINSAT6{0}].[dbo].[SatinAlmaGMOnayListData] @HesapKodu='{1}', @SipTalepNo={2} ", "17", HesapKodu, SipTalepNo)).ToList();
            ViewBag.HesapKodu = HesapKodu;
            ViewBag.SipTalepNo = SipTalepNo;
            return PartialView("SatinalmaSipGMOnayFTD_List");
        }
        public string SipGMOnayListData(string HesapKodu, int SipTalepNo)
        {
            if (MyGlobalVariables.GridSource == null)
                MyGlobalVariables.GridSource = new List<KKP_SPI>();
            else
                MyGlobalVariables.GridSource.Clear();

            MyGlobalVariables.SipEvrak = new KKPEvrakSiparis(KKPSiparisTip.AlimSiparisi);

            MyGlobalVariables.TalepSource = db.Database.SqlQuery<SatTalep>(string.Format("[FINSAT6{0}].[wms].[SatinAlmaGMOnayListData] @HesapKodu='{1}', @SipTalepNo={2} ", "17", HesapKodu, SipTalepNo)).ToList();
            if (MyGlobalVariables.TalepSource[0].SipIslemTip == null || (MyGlobalVariables.TalepSource[0].SipIslemTip != 1 && MyGlobalVariables.TalepSource[0].SipIslemTip != 2))
                return "";

            //SipEvrak.dvzTL = (KKPDvzTL)(short)TalepSource[0].DvzTL;
            //SipEvrak.DovizCinsi = TalepSource[0].DvzCinsi;
            //SipEvrak.DovizKuru = TalepSource[0].DvzKuru ?? 0;
            MyGlobalVariables.SipEvrak.IslemTip = (KKPIslemTipSPI)MyGlobalVariables.TalepSource[0].SipIslemTip;
            MyGlobalVariables.SipEvrak.dvzTL = (KKPDvzTL)(short)MyGlobalVariables.TalepSource[0].FTDDovizTL;
            short siraNo = 0;
            foreach (var item in MyGlobalVariables.TalepSource)
            {
                if ((short)item.DvzTL == (short)1)
                {
                    MyGlobalVariables.DovizDurum = true;
                }
                KKP_SPI spi = new KKP_SPI(KKPSiparisTip.AlimSiparisi);
                spi.MalKodu = item.MalKodu;
                spi.MalAdi = item.MalAdi;
                spi.Birim = item.Birim;
                spi.BirimMiktar = item.BirimMiktar;
                spi.Miktar = Convert.ToDecimal(item.Miktar);
                spi.Fiyat = (decimal)item.BirimFiyat;
                spi.BirimFiyat = (decimal)item.BirimFiyat;

                spi.DvzTL = (short)item.DvzTL;
                spi.DovizCinsi = item.DvzCinsi;
                spi.DovizKuru = item.DvzKuru ?? 0;

                spi.Aciklama = item.Aciklama;
                spi.TeklifAciklamasi = item.TeklifAciklamasi;

                spi.KDVOran = item.KDVOran;
                spi.SiraNo = siraNo;

                spi.SatirHesapla();

                spi.Kod1 = item.TalepNo;
                spi.Kod2 = item.Satinalmaci;
                spi.Kod3 = item.SipTalepNo.ToString();
                spi.Kod4 = item.TeklifNo.ToString();

                spi.Kaydeden = item.Kaydeden;
                spi.Nesne3 = item.TesisKodu == null ? "" : item.TesisKodu;
                spi.SatinAlmaci = item.Satinalmaci;

                spi.Kod11 = item.TeklifVade ?? 0; //Sırf ekranda göstermek için Kod11' e teklif de ki Vade atıyoruz. Ve kaydediyoruz. SPI.Kod11 daha sonra değiştirilip silinebilir önemli değil. (şimdilik)

                //spi.Depo = Degiskenler.SiparisDepo;

                spi.Operator = (short)item.Operator.Value;
                spi.Katsayi = item.Katsayi.Value;

                MyGlobalVariables.SipEvrak.Ekle(spi);
                MyGlobalVariables.GridSource.Add(spi);
                siraNo++;
            }

            if (MyGlobalVariables.DovizDurum == false)
            {
                MyGlobalVariables.SipEvrak.FTDHesapla("TL", Convert.ToDecimal(0));
                MyGlobalVariables.GridFTD = MyGlobalVariables.SipEvrak.FTDList;
            }
            else
            {
                decimal kur = MyGlobalVariables.TalepSource[0].FTDDovizKuru.Value;
                if (kur > 0)
                {
                    MyGlobalVariables.SipEvrak.FTDHesapla(MyGlobalVariables.TalepSource[0].FTDDovizCinsi, kur);
                    MyGlobalVariables.GridFTD = MyGlobalVariables.SipEvrak.FTDList;
                }
            }

            var json = new JavaScriptSerializer().Serialize(MyGlobalVariables.GridSource);
            return json;
        }

        public string SipGMOnayListFTDData(string HesapKodu, int SipTalepNo)
        {
            var json = new JavaScriptSerializer().Serialize(MyGlobalVariables.GridFTD);
            return json;
        }

        public JsonResult SipGMOnayla()
        {
            Result _Result = new Result();
            _Result.Message = "İşlem Başarılı";
            _Result.Status = true;
            if (MyGlobalVariables.TalepSource == null)
            {
                _Result.Message = "Sipariş Seçin";
                _Result.Status = false;
                return Json(_Result, JsonRequestBehavior.AllowGet);
            }
                

            using (KKP kkp = new KKP(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, "17"))
            {
                try
                {
                    string evrakno = kkp.YeniEvrakNo(KKPKynkEvrakTip.AlımSiparişi, 1);
                    foreach (var item in MyGlobalVariables.TalepSource)
                    {
                        //Durum 11: Sipariş Ön Onay; 15: Onaylı Sipariş
                        string sql = string.Format(@"UPDATE sta.Talep 
    SET GMOnaylayan=@Degistiren, GMOnayTarih=@DegisTarih, Durum=15, SipEvrakNo=@SipEvrakNo
    , SirketKodu='{0}'
    , Degistiren=@Degistiren, DegisTarih=@DegisTarih, DegisSirKodu='{0}'
    WHERE ID=@ID AND Durum=11 AND SipTalepNo IS NOT NULL", "17");


                        SqlParameter[] paramlist = new SqlParameter[4]
                        {
                            new SqlParameter("ID", SqlDbType.Int){ Value = item.ID},
                            new SqlParameter("Degistiren", SqlDbType.VarChar){ Value = vUser.UserName.ToString()},
                            new SqlParameter("DegisTarih", SqlDbType.SmallDateTime){ Value = DateTime.Now},
                            new SqlParameter("SipEvrakNo", SqlDbType.VarChar){Value = evrakno}
                        };

                        kkp.ExecuteCommandOnUpdate(sql, true, paramlist);
                    }


                    foreach (var item in MyGlobalVariables.SipEvrak.Satirlar)
                    {
                        item.Aciklama = item.Aciklama.Length > 64 ? item.Aciklama.Substring(0, 64) : item.Aciklama;
                    }

                    kkp.SiparisEvrakList.Add(MyGlobalVariables.SipEvrak);
                    MyGlobalVariables.SipEvrak.MFK.Aciklama = "";
                    MyGlobalVariables.SipEvrak.MFK.Aciklama2 = "";
                    MyGlobalVariables.SipEvrak.MFK.Aciklama3 = "";
                    MyGlobalVariables.SipEvrak.MFK.Aciklama4 = "";
                    MyGlobalVariables.SipEvrak.MFK.Aciklama5 = "";
                    MyGlobalVariables.SipEvrak.MFK.Aciklama6 = "";



                    MyGlobalVariables.SipEvrak.EvrakNo = evrakno;
                    MyGlobalVariables.SipEvrak.HesapKodu = MyGlobalVariables.TalepSource[0].HesapKodu;
                    MyGlobalVariables.SipEvrak.TeslimYeriKodu = MyGlobalVariables.TalepSource[0].HesapKodu;
                    MyGlobalVariables.SipEvrak.Tarih = DateTime.Today;
                    //SipEvrak.IslemTip = (KKPIslemTipSPI)TalepSource[0].SipIslemTip;
                    MyGlobalVariables.SipEvrak.INIGuncellensin = true;
                    MyGlobalVariables.SipEvrak.TahTeslimTarihi = new DateTime(1900, 1, 1).AddDays(-2);

                    if (MyGlobalVariables.DovizDurum == false)
                    {
                        MyGlobalVariables.SipEvrak.FTDHesapla("", Convert.ToDecimal(0));
                    }
                    else
                    {
                        decimal kur = MyGlobalVariables.TalepSource[0].FTDDovizKuru.Value;
                        if (kur > 0)
                        {
                            MyGlobalVariables.SipEvrak.FTDHesapla(MyGlobalVariables.TalepSource[0].FTDDovizCinsi, kur);
                        }
                        //else
                        //{
                        //    Mesaj.Uyari("Döviz Kuru 0'dan büyük bir değer olmalıdır!!");
                        //    return;
                        //}
                    }
                    //SipEvrak.FTDHesapla();


                    kkp.UpdateChanges();
                    //Mesaj.Basari(string.Format("İşlem başarılı bir şekilde gerçekleştirildi. Evrak No: {0}", evrakno));
                    //gridLueSipTalepNo.EditValue = null;
                    //Yenile();

                    int sipTarih = Convert.ToInt32(MyGlobalVariables.SipEvrak.Tarih.ToOADate());

                    //SatTalep talep = (SatTalep)gridLueSipTalepNo.GetSelectedDataRow();
                    //DbSat.SiparisOnayMailGonderim(SipEvrak.EvrakNo, SipEvrak.HesapKodu, sipTarih, Convert.ToInt32(SipEvrak.Satirlar[0].Kod4));

                }
                catch (Exception ex)
                {
                    _Result.Message = "İşlem Sırasında Hata Oluştu.";
                    _Result.Status = false;
                   
                }
                return Json(_Result, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult SipGMReddet( string redAciklama)
        {
            Result _Result = new Result();
            _Result.Message = "İşlem Başarılı";
            _Result.Status = true;

            if (MyGlobalVariables.GridSource == null || MyGlobalVariables.GridSource.Count == 0 || MyGlobalVariables.SipEvrak == null)
            {
                _Result.Message = "Siparis Seçmelisiniz!!";
                _Result.Status = false;
                return Json(_Result, JsonRequestBehavior.AllowGet);
            }
                

            if (redAciklama.Trim() == "")
            {
                _Result.Message = "Geri Çevirme açıklamasını girmek zorundasınız!!";
                _Result.Status = false;
                return Json(_Result, JsonRequestBehavior.AllowGet);
            }

            Connection con = Connection.GetConnectionWithTrans();
            try
            {
                foreach (var item in MyGlobalVariables.TalepSource)
                {
                    //Durum 11: Sipariş Ön Onay; 15: Onaylı Sipariş; 13: Sipariş Onay İptal
                    string sql = @"UPDATE sta.Talep 
SET GMOnaylayan='{0}', GMOnayTarih={1}, Durum=13
, Degistiren='{0}', DegisTarih={1}, DegisSirKodu={3}, Aciklama2='{2}'
WHERE ID={4} AND Durum=11 AND SipTalepNo IS NOT NULL";

                db.Database.ExecuteSqlCommand(string.Format(sql, vUser.UserName.ToString(), DateTime.Now.ToString("yyyy-MM-dd"), redAciklama, "17",item.ID));
                //con.Params.Add("ID", SqlDbType.Int);
                //con.Params.AddWithValue("Degistiren", vUser.UserName.ToString());
                //con.Params.AddWithValue("DegisTarih", DateTime.Now);
                //con.Params.AddWithValue("Aciklama", redAciklama);
                //con.Params.AddWithValue("DegisSirKodu", "17");

                }

                con.Trans.Commit();

                //DbTalep.SiparisdenGeriCevirmeMail(TalepSource, TalepOnayTip.SipGMOnay);

                //gridLueSipTalepNo.EditValue = null;
                //Yenile();
            }
            catch (Exception ex)
            {
                if (con.Trans != null)
                    con.Trans.Rollback();
                _Result.Message = "Geri Çevirme açıklamasını girmek zorundasınız!!";
                _Result.Status = false;

            }
            con.Dispose();
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
            #endregion
        }
}