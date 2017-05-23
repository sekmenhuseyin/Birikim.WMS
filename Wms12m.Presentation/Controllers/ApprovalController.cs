using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Wms12m.Entity;
using Wms12m.Entity.Viewmodels;

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

        public string FiyatSatirEkle(string Data)
        {
            if (CheckPerm("FiyatSatirEkle", PermTypes.Writing) == false) return null;
            JObject parameters = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JObject>(Request["Data"]);
            //JValue parameters = JsonConvert.<Newtonsoft.Json.Linq.JValue>(JsonConvert.SerializeObject(Data));
            SqlExper sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, "17");
            try
            {
                Fiyat fyt = new Fiyat();
                fyt.FiyatListNum = parameters["ListeNo"].ToString();

                fyt.HesapKodu = parameters["HesapKodu"].ToString();
                fyt.MalKodu = parameters["UrunKodu"].ToString();
                fyt.SatisFiyat1 = parameters["SatisFiyat"].ToDecimal();
                fyt.SatisFiyat1Birim = parameters["Birim"].ToString();
                fyt.SatisFiyat1BirimInt = parameters["BirimValue"].ToInt32();
                fyt.DovizSatisFiyat1 = parameters["DovizSatisFiyat"].ToDecimal();
                fyt.DovizSF1Birim = parameters["DovizBirim"].ToString();
                fyt.DovizSF1BirimInt = parameters["DovizBirimValue"].ToInt32();
                fyt.DovizCinsi = parameters["Birim"].ToString();
                fyt.Durum = parameters["Durum"].ToShort();
                fyt.ROW_ID = 0;
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
            var KOD = db.Database.SqlQuery<SMSiparisOnaySelect>(string.Format("[FINSAT6{0}].[wms].[SiparisOnayListGM]", "17")).ToList();
            return PartialView(KOD);
        }
        public JsonResult Siparis_Onay(string Data, int OnayTip, bool OnaylandiMi)
        {

            if (CheckPerm("Sipariş Onaylama", PermTypes.Writing) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            Result _Result = new Result(true);
            JArray parameters = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JArray>(Request["Data"]);
            try
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


            }
            catch (Exception)
            {

                _Result.Status = false;
                _Result.Message = "Hata Oluştu.";
            }
            return Json(_Result, JsonRequestBehavior.AllowGet);




        }
        //public JsonResult Siparis_Onay(string EvrakNo, string Kaydeden, int OnayTip, bool OnaylandiMi)
        //{

        //    if (CheckPerm("Sipariş Onaylama", PermTypes.Writing) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
        //    Result _Result = new Result(true);

        //    try
        //    {
        //        if (OnayTip == 3 && OnaylandiMi == true)//GMOnay
        //        { db.Database.ExecuteSqlCommand(string.Format("[FINSAT6{0}].[wms].[SP_SiparisOnay] @EvrakNo = '{1}',@Kullanici = '{2}',@OnayTip={3},@OnaylandiMi={4}", "17", EvrakNo, vUser.UserName, 3, 1)); }
        //        else
        //        if (OnayTip == 2 && OnaylandiMi == true)//SPGMYOnay
        //        { db.Database.ExecuteSqlCommand(string.Format("[FINSAT6{0}].[wms].[SP_SiparisOnay] @EvrakNo = '{1}',@Kullanici = '{2}',@OnayTip={3},@OnaylandiMi={4}", "17", EvrakNo, vUser.UserName, 2, 1)); }
        //        else
        //        if (OnayTip == 1 && OnaylandiMi == true)//SMOnay
        //        { db.Database.ExecuteSqlCommand(string.Format("[FINSAT6{0}].[wms].[SP_SiparisOnay] @EvrakNo = '{1}',@Kullanici = '{2}',@OnayTip={3},@OnaylandiMi={4}", "17", EvrakNo, vUser.UserName, 1, 1)); }
        //        else
        //        /***********************************/
        //        if (OnayTip == 3 && OnaylandiMi == false)//GMRet
        //        { db.Database.ExecuteSqlCommand(string.Format("[FINSAT6{0}].[wms].[SP_SiparisOnay] @EvrakNo = '{1}',@Kullanici = '{2}',@OnayTip={3},@OnaylandiMi={4}", "17", EvrakNo, vUser.UserName, 3, 0)); }
        //        else
        //        if (OnayTip == 2 && OnaylandiMi == false)//SPGMYRet
        //        { db.Database.ExecuteSqlCommand(string.Format("[FINSAT6{0}].[wms].[SP_SiparisOnay] @EvrakNo = '{1}',@Kullanici = '{2}',@OnayTip={3},@OnaylandiMi={4}", "17", EvrakNo, vUser.UserName, 2, 0)); }
        //        else
        //        if (OnayTip == 1 && OnaylandiMi == false)//SMRet
        //        { db.Database.ExecuteSqlCommand(string.Format("[FINSAT6{0}].[wms].[SP_SiparisOnay] @EvrakNo = '{1}',@Kullanici = '{2}',@OnayTip={3},@OnaylandiMi={4}", "17", EvrakNo, vUser.UserName, 1, 0)); }
        //    }
        //    catch (Exception)
        //    {

        //        _Result.Status = false;
        //        _Result.Message = "Hata Oluştu.";
        //    }




        //    return Json(_Result, JsonRequestBehavior.AllowGet);
        //}
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

        public ActionResult SozlesmeTanim()
        {
            if (CheckPerm("Sözleşme Onaylama", PermTypes.Reading) == false) return Redirect("/");
            ViewBag.SRNO = "SOZ " + db.Database.SqlQuery<int>(string.Format("[FINSAT6{0}].[dbo].[SozlesmeSiraNoSelect]", "17")).FirstOrDefault();
            return View();
        }

        public string SozlesmeListesi(int tip )
        {
            var sozlesmeler = new SozlesmeListesi();
            if (tip == 0)
            {
                sozlesmeler = db.Database.SqlQuery<SozlesmeListesi>(string.Format("[FINSAT6{0}].[dbo].[SozlesmeOnaylanmisList]", "17")).FirstOrDefault();
            }
            else
            {
                sozlesmeler = db.Database.SqlQuery<SozlesmeListesi>(string.Format("[FINSAT6{0}].[dbo].[SozlesmeOnaylanmamisList]", "17")).FirstOrDefault();
            }
            var json = new JavaScriptSerializer().Serialize(sozlesmeler);
            return json;
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
            var KOD = db.Database.SqlQuery<RiskTanim>(string.Format("SELECT *   FROM [FINSAT6{0}].[FINSAT6{0}].[RiskTanim]", "17")).ToList();//--where (OnayTip = 3 and SPGMYOnay = 1 and MIGMYOnay = 1 and GMOnay=0) or (OnayTip = 4 and GMOnay = 0 ) and Durum =0
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