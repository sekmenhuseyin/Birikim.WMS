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
        public string Fiyat_Onay_Koleksiyon(string Data)
        {
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
                
                return "OK";
            }
            catch (Exception ex)
            {
                return "NO";
            }
        }
        public string Fiyat_Onay_Grup(string Data)
        {
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
                return "OK";
            }
            catch (Exception ex)
            {
                return "NO";
            }
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
        public string Fiyat_Onay_Koleksiyon_SM(string Data)
        {
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

                return "OK";
            }
            catch (Exception ex)
            {
                return "NO";
            }
        }
        public string Fiyat_Onay_Grup_SM(string Data)
        {
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
                return "OK";
            }
            catch (Exception ex)
            {
                return "NO";
            }
        }

        public string Fiyat_Red(string Data)
        {
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
                return "OK";
            }
            catch (Exception ex)
            {
                return "NO";
            }
        }
        public string Fiyat_Red_Koleksiyon(string Data)
        {
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
                return "OK";
            }
            catch (Exception ex)
            {
                return "NO";
            }
        }
        public string Fiyat_Red_Grup(string Data)
        {
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
                return "OK";
            }
            catch (Exception ex)
            {
                return "NO";
            }
        }

        public string Fiyat_Red_SM(string Data)
        {
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
                return "OK";
            }
            catch (Exception ex)
            {
                return "NO";
            }
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
            JavaScriptSerializer json = new JavaScriptSerializer();
            json.MaxJsonLength = int.MaxValue;
            var ISSTemp = db.Database.SqlQuery<ISS_Temp>(string.Format("SELECT * FROM [FINSAT6{0}].[FINSAT6{0}].[ISS_Temp] WHERE ListeNo='{1}'", "17", SozlesmeNo)).ToList();
            return json.Serialize(ISSTemp);
        }
        public string FiyatListeleriSelect()
        {
            JavaScriptSerializer json = new JavaScriptSerializer();
            json.MaxJsonLength = int.MaxValue;
            var FYTList = db.Database.SqlQuery<ListeNoSelect>(string.Format("[FINSAT6{0}].[dbo].[FYTSelect2]", "17")).ToList();
            return json.Serialize(FYTList);
        }

        public string SozlesmeCariBilgileri(string ListeNo, string HesapKodu)
        {
            JavaScriptSerializer json = new JavaScriptSerializer();
            json.MaxJsonLength = int.MaxValue;
            var chkBilgi = db.Database.SqlQuery<SozlesmeCariBilgileri>(string.Format("[FINSAT6{0}].[dbo].[SozlesmeCariBilgileri] @HesapKodu='{2}', @ListeNo='{1}'", "17", ListeNo, HesapKodu)).ToList();
            return json.Serialize(chkBilgi);
        }
        public string SozlesmeUrunGrupSelect(int Index)
        {
            var FUGS = db.Database.SqlQuery<UrunGrup>(string.Format("[FINSAT6{0}].[dbo].[STKSelect2] @Index={1}", "17",Index)).ToList();
            var json = new JavaScriptSerializer().Serialize(FUGS);
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