using Birikim.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Wms12m.Entity;

namespace Wms12m.Presentation.Areas.Approvals.Controllers
{
    public class Contract2Controller : RootController
    {
        public ActionResult GM()
        {
            if (CheckPerm(Perms.SözleşmeOnaylama, PermTypes.Reading) == false) return Redirect("/");
            return View();
        }

        public PartialViewResult GM_List(string listeNo, string satir)
        {
            string sat = "";
            if (satir != null)
            {
                sat = "<tbody>";
                JArray parameters = JsonConvert.DeserializeObject<JArray>(satir);
                foreach (string insertObj in parameters)
                {
                    sat += insertObj;
                }
                sat += "</tbody>";
            }
            ViewBag.ListeNo = JsonConvert.SerializeObject(listeNo);
            ViewBag.Satir = sat;
            return PartialView("GM_List");
        }

        public string GMListele()
        {
            var GmListe = new List<GMListeGetir>();
            GmListe = db.Database.SqlQuery<GMListeGetir>(string.Format("[FINSAT6{0}].[wms].[SozlesmeGenelMudurList]", vUser.SirketKodu)).ToList();

            var json = new JavaScriptSerializer().Serialize(GmListe);
            return json;
        }

        public string OnayCekGM()
        {
            List<SozlesmeOnaySelect> RT;
            try
            {
                RT = db.Database.SqlQuery<SozlesmeOnaySelect>(string.Format("[FINSAT6{0}].[wms].[SP_SozlesmeOnay]", vUser.SirketKodu)).ToList();
            }
            catch (Exception)
            {
                RT = new List<SozlesmeOnaySelect>();
            }
            var json = new JavaScriptSerializer().Serialize(RT);
            return json;
        }

