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

namespace Wms12m.Presentation.Areas.Approvals.Controllers
{
    public class ContractController : RootController
    {
        #region GM
        public ActionResult GM()
        {
            if (CheckPerm("Sözleşme Onaylama", PermTypes.Reading) == false) return Redirect("/");
            return View();
        }
        public PartialViewResult GM_List()
        {
            if (CheckPerm("Sözleşme Onaylama", PermTypes.Reading) == false) return null;
            return PartialView();
        }
        public string OnayCekGM()
        {
            if (CheckPerm("Sözleşme Onaylama", PermTypes.Reading) == false) return null;
            List<SozlesmeOnaySelect> RT;
            try
            {
                RT = db.Database.SqlQuery<SozlesmeOnaySelect>(string.Format("[FINSAT6{0}].[wms].[SP_SozlesmeOnay]", "17")).ToList();
            }
            catch (Exception)
            {
                RT = new List<SozlesmeOnaySelect>();
            }
            var json = new JavaScriptSerializer().Serialize(RT);
            return json;
        }
        public JsonResult Onay_GM(string Data)
        {
            Result _Result = new Result(true);
            if (CheckPerm("Sözleşme Onaylama", PermTypes.Writing) == false) return null;
            JArray parameters = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JArray>(Request["Data"]);
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
                            sqlexper.Insert(insrt);

                        }
                    }
                    var sonuc = sqlexper.AcceptChanges();
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
        public JsonResult Sil_GM(string Data)
        {
            Result _Result = new Result(true);
            if (CheckPerm("Sözleşme Onaylama", PermTypes.Writing) == false) return null;
            JArray parameters = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JArray>(Request["Data"]);
            SqlExper sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, "17");
            try
            {
                foreach (JObject insertObj in parameters)
                {
                    DateTime date = DateTime.Now;
                    var shortDate = date.ToString("yyyy-MM-dd");
                    var sonuc = sqlexper.AcceptChanges();
                    db.Database.ExecuteSqlCommand(string.Format("DELETE FROM [FINSAT6{0}].[FINSAT6{0}].[ISS_Temp] WHERE  ListeNo = '{1}'", "17", insertObj["ListeNo"].ToString()));

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
        [HttpPost]
        public PartialViewResult GM_Details(string ListeNo)
        {
            if (CheckPerm("Sözleşme Onaylama", PermTypes.Reading) == false) return null;
            var list = db.Database.SqlQuery<BaglantiDetaySelect1>(string.Format("[FINSAT6{0}].[wms].[BaglantiDetaySelect1] '{1}'", "17", ListeNo)).ToList();
            return PartialView(list);
        }
        #endregion
        #region SM
        public ActionResult SM()
        {
            if (CheckPerm("Sözleşme Onaylama", PermTypes.Reading) == false) return Redirect("/");
            return View();
        }
        public PartialViewResult SM_List()
        {
            if (CheckPerm("Sözleşme Onaylama", PermTypes.Reading) == false) return null;
            return PartialView();
        }
        public string OnayCekSM()
        {
            if (CheckPerm("Fiyat Onaylama", PermTypes.Reading) == false) return null;
            List<SozlesmeOnaySelect> RT;
            try
            {
                RT = db.Database.SqlQuery<SozlesmeOnaySelect>(string.Format("[FINSAT6{0}].[wms].[SP_SozlesmeOnaySM]", "17")).ToList();
            }
            catch (Exception)
            {
                RT = new List<SozlesmeOnaySelect>();
            }
            var json = new JavaScriptSerializer().Serialize(RT);
            return json;
        }
        [HttpPost]
        public PartialViewResult SM_Details(string ListeNo)
        {
            if (CheckPerm("Sözleşme Onaylama", PermTypes.Reading) == false) return null;
            var list = db.Database.SqlQuery<BaglantiDetaySelect1>(string.Format("[FINSAT6{0}].[wms].[BaglantiDetaySelect1] '{1}'", "17", ListeNo)).ToList();
            return PartialView(list);
        }
        public JsonResult Onay_SM(string Data)
        {
            Result _Result = new Result(true);
            if (CheckPerm("Sözleşme Onaylama", PermTypes.Writing) == false) return null;
            JArray parameters = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JArray>(Request["Data"]);
            SqlExper sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, "17");
            try
            {
                foreach (JObject insertObj in parameters)
                {
                    db.Database.ExecuteSqlCommand(string.Format("[FINSAT6{0}].[dbo].[SozlesmeSatisMuduruOnay] @SozlesmeNo = '{1}',@Kullanici = '{2}'", "17", insertObj["ListeNo"].ToString(), vUser.UserName));
                    var list = db.Database.SqlQuery<ISS_Temp>(string.Format("SELECT *  FROM [FINSAT6{0}].[FINSAT6{0}].[ISS_Temp] WHERE ListeNo='{1}'", "17", insertObj["ListeNo"].ToString())).ToList();
                    foreach (ISS_Temp lst in list)
                    {
                        if (lst.OnayTip == 0)
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
                            sqlexper.Insert(insrt);

                        }
                    }
                    var sonuc = sqlexper.AcceptChanges();
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
        public JsonResult Sil_SM(string Data)
        {
            Result _Result = new Result(true);
            if (CheckPerm("Sözleşme Onaylama", PermTypes.Writing) == false) return null;
            JArray parameters = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JArray>(Request["Data"]);
            SqlExper sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, "17");
            try
            {
                foreach (JObject insertObj in parameters)
                {
                    DateTime date = DateTime.Now;
                    var shortDate = date.ToString("yyyy-MM-dd");
                    var sonuc = sqlexper.AcceptChanges();
                    db.Database.ExecuteSqlCommand(string.Format("DELETE FROM [FINSAT6{0}].[FINSAT6{0}].[ISS_Temp] WHERE  ListeNo = '{1}'", "17", insertObj["ListeNo"].ToString()));

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
        #endregion
        #region SPGMY
        public ActionResult SPGMY()
        {
            if (CheckPerm("Sözleşme Onaylama", PermTypes.Reading) == false) return Redirect("/");
            return View();
        }
        public PartialViewResult SPGMY_List()
        {
            if (CheckPerm("Sözleşme Onaylama", PermTypes.Reading) == false) return null;
            return PartialView();
        }
        public string OnayCekSPGMY()
        {
            if (CheckPerm("Sözleşme Onaylama", PermTypes.Reading) == false) return null;
            List<SozlesmeOnaySelect> RT;
            try
            {
                RT = db.Database.SqlQuery<SozlesmeOnaySelect>(string.Format("[FINSAT6{0}].[wms].[SP_SozlesmeOnaySPGMY]", "17")).ToList();
            }
            catch (Exception)
            {
                RT = new List<SozlesmeOnaySelect>();
            }
            var json = new JavaScriptSerializer().Serialize(RT);
            return json;
        }
        [HttpPost]
        public PartialViewResult SPGMY_Details(string ListeNo)
        {
            if (CheckPerm("Sözleşme Onaylama", PermTypes.Reading) == false) return null;
            var list = db.Database.SqlQuery<BaglantiDetaySelect1>(string.Format("[FINSAT6{0}].[wms].[BaglantiDetaySelect1] '{1}'", "17", ListeNo)).ToList();
            return PartialView(list);
        }
        public JsonResult Onay_SPGMY(string Data)
        {
            Result _Result = new Result(true);
            if (CheckPerm("Sözleşme Onaylama", PermTypes.Writing) == false) return null;
            JArray parameters = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JArray>(Request["Data"]);
            SqlExper sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, "17");
            try
            {

                foreach (JObject insertObj in parameters)
                {
                    db.Database.ExecuteSqlCommand(string.Format("[FINSAT6{0}].[dbo].[SozlesmeFinansmanMuduruOnay] @SozlesmeNo = '{1}',@Kullanici = '{2}'", "17", insertObj["ListeNo"].ToString(), vUser.UserName));
                    var list = db.Database.SqlQuery<ISS_Temp>(string.Format("SELECT *  FROM [FINSAT6{0}].[FINSAT6{0}].[ISS_Temp] WHERE ListeNo='{1}'", "17", insertObj["ListeNo"].ToString())).ToList();
                    foreach (ISS_Temp lst in list)
                    {
                        if (lst.OnayTip == 1)
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
                            sqlexper.Insert(insrt);
                        }
                    }
                    var sonuc = sqlexper.AcceptChanges();
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
        public JsonResult Sil_SPGMY(string Data)
        {
            Result _Result = new Result(true);
            if (CheckPerm("Sözleşme Onaylama", PermTypes.Writing) == false) return null;
            JArray parameters = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JArray>(Request["Data"]);
            SqlExper sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, "17");
            try
            {
                foreach (JObject insertObj in parameters)
                {
                    DateTime date = DateTime.Now;
                    var shortDate = date.ToString("yyyy-MM-dd");
                    var sonuc = sqlexper.AcceptChanges();
                    db.Database.ExecuteSqlCommand(string.Format("DELETE FROM [FINSAT6{0}].[FINSAT6{0}].[ISS_Temp] WHERE  ListeNo = '{1}'", "17", insertObj["ListeNo"].ToString()));

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
        #endregion

        public ActionResult Tanim()
        {
            if (CheckPerm("Sözleşme Onaylama", PermTypes.Reading) == false) return Redirect("/");
            ViewBag.SRNO = "SOZ " + db.Database.SqlQuery<int>(string.Format("[FINSAT6{0}].[dbo].[SozlesmeSiraNoSelect]", "17")).FirstOrDefault();
            return View();
        }

        public PartialViewResult Tanim_List(string listeNo, string satir)
        {
            if (CheckPerm("Sözleşme Onaylama", PermTypes.Reading) == false) return null;
            ViewBag.ListeNo = listeNo;
            ViewBag.Satir = satir;
            return PartialView("Tanim_List");
        }

        public string TanimListesiSelect(string listeNo)
        {
            var STL = new List<BaglantiDetaySelect1>();
            if (listeNo != "#12MConsulting#")
            {
                STL = db.Database.SqlQuery<BaglantiDetaySelect1>(string.Format("[FINSAT6{0}].[dbo].[BaglantiDetaySelect1] @ListeNo='{1}'", "17", listeNo)).ToList();
            }
            var json = new JavaScriptSerializer().Serialize(STL);
            return json;
        }

        public string List(int tip)
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

        public string CariBilgileri(string ListeNo, string HesapKodu)
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
        public string UrunGrupSelect(int Index)
        {
            var FUGS = db.Database.SqlQuery<UrunGrup>(string.Format("[FINSAT6{0}].[dbo].[STKSelect2] @Index={1}", "17", Index)).ToList();
            var json = new JavaScriptSerializer().Serialize(FUGS);
            return json;
        }

        public int ListeNoKontrol(string ListeNo)
        {
            var VarMi = db.Database.SqlQuery<int>(string.Format("SELECT Count(ListeNo) FROM [FINSAT6{0}].[FINSAT6{0}].[ISS_Temp] WHERE ListeNo='{1}'", "17", ListeNo)).FirstOrDefault();
            return VarMi;
        }

        public JsonResult ISS_TempUpdate_AktifPasif(string SozlesmeNo, bool AktifPasif)
        {
            Result _Result = new Result(true);
            if (CheckPerm("Fiyat Onaylama", PermTypes.Writing) == false) return null;
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

        public string YeniSatirKayit(string Data)
        {
            if (CheckPerm("FiyatSatirEkle", PermTypes.Writing) == false) return null;
            JObject parameters = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JObject>(Request["Data"]);
            SqlExper sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, "17");
            return "";
        }

        public JsonResult Sil(string SozlesmeNo)
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

        public JsonResult Guncelle(string SozlesmeNo, int BasTarih, short MusUygSekli, decimal YeniBaglantiTutari, int YeniBitisTarihi)
        {
            if (CheckPerm("FiyatSatirEkle", PermTypes.Writing) == false) return null;
            Result _Result = new Result(true, "İşlem Başarılı.");
            SqlExper sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, "17");
            try
            {
                bool filtreKagitVarmi = false;
                var list = db.Database.SqlQuery<ISS_Temp>(string.Format("SELECT * FROM [FINSAT6{0}].[FINSAT6{0}].[ISS_Temp] WHERE ListeNo='{1}' AND BasTarih = {2} AND MusUygSekli={3}", "17", SozlesmeNo, BasTarih, MusUygSekli)).ToList();
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
                if (sonuc.Status == true)
                {
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
        }
    }
}