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
            return PartialView("YetkiDuzenle", yetki);
        }

        public string TipKodSelect()
        {
            var KOD = db.Database.SqlQuery<RaporGetKod>(string.Format("[FINSAT6{0}].[wms].[DB_GetTipKod]", "71")).ToList();
            var json = new JavaScriptSerializer().Serialize(KOD);
            return json;
        }
    }
}