        public JsonResult Onay_GM(string Data, string listeNo)
        {
            Result _Result = new Result(true);
            if (CheckPerm(Perms.SözleşmeOnaylama, PermTypes.Writing) == false) return null;
            try
            {
                var onay = false;
                string[] istisna = { "DovizKuru", "OdemeAlinmadanSevkiyatYok", "PiyasaTipi", "SatisTemsilcisi" };
                var list = db.Database.SqlQuery<ISS_Temp>(string.Format("SELECT *  FROM [FINSAT6{0}].[FINSAT6{0}].[ISS_Temp] WHERE ListeNo='{1}'", vUser.SirketKodu, listeNo.ToString())).ToList();
                foreach (ISS_Temp lst in list)
                {
                    if (lst.OnayTip == 2 || lst.OnayTip == 3 || lst.OnayTip == 4)
                    {
                        onay = true;
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
                            Kod9 = lst.Kod9 ?? "", ///lst.BaglantiParaCinsi;
                            ListeAdi = lst.ListeAdi,
                            ListeNo = lst.ListeNo,
                            MalKod = lst.MalKod,
                            MalKodGrup = lst.MalKodGrup,
                            LimitKontrol = lst.LimitKontrol,
                            OdemeAlinmadanSevkiyatYok = lst.OdemeAlinmadanSevkiyatYok,
                            FiyatTip = lst.FiyatTip,
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
                            SozlesmeVadeGun = lst.SozlesmeVadeGun,
                            NetFiyat = lst.NetFiyat,
                            NetFiyatBirim = lst.NetFiyatBirim,
                            LimitMiktar = lst.LimitMiktar,
                            LimitMiktarBirim = lst.LimitMiktarBirim,
                            TutarYuzde8 = lst.TutarYuzde8
                        };
                        SqlExper.Insert(insrt, null, null, istisna);
                    }
                }
                var sonuc = SqlExper.AcceptChanges();
                if (sonuc.Status == true)
                {
                    if (onay == true)
                        db.Database.ExecuteSqlCommand(string.Format("[FINSAT6{0}].[wms].[SozlesmeGenelMudurOnay] @SozlesmeNo = '{1}',@Kullanici = '{2}'", vUser.SirketKodu, listeNo.ToString(), vUser.UserName));
                    _Result.Status = true;
                    _Result.Message = "İşlem Başarılı ";
                }
                else
                {
                    _Result.Status = false;
                    _Result.Message = "Hata Oluştu. ";
                }
            }
            catch (Exception ex)
            {
                Logger(ex, "contract2/Onay_GM");
                _Result.Status = false;
                _Result.Message = "Hata Oluştu. ";
            }
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Red_GM(string Data, string listeNo)
        {
            Result _Result = new Result(true);
            if (CheckPerm(Perms.SözleşmeOnaylama, PermTypes.Writing) == false) return null;
            try
            {
                db.Database.ExecuteSqlCommand(string.Format("DELETE FROM [FINSAT6{0}].[FINSAT6{0}].[ISS_Temp] WHERE  ListeNo = '{1}'", vUser.SirketKodu, listeNo.ToString()));

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

        [HttpPost]
        public PartialViewResult GM_Details(string ListeNo)
        {
            var list = db.Database.SqlQuery<BaglantiDetaySelect>(string.Format("[FINSAT6{0}].[wms].[BaglantiDetaySelect] '{1}'", vUser.SirketKodu, ListeNo)).ToList();
            return PartialView("Details", list);
        }

        public ActionResult YIGMY()
        {
            if (CheckPerm(Perms.SözleşmeOnaylama, PermTypes.Reading) == false) return Redirect("/");
            return View();
        }

        public PartialViewResult YIGMY_List(string listeNo, string satir)
        {
            string sat = "";
            if (satir != null)
            {
                sat = "<tbody>";
                JArray parameters = JsonConvert.DeserializeObject<JArray>(satir);
                foreach (string insertObj in parameters)
                {
                    sat += insertObj;
                }
                sat += "</tbody>";
            }
            ViewBag.ListeNo = JsonConvert.SerializeObject(listeNo);
            ViewBag.Satir = sat;
            return PartialView("YIGMY_List");
        }

        public string YIGMYListele()
        {
            var YIGMYListe = new List<YIGMYListeGetir>();
            YIGMYListe = db.Database.SqlQuery<YIGMYListeGetir>(string.Format("[FINSAT6{0}].[wms].[SozlesmeSatisMuduruList]", vUser.SirketKodu)).ToList();

            var json = new JavaScriptSerializer().Serialize(YIGMYListe);
            return json;
        }

        public string OnayCekSM()
        {
            List<SozlesmeOnaySelect> RT;
            try
            {
                RT = db.Database.SqlQuery<SozlesmeOnaySelect>(string.Format("[FINSAT6{0}].[wms].[SP_SozlesmeOnaySM]", vUser.SirketKodu)).ToList();
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
            var list = db.Database.SqlQuery<BaglantiDetaySelect>(string.Format("[FINSAT6{0}].[wms].[BaglantiDetaySelect] '{1}'", vUser.SirketKodu, ListeNo)).ToList();
            return PartialView("Details", list);
        }

        public JsonResult Onay_YIGMY(string Data, string listeNo)
        {
            Result _Result = new Result(true);
            if (CheckPerm(Perms.SözleşmeOnaylama, PermTypes.Writing) == false) return null;
            try
            {
                var s = string.Format("[FINSAT6{0}].[wms].[SozlesmeSatisMuduruOnay] @SozlesmeNo = '{1}',@Kullanici = '{2}'", vUser.SirketKodu, listeNo.ToString(), vUser.UserName);
                var xx = db.Database.ExecuteSqlCommand(s);
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

        public JsonResult Red_SM(string Data)
        {
            Result _Result = new Result(true);
            if (CheckPerm(Perms.SözleşmeOnaylama, PermTypes.Writing) == false) return null;
            JArray parameters = JsonConvert.DeserializeObject<JArray>(Request["Data"]);
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

        public ActionResult YDGMY()
        {
            if (CheckPerm(Perms.SözleşmeOnaylama, PermTypes.Reading) == false) return Redirect("/");
            return View();
        }

        public PartialViewResult YDGMY_List(string listeNo, string satir)
        {
            string sat = "";
            if (satir != null)
            {
                sat = "<tbody>";
                JArray parameters = JsonConvert.DeserializeObject<JArray>(satir);
                foreach (string insertObj in parameters)
                {
                    sat += insertObj;
                }
                sat += "</tbody>";
            }
            ViewBag.ListeNo = JsonConvert.SerializeObject(listeNo);
            ViewBag.Satir = sat;
            return PartialView("YDGMY_List");
        }

        public string YDGMYListele()
        {
            var YDGMYListe = new List<YDGMYListeGetir>();
            YDGMYListe = db.Database.SqlQuery<YDGMYListeGetir>(string.Format("[FINSAT6{0}].[wms].[SozlesmeIhracatMuduruList]", vUser.SirketKodu)).ToList();

            var json = new JavaScriptSerializer().Serialize(YDGMYListe);
            return json;
        }

        public string OnayCekSPGMY()
        {
            List<SozlesmeOnaySelect> RT;
            try
            {
                RT = db.Database.SqlQuery<SozlesmeOnaySelect>(string.Format("[FINSAT6{0}].[wms].[SP_SozlesmeOnaySPGMY]", vUser.SirketKodu)).ToList();
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
            var list = db.Database.SqlQuery<BaglantiDetaySelect>(string.Format("[FINSAT6{0}].[wms].[BaglantiDetaySelect] '{1}'", vUser.SirketKodu, ListeNo)).ToList();
            return PartialView("Details", list);
        }

        public JsonResult Onay_YDGMY(string Data, string listeNo)
        {
            Result _Result = new Result(true);
            if (CheckPerm(Perms.SözleşmeOnaylama, PermTypes.Writing) == false) return null;
            try
            {
                db.Database.ExecuteSqlCommand(string.Format("[FINSAT6{0}].[wms].[SozlesmeIhracatMuduruOnay] @SozlesmeNo = '{1}',@Kullanici = '{2}'", vUser.SirketKodu, listeNo.ToString(), vUser.UserName));
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

        public JsonResult Red_SPGMY(string Data)
        {
            Result _Result = new Result(true);
            if (CheckPerm(Perms.SözleşmeOnaylama, PermTypes.Writing) == false) return null;
            JArray parameters = JsonConvert.DeserializeObject<JArray>(Request["Data"]);
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

        public ActionResult MaliyetGiris()
        {
            if (CheckPerm(Perms.SözleşmeTanim, PermTypes.Reading) == false) return Redirect("/");
            return View();
        }

        public PartialViewResult Maliyet_List(string listeNo)
        {
            if (CheckPerm(Perms.SözleşmeTanim, PermTypes.Reading) == false) return null;
            var liste = db.Database.SqlQuery<BaglantiDetaySelect>(string.Format("[FINSAT6{0}].[wms].[BaglantiDetaySelect] @ListeNo='{1}'", vUser.SirketKodu, listeNo)).ToList();
            return PartialView("Maliyet_List", liste);
        }

        /// <summary>
        /// kaydet
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public JsonResult SaveMaliyet(frmBaglantiDetaySelect tbl)
        {
            if (CheckPerm(Perms.SözleşmeTanim, PermTypes.Writing) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            var sql = "";
            for (int i = 0; i < tbl.Maliyet.Length; i++)
            {
                if (tbl.Maliyet[i] == "") tbl.Maliyet[i] = "NULL";
                else tbl.Maliyet[i] = tbl.Maliyet[i].Replace(",", ".");
                sql += string.Format("UPDATE FINSAT6{0}.FINSAT6{0}.ISS_Temp SET Maliyet = {1} WHERE (Row_ID = {2});", vUser.SirketKodu, tbl.Maliyet[i], tbl.ID[i]);
            }
            try
            {
                db.Database.ExecuteSqlCommand(sql);
                return Json(new Result(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new Result(false, "Hata oldu!"), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Tanim()
        {
            if (CheckPerm(Perms.SözleşmeTanim, PermTypes.Reading) == false) return Redirect("/");
            ViewBag.SRNO = "SOZ " + db.Database.SqlQuery<int?>(string.Format("[FINSAT6{0}].[wms].[SozlesmeSiraNoSelect]", vUser.SirketKodu)).FirstOrDefault();
            return View();
        }

        public PartialViewResult Tanim_List(string listeNo, string satir)
        {
            if (CheckPerm(Perms.SözleşmeTanim, PermTypes.Reading) == false) return null;
            string sat = "";
            if (satir != null)
            {
                sat = "<tbody>";
                JArray parameters = JsonConvert.DeserializeObject<JArray>(satir);
                foreach (string insertObj in parameters)
                {
                    sat += insertObj;
                }
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

        public string List(int tip)
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

        public string ListMaliyet(int tip)
        {
            var sozlesmeler = new List<SozlesmeListesi>();
            if (tip == 0)
            {
                sozlesmeler = db.Database.SqlQuery<SozlesmeListesi>(string.Format("[FINSAT6{0}].[wms].[Sozlesme_Maliyet_Girilmis]", vUser.SirketKodu)).ToList();
            }
            else
            {
                sozlesmeler = db.Database.SqlQuery<SozlesmeListesi>(string.Format("[FINSAT6{0}].[wms].[Sozlesme_Maliyet_Bekleyen]", vUser.SirketKodu)).ToList();
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
            var ISSTemp = db.Database.SqlQuery<ISS_Temp>(string.Format(@"
                    SELECT ISS_Temp.*, [FINSAT6{0}].[FINSAT6{0}].KTK.Aciklama as SatisTemsilcisiAd, CHK.CiroB
                    FROM FINSAT6{0}.FINSAT6{0}.ISS_Temp(NOLOCK) INNER JOIN
                            FINSAT6{0}.FINSAT6{0}.KTK(NOLOCK)  ON ISS_Temp.SatisTemsilcisi = KTK.Kod LEFT JOIN
                            FINSAT6{0}.FINSAT6{0}.CHK(NOLOCK) ON CHK.HesapKodu = ISS_Temp.MusteriKod
                    WHERE(FINSAT6{0}.FINSAT6{0}.KTK.Tip = 105) AND ListeNo = '{1}'", vUser.SirketKodu, SozlesmeNo)).FirstOrDefault();
            return json.Serialize(ISSTemp);
        }

        public string FiyatListeleriSelect()
        {
            JavaScriptSerializer json = new JavaScriptSerializer()
            {
                MaxJsonLength = int.MaxValue
            };
            var FYTList = db.Database.SqlQuery<ListeNoSelect>(string.Format("[FINSAT6{0}].[wms].[FYTSelect2]", vUser.SirketKodu)).ToList();
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
            JavaScriptSerializer json = new JavaScriptSerializer()
            {
                MaxJsonLength = int.MaxValue
            };
            var FUGS = db.Database.SqlQuery<UrunGrup>(string.Format("[FINSAT6{0}].[wms].[STKSelect2] @Index={1}", vUser.SirketKodu, Index)).ToList();
            return json.Serialize(FUGS);
        }

        public string GrupKodSelect(string MalKodu)
        {
            var data = db.Database.SqlQuery<string>(string.Format("SELECT GrupKod FROM [FINSAT6{0}].[FINSAT6{0}].[STK]  WHERE MalKodu = '{1}'", vUser.SirketKodu, MalKodu)).FirstOrDefault();
            return data;
        }

        public string BirimSelect(string Index, string UrunGrubu)
        {
            JavaScriptSerializer json = new JavaScriptSerializer()
            {
                MaxJsonLength = int.MaxValue
            };
            var data = db.Database.SqlQuery<string>(string.Format(@"	SELECT TOP 1 Birim1 as Birim    FROM FINSAT6{0}.FINSAT6{0}.STK(NOLOCK)    WHERE {2} = '{1}'
        UNION
            SELECT TOP 1 Birim2 as Birim    FROM FINSAT6{0}.FINSAT6{0}.STK(NOLOCK)    WHERE {2} = '{1}'
        UNION
            SELECT TOP 1 Birim3 as Birim    FROM FINSAT6{0}.FINSAT6{0}.STK(NOLOCK)    WHERE {2} = '{1}'", vUser.SirketKodu, Index, UrunGrubu)).ToList();
            return json.Serialize(data);
        }

        public string EnBoyKalinlik(int Index, string Kriter)
        {
            JavaScriptSerializer json = new JavaScriptSerializer()
            {
                MaxJsonLength = int.MaxValue
            };
            var data = db.Database.SqlQuery<EnBoyKalinlik>(string.Format("[FINSAT6{0}].[wms].[STKBirimSelect] @Kriter='{2}', @Index='{1}'", vUser.SirketKodu, Index.ToInt32(), Kriter.ToString())).ToList();
            return json.Serialize(data);
        }

        public int ListeNoKontrol(string ListeNo)
        {
            var VarMi = db.Database.SqlQuery<int>(string.Format("SELECT Count(ListeNo) FROM [FINSAT6{0}].[FINSAT6{0}].[ISS_Temp] WHERE ListeNo='{1}'", vUser.SirketKodu, ListeNo)).FirstOrDefault();
            return VarMi;
        }

        public JsonResult ISS_TempUpdate_AktifPasif(string OrjListeNo, string YeniSozlesmeNo, bool AktifPasif, bool onayadusur, int? SozlesmeVadeGun, short? LimitKontrol, string Kod1,
            decimal? Kod11, string MusteriKod, short? PiyasaTipi, int? OdemeAlinmadanSevkiyatYok, int? BasTarih, int? BitTarih)
        {
            Result _Result = new Result(true);
            if (CheckPerm(Perms.SözleşmeTanim, PermTypes.Writing) == false) return null;
            try
            {
                var ISSS = db.Database.SqlQuery<ISS_Temp>(string.Format("SELECT * FROM [FINSAT6{0}].[FINSAT6{0}].[ISS_Temp]", vUser.SirketKodu)).ToList();

                if (onayadusur)
                {
                    if (!AktifPasif)   ///Pasif In Aktif olunca
                    {
                        db.Database.ExecuteSqlCommand(string.Format(@"UPDATE [FINSAT6{0}].[FINSAT6{0}].[ISS_Temp] SET AktifPasif = 0, GenelMudurOnay=1,IhracatMuduruOnay=1,
                        FinansmanMuduruOnay=1, SatisMuduruOnay=1, OnaylayanFinansmanMuduru='', OnaylayanGenelMudur='', OnaylayanIhracatMuduru='', OnaylayanSatisMuduru='', ListeNo='{2}',
                        SozlesmeVadeGun={3}, LimitKontrol={4}, Kod1='{5}', Kod11={6}, MusteriKod='{7}', PiyasaTipi={8}, OdemeAlinmadanSevkiyatYok={9}, BasTarih={10}, BitTarih={11}
                        where ListeNo = '{1}'", vUser.SirketKodu, OrjListeNo, YeniSozlesmeNo, SozlesmeVadeGun.ToInt32(), LimitKontrol.ToShort(), Kod1, Kod11.ToDecimal(), MusteriKod, PiyasaTipi.ToShort(), OdemeAlinmadanSevkiyatYok.ToInt32(), BasTarih.ToInt32(), BitTarih.ToInt32()));
                        db.Database.ExecuteSqlCommand(string.Format("DELETE FROM [FINSAT6{0}].[FINSAT6{0}].[ISS] where ListeNo = '{1}'", vUser.SirketKodu, OrjListeNo));
                    }
                    if (AktifPasif)
                    {
                        db.Database.ExecuteSqlCommand(string.Format(@"UPDATE [FINSAT6{0}].[FINSAT6{0}].[ISS_Temp] SET AktifPasif = 1, GenelMudurOnay=1,IhracatMuduruOnay=1,
                        FinansmanMuduruOnay=1, SatisMuduruOnay=1, OnaylayanFinansmanMuduru='', OnaylayanGenelMudur='', OnaylayanIhracatMuduru='', OnaylayanSatisMuduru='', ListeNo='{2}',
                        SozlesmeVadeGun={3}, LimitKontrol={4}, Kod1='{5}', Kod11={6}, MusteriKod='{7}', PiyasaTipi={8}, OdemeAlinmadanSevkiyatYok={9}, BasTarih={10}, BitTarih={11}
                        where ListeNo = '{1}'", vUser.SirketKodu, OrjListeNo, YeniSozlesmeNo, SozlesmeVadeGun.ToInt32(), LimitKontrol.ToShort(), Kod1, Kod11.ToDecimal(), MusteriKod, PiyasaTipi.ToShort(), OdemeAlinmadanSevkiyatYok.ToInt32(), BasTarih.ToInt32(), BitTarih.ToInt32()));
                        db.Database.ExecuteSqlCommand(string.Format("DELETE FROM [FINSAT6{0}].[FINSAT6{0}].[ISS] where ListeNo = '{1}'", vUser.SirketKodu, OrjListeNo));
                    }
                }
                else
                {
                    if (!AktifPasif)   ///Pasif In Aktif olunca
                    {
                        db.Database.ExecuteSqlCommand(string.Format("UPDATE [FINSAT6{0}].[FINSAT6{0}].[ISS_Temp] SET AktifPasif = 0  where ListeNo = '{1}'", vUser.SirketKodu, OrjListeNo));
                    }
                    else
                    {
                        db.Database.ExecuteSqlCommand(string.Format("UPDATE [FINSAT6{0}].[FINSAT6{0}].[ISS_Temp] SET AktifPasif = 1  where ListeNo = '{1}'", vUser.SirketKodu, OrjListeNo));
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

        public string YeniSatirKayit(string Data, bool degisiksatir, bool siranosifirdanbasla, string sozno)
        {
            if (CheckPerm(Perms.SözleşmeTanim, PermTypes.Writing) == false) return null;
            JArray parameters = JsonConvert.DeserializeObject<JArray>(Request["Data"]);
            bool filtreKagitVarmi = false;
            bool birdenfazlasatir = false;
            short SiraNo = 0;
            bool? AktifPasif = null;
            List<ISS_Temp> listiss = new List<ISS_Temp>();
            foreach (JObject bds in parameters)
            {
                ISS_Temp isstmp = new ISS_Temp();
                AktifPasif = bds["AktifPasif"].ToBool();
                string ListeAdi = bds["Unvan"].ToString().Length >= 10 ? bds["Unvan"].ToString().Substring(0, 10) : bds["Unvan"].ToString();

                string listeno = bds["ListeNo"].ToString();
                if (degisiksatir && !birdenfazlasatir && !siranosifirdanbasla)
                {
                    SiraNo = db.Database.SqlQuery<short>(string.Format("SELECT ISNULL(MAX (SiraNo),0) FROM [FINSAT6{0}].[FINSAT6{0}].ISS_Temp (NOLOCK) where ListeNo ='{1}'", vUser.SirketKodu, listeno)).FirstOrDefault();
                    SiraNo++;
                }

                if (birdenfazlasatir)
                {
                    SiraNo++;
                }

                birdenfazlasatir = true;

                isstmp.MalKod = bds["UrunKodu"].ToString();
                isstmp.ListeNo = bds["ListeNo"].ToString();
                isstmp.ListeAdi = bds["Unvan"].ToString().Length >= 30 ? bds["Unvan"].ToString().Substring(0, 30) : bds["Unvan"].ToString();
                isstmp.BasTarih = bds["BasTarih"].ToString() == "" ? (int)DateTime.Now.ToOADate() : bds["BasTarihInt"].ToInt32();
                isstmp.BasSaat = fn.ToOATime();
                // saati araştır
                isstmp.BitTarih = bds["BitTarih"].ToString() == "" ? (int)DateTime.Now.ToOADate() : bds["BitTarihInt"].ToInt32();
                isstmp.CekOrtalamaVadeTarih = bds["CekOrtalamaVadeTarih"].ToString() == "" ? DateTime.Now.ToDatetime() : bds["CekOrtalamaVadeTarih"].ToDatetime();
                isstmp.BitSaat = fn.ToOATime();
                isstmp.MusUygSekli = 1;
                isstmp.MusKodGrup = 0;
                isstmp.MusteriKod = bds["HesapKodu"] == null ? "" : bds["HesapKodu"].ToString();
                isstmp.MalUygSekli = 1;
                if (bds["MalKodGrup"].ToString() == "Mal Kodu")
                    isstmp.MalKodGrup = 0;
                else if (bds["MalKodGrup"].ToString() == "Grup Kodu")
                    isstmp.MalKodGrup = 1;
                else if (bds["MalKodGrup"].ToString() == "Tip Kodu")
                    isstmp.MalKodGrup = 2;
                else if (bds["MalKodGrup"].ToString() == "Özel Kod")
                    isstmp.MalKodGrup = 3;
                else if (bds["MalKodGrup"].ToString() == "Kod1")
                    isstmp.MalKodGrup = 4;
                else if (bds["MalKodGrup"].ToString() == "Kod2")
                    isstmp.MalKodGrup = 5;
                else if (bds["MalKodGrup"].ToString() == "Kod3")
                    isstmp.MalKodGrup = 6;
                else if (bds["MalKodGrup"].ToString() == "Kod4")
                    isstmp.MalKodGrup = 7;

                isstmp.Birim = bds["FiyatBirim"] == null ? "" : bds["FiyatBirim"].ToString();
                isstmp.SatisTemsilcisi = bds["SatisTemsilcisi"].ToString();
                isstmp.OdemeAlinmadanSevkiyatYok = bds["OdemeAlinmadanSevkiyatYok"].ToBool();
                isstmp.DovizKuru = bds["DovizKuru"] == null ? 0 : bds["DovizKuru"].ToString().ToDecimal();
                isstmp.PiyasaTipi = bds["PiyasaTipi"].ToShort();
                isstmp.LimitMiktar = bds["LimitMiktar"] == null ? 0 : bds["LimitMiktar"].ToString().ToDecimal();
                isstmp.LimitMiktarBirim = bds["LimitMiktarBirim"] == null ? "" : bds["LimitMiktarBirim"].ToString();
                isstmp.NetFiyatBirim = bds["NetFiyatBirim"].ToString();
                isstmp.NetFiyat = bds["NetFiyat"].ToString().ToDecimal();
                isstmp.FiyatTip = bds["FiyatTip"].ToShort();
                isstmp.SozlesmeVadeGun = bds["SozlesmeVadeGun"].ToInt32();
                isstmp.Kalite = bds["Kalite"].ToString();
                isstmp.AktifPasif = bds["AktifPasif"].ToBool();

                isstmp.SiraNo = SiraNo.ToShort();
                isstmp.Oran = 0;
                isstmp.Oran1 = Convert.ToSingle(bds["Oran1"].ToString());
                isstmp.Oran2 = Convert.ToSingle(bds["Oran2"].ToString());
                isstmp.Oran3 = Convert.ToSingle(bds["Oran3"].ToString());
                isstmp.Oran4 = Convert.ToSingle(bds["Oran4"].ToString());
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
                isstmp.CekTutari = 0;

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
                isstmp.Aciklama = bds["Aciklama"].ToString();
                isstmp.Kod1 = bds["BaglantiTipi"].ToString();
                isstmp.Kod2 = bds["SiraNo"].ToString();
                isstmp.Kod3 = bds["Kod3"].ToString();
                isstmp.Kod4 = "";
                isstmp.Kod5 = "";
                isstmp.Kod6 = "";
                isstmp.Kod7 = "";
                isstmp.Kod8 = "";
                isstmp.Kod9 = "";
                isstmp.Kod10 = bds["Kod10"] == null ? "" : bds["Kod10"].ToString();
                isstmp.Kod11 = bds["BaglantiTutari"].ToString().ToDecimal();
                isstmp.Kod12 = 0;
                isstmp.DevirTarih = 0;
                isstmp.DevirTutar = 0;
                isstmp.LimitKontrol = bds["LimitKontrol"].ToShort();

                if (bds["PiyasaTipi"].ToShort() == 2)
                { isstmp.OnayTip = 4; }
                else if (bds["OnayTip"].ToShort() == 3)
                { isstmp.OnayTip = 3; }
                else if (bds["OnayTip"].ToShort() == 2)
                { isstmp.OnayTip = 2; }
                else
                { isstmp.OnayTip = 3; }

                isstmp.SatisMuduruOnay = true;
                isstmp.IhracatMuduruOnay = true;
                isstmp.OnaylayanIhracatMuduru = "";
                isstmp.FinansmanMuduruOnay = false;
                isstmp.GenelMudurOnay = true;
                isstmp.OnaylayanSatisMuduru = "";
                isstmp.OnaylayanFinansmanMuduru = "";
                isstmp.OnaylayanGenelMudur = "";


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
                        bool kontrol = false;
                        string SozlesmeSiraNo = "";
                        string[] istisna = { "SatisTemsilcisiAd", "CiroB" };
                        SozlesmeSiraNo = "SÖZ " + db.Database.SqlQuery<int>(string.Format("[FINSAT6{0}].[wms].[SozlesmeSiraNoSelect]", vUser.SirketKodu)).FirstOrDefault();
                        string HesapKodu = "";
                        string ListeNo = "";
                        decimal BaglantiTutari = 0;
                        foreach (ISS_Temp isstemp in listiss)
                        {
                            if (!degisiksatir)
                            {
                                if (isstemp.Kod2.Trim() != SozlesmeSiraNo)
                                {
                                    kontrol = true;
                                    isstemp.Kod2 = SozlesmeSiraNo;
                                }
                            }
                            else if (degisiksatir) { isstemp.Kod2 = SozlesmeSiraNo; }

                            HesapKodu = isstemp.MusteriKod;
                            ListeNo = isstemp.ListeNo;
                            BaglantiTutari = isstemp.Kod11;
                            SqlExper.Insert(isstemp, null, null, istisna);
                            var sonuc = SqlExper.AcceptChanges();
                            if (sonuc.Status == false)
                            {
                                return "NO";
                            }
                        }
                        db.SaveChanges();
                        dbContextTransaction.Commit();

                        string[] esgec = { "DovizKuru", "OdemeAlinmadanSevkiyatYok", "PiyasaTipi", "SatisTemsilcisi" };
                        var list = db.Database.SqlQuery<ISS_Temp>(string.Format("SELECT *  FROM [FINSAT6{0}].[FINSAT6{0}].[ISS_Temp] WHERE ListeNo='{1}'", vUser.SirketKodu, sozno.ToString())).ToList();
                        foreach (ISS_Temp lst in list)
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
                                Kod9 = lst.Kod9 ?? "", ///lst.BaglantiParaCinsi;
                                ListeAdi = lst.ListeAdi,
                                ListeNo = lst.ListeNo,
                                MalKod = lst.MalKod,
                                MalKodGrup = lst.MalKodGrup,
                                LimitKontrol = lst.LimitKontrol,
                                OdemeAlinmadanSevkiyatYok = lst.OdemeAlinmadanSevkiyatYok,
                                FiyatTip = lst.FiyatTip,
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
                                SozlesmeVadeGun = lst.SozlesmeVadeGun,
                                NetFiyat = lst.NetFiyat,
                                NetFiyatBirim = lst.NetFiyatBirim,
                                LimitMiktar = lst.LimitMiktar,
                                LimitMiktarBirim = lst.LimitMiktarBirim,
                                TutarYuzde8 = lst.TutarYuzde8
                            };
                            SqlExper.Insert(insrt, null, null, esgec);
                        }
                        var netice = SqlExper.AcceptChanges();

                        if (kontrol)
                            return SozlesmeSiraNo;
                        else
                            return "OK";
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
            Result _Result = new Result(true, "İşlem Başarılı.");
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

        public JsonResult Guncelle(string SozlesmeNo, int BasTarih, short MusUygSekli, decimal YeniBaglantiTutari, int YeniBitisTarihi)
        {
            if (CheckPerm(Perms.SözleşmeTanim, PermTypes.Writing) == false) return null;
            Result _Result = new Result(true, "İşlem Başarılı.");
            try
            {
                bool filtreKagitVarmi = false;
                var list = db.Database.SqlQuery<ISS_Temp>(string.Format("SELECT * FROM [FINSAT6{0}].[FINSAT6{0}].[ISS_Temp] WHERE ListeNo='{1}' AND BasTarih = {2} AND MusUygSekli={3}", vUser.SirketKodu, SozlesmeNo, BasTarih, MusUygSekli)).ToList();
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

                    SqlExper.Update(item, null, null, false, "timestamp");
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

                        SqlExper.Update(item, null, null, false, "timestamp");
                    }
                }

                var sonuc = SqlExper.AcceptChanges();
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

        public string SatisTemsilcisiSelect()
        {
            var FHKS = db.Database.SqlQuery<SatisTemsilcisiSelect>(string.Format("[FINSAT6{0}].[wms].[Sozlesme_SatisTemsilcileri]", vUser.SirketKodu)).ToList();
            var json = new JavaScriptSerializer().Serialize(FHKS);
            return json;
        }
    }
}