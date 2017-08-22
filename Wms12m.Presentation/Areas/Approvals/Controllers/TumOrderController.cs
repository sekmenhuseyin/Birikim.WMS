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
    public class TumOrderController : RootController
    {
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
                var Sirketler = tbl.GosterilecekSirket;
                var CHKAraligi = tbl.GostCHKKodAlani;
                var TipKodlari = tbl.GostSTKDeger;
                var Kod3Araligi = tbl.GostKod3OrtBakiye;
                var RiskAraligi = tbl.GostRiskDeger;
                var Grup = vUser.RoleName;

                if (Sirketler.Contains("Tüm;"))
                {
                    if (Sirketler.Contains("Tümpa;"))
                    {
                        // TÜmAndTümpaProcedure Çağır
                        sipBilgi = db.Database.SqlQuery<SipOnay>(string.Format("[FINSAT6{0}].[wms].[TumAndTumpaSiparisOnayList] @OnayDurm='{1}', @Secim=0, @ChkAralik='{2}', @Sirketler='{3}', @TipKodlari='{4}',@Kod3Aralik='{5}',@RiskAralik='{6}', @Grup='{7}', @BasTarih={8}, @BitTarih={9}", "71", tip, CHKAraligi, Sirketler, TipKodlari, Kod3Araligi, RiskAraligi, Grup, bastarih, bittarih)).ToList();
                    }
                    else
                    {
                        // TÜm Procedure Çağır
                        sipBilgi = db.Database.SqlQuery<SipOnay>(string.Format("[FINSAT6{0}].[wms].[TumSiparisOnayList] @OnayDurm='{1}', @Secim=0, @ChkAralik='{2}', @Sirketler='{3}', @TipKodlari='{4}',@Kod3Aralik='{5}',@RiskAralik='{6}', @Grup='{7}', @BasTarih={8}, @BitTarih={9}", "71", tip, CHKAraligi, Sirketler, TipKodlari, Kod3Araligi, RiskAraligi, Grup, bastarih, bittarih)).ToList();
                    }
                }
                else if (Sirketler.Contains("Tümpa;"))
                {
                    // TÜmpa Procedure Çağır
                    sipBilgi = db.Database.SqlQuery<SipOnay>(string.Format("[FINSAT6{0}].[wms].[TumpaSiparisOnayList] @OnayDurm='{1}', @Secim=0, @ChkAralik='{2}', @Sirketler='{3}', @TipKodlari='{4}',@Kod3Aralik='{5}',@RiskAralik='{6}', @Grup='{7}', @BasTarih={8}, @BitTarih={9}", "71", tip, CHKAraligi, Sirketler, TipKodlari, Kod3Araligi, RiskAraligi, Grup, bastarih, bittarih)).ToList();
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
                }
                try
                {
                    db.SaveChanges();
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
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }


        public JsonResult Reddet(string Data)
        {
            Result _Result = new Result(true);
            JArray parameters = JsonConvert.DeserializeObject<JArray>(Request["Data"]);
            var logDetay = "";
            using (var dbContextTransaction = db.Database.BeginTransaction())
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
                }
                try
                {
                    db.SaveChanges();
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
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
    }
}