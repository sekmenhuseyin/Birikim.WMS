using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m.Presentation.Controllers
{
    public class SiparisYetkiController : RootController
    {
        // GET: SiparisYetki
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult List()
        {
            if (CheckPerm(Perms.Kullanıcılar, PermTypes.Reading) == false) return null;
            List<UserDetail> list;
            List<SipOnayYetkiler> yetkiler = new List<SipOnayYetkiler>();
            if (vUser.Id == 1)
                list = db.UserDetails.ToList();
            else
                list = db.UserDetails.Where(m => m.UserID > 1).ToList();

            foreach (var item in list)
            {
                SipOnayYetkiler yetki = new SipOnayYetkiler();
                yetki.GostCHKKodAlani = item.GostCHKKodAlani;
                yetki.GosterilecekSirket = item.GosterilecekSirket;
                yetki.GostKod3OrtBakiye = item.GostKod3OrtBakiye;
                yetki.GostRiskDeger = item.GostRiskDeger;
                yetki.GostSTKDeger = item.GostSTKDeger;
                yetki.Kod = item.User.Kod;
                yetki.AdSoyad = item.User.AdSoyad;
                yetki.UserID = item.UserID;

                yetkiler.Add(yetki);
            }

            return PartialView("List", yetkiler);
        }

        public PartialViewResult YetkiDuzenle(int ID)
        {
            var tbl = db.UserDetails.Where(m => m.UserID == ID).FirstOrDefault();
            SipOnayYetkiler yetki = new SipOnayYetkiler();
            yetki.GostCHKKodAlani = tbl.GostCHKKodAlani;
            yetki.GosterilecekSirket = tbl.GosterilecekSirket;
            yetki.GostKod3OrtBakiye = tbl.GostKod3OrtBakiye;
            yetki.GostRiskDeger = tbl.GostRiskDeger;
            yetki.GostSTKDeger = tbl.GostSTKDeger;
            yetki.AdSoyad = tbl.User.AdSoyad;
            ViewBag.ID = ID;
            return PartialView("YetkiDuzenle", yetki);
        }

        public string TipKodSelect()
        {
            var KOD = db.Database.SqlQuery<RaporGetKod>(string.Format("[FINSAT6{0}].[wms].[DB_GetTipKod]", "71")).ToList();
            var json = new JavaScriptSerializer().Serialize(KOD);
            return json;
        }


        public JsonResult ParametreUpdate(string CHKAraligi, string Sirketler, string Tipler, string Kod3, string Risk, int ID)
        {
            Result _Result = new Result(true);
            if (CheckPerm(Perms.SözleşmeTanim, PermTypes.Writing) == false) return null;
            db.Database.ExecuteSqlCommand(string.Format("[BIRIKIM].[wms].[TumpaSiparisParametreOnayla] @CHKAraligi = '{0}',@Sirketler = '{1}', @Tipler='{2}',@Kod3 = '{3}', @Risk='{4}', @UserID={5}", CHKAraligi, Sirketler, Tipler, Kod3, Risk, ID));
            try
            {
                db.SaveChanges();
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
    }
}