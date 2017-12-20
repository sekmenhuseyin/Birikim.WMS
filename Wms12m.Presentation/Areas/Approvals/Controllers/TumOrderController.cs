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
        /// <summary>
        /// anasayfa
        /// </summary>
        public ActionResult Index(string onayRed)
        {
            if (CheckPerm(Perms.SiparişOnaylama, PermTypes.Reading) == false) return Redirect("/");
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
        /// <summary>
        /// liste
        /// </summary>
        public PartialViewResult TumOrderList(string Tip, int bastarih, int bittarih)
        {
            ViewBag.Tip = Tip;
            ViewBag.bastarih = bastarih;
            ViewBag.bittarih = bittarih;
            return PartialView("List");
        }
        /// <summary>
        /// liste
        /// </summary>
        public string TumOrderListData(string tip, int bastarih, int bittarih)
        {
            var json = new JavaScriptSerializer()
            {
                MaxJsonLength = int.MaxValue
            };
            List<SipOnay> sipBilgi = new List<SipOnay>();

            try
            {
                var tbl = db.UserDetails.Where(m => m.UserID == vUser.Id).FirstOrDefault();
                if (tbl != null)
                {
                    var Sirketler = tbl.GosterilecekSirket ?? "";
                    var CHKAraligi = tbl.GostCHKKodAlani ?? "";
                    var TipKodlari = tbl.GostSTKDeger ?? "";
                    var Kod3Araligi = "";
                    var Kod3Araligi2 = "";
                    var RiskAraligi = "";
                    var RiskAraligi2 = "";

                    var Kod3Array = tbl.GostKod3OrtBakiye == null ? ";".Split(';') : tbl.GostKod3OrtBakiye.Split(';');
                    var RiskArray = tbl.GostRiskDeger == null ? ";".Split(';') : tbl.GostRiskDeger.Split(';');

                    if (Kod3Array.Length > 3)
                    {
                        Kod3Araligi = Kod3Array[0] + ";" + Kod3Array[1];
                        Kod3Araligi2 = Kod3Array[2] + ";" + Kod3Array[3];
                    }
                    else if (Kod3Array.Length > 1 && Kod3Array[0] != "")
                    {
                        Kod3Araligi = Kod3Array[0] + ";" + Kod3Array[1];
                        Kod3Araligi2 = "0;0";
                    }
                    else
                    {
                        Kod3Araligi = "0;9999999999999999";
                        Kod3Araligi2 = "0;0";
                    }

                    if (RiskArray.Length > 3)
                    {
                        RiskAraligi = RiskArray[0] + ";" + RiskArray[1];
                        RiskAraligi2 = RiskArray[2] + ";" + RiskArray[3];
                    }
                    else if (RiskArray.Length > 1 && RiskArray[0] != "")
                    {
                        RiskAraligi = RiskArray[0] + ";" + RiskArray[1];
                        RiskAraligi2 = "0;0";
                    }
                    else
                    {
                        RiskAraligi = "0;9999999999999999";
                        RiskAraligi2 = "0;0";
                    }

                    var Grup = vUser.RoleName;
                    // TODO: şirket ayrımı
                    if (Sirketler.Contains("Tüm;"))
                    {
                        if (Sirketler.Contains("Tümpa;"))
                        {
                            // TÜmAndTümpaProcedure Çağır
                            sipBilgi = db.Database.SqlQuery<SipOnay>(string.Format("[FINSAT6{0}].[wms].[TumAndTumpaSiparisOnayList] @OnayDurm='{1}', @Secim=0, @ChkAralik='{2}', @Sirketler='{3}', @TipKodlari='{4}',@Kod3Aralik='{5}',@RiskAralik='{6}', @Grup='{7}', @BasTarih={8}, @BitTarih={9},@Kod3Aralik2='{10}',@RiskAralik2='{11}'", "71", tip, CHKAraligi, Sirketler, TipKodlari, Kod3Araligi, RiskAraligi, Grup, bastarih, bittarih, Kod3Araligi2, RiskAraligi2)).ToList();
                        }
                        else
                        {
                            var sql = string.Format("[FINSAT6{0}].[wms].[TumSiparisOnayList] @OnayDurm='{1}', @Secim=0, @ChkAralik='{2}', @Sirketler='{3}', @TipKodlari='{4}',@Kod3Aralik='{5}',@RiskAralik='{6}', @Grup='{7}', @BasTarih={8}, @BitTarih={9},@Kod3Aralik2='{10}',@RiskAralik2='{11}'", "71", tip, CHKAraligi, Sirketler, TipKodlari, Kod3Araligi, RiskAraligi, Grup, bastarih, bittarih, Kod3Araligi2, RiskAraligi2);
                            // TÜm Procedure Çağır
                            sipBilgi = db.Database.SqlQuery<SipOnay>(sql).ToList();
                        }
                    }
                    else if (Sirketler.Contains("Tümpa;"))
                    {
                        // TÜmpa Procedure Çağır
                        sipBilgi = db.Database.SqlQuery<SipOnay>(string.Format("[FINSAT6{0}].[wms].[TumpaSiparisOnayList] @OnayDurm='{1}', @Secim=0, @ChkAralik='{2}', @Sirketler='{3}', @TipKodlari='{4}',@Kod3Aralik='{5}',@RiskAralik='{6}', @Grup='{7}', @BasTarih={8}, @BitTarih={9},@Kod3Aralik2='{10}',@RiskAralik2='{11}'", "71", tip, CHKAraligi, Sirketler, TipKodlari, Kod3Araligi, RiskAraligi, Grup, bastarih, bittarih, Kod3Araligi2, RiskAraligi2)).ToList();
                    }
                }
            }
            catch (Exception)
            {
            }

            return json.Serialize(sipBilgi);
        }
        /// <summary>
        /// onayla
        /// </summary>
        public JsonResult Onayla(string Data)
        {
            if (CheckPerm(Perms.SiparişOnaylama, PermTypes.Writing) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            var _Result = new Result(true);
            var parameters = JsonConvert.DeserializeObject<JArray>(Request["Data"]);
            var logDetay = "";
            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                var tbl = db.UserDetails.Where(m => m.UserID == vUser.Id).FirstOrDefault();
                foreach (JObject insertObj in parameters)
                {
                    // TODO: şirket ayrımı
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

                    var sip = new TumSiparisOnayLog()
                    {
                        Bakiye = insertObj["Bakiye"].ToDecimal(),
                        Unvan = insertObj["Unvan"].ToString(),
                        EvrakNo = insertObj["EvrakNo"].ToString(),
                        DegisTarih = (int)DateTime.Now.ToOADate(),
                        Degistiren = vUser.UserName.ToString(),
                        SirketAralik = tbl.GosterilecekSirket,
                        CHKAralik = tbl.GostCHKKodAlani,
                        TipKodlari = tbl.GostSTKDeger,
                        RiskBakiyeAralik = tbl.GostKod3OrtBakiye,
                        RiskAralik = tbl.GostRiskDeger,
                        Risk = insertObj["Risk"].ToDecimal(),
                        RoleName = vUser.RoleName,
                        Firma = insertObj["Firma"].ToString(),
                        GunIciSiparis = insertObj["GunIciSiparis"].ToDecimal(),
                        HesapKodu = insertObj["HesapKodu"].ToString(),
                        Kod2 = insertObj["Kod2"].ToString(),
                        Kod3OrtBakiye = insertObj["Kod3OrtBakiye"].ToDecimal(),
                        Kod3OrtGun = insertObj["Kod3OrtGun"].ToDecimal(),
                        KrediLimiti = insertObj["KrediLimiti"].ToDecimal(),
                        OnayDurumu = "Onaylandı",
                        OrtGun = insertObj["OrtGun"].ToDecimal(),
                        PRTBakiye = insertObj["PRTBakiye"].ToDecimal(),
                        SCek = insertObj["SCek"].ToDecimal(),
                        SicakSiparis = insertObj["SicakSiparis"].ToDecimal(),
                        SiparisTuru = insertObj["SiparisTuru"].ToString(),
                        SogukSiparis = insertObj["SogukSiparis"].ToDecimal(),
                        TCek = insertObj["TCek"].ToDecimal(),
                        TipKodu = insertObj["TipKodu"].ToString(),
                        RiskBakiyesi = insertObj["RiskBakiyesi"].ToDecimal()
                    };
                    logdb.TumSiparisOnayLogs.Add(sip);
                }

                try
                {
                    db.SaveChanges();
                    logdb.SaveChanges();
                    dbContextTransaction.Commit();

                    _Result.Status = true;
                    _Result.Message = "İşlem Başarılı ";
                }
                catch (Exception ex)
                {
                    Logger(ex, "TumOrder");
                    _Result.Status = false;
                    _Result.Message = "Hata Oluştu";
                }
            }

            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// reddet
        /// </summary>
        public JsonResult Reddet(string Data)
        {
            if (CheckPerm(Perms.SiparişOnaylama, PermTypes.Writing) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            var _Result = new Result(true);
            var parameters = JsonConvert.DeserializeObject<JArray>(Request["Data"]);
            var logDetay = "";
            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                var tbl = db.UserDetails.Where(m => m.UserID == vUser.Id).FirstOrDefault();
                using (var logdbContextTransaction = logdb.Database.BeginTransaction())
                {
                    foreach (JObject insertObj in parameters)
                    {
                        // TODO: şirket ayrımı
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

                        logdb.TumSiparisOnayLogs.Add(new TumSiparisOnayLog
                        {
                            Bakiye = insertObj["Bakiye"].ToDecimal(),
                            Unvan = insertObj["Unvan"].ToString(),
                            EvrakNo = insertObj["EvrakNo"].ToString(),
                            DegisTarih = (int)DateTime.Now.ToOADate(),
                            Degistiren = vUser.UserName.ToString(),
                            SirketAralik = tbl.GosterilecekSirket,
                            CHKAralik = tbl.GostCHKKodAlani,
                            TipKodlari = tbl.GostSTKDeger,
                            RiskBakiyeAralik = tbl.GostKod3OrtBakiye,
                            RiskAralik = tbl.GostRiskDeger,
                            Risk = insertObj["Risk"].ToDecimal(),
                            RoleName = vUser.RoleName,
                            Firma = insertObj["Firma"].ToString(),
                            GunIciSiparis = insertObj["GunIciSiparis"].ToDecimal(),
                            HesapKodu = insertObj["HesapKodu"].ToString(),
                            Kod2 = insertObj["Kod2"].ToString(),
                            Kod3OrtBakiye = insertObj["Kod3OrtBakiye"].ToDecimal(),
                            Kod3OrtGun = insertObj["Kod3OrtGun"].ToDecimal(),
                            KrediLimiti = insertObj["KrediLimiti"].ToDecimal(),
                            OnayDurumu = "Reddedildi",
                            OrtGun = insertObj["OrtGun"].ToDecimal(),
                            PRTBakiye = insertObj["PRTBakiye"].ToDecimal(),
                            SCek = insertObj["SCek"].ToDecimal(),
                            SicakSiparis = insertObj["SicakSiparis"].ToDecimal(),
                            SiparisTuru = insertObj["SiparisTuru"].ToString(),
                            SogukSiparis = insertObj["SogukSiparis"].ToDecimal(),
                            TCek = insertObj["TCek"].ToDecimal(),
                            TipKodu = insertObj["TipKodu"].ToString(),
                            RiskBakiyesi = insertObj["RiskBakiyesi"].ToDecimal()
                        });
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
                    catch (Exception)
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