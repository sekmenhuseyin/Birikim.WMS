﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Wms12m.Entity;

namespace Wms12m.Presentation.Controllers
{
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
            var KOD = db.Database.SqlQuery<CekOnaySelect>(string.Format("[FINSAT6{0}].[wms].[CekOnaySPGMY]", "33")).ToList();
            return PartialView(KOD);
        }

        public ActionResult Cek_MIGMY()
        {
            if (CheckPerm("Çek Onaylama", PermTypes.Reading) == false) return Redirect("/");
            return View();
        }
        public PartialViewResult Cek_MIGMY_List()
        {
            if (CheckPerm("Çek Onaylama", PermTypes.Reading) == false) return null;
            var KOD = db.Database.SqlQuery<CekOnaySelect>(string.Format("[FINSAT6{0}].[wms].[CekOnayMIGMY]", "33")).ToList();
            return PartialView(KOD);
        }

        public ActionResult Cek_GM()
        {
            if (CheckPerm("Çek Onaylama", PermTypes.Reading) == false) return Redirect("/");
            return View();
        }
        public PartialViewResult Cek_GM_List()
        {
            if (CheckPerm("Çek Onaylama", PermTypes.Reading) == false) return null;
            var KOD = db.Database.SqlQuery<CekOnaySelect>(string.Format("[FINSAT6{0}].[wms].[CekOnayGM]", "33")).ToList();
            return PartialView(KOD);
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
            var KOD = db.Database.SqlQuery<FiyatOnayGMSelect>(string.Format("[FINSAT6{0}].[wms].[FiyatOnayListGM]", "33")).ToList();
            return PartialView(KOD);
        }
        public ActionResult Fiyat_SPGMY()
        {
            if (CheckPerm("Fiyat Onaylama", PermTypes.Reading) == false) return Redirect("/");
            return View();
        }
        public PartialViewResult Fiyat_SPGMY_List()
        {
            if (CheckPerm("Fiyat Onaylama", PermTypes.Reading) == false) return null;
            var KOD = db.Database.SqlQuery<FiyatOnayGMSelect>(string.Format("[FINSAT6{0}].[wms].[FiyatOnayListSPGMY]", "33")).ToList();
            return PartialView(KOD);
        }
        public ActionResult Fiyat_SM()
        {
            if (CheckPerm("Fiyat Onaylama", PermTypes.Reading) == false) return Redirect("/");
            return View();
        }
        public PartialViewResult Fiyat_SM_List()
        {
            if (CheckPerm("Fiyat Onaylama", PermTypes.Reading) == false) return null;
            var KOD = db.Database.SqlQuery<FiyatOnayGMSelect>(string.Format("[FINSAT6{0}].[wms].[FiyatOnayList]", "33")).ToList();
            return PartialView(KOD);
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
            var KOD = db.Database.SqlQuery<SMSiparisOnaySelect>(string.Format("[FINSAT6{0}].[wms].[SiparisOnayListSM]", "33")).ToList();
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
            var KOD = db.Database.SqlQuery<SMSiparisOnaySelect>(string.Format("[FINSAT6{0}].[wms].[SiparisOnayListSPGMY]", "33")).ToList();
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
            var KOD = db.Database.SqlQuery<SMSiparisOnaySelect>(string.Format("[FINSAT6{0}].[wms].[SiparisOnayListGM]", "33")).ToList();
            return PartialView(KOD);
        }
        public bool Siparis_Onay(string EvrakNo, string Kaydeden, int OnayTip, bool OnaylandiMi)
        {
            if (CheckPerm("Sipariş Onaylama", PermTypes.Writing) == false) return false;
            bool Result = true;
            if (OnayTip == 3 && OnaylandiMi == true)//GMOnay
            { db.Database.ExecuteSqlCommand(string.Format("[FINSAT6{0}].[wms].[SP_SiparisOnay]", "33", EvrakNo, vUser.UserName, 3, 1)); }
            if (OnayTip == 2 && OnaylandiMi == true)//SPGMYOnay
            { db.Database.ExecuteSqlCommand(string.Format("[FINSAT6{0}].[wms].[SP_SiparisOnay]", "33", EvrakNo, vUser.UserName, 2, 1)); }
            if (OnayTip == 1 && OnaylandiMi == true)//SMOnay
            { db.Database.ExecuteSqlCommand(string.Format("[FINSAT6{0}].[wms].[SP_SiparisOnay]", "33", EvrakNo, vUser.UserName, 1, 1)); }
            return Result;
        }
        #endregion
        #region Stok
        public ActionResult Stok()
        {
            if (CheckPerm("Stok Onaylama", PermTypes.Reading) == false) return Redirect("/");
            return View();
        }
        public PartialViewResult StokList(string Durum)
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
            var KOD = db.Database.SqlQuery<StokOnaySelect>(string.Format("[FINSAT6{0}].[wms].[StokOnaySelect] {1}", "33", param)).ToList();
            return PartialView("List", KOD);
        }
        #endregion
        #region Teminat
        public ActionResult Teminat()
        {
            if (CheckPerm("Teminat Onaylama", PermTypes.Reading) == false) return Redirect("/");
            return View();
        }
        public PartialViewResult TeminatList()
        {
            if (CheckPerm("Teminat Onaylama", PermTypes.Reading) == false) return null;
            var KOD = db.Database.SqlQuery<TeminatOnaySelect>(string.Format("[FINSAT6{0}].[wms].[TeminatOnayList]", "33")).ToList();
            return PartialView(KOD);
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

        public PartialViewResult PartialTeminatTanim(string chk)
        {
            if (CheckPerm("Teminat Onaylama", PermTypes.Reading) == false) return null;
            //var TMNT = db.Database.SqlQuery<TeminatSelect>(string.Format("[FINSAT6{0}].[dbo].[TeminatOnaySelect]", "17")).ToList();
            ViewBag.CHK = chk;
            return PartialView("_PartialTeminatTanim");
        }

        public string TeminatSelect(string chk)
        {
            var TMNT = db.Database.SqlQuery<TeminatSelect>(string.Format("[FINSAT6{0}].[dbo].[TeminatOnaySelect] @HesapKodu='{1}'", "17", chk)).ToList();
            var json = new JavaScriptSerializer().Serialize(TMNT);
            return json;
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
            List<SozlesmeOnaySelect> list = db.Database.SqlQuery<SozlesmeOnaySelect>(string.Format("[FINSAT6{0}].[wms].[SP_SozlesmeOnay]", "33")).ToList();
            return PartialView(list);
        }
        [HttpPost]
        public PartialViewResult Sozlesme_GM_Details(string ListeNo)
        {
            if (CheckPerm("Sözleşme Onaylama", PermTypes.Reading) == false) return null;
            var list = db.Database.SqlQuery<BaglantiDetaySelect1>(string.Format("[FINSAT6{0}].[wms].[BaglantiDetaySelect1] '{1}'", "33", ListeNo)).ToList();
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
            List<SozlesmeOnaySelect> list = db.Database.SqlQuery<SozlesmeOnaySelect>(string.Format("[FINSAT6{0}].[wms].[SP_SozlesmeOnaySM]", "33")).ToList();
            return PartialView(list);
        }
        [HttpPost]
        public PartialViewResult Sozlesme_SM_Details(string ListeNo)
        {
            if (CheckPerm("Sözleşme Onaylama", PermTypes.Reading) == false) return null;
            var list = db.Database.SqlQuery<BaglantiDetaySelect1>(string.Format("[FINSAT6{0}].[wms].[BaglantiDetaySelect1] '{1}'", "33", ListeNo)).ToList();
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
            List<SozlesmeOnaySelect> list = db.Database.SqlQuery<SozlesmeOnaySelect>(string.Format("[FINSAT6{0}].[wms].[SP_SozlesmeOnaySPGMY]", "33")).ToList();
            return PartialView(list);
        }
        [HttpPost]
        public PartialViewResult Sozlesme_SPMY_Details(string ListeNo)
        {
            if (CheckPerm("Sözleşme Onaylama", PermTypes.Reading) == false) return null;
            var list = db.Database.SqlQuery<BaglantiDetaySelect1>(string.Format("[FINSAT6{0}].[wms].[BaglantiDetaySelect1] '{1}'", "33", ListeNo)).ToList();
            return PartialView(list);
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
            var KOD = db.Database.SqlQuery<RiskTanim>(string.Format("SELECT *   FROM [FINSAT6{0}].[FINSAT6{0}].[RiskTanim]   where OnayTip = 0", "33")).ToList();//-- and SMOnay = 1 and Durum = 0 eklenecek.
            return PartialView(KOD);
        }
        public ActionResult Risk_GM()
        {
            if (CheckPerm("Risk Onaylama", PermTypes.Reading) == false) return Redirect("/");
            return View();
        }
        public PartialViewResult Risk_GM_List()
        {
            if (CheckPerm("Risk Onaylama", PermTypes.Reading) == false) return null;
            var KOD = db.Database.SqlQuery<RiskTanim>(string.Format("SELECT *   FROM [FINSAT6{0}].[FINSAT6{0}].[RiskTanim]", "33")).ToList();//--where (OnayTip = 3 and SPGMYOnay = 1 and MIGMYOnay = 1 and GMOnay=0) or (OnayTip = 4 and GMOnay = 0 ) and Durum =0
            return PartialView(KOD);
        }
        public ActionResult Risk_SPGMY()
        {
            if (CheckPerm("Risk Onaylama", PermTypes.Reading) == false) return Redirect("/");
            return View();
        }
        public PartialViewResult Risk_SPGMY_List()
        {
            if (CheckPerm("Risk Onaylama", PermTypes.Reading) == false) return null;
            var KOD = db.Database.SqlQuery<RiskTanim>(string.Format("SELECT *   FROM [FINSAT6{0}].[FINSAT6{0}].[RiskTanim] where SPGMYOnay =0", "33")).ToList();//--where (OnayTip = 1 and SPGMYOnay =0) OR (OnayTip = 2 and SPGMYOnay =0) OR (OnayTip = 3 and SPGMYOnay = 0) and Durum = 0
            return PartialView(KOD);
        }
        public ActionResult Risk_MIGMY()
        {
            if (CheckPerm("Risk Onaylama", PermTypes.Reading) == false) return Redirect("/");
            return View();
        }
        public PartialViewResult Risk_MIGMY_List()
        {
            if (CheckPerm("Risk Onaylama", PermTypes.Reading) == false) return null;
            var KOD = db.Database.SqlQuery<RiskTanim>(string.Format("SELECT *   FROM [FINSAT6{0}].[FINSAT6{0}].[RiskTanim] where SPGMYOnay = 1", "33")).ToList();//--where (OnayTip = 2 and SPGMYOnay = 1 and MIGMYOnay = 0) OR (OnayTip = 3 and SPGMYOnay = 1 and MIGMYOnay = 0) and Durum = 0
            return PartialView(KOD);
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

                    RiskTanim rsk = new RiskTanim();

                    rsk.HesapKodu = insertObj["HesapKodu"].ToString();
                    rsk.Unvan = insertObj["Unvan"].ToString();
                    rsk.SahsiCekLimiti = insertObj["SahsiCekLimiti"].ToDecimal();
                    rsk.MusteriCekLimiti = insertObj["MusteriCekLimiti"].ToDecimal();
                    rsk.SMOnay = false;
                    rsk.SMOnaylayan = "";
                    rsk.SPGMYOnay = false;
                    rsk.SPGMYOnaylayan = "";
                    rsk.MIGMYOnay = false;
                    rsk.MIGMYOnaylayan = "";
                    rsk.GMOnay = false;
                    rsk.GMOnaylayan = "";
                    rsk.Durum = false;

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
    }
}