using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m.Presentation.Areas.Approvals.Controllers
{
    public class TumOrderController : RootController
    {
        public LOGEntities logdb = new LOGEntities();
        // GET: Approvals/IzOrder
        public ActionResult Index(string onayRed)
        {
            if (onayRed == null)
            {
                ViewBag.OnayDurum = "Beklemede";
            }
            else
            {
                ViewBag.OnayDurum = onayRed;
            }
            return View();
        }

        public PartialViewResult TumOrderList(string Tip, int bastarih, int bittarih)
        {
            ViewBag.Tip = Tip;
            ViewBag.bastarih = bastarih;
            ViewBag.bittarih = bittarih;
            return PartialView("List");
        }

        public string TumOrderListData(string tip, int bastarih, int bittarih)
        {

            JavaScriptSerializer json = new JavaScriptSerializer()
            {
                MaxJsonLength = int.MaxValue
            };
            List<SipOnay> sipBilgi = new List<SipOnay>();

            try
            {
                var tbl = db.UserDetails.Where(m => m.UserID == vUser.Id).FirstOrDefault();
                if (tbl != null)
                {
                    var Sirketler = tbl.GosterilecekSirket;
                    var CHKAraligi = tbl.GostCHKKodAlani;
                    var TipKodlari = tbl.GostSTKDeger;
                    var Kod3Araligi = "";
                    var Kod3Araligi2 = "";
                    var RiskAraligi = "";
                    var RiskAraligi2 = "";

                    var Kod3Array = tbl.GostKod3OrtBakiye.Split(';');
                    var RiskArray = tbl.GostRiskDeger.Split(';');


                    if (Kod3Array.Length > 3)
                    {
                        Kod3Araligi = Kod3Array[0] + ";" + Kod3Array[1];
                        Kod3Araligi2 = Kod3Array[2] + ";" + Kod3Array[3];

                    }
                    else if (Kod3Array.Length > 1)
                    {
                        Kod3Araligi = Kod3Array[0] + ";" + Kod3Array[1];
                    }

                    if (RiskArray.Length > 3)
                    {
                        RiskAraligi = RiskArray[0] + ";" + RiskArray[1];
                        RiskAraligi2 = RiskArray[2] + ";" + RiskArray[3];

                    }
                    else if (RiskArray.Length > 1)
                    {
                        RiskAraligi = RiskArray[0] + ";" + RiskArray[1];
                    }

                    var Grup = vUser.RoleName;

                    if (Sirketler.Contains("Tüm;"))
                    {
                        if (Sirketler.Contains("Tümpa;"))
                        {
                            // TÜmAndTümpaProcedure Çağır
                            sipBilgi = db.Database.SqlQuery<SipOnay>(string.Format("[FINSAT6{0}].[wms].[TumAndTumpaSiparisOnayList] @OnayDurm='{1}', @Secim=0, @ChkAralik='{2}', @Sirketler='{3}', @TipKodlari='{4}',@Kod3Aralik='{5}',@RiskAralik='{6}', @Grup='{7}', @BasTarih={8}, @BitTarih={9},@Kod3Aralik2='{10}',@RiskAralik2='{11}'", "71", tip, CHKAraligi, Sirketler, TipKodlari, Kod3Araligi, RiskAraligi, Grup, bastarih, bittarih, Kod3Araligi2, RiskAraligi2)).ToList();
                        }
                        else
                        {
                            // TÜm Procedure Çağır
                            sipBilgi = db.Database.SqlQuery<SipOnay>(string.Format("[FINSAT6{0}].[wms].[TumSiparisOnayList] @OnayDurm='{1}', @Secim=0, @ChkAralik='{2}', @Sirketler='{3}', @TipKodlari='{4}',@Kod3Aralik='{5}',@RiskAralik='{6}', @Grup='{7}', @BasTarih={8}, @BitTarih={9},@Kod3Aralik2='{10}',@RiskAralik2='{11}'", "71", tip, CHKAraligi, Sirketler, TipKodlari, Kod3Araligi, RiskAraligi, Grup, bastarih, bittarih, Kod3Araligi2, RiskAraligi2)).ToList();
                        }
                    }
                    else if (Sirketler.Contains("Tümpa;"))
                    {
                        // TÜmpa Procedure Çağır
                        sipBilgi = db.Database.SqlQuery<SipOnay>(string.Format("[FINSAT6{0}].[wms].[TumpaSiparisOnayList] @OnayDurm='{1}', @Secim=0, @ChkAralik='{2}', @Sirketler='{3}', @TipKodlari='{4}',@Kod3Aralik='{5}',@RiskAralik='{6}', @Grup='{7}', @BasTarih={8}, @BitTarih={9},@Kod3Aralik2='{10}',@RiskAralik2='{11}'", "71", tip, CHKAraligi, Sirketler, TipKodlari, Kod3Araligi, RiskAraligi, Grup, bastarih, bittarih, Kod3Araligi2, RiskAraligi2)).ToList();
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return json.Serialize(sipBilgi);
        }


        public JsonResult Onayla(string Data)
        {
            Result _Result = new Result(true);
            JArray parameters = JsonConvert.DeserializeObject<JArray>(Request["Data"]);
            var logDetay = "";
            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                using (var logdbContextTransaction = logdb.Database.BeginTransaction())
                {
                    var tbl = db.UserDetails.Where(m => m.UserID == vUser.Id).FirstOrDefault();
                    foreach (JObject insertObj in parameters)
                    {
                        if (insertObj["Firma"].ToString() == "Tümpa")
                        {
                            db.Database.ExecuteSqlCommand(string.Format("[FINSAT6{0}].[wms].[TumpaSiparisOnayla] @OncekiDurum = '{1}',@Kullanici = '{2}', @EvrakNo='{3}'", "71", insertObj["OnayDurumu"].ToString(), vUser.UserName, insertObj["EvrakNo"].ToString()));
                            logDetay = "FINSAT671.SPI tablosunda Evrak Numarası '" + insertObj["EvrakNo"].ToString() + "' ve Onay Durumu '" + insertObj["OnayDurumu"].ToString() + "' olan satırların OnayDurumu " + vUser.UserName + " kullanıcısının siparişi onaylaması sonucu 'Onaylandı' olarak güncellenmiştir.";
                        }
                        else if (insertObj["Firma"].ToString() == "Tüm")
                        {
                            db.Database.ExecuteSqlCommand(string.Format("[FINSAT6{0}].[wms].[TumSiparisOnayla] @OncekiDurum = '{1}',@Kullanici = '{2}', @EvrakNo='{3}'", "71", insertObj["OnayDurumu"].ToString(), vUser.UserName, insertObj["EvrakNo"].ToString()));
                            logDetay = "FINSAT633.SPI tablosunda Evrak Numarası '" + insertObj["EvrakNo"].ToString() + "' ve Onay Durumu '" + insertObj["OnayDurumu"].ToString() + "' olan satırların OnayDurumu " + vUser.UserName + " kullanıcısının siparişi onaylaması sonucu 'Onaylandı' olarak güncellenmiştir.";
                        }

                        LogActions("Approvals", "TumOrder", "Onayla", ComboItems.alOnayla, 0, logDetay);

                        TumSiparisOnayLog sip = new TumSiparisOnayLog();
                        sip.Bakiye = insertObj["Bakiye"].ToDecimal();
                        sip.Unvan = insertObj["Unvan"].ToString();
                        sip.EvrakNo = insertObj["EvrakNo"].ToString();
                        sip.DegisTarih = (int)DateTime.Now.ToOADate();
                        sip.Degistiren = vUser.UserName.ToString();
                        sip.SirketAralik = tbl.GosterilecekSirket;
                        sip.CHKAralik = tbl.GostCHKKodAlani;
                        sip.TipKodlari = tbl.GostSTKDeger;
                        sip.RiskBakiyeAralik = tbl.GostKod3OrtBakiye;
                        sip.RiskAralik = tbl.GostRiskDeger;
                        sip.Risk = insertObj["Risk"].ToDecimal();
                        sip.RoleName = vUser.RoleName;
                        sip.Firma = insertObj["Firma"].ToString();
                        sip.GunIciSiparis = insertObj["GunIciSiparis"].ToDecimal();
                        sip.HesapKodu = insertObj["HesapKodu"].ToString();
                        sip.Kod2 = insertObj["Kod2"].ToString();
                        sip.Kod3OrtBakiye = insertObj["Kod3OrtBakiye"].ToDecimal();
                        sip.Kod3OrtGun = insertObj["Kod3OrtGun"].ToDecimal();
                        sip.KrediLimiti = insertObj["KrediLimiti"].ToDecimal();
                        sip.OnayDurumu = "Onaylandı";
                        sip.OrtGun = insertObj["OrtGun"].ToDecimal();
                        sip.PRTBakiye = insertObj["PRTBakiye"].ToDecimal();
                        sip.SCek = insertObj["SCek"].ToDecimal();
                        sip.SicakSiparis = insertObj["SicakSiparis"].ToDecimal();
                        sip.SiparisTuru = insertObj["SiparisTuru"].ToString();
                        sip.SogukSiparis = insertObj["SogukSiparis"].ToDecimal();
                        sip.TCek = insertObj["TCek"].ToDecimal();
                        sip.TipKodu = insertObj["TipKodu"].ToString();
                        sip.RiskBakiyesi = insertObj["RiskBakiyesi"].ToDecimal();

                        logdb.TumSiparisOnayLogs.Add(sip);
                    }
                    try
                    {
                        db.SaveChanges();
                        logdb.SaveChanges();
                        logdbContextTransaction.Commit();
                        dbContextTransaction.Commit();

                        _Result.Status = true;
                        _Result.Message = "İşlem Başarılı ";
                    }
                    catch (Exception ex)
                    {
                        _Result.Status = false;
                        _Result.Message = "Hata Oluştu.";
                    }
                }
            }
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }


        public JsonResult Reddet(string Data)
        {
            Result _Result = new Result(true);
            JArray parameters = JsonConvert.DeserializeObject<JArray>(Request["Data"]);
            var logDetay = "";
            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                var tbl = db.UserDetails.Where(m => m.UserID == vUser.Id).FirstOrDefault();
                using (var logdbContextTransaction = logdb.Database.BeginTransaction())
                {
                    foreach (JObject insertObj in parameters)
                    {
                        if (insertObj["Firma"].ToString() == "Tümpa")
                        {
                            db.Database.ExecuteSqlCommand(string.Format("[FINSAT6{0}].[wms].[TumpaSiparisReddet] @OncekiDurum = '{1}',@Kullanici = '{2}', @EvrakNo='{3}'", "71", insertObj["OnayDurumu"].ToString(), vUser.UserName, insertObj["EvrakNo"].ToString()));
                            logDetay = "FINSAT671.SPI tablosunda Evrak Numarası '" + insertObj["EvrakNo"].ToString() + "' ve Onay Durumu '" + insertObj["OnayDurumu"].ToString() + "' olan satırların OnayDurumu " + vUser.UserName + " kullanıcısının siparişi reddetmesi sonucu 'Reddedildi' olarak güncellenmiştir.";
                        }
                        else if (insertObj["Firma"].ToString() == "Tüm")
                        {
                            db.Database.ExecuteSqlCommand(string.Format("[FINSAT6{0}].[wms].[TumSiparisReddet] @OncekiDurum = '{1}',@Kullanici = '{2}', @EvrakNo='{3}'", "71", insertObj["OnayDurumu"].ToString(), vUser.UserName, insertObj["EvrakNo"].ToString()));
                            logDetay = "FINSAT633.SPI tablosunda Evrak Numarası '" + insertObj["EvrakNo"].ToString() + "' ve Onay Durumu '" + insertObj["OnayDurumu"].ToString() + "' olan satırların OnayDurumu " + vUser.UserName + " kullanıcısının siparişi reddetmesi sonucu 'Reddedildi' olarak güncellenmiştir.";
                        }

                        LogActions("Approvals", "TumOrder", "Reddet", ComboItems.alRed, 0, logDetay);

                        TumSiparisOnayLog sip = new TumSiparisOnayLog();
                        sip.Bakiye = insertObj["Bakiye"].ToDecimal();
                        sip.Unvan = insertObj["Unvan"].ToString();
                        sip.EvrakNo = insertObj["EvrakNo"].ToString();
                        sip.DegisTarih = (int)DateTime.Now.ToOADate();
                        sip.Degistiren = vUser.UserName.ToString();
                        sip.SirketAralik = tbl.GosterilecekSirket;
                        sip.CHKAralik = tbl.GostCHKKodAlani;
                        sip.TipKodlari = tbl.GostSTKDeger;
                        sip.RiskBakiyeAralik = tbl.GostKod3OrtBakiye;
                        sip.RiskAralik = tbl.GostRiskDeger;
                        sip.Risk = insertObj["Risk"].ToDecimal();
                        sip.RoleName = vUser.RoleName;
                        sip.Firma = insertObj["Firma"].ToString();
                        sip.GunIciSiparis = insertObj["GunIciSiparis"].ToDecimal();
                        sip.HesapKodu = insertObj["HesapKodu"].ToString();
                        sip.Kod2 = insertObj["Kod2"].ToString();
                        sip.Kod3OrtBakiye = insertObj["Kod3OrtBakiye"].ToDecimal();
                        sip.Kod3OrtGun = insertObj["Kod3OrtGun"].ToDecimal();
                        sip.KrediLimiti = insertObj["KrediLimiti"].ToDecimal();
                        sip.OnayDurumu = "Reddedildi";
                        sip.OrtGun = insertObj["OrtGun"].ToDecimal();
                        sip.PRTBakiye = insertObj["PRTBakiye"].ToDecimal();
                        sip.SCek = insertObj["SCek"].ToDecimal();
                        sip.SicakSiparis = insertObj["SicakSiparis"].ToDecimal();
                        sip.SiparisTuru = insertObj["SiparisTuru"].ToString();
                        sip.SogukSiparis = insertObj["SogukSiparis"].ToDecimal();
                        sip.TCek = insertObj["TCek"].ToDecimal();
                        sip.TipKodu = insertObj["TipKodu"].ToString();
                        sip.RiskBakiyesi = insertObj["RiskBakiyesi"].ToDecimal();

                        logdb.TumSiparisOnayLogs.Add(sip);
                    }
                    try
                    {
                        db.SaveChanges();
                        logdb.SaveChanges();
                        logdbContextTransaction.Commit();
                        dbContextTransaction.Commit();


                        _Result.Status = true;
                        _Result.Message = "İşlem Başarılı ";
                    }
                    catch (Exception ex)
                    {
                        _Result.Status = false;
                        _Result.Message = "Hata Oluştu.";
                    }
                }
            }
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
    }
}