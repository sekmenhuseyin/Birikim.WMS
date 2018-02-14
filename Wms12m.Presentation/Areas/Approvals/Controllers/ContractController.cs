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
    public class ContractController : RootController
    {
        public ActionResult GM()
        {
            if (CheckPerm(Perms.SözleşmeOnaylama, PermTypes.Reading) == false) return Redirect("/");
            ViewBag.Tip = "GM";
            return View("Index");
        }

        public ActionResult SM()
        {
            if (CheckPerm(Perms.SözleşmeOnaylama, PermTypes.Reading) == false) return Redirect("/");
            ViewBag.Tip = "SM";
            return View("Index");
        }

        public ActionResult SPGMY()
        {
            if (CheckPerm(Perms.SözleşmeOnaylama, PermTypes.Reading) == false) return Redirect("/");
            ViewBag.Tip = "SPGMY";
            return View("Index");
        }

        public JsonResult List(string Tip)
        {
            var list = db.Database.SqlQuery<SozlesmeOnaySelect>(string.Format("[FINSAT6{0}].[wms].[SP_SozlesmeOnay{1}]", vUser.SirketKodu, Tip == "GM" ? "" : Tip)).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult Details(string ListeNo)
        {
            var list = db.Database.SqlQuery<BaglantiDetaySelect>(string.Format("[FINSAT6{0}].[wms].[BaglantiDetaySelect] '{1}'", vUser.SirketKodu, ListeNo)).ToList();
            return PartialView("Details", list);
        }

        public JsonResult Onay_GM(string Data)
        {
            var _Result = new Result(false, "Yetkiniz yok");
            if (CheckPerm(Perms.SözleşmeOnaylama, PermTypes.Writing) == false) return Json(_Result, JsonRequestBehavior.AllowGet);
            var parameters = JsonConvert.DeserializeObject<JArray>(Request["Data"]);
            var sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, vUser.SirketKodu);
            try
            {
                foreach (JObject insertObj in parameters)
                {
                    db.Database.ExecuteSqlCommand(string.Format("[FINSAT6{0}].[wms].[SozlesmeGenelMudurOnay] @SozlesmeNo = '{1}',@Kullanici = '{2}'", vUser.SirketKodu, insertObj["ListeNo"].ToString(), vUser.UserName));

                    var list = db.Database.SqlQuery<ISS_Temp>(string.Format("SELECT *  FROM [FINSAT6{0}].[FINSAT6{0}].[ISS_Temp] WHERE ListeNo='{1}'", vUser.SirketKodu, insertObj["ListeNo"].ToString())).ToList();

                    foreach (ISS_Temp lst in list)
                    {
                        if (lst.OnayTip == 2)
                        {
                            db.Database.ExecuteSqlCommand(string.Format("UPDATE [FINSAT6{0}].[FINSAT6{0}].[ISS_Temp] SET Kod12=0, Kod9='' WHERE ListeNo='{1}'", vUser.SirketKodu, insertObj["ListeNo"].ToString()));
                            var insrt = new ISS()
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
            catch (Exception)
            {
                _Result.Status = false;
                _Result.Message = "Hata Oluştu. ";
            }

            return Json(_Result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Onay_SM(string Data)
        {
            var _Result = new Result(true);
            if (CheckPerm(Perms.SözleşmeOnaylama, PermTypes.Writing) == false) return null;
            var parameters = JsonConvert.DeserializeObject<JArray>(Request["Data"]);
            var sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, vUser.SirketKodu);
            try
            {
                foreach (JObject insertObj in parameters)
                {
                    var s = string.Format("[FINSAT6{0}].[wms].[SozlesmeSatisMuduruOnay] @SozlesmeNo = '{1}',@Kullanici = '{2}'", vUser.SirketKodu, insertObj["ListeNo"].ToString(), vUser.UserName);
                    var xx = db.Database.ExecuteSqlCommand(s);
                    var list = db.Database.SqlQuery<ISS_Temp>(string.Format("SELECT *  FROM [FINSAT6{0}].[FINSAT6{0}].[ISS_Temp] WHERE ListeNo='{1}'", vUser.SirketKodu, insertObj["ListeNo"].ToString())).ToList();
                    foreach (ISS_Temp lst in list)
                    {
                        if (lst.OnayTip == 0)
                        {
                            var insrt = new ISS()
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
            catch (Exception)
            {
                _Result.Status = false;
                _Result.Message = "Hata Oluştu. ";
            }

            return Json(_Result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Onay_SPGMY(string Data)
        {
            var _Result = new Result(true);
            if (CheckPerm(Perms.SözleşmeOnaylama, PermTypes.Writing) == false) return null;
            var parameters = JsonConvert.DeserializeObject<JArray>(Request["Data"]);
            var sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, vUser.SirketKodu);
            try
            {
                foreach (JObject insertObj in parameters)
                {
                    db.Database.ExecuteSqlCommand(string.Format("[FINSAT6{0}].[wms].[SozlesmeFinansmanMuduruOnay] @SozlesmeNo = '{1}',@Kullanici = '{2}'", vUser.SirketKodu, insertObj["ListeNo"].ToString(), vUser.UserName));
                    var list = db.Database.SqlQuery<ISS_Temp>(string.Format("SELECT *  FROM [FINSAT6{0}].[FINSAT6{0}].[ISS_Temp] WHERE ListeNo='{1}'", vUser.SirketKodu, insertObj["ListeNo"].ToString())).ToList();
                    foreach (ISS_Temp lst in list)
                    {
                        if (lst.OnayTip == 1)
                        {
                            var insrt = new ISS()
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
            catch (Exception)
            {
                _Result.Status = false;
                _Result.Message = "Hata Oluştu. ";
            }

            return Json(_Result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Red(string Data)
        {
            var _Result = new Result(false, "Yetkiniz yok");
            if (CheckPerm(Perms.SözleşmeOnaylama, PermTypes.Writing) == false) return Json(_Result, JsonRequestBehavior.AllowGet);
            var parameters = JsonConvert.DeserializeObject<JArray>(Request["Data"]);
            try
            {
                foreach (JObject insertObj in parameters)
                {
                    db.Database.ExecuteSqlCommand(string.Format("DELETE FROM [FINSAT6{0}].[FINSAT6{0}].[ISS_Temp] WHERE  ListeNo = '{1}'", vUser.SirketKodu, insertObj["ListeNo"].ToString()));
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

        #region Tanim

        public ActionResult Tanim()
        {
            if (CheckPerm(Perms.SözleşmeTanim, PermTypes.Reading) == false) return Redirect("/");
            ViewBag.SRNO = "SOZ " + db.Database.SqlQuery<int>(string.Format("[FINSAT6{0}].[wms].[SozlesmeSiraNoSelect]", vUser.SirketKodu)).FirstOrDefault();
            return View();
        }

        public PartialViewResult Tanim_List(string listeNo, string satir)
        {
            if (CheckPerm(Perms.SözleşmeTanim, PermTypes.Reading) == false) return null;
            var sat = "";
            if (satir != null)
            {
                sat = "<tbody>";
                var parameters = JsonConvert.DeserializeObject<JArray>(satir);
                foreach (string insertObj in parameters)
                    sat += insertObj;

                sat += "</tbody>";
            }

            ViewBag.ListeNo = JsonConvert.SerializeObject(listeNo);
            ViewBag.Satir = sat;
            return PartialView("Tanim_List");
        }

        public string TanimListesiSelect(string listeNo)
        {
            var STL = new List<BaglantiDetaySelect>();
            if (listeNo != "#12MConsulting#")
            {
                STL = db.Database.SqlQuery<BaglantiDetaySelect>(string.Format("[FINSAT6{0}].[wms].[BaglantiDetaySelect] @ListeNo='{1}'", vUser.SirketKodu, listeNo)).ToList();
            }

            var json = new JavaScriptSerializer().Serialize(STL);
            return json;
        }

        public string TanimList(int tip)
        {
            var sozlesmeler = new List<SozlesmeListesi>();
            if (tip == 0)
            {
                sozlesmeler = db.Database.SqlQuery<SozlesmeListesi>(string.Format("[FINSAT6{0}].[wms].[SozlesmeOnaylanmisList]", vUser.SirketKodu)).ToList();
            }
            else
            {
                sozlesmeler = db.Database.SqlQuery<SozlesmeListesi>(string.Format("[FINSAT6{0}].[wms].[SozlesmeOnaylanmamisList]", vUser.SirketKodu)).ToList();
            }

            var json = new JavaScriptSerializer().Serialize(sozlesmeler);
            return json;
        }

        public string ISSTempList(string SozlesmeNo)
        {
            var json = new JavaScriptSerializer()
            {
                MaxJsonLength = int.MaxValue
            };
            var ISSTemp = db.Database.SqlQuery<ISS_Temp>(string.Format("SELECT * FROM [FINSAT6{0}].[FINSAT6{0}].[ISS_Temp] WHERE ListeNo='{1}'", vUser.SirketKodu, SozlesmeNo)).ToList();
            return json.Serialize(ISSTemp);
        }

        public string FiyatListeleriSelect()
        {
            var json = new JavaScriptSerializer()
            {
                MaxJsonLength = int.MaxValue
            };
            var FYTList = db.Database.SqlQuery<ListeNoSelect>(string.Format("[FINSAT6{0}].[wms].[FYTSelect2]", vUser.SirketKodu)).ToList();
            return json.Serialize(FYTList);
        }

        public string CariBilgileri(string ListeNo, string HesapKodu)
        {
            var json = new JavaScriptSerializer()
            {
                MaxJsonLength = int.MaxValue
            };
            List<SozlesmeCariBilgileri> chkBilgi;
            try
            {
                chkBilgi = db.Database.SqlQuery<SozlesmeCariBilgileri>(string.Format("[FINSAT6{0}].[wms].[SozlesmeCariBilgileri] @HesapKodu='{2}', @ListeNo='{1}'", vUser.SirketKodu, ListeNo, HesapKodu)).ToList();
            }
            catch (Exception)
            {
                chkBilgi = new List<Entity.SozlesmeCariBilgileri>();
            }

            return json.Serialize(chkBilgi);
        }

        public string UrunGrupSelect(int Index)
        {
            var json = new JavaScriptSerializer()
            {
                MaxJsonLength = int.MaxValue
            };
            var FUGS = db.Database.SqlQuery<UrunGrup>(string.Format("[FINSAT6{0}].[wms].[STKSelect2] @Index={1}", vUser.SirketKodu, Index)).ToList();
            return json.Serialize(FUGS);
        }

        public int ListeNoKontrol(string ListeNo)
        {
            var VarMi = db.Database.SqlQuery<int>(string.Format("SELECT Count(ListeNo) FROM [FINSAT6{0}].[FINSAT6{0}].[ISS_Temp] WHERE ListeNo='{1}'", vUser.SirketKodu, ListeNo)).FirstOrDefault();
            return VarMi;
        }

        public JsonResult ISS_TempUpdate_AktifPasif(string SozlesmeNo, bool AktifPasif)
        {
            var _Result = new Result(true);
            if (CheckPerm(Perms.SözleşmeTanim, PermTypes.Writing) == false) return null;
            var sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, vUser.SirketKodu);
            try
            {
                var ISSS = db.Database.SqlQuery<ISS_Temp>(string.Format("SELECT * FROM [FINSAT6{0}].[FINSAT6{0}].[ISS_Temp]", vUser.SirketKodu)).ToList();
                if (!AktifPasif)   ///Pasif In Aktif olunca
                {
                    db.Database.ExecuteSqlCommand(string.Format("UPDATE [FINSAT6{0}].[FINSAT6{0}].[ISS_Temp] SET AktifPasif = 0  where ListeNo = '{1}'", vUser.SirketKodu, SozlesmeNo));
                    db.Database.ExecuteSqlCommand(string.Format("DELETE FROM [FINSAT6{0}].[FINSAT6{0}].[ISS] where ListeNo = '{1}'", vUser.SirketKodu, SozlesmeNo));
                }
                else
                {
                    db.Database.ExecuteSqlCommand(string.Format("UPDATE [FINSAT6{0}].[FINSAT6{0}].[ISS_Temp] SET AktifPasif = 1  where ListeNo = '{1}'", vUser.SirketKodu, SozlesmeNo));
                    foreach (ISS_Temp lst in ISSS)
                    {
                        var insrt = new ISS()
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
                        var VarMi = db.Database.SqlQuery<int>(string.Format("SELECT Count(*) FROM [FINSAT6{0}].[FINSAT6{0}].[ISS] WHERE ListeNo='{1}' AND BasTarih={2} AND MusUygSekli='{3}' AND SiraNo={4}", vUser.SirketKodu, insrt.ListeNo, insrt.BasTarih, insrt.MusUygSekli, insrt.SiraNo)).FirstOrDefault();

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
            catch (Exception)
            {
                _Result.Status = false;
                _Result.Message = "Hata Oluştu. ";
            }

            return Json(_Result, JsonRequestBehavior.AllowGet);
        }

        public string YeniSatirKayit(string Data)
        {
            if (CheckPerm(Perms.SözleşmeTanim, PermTypes.Writing) == false) return null;
            var parameters = JsonConvert.DeserializeObject<JArray>(Request["Data"]);
            var sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, vUser.SirketKodu);
            var filtreKagitVarmi = false;

            var SiraNo = 0;
            bool? AktifPasif = null;
            List<ISS_Temp> listiss = new List<ISS_Temp>();
            foreach (JObject bds in parameters)
            {
                var isstmp = new ISS_Temp();
                AktifPasif = bds["AktifPasif"].ToBool();
                var ListeAdi = bds["Unvan"].ToString().Length >= 10 ? bds["Unvan"].ToString().Substring(0, 10) : bds["Unvan"].ToString();
                if (bds["VadeTarihInt"].ToInt32() == 0)
                    isstmp.ValorGun = bds["ValorGun"].ToInt32();
                else if (bds["ValorGun"].ToInt32() == 0)
                    isstmp.VadeTarihi = bds["VadeTarihInt"].ToInt32();

                isstmp.ListeNo = bds["ListeNo"].ToString();
                isstmp.ListeAdi = bds["Unvan"].ToString().Length >= 30 ? bds["Unvan"].ToString().Substring(0, 30) : bds["Unvan"].ToString();
                isstmp.BasTarih = bds["BasTarih"].ToString() == "" ? (int)DateTime.Now.ToOADate() : bds["BasTarihInt"].ToInt32();
                isstmp.BasSaat = fn.ToOATime();
                // saati araştır
                isstmp.BitTarih = bds["BitTarih"].ToString() == "" ? (int)DateTime.Now.ToOADate() : bds["BitTarihInt"].ToInt32();
                isstmp.BitSaat = fn.ToOATime();
                isstmp.MusUygSekli = 1;
                isstmp.MusKodGrup = 0;
                isstmp.MusteriKod = bds["HesapKodu"] == null ? "" : bds["HesapKodu"].ToString();
                isstmp.MalUygSekli = 1;
                if (bds["UrunGrubu"].ToString() == "Mal Kodu")
                    isstmp.MalKodGrup = 0;
                else if (bds["UrunGrubu"].ToString() == "Grup Kodu")
                    isstmp.MalKodGrup = 1;
                else if (bds["UrunGrubu"].ToString() == "Tip Kodu")
                    isstmp.MalKodGrup = 2;
                else if (bds["UrunGrubu"].ToString() == "Özel Kod")
                    isstmp.MalKodGrup = 3;
                else if (bds["UrunGrubu"].ToString() == "Kod1")
                    isstmp.MalKodGrup = 4;
                else if (bds["UrunGrubu"].ToString() == "Kod2")
                    isstmp.MalKodGrup = 5;
                else if (bds["UrunGrubu"].ToString() == "Kod3")
                    isstmp.MalKodGrup = 6;
                else if (bds["UrunGrubu"].ToString() == "Kod4")
                    isstmp.MalKodGrup = 7;

                isstmp.MalKod = bds["UrunKodu"].ToString();
                isstmp.SiraNo = (short)SiraNo;
                SiraNo++;
                isstmp.Oran = 0;
                isstmp.Oran1 = Convert.ToSingle(bds["Iskonto1"].ToString());
                isstmp.Oran2 = Convert.ToSingle(bds["Iskonto2"].ToString());
                isstmp.Oran3 = Convert.ToSingle(bds["Iskonto3"].ToString());
                isstmp.Oran4 = Convert.ToSingle(bds["Iskonto4"].ToString());
                isstmp.Oran5 = Convert.ToSingle(bds["Iskonto5"].ToString());
                isstmp.MikAralik1 = 0;
                isstmp.MikYuzde1 = 0;
                isstmp.MikAralik2 = 0;
                isstmp.MikYuzde2 = 0;
                isstmp.MikAralik3 = 0;
                isstmp.MikYuzde3 = 0;
                isstmp.MikAralik4 = 0;
                isstmp.MikYuzde4 = 0;
                isstmp.MikAralik5 = 0;
                isstmp.MikYuzde5 = 0;
                isstmp.MikAralik6 = 0;
                isstmp.MikYuzde6 = 0;
                isstmp.MikAralik7 = 0;
                isstmp.MikYuzde7 = 0;
                isstmp.MikAralik8 = 0;
                isstmp.MikYuzde8 = 0;
                isstmp.TutarAralik1 = 0;
                isstmp.TutarYuzde1 = 0;
                isstmp.TutarAralik2 = 0;
                isstmp.TutarYuzde2 = 0;
                isstmp.TutarAralik3 = 0;
                isstmp.TutarYuzde3 = 0;
                isstmp.TutarAralik4 = 0;
                isstmp.TutarYuzde4 = 0;
                isstmp.TutarAralik5 = 0;
                isstmp.TutarYuzde5 = 0;
                isstmp.TutarAralik6 = 0;
                isstmp.TutarYuzde6 = 0;
                isstmp.TutarAralik7 = 0;
                isstmp.TutarYuzde7 = 0;
                isstmp.TutarAralik8 = 0;
                isstmp.TutarYuzde8 = 0;
                isstmp.OdemeAralik1 = 0;
                isstmp.OdemeYuzde1 = 0;
                isstmp.OdemeAralik2 = 0;
                isstmp.OdemeYuzde2 = 0;
                isstmp.OdemeAralik3 = 0;
                isstmp.OdemeYuzde3 = 0;
                isstmp.OdemeAralik4 = 0;
                isstmp.OdemeYuzde4 = 0;
                isstmp.OdemeAralik5 = 0;
                isstmp.OdemeYuzde5 = 0;
                isstmp.OdemeAralik6 = 0;
                isstmp.OdemeYuzde6 = 0;
                isstmp.OdemeAralik7 = 0;
                isstmp.OdemeYuzde7 = 0;
                isstmp.OdemeAralik8 = 0;
                isstmp.OdemeYuzde8 = 0;
                isstmp.KayitTuru = -1;
                isstmp.GuvenlikKod = "12";
                isstmp.Kaydeden = vUser.UserName.ToString();
                isstmp.KayitTarih = (int)DateTime.Now.ToOADate();
                isstmp.KayitSaat = fn.ToOATime();
                isstmp.KayitKaynak = 136;
                isstmp.KayitSurum = "8.00.001";
                isstmp.Degistiren = vUser.UserName.ToString();
                isstmp.DegisTarih = (int)DateTime.Now.ToOADate();
                isstmp.DegisSaat = fn.ToOATime();
                isstmp.DegisKaynak = 136;
                isstmp.DegisSurum = "8.00.001";
                isstmp.CheckSum = 12;
                isstmp.Aciklama = bds["Not"].ToString();
                isstmp.Kod1 = bds["BaglantiTipi"].ToString();
                isstmp.Kod2 = bds["SiraNo"].ToString();
                isstmp.Kod3 = "";
                isstmp.Kod4 = bds["Kalite"].ToString();
                isstmp.Kod5 = "";
                isstmp.Kod6 = "";
                isstmp.Kod7 = "";
                isstmp.Kod8 = "";
                isstmp.Kod9 = "";
                isstmp.Kod10 = bds["Kod10"].ToString();
                isstmp.Kod11 = Convert.ToDecimal(bds["BaglantiTutari"].ToString());
                isstmp.Kod12 = 0;
                isstmp.DevirTarih = 0;
                isstmp.DevirTutar = 0;

                /// Sözleşmeler İçin KağıtFiltre Kontrolü
                if ((isstmp.MalKodGrup == 0 && isstmp.MalKod.StartsWith("2800")) ||
                   (isstmp.MalKodGrup == 1 && isstmp.MalKod == "FKAĞIT") ||
                    (isstmp.MalKodGrup == 0 && (isstmp.MalKod == "M001001000017051" || isstmp.MalKod == "M001001000022051")))
                {
                    filtreKagitVarmi = true;
                }

                isstmp.OnayTip = -1;
                isstmp.SatisMuduruOnay = false;
                isstmp.FinansmanMuduruOnay = false;
                isstmp.GenelMudurOnay = false;
                isstmp.OnaylayanSatisMuduru = "";
                isstmp.OnaylayanFinansmanMuduru = "";
                isstmp.OnaylayanGenelMudur = "";

                isstmp.CekTutari = Convert.ToDecimal(bds["CekTutari"].ToString());
                isstmp.CekOrtalamaVadeTarih = bds["CekOrtVadeTarihi"].ToString() == "" ? DateTime.Now : bds["CekOrtVadeTarihi"].ToDatetime();
                isstmp.NakitTutari = Convert.ToDecimal(bds["NakitTutar"].ToString());

                isstmp.BaglantiParaCinsi = bds["BaglantiParaCinsi"].ToString();
                isstmp.AktifPasif = AktifPasif.ToBool();

                listiss.Add(isstmp);
            }

            if (filtreKagitVarmi)
            {
                foreach (var item in listiss)
                {
                    item.OnayTip = 2;
                    item.SatisMuduruOnay = false;
                    item.FinansmanMuduruOnay = true;
                    item.GenelMudurOnay = false;
                    item.OnaylayanSatisMuduru = "OZ";  /// SM sadece Özgür Beyin onayına düşsün diye
                    item.OnaylayanFinansmanMuduru = "";
                    item.OnaylayanGenelMudur = "";
                }
            }

            if (listiss.Count > 0)
            {
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var kontrol = false;
                        var SozlesmeSiraNo = "";

                        SozlesmeSiraNo = "SÖZ " + db.Database.SqlQuery<int>(string.Format("[FINSAT6{0}].[wms].[SozlesmeSiraNoSelect]", vUser.SirketKodu)).FirstOrDefault();
                        var HesapKodu = "";
                        var ListeNo = "";
                        decimal BaglantiTutari = 0;
                        foreach (ISS_Temp isstemp in listiss)
                        {
                            if (isstemp.Kod2.Trim() != SozlesmeSiraNo)
                            {
                                kontrol = true;
                                isstemp.Kod2 = SozlesmeSiraNo;
                            }

                            HesapKodu = isstemp.MusteriKod;
                            ListeNo = isstemp.ListeNo;
                            BaglantiTutari = isstemp.Kod11;
                            sqlexper.Insert(isstemp);
                            var sonuc = sqlexper.AcceptChanges();
                            if (sonuc.Status == false) return "NO";
                        }

                        var s = string.Format("[FINSAT6{0}].[wms].[SetSozlesmeOnayTip] @HesapKodu='{1}' , @ListeNo='{2}' , @BaglantiTutari={3}", vUser.SirketKodu, HesapKodu, ListeNo, BaglantiTutari.ToString().Replace(",", "."));
                        var xx = db.Database.ExecuteSqlCommand(s);
                        db.SaveChanges();
                        dbContextTransaction.Commit();
                        if (kontrol) return SozlesmeSiraNo;
                        else return "OK";
                    }
                    catch (Exception hata)
                    {
                        return hata.Message;
                    }
                }
            }
            else
            {
                return "Satır Girmelisiniz.";
            }
        }

        public JsonResult Sil(string SozlesmeNo)
        {
            if (CheckPerm(Perms.SözleşmeTanim, PermTypes.Deleting) == false) return null;
            var _Result = new Result(true, "İşlem Başarılı.");
            try
            {
                db.Database.ExecuteSqlCommand(string.Format("DELETE FROM [FINSAT6{0}].[FINSAT6{0}].[ISS_Temp]  WHERE ListeNo = '{1}'", vUser.SirketKodu, SozlesmeNo));
            }
            catch (Exception)
            {
                _Result.Status = false;
                _Result.Message = "Hata Oluştu. ";
            }

            return Json(_Result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Guncelle(string SozlesmeNo, int BasTarih, short MusUygSekli, string YeniBaglantiTutari, int YeniBitisTarihi)
        {
            if (CheckPerm(Perms.SözleşmeTanim, PermTypes.Writing) == false) return null;
            var _Result = new Result(true, 1, "İşlem Başarılı.");
            var sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, vUser.SirketKodu);
            try
            {
                var filtreKagitVarmi = false;
                var list = db.Database.SqlQuery<ISS_Temp>(string.Format("SELECT * FROM [FINSAT6{0}].[FINSAT6{0}].[ISS_Temp] WHERE ListeNo='{1}' AND BasTarih = {2} AND MusUygSekli={3}", vUser.SirketKodu, SozlesmeNo, BasTarih, MusUygSekli)).ToList();
                foreach (ISS_Temp item in list)
                {
                    item.pk_ListeNo = SozlesmeNo;
                    item.pk_BasTarih = BasTarih;
                    item.pk_MusUygSekli = MusUygSekli;
                    item.pk_SiraNo = item.SiraNo;
                    item.Kod12 = item.Kod11;
                    item.Kod11 = YeniBaglantiTutari.ToDecimal();
                    if (YeniBitisTarihi != item.BitTarih)
                    {
                        item.Kod9 = item.BitTarih.ToString2();
                        item.BitTarih = YeniBitisTarihi;
                    }

                    if ((item.MalKodGrup == 0 && item.MalKod.StartsWith("2800")) || (item.MalKodGrup == 1 && item.MalKod == "FKAĞIT") || (item.MalKodGrup == 0 && (item.MalKod == "M001001000017051" || item.MalKod == "M001001000022051")))
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
                    db.Database.ExecuteSqlCommand(string.Format("DELETE FROM [FINSAT6{0}].[FINSAT6{0}].[ISS] where ListeNo='{1}' AND BasTarih = {2} AND MusUygSekli={3}", vUser.SirketKodu, SozlesmeNo, BasTarih, MusUygSekli));
                }
                else
                {
                    _Result.Status = false;
                    _Result.Message = "Hata Oluştu. ";
                }
            }
            catch (Exception)
            {
                _Result.Status = false;
                _Result.Message = "Hata Oluştu. ";
            }

            return Json(_Result, JsonRequestBehavior.AllowGet);
        }

        public bool BaglantiTutariGuncelle(string BaglantiNumarasi, decimal YeniBaglantiTutari)
        {
            var toplam = db.Database.SqlQuery<decimal>(string.Format("exec [FINSAT6{0}].[wms].[BaglantiTutariGuncelle]  @BaglantiNumarasi='{1}'", vUser.SirketKodu, BaglantiNumarasi)).FirstOrDefault();
            return toplam <= YeniBaglantiTutari;
        }

        #endregion Tanim
    }
}