using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m.Presentation.Areas.Approvals.Controllers
{
    public class OrderController : RootController
    {
        #region genel sipariş
        /// <summary>
        /// SM sayfası
        /// </summary>
        public ActionResult SM()
        {
            if (CheckPerm(Perms.SiparişOnaylama, PermTypes.Reading) == false) return Redirect("/");
            var kOD = db.Database.SqlQuery<SiparisOnaySelect>(string.Format("[FINSAT6{0}].[wms].[SiparisOnayListSM]", vUser.SirketKodu)).ToList();
            ViewBag.OnayTip = 1;
            ViewBag.baslik = "SM";
            return View("Index", kOD);
        }
        /// <summary>
        /// SPGMY sayfası
        /// </summary>
        public ActionResult SPGMY()
        {
            if (CheckPerm(Perms.SiparişOnaylama, PermTypes.Reading) == false) return Redirect("/");
            var KOD = db.Database.SqlQuery<SiparisOnaySelect>(string.Format("[FINSAT6{0}].[wms].[SiparisOnayListSPGMY]", vUser.SirketKodu)).ToList();
            ViewBag.OnayTip = 2;
            ViewBag.baslik = "SPGMY";
            return View("Index", KOD);
        }
        /// <summary>
        /// GM sayfası
        /// </summary>
        public ActionResult GM()
        {
            if (CheckPerm(Perms.SiparişOnaylama, PermTypes.Reading) == false) return Redirect("/");
            var KOD = db.Database.SqlQuery<SiparisOnaySelect>(string.Format("[FINSAT6{0}].[wms].[SiparisOnayListGM]", vUser.SirketKodu)).ToList();
            ViewBag.OnayTip = 3;
            ViewBag.baslik = "GM";
            return View("Index", KOD);
        }
        /// <summary>
        /// Onaylama
        /// </summary>
        public JsonResult Onay(string Data, int OnayTip, bool OnaylandiMi)
        {
            if (CheckPerm(Perms.SiparişOnaylama, PermTypes.Writing) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            var parameters = JsonConvert.DeserializeObject<JArray>(Request["Data"]);
            string sql = "";
            foreach (string insertObj in parameters)
            {
                var OTip = db.Database.SqlQuery<short>(string.Format("SELECT OnayTip FROM FINSAT6{0}.FINSAT6{0}.SiparisOnay WHERE EvrakNo='{1}'", vUser.SirketKodu, insertObj)).FirstOrDefault();
                sql += string.Format("EXEC [FINSAT6{0}].[wms].[SP_SiparisOnay] @EvrakNo = '{1}', @Kullanici = '{2}', @OnaylayanTip={3}, @OnaylandiMi={4}, @OnayTip={5};", vUser.SirketKodu, insertObj, vUser.UserName, OnayTip, OnaylandiMi == true ? 1 : 0, OTip);
            }
            try
            {
                if (sql != "") db.Database.ExecuteSqlCommand(sql);
                return Json(new Result(true, OnayTip, "İşlem Başarılı "), JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new Result(false, "Hata Oluştu."), JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
        #region Tüm
        /// <summary>
        /// anasayfa
        /// </summary>
        public ActionResult Index()
        {
            if (CheckPerm(Perms.SiparişOnaylama, PermTypes.Reading) == false) return Redirect("/");
            return View("TumIndex");
        }
        /// <summary>
        /// liste
        /// </summary>
        public string List(string tip, int bastarih, int bittarih)
        {
            var json = new JavaScriptSerializer()
            {
                MaxJsonLength = int.MaxValue
            };
            try
            {
                var tbl = db.UserDetails.Where(m => m.UserID == vUser.Id).FirstOrDefault();
                if (tbl != null)
                {
                    var CHKAraligi = tbl.GostCHKKodAlani ?? "";
                    var TipKodlari = tbl.GostSTKDeger ?? "";
                    var Kod3Araligi = "";
                    var Kod3Araligi2 = "";
                    var RiskAraligi = "";
                    var RiskAraligi2 = "";
                    var Kod3Array = tbl.GostKod3OrtBakiye == null ? ";".Split(';') : tbl.GostKod3OrtBakiye.Split(';');
                    var RiskArray = tbl.GostRiskDeger == null ? ";".Split(';') : tbl.GostRiskDeger.Split(';');
                    tip = tip == "0" ? "Beklemede" : tip == "1" ? "Onaylandı" : "Reddedildi";
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
                    //run sql
                    var sql = string.Format("[FINSAT6{0}].[wms].[SiparisOnayList] @OnayDurm='{1}', @Secim=0, @ChkAralik='{2}', @TipKodlari='{3}',@Kod3Aralik='{4}',@RiskAralik='{5}', @Grup='{6}', @BasTarih={7}, @BitTarih={8}, @Kod3Aralik2='{9}', @RiskAralik2='{10}'", vUser.SirketKodu, tip, CHKAraligi, TipKodlari, Kod3Araligi, RiskAraligi, vUser.RoleName, bastarih, bittarih, Kod3Araligi2, RiskAraligi2);
                    var sipBilgi = db.Database.SqlQuery<SipOnay>(sql).ToList();
                    return json.Serialize(sipBilgi);
                }
            }
            catch (Exception)
            {
            }
            return json.Serialize(new List<SipOnay>());
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
            Exception exx = null;
            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                var tbl = db.UserDetails.Where(m => m.UserID == vUser.Id).FirstOrDefault();
                foreach (JObject insertObj in parameters)
                {
                    db.Database.ExecuteSqlCommand(string.Format("[FINSAT6{0}].[wms].[SiparisOnayla] @OncekiDurum = '{1}',@Kullanici = '{2}', @EvrakNo='{3}'", vUser.SirketKodu, insertObj["OnayDurumu"].ToString(), vUser.UserName, insertObj["EvrakNo"].ToString()));
                    logDetay = vUser.SirketKodu + ".SPI tablosunda Evrak Numarası '" + insertObj["EvrakNo"].ToString() + "' ve Onay Durumu '" + insertObj["OnayDurumu"].ToString() + "' olan satırların OnayDurumu " + vUser.UserName + " kullanıcısının siparişi onaylaması sonucu 'Onaylandı' olarak güncellenmiştir.";
                    LogActions("Approvals", "TumOrder", "Onayla", ComboItems.alOnayla, 0, logDetay);
                    dbl.TumSiparisOnayLogs.Add(new TumSiparisOnayLog()
                    {
                        Bakiye = insertObj["Bakiye"].ToDecimal(),
                        Unvan = insertObj["Unvan"].ToString(),
                        EvrakNo = insertObj["EvrakNo"].ToString(),
                        DegisTarih = (int)DateTime.Now.ToOADate(),
                        Degistiren = vUser.UserName,
                        SirketAralik = "",
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
                    });
                }

                try
                {
                    db.SaveChanges();
                    dbl.SaveChanges();
                    dbContextTransaction.Commit();
                    _Result.Status = true;
                    _Result.Message = "İşlem Başarılı ";
                }
                catch (DbEntityValidationException e)
                {
                    logDetay = "";
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        logDetay += string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:", eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            logDetay += string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    exx = e;
                    _Result.Status = false;
                    _Result.Message = "Hata Oluştu";
                }
                catch (Exception ex)
                {
                    exx = ex;
                    _Result.Status = false;
                    _Result.Message = "Hata Oluştu";
                }
            }
            if (exx != null) Logger(exx, "TumOrder", logDetay);
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
                using (var logdbContextTransaction = dbl.Database.BeginTransaction())
                {
                    foreach (JObject insertObj in parameters)
                    {
                        db.Database.ExecuteSqlCommand(string.Format("[FINSAT6{0}].[wms].[SiparisReddet] @OncekiDurum = '{1}',@Kullanici = '{2}', @EvrakNo='{3}'", vUser.SirketKodu, insertObj["OnayDurumu"].ToString(), vUser.UserName, insertObj["EvrakNo"].ToString()));
                        logDetay = vUser.SirketKodu + "SPI tablosunda Evrak Numarası '" + insertObj["EvrakNo"].ToString() + "' ve Onay Durumu '" + insertObj["OnayDurumu"].ToString() + "' olan satırların OnayDurumu " + vUser.UserName + " kullanıcısının siparişi reddetmesi sonucu 'Reddedildi' olarak güncellenmiştir.";
                        LogActions("Approvals", "TumOrder", "Reddet", ComboItems.alRed, 0, logDetay);
                        dbl.TumSiparisOnayLogs.Add(new TumSiparisOnayLog
                        {
                            Bakiye = insertObj["Bakiye"].ToDecimal(),
                            Unvan = insertObj["Unvan"].ToString(),
                            EvrakNo = insertObj["EvrakNo"].ToString(),
                            DegisTarih = (int)DateTime.Now.ToOADate(),
                            Degistiren = vUser.UserName.ToString(),
                            SirketAralik = "",
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
                        dbl.SaveChanges();
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
        #endregion
    }
}